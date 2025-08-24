using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Sender.RabbitMQStructure
{
    public class ConsumerResult : GeneralDLL.Core.RabbitMQ.Consumer.ConsumerOfPublisherResultAbstract
    {
        public ConsumerResult(
            AppConnectionString appConnectionString,
            ILogger<ConsumerResult> logger) : base(appConnectionString, logger)
        {
        }

        public override string QueueName => "TempData1234";

        protected override async Task AfterReceiveRPCData(string data, BasicDeliverEventArgs e)
        {
            var v = queueData.name + $"({queueData.routingKey}): " + data + " => " + e.BasicProperties.ReplyTo;
            logger.LogInformation(v);

            await Task.Delay(2000);
        }
    }
}