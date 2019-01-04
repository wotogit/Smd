using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
 
using System;
using System.Collections.Generic;
using System.Text;
using Wtl.Authorization;
using Wtl.Authorization.Roles;
using Wtl.Authorization.Users;  


namespace Wtl.Identity
{ 
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
          services.AddLogging();

            return services.AddSmdIdentity<User, Role>(options =>
            {
               // options.Tokens.ProviderMap[GoogleAuthenticatorProvider.Name] = new TokenProviderDescriptor(typeof(GoogleAuthenticatorProvider));
            }) 
                .AddSmdUserManager<UserManager>()
                .AddSmdRoleManager<RoleManager>() 
                .AddSmdUserStore<UserStore>()
                .AddSmdRoleStore<RoleStore>()
                .AddSmdSignInManager<SignInManager>()
                .AddSmdUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddSmdSecurityStampValidator<SecurityStampValidator>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
