using System;
using System.Collections.Generic;
using System.Text;

namespace Wtl.Core.Domain.Authorization.Users
{
    /// <summary>
    /// 用户权限
    /// </summary> 
    public class UserPermission : Permission
    {
        public virtual long UserId { get; set; }
    }
}
