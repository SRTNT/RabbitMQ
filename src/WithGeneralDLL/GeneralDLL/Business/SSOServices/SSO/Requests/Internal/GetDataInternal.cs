// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.SSO.Requests.Internal
{
    public class GetDataInternal
    {
        #region GetByMobile
        public class GetByMobile
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<object>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenSSO; }

                public override string baseURL => $"InternalOTP/getByMobile/{mobile}";
                public override HttpClientServices.RequestType requestType => HttpClientServices.RequestType.Get;
                public override string token { get; }

                //اطلاعات ورودی برای درخواست
                public long mobile { get; set; }
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
    }
}