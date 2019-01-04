using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smd.Authorization;
using Smd.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Wtl.Authorization.Roles;
using Wtl.Authorization.Users;

namespace Wtl.Identity
{
    public class SignInManager : SmdSignInManager<Role, User>
    {
        public SignInManager(
            UserManager userManager,
            IHttpContextAccessor contextAccessor,
            UserClaimsPrincipalFactory claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger, 
            ISettingManager settingManager,
            IAuthenticationSchemeProvider schemes
            ) : base(
                userManager,
                contextAccessor,
                claimsFactory,
                optionsAccessor,
                logger, 
                settingManager,
                schemes)
        {
        }
    }
}
