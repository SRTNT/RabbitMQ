using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.Databases
{
    public class BaseDBContextUnitOfWork
    {
        private readonly DataBase _dbContext;

        public BaseDBContextUnitOfWork(DataBase dbContext)
        { _dbContext = dbContext; }

        #region Get Last ID
        protected async Task<int> GetLastID()
        {
            // دستور SQL برای دریافت مقدار id جدید
            FormattableString sql = $"SELECT CAST(SCOPE_IDENTITY() AS int)";
            // اجرای دستور SQL و دریافت مقدار id جدید
            return await _dbContext.Database.SqlQuery<int>(sql).FirstOrDefaultAsync();
        }
        #endregion

        #region Transaction
        public async Task BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();
        public async Task CommitTransactionAsync() => await _dbContext.Database.CommitTransactionAsync();
        public async Task RollbackTransactionAsync() => await _dbContext.Database.RollbackTransactionAsync();

        public async Task<T> SRT_DoTaskWithTransactionAsync<T>(Func<Task<T>> func)
        {
            try
            {
                await BeginTransactionAsync();
                var r = await func();
                await CommitTransactionAsync();

                return r;
            }
            catch (Exception ex)
            {
                await RollbackTransactionAsync();
                throw new Exception("DoTaskWithTransactionAsync", ex);
            }
        }
        #endregion
    }
}
