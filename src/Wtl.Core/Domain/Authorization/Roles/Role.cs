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
    [Table("SmdRoles")]
    public class Role:AuditedEntity<long,User> ,IAudited<User>
    {
        /// <summary>
        /// Maximum length of the <see cref="DisplayName"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 32;
        /// <summary>
        /// 随机值
        /// </summary>
        public const int MaxConcurrencyStampLength = 128;

        /// <summary>
        /// 角色标识（唯一）
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 显示的角色名称
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 是否为固定的角色?
        /// 固定角色不能删除，不能变更名字 
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        /// 新注册的用户默认赋予的角色
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// 角色的声明单元
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RoleClaim> Claims { get; set; }

        /// <summary>
        /// 随机值。每次保存到数据库时此值需要变更
        /// </summary>
        [StringLength(MaxConcurrencyStampLength)] 
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 角色对应的权限资源
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RolePermission> Permissions { get; set; }
       

        protected Role()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        protected Role(string displayName)
            : this()
        { 
            DisplayName = displayName;
        }

        protected Role(string name, string displayName)
            : this(displayName)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"[Role {Id}, Name={Name}]";
        }
    }
}
