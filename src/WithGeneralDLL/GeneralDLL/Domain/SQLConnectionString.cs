// Ignore Spelling: DTO SRT SQL

using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Domain
{
    public class AppConnectionString
    {
        public GeneralConnectionString DefaultConnection { get; set; }

        public GeneralConnectionString GeneralDLL_CacheRedis { get; set; }
        public GeneralConnectionString GeneralDLL_CacheSQL { get; set; }
        public GeneralConnectionString GeneralDLL_ErrorSQL { get; set; }
        public GeneralConnectionString GeneralDLL_RabbitMQ { get; set; }

        internal List<GeneralConnectionString> GetAllConnection()
        {
            return this.GetType()
                       .GetProperties()
                       .Where(q => q.PropertyType == typeof(GeneralConnectionString))
                       .Select(q => (GeneralConnectionString)q.GetValue(this))
                       .ToList();
        }
    }

    public class GeneralConnectionString
    {
        /// <summary>
        /// Just For help
        /// SQL / Redis / Postgress / RabbitMQ
        /// </summary>
        public string connectionType { get; set; } = "";

        public string host { get; set; } = "";
        public string port { get; set; } = "";

        public string user { get; set; } = "";
        public string pass { get; set; } = "";
        public string dbName { get; set; } = "";

        public string GetConnectionString_SQL()
        {
            if (string.IsNullOrEmpty(port))
                return $"Data Source={host};" +
                       $"User ID={user};" +
                       $"Initial Catalog={dbName};" +
                       $"Password={pass};" +
                       $"Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";

            return $"Data Source={host},{port};" +
                   $"User ID={user};" +
                   $"Initial Catalog={dbName};" +
                   $"Password={pass};" +
                   $"Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";
        }
        public string GetConnectionString_REDIS()
        { return $"{host}:{port}"; }
        public string GetConnectionString_RabbitMQ()
        { return $"amqp://{user}:{pass}@{host}:{port}/"; }
        public string GetConnectionString_Postgress()
        { return $"User ID={user};Password={pass};Host={host};Port={port};Database={dbName};Pooling=true;Include Error Detail=true"; }

        /// <summary>
        /// Dynamicly Return Connection String
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            switch (connectionType.ToLower())
            {
                case "sql":
                    return GetConnectionString_SQL();
                case "redis":
                    return GetConnectionString_REDIS();
                case "postgress":
                    return GetConnectionString_Postgress();
                case "rabbitmq":
                    return GetConnectionString_RabbitMQ();
                default:
                    return "";
            }
        }
    }
}
