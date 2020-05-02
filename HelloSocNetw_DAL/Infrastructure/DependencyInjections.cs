using HelloSocNetw_DAL.Entities.IdentityEntities;
using HelloSocNetw_DAL.Identity;
using HelloSocNetw_DAL.Infrastructure.Services;
using HelloSocNetw_DAL.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_DAL.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection ConfigureDALServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppIdentityUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<SocNetwContext>()
                .AddUserStore<AppUserStore>()
                .AddRoleStore<AppRoleStore>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IIncludesParser, IncludesParser>();
            services.AddScoped<IDateService, DateService>();

            return services;
        }
    }
}