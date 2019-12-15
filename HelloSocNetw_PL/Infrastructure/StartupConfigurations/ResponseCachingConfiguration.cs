using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class ResponseCachingConfiguration
    {
        public static void AddConfiguredResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }
    }
}
