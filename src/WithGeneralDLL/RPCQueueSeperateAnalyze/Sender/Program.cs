using Sender.DataAccess;
using GeneralDLL.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

#region App Setting
var lstDatabaseNamespace = new List<string>() { };
var dmAppSettingBase = new GeneralDLL.Domain.AppSetting(lstDatabaseNamespace);
dmAppSettingBase.AddResponseResult(statusCode: StatusCodes.Status404NotFound, type: typeof(void));
#endregion

var (builder, dmAppSetting) = BaseConfigurations.SRT_ConfigController(args, dmAppSettingBase);

Console.Title = "Send - " + dmAppSetting.CoreSystemENV.environment;

#region Other Builder Config - Local
builder.AddRepositoryInjection();
builder.AddUnitOfWorkInjection();
#endregion

#region Swagger Config
var dmSwaggerConfig = new GeneralDLL.Core.Swaggers.SwaggerDomain();

dmSwaggerConfig.lstVersions.Add(new OpenApiInfo
{
    Title = "Send V1",
    Version = "v1",
    Contact = new OpenApiContact()
    {
        Email = "s.r.taheri@gmail.com",
        Name = "Contact",
        Url = new Uri("http://google.com")
    },
    License = new OpenApiLicense() { Name = "License", Url = new Uri("http://google.com") },
    TermsOfService = new Uri("http://google.com")
});

#region Api Header Scheme
dmSwaggerConfig.HeaderStructure = new AddCustomHeadersOperationFilter();
#endregion

dmSwaggerConfig.swaggerTitle = "Send";
dmSwaggerConfig.RoutePrefix = dmAppSetting.CoreSystemENV.environment != GeneralDLL.Core.ENV.SystemENV.EnvironmentType.Production;
dmSwaggerConfig.SwaggerInProduction = true;
dmSwaggerConfig.DocExpansion = DocExpansion.None;

// lstSwaggerUIAction
// lstSwaggerAction

dmAppSetting.SwaggerConfig = dmSwaggerConfig;
#endregion

#region Redis - Error Log
dmAppSetting.UseErrorLogSQL = true; // Default is true
dmAppSetting.UseCachStructure = GeneralDLL.Domain.AppSetting.CashType.CashWithSQL;
#endregion

#region Environment
dmAppSetting.lstBuilderEnvironments.Add((_builder, env) => { Console.WriteLine($"Builder Environment: {env}"); });
dmAppSetting.lstAppEnvironments.Add((_app, env) => { Console.WriteLine($"App Environment: {env}"); });
#endregion

#region Other Controllers Config
// lstControllersOptions
// ReturnHttpNotAcceptable
#endregion

#region Inject Other Services
dmAppSetting.lstInjectService.Add(GeneralDLL.Core.SYS_DI.NotificationService.AddNotificationService);
dmAppSetting.lstInjectService.Add(GeneralDLL.Core.SYS_DI.Authentication.AddAuthentication);
#endregion

#region Databases
//dmAppSetting.lstDatabaseNamespace.Add(typeof(BotTournamentsCore.DataAccess.BotTournamentsCoreContext).Namespace);
#endregion

#region Rabbit MQ
// lstRabbitMQNamespace
dmAppSetting.lstRabbitMQNamespace.Add(typeof(Sender.RabbitMQStructure.PublisherMessage).Namespace);
#endregion

#region Serilog - always true
// UseSerilog
#endregion

var app = await builder.SRT_ConfigBuilderAndBuildApp(dmAppSetting);

app.UseAuthorization();
app.MapControllers();
app.Run();