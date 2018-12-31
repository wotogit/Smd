using Smd.Domain;
using Smd.Domain.Entiies;
using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text; 

namespace Smd.Authorization.Roles
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("SmdRoles")]
    public class SmdRoleBase : AuditedEntity<long>
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
        /// 角色对应的权限资源
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RolePermission> Permissions { get; set; }
       

        protected SmdRoleBase()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        protected SmdRoleBase(string displayName)
            : this()
        { 
            DisplayName = displayName;
        }

        protected SmdRoleBase(string name, string displayName)
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
