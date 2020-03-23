using HelloSocNetw_DAL;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppIdentityUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
                var lgr = serviceScope.ServiceProvider.GetRequiredService<ILogger<Startup>>();

                lgr.LogInformation("Seeding the database");
                AppDbInitializer.SeedAsync(unitOfWork, userManager, roleManager).Wait();
            }
        }
    }
}
