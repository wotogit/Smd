using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Smd.Authorization.Roles;
using Smd.Configuration;
using Smd.Domain.Repositories;
using Smd.Domain.Services;
using Smd.Json;
using Smd.Repositories;
using Smd.Runtime.Caching;
using Smd.Runtime.Session;
using Smd.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smd.Authorization.Users
{
    public class SmdUserManager<TRole, TUser> : UserManager<TUser>, IDomainService
         where TRole : SmdRole<TUser>, new()
         where TUser : SmdUser<TUser>
    {
        protected IUserPermissionStore<TUser> UserPermissionStore
        {
            get
            {
                if (!(Store is IUserPermissionStore<TUser>))
                {
                    throw new SmdException("Store is not IUserPermissionStore");
                }

                return Store as IUserPermissionStore<TUser>;
            }
        }
         

     //   protected string LocalizationSourceName { get; set; }

        public ISmdSession SmdSession { get; set; }
         

        protected SmdRoleManager<TRole, TUser> RoleManager { get; }

        protected SmdUserStore<TRole, TUser> SmdStore { get; }
         

        private readonly IPermissionManager _permissionManager; 
        private readonly ICacheManager _cacheManager;   
        private readonly ISettingManager _settingManager;
        private readonly IOptions<IdentityOptions> _optionsAccessor;

        public SmdUserManager(
            SmdRoleManager<TRole, TUser> roleManager,
            SmdUserStore<TRole, TUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<TUser>> logger,
            IPermissionManager permissionManager, 
            ICacheManager cacheManager ,
            ISettingManager settingManager)
            : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {
            _permissionManager = permissionManager; 
            _cacheManager = cacheManager; 
            _settingManager = settingManager;
            _optionsAccessor = optionsAccessor;

            SmdStore = store;
            RoleManager = roleManager; 
          //  LocalizationSourceName = SmdZeroConsts.LocalizationSourceName;
        }

        public override async Task<IdentityResult> CreateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.EmailAddress);
            if (!result.Succeeded)
            {
                return result;
            }

            var isLockoutEnabled = false; //user.IsLockoutEnabled;

            var identityResult = await base.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await SetLockoutEnabledAsync(user, isLockoutEnabled);
            }

            return identityResult;
        }

        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="permissionName">Permission name</param>
        public virtual async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return await IsGrantedAsync(
                userId,
                _permissionManager.GetPermission(permissionName)
                );
        }

        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permission">Permission</param>
        public virtual Task<bool> IsGrantedAsync(TUser user, Permission permission)
        {
            return IsGrantedAsync(user.Id, permission);
        }

        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="permission">Permission</param>
        public virtual async Task<bool> IsGrantedAsync(long userId, Permission permission)
        { 
             
            //Get cached user permissions
            var cacheItem = await GetUserPermissionCacheItemAsync(userId);
            if (cacheItem == null)
            {
                return false;
            }

            //Check for user-specific value
            if (cacheItem.GrantedPermissions.Contains(permission.Name))
            {
                return true;
            }

            if (cacheItem.ProhibitedPermissions.Contains(permission.Name))
            {
                return false;
            }

            //Check for roles
            foreach (var roleId in cacheItem.RoleIds)
            {
                if (await RoleManager.IsGrantedAsync(roleId, permission))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets granted permissions for a user.
        /// </summary>
        /// <param name="user">Role</param>
        /// <returns>List of granted permissions</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(TUser user)
        {
            var permissionList = new List<Permission>();

            foreach (var permission in _permissionManager.GetAllPermissions())
            {
                if (await IsGrantedAsync(user.Id, permission))
                {
                    permissionList.Add(permission);
                }
            }

            return permissionList;
        }

        /// <summary>
        /// Sets all granted permissions of a user at once.
        /// Prohibits all other permissions.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="permissions">Permissions</param>
        public virtual async Task SetGrantedPermissionsAsync(TUser user, IEnumerable<Permission> permissions)
        {
            var oldPermissions = await GetGrantedPermissionsAsync(user);
            var newPermissions = permissions.ToArray();

            foreach (var permission in oldPermissions.Where(p => !newPermissions.Contains(p)))
            {
                await ProhibitPermissionAsync(user, permission);
            }

            foreach (var permission in newPermissions.Where(p => !oldPermissions.Contains(p)))
            {
                await GrantPermissionAsync(user, permission);
            }
        }

        /// <summary>
        /// Prohibits all permissions for a user.
        /// </summary>
        /// <param name="user">User</param>
        public async Task ProhibitAllPermissionsAsync(TUser user)
        {
            foreach (var permission in _permissionManager.GetAllPermissions())
            {
                await ProhibitPermissionAsync(user, permission);
            }
        }

        /// <summary>
        /// Resets all permission settings for a user.
        /// It removes all permission settings for the user.
        /// User will have permissions according to his roles.
        /// This method does not prohibit all permissions.
        /// For that, use <see cref="ProhibitAllPermissionsAsync"/>.
        /// </summary>
        /// <param name="user">User</param>
        public async Task ResetAllPermissionsAsync(TUser user)
        {
            await UserPermissionStore.RemoveAllPermissionSettingsAsync(user);
        }

        /// <summary>
        /// Grants a permission for a user if not already granted.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permission">Permission</param>
        public virtual async Task GrantPermissionAsync(TUser user, Permission permission)
        {
            await UserPermissionStore.RemovePermissionAsync(user, new PermissionGrantInfo(permission.Name, false));

            if (await IsGrantedAsync(user.Id, permission))
            {
                return;
            }

            await UserPermissionStore.AddPermissionAsync(user, new PermissionGrantInfo(permission.Name, true));
        }

        /// <summary>
        /// Prohibits a permission for a user if it's granted.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permission">Permission</param>
        public virtual async Task ProhibitPermissionAsync(TUser user, Permission permission)
        {
            await UserPermissionStore.RemovePermissionAsync(user, new PermissionGrantInfo(permission.Name, true));

            if (!await IsGrantedAsync(user.Id, permission))
            {
                return;
            }

            await UserPermissionStore.AddPermissionAsync(user, new PermissionGrantInfo(permission.Name, false));
        }

        public virtual Task<TUser> FindByNameOrEmailAsync(string userNameOrEmailAddress)
        {
            return SmdStore.FindByNameOrEmailAsync(userNameOrEmailAddress);
        }

        public virtual Task<List<TUser>> FindAllAsync(UserLoginInfo login)
        {
            return SmdStore.FindAllAsync(login);
        }

        public virtual Task<TUser> FindAsync( UserLoginInfo login)
        {
            return SmdStore.FindAsync( login);
        }
 
        /// <summary>
        /// Gets a user by given id.
        /// Throws exception if no user found with given id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        /// <exception cref="SmdException">Throws exception if no user found with given id</exception>
        public virtual async Task<TUser> GetUserByIdAsync(long userId)
        {
            var user = await FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new SmdException("There is no user with id: " + userId);
            }

            return user;
        }

        public override async Task<IdentityResult> UpdateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.EmailAddress);
            if (!result.Succeeded)
            {
                return result;
            }

            //Admin user's username can not be changed!
            if (user.UserName != SmdUserBase.AdminUserName)
            {
                if ((await GetOldUserNameAsync(user.Id)) == SmdUserBase.AdminUserName)
                {
                    throw new UserFriendlyException("为能修改管理员用户名："+ SmdUserBase.AdminUserName);
                }
            }

            return await base.UpdateAsync(user);
        }

        public override async Task<IdentityResult> DeleteAsync(TUser user)
        {
            if (user.UserName == SmdUserBase.AdminUserName)
            {
                throw new UserFriendlyException("不能删除管理员："+ SmdUserBase.AdminUserName);
            }

            return await base.DeleteAsync(user);
        }

        public virtual async Task<IdentityResult> ChangePasswordAsync(TUser user, string newPassword)
        {
            var errors = new List<IdentityError>();

            foreach (var validator in PasswordValidators)
            {
                var validationResult = await validator.ValidateAsync(this, user, newPassword);
                if (!validationResult.Succeeded)
                {
                    errors.AddRange(validationResult.Errors);
                }
            }

            if (errors.Any())
            {
                return IdentityResult.Failed(errors.ToArray());
            }

            await SmdStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(user, newPassword));
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> CheckDuplicateUsernameOrEmailAddressAsync(long? expectedUserId, string userName, string emailAddress)
        {
            var user = (await FindByNameAsync(userName));
            if (user != null && user.Id != expectedUserId)
            {
                throw new UserFriendlyException(string.Format("名字{0}已被占用", userName));
            }

            user = (await FindByEmailAsync(emailAddress));
            if (user != null && user.Id != expectedUserId)
            {
                throw new UserFriendlyException(string.Format("邮箱地址 '{0}' 已被占用", emailAddress));
            }

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> SetRoles(TUser user, string[] roleNames)
        {
            await SmdStore.UserRepository.EnsureCollectionLoadedAsync(user, u => u.Roles);

            //Remove from removed roles
            foreach (var userRole in user.Roles.ToList())
            {
                var role = await RoleManager.FindByIdAsync(userRole.RoleId.ToString());
                if (roleNames.All(roleName => role.Name != roleName))
                {
                    var result = await RemoveFromRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }

            //Add to added roles
            foreach (var roleName in roleNames)
            {
                var role = await RoleManager.GetRoleByNameAsync(roleName);
                if (user.Roles.All(ur => ur.RoleId != role.Id))
                {
                    var result = await AddToRoleAsync(user, roleName);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }

            return IdentityResult.Success;
        }

        
         

        public virtual async Task InitializeOptionsAsync()
        {
            Options = JsonConvert.DeserializeObject<IdentityOptions>(_optionsAccessor.Value.ToJsonString());

            //Lockout
            Options.Lockout.AllowedForNewUsers = false;// await IsTrueAsync(SmdZeroSettingNames.UserManagement.UserLockOut.IsEnabled, tenantId);
            Options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0,5,0) ; //TimeSpan.FromSeconds(await GetSettingValueAsync<int>(SmdZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds, tenantId));
            Options.Lockout.MaxFailedAccessAttempts = 5;// await GetSettingValueAsync<int>(SmdZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, tenantId);

            //Password complexity
            //数字
            Options.Password.RequireDigit = false;// await GetSettingValueAsync<bool>(SmdZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit, tenantId);
            //小定字母
            Options.Password.RequireLowercase = false;//await GetSettingValueAsync<bool>(SmdZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase, tenantId);
            //需要非字母数字
            Options.Password.RequireNonAlphanumeric = false;//await GetSettingValueAsync<bool>(SmdZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric, tenantId);
            //大写字母
            Options.Password.RequireUppercase = false;//await GetSettingValueAsync<bool>(SmdZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase, tenantId);
            Options.Password.RequiredLength = 6;// await GetSettingValueAsync<int>(SmdZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength, tenantId);

           await  Task.FromResult(0);
        }

        protected virtual Task<string> GetOldUserNameAsync(long userId)
        {
            return SmdStore.GetUserNameFromDatabaseAsync(userId);
        }

        private async Task<UserPermissionCacheItem> GetUserPermissionCacheItemAsync(long userId)
        {
            var cacheKey = userId + "@" +0;
            return await _cacheManager.GetUserPermissionCache().GetAsync(cacheKey, async () =>
            {
                var user = await FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return null;
                }

                var newCacheItem = new UserPermissionCacheItem(userId);

                foreach (var roleName in await GetRolesAsync(user))
                {
                    newCacheItem.RoleIds.Add((await RoleManager.GetRoleByNameAsync(roleName)).Id);
                }

                foreach (var permissionInfo in await UserPermissionStore.GetPermissionsAsync(userId))
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPermissions.Add(permissionInfo.Name);
                    }
                    else
                    {
                        newCacheItem.ProhibitedPermissions.Add(permissionInfo.Name);
                    }
                }

                return newCacheItem;
            });
        }

        public override async Task<IList<string>> GetValidTwoFactorProvidersAsync(TUser user)
        {
            var providers = new List<string>();

            foreach (var provider in await base.GetValidTwoFactorProvidersAsync(user))
            {
                //if (provider == "Email" &&
                //    !await IsTrueAsync(SmdZeroSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled, user.TenantId))
                //{
                //    continue;
                //}

                //if (provider == "Phone" &&
                //    !await IsTrueAsync(SmdZeroSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled, user.TenantId))
                //{
                //    continue;
                //}
                continue;

                providers.Add(provider);
            }

            return providers;
        }

        private bool IsTrue(string settingName)
        {
            return GetSettingValue<bool>(settingName);
        }

        private Task<bool> IsTrueAsync(string settingName)
        {
            return GetSettingValueAsync<bool>(settingName);
        }

        private T GetSettingValue<T>(string settingName) where T : struct
        {
            return _settingManager.GetSettingValueForApplication<T>(settingName);
        }

        private Task<T> GetSettingValueAsync<T>(string settingName) where T : struct
        {
            return   _settingManager.GetSettingValueForApplicationAsync<T>(settingName);
        }

       
    }
}
