using Smd.Authorization.Users;
using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Smd.Authorization.Roles
{
    public abstract class SmdRole<TUser>:SmdRoleBase,IAudited<TUser> where TUser : SmdUser<TUser>
    {
        /// <summary>
        /// 随机值
        /// </summary>
        public const int MaxConcurrencyStampLength = 128;
        /// <summary>
        /// Claims of this user.
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RoleClaim> Claims { get; set; }

        /// <summary>
        /// A random value that must change whenever a user is persisted to the store
        /// </summary>
        [StringLength(MaxConcurrencyStampLength)]
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }

        public SmdRole()
        {

        }
        protected SmdRole(string displayName)
           : base(displayName)
        { 
        }

        protected SmdRole(string name, string displayName)
            : base(displayName)
        { 
        }
    }
}
