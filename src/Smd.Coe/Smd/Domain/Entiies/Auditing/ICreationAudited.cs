using Smd.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Domain.Entiies.Auditing
{
    /// <summary>
    /// 实现此接口的实体可以得到创建者信息
    /// </summary>
    public interface ICreationAudited: IHasCreationTime
    {
        /// <summary>
        /// 创建者ID
        /// </summary>
        long? CreatorUserId { get; set; }
    }

    public interface ICreationAudited<TUser> : ICreationAudited
       where TUser : IEntity<long>
    {
        /// <summary>
        /// 创建者实体
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}
