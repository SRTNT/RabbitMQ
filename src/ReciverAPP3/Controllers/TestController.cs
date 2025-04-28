using Microsoft.AspNetCore.Mvc;

namespace ReciverAPP3.Controllers
{
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
}
