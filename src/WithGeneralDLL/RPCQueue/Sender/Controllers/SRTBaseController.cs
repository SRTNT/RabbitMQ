// Ignore Spelling: Backend

using GeneralDLL.SRTExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace Sender.Controllers
{
    [ApiController]
    public class SRTBaseController : GeneralDLL.Core.SYS_Controllers.SRTBaseControllerSystem
    {
        #region Injection Fields
        #endregion

        #region Constructors
        public SRTBaseController(ILogger<SRTBaseController> logger) : base(logger)
        {
        }
        #endregion


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            try
            {
                var header = GeneralDLL.GeneralFunctions.SSO.GetHeaderData(HttpContext, "X-Device-Identity");
                var headerObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(header);

                headerBrowser = headerObject?.FirstOrDefault(q => q.Key == "browser").Value.ToString() ?? "";
                headerBrowserID = headerObject?.FirstOrDefault(q => q.Key == "browserID").Value.ToString() ?? "";
            }
            catch { }
        }

        protected string headerBrowser = string.Empty;
        protected string headerBrowserID = string.Empty;
    }
}
