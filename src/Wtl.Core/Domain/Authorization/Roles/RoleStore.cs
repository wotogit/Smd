using Smd.Authorization.Roles;
using Smd.Domain.Repositories;
using Smd.Domain.Uow;
using Smd.Repositories;
using Wtl.Authorization.Users;

namespace Wtl.Authorization.Roles
{
    public class RoleStore : SmdRoleStore<Role, User>
    {
        public RoleStore( 
            IRepository<Role,long> roleRepository,
            IRepository<RolePermission, long> rolePermissionSettingRepository)
            : base( 
                roleRepository,
                rolePermissionSettingRepository)
        {

        }
    }
}
