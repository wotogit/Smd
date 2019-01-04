using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Smd.Authorization.Roles;
using Smd.Authorization.Users;
using Smd.Dependency; 
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Smd.Authorization
{
    public class SmdUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>, ITransientDependency
        where TRole : SmdRole<TUser>, new()
        where TUser : SmdUser<TUser>
    {
        public SmdUserClaimsPrincipalFactory(
            SmdUserManager<TRole, TUser> userManager,
            SmdRoleManager<TRole, TUser> roleManager,
            IOptions<IdentityOptions> optionsAccessor
            ) : base(userManager, roleManager, optionsAccessor)
        {

        }

       // [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await base.CreateAsync(user);

            //if (user.TenantId.HasValue)
            //{
            //    principal.Identities.First().AddClaim(new Claim(SmdClaimTypes.TenantId,user.TenantId.ToString()));
            //}

            return principal;
        }
    }
}