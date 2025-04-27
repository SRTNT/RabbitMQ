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
            int numberRecive = 0;

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

            #region Create 2 Queue
            await channel.QueueDeclareAsync(queue: "task_queue",
                                    durable: true, // For Not Delete if the rabbitmq crash => true
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            await channel.QueueDeclareAsync(queue: "task_queue2",
                                            durable: true, // For Not Delete if the rabbitmq crash => true
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null); 
            #endregion

            // Control how many message analyze by this consumer
            await channel.BasicQosAsync(prefetchSize: 0, // Size of message in byte - 0 = no limit 
                                        prefetchCount: 1, // number of message that send to consumer for analyze
                                        global: false); // true => all connect consumer get this config
                                                        // false => just this consumer has this config

            _logger.LogInformation(" [*] Waiting for messages.");

            #region Create Cunsumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                // Accessing the queue name
                var queueName = ((AsyncEventingBasicConsumer)model).Channel.CurrentQueue;

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogWarning($"{queueName} - [x] Received {message}");

                await Task.Delay(1000);

                // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);

                numberRecive++;
            };
            var consumer2 = new AsyncEventingBasicConsumer(channel);
            consumer2.ReceivedAsync += async (model, ea) =>
            {
                // Accessing the queue name
                var queueName = ((AsyncEventingBasicConsumer)model).Channel.CurrentQueue;

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogWarning($"{queueName} - [x] Received {message}");

                await Task.Delay(1000);

                // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);

                numberRecive++;
            };
            #endregion

            #region Map Consumer to queue
            await channel.BasicConsumeAsync(queue: "task_queue",
                                                autoAck: false,
                                                consumer: consumer);
            await channel.BasicConsumeAsync(queue: "task_queue2",
                                            autoAck: false,
                                            consumer: consumer2); 
            #endregion

            while (numberRecive < 4)
            {
                await Task.Delay(1000);
            }
        }
    }
}
