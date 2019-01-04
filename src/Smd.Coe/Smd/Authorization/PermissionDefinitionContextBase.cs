using Smd.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Authorization
{
    public abstract class PermissionDefinitionContextBase : IPermissionDefinitionContext
    {
        protected readonly PermissionDictionary Permissions;

        protected PermissionDefinitionContextBase()
        {
            Permissions = new PermissionDictionary();
        }

        public Permission CreatePermission(
            string name,
            string displayName = null,
            string description = null )
        {
            if (Permissions.ContainsKey(name))
            {
                throw new SmdException("There is already a permission with name: " + name);
            }

            var permission = new Permission(name, displayName, description);
            Permissions[permission.Name] = permission;
            return permission;
        }

        public Permission GetPermissionOrNull(string name)
        {
            return Permissions.GetOrDefault(name);
        }
    }
}
