// Ignore Spelling: jwt dm app dto srt Meka otp shaba metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.SSO.None
{

    public partial interface IClient_PasswordChanged_General
    {
        Task<Requests.None.Client_PasswordChanged_General.CheckMobile.SRTResponse> CheckMobile(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration data);

        Task<Requests.None.Client_PasswordChanged_General.VerifyOTP.SRTResponse> VerifyOTP(GeneralDLL.DTO.SSOServices.SSO.Registration.ConfirmChangePassword data);

        Task<Requests.None.Client_PasswordChanged_General.SavePassword.SRTResponse> SavePassword(GeneralDLL.DTO.SSOServices.GetStringFromBody data);
    }

    public partial class Client_PasswordChanged_General : IClient_PasswordChanged_General
    {
        private readonly HttpClient _client;

        #region Constructors
        public Client_PasswordChanged_General(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region CheckMobile
        public async Task<Requests.None.Client_PasswordChanged_General.CheckMobile.SRTResponse> CheckMobile(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration data)
        {
            try
            {
                var exe = new Requests.None.Client_PasswordChanged_General.CheckMobile.SRTResponse
                    (new Requests.None.Client_PasswordChanged_General.CheckMobile.SRTRequest(_client)
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

        #region VerifyOTP
        public async Task<Requests.None.Client_PasswordChanged_General.VerifyOTP.SRTResponse> VerifyOTP(GeneralDLL.DTO.SSOServices.SSO.Registration.ConfirmChangePassword data)
        {
            try
            {
                var exe = new Requests.None.Client_PasswordChanged_General.VerifyOTP.SRTResponse
                    (new Requests.None.Client_PasswordChanged_General.VerifyOTP.SRTRequest(_client)
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

        #region SavePassword
        public async Task<Requests.None.Client_PasswordChanged_General.SavePassword.SRTResponse> SavePassword(GeneralDLL.DTO.SSOServices.GetStringFromBody data)
        {
            try
            {
                var exe = new Requests.None.Client_PasswordChanged_General.SavePassword.SRTResponse
                    (new Requests.None.Client_PasswordChanged_General.SavePassword.SRTRequest(_client)
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