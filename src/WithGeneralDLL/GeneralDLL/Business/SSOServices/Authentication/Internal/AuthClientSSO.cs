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

    public partial interface IAuthClientSSO
    {
        Task<Requests.Internal.AuthClientSSO.GetTokenBrowserGenerateIfNotExisted.SRTResponse> GetTokenBrowserGenerateIfNotExisted(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck);

        Task<Requests.Internal.AuthClientSSO.Logout.SRTResponse> Logout(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck);

        Task<Requests.Internal.AuthClientSSO.CheckToken.SRTResponse> CheckToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck);
    }

    public partial class AuthClientSSO : IAuthClientSSO
    {
        private readonly HttpClient _client;

        #region Constructors
        public AuthClientSSO(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region GetTokenBrowserGenerateIfNotExisted
        public async Task<Requests.Internal.AuthClientSSO.GetTokenBrowserGenerateIfNotExisted.SRTResponse> GetTokenBrowserGenerateIfNotExisted(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck)
        {
            try
            {
                var exe = new Requests.Internal.AuthClientSSO.GetTokenBrowserGenerateIfNotExisted.SRTResponse
                    (new Requests.Internal.AuthClientSSO.GetTokenBrowserGenerateIfNotExisted.SRTRequest(_client)
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
        public async Task<Requests.Internal.AuthClientSSO.Logout.SRTResponse> Logout(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck)
        {
            try
            {
                var exe = new Requests.Internal.AuthClientSSO.Logout.SRTResponse
                    (new Requests.Internal.AuthClientSSO.Logout.SRTRequest(_client)
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

        #region CheckToken
        public async Task<Requests.Internal.AuthClientSSO.CheckToken.SRTResponse> CheckToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck)
        {
            try
            {
                var exe = new Requests.Internal.AuthClientSSO.CheckToken.SRTResponse
                    (new Requests.Internal.AuthClientSSO.CheckToken.SRTRequest(_client)
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