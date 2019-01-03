using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Timing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class DisableDateTimeNormalizationAttribute : Attribute
    {

    }
}
