using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.ErrorController.Repository
{
    public interface IErrorLogger
    {
        Task<long> Save_Error_ToDB(Exception exp);
        Task<long> Save_Log(string text);
        string GetErrorString(Exception exp);
    }
}
