using System;

namespace Smd.Zero.Configuration
{
    public interface ISmdZeroEntityTypes
    {
        /// <summary>
        /// User type of the application.
        /// </summary>
        Type User { get; set; }

        /// <summary>
        /// Role type of the application.
        /// </summary>
        Type Role { get; set; } 
    }
}