// Ignore Spelling: App DTO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.FileManagement
{
    public class Category_Description
    {
        public string Description { get; set; }

        public virtual bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(Description)) return false;
            return true;
        }
    }
}
