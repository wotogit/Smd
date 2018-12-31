using Smd.Domain.Entiies.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Smd.Authorization.Users
{
    public abstract class SmdUser<TUser>:SmdUserBase,IAudited<TUser> where TUser : SmdUser<TUser>
    {
        public const int MaxConcurrencyStampLength = 128;

        [StringLength(MaxConcurrencyStampLength)]
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }

        protected SmdUser()
        {
        }

    }
}
