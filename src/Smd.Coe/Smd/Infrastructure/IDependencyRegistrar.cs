using Autofac;
using Smd.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Infrastructure
{
    /// <summary>
    /// Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, SmdConfig config);

        /// <summary>
        /// Gets order of this dependency registrar implementation,越小越先注册
        /// </summary>
        int Order { get; }
    }
}
