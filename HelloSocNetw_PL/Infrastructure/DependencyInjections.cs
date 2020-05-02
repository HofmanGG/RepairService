using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_BLL.Services;
using HelloSocNetw_PL.Infrastructure.Services;
using HelloSocNetw_PL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection ConfigurePLServices(this IServiceCollection services)
        {
            // Service dependency injection configuration
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRepairRequestsService, RepairRequestsService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}