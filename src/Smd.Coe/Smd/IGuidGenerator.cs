using System;
using System.Collections.Generic;
using System.Text;

namespace Smd
{
    /// <summary>
    /// GUID生成器
    /// </summary>
    public interface IGuidGenerator
    {
        Guid Create();
    }
}
