using Microsoft.AspNetCore.Mvc;

namespace SenderAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(1);
            return Ok();
        }
    }
}
