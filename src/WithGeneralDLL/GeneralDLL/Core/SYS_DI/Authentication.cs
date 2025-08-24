// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using Microsoft.Extensions.DependencyInjection;

namespace GeneralDLL.Core.SYS_DI
{
    public static partial class Authentication
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, Domain.URLData _urlData)
        {
            services.AddHttpClient<GeneralDLL.Business.SSOServices.Authentication.Internal.IAuthAdmin,
                                   GeneralDLL.Business.SSOServices.Authentication.Internal.AuthAdmin>(c => c.BaseAddress = new Uri(_urlData.Authentication));

            services.AddHttpClient<GeneralDLL.Business.SSOServices.Authentication.Internal.IAuthAdminSSO,
                                   GeneralDLL.Business.SSOServices.Authentication.Internal.AuthAdminSSO>(c => c.BaseAddress = new Uri(_urlData.Authentication));

            services.AddHttpClient<GeneralDLL.Business.SSOServices.Authentication.Internal.IAuthClient,
                                   GeneralDLL.Business.SSOServices.Authentication.Internal.AuthClient>(c => c.BaseAddress = new Uri(_urlData.Authentication));

            services.AddHttpClient<GeneralDLL.Business.SSOServices.Authentication.Internal.IAuthClientSSO,
                                   GeneralDLL.Business.SSOServices.Authentication.Internal.AuthClientSSO>(c => c.BaseAddress = new Uri(_urlData.Authentication));

            return services;
        }
    }
}