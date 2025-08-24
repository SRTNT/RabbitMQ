using GeneralDLL.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.SerilogConfig
{
    public static class SRTSerilogConfig
    {
        public static WebApplicationBuilder AddSerilogConfig(this WebApplicationBuilder builder, string tagName = "SRTSerilog")
        {
            #region Get Config From APP Setting Json
            var serilogConfig = new SerilogConfigStructure();
            builder.Configuration.GetSection(tagName).Bind(serilogConfig);

            serilogConfig.RefreshData();
            #endregion

            #region Serilog Config
            var serilog = new LoggerConfiguration();

            foreach (var item in serilogConfig.WriteTo)
            { serilog = item.Set(serilog); }

            Log.Logger = serilog.MinimumLevel.Is(serilogConfig.minimumLevel)
                                             .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                                             .MinimumLevel.Override("System.Net.Http", Serilog.Events.LogEventLevel.Information)
                                             //.MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
                                             //.Enrich.WithProcessId()
                                             //.Enrich.WithProcessName()
                                             //.Enrich.FromLogContext()
                                             .CreateLogger();
            #endregion

            builder.Host.UseSerilog();

            return builder;
        }
    }
}