﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Auditing
{
    public interface IClientInfoProvider
    {
        string BrowserInfo { get; }

        string ClientIpAddress { get; }

        string ComputerName { get; }
    }
}
