﻿using Smd.Domain.Entiies;
using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Smd.Authorization.Users
{
    /// <summary>
    /// 用户可使用的外部登录类型
    /// </summary>
    [Table("SmdUserLogins")]
    public class UserLogin : Entity<long>, IHasCreationTime
    {
        /// <summary>
        /// Maximum length of <see cref="LoginProvider"/> property.
        /// </summary>
        public const int MaxLoginProviderLength = 128;

        /// <summary>
        /// Maximum length of <see cref="ProviderKey"/> property.
        /// </summary>
        public const int MaxProviderKeyLength = 256;


        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 登录提供者
        /// </summary>
        [Required]
        [MaxLength(MaxLoginProviderLength)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// 提供者Key
        /// </summary>
        [Required]
        [MaxLength(MaxProviderKeyLength)]
        public virtual string ProviderKey { get; set; }
        public DateTime CreationTime { get; set; }

        public UserLogin()
        {

        }

        public UserLogin(long userId, string loginProvider, string providerKey)
        {
            UserId = userId;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
    }
}
