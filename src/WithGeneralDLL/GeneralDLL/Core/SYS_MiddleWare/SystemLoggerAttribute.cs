using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.SYS_MiddleWare
{
    internal class SystemLoggerAttribute
    {
        private readonly RequestDelegate _next;

        #region Constructors
        public SystemLoggerAttribute(RequestDelegate next)
        {
            _next = next;
        } 
        #endregion

        public async Task InvokeAsync(HttpContext context)
        {
            var log = GetRequestData(context);

            await _next(context);

            log += $@"
░█▀▀█ ░█▀▀▀ ░█▀▀▀█ ░█▀▀█ ░█▀▀▀█ ░█▄─░█ ░█▀▀▀█ ░█▀▀▀ 
░█▄▄▀ ░█▀▀▀ ─▀▀▀▄▄ ░█▄▄█ ░█──░█ ░█░█░█ ─▀▀▀▄▄ ░█▀▀▀ 
░█─░█ ░█▄▄▄ ░█▄▄▄█ ░█─── ░█▄▄▄█ ░█──▀█ ░█▄▄▄█ ░█▄▄▄
- Response StatusCode: {context.Response.StatusCode}
- date: {DateTime.Now.ToString("G")}";

            Log.Information(log);
        }

        private string GetRequestData(HttpContext context)
        {
            #region Header
            var vHeader = "";
            foreach (var item in context.Request.Headers)
            {
                vHeader += $@"  - {item.Key} : {item.Value}
";
            }
            #endregion

            #region Query
            var vQuery = "";
            foreach (var item in context.Request.Query)
            {
                vQuery += $@"  - {item.Key} : {item.Value}
";
            }
            #endregion

            #region Cookies
            var vCookies = "";
            foreach (var item in context.Request.Cookies)
            {
                vCookies += $@"  - {item.Key} : {item.Value}
";
            }
            #endregion

            #region RouteValues
            var vRouteValues = "";
            foreach (var item in context.Request.RouteValues)
            {
                vRouteValues += $@"  - {item.Key} : {item.Value}
";
            }
            #endregion

            #region Generate Message
            var v = $@"Request Receive ------------------------------------ {DateTime.Now.ToString("G")}

▒█▄░▒█ ▒█▀▀▀ ▒█░░▒█ 　 ▒█▀▀█ ▒█▀▀▀ ▒█▀▀█ ▒█░▒█ ▒█▀▀▀ ▒█▀▀▀█ ▀▀█▀▀ 
▒█▒█▒█ ▒█▀▀▀ ▒█▒█▒█ 　 ▒█▄▄▀ ▒█▀▀▀ ▒█░▒█ ▒█░▒█ ▒█▀▀▀ ░▀▀▀▄▄ ░▒█░░ 
▒█░░▀█ ▒█▄▄▄ ▒█▄▀▄█ 　 ▒█░▒█ ▒█▄▄▄ ░▀▀█▄ ░▀▄▄▀ ▒█▄▄▄ ▒█▄▄▄█ ░▒█░░
- Protocol: {context.Request.Protocol}
- Method: {context.Request.Method}
- Host: {context.Request.Host}
- Path: {context.Request.Path}
- QueryString: {context.Request.QueryString}
- Query: 
{vQuery}
- Headers: 
{vHeader}
- Cookies: 
{vCookies}
- RouteValues: 
{vRouteValues}
- date: {DateTime.Now.ToString("G")}

";
            #endregion

            return v;
        }
    }
}