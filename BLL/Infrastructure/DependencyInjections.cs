using DAL.Interfaces;
using Entities;
using DAL.EFRepositories;
using DAL.UnitsOfWork;
using DAL.Сontexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure
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