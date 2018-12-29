using System;
using System.Collections.Generic;
using System.Text;

namespace Wtl.Core.Domain.Authorization.Roles
{
    public class RolePermission:Permission
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual int RoleId { get; set; }
    }
}
