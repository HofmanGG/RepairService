using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_DAL.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure
{
    public static class DependenciesConfiguration
    {
        public static void AddConfiguredDependencies(this IServiceCollection services)
        {
            services.ConfigureDALServices();
            services.ConfigureBLLServices();
            services.ConfigurePLServices();
        }
    }
}
