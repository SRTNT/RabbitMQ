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

    public partial interface IClientSSO_Info
    {
        Task<Requests.None.ClientSSO_Info.GetInfo.SRTResponse> GetInfo();

        Task<Requests.None.ClientSSO_Info.Logout.SRTResponse> Logout();
    }

    public partial class ClientSSO_Info : IClientSSO_Info
    {
        private readonly HttpClient _client;

        #region Constructors
        public ClientSSO_Info(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region GetInfo
        public async Task<Requests.None.ClientSSO_Info.GetInfo.SRTResponse> GetInfo()
        {
            try
            {
                var exe = new Requests.None.ClientSSO_Info.GetInfo.SRTResponse
                    (new Requests.None.ClientSSO_Info.GetInfo.SRTRequest(_client)
                    {

                    });
                await exe.SendRequest();

                return exe;
            }
            catch (Exception ex)
            { throw SetErrorFunction(new Exception(new StackTrace().GetFrame(1).GetMethod().Name, ex)); }
        }
        #endregion

        #region Logout
        public async Task<Requests.None.ClientSSO_Info.Logout.SRTResponse> Logout()
        {
            try
            {
                var exe = new Requests.None.ClientSSO_Info.Logout.SRTResponse
                    (new Requests.None.ClientSSO_Info.Logout.SRTRequest(_client)
                    {

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