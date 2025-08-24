using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.RabbitMQStructure
{
    public class ConsumerMessage1 : GeneralDLL.Core.RabbitMQ.Consumer.ConsumerMessageAbstract
    {
        public ConsumerMessage1(
            AppConnectionString appConnectionString,
            ILogger<ConsumerMessage1> logger) : base(appConnectionString, logger)
        {
        }

        public override QueueData queueData => new GeneralDLL.Core.RabbitMQ.Domain.QueueData()
        {
            autoDelete = false,
            durable = true,
            exclusive = false,
            name = "queueNormal1",
            routingKey = string.Empty
        };

        public override Func<(string data, string routeKey, IReadOnlyBasicProperties propsFromSender, BasicDeliverEventArgs e), Task<string>> AfterReceiveData => (e) =>
        {
            var v = queueData.name + $"({queueData.routingKey}): " + e;
            logger.LogInformation(v);
            return Task.FromResult(string.Empty);
        };
    }
}