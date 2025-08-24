// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using Microsoft.Extensions.DependencyInjection;

namespace GeneralDLL.Core.SYS_DI
{
    public static partial class SSO
    {
        public static IServiceCollection AddSSO(this IServiceCollection services, Domain.URLData _urlData)
        {
            services.AddHttpClient<GeneralDLL.Business.SSOServices.SSO.None.IClientSSO_Info,
                                   GeneralDLL.Business.SSOServices.SSO.None.ClientSSO_Info>(c => c.BaseAddress = new Uri(_urlData.SSO));

            services.AddHttpClient<GeneralDLL.Business.SSOServices.SSO.None.IClient_LoginRegistration_General,
                                   GeneralDLL.Business.SSOServices.SSO.None.Client_LoginRegistration_General>(c => c.BaseAddress = new Uri(_urlData.SSO));

            services.AddHttpClient<GeneralDLL.Business.SSOServices.SSO.None.IClient_PasswordChanged_General,
                                   GeneralDLL.Business.SSOServices.SSO.None.Client_PasswordChanged_General>(c => c.BaseAddress = new Uri(_urlData.SSO));

            services.AddHttpClient<GeneralDLL.Business.SSOServices.SSO.Internal.IGetDataInternal,
                                   GeneralDLL.Business.SSOServices.SSO.Internal.GetDataInternal>(c => c.BaseAddress = new Uri(_urlData.SSO));

            services.AddHttpClient<GeneralDLL.Business.SSOServices.SSO.Internal.IRegistrationInternal,
                                   GeneralDLL.Business.SSOServices.SSO.Internal.RegistrationInternal>(c => c.BaseAddress = new Uri(_urlData.SSO));

            return services;
        }
    }
}