using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Domain.Entiies.Auditing
{
    public interface IHasModificationTime
    {
        /// <summary>
        /// 最近一次修改时间
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}
