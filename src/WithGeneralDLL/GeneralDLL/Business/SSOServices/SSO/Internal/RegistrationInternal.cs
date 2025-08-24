// Ignore Spelling: jwt dm app dto srt Meka otp shaba metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.SSO.Internal
{

    public partial interface IRegistrationInternal
    {
        Task<Requests.Internal.RegistrationInternal.Check1TimeToken.SRTResponse> Check1TimeToken(GeneralDLL.DTO.SSOServices.SSO.Registration.GetJWTFrom1Token data);
    }

    public partial class RegistrationInternal : IRegistrationInternal
    {
        private readonly HttpClient _client;

        #region Constructors
        public RegistrationInternal(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region Check1TimeToken
        public async Task<Requests.Internal.RegistrationInternal.Check1TimeToken.SRTResponse> Check1TimeToken(GeneralDLL.DTO.SSOServices.SSO.Registration.GetJWTFrom1Token data)
        {
            try
            {
                var exe = new Requests.Internal.RegistrationInternal.Check1TimeToken.SRTResponse
                    (new Requests.Internal.RegistrationInternal.Check1TimeToken.SRTRequest(_client)
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