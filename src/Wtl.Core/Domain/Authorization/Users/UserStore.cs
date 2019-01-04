using Smd.Authorization.Users;
using Smd.Domain.Repositories;
using Smd.Domain.Uow;
using Smd.Linq;
using Smd.Repositories;
using Wtl.Authorization.Roles;

namespace Wtl.Authorization.Users
{
    /// <summary>
    /// Used to perform database operations for <see cref="UserManager"/>.
    /// </summary>
    public class UserStore : SmdUserStore<Role, User>
    {
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role,long> roleRepository,
            IAsyncQueryableExecuter asyncQueryableExecuter,  
            IRepository<UserClaim, long> userCliamRepository,
            IRepository<UserPermission, long> userPermissionSettingRepository)
            : base( 
                userRepository,
                roleRepository,
                asyncQueryableExecuter,
                userRoleRepository,
                userLoginRepository,
                userCliamRepository,
                userPermissionSettingRepository)
        {
        }
    }
}