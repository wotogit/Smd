using System;
using System.Reflection;
using Smd.Authorization.Roles;
using Smd.Authorization.Users; 

namespace Smd.Zero.Configuration
{
    public class SmdZeroEntityTypes : ISmdZeroEntityTypes
    {
        public Type User
        {
            get { return _user; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof (SmdUserBase).IsAssignableFrom(value))
                {
                    throw new SmdException(value.AssemblyQualifiedName + " should be derived from " + typeof(SmdUserBase).AssemblyQualifiedName);
                }

                _user = value;
            }
        }
        private Type _user;

        public Type Role
        {
            get { return _role; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof(SmdRoleBase).IsAssignableFrom(value))
                {
                    throw new SmdException(value.AssemblyQualifiedName + " should be derived from " + typeof(SmdRoleBase).AssemblyQualifiedName);
                }

                _role = value;
            }
        }
        private Type _role;

 
    }
}