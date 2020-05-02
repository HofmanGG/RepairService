using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddConfiguredAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationCore();

            return services;
        }
    }
}
