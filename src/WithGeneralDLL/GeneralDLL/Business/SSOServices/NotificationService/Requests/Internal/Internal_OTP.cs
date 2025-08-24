// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.NotificationService.Requests.Internal
{
    public class Internal_OTP
    {
        #region SendOTP
        public class SendOTP
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<object>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenSMS; }

                public override string baseURL => $"InternalOTP/sendOTP/{mobile}";
                public override HttpClientServices.RequestType requestType => HttpClientServices.RequestType.Post;
                public override string token { get; }

                //اطلاعات ورودی برای درخواست
                public long mobile { get; set; }
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

        #region VerifyOTP
        public class VerifyOTP
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<object>
            {
                public SRTRequest(HttpClient client) : base(client)
                { token = GeneralDLL.Domain.SecuritySystem.TokenSMS; }

                public override string baseURL => $"InternalOTP/verifyOTP/{mobile}/{otp}/{senderType}";
                public override HttpClientServices.RequestType requestType => HttpClientServices.RequestType.Post;
                public override string token { get; }

                //اطلاعات ورودی برای درخواست
                public long mobile { get; set; }
                public string otp { get; set; }
                public GeneralDLL.DTO.SSOServices.SystemEnums.OTPSenderType senderType { get; set; }
            }

            /// <summary>
            /// TOut: bool
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

                public bool APIResult_Data
                    => IsSuccess
                        ? (bool)base.APIResult_ResponseData.data
                        : false;
            }
        }
        #endregion
    }
}