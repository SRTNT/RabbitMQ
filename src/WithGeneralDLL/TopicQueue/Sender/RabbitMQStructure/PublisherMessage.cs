using GeneralDLL.Domain;
using RabbitMQ.Client;

namespace Sender.RabbitMQStructure
{
    public class PublisherMessage : GeneralDLL.Core.RabbitMQ.Publisher.PublisherMessageAbstract
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
            name = "exchangeTopic",
            type = ExchangeType.Topic
        };

        public override List<GeneralDLL.Core.RabbitMQ.Domain.QueueData> lstQueueData => new List<GeneralDLL.Core.RabbitMQ.Domain.QueueData>()
        {
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueTopic1",
                routingKey = "*.orange.*"
            },
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueTopic2",
                routingKey = "*.orange"
            },
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueTopic3",
                routingKey = "*.*.orange"
            },
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueTopic4",
                routingKey = "orange.#"
            }
        };
    }
}
