using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smd.Threading
{
    public static class SmdTaskCache
    {
        public static Task CompletedTask { get; } = Task.FromResult(0);
    }
}
