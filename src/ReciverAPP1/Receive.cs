using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using ReciverAPP1.Controllers;

namespace ReciverAPP1
{
    public class Receive
    {
        private readonly ILogger<Receive> _logger;

        public Receive(ILogger<Receive> logger)
        {
            _logger = logger;
        }

        public async Task ReadyForGet()
        {
            bool isFinished = false;

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

            // Control how many message analyze by this consumer
            await channel.BasicQosAsync(prefetchSize: 0, // Size of message in byte - 0 = no limit 
                                        prefetchCount: 1, // number of message that send to consumer for analyze
                                        global: false); // true => all connect consumer get this config
                                                        // false => just this consumer has this config

            _logger.LogInformation(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogWarning($" [x] Received {message}");

                await Task.Delay(1000);

                // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);

                isFinished = true;
            };

            await channel.BasicConsumeAsync(queue: "task_queue",
                                            autoAck: false,
                                            consumer: consumer);

            while (!isFinished)
            {
                await Task.Delay(1000);
            }
        }
    }
}
