using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.ErrorController.Repository
{
    public class ErrorLogger : IErrorLogger
    {
        private readonly ErrorReportDBContext _dbContext;
        private readonly ILogger<ErrorReportDBContext> logger;

        #region Constructors
        public ErrorLogger(ErrorReportDBContext dbContext, ILogger<ErrorReportDBContext> _logger)
        {
            _dbContext = dbContext;
            logger = _logger;
        }
        #endregion

        public async Task<long> Save_Error_ToDB(Exception exp)
        {
            return await SaveError_ToDB(exp);
        }
        public async Task<long> Save_Log(string text)
        {
            return await SaveError_ToDB(new Exception("LOG", new Exception(text, new Exception(DateTime.Now.ToString("G")))));
        }

        public string GetErrorString(Exception exp) => GetError_String(exp);
        public static string GetError_String(Exception exp)
        {
            string val = "";
            int number = 1;

            while (exp.InnerException != null)
            {
                val += (number + " - " + exp.Message + Environment.NewLine);
                exp = exp.InnerException;
                number++;
            }
            val += Environment.NewLine;
            val += "Message: " + exp.Message + Environment.NewLine;
            val += "StackTrace: " + exp.StackTrace;
            val += "Program: " + AppDomain.CurrentDomain.FriendlyName;
            val += "Date: " + DateTime.Now;

            return val;
        }

        private async Task<long> SaveError_ToDB(Exception exp)
        {
            var ex_main = exp;
            try
            {
                #region Main Work
                var data = new ErrorController.Domain.ErrorReport();

                int count = 0;
                string val = exp.Message;
                while (exp.InnerException != null && count <= 6)
                {
                    val = exp.Message;
                    exp = exp.InnerException;
                    if (exp.InnerException != null)
                        set_data(data, val, count++);
                }
                set_data(data, val, count);

                if (exp.InnerException == null)
                {
                    data.Message = exp.Message;
                    data.StackTrace = exp.StackTrace;
                }
                else
                {
                    val = "";
                    while (exp.InnerException != null)
                    {
                        val += exp.Message + Environment.NewLine;
                        exp = exp.InnerException;
                    }
                    val += "Message: " + exp.Message + Environment.NewLine;
                    val += "StackTrace: " + exp.StackTrace;
                }
                data.Program = AppDomain.CurrentDomain.FriendlyName;
                data.date = DateTime.Now;

                _dbContext.ErrorReports.Add(data);
                await _dbContext.SaveChangesAsync();

                return data.id;
                #endregion
            }
            catch
            {
                var v = GetErrorString(ex_main);
                logger.LogCritical(v);

                return 0;
            }
        }
        private void set_data(ErrorController.Domain.ErrorReport data, string value, int count)
        {
            switch (count)
            {
                case 0:
                    data.layer1 = value;
                    break;
                case 1:
                    data.layer2 = value;
                    break;
                case 2:
                    data.layer3 = value;
                    break;
                case 3:
                    data.layer4 = value;
                    break;
                case 4:
                    data.layer5 = value;
                    break;
                case 5:
                    data.layer6 = value;
                    break;
                case 6:
                    data.layer7 = value;
                    break;
                case 7:
                    data.layer8 = value;
                    break;
                case 8:
                    data.layer9 = value;
                    break;
                case 9:
                    data.layer10 = value;
                    break;
                case 10:
                    data.layer11 = value;
                    break;
                case 11:
                    data.layer12 = value;
                    break;
                case 12:
                    data.layer13 = value;
                    break;
                case 13:
                    data.layer14 = value;
                    break;
                case 14:
                    data.layer15 = value;
                    break;
                case 15:
                    data.layer16 = value;
                    break;
                case 16:
                    data.layer17 = value;
                    break;
                case 17:
                    data.layer18 = value;
                    break;
                case 18:
                    data.layer19 = value;
                    break;
                case 19:
                    data.layer20 = value;
                    break;
                default:
                    throw new Exception("Not Find Layer");
            }
        }
    }
}
