using HelloSocNetw_DAL;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Infrastructure.Middlewares
{
    public static class DatabaseSeedingMiddleware
    {
        public static void UseCustomDatabaseSeeding(this IApplicationBuilder app)
        {

            //Seeding data into database
            var serviceProvider = app.ApplicationServices;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
            {
                var unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
                var identityUnitOfWork = serviceScope.ServiceProvider.GetService<IIdentityUnitOfWork>();
                var lgr = serviceScope.ServiceProvider.GetRequiredService<ILogger<Startup>>();

                lgr.LogInformation("Seeding the database");
                AppDbInitializer.SeedAsync(unitOfWork, identityUnitOfWork).Wait();
            }
        }
    }
}
