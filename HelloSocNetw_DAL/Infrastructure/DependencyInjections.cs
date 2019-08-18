using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_DAL.Infrastructure
{
    public static class DependencyInjections
    {
        public static void ConfigureDALServices( this IServiceCollection services)
        {
            services.AddIdentity<AppIdentityUser, AppUserRole>()
                .AddEntityFrameworkStores<SocNetwContext>()
                .AddRoleManager<AppRoleManager>()
                .AddUserManager<AppUserManager>()
                .AddUserStore<AppUserStore>()
                .AddRoleStore<AppRoleStore>()
                .AddDefaultTokenProviders();
        }
    }
}