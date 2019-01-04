using Smd.Collections;
using Smd.Collections.Extensions;
using Smd.Configuration;
using Smd.Dependency;
using Smd.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Smd.Authorization
{
    /// <summary>
    /// Permission manager.
    /// </summary>
   public   class PermissionManager : PermissionDefinitionContextBase, IPermissionManager, ISingletonDependency
    {
        public ISmdSession SmdSession { get; set; }

 
        private readonly IAuthorizationConfiguration _authorizationConfiguration;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PermissionManager( 
            IAuthorizationConfiguration authorizationConfiguration)
        { 
            _authorizationConfiguration = authorizationConfiguration;

            SmdSession = NullSmdSession.Instance;
        }

        public void Initialize()
        {
            //foreach (var providerType in _authorizationConfiguration.Providers)
            //{
            //    using (var provider = _iocManager.ResolveAsDisposable<AuthorizationProvider>(providerType))
            //    {
            //        provider.Object.SetPermissions(this);
            //    }
            //}

            Permissions.AddAllPermissions();
        }

        public Permission GetPermission(string name)
        {
            var permission = Permissions.GetOrDefault(name);
            if (permission == null)
            {
                throw new SmdException("There is no permission with name: " + name);
            }

            return permission;
        }

        public IReadOnlyList<Permission> GetAllPermissions()
        { 
           return Permissions.Values.ToImmutableList();
            
        }
         
    }
}
