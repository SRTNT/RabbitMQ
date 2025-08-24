using GeneralDLL.Core.Databases;
using GeneralDLL.Core.ENV;
using GeneralDLL.Core.RabbitMQ.Consumer;
using GeneralDLL.Core.RabbitMQ.Publisher;
using GeneralDLL.Core.Swaggers;
using GeneralDLL.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Logging;
using RabbitMQ.Client;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeneralDLL.Core.RabbitMQ
{
    public static class BaseConfigurations
    {
        #region Basic Configure Builder
        public static async Task<WebApplicationBuilder> SRT_RabbitMQConfig(this WebApplicationBuilder builder, AppSetting config)
        {
            foreach (var configItem in config.lstRabbitMQNamespace)
            {
                var lstType = RegisterServicesByType.SRT_GetTypes<IConsumerMessage>(configItem);
                RabbitMQConsumerConfig(builder, lstType, builder.SRT_GetLogger());

                lstType = RegisterServicesByType.SRT_GetTypes<IPublisherMessage>(configItem);
                RabbitMQPublisherConfig(builder, lstType, builder.SRT_GetLogger());
            }

            await Task.Delay(1);
            return builder;
        }
        #endregion

        private static WebApplicationBuilder RabbitMQPublisherConfig(WebApplicationBuilder builder, List<Type> lstType, ILogger logger)
        {
            foreach (var rabbitMQType in lstType)
            {
                // Use reflection to add the type as a scoped service
                builder.Services.AddScoped(rabbitMQType);
            }

            return builder;
        }

        private static WebApplicationBuilder RabbitMQConsumerConfig(WebApplicationBuilder builder, List<Type> lstType, ILogger logger)
        {
            // Use a HashSet to track registered types
            var registeredTypes = new HashSet<Type>();

            foreach (var rabbitMQType in lstType)
            {
                // Check if the type has already been registered
                if (registeredTypes.Add(rabbitMQType))
                {
                    // Register the hosted service with a factory method
                    builder.Services.AddSingleton(provider =>
                    {
                        // Resolve the logger and app connection string
                        var loggerInstance = (ILogger)provider.GetRequiredService(typeof(ILogger<>).MakeGenericType(rabbitMQType));
                        var appConnectionString = provider.GetRequiredService<AppConnectionString>();

                        // Create an instance of the hosted service
                        return (IHostedService)Activator.CreateInstance(rabbitMQType, appConnectionString, loggerInstance)!;
                    });
                }
                else
                {
                    logger.LogWarning($"Service {rabbitMQType.FullName} is already registered.");
                }
            }

            return builder;
        }

    }
}
