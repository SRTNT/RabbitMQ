// Ignore Spelling: DTO SRT SSO

using GeneralDLL.Core.ENV;
using GeneralDLL.SRTExtensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.GeneralFunctions
{
    public class SSO
    {
        public static string GetHeaderData(HttpContext httpContext, string nameHeader)
        {
            httpContext.Request.Headers.TryGetValue(nameHeader, out var v);
            return v.Any() ? v.ToString() : null;
        }

        public static void SetHttpOnlyToken(HttpContext context, string token)
        {
            // Resolve ISystemENV from DI
            var _systemEnv = context.RequestServices.GetRequiredService<ISystemENV>();

            var setFunc = (bool secure, SameSiteMode sameSiteMode) =>
            {
                context.Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = secure,
                    SameSite = sameSiteMode,
                    Path = "/",
                    Expires = DateTimeOffset.Now.AddDays(token.SRT_StringIsNullOrEmpty() ? -2 : 5),
                    Domain = context.Request.Headers.Origin.ToString().Replace("http://front.", "").Replace(":3000", "")
                });
            };

#if DEBUG
            setFunc(false, SameSiteMode.Lax);
#else
            //if (_systemEnv.environment != Core.ENV.SystemENV.EnvironmentType.Production)
            //    setFunc(true, SameSiteMode.None);
            //else
                setFunc(false, SameSiteMode.Lax);
#endif
        }
    }
}
