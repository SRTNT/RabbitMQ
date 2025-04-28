using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Text;
using System.Net.Sockets;

namespace SenderAPP
{
    public class Sender : IAsyncDisposable
    {
        bool getResult = false;

        private readonly ILogger<Sender> _logger;

        public Sender(ILogger<Sender> logger)
        {
            _logger = logger;
        }

        private const string QUEUE_NAME = "rpc_queue";

        private string CorrelationId = "";
        private string result = "";

        private IConnection? _connection;
        private IChannel? _channel;
        private string? _replyQueueName;

        public async Task<string> SendMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "sa",
                Password = "SaeedTNT220",
                VirtualHost = "/"
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await MakeReadyCallBack();

            await SendMessageToReciver(message);

            while (!getResult)
            {
                await Task.Delay(1000);
            }

            return result;
        }

        #region Make Ready for result
        private async Task MakeReadyCallBack()
        {
            // declare a server-named queue - temp queue
            var queueDeclareResult = await _channel.QueueDeclareAsync();

            _replyQueueName = queueDeclareResult.QueueName;

            #region Create Cunsumer
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                string? correlationId = ea.BasicProperties.CorrelationId;
                result = "";

                if (false == string.IsNullOrEmpty(correlationId))
                {
                    if (this.CorrelationId == correlationId)
                    {
                        var body = ea.Body.ToArray();
                        result = Encoding.UTF8.GetString(body);
                    }
                }

                _logger.LogWarning($"Get Result: {result}");
                getResult = true;

                return Task.CompletedTask;
            };
            #endregion

            await _channel.BasicConsumeAsync(queue: _replyQueueName,
                                             autoAck: true,
                                             consumer: consumer);
        }
        #endregion

        #region Send Message
        private async Task SendMessageToReciver(string message, CancellationToken cancellationToken = default)
        {
            CorrelationId = Guid.NewGuid().ToString();
            var props = new BasicProperties
            {
                CorrelationId = CorrelationId,
                ReplyTo = _replyQueueName
            };

            var messageBytes = Encoding.UTF8.GetBytes(message);
            await _channel.BasicPublishAsync(exchange: string.Empty,
                                             routingKey: QUEUE_NAME,
                                             mandatory: true,
                                             basicProperties: props,
                                             body: messageBytes);
        }
        #endregion

        public async ValueTask DisposeAsync()
        {
            if (_channel is not null)
            {
                await _channel.CloseAsync();
            }

            if (_connection is not null)
            {
                await _connection.CloseAsync();
            }
        }
    }
}