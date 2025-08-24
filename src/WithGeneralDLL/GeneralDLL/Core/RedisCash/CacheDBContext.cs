using GeneralDLL.Core.Databases;
using GeneralDLL.Core.RedisCash.Domains;
using GeneralDLL.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralDLL.Core.RedisCash
{
    public class CacheDBContext : DataBase
    {
        public DbSet<CashBackup> CashBackup { get; set; }

        #region Constructors
        public CacheDBContext(DbContextOptions<CacheDBContext> options,
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
                    dbName = "CacheDatabase",
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
            builder.Services.AddDbContext<CacheDBContext>(options =>
                options.UseSqlServer(ConnectionStrings.GeneralDLL_CacheSQL.GetConnectionString())
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
