using Smd.Authorization.Roles;
using Smd.Domain;
using Smd.Domain.Entiies;
using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wtl.Core.Domain.Authorization.Users;

namespace Wtl.Core.Domain.Authorization.Roles
{
    /// <summary>
    /// 角色
    /// </summary> 
    public class Role : SmdRole<User>
    {
        public Role()
        {

        }

        public Role(string displayName)
            : base(displayName)
        {

        }

        public Role(string name, string displayName)
            : base(name, displayName)
        {

        }
    }
}
