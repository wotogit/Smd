﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Authorization.Roles
{
    public class RolePermission: PermissionSetting
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual long RoleId { get; set; }
    }
}