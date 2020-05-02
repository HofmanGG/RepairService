using System.Collections;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using HelloSocNetw_BLL.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HelloSocNetw_PL.Infrastructure.StartupConfigurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddConfiguredAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthJwtTokenOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = AuthJwtTokenOptions.Audience,
                    ValidateLifetime = true,

                    IssuerSigningKey = AuthJwtTokenOptions.GetSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

            return services;
        }
    }
}

