using System;
using System.Reflection;
using AutoMapper;
using BLL.Services;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure
{
    public static class DependencyInjections
    {
        public static void ConfigurePLServices(this IServiceCollection services)
        {
            // Service dependency injection configuration
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRepairRequestsService, RepairRequestsService>();

            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}