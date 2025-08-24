using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RabbitMQ.Publisher
{
    public abstract class PublisherMessageRPCAbstract : PublisherMessageAbstract
    {
        public abstract Task AnalyzeResult(string dataResult, string routeKey, IReadOnlyBasicProperties propsFromSender, BasicDeliverEventArgs e);

        #region Constructors
        protected PublisherMessageRPCAbstract(
            AppConnectionString appConnectionString,
            ILogger<PublisherMessageRPCAbstract> logger) : base(appConnectionString, logger)
        {
        }
        #endregion

        private ConcurrentDictionary<string, TaskCompletionSource<(string message, BasicDeliverEventArgs e)>> _callbackMapper =
            new ConcurrentDictionary<string, TaskCompletionSource<(string message, BasicDeliverEventArgs e)>>();

        #region Send Message
        public override async Task SendMessage(string message, string routeKey = null)
        {
            await MakeReadyExchangeAndQueue();

            basicProperties = new BasicProperties
            {
                CorrelationId = Guid.NewGuid().ToString(),
                ReplyTo = "QueueTemp" + Guid.NewGuid().ToString(), // if you want receive data
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            var tcs = new TaskCompletionSource<(string message, BasicDeliverEventArgs e)>(TaskCreationOptions.RunContinuationsAsynchronously);
            _callbackMapper.TryAdd(basicProperties.CorrelationId, tcs);

            await MakeReadyRecieveResult();

            await base.SendMessage(message, routeKey);

            if (_callbackMapper.TryGetValue(basicProperties.CorrelationId, out tcs))
            {
                var r = await tcs.Task;
                await AnalyzeResult(r.message, r.e.RoutingKey, r.e.BasicProperties, r.e);
            }
        }
        #endregion

        #region Make Ready Receive Result
        private async Task MakeReadyRecieveResult()
        {
            #region Receive Result
            var queue = await channel.QueueDeclareAsync(queue: basicProperties.ReplyTo);

            var consumer = new AsyncEventingBasicConsumer(base.channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                string correlationId = ea.BasicProperties.CorrelationId;

                if (!string.IsNullOrEmpty(correlationId))
                {
                    if (_callbackMapper.TryRemove(correlationId, out var tcs))
                    {
                        var body = ea.Body.ToArray();
                        var response = Encoding.UTF8.GetString(body);
                        tcs.TrySetResult(new(response, ea));

                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                }
            };
            #endregion

            await base.channel.BasicConsumeAsync(queue.QueueName, true, consumer);
        }
        #endregion
    }
}
