using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Domain.Entiies.Auditing
{
    /// <summary>
    /// 审计
    /// </summary>
    public interface IAudited:ICreationAudited,IModificationAudited
    {
    }

    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
       where TUser : IEntity<long>
    {

    }
}
