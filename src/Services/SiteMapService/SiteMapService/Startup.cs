using ConsulConfig;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Setup;
using SiteMapService.Context;
using SiteMapService.Extension;
using SiteMapService.IntegrationEvents;
using SiteMapService.SeedWork;

namespace SiteMapService
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
            services.AddApplicationRegistration();     
            services.AddTransient<IRepository, SiteMapDbContext>();
            services.AddTransient<SiteMapCreatedIntegrationEventHandler>();

            services.RabbitMqSetUp(Configuration, "SiteMapService");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PageService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("MyPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.RegisterWithConsul(lifetime, Configuration);

            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<SiteMapCreatedIntegrationEvent, SiteMapCreatedIntegrationEventHandler>();
        }
    }
}
