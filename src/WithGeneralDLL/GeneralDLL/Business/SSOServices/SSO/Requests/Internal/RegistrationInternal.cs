// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.SSO.Requests.Internal
{
    public class RegistrationInternal
    {
        #region Check1TimeToken
        public class Check1TimeToken
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.SSO.Registration.GetJWTFrom1Token>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenSSO; }

                public override string baseURL => $"InternalOTP/Check1TimeToken";
                public override HttpClientServices.RequestType requestType => HttpClientServices.RequestType.Post;
                public override string token { get; }
            }

            /// <summary>
            /// TOut: string
            /// </summary>
            public class SRTResponse : GeneralDLL.HttpClientServices.BaseHttpClientService
            {
                public SRTResponse(SRTRequest request) : base(request)
                { }

                public override async Task<SRTResponse> SendRequest()
                {
                    try
                    {
                        await base.SendRequestAsync();

                        return this;
                    }
                    catch (Exception ex)
                    { throw new Exception("SendRequest - GetAll", ex); }
                }

                public string APIResult_Data
                    => IsSuccess
                        ? (string)base.APIResult_ResponseData.data
                        : "";
            }
        }
        #endregion
    }
}