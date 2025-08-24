//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GeneralDLL.Core.Swaggers
//{
//    public class SwaggerBasePathMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public SwaggerBasePathMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            // Set the base path dynamically
//            var basePath = $"{context.Request.Scheme}://{context.Request.Host}";

//            // Store basePath in HttpContext for later use
//            context.Items["SwaggerBasePath"] = basePath;

//            await _next(context);
//        }
//    }
//}
