using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GeneralDLL.Core.Swaggers
{
    public class SwaggerDomain
    {
        public List<OpenApiInfo> lstVersions { get; set; } = new List<OpenApiInfo>();

        public List<OpenApiSecurity> lstOpenApiSecurityScheme { get; set; } = new List<OpenApiSecurity>();

        public GeneralDLL.Core.Swaggers.SwaggerInterface HeaderStructure { get; set; }

        public bool RoutePrefix { get; set; } = true;
        public bool SwaggerInProduction { get; set; } = true;

        public string swaggerTitle { get; set; } = "Swagger Title";

        public DocExpansion DocExpansion { get; set; } = DocExpansion.None;

        public List<Action<SwaggerUIOptions>> lstSwaggerUIAction { get; set; } = new List<Action<SwaggerUIOptions>>();
        public List<Action<SwaggerOptions>> lstSwaggerAction { get; set; } = new List<Action<SwaggerOptions>>();
    }

    public class OpenApiSecurity
    {
        public OpenApiSecurityScheme openApiSecurityScheme { get; set; }
        public OpenApiSecurityRequirement openApiSecurityRequirement { get; set; }
    }
}
