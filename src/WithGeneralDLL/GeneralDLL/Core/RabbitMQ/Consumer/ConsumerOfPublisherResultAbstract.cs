using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Domain;
using GeneralDLL.SRTExtensions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RabbitMQ.Consumer
{
    public abstract class ConsumerOfPublisherResultAbstract : ConsumerMessageAbstract
    {
        public abstract string QueueName { get; }
        protected abstract Task AfterReceiveRPCData(string data, BasicDeliverEventArgs e);

        public override QueueData queueData => new QueueData()
        {
            name = QueueName,
            autoDelete = false,
            durable = true,
            exclusive = false,
            routingKey = string.Empty,
            autoAck = false,
        };

        #region Constructors
        protected ConsumerOfPublisherResultAbstract(
            AppConnectionString appConnectionString,
            ILogger<ConsumerOfPublisherResultAbstract> logger) : base(appConnectionString, logger) { }
        #endregion

        public override async Task<string> AfterReceiveData(string data, BasicDeliverEventArgs e)
        {
            string correlationId = e.BasicProperties.CorrelationId;

            if (!string.IsNullOrEmpty(correlationId))
            {
                await AfterReceiveRPCData(data, e);
            }

            return null;
        }
    }
}
