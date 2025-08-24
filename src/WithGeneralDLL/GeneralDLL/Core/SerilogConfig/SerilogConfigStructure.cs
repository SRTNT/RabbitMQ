using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Serilog.Events;

namespace GeneralDLL.Core.SerilogConfig
{
    public class SerilogConfigStructure
    {
        #region Each Logger
        public class ConfigEachStructure
        {
            public enum SerilogType
            {
                Console,
                File
            }

            public SerilogType type { get; set; }
            public RollingInterval rollingInterval { get; set; } = RollingInterval.Hour;
            public string metaData { get; set; }

            internal LoggerConfiguration Set(LoggerConfiguration serilog)
            {
                #region String Format
                //Default => "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                //                                   u => Uppercase      l => for string         
                //                                   l => lowercase      j => for json
                //                                   3 => number of character
                var stringFormat = "[{Timestamp:HH:mm:ss} {Level:u3}] - {Message:lj}{NewLine}{Exception}{NewLine}---------------------------{NewLine}";
                #endregion

                switch (type)
                {
                    case SerilogType.Console:
                        serilog.WriteTo.Console(
                            theme: AnsiConsoleTheme.Sixteen, //Default: Code
                            outputTemplate: stringFormat
                            );
                        break;
                    case SerilogType.File:
                        serilog.WriteTo.File(
                            path: metaData,
                            rollingInterval: rollingInterval, //period of time for each file => default => Infinite (unlimited)
                            fileSizeLimitBytes: 1024000, //max size of file in byte => null : unlimited
                            rollOnFileSizeLimit: true, //if we receive to max of size => create new file - in default add till 31 new files
                            retainedFileCountLimit: null,//for change 31 number => null : unlimited
                            outputTemplate: stringFormat
                            );
                        break;
                    default:
                        throw new NotImplementedException();
                }

                return serilog;
            }
        }
        #endregion

        public List<ConfigEachStructure> WriteTo { get; set; } = new List<ConfigEachStructure>();
        public LogEventLevel minimumLevel { get; set; } = LogEventLevel.Warning;

        public void RefreshData()
        {
            if (WriteTo.Count > 0) return;

            WriteTo.Add(new ConfigEachStructure()
            {
                type = ConfigEachStructure.SerilogType.Console
            });
            WriteTo.Add(new ConfigEachStructure()
            {
                type = ConfigEachStructure.SerilogType.File,
                metaData = "SeriLog/log.txt"
            });
        }
    }
}