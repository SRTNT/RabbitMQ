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

    public partial interface IAuthAdmin
    {
        Task<Requests.Internal.AuthAdmin.CheckToken.SRTResponse> CheckToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck);

        Task<Requests.Internal.AuthAdmin.SetToken.SRTResponse> SetToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientSSO data);
    }

    public partial class AuthAdmin : IAuthAdmin
    {
        private readonly HttpClient _client;

        #region Constructors
        public AuthAdmin(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region CheckToken
        public async Task<Requests.Internal.AuthAdmin.CheckToken.SRTResponse> CheckToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken tokenCheck)
        {
            try
            {
                var exe = new Requests.Internal.AuthAdmin.CheckToken.SRTResponse
                    (new Requests.Internal.AuthAdmin.CheckToken.SRTRequest(_client)
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
        public async Task<Requests.Internal.AuthAdmin.SetToken.SRTResponse> SetToken(GeneralDLL.DTO.SSOServices.AUTH.DataClientSSO data)
        {
            try
            {
                var exe = new Requests.Internal.AuthAdmin.SetToken.SRTResponse
                    (new Requests.Internal.AuthAdmin.SetToken.SRTRequest(_client)
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