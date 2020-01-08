using HelloSocNetw_BLL.Entities;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Infrastructure.MapperProfiles;
using HelloSocNetw_PL.Infrastructure.Middlewares;
using HelloSocNetw_PL.Infrastructure.StartupConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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
            services.AddHttpContextAccessor();
            services.AddConfiguredDependencies();
            services.AddConfiguredDBContext(Configuration);
            services.AddConfiguredAutomapper();
            services.AddConfiguredAuthorization();
            services.AddConfiguredAuthentication();
            services.AddConfiguredControllers();
            services.AddConfiguredSwagger();
            services.AddConfiguredResponseCaching();
            services.AddConfiguredLogging();
            services.AddOptions();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
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

            app.UseCustomDatabaseSeeding();
            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();
            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
