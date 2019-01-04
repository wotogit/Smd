using Autofac;
using Smd.Configuration;
using Smd.EntityFramework.Linq;
using Smd.EntityFramework.Repositories;
using Smd.Infrastructure;
using Smd.Linq;
using Smd.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.EntityFramework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {  
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, SmdConfig config)
        {
            builder.RegisterGeneric(typeof( EfCoreRepositoryBase<,,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            builder.RegisterType<EfCoreAsyncQueryableExecuter>().As<IAsyncQueryableExecuter>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
