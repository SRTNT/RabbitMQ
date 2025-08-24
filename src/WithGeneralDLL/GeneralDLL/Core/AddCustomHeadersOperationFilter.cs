using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core
{
    public class AddCustomHeadersOperationFilter : IOperationFilter, Swaggers.SwaggerInterface
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Add your custom headers here
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = $@"Browser Identity.",
                Name = "X-Device-Identity",
                In = ParameterLocation.Header,
                Required = true, // Set to true if the header is required
                Schema = new OpenApiSchema { Type = "string" }
            });

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Description = $@"Browser ID.",
            //    Name = "computerID",
            //    In = ParameterLocation.Header,
            //    Required = true,
            //    Schema = new OpenApiSchema { Type = "string" }
            //});

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Description = $@"Computer ID.",
            //    Name = "browserID",
            //    In = ParameterLocation.Header,
            //    Required = true,
            //    Schema = new OpenApiSchema { Type = "string" }
            //});
        }

        public void ApplySetting(SwaggerGenOptions options)
        {
            options.OperationFilter<AddCustomHeadersOperationFilter>();
        }
    }
}
