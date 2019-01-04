 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Smd.Authorization;
using Wtl.Authorization.Roles;
using Wtl.Authorization.Users; 

namespace Wtl.Identity
{
    public class SecurityStampValidator : SmdSecurityStampValidator<Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, 
            SignInManager signInManager,
            ISystemClock systemClock) 
            : base(options, signInManager, systemClock)
        {
        }
    }
}