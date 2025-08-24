// Ignore Spelling: jwt dm app dto srt Meka otp shaba metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.NotificationService.Internal
{

    public partial interface IInternal_OTP
    {
        Task<Requests.Internal.Internal_OTP.SendOTP.SRTResponse> SendOTP(long mobile);

        Task<Requests.Internal.Internal_OTP.VerifyOTP.SRTResponse> VerifyOTP(long mobile, string otp, GeneralDLL.DTO.SSOServices.SystemEnums.OTPSenderType senderType);
    }

    public partial class Internal_OTP : IInternal_OTP
    {
        private readonly HttpClient _client;

        #region Constructors
        public Internal_OTP(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region SendOTP
        public async Task<Requests.Internal.Internal_OTP.SendOTP.SRTResponse> SendOTP(long mobile)
        {
            try
            {
                var exe = new Requests.Internal.Internal_OTP.SendOTP.SRTResponse
                    (new Requests.Internal.Internal_OTP.SendOTP.SRTRequest(_client)
                    {
                        mobile = mobile
                    });
                await exe.SendRequest();

                return exe;
            }
            catch (Exception ex)
            { throw SetErrorFunction(new Exception(new StackTrace().GetFrame(1).GetMethod().Name, ex)); }
        }
        #endregion

        #region VerifyOTP
        public async Task<Requests.Internal.Internal_OTP.VerifyOTP.SRTResponse> VerifyOTP(long mobile, string otp, GeneralDLL.DTO.SSOServices.SystemEnums.OTPSenderType senderType)
        {
            try
            {
                var exe = new Requests.Internal.Internal_OTP.VerifyOTP.SRTResponse
                    (new Requests.Internal.Internal_OTP.VerifyOTP.SRTRequest(_client)
                    {
                        mobile = mobile,
                        otp = otp,
                        senderType = senderType
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