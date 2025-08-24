// Ignore Spelling: SRT admin dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GeneralDLL.Domain;
using System.Diagnostics;

namespace GeneralDLL.Core.SYS_Controllers
{
    public class SRTBaseTestConnectionController : SRTBaseControllerSystem
    {
        #region Constructors
        public SRTBaseTestConnectionController(ILogger<SRTBaseTestConnectionController> logger) : base(logger)
        {
        }
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region API - Test
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(ResponseData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseData), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Test()
        {
            try
            {
                await Task.Delay(100);
                return Response_OK(null, null);
            }
            catch (Exception ex)
            { return await Response_Error_ControlInternalServerError("", SetErrorFunction(new Exception(new StackTrace().GetFrame(1).GetMethod().Name, ex))); }
        }
        #endregion
    }
}