using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Authorization.Users
{
    /// <summary>
    /// Used to cache roles and permissions of a user.
    /// </summary>
    [Serializable]
    public class UserPermissionCacheItem
    {
        public const string CacheStoreName = "SmdZeroUserPermissions";

        public long UserId { get; set; }

        public List<int> RoleIds { get; set; }

        public HashSet<string> GrantedPermissions { get; set; }

        public HashSet<string> ProhibitedPermissions { get; set; }

        public UserPermissionCacheItem()
        {
            RoleIds = new List<int>();
            GrantedPermissions = new HashSet<string>();
            ProhibitedPermissions = new HashSet<string>();
        }

        public UserPermissionCacheItem(long userId)
            : this()
        {
            UserId = userId;
        }
    }
}
