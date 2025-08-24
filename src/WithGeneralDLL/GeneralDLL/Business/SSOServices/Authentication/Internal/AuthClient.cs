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

    public partial interface IAuthClient
    {
        Task<Requests.Internal.AuthClient.CheckToken.SRTResponse> CheckToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck);

        Task<Requests.Internal.AuthClient.SetToken.SRTResponse> SetToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientSSO data);
    }

    public partial class AuthClient : IAuthClient
    {
        private readonly HttpClient _client;

        #region Constructors
        public AuthClient(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region CheckToken
        public async Task<Requests.Internal.AuthClient.CheckToken.SRTResponse> CheckToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck)
        {
            try
            {
                var exe = new Requests.Internal.AuthClient.CheckToken.SRTResponse
                    (new Requests.Internal.AuthClient.CheckToken.SRTRequest(_client)
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

        #region SetToken
        public async Task<Requests.Internal.AuthClient.SetToken.SRTResponse> SetToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientSSO data)
        {
            try
            {
                var exe = new Requests.Internal.AuthClient.SetToken.SRTResponse
                    (new Requests.Internal.AuthClient.SetToken.SRTRequest(_client)
                    {
                        Data = data
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