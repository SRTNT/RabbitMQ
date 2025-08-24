// Ignore Spelling: username DTO App

using GeneralDLL.Core.Databases;
using GeneralDLL.DTO.SSOServices.FileManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Users.AdminData
{
    public class AdminInfoAdmin : IDatabaseEntry, IDatabaseEntryAdmin
    {
        public int id { get; set; }
        public int id_UserInfo { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();

        public int id_AdminChange { get; set; }
        public string fullName_AdminChange { get; set; } = string.Empty;

        public DateTime date_Create { get; set; }
        public DateTime date_Update { get; set; }
    }
}
