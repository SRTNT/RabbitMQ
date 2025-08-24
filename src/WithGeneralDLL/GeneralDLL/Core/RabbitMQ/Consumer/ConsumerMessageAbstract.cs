using Azure;
using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using GeneralDLL.SRTExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RabbitMQ.Consumer
{
    public abstract class ConsumerMessageAbstract : IConsumerMessage, IAsyncDisposable
    {
        protected readonly AppConnectionString appConnectionString;
        protected readonly ILogger<ConsumerMessageAbstract> logger;

        protected IConnection connection;
        protected IChannel channel;

        #region Constructors
        protected ConsumerMessageAbstract(AppConnectionString appConnectionString, ILogger<ConsumerMessageAbstract> logger)
        {
            this.appConnectionString = appConnectionString;
            this.logger = logger;
        }
        #endregion

        #region Basic data For Queue & Exchange
        public abstract QueueData queueData { get; }
        #endregion

        /// <summary>
        /// routeKey = e.RoutingKey
        /// IReadOnlyBasicProperties propsFromSender = e.BasicProperties 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public abstract Task<string> AfterReceiveData(string data, BasicDeliverEventArgs e);

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

            #region Create Queue
            var queue = await channel.QueueDeclareAsync(
                                            queue: queueData.name,
                                            durable: queueData.durable, // For Not Delete if the rabbitmq crash => true
                                            exclusive: queueData.exclusive,
                                            autoDelete: queueData.autoDelete,
                                            arguments: queueData.arguments);
            #endregion

            // Control how many message analyze by this consumer
            await channel.BasicQosAsync(prefetchSize: 0, // Size of message in byte - 0 = no limit 
                                        prefetchCount: 1, // number of message that send to consumer for analyze
                                        global: false); // true => all connect consumer get this config
                                                        // false => just this consumer has this config

            #region Create Consumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var r = await AfterReceiveData(message, ea);

                #region Reply RPC Result
                if (!r.SRT_StringIsNullOrEmpty())
                {
                    var replyProps = new BasicProperties
                    {
                        CorrelationId = ea.BasicProperties.CorrelationId
                    };
                    var responseBytes = Encoding.UTF8.GetBytes(r);
                    await channel.BasicPublishAsync(exchange: string.Empty,
                                                    routingKey: ea.BasicProperties.ReplyTo,
                                                    mandatory: true,
                                                    basicProperties: replyProps,
                                                    body: responseBytes);
                }
                #endregion

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            #endregion

            await channel.BasicConsumeAsync(
                                queue: queue.QueueName,
                                autoAck: false,
                                consumer: consumer);

        }
        #endregion

        private bool isWorking = false;
        public async Task StartAsync(CancellationToken cts)
        {
            if (channel is not null && !channel.IsClosed)
                return;

            if (isWorking)
                return;

            isWorking = true;

            logger.LogWarning("RabbitMQ - Start Consumer: " + this.GetType().Name);

            await MakeReadyExchangeAndQueue();
        }

        public async Task StopAsync(CancellationToken cts)
        {
            isWorking = false;

            logger.LogWarning("RabbitMQ - Stop Consumer: " + this.GetType().Name);

            await channel.CloseAsync();
            await connection.CloseAsync();

            await channel.DisposeAsync();
            await connection.DisposeAsync();
        }

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
