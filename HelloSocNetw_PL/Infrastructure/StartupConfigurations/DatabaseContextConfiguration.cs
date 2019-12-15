using HelloSocNetw_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public static class DatabaseContextConfiguration
    {
        public static void AddConfiguredDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectingString = configuration.GetConnectionString("HelloDatabase");
            services.AddDbContextPool<SocNetwContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectingString)
                    .EnableSensitiveDataLogging());
        }
    }
}
