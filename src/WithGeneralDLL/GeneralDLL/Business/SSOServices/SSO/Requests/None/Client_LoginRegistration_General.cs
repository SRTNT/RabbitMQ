// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.SSO.Requests.None
{
    public class Client_LoginRegistration_General
    {
        #region CheckAuthentication
        public class CheckAuthentication
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.SSO.Registration.DataForAuthentication>
            {
                public SRTRequest(HttpClient client) : base(client)
                { }

                public override string baseURL => $"registration/checkAuthentication";
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

        #region CheckMobile
        public class CheckMobile
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration>
            {
                public SRTRequest(HttpClient client) : base(client)
                { }

                public override string baseURL => $"registration/checkMobile";
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

        #region SendOTPForRegisteredUser
        public class SendOTPForRegisteredUser
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration>
            {
                public SRTRequest(HttpClient client) : base(client)
                { }

                public override string baseURL => $"registration/SendOTPForRegisteredUser";
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

        #region VerifyOTP
        public class VerifyOTP
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.SSO.Registration.ConfirmOTP>
            {
                public SRTRequest(HttpClient client) : base(client)
                { }

                public override string baseURL => $"registration/verifyOTP";
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

        #region CheckPassword
        public class CheckPassword
        {
            public class SRTRequest : GeneralDLL.HttpClientServices.BaseAPIRequest<GeneralDLL.DTO.SSOServices.SSO.Registration.CheckPassword>
            {
                public SRTRequest(HttpClient client) : base(client)
                { }

                public override string baseURL => $"registration/CheckPassword";
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