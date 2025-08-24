// Ignore Spelling: DTO username Shahkar App

using GeneralDLL.Core.Databases;
using GeneralDLL.SRTExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Users.ClientData
{
    public class ClientInfoAdmin : IDatabaseEntry, IDatabaseEntryAdmin
    {
        public int id { get; set; }
        public int id_UserInfo { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();

        public int id_AdminChange { get; set; }
        public string fullName_AdminChange { get; set; } = string.Empty;

        #region Client
        public string fatherName { get; set; } = string.Empty;
        public DateTime? birthday { get; set; } = null;
        public string birthPlace { get; set; } = string.Empty;

        public string job { get; set; } = string.Empty;
        public GeneralDLL.DTO.SSOServices.SystemEnums.PersonEducationType? education { get; set; } = null;
        #endregion

        public DateTime date_Create { get; set; }
        public DateTime date_Update { get; set; }
    }
}