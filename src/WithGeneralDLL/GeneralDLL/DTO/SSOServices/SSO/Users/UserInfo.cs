// Ignore Spelling: username

using GeneralDLL.SRTExtensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDLL.DTO.SSOServices.SSO.Users
{
    public class UserInfoAdmin
    {
        public int id { get; set; }

        public Guid guid { get; set; } = Guid.NewGuid();

        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;

        public SystemEnums.GenderType gender { get; set; } = SystemEnums.GenderType.none;

        public string nationalCode { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        public FileManagement.FileInfoClient avatarURL { get; set; }

        #region User Login Info
        public long mobile { get; set; }
        public string username { get; set; } = string.Empty;
        public string pass { get; set; } = string.Empty;
        public SystemEnums.SSO_ActiveType accountState { get; set; } = SystemEnums.SSO_ActiveType.active;
        #endregion

        public AdminData.AdminInfoAdmin AdminData { get; set; }
        public ClientData.ClientInfoAdmin ClientData { get; set; }

        public int id_AdminChange { get; set; }
        public string fullName_AdminChange { get; set; } = string.Empty;

        public DateTime date_Create { get; set; }
        public DateTime date_Update { get; set; }

        public string GetFullName()
        {
            return $"{lastName} - {firstName}".Trim();
        }
    }
}
