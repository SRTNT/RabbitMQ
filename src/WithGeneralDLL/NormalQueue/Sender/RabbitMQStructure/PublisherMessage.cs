using GeneralDLL.Core.RabbitMQ.Domain;
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

        public override GeneralDLL.Core.RabbitMQ.Domain.ExchangeData? exchangeData => null;

        public override List<GeneralDLL.Core.RabbitMQ.Domain.QueueData> lstQueueData => new List<GeneralDLL.Core.RabbitMQ.Domain.QueueData>()
        {
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueNormal1",
                routingKey = string.Empty
            },
            new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
            {
                autoDelete = false,
                durable = true,
                exclusive = false,
                name = "queueNormal2",
                routingKey = string.Empty
            }
        };
    }
}
