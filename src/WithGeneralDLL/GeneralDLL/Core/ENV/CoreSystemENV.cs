// Ignore Spelling: ENV dev najafi gholizadeh akbarzadeh rahimzadeh 

using GeneralDLL.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace GeneralDLL.Core.ENV
{
    public class CoreSystemENV : SystemENV
    {
        public readonly IWebHostEnvironment _env;

        #region Constructors
        public CoreSystemENV(IWebHostEnvironment _env) : base(GetEnvironmentType(_env))
        {
            this._env = _env;
        }
        #endregion

        #region Get Environment Type - use In Constructors
        public static Func<EnvironmentType> GetEnvironmentType(IWebHostEnvironment _env)
        {
            var r = FixEnvironmentName(_env.EnvironmentName) switch
            {
                "Production" => EnvironmentType.Production,
                "Development" => EnvironmentType.Development,
                "Preview" => EnvironmentType.Preview,
                "LocalSRT" => EnvironmentType.LocalSRT,
                "LocalRahimzadeh" => EnvironmentType.LocalRahimzadeh,
                "LocalAkbarzadeh" => EnvironmentType.LocalAkbarzadeh,
                "LocalGholizadeh" => EnvironmentType.LocalGholizadeh,
                "LocalNajafi" => EnvironmentType.LocalNajafi,
                _ => EnvironmentType.Test,
            };

            if (r != EnvironmentType.Test)
                return () => r;

            return () =>
            {
                if (_env.IsProduction())
                    return EnvironmentType.Production;

                if (_env.IsDevelopment())
                    return EnvironmentType.Development;

                if (_env.IsStaging())
                    return EnvironmentType.Preview;

                return EnvironmentType.Test;
            };
        }

        #region Normalize Environment
        public static string FixEnvironmentName(string environmentName)
        {
            string env = "Test";

            if (environmentName.ToLower() == "production" ||
                environmentName.ToLower() == "product")
                env = "Production";

            else if (environmentName.ToLower() == "development" ||
                 environmentName.ToLower() == "develop" ||
                 environmentName.ToLower() == "dev")
                env = "Development";

            else if (environmentName.ToLower() == "preview" ||
                  environmentName.ToLower() == "pre" ||
                  environmentName.ToLower() == "stage" ||
                  environmentName.ToLower() == "staging")
                env = "Preview";

            else if (environmentName.ToLower().Contains("srt"))
                env = "LocalSRT";
            else if (environmentName.ToLower().Contains("rahimzadeh"))
                env = "LocalRahimzadeh";
            else if (environmentName.ToLower().Contains("akbarzadeh"))
                env = "LocalAkbarzadeh";
            else if (environmentName.ToLower().Contains("gholizadeh"))
                env = "LocalGholizadeh";
            else if (environmentName.ToLower().Contains("najafi"))
                env = "LocalNajafi";

            return env;
        }
        #endregion

        #endregion
    }
}
