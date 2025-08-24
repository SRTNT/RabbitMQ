// Ignore Spelling: jwt dm app dto srt Meka otp shaba  metri chand 

using Microsoft.Extensions.DependencyInjection;

namespace GeneralDLL.Core.SYS_DI
{
    public static partial class NotificationService
    {
        public static IServiceCollection AddNotificationService(this IServiceCollection services, Domain.URLData _urlData)
        {
            services.AddHttpClient<GeneralDLL.Business.SSOServices.NotificationService.Internal.IInternal_OTP,
                                   GeneralDLL.Business.SSOServices.NotificationService.Internal.Internal_OTP>(c => c.BaseAddress = new Uri(_urlData.NotificationService));

            return services;
        }
    }
}