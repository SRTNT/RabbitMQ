using GeneralDLL.Core.Databases;
using GeneralDLL.Core.ENV;
using GeneralDLL.Core.RabbitMQ;
using GeneralDLL.Core.SerilogConfig;
using GeneralDLL.Core.Swaggers;
using GeneralDLL.Core.SYS_MiddleWare;
using GeneralDLL.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Logging;
using System;
using System.Runtime.CompilerServices;

namespace GeneralDLL.Core
{
    public static class BaseConfigurations
    {
        #region Basic Configure Builder
        /// <summary>
        /// Get Environments Variable
        /// Create builder in depends on Environment
        /// create appSetting
        /// get connection string from app setting and log it
        /// injection of connection string
        /// config controllers with default 500 status response of actions
        /// config base of version
        /// Configuration RabbitMQ
        /// </summary>
        /// <param name="args"></param>
        /// <param name="config"></param>
        /// <returns>builder & connection string</returns>
        public static (WebApplicationBuilder, AppSetting) SRT_ConfigController(string[] args, AppSetting config)
        {
            Console.WriteLine("Start Configurations ..................... ");
            Console.WriteLine("");
            Console.WriteLine("");

            #region Create Builder
            string environment = CoreSystemENV.FixEnvironmentName(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = environment });

            var appSetting = new AppSetting(builder.Environment, config);
            #endregion

            #region Get Connection String
            var dmConnectionString = builder.SRT_GetConnectionString();
            appSetting.ConnectionStrings = dmConnectionString;
            #endregion

            #region Api version
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            #endregion

            appSetting.lstInjectService.Add(GeneralDLL.Core.SYS_DI.Authentication.AddAuthentication);

            return (builder, appSetting);
        }
        #endregion

        #region Build and return app
        /// <summary>
        /// Add Swagger versions
        /// Control swagger ui in depends on versions
        /// Control authentication api for swagger
        /// Control Environment for builder - app - swagger
        /// Configure Database
        /// Control database migration and base data
        /// Add Cash String - Redis | SQL
        /// Configure Rabbit MQ - Redis - Error Controller
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configApp"></param>
        /// <returns></returns>
        public static async Task<WebApplication> SRT_ConfigBuilderAndBuildApp(this WebApplicationBuilder builder, AppSetting configApp)
        {
            var config = configApp.SwaggerConfig;
            var ConnectionStrings = configApp.ConnectionStrings;

            var loggerBuilder = builder.SRT_GetLogger();
            loggerBuilder.LogWarning("Environment -> " + configApp.CoreSystemENV.environment.ToString());

            #region Config Controllers
            builder.Services.AddControllers(option =>
            {
                option.ReturnHttpNotAcceptable = configApp.ReturnHttpNotAcceptable;

                configApp.lstResponseResult.ForEach(filter => option.Filters.Add(filter));
                configApp.lstControllersOptions.ForEach(item => item.Invoke(option));
            });
            #endregion

            builder.Services.AddEndpointsApiExplorer();

            #region Add Services which MS Want to Add
            if (configApp.lstInjectService != null && configApp.lstInjectService.Count > 0)
            {
                #region Get URL Data From App Setting
                var _urlData = new URLData();
                builder.Configuration.GetSection(configApp.BaseURLtagName).Bind(_urlData);
                _urlData.BaseURL = _urlData.BaseURL;
                #endregion

                foreach (var item in configApp.lstInjectService)
                { item.Invoke(builder.Services, _urlData); }
            }
            #endregion

            #region Add Serilog
            if (configApp.UseSerilog)
            {
                builder.AddSerilogConfig();
                loggerBuilder.LogWarning("Builder - Add Serilog - OK");
            }
            #endregion

            #region Add Swagger in Builder
            var (_, isApplyed) = builder.SRT_AddSwaggerGen(configApp);
            if (isApplyed)
                loggerBuilder.LogWarning("Builder - Swagger - OK");
            #endregion

            #region Environment Controller - Builder
            configApp.lstBuilderEnvironments.ForEach(q => q.Invoke(builder, configApp.CoreSystemENV.environment));
            #endregion

            #region Cache | Error Log
            if (configApp.UseCachStructure != AppSetting.CashType.None)
            {
                builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = ConnectionStrings.GeneralDLL_CacheRedis.GetConnectionString(); });

                if (configApp.UseCachStructure == AppSetting.CashType.CashWithSQL)
                    configApp.lstDatabaseNamespace.Add(typeof(GeneralDLL.Core.RedisCash.CacheDBContext).Namespace);

                loggerBuilder.LogWarning("Builder - Add Cash Namespace - OK");
            }
            if (configApp.UseErrorLogSQL)
            {
                configApp.lstDatabaseNamespace.Add(typeof(GeneralDLL.Core.ErrorController.ErrorReportDBContext).Namespace);
                loggerBuilder.LogWarning("Builder - Error Log - OK");
            }
            #endregion

            #region Database Builder
            if (configApp.lstDatabaseNamespace != null && configApp.lstDatabaseNamespace.Count > 0)
            {
                configApp.lstDatabaseNamespace.ForEach(dbNamespace => builder.SRT_AddGeneralConfig(configApp.ConnectionStrings, dbNamespace));
                loggerBuilder.LogWarning("Builder - Databases - OK");
            }
            #endregion

            #region Database Repository - Cache | Error Log

            #region Repository Cache
            if (configApp.UseCachStructure != AppSetting.CashType.None)
            {
                if (configApp.UseCachStructure == AppSetting.CashType.CashWithSQL)
                {
                    builder.Services.AddScoped<RedisCash.Repository.IGeneralDLLRepository, RedisCash.Repository.GeneralDLLRepositorySQL>();
                }
                else
                {
                    builder.Services.AddScoped<RedisCash.Repository.IGeneralDLLRepository, RedisCash.Repository.GeneralDLLRepository>();
                }

                loggerBuilder.LogWarning("Builder - Add cash Repository - OK");
            }
            #endregion

            #region Repository Error Log
            if (configApp.UseErrorLogSQL)
            {
                builder.Services.AddScoped<ErrorController.Repository.IErrorLogger, ErrorController.Repository.ErrorLogger>();
                loggerBuilder.LogWarning("Builder - Add Error Repository - OK");
            }
            #endregion 

            #endregion

            #region Add Necessary Injection - ConnectionSetting - Environment
            builder.Services.AddSingleton(configApp);
            builder.Services.AddSingleton<ENV.ISystemENV>(configApp.CoreSystemENV);
            builder.Services.AddSingleton(configApp.ConnectionStrings);

            loggerBuilder.LogWarning("Builder - Add Injection SQLConnection | CoreSystemENV - OK");
            #endregion

            #region rabbitMQ
            if (configApp.lstRabbitMQNamespace.Count > 0)
            {
                await builder.SRT_RabbitMQConfig(configApp);
                loggerBuilder.LogWarning("Builder - Add Rabbit MQ Structure - Server & Client");
            }
            #endregion

            var app = builder.Build();

            #region Add Console Log MiddleWare
            app.UseMiddleware<SystemLoggerAttribute>();
            #endregion

            var loggerApp = app.SRT_GetLogger();

            #region Swagger Environment
            (_, isApplyed) = app.SRT_AddSwaggerUI(configApp);
            if (isApplyed)
                loggerApp.LogWarning("APP - Add swagger UI - OK");
            #endregion

            #region Environment Controller - App
            configApp.lstAppEnvironments.ForEach(q => q.Invoke(app, configApp.CoreSystemENV.environment));
            #endregion

            #region Database APP
            foreach (var dbNamespace in configApp.lstDatabaseNamespace)
            {
                try
                {
                    Console.WriteLine("------------------------------- " + dbNamespace);
                    app.SRT_CheckDatabase(dbNamespace).Wait();
                }
                catch (Exception ex)
                {
                    loggerApp.LogCritical(ex.Message);
                    throw new Exception("Database APP", ex);
                }
            }
            #endregion

            return app;
        }
        #endregion

        #region Get Connection String & Log It
        private static AppConnectionString SRT_GetConnectionString(this WebApplicationBuilder builder)
        {
            // get data From Configuration
            var sqlConnection = new AppConnectionString();
            builder.Configuration.GetSection("ConnectionDB").Bind(sqlConnection);

            // Log
            #region Log Connection String
            Console.WriteLine("\n\n******************************* Connection Strings ****************************");

            LogConnection(builder, sqlConnection.DefaultConnection);
            LogConnection(builder, sqlConnection.GeneralDLL_ErrorSQL);
            LogConnection(builder, sqlConnection.GeneralDLL_RabbitMQ);
            LogConnection(builder, sqlConnection.GeneralDLL_CacheSQL);
            LogConnection(builder, sqlConnection.GeneralDLL_CacheRedis);

            Console.WriteLine("*******************************************************************************\n\n");
            #endregion

            return sqlConnection;
        }

        private static void LogConnection(WebApplicationBuilder builder, GeneralConnectionString connection, [CallerArgumentExpression("connection")] string instanceName = null)
        {
            if (connection == null) return;

            var connectionString = connection.GetConnectionString();
            var parts = connectionString.Split(';').Select(part => part + (string.IsNullOrEmpty(part) ? "" : ";")).ToList();

            parts.Insert(0, "-----------------------------------");

            var logger = builder.SRT_GetLogger(instanceName.Split('.')[1]);
            logger.LogWarning(string.Join("\n", parts));
        }
        #endregion

        #region Get Logger
        public static ILogger SRT_GetLogger(this WebApplicationBuilder builder, string title = "configure Application")
        {
            // Get the logger from the service provider
            var loggerFactory = builder.Services.BuildServiceProvider()
                                                .GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger(title);
        }
        public static ILogger SRT_GetLogger(this WebApplication app, string title = "configure Application")
        {
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger(title);
        }
        #endregion
    }
}