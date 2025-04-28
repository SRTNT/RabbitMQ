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
            const string QUEUE_NAME = "rpc_queue";

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

            await channel.QueueDeclareAsync(queue: QUEUE_NAME,
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            await channel.BasicQosAsync(prefetchSize: 0,
                                        prefetchCount: 1,
                                        global: false);

            int numberRecive = 0;

            #region Consumer
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (object sender, BasicDeliverEventArgs ea) =>
            {
                AsyncEventingBasicConsumer cons = (AsyncEventingBasicConsumer)sender;
                IChannel ch = cons.Channel;
                string response = string.Empty;

                byte[] body = ea.Body.ToArray();

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    int n = int.Parse(message);
                    _logger.LogInformation($" [.] message: {message}");
                    response = Fib(n).ToString();
                }
                catch (Exception e)
                {
                    _logger.LogError($" [.] {e.Message}");
                    response = string.Empty;
                }
                finally
                {
                    numberRecive++;

                    // return result to client
                    var replyProps = new BasicProperties
                    { CorrelationId = ea.BasicProperties.CorrelationId };

                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    await ch.BasicPublishAsync(exchange: string.Empty,
                                               routingKey: ea.BasicProperties.ReplyTo!,
                                               mandatory: true,
                                               basicProperties: replyProps,
                                               body: responseBytes);

                    _logger.LogWarning($" [x] Send Result: {response} to {ea.BasicProperties.ReplyTo}");
                    await ch.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };
            #endregion

            await channel.BasicConsumeAsync(QUEUE_NAME, false, consumer);

            _logger.LogInformation("Ready For Get Message ........................");

            // Assumes only valid positive integer input.
            // Don't expect this one to work for big numbers,
            // and it's probably the slowest recursive implementation possible.
            static int Fib(int n)
            {
                if (n is 0 or 1)
                {
                    return n;
                }

                return Fib(n - 1) + Fib(n - 2);
            }

            while (numberRecive < 10)
            {
                await Task.Delay(1000);
            }

            _logger.LogCritical("Stop Get Message ........................");
        }
    }
}
