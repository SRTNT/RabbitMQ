using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.RabbitMQStructure
{
    public class ConsumerMessage3 : GeneralDLL.Core.RabbitMQ.Consumer.ConsumerMessageAbstract
    {
        public ConsumerMessage3(
            AppConnectionString appConnectionString,
            ILogger<ConsumerMessage3> logger) : base(appConnectionString, logger)
        {
        }

        public override QueueData queueData => new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
        {
            autoDelete = false,
            durable = true,
            exclusive = false,
            name = "queueTopic3",
            routingKey = "*.*.orange"
        };

        public override Task<string> AfterReceiveData(string data, BasicDeliverEventArgs e)
        {
            var v = queueData.name + $"({queueData.routingKey}): " + e;
            logger.LogInformation(v);
            return Task.FromResult(string.Empty);
        }
    }
}