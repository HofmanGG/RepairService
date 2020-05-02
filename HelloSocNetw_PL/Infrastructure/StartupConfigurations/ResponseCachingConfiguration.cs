using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class ResponseCachingConfiguration
    {
        public static IServiceCollection AddConfiguredResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();

            return services;
        }
    }
}
