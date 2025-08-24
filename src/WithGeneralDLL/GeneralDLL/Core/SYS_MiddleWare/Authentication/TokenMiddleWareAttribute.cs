// Ignore Spelling: SRT Auth

using GeneralDLL.Business.SSOServices.Authentication.Internal;
using GeneralDLL.Core.ENV;
using GeneralDLL.Core.ErrorController.Repository;
using GeneralDLL.Domain;
using GeneralDLL.DTO.SSOServices.AUTH;
using GeneralDLL.HttpClientServices;
using GeneralDLL.SRTExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;

namespace GeneralDLL.Core.SYS_MiddleWare.Authentication
{
    public class TokenMiddleWareAttribute : ActionFilterAttribute
    {
        #region Type Authentication Controller
        public enum TypeAuthenticationController
        {
            None,
            Admin,
            Client,
            ClientSSO,
            Internal
        }
        #endregion

        #region Constructors
        public TokenMiddleWareAttribute(TypeAuthenticationController controllerType,
                                        bool AllowNotAuthenticated = false,
                                        string BaseToken = null)
        {
            this.BaseToken = BaseToken;
            ControllerType = controllerType;
            this.AllowNotAuthenticated = AllowNotAuthenticated;
        }
        #endregion

        private string BaseToken { get; set; }
        private bool AllowNotAuthenticated { get; set; }

        private TypeAuthenticationController ControllerType { get; set; }

        #region Handler
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var _authServiceAdmin = context.HttpContext.RequestServices.GetRequiredService<IAuthAdmin>();
                var _authServiceClient = context.HttpContext.RequestServices.GetRequiredService<IAuthClient>();
                var _authServiceClientSSO = context.HttpContext.RequestServices.GetRequiredService<IAuthClientSSO>();

                // Get Header Info (browser/browserID)
                var dmCheckToken = CreateTokenModel(context, ControllerType);

                #region Control Authentication
                switch (ControllerType)
                {
                    case TypeAuthenticationController.None:
                        break;

                    #region Admin
                    case TypeAuthenticationController.Admin:
                        {
                            if (!dmCheckToken.isDataComplete())
                                if (!dmCheckToken.token.SRT_StringIsNullOrEmpty() || !AllowNotAuthenticated)
                                    throw new Exception("Admin");

                            var result = await _authServiceAdmin.CheckToken(dmCheckToken);
                            SetUserData(context, result);
                        }
                        break;
                    #endregion

                    #region Client
                    case TypeAuthenticationController.Client:
                        {
                            if (!dmCheckToken.isDataComplete())
                                if (!dmCheckToken.token.SRT_StringIsNullOrEmpty() || !AllowNotAuthenticated)
                                    throw new Exception("Client");

                            var result = await _authServiceClient.CheckToken(dmCheckToken);
                            SetUserData(context, result);
                        }
                        break;
                    #endregion

                    #region Client SSO
                    case TypeAuthenticationController.ClientSSO:
                        {
                            if (!dmCheckToken.isDataComplete())
                                if (!dmCheckToken.token.SRT_StringIsNullOrEmpty() || !AllowNotAuthenticated)
                                    throw new Exception("Client SSO");

                            var result = await _authServiceClientSSO.CheckToken(dmCheckToken);
                            SetUserData(context, result);
                        }
                        break;
                    #endregion

                    #region Internal
                    case TypeAuthenticationController.Internal:
                        if (dmCheckToken.token != BaseToken)
                            throw new Exception($"Error On Internal token - input: {dmCheckToken.token}");
                        break;
                    #endregion

                    default:
                        break;
                }
                #endregion

                await next();

                //Control HTTP Only Token
                ControlHTTPOnlyToken(context, dmCheckToken, ControllerType);
            }
            catch (Exception ex)
            {
                var _errorRepository = context.HttpContext.RequestServices.GetService<IErrorLogger>();
                var error = "Error In Auth Middleware => ID: " + await _errorRepository.Save_Error_ToDB(ex);

                context.Result = new ObjectResult(new ResponseData()
                {
                    status = HttpStatusCode.Unauthorized,
                    data = null,
                    message = error,
                    errorCode = null,
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
        #endregion

        #region Control HTTP Only Token

        private void ControlHTTPOnlyToken(
            ActionExecutingContext context,
            DataClientCheckToken dmCheckToken,
            TypeAuthenticationController ControllerType)
        {
            var action = context.ActionDescriptor.RouteValues["action"]?.ToLower();

            // Set Cookie From Action
            if (string.Equals(action, "verifyotp", StringComparison.OrdinalIgnoreCase))
                return;
            if (string.Equals(action, "checkpassword", StringComparison.OrdinalIgnoreCase))
                return;
            if (string.Equals(action, "SavePassword", StringComparison.OrdinalIgnoreCase))
                return;
            if (ControllerType == TypeAuthenticationController.Internal)
                return;

            if (string.Equals(action, "logout", StringComparison.OrdinalIgnoreCase))
                GeneralFunctions.SSO.SetHttpOnlyToken(context.HttpContext, "");
            else if (dmCheckToken.token.SRT_StringIsNullOrEmpty())
                GeneralFunctions.SSO.SetHttpOnlyToken(context.HttpContext, "");
            else if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                GeneralFunctions.SSO.SetHttpOnlyToken(context.HttpContext, "");
            else
                GeneralFunctions.SSO.SetHttpOnlyToken(context.HttpContext, dmCheckToken.token);
        }

        #endregion

        #region Create Auth Token Model
        private DataClientCheckToken CreateTokenModel(
            ActionExecutingContext context,
            TypeAuthenticationController ControllerType)
        {
            var httpContext = context.HttpContext;

            var token = ReadToken(httpContext, ControllerType);
            var headerData = GeneralDLL.GeneralFunctions.SSO.GetHeaderData(httpContext, "X-Device-Identity");
            var headerObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(headerData ?? "{}");

            return new DataClientCheckToken
            {
                token = token,
                browser = headerObj.TryGetValue("browser", out var browser) ? browser?.ToString() : null,
                browserID = headerObj.TryGetValue("browserID", out var browserID) ? browserID?.ToString() : null
            };
        }

        [NonAction]
        public static string ReadToken(
            HttpContext context,
            TypeAuthenticationController ControllerType,
            bool justFromCoolie = false)
        {
            var tokenHeader = "";
            if (context.Request.Headers.TryGetValue("Authorization", out var headerValue))
            {
                var parts = headerValue.ToString().Split(' ');
                tokenHeader = parts.Length == 2 ? parts[1] : "";
            }

            if (ControllerType == TypeAuthenticationController.Internal)
                return tokenHeader;

            if (context.Request.Cookies.TryGetValue("AuthToken", out var cookieToken))
            {
                if (justFromCoolie)
                    return cookieToken;
                else if (cookieToken == tokenHeader)
                    return cookieToken;
            }

            return "";
        }
        #endregion

        #region Set User Data
        private void SetUserData(ActionExecutingContext context, BaseHttpClientService apiResult)
        {
            if (apiResult.IsSuccess)
            {
                if (apiResult.APIResult_ResponseData.data == null)
                    throw new Exception("Invalid Client Data");

                context.HttpContext.Items["UserData"] = apiResult.APIResult_ResponseData.GetData<DTO.SSOServices.SSO.Users.UserInfoAdmin>();
            }
            else
            {
                if (!AllowNotAuthenticated)
                    throw new Exception(apiResult.APIResult_ResponseData.message);
            }
        }
        #endregion
    }
}