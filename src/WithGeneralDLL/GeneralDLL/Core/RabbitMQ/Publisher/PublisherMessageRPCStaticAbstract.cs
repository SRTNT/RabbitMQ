using GeneralDLL.Core.RabbitMQ.Consumer;
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
    public abstract class PublisherMessageRPCStaticAbstract : PublisherMessageAbstract
    {
        public abstract string CallBackQueueName { get; }

        #region Constructors
        protected PublisherMessageRPCStaticAbstract(
            AppConnectionString appConnectionString,
            ILogger<PublisherMessageRPCStaticAbstract> logger) : base(appConnectionString, logger)
        {
        }
        #endregion

        #region Send Message
        public override async Task SendMessage(string message, string routeKey = null)
        {
            basicProperties = new BasicProperties
            {
                CorrelationId = Guid.NewGuid().ToString(),
                ReplyTo = CallBackQueueName,
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            await base.SendMessage(message, routeKey);
        }
        #endregion
    }
}
