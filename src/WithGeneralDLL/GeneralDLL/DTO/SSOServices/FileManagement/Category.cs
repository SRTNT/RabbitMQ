// Ignore Spelling: App DTO

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.FileManagement
{
    public class Category
    {
        public int id { get; set; }
        public int? id_Parent { get; set; } = null;
        public string name { get; set; }
        public string minimal { get; set; }
        public string mainPicture { get; set; }

        public virtual Category_Description Descriptions { get; set; } = new Category_Description();

        public virtual bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (string.IsNullOrEmpty(mainPicture)) return false;
            if (!Descriptions.IsPolicyOK()) return false;

            return true;
        }
    }
}
