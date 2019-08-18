using System.Reflection;
using AutoMapper;
using HelloSocNetw_BLL.Infrastructure.MapperProfiles;
using HelloSocNetw_DAL;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Identity;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_DAL.UnitsOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_BLL.Infrastructure
{
    public static class DependencyInjections
    {
        public static void ConfigureBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}