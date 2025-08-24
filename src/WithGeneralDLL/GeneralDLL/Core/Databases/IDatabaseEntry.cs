using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.Databases
{
    public interface IDatabaseEntry
    {
        int id { get; set; }

        DateTime date_Create { get; set; }
        DateTime date_Update { get; set; }
    }
}
