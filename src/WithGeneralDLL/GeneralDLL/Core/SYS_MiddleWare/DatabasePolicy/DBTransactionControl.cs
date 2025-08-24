//using GeneralDLL.Core.Domain;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System.Transactions;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.EntityFrameworkCore;

//namespace GeneralDLL.Core.SYS_MiddleWare.DatabasePolicy
//{
//    public class DBTransactionControl : TypeFilterAttribute
//    {
//        public DBTransactionControl() : base(typeof(DBTransactionControlWorker))
//        {
//        }

//        private class DBTransactionControlWorker : IAsyncActionFilter
//        {

//            public DBTransactionControlWorker()
//            {
//            }

//            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//            {
//                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
//                {
//                    try
//                    {
//                        var dbContext = context.HttpContext.RequestServices.GetRequiredService<DbContext>();

//                        try
//                        {
//                            await dbContext.Database.BeginTransactionAsync();

//                            await next();

//                            await dbContext.Database.CommitTransactionAsync();

//                            scope.Complete();
//                        }
//                        catch (Exception ex)
//                        {
//                            await dbContext.Database.RollbackTransactionAsync();

//                            // در صورت خطا، تراکنش لغو می‌شود.
//                        }
//                    }
//                    catch (Exception ex)
//                    { }
//                }
//            }
//        }
//    }
//}
