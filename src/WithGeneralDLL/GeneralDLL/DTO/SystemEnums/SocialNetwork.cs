using System.ComponentModel;

namespace GeneralDLL.DTO.SystemEnums
{
    public enum SocialNetwork
    {
        /// <summary>
        /// تلگرام
        /// </summary>
        [Description("تلگرام")]
        Telegram = 0,
        /// <summary>
        /// بله
        /// </summary>
        [Description("بله")]
        Bale = 1,
        /// <summary>
        /// گپ
        /// </summary>
        [Description("گپ")]
        Gap = 2,
        /// <summary>
        /// آی گپ
        /// </summary>
        [Description("آی گپ")]
        iGap = 3,
        /// <summary>
        /// سروش پلاس
        /// </summary>
        [Description("سروش پلاس")]
        Soroush = 4,
        /// <summary>
        /// اینستاگرام
        /// </summary>
        [Description("اینستاگرام")]
        Instagram = 5,
        /// <summary>
        /// LandingPage
        /// </summary>
        [Description("LandingPage")]
        LandingPage = 99,
        /// <summary>
        /// API
        /// </summary>
        [Description("API")]
        API = 100,
        /// <summary>
        /// SNCore
        /// </summary>
        [Description("SNCore")]
        SNCore = 101,
        /// <summary>
        /// NONE
        /// </summary>
        [Description("NONE")]
        NONE = 200,
    }
}
