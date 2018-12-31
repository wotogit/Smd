using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Runtime.Session
{
    public abstract class SmdSessionBase: ISmdSession
    {
        public const string SessionOverrideContextKey = "Smd.Runtime.Session.Override"; 

        public abstract long? UserId { get; } 

        public abstract long? ImpersonatorUserId { get; }
         
    }
}
