using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Domain.Entiies.Auditing
{
    public interface IModificationAudited:IHasModificationTime
    {
        long? LastModifierUserId { get; set; }
    }

    public interface IModificationAudited<TUser> : IModificationAudited
       where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}
