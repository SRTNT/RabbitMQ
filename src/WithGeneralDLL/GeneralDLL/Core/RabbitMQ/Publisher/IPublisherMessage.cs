using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RabbitMQ.Publisher
{
    public interface IPublisherMessage
    {
        Domain.ExchangeData exchangeData { get; }

        List<Domain.QueueData> lstQueueData { get; }

        Task SendMessage(string message, string routeKey = null);
    }
}
