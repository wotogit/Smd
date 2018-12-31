﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Logging
{
    /// <summary>
    /// Indicates severity for log.
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Debug.
        /// </summary>
        Debug,

        /// <summary>
        /// Info.
        /// </summary>
        Info,

        /// <summary>
        /// Warn.
        /// </summary>
        Warn,

        /// <summary>
        /// Error.
        /// </summary>
        Error,

        /// <summary>
        /// Fatal.
        /// </summary>
        Fatal
    }
}
