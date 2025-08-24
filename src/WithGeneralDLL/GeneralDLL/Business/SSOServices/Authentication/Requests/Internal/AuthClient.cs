// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.Authentication.Requests.Internal
{
    public class AuthClient
    {
        #region CheckToken
        public class CheckToken
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.AUTH.DataClientCheckToken>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenAuth; }

                public override string baseURL => $"client/auth/CheckToken";
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

                public GeneralDLL.DTO.SSOServices.SSO.Users.UserInfoAdmin APIResult_Data
                    => IsSuccess
                        ? base.APIResult_ResponseData.GetData<GeneralDLL.DTO.SSOServices.SSO.Users.UserInfoAdmin>()
                        : null;
            }
        }
        #endregion

        #region SetToken
        public class SetToken
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.AUTH.DataClientSSO>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenAuth; }

                public override string baseURL => $"client/auth/SetToken";
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

                public GeneralDLL.DTO.SSOServices.AUTH.JWTData APIResult_Data
                    => IsSuccess
                        ? base.APIResult_ResponseData.GetData<GeneralDLL.DTO.SSOServices.AUTH.JWTData>()
                        : null;
            }
        }
        #endregion
    }
}