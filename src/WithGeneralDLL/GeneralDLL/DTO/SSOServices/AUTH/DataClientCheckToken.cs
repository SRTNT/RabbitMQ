// Ignore Spelling: Admin username dto app


using GeneralDLL.SRTExtensions;

namespace GeneralDLL.DTO.SSOServices.AUTH
{
    public class DataClientCheckToken
    {
        public string token { get; set; }

        public string browser { get; set; }
        public string browserID { get; set; }

        internal bool isDataComplete()
        {
            return !token.SRT_StringIsNullOrEmpty() &&
                   !browser.SRT_StringIsNullOrEmpty() &&
                   !browserID.SRT_StringIsNullOrEmpty();
        }
    }
}