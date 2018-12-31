﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Authorization.Roles
{
    /// <summary>
    /// Equality comparer for <see cref="Permission"/> objects.
    /// </summary>
    public class PermissionEqualityComparer : IEqualityComparer<Permission>
    {
        public static PermissionEqualityComparer Instance { get { return _instance; } }
        private static PermissionEqualityComparer _instance = new PermissionEqualityComparer();

        public bool Equals(Permission x, Permission y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }

            return Equals(x.Name, y.Name);
        }

        public int GetHashCode(Permission permission)
        {
            return permission.Name.GetHashCode();
        }
    }
}
