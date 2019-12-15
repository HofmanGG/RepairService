using AutoMapper;
using HelloSocNetw_BLL.Infrastructure.MapperProfiles;
using HelloSocNetw_PL.Infrastructure.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Infrastructure
{

    public static class ConfiguredAutomapper
    {
        public static void AddConfiguredAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserInfoDTOProfile),
                typeof(CountryDTOProfile),
                typeof(RepairRequestDTOProfile),
                typeof(AppIdentityUserDTOProfile),

               
                typeof(UserInfoModelProfile),
                typeof(CountryModelProfile),
                typeof(RepairRequestModelProfile),
                typeof(RegisterModelProfile));
        }
    }
}
