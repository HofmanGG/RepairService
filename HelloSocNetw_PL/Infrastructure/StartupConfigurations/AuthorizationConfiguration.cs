using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class AuthorizationConfiguration
    {
        public static void AddConfiguredAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationCore();
        }
    }
}
