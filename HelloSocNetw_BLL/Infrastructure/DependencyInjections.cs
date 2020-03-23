using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_BLL.Services;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_DAL.UnitsOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_BLL.Infrastructure
{
    public static class DependencyInjections
    {
        public static void ConfigureBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJWTService, JWTService>();
        }
    }
}