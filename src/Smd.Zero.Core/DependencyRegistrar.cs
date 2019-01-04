using Autofac;
using Smd.Authorization;
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

namespace Smd.Zero.Core
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
       

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, SmdConfig config)
        {
           
            builder.RegisterType<CachingConfiguration>().As<ICachingConfiguration>().InstancePerLifetimeScope();
            builder.RegisterType<SmdMemoryCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<SettingsConfiguration>().As<ISettingsConfiguration>().SingleInstance();
            builder.RegisterType<SettingDefinitionManager>().As<ISettingDefinitionManager>().InstancePerLifetimeScope(); 
            builder.RegisterGeneric(typeof(TypedCacheWrapper<,>)).As(typeof(ITypedCache<,>)).InstancePerLifetimeScope();
            builder.RegisterType<SettingManager>().As<ISettingManager>().InstancePerLifetimeScope();

           
            builder.RegisterType<PermissionManager>().As<IPermissionManager>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
