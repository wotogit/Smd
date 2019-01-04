using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smd;
using Smd.Authorization;
using Smd.Authorization.Users;
using Smd.Configuration;
using Smd.Domain.Repositories;
using Smd.Domain.Uow;  
using Smd.Runtime.Caching;
using Smd.Threading;
using Smd.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wtl.Authorization.Roles; 

namespace Wtl.Authorization.Users
{
    /// <summary>
    /// User manager.
    /// Used to implement domain logic for users.
    /// Extends <see cref="SmdUserManager{TRole,TUser}"/>.
    /// </summary>
    public class UserManager : SmdUserManager<Role, User>
    {  

        public UserManager(
            UserStore userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager> logger,
            RoleManager roleManager,
            IPermissionManager permissionManager, 
            ICacheManager cacheManager, 
            ISettingManager settingManager)
            : base(
                  roleManager,
                  userStore,
                  optionsAccessor,
                  passwordHasher,
                  userValidators,
                  passwordValidators,
                  keyNormalizer,
                  errors,
                  services,
                  logger,
                  permissionManager, 
                  cacheManager, 
                  settingManager)
        {  
        }

       // [UnitOfWork]
        public virtual async Task<User> GetUserOrNullAsync(long userId)
        {
            return await FindByIdAsync(userId.ToString());
        }

        public User GetUserOrNull(long userId)
        {
            return AsyncHelper.RunSync(() => GetUserOrNullAsync(userId));
        }

        public async Task<User> GetUserAsync(long userId)
        {
            var user = await GetUserOrNullAsync(userId);
            if (user == null)
            {
                throw new Exception("There is no user: " + userId);
            }

            return user;
        }

        public User GetUser(long userId)
        {
            return AsyncHelper.RunSync(() => GetUserAsync(userId));
        }

        public override Task<IdentityResult> SetRoles(User user, string[] roleNames)
        {
            if (user.Name.ToStr().ToLower() == "admin" && !roleNames.Contains(StaticRoleNames.Host.Admin))
            {
                throw new UserFriendlyException("无法移除管理员账号");
            }

            return base.SetRoles(user, roleNames);
        }

        public override async Task SetGrantedPermissionsAsync(User user, IEnumerable<Permission> permissions)
        {
            CheckPermissionsToUpdate(user, permissions);

            await base.SetGrantedPermissionsAsync(user, permissions);
        }

        private void CheckPermissionsToUpdate(User user, IEnumerable<Permission> permissions)
        {
            if (user.Name == SmdUserBase.AdminUserName &&
                (!permissions.Any(p => p.Name == AppPermissions.Pages_Administration_Roles_Edit) ||
                !permissions.Any(p => p.Name == AppPermissions.Pages_Administration_Users_ChangePermissions)))
            {
                throw new UserFriendlyException("不能从管理员用户删除用户角色权限");
            }
        }
         
    }
}