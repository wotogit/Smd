using JetBrains.Annotations;
using Smd.Domain.Entiies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wtl.Core.Domain.Authorization.Users
{
    /// <summary>
    /// 表示用户的身份验证令牌
    /// </summary>
    [Table("SmdUserTokens")]
    public class UserToken:Entity<long>
    {
        /// <summary>
        /// Maximum length of the <see cref="LoginProvider"/> property.
        /// </summary>
        public const int MaxLoginProviderLength = 64;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// Maximum length of the <see cref="Value"/> property.
        /// </summary>
        public const int MaxValueLength = 512; 

        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        [StringLength(MaxLoginProviderLength)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        [StringLength(MaxValueLength)]
        public virtual string Value { get; set; }

        protected UserToken()
        {

        }

        protected internal UserToken(User user, [NotNull] string loginProvider, [NotNull] string name, string value)
        { 
            UserId = user.Id;
            LoginProvider = loginProvider;
            Name = name;
            Value = value;
        }
    }
}
