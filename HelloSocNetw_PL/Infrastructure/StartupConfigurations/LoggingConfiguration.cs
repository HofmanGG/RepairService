using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class LoggingConfiguration
    {
        public static void AddConfiguredLogging(this IServiceCollection services)
        {
            services.AddLogging();
        }
    }
}
