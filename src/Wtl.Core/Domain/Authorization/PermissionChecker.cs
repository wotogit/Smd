using Smd.Authorization; 
using Wtl.Authorization.Roles;
using Wtl.Authorization.Users;

namespace Wtl.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
