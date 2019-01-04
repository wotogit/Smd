using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using Smd.Auditing;
using Smd.Authorization.Roles;
using Smd.Authorization.Users;
using Smd.Configuration; 
using Smd.Dependency;
using Smd.Domain.Repositories;
using Smd.Domain.Uow;
 
using Smd.IdentityFramework; 
using Smd.Timing;
using Smd.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Smd.Repositories;
using Smd.Infrastructure;

namespace Smd.Authorization
{
    public class SmdLogInManager<TRole, TUser> : ITransientDependency
        where TRole : SmdRole<TUser>, new()
        where TUser : SmdUser<TUser>
    {
        public IClientInfoProvider ClientInfoProvider { get; set; }

        protected SmdUserManager<TRole, TUser> UserManager { get; }
        protected ISettingManager SettingManager { get; }
        protected IRepository<UserLoginAttempt, long> UserLoginAttemptRepository { get; }
        protected IUserManagementConfig UserManagementConfig { get; } 
        protected SmdRoleManager<TRole, TUser> RoleManager { get; }

        private readonly IPasswordHasher<TUser> _passwordHasher;

        private readonly SmdUserClaimsPrincipalFactory<TUser, TRole> _claimsPrincipalFactory;

        public SmdLogInManager(
            SmdUserManager<TRole, TUser> userManager, 
            ISettingManager settingManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
            IUserManagementConfig userManagementConfig, 
            IPasswordHasher<TUser> passwordHasher,
            SmdRoleManager<TRole, TUser> roleManager,
            SmdUserClaimsPrincipalFactory<TUser, TRole> claimsPrincipalFactory)
        {
            _passwordHasher = passwordHasher;
            _claimsPrincipalFactory = claimsPrincipalFactory; 
            SettingManager = settingManager;
            UserLoginAttemptRepository = userLoginAttemptRepository;
            UserManagementConfig = userManagementConfig; 
            RoleManager = roleManager;
            UserManager = userManager;

           // ClientInfoProvider = NullClientInfoProvider.Instance;
        }

        // [UnitOfWork]
        public virtual async Task<SmdLoginResult<TUser>> LoginAsync(UserLoginInfo login)
        {
            var result = await LoginAsyncInternal(login);
            await SaveLoginAttempt(result, login.ProviderKey + "@" + login.LoginProvider);
            return result;
        }

        protected virtual async Task<SmdLoginResult<TUser>> LoginAsyncInternal(UserLoginInfo login)
        {
            if (login == null || login.LoginProvider.IsNullOrEmpty() || login.ProviderKey.IsNullOrEmpty())
            {
                throw new ArgumentException("login");
            }


            var user = await UserManager.FindAsync(login);
            if (user == null)
            {
                return new SmdLoginResult<TUser>(LoginResultType.未知的外部登录);
            }

            return await CreateLoginResultAsync(user);

        }

        //[UnitOfWork]
        public virtual async Task<SmdLoginResult<TUser>> LoginAsync(string userNameOrEmailAddress, string plainPassword,  bool shouldLockout = true)
        {
            var result = await LoginAsyncInternal(userNameOrEmailAddress, plainPassword, shouldLockout);
            await SaveLoginAttempt(result, userNameOrEmailAddress);
            return result;
        }

        protected virtual async Task<SmdLoginResult<TUser>> LoginAsyncInternal(string userNameOrEmailAddress, string plainPassword, bool shouldLockout)
        {
            if (userNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userNameOrEmailAddress));
            }

            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }

            await UserManager.InitializeOptionsAsync();

            //TryLoginFromExternalAuthenticationSources method may create the user, that's why we are calling it before SmdStore.FindByNameOrEmailAsync
           // var loggedInFromExternalSource = await TryLoginFromExternalAuthenticationSources(userNameOrEmailAddress, plainPassword);

            var user = await UserManager.FindByNameOrEmailAsync(userNameOrEmailAddress);
            if (user == null)
            {
                return new SmdLoginResult<TUser>(LoginResultType.用户名或邮箱地址错误);
            }

            if (await UserManager.IsLockedOutAsync(user))
            {
                return new SmdLoginResult<TUser>(LoginResultType.已锁定, user);
            }

            //if (!loggedInFromExternalSource)
            //{
                if (!await UserManager.CheckPasswordAsync(user, plainPassword))
                {
                    if (shouldLockout)
                    {
                        if (await TryLockOutAsync(user.Id))
                        {
                            return new SmdLoginResult<TUser>(LoginResultType.已锁定, user);
                        }
                    }

                    return new SmdLoginResult<TUser>(LoginResultType.用户名或邮箱地址错误, user);
                }

                await UserManager.ResetAccessFailedCountAsync(user);
           // }

            return await CreateLoginResultAsync(user);

        }

        protected virtual async Task<SmdLoginResult<TUser>> CreateLoginResultAsync(TUser user)
        {
            if (!user.IsActive)
            {
                return new SmdLoginResult<TUser>(LoginResultType.用户未激活);
            }

            if (await IsEmailConfirmationRequiredForLoginAsync() && !user.IsEmailConfirmed)
            {
                return new SmdLoginResult<TUser>(LoginResultType.用户邮箱未验证);
            }

            if (await IsPhoneConfirmationRequiredForLoginAsync() && !user.IsPhoneNumberConfirmed)
            {
                return new SmdLoginResult<TUser>(LoginResultType.用户手机未验证);
            }

            user.LastLoginTime = DateTime.Now;

            await UserManager.UpdateAsync(user);

            var principal = await _claimsPrincipalFactory.CreateAsync(user);

            return new SmdLoginResult<TUser>(
                user,
                principal.Identity as ClaimsIdentity
            );
        }

        protected virtual async Task SaveLoginAttempt(SmdLoginResult<TUser> loginResult,  string userNameOrEmailAddress)
        {


            var loginAttempt = new UserLoginAttempt
            {

                UserId = loginResult.User != null ? loginResult.User.Id : (long?)null,
                UserNameOrEmailAddress = userNameOrEmailAddress,

                Result = loginResult.Result,

                BrowserInfo = ClientInfoProvider.BrowserInfo,
                ClientIpAddress = ClientInfoProvider.ClientIpAddress,
                ClientName = ClientInfoProvider.ComputerName,
            };

            await UserLoginAttemptRepository.InsertAsync(loginAttempt);
        }

        protected virtual async Task<bool> TryLockOutAsync(  long userId)
        {

            var user = await UserManager.FindByIdAsync(userId.ToString());

            (await UserManager.AccessFailedAsync(user)).CheckErrors();

            var isLockOut = await UserManager.IsLockedOutAsync(user);

            return isLockOut;

        }

      
         

        protected virtual async Task<bool> IsEmailConfirmationRequiredForLoginAsync()
        {
            return false;// await SettingManager.GetSettingValueForApplicationAsync<bool>(SmdZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);
        }

        protected virtual Task<bool> IsPhoneConfirmationRequiredForLoginAsync()
        {
            return Task.FromResult(false);
        }
    }
}
