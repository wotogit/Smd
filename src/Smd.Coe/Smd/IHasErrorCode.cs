using System;
using System.Collections.Generic;
using System.Text;

namespace Smd
{
    public interface IHasErrorCode
    {
        int Code { get; set; }
    }
}
