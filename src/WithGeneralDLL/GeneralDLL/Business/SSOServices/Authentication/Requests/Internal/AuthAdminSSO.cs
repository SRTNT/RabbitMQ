// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.Authentication.Requests.Internal
{
    public class AuthAdminSSO
    {
        #region GetTokenBrowserGenerateIfNotExisted
        public class GetTokenBrowserGenerateIfNotExisted
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenAuth; }

                public override string baseURL => $"admin/authSSO/GetTokenBrowserGenerateIfNotExisted";
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

        #region Logout
        public class Logout
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenAuth; }

                public override string baseURL => $"admin/authSSO/logout";
                public override HttpClientServices.RequestType requestType => HttpClientServices.RequestType.Post;
                public override string token { get; }
            }

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
            }
        }
        #endregion
    }
}