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

        public async Task SendData(string message)
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

            await channel.QueueDeclareAsync(queue: "task_queue",
                                            durable: true, // For Not Delete if the rabbitmq crash => true
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties
            {
                Persistent = true // if the server has crash:
                                  // true:  keep message for next run
                                  // false: delete message
            };

            await channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: "task_queue",
                                            mandatory: true, // if the rout key not existed return message to main server
                                            basicProperties: properties,
                                            body: body);

            _logger.LogInformation($" [x] Sent {message}");
        }
    }
}
