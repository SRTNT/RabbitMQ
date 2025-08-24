// Ignore Spelling: ENV

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GeneralDLL.Core.ENV
{
    public interface ISystemENV
    {
        SystemENV.EnvironmentType environment { get; }

        OSPlatform GetOSPlatform();
        Architecture GetProcessArchitecture();
        Architecture GetOSArchitecture();
    }
}
