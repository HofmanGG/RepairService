using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {  
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "My API", 
                    Version = "v1",
                    Description = "ASP.NET Core Web API"});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
