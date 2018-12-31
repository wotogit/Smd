using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Runtime.Session
{

    /// <summary>
    /// Implements null object pattern for <see cref="ISmdSession"/>.
    /// </summary>
    public class NullSmdSession : SmdSessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullSmdSession Instance { get; } = new NullSmdSession();

        /// <inheritdoc/>
        public override long? UserId => null;

        public override long? ImpersonatorUserId => null;
    }
}
