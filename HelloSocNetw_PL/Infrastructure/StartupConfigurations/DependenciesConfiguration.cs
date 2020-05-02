using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection AddConfiguredDependencies(this IServiceCollection services)
        {
            services.ConfigureDALServices();
            services.ConfigureBLLServices();
            services.ConfigurePLServices();

            return services;
        }
    }
}
