using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Sender.RabbitMQStructure
{
    public class PublisherMessage : GeneralDLL.Core.RabbitMQ.Publisher.PublisherMessageRPCAbstract
    {
        public PublisherMessage(
            AppConnectionString appConnectionString,
            ILogger<PublisherMessage> logger) : base(appConnectionString, logger)
        {
        }

        //* (star) can substitute for exactly one word.
        //# (hash) can substitute for zero or more words.
        public override GeneralDLL.Core.RabbitMQ.Domain.ExchangeData? exchangeData => new GeneralDLL.Core.RabbitMQ.Domain.ExchangeData()
        {
            autoDelete = false,
            durable = true,
            name = "exchangeRPC",
            type = ExchangeType.Topic
        };

        public override List<GeneralDLL.Core.RabbitMQ.Domain.QueueData> lstQueueData => new List<GeneralDLL.Core.RabbitMQ.Domain.QueueData>()
        {
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueRPC1",
                routingKey = "*.orange.*"
            },
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueRPC2",
                routingKey = "*.orange"
            },
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueRPC3",
                routingKey = "*.*.orange"
            },
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueRPC4",
                routingKey = "orange.#"
            }
        };

        public override async Task AnalyzeResult(string dataResult, string routeKey, IReadOnlyBasicProperties propsFromSender, BasicDeliverEventArgs e)
        {
            logger.LogInformation(dataResult);
            await Task.Delay(10000);
        }
    }
}