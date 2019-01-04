using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Smd.Authorization;
using Smd.Authorization.Roles;
using Smd.Authorization.Users;
using Smd.Zero.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SmdZeroServiceCollectionExtensions
    {
        public static SmdIdentityBuilder AddSmdIdentity<TUser, TRole>(this IServiceCollection services)
            where TRole : SmdRole<TUser>, new()
            where TUser : SmdUser<TUser>
        {
            return services.AddSmdIdentity< TUser, TRole>(setupAction: null);
        }

        public static SmdIdentityBuilder AddSmdIdentity< TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction) 
            where TRole : SmdRole<TUser>, new()
            where TUser : SmdUser<TUser>
        {
            services.AddSingleton<ISmdZeroEntityTypes>(new SmdZeroEntityTypes
            { 
                Role = typeof(TRole),
                User = typeof(TUser)
            });  

            //SmdRoleManager
            services.TryAddScoped<SmdRoleManager<TRole, TUser>>();
            services.TryAddScoped(typeof(RoleManager<TRole>), provider => provider.GetService(typeof(SmdRoleManager<TRole, TUser>)));

            //SmdUserManager
            services.TryAddScoped<SmdUserManager<TRole, TUser>>();
            services.TryAddScoped(typeof(UserManager<TUser>), provider => provider.GetService(typeof(SmdUserManager<TRole, TUser>)));

            //SignInManager
            services.TryAddScoped<SmdSignInManager<TRole, TUser>>();
            services.TryAddScoped(typeof(SignInManager<TUser>), provider => provider.GetService(typeof(SmdSignInManager<TRole, TUser>)));

            //SmdLogInManager
            services.TryAddScoped<SmdLogInManager<TRole, TUser>>();

            //SmdUserClaimsPrincipalFactory
            services.TryAddScoped<SmdUserClaimsPrincipalFactory<TUser, TRole>>();
            services.TryAddScoped(typeof(UserClaimsPrincipalFactory<TUser, TRole>), provider => provider.GetService(typeof(SmdUserClaimsPrincipalFactory<TUser, TRole>)));
            services.TryAddScoped(typeof(IUserClaimsPrincipalFactory<TUser>), provider => provider.GetService(typeof(SmdUserClaimsPrincipalFactory<TUser, TRole>)));

            //SmdSecurityStampValidator
            services.TryAddScoped<SmdSecurityStampValidator<TRole, TUser>>();
            services.TryAddScoped(typeof(SecurityStampValidator<TUser>), provider => provider.GetService(typeof(SmdSecurityStampValidator<TRole, TUser>)));
            services.TryAddScoped(typeof(ISecurityStampValidator), provider => provider.GetService(typeof(SmdSecurityStampValidator<TRole, TUser>)));

            //PermissionChecker
            services.TryAddScoped<PermissionChecker<TRole, TUser>>();
            services.TryAddScoped(typeof(IPermissionChecker), provider => provider.GetService(typeof(PermissionChecker<TRole, TUser>)));

            //SmdUserStore
            services.TryAddScoped<SmdUserStore<TRole, TUser>>();
            services.TryAddScoped(typeof(IUserStore<TUser>), provider => provider.GetService(typeof(SmdUserStore<TRole, TUser>)));

            //SmdRoleStore
            services.TryAddScoped<SmdRoleStore<TRole, TUser>>();
            services.TryAddScoped(typeof(IRoleStore<TRole>), provider => provider.GetService(typeof(SmdRoleStore<TRole, TUser>)));
             

            return new SmdIdentityBuilder(services.AddIdentity<TUser, TRole>(setupAction));
        }
    }
}
