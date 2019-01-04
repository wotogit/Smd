using Autofac; 
using Smd.Authorization;
using Smd.Authorization.Roles;
using Smd.Authorization.Users;
using Smd.Configuration;
using Smd.Configuration.Startup;
using Smd.Infrastructure;
using Smd.Linq;
using Smd.Runtime.Caching;
using Smd.Runtime.Caching.Configuration;
using Smd.Runtime.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Wtl.Authorization;
using Wtl.Authorization.Roles;
using Wtl.Authorization.Users;
using Wtl.Identity;

namespace Wtl.Web.Core
{
    public class DependencyRegistrar : IDependencyRegistrar
    {


        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, SmdConfig config)
        {
            builder.RegisterGeneric(typeof(SmdRoleStore<,>)).As(typeof(Microsoft.AspNetCore.Identity.IRoleStore<>)).InstancePerLifetimeScope();
            builder.RegisterType<RoleStore>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SmdRoleManager<,>)).As(typeof(Microsoft.AspNetCore.Identity.RoleManager<>)).InstancePerLifetimeScope();
            builder.RegisterType<RoleManager>().InstancePerLifetimeScope();

            builder.RegisterType<UserStore>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(SmdUserManager<,>)).As(typeof(Microsoft.AspNetCore.Identity.UserManager<>)).InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SmdSignInManager<,>)).As(typeof(Microsoft.AspNetCore.Identity.SignInManager<>)).InstancePerLifetimeScope();
            builder.RegisterType<SignInManager>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SmdUserClaimsPrincipalFactory<,>)).As(typeof(Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory<,>)).InstancePerLifetimeScope(); 
            builder.RegisterType<UserClaimsPrincipalFactory>().InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(SmdSecurityStampValidator<,>)).As(typeof(Microsoft.AspNetCore.Identity.SecurityStampValidator<>)).InstancePerLifetimeScope();
            builder.RegisterType<SecurityStampValidator>().InstancePerLifetimeScope();

           // builder.RegisterGeneric(typeof(PermissionChecker)).As<IPermissionChecker>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionChecker>().As<IPermissionChecker>().InstancePerLifetimeScope();
          //  builder.RegisterType<PermissionChecker>().InstancePerLifetimeScope(); 
        }

        public int Order
        {
            get { return 2; }
        }
    }
}
