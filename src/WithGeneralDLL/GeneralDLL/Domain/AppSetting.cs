using GeneralDLL.Core.ENV;
using GeneralDLL.Core.RabbitMQ.Domain;
using GeneralDLL.Core.Swaggers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralDLL.Domain
{
    public class AppSetting
    {
        public enum CashType
        {
            None,
            CashNoSQL,
            CashWithSQL
        }

        #region Constructors
        public AppSetting(List<string> lstDatabaseNamespace = null)
        {
            AddResponseResult(StatusCodes.Status500InternalServerError, typeof(void));
            this.lstDatabaseNamespace = lstDatabaseNamespace ?? new List<string>();
        }

        public AppSetting(IWebHostEnvironment _env, AppSetting _base)
        {
            CoreSystemENV = new CoreSystemENV(_env);

            lstResponseResult = _base.lstResponseResult;
            ReturnHttpNotAcceptable = _base.ReturnHttpNotAcceptable;
            UseCachStructure = _base.UseCachStructure;
            SwaggerConfig = _base.SwaggerConfig;
            ConnectionStrings = _base.ConnectionStrings;
            lstBuilderEnvironments = _base.lstBuilderEnvironments;
            lstAppEnvironments = _base.lstAppEnvironments;
            lstControllersOptions = _base.lstControllersOptions;
            lstDatabaseNamespace = _base.lstDatabaseNamespace;
            lstRabbitMQNamespace = _base.lstRabbitMQNamespace;
        }
        #endregion

        public void AddResponseResult(int statusCode, Type type)
        {
            lstResponseResult.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(statusCode: statusCode, type: type));
        }

        public string APPName => SwaggerConfig.swaggerTitle;

        public List<Action<Microsoft.AspNetCore.Mvc.MvcOptions>> lstControllersOptions { get; set; } = new List<Action<Microsoft.AspNetCore.Mvc.MvcOptions>>();
        public List<Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute> lstResponseResult { get; set; } = new List<Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute>();
        public bool ReturnHttpNotAcceptable { get; set; } = false;
        public bool UseErrorLogSQL { get; set; } = true;
        public bool UseSerilog { get; set; } = true;
        public CashType UseCachStructure { get; set; } = CashType.CashWithSQL;

        public string BaseURLtagName { get; set; } = "urlData";

        public SwaggerDomain SwaggerConfig { get; set; }
        public AppConnectionString ConnectionStrings { get; set; }
        public CoreSystemENV CoreSystemENV { get; set; }

        public List<Action<WebApplicationBuilder, SystemENV.EnvironmentType>> lstBuilderEnvironments { get; set; } = new List<Action<WebApplicationBuilder, SystemENV.EnvironmentType>>();
        public List<Action<WebApplication, SystemENV.EnvironmentType>> lstAppEnvironments { get; set; } = new List<Action<WebApplication, SystemENV.EnvironmentType>>();

        public List<Func<IServiceCollection, URLData, IServiceCollection>> lstInjectService { get; set; } = new List<Func<IServiceCollection, URLData, IServiceCollection>>();

        public List<string> lstDatabaseNamespace { get; set; } = new List<string>();
        public List<string> lstRabbitMQNamespace { get; set; } = new List<string>();
    }
}
