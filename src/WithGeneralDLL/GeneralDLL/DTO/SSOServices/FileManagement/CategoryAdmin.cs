// Ignore Spelling: App DTO Admin

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.FileManagement
{
    public class CategoryAdmin : Category
    {
        public DateTime date { get; set; } = DateTime.Now;
        public int id_user { get; set; }

        public override bool IsPolicyOK()
        {
            return base.IsPolicyOK(); ;
        }
    }
}
