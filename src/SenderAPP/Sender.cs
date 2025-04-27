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

        public async Task SendData(string message, string severity = "info")
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
            var queueName = "log_queue";

            // Create Exchange
            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct);

            #region Create Queue
            await channel.QueueDeclareAsync(queue: queueName,
                                            durable: true, // For Not Delete if the rabbitmq crash => true
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
            #endregion

            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties
            {
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            #region Map Exchange To Queue
            await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: severity);
            #endregion

            #region Send Message
            await channel.BasicPublishAsync(exchange: exchangeName,
                                            routingKey: severity,
                                            mandatory: true, // if the rout key not existed return message to main server
                                            basicProperties: properties,
                                            body: body);
            #endregion

            _logger.LogInformation($"Sender [x] Sent {severity} : {message}");
        }
    }
}
