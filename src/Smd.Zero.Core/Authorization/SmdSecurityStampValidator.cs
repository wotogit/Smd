using System.Threading.Tasks;
using Smd.Authorization.Roles;
using Smd.Authorization.Users; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Smd.Authorization
{
    public class SmdSecurityStampValidator< TRole, TUser> : SecurityStampValidator<TUser> 
        where TRole : SmdRole<TUser>, new()
        where TUser : SmdUser<TUser>
    {
        public SmdSecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SmdSignInManager<TRole, TUser> signInManager,
            ISystemClock systemClock)
            : base(
                options, 
                signInManager,
                systemClock)
        {
        } 
        public override Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            return base.ValidateAsync(context);
        }
    }
}
