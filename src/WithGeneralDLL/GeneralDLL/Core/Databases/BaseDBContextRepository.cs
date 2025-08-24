using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeneralDLL.Core.Databases
{
    public class BaseDBContextRepository
    {
        private readonly DataBase _dbContext;

        public BaseDBContextRepository(DataBase dbContext)
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

        #region Add/Update
        protected async Task<T> AddUpdate<T>(DbSet<T> dbSet, T data, DTO.SSOServices.SSO.Users.UserInfoAdmin adminInfoChange = null)
            where T : class, IDatabaseEntry
        {
            var hasInterfaceAdmin = data.GetType()
                                        .GetInterfaces()
                                        .Any(i => i.Name == typeof(IDatabaseEntryAdmin).Name);

            if (hasInterfaceAdmin)
            {
                ((IDatabaseEntryAdmin)data).id_AdminChange = adminInfoChange?.id ?? 0;
                ((IDatabaseEntryAdmin)data).fullName_AdminChange = adminInfoChange?.GetFullName() ?? "SYSTEM";
            }

            data.date_Update = DateTime.Now;

            if (data.id == 0)
            {
                data.date_Create = DateTime.Now;
                dbSet.Add(data);
            }
            else
                dbSet.Update(data);

            await _dbContext.SaveChangesAsync();

            return data;
        }
        #endregion

        #region Add/Update List
        protected async Task AddUpdateList<T>(
            DbSet<T> dbSet,
            IEnumerable<T> lstData,
            DTO.SSOServices.SSO.Users.UserInfoAdmin adminInfoChange = null)
            where T : class, IDatabaseEntry
        {
            var hasInterfaceAdmin = lstData.First()
                                           .GetType()
                                           .GetInterfaces()
                                           .Any(i => i.Name == typeof(IDatabaseEntryAdmin).Name);

            if (hasInterfaceAdmin)
            {
                foreach (var item in lstData)
                {
                    ((IDatabaseEntryAdmin)item).id_AdminChange = adminInfoChange?.id ?? 0;
                    ((IDatabaseEntryAdmin)item).fullName_AdminChange = adminInfoChange?.GetFullName() ?? "SYSTEM";
                }
            }

            var lstAdd = lstData.Where(q => q.id == 0)
                                .Select(q =>
                                {
                                    q.date_Create = DateTime.Now;
                                    q.date_Update = DateTime.Now;

                                    return q;
                                }).ToList();
            var lstUpdate = lstData.Where(q => q.id != 0)
                                   .Select(q => q.date_Update = DateTime.Now)
                                   .ToList();

            if (lstAdd.Any())
                _dbContext.AddRange(lstAdd);
            if (lstUpdate.Any())
                _dbContext.UpdateRange(lstUpdate);

            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Add/Update List+Delete
        protected async Task AddUpdateDeleteList<T>(
            DbSet<T> dbSet,
            IEnumerable<T> lstData,
            IEnumerable<T> lstOld,
            DTO.SSOServices.SSO.Users.UserInfoAdmin adminInfoChange = null)
            where T : class, IDatabaseEntry
        {
            var hasInterfaceAdmin = lstData.First()
                                           .GetType()
                                           .GetInterfaces()
                                           .Any(i => i.Name == typeof(IDatabaseEntryAdmin).Name);

            if (hasInterfaceAdmin)
            {
                foreach (var item in lstData)
                {
                    ((IDatabaseEntryAdmin)item).id_AdminChange = adminInfoChange?.id ?? 0;
                    ((IDatabaseEntryAdmin)item).fullName_AdminChange = adminInfoChange?.GetFullName() ?? "SYSTEM";
                }
            }

            var lstAdd = lstData.Where(q => q.id == 0)
                                .Select(q =>
                                {
                                    q.date_Create = DateTime.Now;
                                    q.date_Update = DateTime.Now;

                                    return q;
                                }).ToList();
            var lstUpdate = lstData.Where(q => q.id != 0)
                                   .Select(q => { q.date_Update = DateTime.Now; return q; })
                                   .ToList();

            var lstDelete = lstOld.Where(q => !lstUpdate.Any(r => r.id == q.id))
                                  .ToList();

            if (lstAdd.Any())
                _dbContext.AddRange(lstAdd);
            if (lstUpdate.Any())
                _dbContext.UpdateRange(lstUpdate);
            if (lstDelete.Any())
                _dbContext.RemoveRange(lstUpdate);

            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
