// Ignore Spelling: DTO SRT SMS Metri Chand

using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralDLL.SRTExtensions;
using GeneralDLL.Core.SYS_DI;

namespace GeneralDLL.Domain
{
    public class URLData
    {
        private string _BaseURL = null;
        public string BaseURL
        {
            get => _BaseURL;
            set
            {
                _BaseURL = value;
                if (_BaseURL.SRT_StringIsNullOrEmpty())
                    return;

                if (_BaseURL.IndexOf("/api") < 0)
                {
                    SSO = GenerateURL(SSO, value, ":9001");
                    BaleBot = GenerateURL(BaleBot, value, ":9010");
                    Authentication = GenerateURL(Authentication, value, ":9000");
                    NotificationService = GenerateURL(NotificationService, value, ":9100");
                }
                else
                {
                    SSO = GenerateURL(SSO, value, "/SSO");
                    BaleBot = GenerateURL(BaleBot, value, "/BaleBot");
                    Authentication = GenerateURL(Authentication, value, "/authentication");
                    NotificationService = GenerateURL(NotificationService, value, "/notificationservice");
                }
            }
        }

        private string GenerateURL(string oldData, string value, string apiURL)
        {
            return oldData.SRT_StringIsNullOrEmpty() ? value + apiURL : oldData;
        }

        public string SSO { get; set; }
        public string BaleBot { get; set; }

        public string Authentication { get; set; }
        public string NotificationService { get; set; }
    }
}
