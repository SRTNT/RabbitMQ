// Ignore Spelling: jwt dm app dto srt Meka otp shaba metri chand 

using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GeneralDLL.DTO.SSOServices.SystemEnums;

namespace GeneralDLL.Business.SSOServices.SSO.Internal
{

    public partial interface IGetDataInternal
    {
        Task<Requests.Internal.GetDataInternal.GetByMobile.SRTResponse> GetByMobile(long mobile);
    }

    public partial class GetDataInternal : IGetDataInternal
    {
        private readonly HttpClient _client;

        #region Constructors
        public GetDataInternal(HttpClient client)
        {
            this._client = client;
        } 
        #endregion

        #region Set Error Function
        private Exception SetErrorFunction(Exception ex)
        { return new Exception(GetType().Namespace, new Exception(GetType().Name, ex)); }
        #endregion

        #region GetByMobile
        public async Task<Requests.Internal.GetDataInternal.GetByMobile.SRTResponse> GetByMobile(long mobile)
        {
            try
            {
                var exe = new Requests.Internal.GetDataInternal.GetByMobile.SRTResponse
                    (new Requests.Internal.GetDataInternal.GetByMobile.SRTRequest(_client)
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
    }
}