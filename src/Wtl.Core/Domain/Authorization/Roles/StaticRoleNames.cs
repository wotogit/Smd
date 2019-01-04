using System;
using System.Collections.Generic;
using System.Text;

namespace Wtl.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
        }

        public static class Tenants
        {
            public const string Admin = "Admin";

            public const string User = "User";
        }
    }
}
