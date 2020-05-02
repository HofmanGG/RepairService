using AutoMapper;
using HelloSocNetw_BLL.Infrastructure.MapperProfiles;
using HelloSocNetw_PL.Infrastructure.MapperProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{

    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddConfiguredAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserInfoDTOProfile),
               
                typeof(UserInfoModelProfile));

            return services;
        }
    }
}
