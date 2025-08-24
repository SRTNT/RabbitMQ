using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Company
{
    public class CompanyInfo
    {
        public string guid { get; set; }

        public string name { get; set; }
        public string description { get; set; }

        public int id_AdminChange { get; set; }
        public string fullName_AdminChange { get; set; }

        public GeneralDLL.DTO.SSOServices.SystemEnums.SSO_ActiveType activeStatus { get; set; } = GeneralDLL.DTO.SSOServices.SystemEnums.SSO_ActiveType.active;
    }

    public class CompanyInfoAdmin
    {
        public int id { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();

        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        public int id_AdminChange { get; set; }
        public string fullName_AdminChange { get; set; } = string.Empty;

        public GeneralDLL.DTO.SSOServices.SystemEnums.SSO_ActiveType activeStatus { get; set; } = GeneralDLL.DTO.SSOServices.SystemEnums.SSO_ActiveType.active;

        public DateTime date_Create { get; set; }
        public DateTime date_Update { get; set; }
    }

    public class CompanySearch : Domain.SearchInput
    {
        public string name { get; set; } = null;
        public string fullName_AdminChange { get; set; } = null;
        public GeneralDLL.DTO.SSOServices.SystemEnums.SSO_ActiveType? activeStatus { get; set; } = null;
        public DateTime? date_Start { get; set; } = null;
        public DateTime? date_End { get; set; } = null;
    }
    public class CompanySearchResult : Domain.SearchResult<CompanyInfoAdmin>
    { }
}
