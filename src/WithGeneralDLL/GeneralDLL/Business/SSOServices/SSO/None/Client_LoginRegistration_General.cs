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

    public partial interface IClient_LoginRegistration_General
    {
        Task<Requests.None.Client_LoginRegistration_General.CheckAuthentication.SRTResponse> CheckAuthentication(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForAuthentication data);

        Task<Requests.None.Client_LoginRegistration_General.CheckMobile.SRTResponse> CheckMobile(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration data);

        Task<Requests.None.Client_LoginRegistration_General.SendOTPForRegisteredUser.SRTResponse> SendOTPForRegisteredUser(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration data);

        Task<Requests.None.Client_LoginRegistration_General.VerifyOTP.SRTResponse> VerifyOTP(GeneralDLL.DTO.SSOServices.SSO.Registration.ConfirmOTP data);

        Task<Requests.None.Client_LoginRegistration_General.CheckPassword.SRTResponse> CheckPassword(GeneralDLL.DTO.SSOServices.SSO.Registration.CheckPassword data);
    }

    public partial class Client_LoginRegistration_General : IClient_LoginRegistration_General
    {
        private readonly HttpClient _client;

        #region Constructors
        public Client_LoginRegistration_General(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region CheckAuthentication
        public async Task<Requests.None.Client_LoginRegistration_General.CheckAuthentication.SRTResponse> CheckAuthentication(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForAuthentication data)
        {
            try
            {
                var exe = new Requests.None.Client_LoginRegistration_General.CheckAuthentication.SRTResponse
                    (new Requests.None.Client_LoginRegistration_General.CheckAuthentication.SRTRequest(_client)
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

        #region CheckMobile
        public async Task<Requests.None.Client_LoginRegistration_General.CheckMobile.SRTResponse> CheckMobile(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration data)
        {
            try
            {
                var exe = new Requests.None.Client_LoginRegistration_General.CheckMobile.SRTResponse
                    (new Requests.None.Client_LoginRegistration_General.CheckMobile.SRTRequest(_client)
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

        #region SendOTPForRegisteredUser
        public async Task<Requests.None.Client_LoginRegistration_General.SendOTPForRegisteredUser.SRTResponse> SendOTPForRegisteredUser(GeneralDLL.DTO.SSOServices.SSO.Registration.DataForRegistration data)
        {
            try
            {
                var exe = new Requests.None.Client_LoginRegistration_General.SendOTPForRegisteredUser.SRTResponse
                    (new Requests.None.Client_LoginRegistration_General.SendOTPForRegisteredUser.SRTRequest(_client)
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
        public async Task<Requests.None.Client_LoginRegistration_General.VerifyOTP.SRTResponse> VerifyOTP(GeneralDLL.DTO.SSOServices.SSO.Registration.ConfirmOTP data)
        {
            try
            {
                var exe = new Requests.None.Client_LoginRegistration_General.VerifyOTP.SRTResponse
                    (new Requests.None.Client_LoginRegistration_General.VerifyOTP.SRTRequest(_client)
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

        #region CheckPassword
        public async Task<Requests.None.Client_LoginRegistration_General.CheckPassword.SRTResponse> CheckPassword(GeneralDLL.DTO.SSOServices.SSO.Registration.CheckPassword data)
        {
            try
            {
                var exe = new Requests.None.Client_LoginRegistration_General.CheckPassword.SRTResponse
                    (new Requests.None.Client_LoginRegistration_General.CheckPassword.SRTRequest(_client)
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