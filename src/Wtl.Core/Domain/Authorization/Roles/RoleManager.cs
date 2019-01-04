using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smd.Authorization;  
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Wtl.Authorization.Users;
using Smd.Authorization.Roles;
using Smd.Runtime.Caching;
using Smd.UI;

namespace Wtl.Authorization.Roles
{
    /// <summary>
    /// Role manager.
    /// Used to implement domain logic for roles.
    /// </summary>
    public class RoleManager : SmdRoleManager<Role, User>
    { 

        public RoleManager(
            RoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager> logger,
            IPermissionManager permissionManager, 
            ICacheManager cacheManager )
            : base(
                store,
                roleValidators,
                keyNormalizer,
                errors,
                logger,
                permissionManager,
                cacheManager )
        { 
        }

        public override Task SetGrantedPermissionsAsync(Role role, IEnumerable<Permission> permissions)
        {
            CheckPermissionsToUpdate(role, permissions);

            return base.SetGrantedPermissionsAsync(role, permissions);
        }

        private void CheckPermissionsToUpdate(Role role, IEnumerable<Permission> permissions)
        {
            if (role.Name == StaticRoleNames.Host.Admin &&
                (!permissions.Any(p => p.Name == AppPermissions.Pages_Administration_Roles_Edit) ||
                 !permissions.Any(p => p.Name == AppPermissions.Pages_Administration_Users_ChangePermissions)))
            {
                throw new UserFriendlyException("不能移除管理员角色");
            }
        }
         
    }
}