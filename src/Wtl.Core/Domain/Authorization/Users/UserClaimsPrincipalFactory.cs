 
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Smd.Authorization;
using Wtl.Authorization.Roles;

namespace Wtl.Authorization.Users
{
    public class UserClaimsPrincipalFactory : SmdUserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
        }
    }
}
