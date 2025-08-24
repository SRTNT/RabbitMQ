using GeneralDLL.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.Databases
{
    public interface IDataBase
    {
        void BaseConfiguration(WebApplicationBuilder builder, AppConnectionString ConnectionStrings);

        Task InsertBaseData();
    }

    public abstract class DataBase : DbContext, IDataBase
    {
        public AppConnectionString ConnectionStrings { get; }

        public DataBase(DbContextOptions options, AppConnectionString connectionStrings) : base(options)
        {
            ConnectionStrings = connectionStrings;
        }

        public abstract void BaseConfiguration(WebApplicationBuilder builder, AppConnectionString ConnectionStrings);

        public abstract Task InsertBaseData();
    }
}
