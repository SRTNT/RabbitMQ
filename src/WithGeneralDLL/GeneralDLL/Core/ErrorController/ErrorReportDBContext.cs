using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralDLL.Domain;
using GeneralDLL.Core.ErrorController.Domain;
using GeneralDLL.Core.Databases;
using GeneralDLL.Core.RedisCash;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralDLL.Core.ErrorController
{
    public class ErrorReportDBContext : DataBase
    {
        public DbSet<ErrorReport> ErrorReports { get; set; }

        #region Constructors
        public ErrorReportDBContext(DbContextOptions<ErrorReportDBContext> options,
                                    AppConnectionString ConnectionStrings)
            : base(options, ConnectionStrings)
        {
        }
        #endregion

        #region On Configuration => اگر کانفیگ نشده بود جهت تست - کانفیگ دیفالت برایش ست کند
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Console.WriteLine("Base Configuration -------------------- Attention");

                var connection = new GeneralConnectionString()
                {
                    connectionType = "SQL",
                    dbName = "ErrorDatabase",
                    host = "localhost",
                    port = "11433",
                    user = "sa",
                    pass = "SaeedTNT220"
                };

                optionsBuilder.UseSqlServer(connection.GetConnectionString())
                              .LogTo
                              (
                               Console.WriteLine,
                               new[] { DbLoggerCategory.Database.Command.Name },
                               Microsoft.Extensions.Logging.LogLevel.Information
                              )
                              .EnableSensitiveDataLogging();
            }
        }
        #endregion

        #region Base Function
        public override void BaseConfiguration(WebApplicationBuilder builder, AppConnectionString ConnectionStrings)
        {
            builder.Services.AddDbContext<ErrorReportDBContext>(options =>
                options.UseSqlServer(ConnectionStrings.GeneralDLL_ErrorSQL.GetConnectionString())
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        public override async Task InsertBaseData()
        {
            await Task.CompletedTask;
        }
        #endregion

        #region On Model Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        #endregion
    }
}
