using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class LoggingConfiguration
    {
        public static IServiceCollection AddConfiguredLogging(this IServiceCollection services)
        {
            services.AddLogging();

            return services;
        }
    }
}
