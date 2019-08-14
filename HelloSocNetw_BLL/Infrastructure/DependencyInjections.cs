using HelloSocNetw_DAL;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_DAL.UnitsOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_BLL.Infrastructure
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
            services.AddIdentity<UserInfo, IdentityRole>()
                .AddEntityFrameworkStores<SocNetwContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}