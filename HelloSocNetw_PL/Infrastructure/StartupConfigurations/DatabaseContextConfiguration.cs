using HelloSocNetw_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class DatabaseContextConfiguration
    {
        public static IServiceCollection AddConfiguredDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectingString = configuration.GetConnectionString("HelloDatabase");

            services.AddEntityFrameworkSqlServer();

            services.AddDbContextPool<SocNetwContext>((serviceProvider, optionsBuilder) =>
                optionsBuilder
                    .UseSqlServer(connectingString)
                    .EnableSensitiveDataLogging()
                    .UseInternalServiceProvider(serviceProvider));

            return services;
        }
    }
}
