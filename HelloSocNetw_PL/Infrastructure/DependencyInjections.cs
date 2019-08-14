using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_BLL.Services;
using HelloSocNetw_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure
{
    public class DependencyInjections
    {
        public DependencyInjections(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Service dependency injection configuration
            services.AddScoped<IUserService, IdentityUserService>();

            var connectingString = Configuration.GetConnectionString("HelloDatabase");
            services.AddDbContextPool<SocNetwContext>(options =>
                options.UseSqlServer(connectingString));
        }
    }
}