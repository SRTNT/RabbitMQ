// Ignore Spelling: SRT app env

using GeneralDLL.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace GeneralDLL.Core.Databases
{
    public static class DatabaseConfigurations
    {
        #region Builder Configuration
        public static WebApplicationBuilder SRT_AddGeneralConfig<baseClassForSearchContext>(this WebApplicationBuilder builder,
                                                                 AppConnectionString connectionStrings)
        {
            var lstType = RegisterServicesByType.SRT_GetTypes<IDataBase, baseClassForSearchContext>();
            return AddGeneralConfig(builder, connectionStrings, lstType);
        }

        public static WebApplicationBuilder SRT_AddGeneralConfig(this WebApplicationBuilder builder,
                                                                 AppConnectionString connectionStrings,
                                                                 string namespacePrefixSearchInChild)
        {
            var lstType = RegisterServicesByType.SRT_GetTypes<IDataBase>(namespacePrefixSearchInChild);
            return AddGeneralConfig(builder, connectionStrings, lstType);
        }

        private static WebApplicationBuilder AddGeneralConfig(WebApplicationBuilder builder, AppConnectionString connectionStrings, List<Type> lstType)
        {
            foreach (var dbContextType in lstType)
            {
                // Create an instance of the DbContext using reflection
                var optionsType = typeof(DbContextOptions<>).MakeGenericType(dbContextType);
                var options = Activator.CreateInstance(optionsType); // You may need to pass actual options here

                var dbContextInstance = Activator.CreateInstance(dbContextType, options, connectionStrings);

                // Call the BaseConfiguration method
                var configurationMethod = dbContextType.GetMethod("BaseConfiguration");
                configurationMethod?.Invoke(dbContextInstance, new object[] { builder, connectionStrings });
            }

            return builder;
        }
        #endregion

        #region APP Configuration
        public static async Task<WebApplication> SRT_CheckDatabase<baseClassForSearchContext>(this WebApplication app)
        {
            var lstType = RegisterServicesByType.SRT_GetTypes<IDataBase, baseClassForSearchContext>();
            return await CheckDatabase(app, lstType, app.SRT_GetLogger());
        }

        public static async Task<WebApplication> SRT_CheckDatabase(this WebApplication app,
                                                                   string namespacePrefixSearchInChild)
        {
            var lstType = RegisterServicesByType.SRT_GetTypes<IDataBase>(namespacePrefixSearchInChild);
            return await CheckDatabase(app, lstType, app.SRT_GetLogger());
        }

        private static async Task<WebApplication> CheckDatabase(WebApplication app, List<Type> lstType, ILogger logger)
        {
            foreach (Type item in lstType)
            {
                try
                {
                    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var _dbContext = serviceScope.ServiceProvider.GetService(item) as DataBase;

                        var lstPending = (await _dbContext.Database.GetPendingMigrationsAsync()).ToList();

                        if (lstPending.Count > 0)
                        { _dbContext.Database.Migrate(); }

                        logger.LogWarning($"APP - Check Migration: {_dbContext.GetType().Name} - OK");
                    }

                    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var _dbContext = serviceScope.ServiceProvider.GetService(item) as DataBase;

                        await _dbContext.InsertBaseData();

                        logger.LogWarning($"APP - Insert Basic Record: {_dbContext.GetType().Name} - OK");
                    }
                }
                catch
                { throw; }
            }

            return app;
        }
        #endregion
    }
}