using Smd.Domain.Entiies.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Smd.Authorization.Users
{
    /// <summary>
    /// 记录用户摘要
    /// </summary>
    [Table("SmdUserAccounts")] 
    public class UserAccount : AuditedEntity<long>
    {
        /// <summary>
        /// Maximum length of the <see cref="UserName"/> property.
        /// </summary>
        public const int MaxUserNameLength = 256;

        /// <summary>
        /// Maximum length of the <see cref="EmailAddress"/> property.
        /// </summary>
        public const int MaxEmailAddressLength = 256;
         

        public virtual long UserId { get; set; }

        public virtual long? UserLinkId { get; set; }

        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }

        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; }

        public virtual DateTime? LastLoginTime { get; set; }
    }
}