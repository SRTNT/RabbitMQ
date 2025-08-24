using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.RabbitMQStructure
{
    public class ConsumerMessage2 : GeneralDLL.Core.RabbitMQ.Consumer.ConsumerMessageAbstract
    {
        public ConsumerMessage2(
            AppConnectionString appConnectionString,
            ILogger<ConsumerMessage2> logger) : base(appConnectionString, logger)
        {
        }

        public override QueueData queueData => new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
        {
            autoDelete = false,
            durable = true,
            exclusive = false,
            name = "queueTopic2",
            routingKey = "*.orange"
        };

        public override Func<(string data, string routeKey, IReadOnlyBasicProperties propsFromSender, BasicDeliverEventArgs e), Task<string>> AfterReceiveData => (e) =>
        {
            var v = queueData.name + $"({queueData.routingKey}): " + e;
            logger.LogInformation(v);
            return Task.FromResult(string.Empty);
        };
    }
}