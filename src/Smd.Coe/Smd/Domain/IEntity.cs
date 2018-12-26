using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Domain
{
    /// <summary>
    /// 实体基类接口，所有实体必须实体此接口
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        TPrimaryKey Id { get; set; }
        /// <summary>
        /// 当前实体是否是临时的(没有持久化到数据库，没有Id值)
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}
