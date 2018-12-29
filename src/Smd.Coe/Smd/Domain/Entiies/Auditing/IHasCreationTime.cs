using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Domain.Entiies.Auditing
{
    /// <summary>
    /// 需要创建时间的时间可以实现此接口
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
