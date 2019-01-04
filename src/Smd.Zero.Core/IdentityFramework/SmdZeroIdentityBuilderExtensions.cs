 
using Smd.Authorization;
using Microsoft.AspNetCore.Identity;
using Smd.Authorization.Users;
using Smd.Authorization.Roles; 

// ReSharper disable once CheckNamespace - This is done to add extension methods to Microsoft.Extensions.DependencyInjection namespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class SmdZeroIdentityBuilderExtensions
    { 
         

        public static SmdIdentityBuilder AddSmdRoleManager<TRoleManager>(this SmdIdentityBuilder builder)
            where TRoleManager : class
        {
            var abpManagerType = typeof(SmdRoleManager<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var managerType = typeof(RoleManager<>).MakeGenericType(builder.RoleType);
            builder.Services.AddScoped(abpManagerType, services => services.GetRequiredService(managerType));
            builder.AddRoleManager<TRoleManager>();
            return builder;
        }

        public static SmdIdentityBuilder AddSmdUserManager<TUserManager>(this SmdIdentityBuilder builder)
            where TUserManager : class
        {
            var abpManagerType = typeof(SmdUserManager<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var managerType = typeof(UserManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(abpManagerType, services => services.GetRequiredService(managerType));
            builder.AddUserManager<TUserManager>();
            return builder;
        }

        public static SmdIdentityBuilder AddSmdSignInManager<TSignInManager>(this SmdIdentityBuilder builder)
            where TSignInManager : class
        {
            var abpManagerType = typeof(SmdSignInManager<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var managerType = typeof(SignInManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(abpManagerType, services => services.GetRequiredService(managerType));
            builder.AddSignInManager<TSignInManager>();
            return builder;
        }

        public static SmdIdentityBuilder AddSmdLogInManager<TLogInManager>(this SmdIdentityBuilder builder)
            where TLogInManager : class
        {
            var type = typeof(TLogInManager);
            var abpManagerType = typeof(SmdLogInManager<,>).MakeGenericType( builder.RoleType, builder.UserType);
            builder.Services.AddScoped(type, provider => provider.GetService(abpManagerType));
            builder.Services.AddScoped(abpManagerType, type);
            return builder;
        }

        public static SmdIdentityBuilder AddSmdUserClaimsPrincipalFactory<TUserClaimsPrincipalFactory>(this SmdIdentityBuilder builder)
            where TUserClaimsPrincipalFactory : class
        {
            var type = typeof(TUserClaimsPrincipalFactory);
            builder.Services.AddScoped(typeof(UserClaimsPrincipalFactory<,>).MakeGenericType(builder.UserType, builder.RoleType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(SmdUserClaimsPrincipalFactory<,>).MakeGenericType(builder.UserType, builder.RoleType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(IUserClaimsPrincipalFactory<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(type);
            return builder;
        }

        public static SmdIdentityBuilder AddSmdSecurityStampValidator<TSecurityStampValidator>(this SmdIdentityBuilder builder)
            where TSecurityStampValidator : class, ISecurityStampValidator
        {
            var type = typeof(TSecurityStampValidator);
            builder.Services.AddScoped(typeof(SecurityStampValidator<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(SmdSecurityStampValidator<,>).MakeGenericType(builder.RoleType, builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(ISecurityStampValidator), services => services.GetRequiredService(type));
            builder.Services.AddScoped(type);
            return builder;
        }

        public static SmdIdentityBuilder AddPermissionChecker<TPermissionChecker>(this SmdIdentityBuilder builder)
            where TPermissionChecker : class
        {
            var type = typeof(TPermissionChecker);
            var checkerType = typeof(PermissionChecker<,>).MakeGenericType(builder.RoleType, builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(checkerType, provider => provider.GetService(type));
            builder.Services.AddScoped(typeof(IPermissionChecker), provider => provider.GetService(type));
            return builder;
        }

        public static SmdIdentityBuilder AddSmdUserStore<TUserStore>(this SmdIdentityBuilder builder)
            where TUserStore : class
        {
            var type = typeof(TUserStore);
            var abpStoreType = typeof(SmdUserStore<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var storeType = typeof(IUserStore<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(abpStoreType, services => services.GetRequiredService(type));
            builder.Services.AddScoped(storeType, services => services.GetRequiredService(type));
            return builder;
        }

        public static SmdIdentityBuilder AddSmdRoleStore<TRoleStore>(this SmdIdentityBuilder builder)
            where TRoleStore : class
        {
            var type = typeof(TRoleStore);
            var abpStoreType = typeof(SmdRoleStore<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var storeType = typeof(IRoleStore<>).MakeGenericType(builder.RoleType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(abpStoreType, services => services.GetRequiredService(type));
            builder.Services.AddScoped(storeType, services => services.GetRequiredService(type));
            return builder;
        }
 
    }
}
