using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace SenderAPP
{
    public class Sender
    {
        private readonly ILogger<Sender> _logger;

        public Sender(ILogger<Sender> logger)
        {
            _logger = logger;
        }

        public async Task SendData(string message, string key = "s.saeed.e")
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "sa",
                Password = "SaeedTNT220",
                VirtualHost = "/"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var exchangeName = "logs";

            var lstQueue = new[]
            {
                new {QueueName="log_queue1",routingKey="*.image.#"},
                new {QueueName="log_queue2",routingKey="*.png"},
                new {QueueName="log_queue3",routingKey="#.saeed.#"},
            };

            // Create Exchange
            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Topic);

            #region Create Queue

            foreach (var queue in lstQueue)
            {
                await channel.QueueDeclareAsync(queue: queue.QueueName,
                                                durable: true, // For Not Delete if the rabbitmq crash => true
                                                exclusive: false,
                                                autoDelete: false,
                                                arguments: null);
            }

            #endregion

            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties
            {
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            #region Map Exchange To Queue
            foreach (var queue in lstQueue)
            {
                await channel.QueueBindAsync(queue: queue.QueueName, exchange: exchangeName, routingKey: queue.routingKey);
            }
            #endregion

            #region Send Message
            await channel.BasicPublishAsync(exchange: exchangeName,
                                            routingKey: key,
                                            mandatory: true, // if the rout key not existed return message to main server
                                            basicProperties: properties,
                                            body: body);
            #endregion

            _logger.LogInformation($"Sender [x] Sent {key} : {message}");
        }
    }
}
