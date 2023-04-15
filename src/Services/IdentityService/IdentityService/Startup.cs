using ConsulConfig;
using IdentityService.Context;
using IdentityService.Extension;
using IdentityService.Helper;
using IdentityService.SeedWork;
using IdentityService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Setup;

namespace IdentityService
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
            services.ConfigureDbContext(Configuration);
            services.AddTransient<IRepository,IdentityContext>();
            services.AddTransient<IAdminAuthService, AdminAuthService>();
            services.AddTransient<IJwtService, JwtService>();

            services.AddApplicationRegistration();
            services.RabbitMqSetUp(Configuration, "IdentityService");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService v1"));
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService v1"));
            }
  
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.RegisterWithConsul(lifetime, Configuration);
        }
    }
}
