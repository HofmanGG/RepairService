using FluentValidation.AspNetCore;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Infrastructure
{
    public static class ControllersConfiguration
    {
        public static void AddConfiguredControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
    }
}
