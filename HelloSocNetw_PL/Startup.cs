using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.MapperProfiles;
using HelloSocNetw_DAL;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Infrastructure.MapperProfiles;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HelloSocNetw_PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.ConfigureDALServices();
            services.ConfigureBLLServices();
            services.ConfigurePLServices();

            services.AddAutoMapper(typeof(RegisterModelProfile), typeof(UserInfoModelProfile), typeof(UserInfoDTOProfile));

            var connectingString = Configuration.GetConnectionString("HelloDatabase");
            services.AddDbContextPool<SocNetwContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectingString)
                    .EnableSensitiveDataLogging());

            services.AddAuthentication(options => {
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
            services.AddAutoMapper(typeof(UserInfoModelProfile));
            services.AddMvc(opt =>
                {
                    opt.Filters.Add(typeof(ValidatorActionFilter));

                }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var serviceProvider = app.ApplicationServices;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
            {
                var unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
                AppDbInitializer.Seed(unitOfWork);
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
