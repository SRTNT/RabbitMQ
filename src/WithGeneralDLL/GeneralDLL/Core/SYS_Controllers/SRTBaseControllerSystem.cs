// Ignore Spelling: SRT admin dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GeneralDLL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GeneralDLL.Core.ErrorController.Repository;
using GeneralDLL.Core.ENV;

namespace GeneralDLL.Core.SYS_Controllers
{
    public class SRTBaseControllerSystem : Controller
    {
        #region Injection Fields
        protected readonly ILogger<SRTBaseControllerSystem> _logger;

        protected IConfiguration SRT_Base_AppConfiguration => HttpContext.RequestServices.GetService<IConfiguration>();
        protected IErrorLogger SRT_Base_ErrorLogger => HttpContext.RequestServices.GetService<IErrorLogger>();
        protected AppSetting SRT_Base_AppSetting => HttpContext.RequestServices.GetService<AppSetting>();
        protected AppConnectionString SRT_Base_AppConnectionString => HttpContext.RequestServices.GetService<AppConnectionString>();
        protected ISystemENV SRT_Base_SystemENV => HttpContext.RequestServices.GetService<ISystemENV>();
        #endregion

        #region Constructors
        public SRTBaseControllerSystem(ILogger<SRTBaseControllerSystem> logger)
        { _logger = logger; }
        #endregion

        #region Response

        [NonAction]
        public async Task<IActionResult> Response_GenerateResult(ResponseData data)
        {
            switch (data.status)
            {
                case HttpStatusCode.OK:
                    return Response_OK(data.message, data.data);
                case HttpStatusCode.NotFound:
                    return Response_Error_NotFoundInputError(data.message, data.data);
                case HttpStatusCode.BadRequest:
                    return await Response_Error_InputParameterError(data.message, data.data);
                case HttpStatusCode.Unauthorized:
                    return Response_Error_Unauthorized(data.message, data.data);
                case HttpStatusCode.Conflict:
                    return Response_Error_ExistedDataBefore(data.message, data.data);
                case HttpStatusCode.InternalServerError:
                    return await Response_Error_ControlInternalServerError(data.message, new Exception(JsonSerializer.Serialize(data)));

                default:
                    throw new Exception();
            }
        }

        #region Unauthorized Error
        [NonAction]
        public virtual IActionResult Response_Error_Unauthorized(string message = "", object data = null)
        {
            return Unauthorized(new ResponseData()
            {
                status = HttpStatusCode.Unauthorized,
                data = data,
                message = message,
                errorCode = null,
            });
        }
        #endregion

        #region Input Parameter Error
        [NonAction]
        public virtual IActionResult Response_Error_NotFoundInputError(string message = null, object data = null, string url = null)
        {
            return NotFound(new ResponseData()
            {
                status = HttpStatusCode.NotFound,
                data = data,
                message = message,
                errorCode = "",
                redirectURL = url
            });
        }
        #endregion

        #region Input Parameter Error
        [NonAction]
        public virtual IActionResult Response_Error_BadRequest(string message = null, object data = null)
        {
            return BadRequest(new ResponseData()
            {
                status = HttpStatusCode.BadRequest,
                data = data,
                message = message,
                errorCode = "",
            });
        }
        #endregion

        #region Input Parameter Error
        [NonAction]
        public virtual async Task<IActionResult> Response_Error_InputParameterError(string message = null, object data = null, Exception error = null, string url = null)
        {

            var code = error is null ? "" : await SaveLogError(error);

            return BadRequest(new ResponseData()
            {
                status = HttpStatusCode.BadRequest,
                data = data,
                message = message,
                errorCode = code,
                redirectURL = url
            });
        }
        #endregion

        #region OK
        [NonAction]
        public virtual IActionResult Response_OK(string message = null, object data = null, string url = null)
        {
            return Ok(new ResponseData()
            {
                status = HttpStatusCode.OK,
                data = data,
                message = message,
                errorCode = null,
                redirectURL = url
            });
        }
        #endregion

        #region Existed Data Before / Finished Process Before
        /// <summary>
        /// Finished Process Before
        /// Existed Data Before
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IActionResult Response_Error_FinishedProcessBefore(string message = null, object data = null)
            => Response_Error_ExistedDataBefore(message, data);
        [NonAction]
        public virtual IActionResult Response_Error_FoundDataBefore(string message = null, object data = null)
            => Response_Error_ExistedDataBefore(message, data);

        /// <summary>
        /// Finished Process Before
        /// Existed Data Before
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public virtual IActionResult Response_Error_ExistedDataBefore(string message = null, object data = null)
        {
            return Conflict(new ResponseData()
            {
                status = HttpStatusCode.Conflict,
                data = data,
                message = message,
                errorCode = null
            });
        }
        #endregion

        #region Not Response From Child
        [NonAction]
        public virtual IActionResult Response_Error_NotResponseFromChild(string message = null, object data = null)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new ResponseData()
            {
                status = HttpStatusCode.BadRequest,
                data = data,
                message = message,
                errorCode = null
            });
        }
        [NonAction]
        public virtual IActionResult Response_Error_NotResponseFromChild(string message, ResponseData responseData)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new ResponseData()
            {
                status = HttpStatusCode.BadRequest,
                data = JsonSerializer.Serialize(responseData),
                message = message,
                errorCode = responseData.errorCode
            });
        }
        #endregion

        #region public Server Error
        [NonAction]
        public async Task<IActionResult> Response_Error_ControlInternalServerError(string message, Exception ex)
        {
            var codeError = await SaveLogError(ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseData()
            {
                status = HttpStatusCode.InternalServerError,
                data = null,
                message = message,
                errorCode = codeError
            });
        }

        [NonAction]
        protected async Task<string> SaveLogError(Exception ex)
        {
            var v = "Error Log => ID: " + await SRT_Base_ErrorLogger.Save_Error_ToDB(ex);
            _logger.LogError(v);

            return v;
        }
        #endregion

        #endregion

        #region Get User
        [NonAction]
        public virtual T GetUser<T>()
            where T : class
        {
            if (HttpContext.Items.TryGetValue("UserData", out var userData))
            {
                var dm = userData as T;
                if (dm is null)
                    dm = JsonSerializer.Deserialize<T>(userData.ToString());

                return dm;
            }

            return null;
        }

        #region Get User Info
        [NonAction]
        public virtual DTO.SSOServices.SSO.Users.UserInfoAdmin GetUserInfo()
        {
            try
            {
                return GetUser<DTO.SSOServices.SSO.Users.UserInfoAdmin>();
            }
            catch { return null; }
        }
        #endregion

        #endregion
    }
}