using FluentValidation.AspNetCore;
using HelloSocNetw_PL.Infrastructure.Filters;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection AddConfiguredControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add(new ModelValidatorFilter());
                    //options.Filters.Add(new ApiExceptionFilter());
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }
    }
}
