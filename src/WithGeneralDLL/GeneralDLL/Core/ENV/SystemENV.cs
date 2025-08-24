// Ignore Spelling: ENV

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GeneralDLL.Core.ENV
{
    public abstract class SystemENV : ISystemENV
    {
        Func<EnvironmentType> GetENV;

        public SystemENV(Func<EnvironmentType> GetENV)
        {
            this.GetENV = GetENV;
        }

        public EnvironmentType environment { get => GetENV(); }

        public enum EnvironmentType
        {
            LocalSRT,
            LocalRahimzadeh,
            LocalAkbarzadeh,
            LocalSajadi,
            LocalGholizadeh,
            LocalNajafi,
            Test,
            Development,
            Preview,
            Production
        }

        public OSPlatform GetOSPlatform()
        {
            OSPlatform osPlatform = (OSPlatform)Enum.Parse(typeof(OSPlatform), RuntimeInformation.OSDescription);
            return osPlatform;
        }
        public Architecture GetProcessArchitecture()
        { return RuntimeInformation.ProcessArchitecture; }
        public Architecture GetOSArchitecture()
        { return RuntimeInformation.OSArchitecture; }
    }
}