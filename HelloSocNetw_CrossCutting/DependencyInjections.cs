using HelloSocNetw_CrossCutting.Services;
using Microsoft.Extensions.DependencyInjection;
using HelloSocNetw_CrossCutting.Interfaces;

namespace HelloSocNetw_CrossCutting
{
    public static class DependencyInjections
    {
        public static void ConfigureCrossCuttingServices(this IServiceCollection services)
        {
            // Service dependency injection configuration
            services.AddScoped<IDateTimeService, DateTimeService>();
        }
    }
}