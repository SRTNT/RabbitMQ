// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.SSO.Requests.None
{
    public class ClientSSO_Info
    {
        #region GetInfo
        public class GetInfo
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<object>
            {
                public SRTRequest(HttpClient client) : base(client)
                { }

                public override string baseURL => $"clientSSO/GetInfo";
                public override HttpClientServices.RequestType requestType => HttpClientServices.RequestType.Get;
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

                public GeneralDLL.DTO.SSOServices.SSO.Users.ClientData.ClientInfoAdmin APIResult_Data
                    => IsSuccess
                        ? base.APIResult_ResponseData.GetData<GeneralDLL.DTO.SSOServices.SSO.Users.ClientData.ClientInfoAdmin>()
                        : null;
            }
        }
        #endregion

        #region Logout
        public class Logout
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<object>
            {
                public SRTRequest(HttpClient client) : base(client)
                { }

                public override string baseURL => $"clientSSO/logout";
                public override HttpClientServices.RequestType requestType => HttpClientServices.RequestType.Delete;
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