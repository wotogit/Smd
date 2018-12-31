using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Authorization.Users
{
    /// <summary>
    /// 用户权限
    /// </summary> 
    public class UserPermission : PermissionSetting
    {
        public virtual long UserId { get; set; }
    }
}
