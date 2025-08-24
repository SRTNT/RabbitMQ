using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RabbitMQ.Publisher
{
    public abstract class PublisherMessageAbstract : IPublisherMessage, IAsyncDisposable
    {
        protected readonly AppConnectionString appConnectionString;
        protected readonly ILogger<PublisherMessageAbstract> logger;

        protected IConnection connection;
        protected IChannel channel;

        #region Constructors
        protected PublisherMessageAbstract(
            AppConnectionString appConnectionString,
            ILogger<PublisherMessageAbstract> logger)
        {
            this.appConnectionString = appConnectionString;
            this.logger = logger;
        }
        #endregion

        #region Basic data For Queue & Exchange
        /// <summary>
        /// for Fanout/Topic/Direct
        /// </summary>
        public abstract ExchangeData exchangeData { get; }

        public abstract List<QueueData> lstQueueData { get; }

        public virtual BasicProperties basicProperties { get; protected set; } =
            new BasicProperties
            {
                // CorrelationId = Guid.NewGuid().ToString(),
                // ReplyTo = "", // if you want receive data
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };
        #endregion

        #region Factory
        protected ConnectionFactory factory => new ConnectionFactory
        {
            HostName = appConnectionString.GeneralDLL_RabbitMQ.host,
            Port = int.Parse(appConnectionString.GeneralDLL_RabbitMQ.port),
            UserName = appConnectionString.GeneralDLL_RabbitMQ.user,
            Password = appConnectionString.GeneralDLL_RabbitMQ.pass,
            VirtualHost = "/"
        };
        #endregion

        #region Make Ready Structure
        /// <summary>
        /// Set connection, channel
        /// </summary>
        /// <returns></returns>
        protected async Task MakeReadyStructure()
        {
            if (channel is not null && !channel.IsClosed)
                return;

            channel?.Dispose();
            connection?.Dispose();

            connection = await factory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
        }
        #endregion

        #region Make Ready Queue
        /// <summary>
        /// Set exchangeData, queueData
        /// Set connection, channel
        /// </summary>
        /// <returns></returns>
        protected async Task MakeReadyExchangeAndQueue()
        {
            if (channel is not null && !channel.IsClosed)
                return;

            await MakeReadyStructure();

            if (exchangeData != null)
            {
                await channel.ExchangeDeclareAsync(exchange: exchangeData.name,
                                                   autoDelete: exchangeData.autoDelete,
                                                   durable: exchangeData.durable,
                                                   type: exchangeData.type);
            }

            foreach (var queueData in lstQueueData)
            {
                #region Create Queue
                var queue = await channel.QueueDeclareAsync(
                                                queue: queueData.name,
                                                durable: queueData.durable, // For Not Delete if the rabbitmq crash => true
                                                exclusive: queueData.exclusive,
                                                autoDelete: queueData.autoDelete,
                                                arguments: queueData.arguments);
                #endregion

                if (exchangeData != null)
                {
                    #region Map Exchange To Queue
                    await channel.QueueBindAsync(queue: queueData.name,
                                                 exchange: exchangeData.name,
                                                 routingKey: queueData.routingKey);
                    #endregion
                }
            }
        }
        #endregion

        #region Send Message
        public virtual async Task SendMessage(string message, string routeKey = null)
        {
            await MakeReadyExchangeAndQueue();

            var qType = "None";

            switch (exchangeData?.type)
            {
                case null:
                    await SendQueue(message, routeKey);
                    qType = "Queue";
                    break;
                case ExchangeType.Direct:
                    await SendDirect(message, routeKey);
                    qType = "Direct";
                    break;
                case ExchangeType.Fanout:
                    await SendFanout(message, routeKey);
                    qType = "Fanout";
                    break;
                case ExchangeType.Topic:
                    await SendTopic(message, routeKey);
                    qType = "Topic";
                    break;
                default:
                    break;
            }

            logger.LogWarning("RabbitMQ - Send Message - " + this.GetType().Name + $"({qType}): " + exchangeData?.ToString());
        }
        #endregion

        #region Send Queue - Send message to all queue same as others - no exchange
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="routeKey"></param>
        /// <returns></returns>
        private async Task SendQueue(string message, string routeKey = null)
        {
            var body = Encoding.UTF8.GetBytes(message);

            foreach (var queueData in lstQueueData)
            {
                await channel.BasicPublishAsync(exchange: string.Empty,
                                                routingKey: queueData.name,
                                                mandatory: true, // if the rout key not existed return message to main server
                                                basicProperties: basicProperties,
                                                body: body);
            }
        }
        #endregion

        #region Send Fanout - Send message to exchange - send all to all queue in exchange
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="routeKey"></param>
        /// <returns></returns>
        private async Task SendFanout(string message, string routeKey = null)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties
            {
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            await channel.BasicPublishAsync(
                                exchange: exchangeData.name,
                                routingKey: string.Empty,
                                mandatory: true, // if the rout key not existed return message to main server
                                basicProperties: basicProperties,
                                body: body);
        }
        #endregion

        #region Send Direct - Send message to exchange withe key - send all to all queue in exchange
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="routeKey"></param>
        /// <returns></returns>
        private async Task SendDirect(string message, string routeKey = null)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties
            {
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            await channel.BasicPublishAsync(exchange: exchangeData.name,
                                            routingKey: routeKey,
                                            mandatory: true, // if the rout key not existed return message to main server
                                            basicProperties: basicProperties,
                                            body: body);
        }
        #endregion

        #region Send Topic - Send data by condition of key
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="routeKey"></param>
        /// <returns></returns>
        private async Task SendTopic(string message, string routeKey = null)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties
            {
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            await channel.BasicPublishAsync(exchange: exchangeData.name,
                                            routingKey: routeKey,
                                            mandatory: true, // if the rout key not existed return message to main server
                                            basicProperties: basicProperties,
                                            body: body);
        }
        #endregion

        public async ValueTask DisposeAsync()
        {
            if (channel is not null)
            {
                await channel.CloseAsync();
            }

            if (connection is not null)
            {
                await connection.CloseAsync();
            }
        }
    }
}
