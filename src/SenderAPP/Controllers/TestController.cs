using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace SenderAPP.Controllers
{
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
}
