// Ignore Spelling: jwt dm app dto srt Meka otp shaba metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.Authentication.Internal
{

    public partial interface IAuthAdminSSO
    {
        Task<Requests.Internal.AuthAdminSSO.GetTokenBrowserGenerateIfNotExisted.SRTResponse> GetTokenBrowserGenerateIfNotExisted(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck);

        Task<Requests.Internal.AuthAdminSSO.Logout.SRTResponse> Logout(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck);
    }

    public partial class AuthAdminSSO : IAuthAdminSSO
    {
        private readonly HttpClient _client;

        #region Constructors
        public AuthAdminSSO(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region GetTokenBrowserGenerateIfNotExisted
        public async Task<Requests.Internal.AuthAdminSSO.GetTokenBrowserGenerateIfNotExisted.SRTResponse> GetTokenBrowserGenerateIfNotExisted(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck)
        {
            try
            {
                var exe = new Requests.Internal.AuthAdminSSO.GetTokenBrowserGenerateIfNotExisted.SRTResponse
                    (new Requests.Internal.AuthAdminSSO.GetTokenBrowserGenerateIfNotExisted.SRTRequest(_client)
                    {
                        Data = tokenCheck
                    });
                await exe.SendRequest();

                return exe;
            }
            catch (Exception ex)
            { throw SetErrorFunction(new Exception(new StackTrace().GetFrame(1).GetMethod().Name, ex)); }
        }
        #endregion

        #region Logout
        public async Task<Requests.Internal.AuthAdminSSO.Logout.SRTResponse> Logout(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck)
        {
            try
            {
                var exe = new Requests.Internal.AuthAdminSSO.Logout.SRTResponse
                    (new Requests.Internal.AuthAdminSSO.Logout.SRTRequest(_client)
                    {
                        Data = tokenCheck
                    });
                await exe.SendRequest();

                return exe;
            }
            catch (Exception ex)
            { throw SetErrorFunction(new Exception(new StackTrace().GetFrame(1).GetMethod().Name, ex)); }
        }
        #endregion
    }
}