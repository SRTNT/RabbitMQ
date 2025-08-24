// Ignore Spelling: Backend sms otp Admin auth dm sso

using GeneralDLL.SRTExceptions;
using GeneralDLL.SRTExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.Diagnostics;
using System.Text.Json;

namespace Sender.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : SRTBaseController
    {
        private readonly RabbitMQStructure.PublisherMessage publisherMessage;

        #region Constructors
        public TestController(
            ILogger<TestController> logger, RabbitMQStructure.PublisherMessage publisherMessage) : base(logger)
        {
            this.publisherMessage = publisherMessage;
        }
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region Check Authentication
        [HttpPost]
        [Route("send")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> send(string message = "saeed", int routeKeyID = 1)
        {
            try
            {
                var routeKey = routeKeyID == 1 ? "saeed.orange.top"
                                               : routeKeyID == 2 ? "saeed.orange"
                                                                 : routeKeyID == 3 ? "saeed.ask.orange"
                                                                                   : routeKeyID == 4 ? "orange.top.saeed.ali"
                                                                                                     : "saeed";

                await publisherMessage.SendMessage(message, routeKey);
                return Response_OK(data: true);
            }
            catch (Exception ex)
            { return await Response_Error_ControlInternalServerError("", SetErrorFunction(new Exception(new StackTrace().GetFrame(1)!.GetMethod()!.Name, ex))); }
        }
        #endregion
    }
}