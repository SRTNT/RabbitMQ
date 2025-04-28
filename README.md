<h3 align="center">A Passionate Backend-Frontend Developer from Iran</h3>

## Table of Contents
- [About Me](#about-me)
- [Connect with Me](#connect-with-me)
- [Languages and Tools](#languages-and-tools)
- [GitHub Stats](#github-stats)
- [Projects Overview](#projects-overview)
  - [Hello World](#hello-world)
  - [Work Queues](#work-queues)
  - [Direct Exchange - Fanout](#direct-exchange---fanout)
  - [Direct Exchange - Direct](#direct-exchange---direct)
  - [Conditional Exchange - Topic](#conditional-exchange---topic)
- [Additional Resources](#additional-resources)

<p align="left">
  <img src="https://komarev.com/ghpvc/?username=srtnt&label=Profile%20views&color=0e75b6&style=flat" alt="srtnt" />
</p>

<p align="left">
  <a href="https://github.com/ryo-ma/github-profile-trophy">
    <img src="https://github-profile-trophy.vercel.app/?username=srtnt" alt="srtnt" />
  </a>
</p>

<p align="left">
  <a href="https://twitter.com/" target="_blank">
    <img src="https://img.shields.io/twitter/follow/?logo=twitter&style=for-the-badge" alt="Twitter Badge" />
  </a>
</p>

---

### About Me
- 🔭 **Current Project:** [Swagger Test](https://github.com/SRTNT/RabbitMQ)
- 🌱 **Learning:** Linux, Docker, Kubernetes
- 👨‍💻 **Projects:** [My GitHub](https://github.com/SRTNT)
- 💬 **Ask Me About:** C#, Platform Design
- 📫 **Contact:** s.r.taheri@gmail.com

---

### Connect with Me
<p align="left">
  <!-- Add links to your social media or professional profiles here -->
</p>

---

### Languages and Tools
<p align="left">
  <a href="https://www.gnu.org/software/bash/" target="_blank" rel="noreferrer">
    <img src="https://www.vectorlogo.zone/logos/gnu_bash/gnu_bash-icon.svg" alt="bash" width="40" height="40"/>
  </a>
  <a href="https://www.blender.org/" target="_blank" rel="noreferrer">
    <img src="https://download.blender.org/branding/community/blender_community_badge_white.svg" alt="blender" width="40" height="40"/>
  </a>
  <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer">
    <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/>
  </a>
  <!-- Add other tools here -->
</p>

---

### GitHub Stats
<p align="left">
  <img align="left" src="https://github-readme-stats.vercel.app/api/top-langs?username=srtnt&show_icons=true&locale=en&layout=compact" alt="Top Languages" />
</p>
<p>&nbsp;<img align="center" src="https://github-readme-stats.vercel.app/api?username=srtnt&show_icons=true&locale=en" alt="GitHub Stats" /></p>

---

## Projects Overview

### Hello World
#### Step 1: Configure Sender
```csharp
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

        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
        
        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
        
        _logger.LogInformation($" [x] Sent {message}");
    }
}
```
- Dependency Injection:
```csharp
builder.Services.AddScoped<Sender>();
```
- Controller Action:
```csharp
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly Sender sender;

        public TestController(ILogger<TestController> logger, Sender sender)
        {
            _logger = logger;
            this.sender = sender;
        }

        [HttpGet()]
        [Route("{message}")]
        public async Task<IActionResult> Get(string message)
        {
            await sender.SendData(message);

            return Ok();
        }
    }
```
#### Step 2: Configure Receiver
- Worker:
```csharp
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

            await channel.QueueDeclareAsync(queue: "hello",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            _logger.LogInformation(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogWarning($" [x] Received {message}");

                isFinished = true;

                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: "hello", 
                                            autoAck: true,
                                            consumer: consumer);

            while (!isFinished)
            {
                await Task.Delay(1000);
            }
        }
    }
```
- Dependency Injection:
```csharp
builder.Services.AddScoped<Receive>();
```
- Controller Action:
```csharp
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly Receive receive;

        public TestController(ILogger<TestController> logger, Receive receive)
        {
            _logger = logger;
            this.receive = receive;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            await receive.ReadyForGet();

            return Ok();
        }
    }
```

#### Source Code
- [Code](https://github.com/SRTNT/RabbitMQ/tree/HelloWorld)
- [RabbitMQ Documentation](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet)

### Work Queues
#### Step 1: Configure sender
- Worker
```
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
```
- Dependency Injection:
```
builder.Services.AddScoped<Sender>();
```
- Controller Action:
```
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly Sender sender;

        public TestController(ILogger<TestController> logger, Sender sender)
        {
            _logger = logger;
            this.sender = sender;
        }

        [HttpGet()]
        [Route("{message}")]
        public async Task<IActionResult> Get(string message)
        {
            await sender.SendData(message);

            return Ok();
        }
    }
```
#### Step 2: Configure Reciver
- Worker
```
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
```
- Dependency Injection:
```
builder.Services.AddScoped<Receive>();
```
- Controller Action:
```
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly Receive receive;

        public TestController(ILogger<TestController> logger, Receive receive)
        {
            _logger = logger;
            this.receive = receive;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            await receive.ReadyForGet();

            return Ok();
        }
    }
```

#### Source
- [Code](https://github.com/SRTNT/RabbitMQ/tree/Work-Queues)
- [RabbitMQ Documentation](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet)


### Direct Exchange - Fanout
#### Step 1: Configure sender
- create Exchange
- create Queue
- Map Exchange To Queue
- Send Message

#### Step 2: Configure Reciver
- create Queue
- create Cunsumer
- map cunsumer to queue

#### Points
- publish message to more than 1 queue
- each queue recive anc ACT message seperatly

#### Source
- [Code](https://github.com/SRTNT/RabbitMQ/tree/Direct-Exchange-(fanout))
- [RabbitMQ Documentation](https://www.rabbitmq.com/tutorials/tutorial-three-dotnet)


### Direct Exchange - Direct
#### Step 1: Configure sender
- create Exchange
- create Queue
- Map Exchange To Queue With Routing Key
- Send Message

#### Step 2: Configure Reciver
- create Queue
- create Cunsumer - Show Routing Key with value
- map cunsumer to queue

#### Points
- publish message to more than 1 queue
- each queue recive anc ACT message seperatly
- you can configure connection between Exchange and Queue with in reciver in place of sender

#### Source
- [Code](https://github.com/SRTNT/RabbitMQ/tree/Route-Exchange-(Direct))
- [RabbitMQ Documentation](https://www.rabbitmq.com/tutorials/tutorial-four-dotnet)


### Conditional Exchange - Topic
#### Step 1: Configure sender
- create Exchange
- create 3 Queue
- Map Exchange To Queue With Routing Key with deference value
    - '*.image.#'
    - '#.saeed.#'
    - '*.png'
- Send Message

#### Step 2: Configure Reciver
- create Queue in each App
- create Cunsumer - Show Routing Key with value
- map cunsumer to queue

#### Points
- '*' (star) can substitute for exactly one word.
- '#' (hash) can substitute for zero or more words.
- each queue recive anc ACT message seperatly
- you can configure connection between Exchange and Queue with in reciver in place of sender

#### Source
- [Code](https://github.com/SRTNT/RabbitMQ/tree/Topics-Exchange-(Topics))
- [RabbitMQ Documentation](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet#topic-exchange)


### remote Procedure Call - RPC
#### Step 1: Configure sender
- create Main Queue for send Request
- create Callback Queue for get Response
- create correlation id for check result id
- Send Message with call back and correlation id

#### Step 2: Configure Reciver
- create Main Queue for send Request
- after get message from Main Queue
    - execute function
    - return result to Callback Queue with correlation id

#### Source
- [Code](https://github.com/SRTNT/RabbitMQ/tree/Remote-Procedure-Call-(RPC))
- [RabbitMQ Documentation](https://www.rabbitmq.com/tutorials/tutorial-six-dotnet)
