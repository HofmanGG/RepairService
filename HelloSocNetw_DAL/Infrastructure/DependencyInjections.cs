using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Identity;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_DAL.Infrastructure
{
    public static class DependencyInjections
    {
        public static void ConfigureDALServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppIdentityUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<SocNetwContext>()
                .AddRoleManager<AppRoleManager>()
                .AddUserManager<AppUserManager>()
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

            services.AddScoped<IIncludesParserFactory, IncludesParserFactory>();
        }
    }
}