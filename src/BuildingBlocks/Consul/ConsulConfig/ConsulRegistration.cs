using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ConsulConfig
{
    public static class ConsulRegistration
    {
        public static IServiceCollection ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
                {
                    consulConfig.Address = new Uri("http://localhost:8500");
                }));
            }
            else
            {
                services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
                {
                    consulConfig.Address = new Uri(configuration["ConsulConfig:Address"]);
                }));
            }
        
            return services;
        }
        public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app,
            IHostApplicationLifetime lifetime,
            IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                RegisterWithConsulDevelopment(app,lifetime,configuration);
            }
            else
            {
                RegisterWithConsulProduction(app, lifetime, configuration);
            }

            return app;
        }

        private static void RegisterWithConsulDevelopment(IApplicationBuilder app,
            IHostApplicationLifetime lifetime,
            IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            var uri = configuration.GetValue<Uri>("ConsulConfig:ServiceAddress");
            var serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
            var serviceId = configuration.GetValue<string>("ConsulConfig:ServiceId");
            var port = configuration.GetValue<int>("ConsulConfig:Port");
            var registration = new AgentServiceRegistration()
            {
                ID = serviceId,
                Name = serviceName,
                Address = "localhost",
                Port = port,
                Tags = new[] { serviceName, serviceId }
            };

            logger.LogInformation("Registerin with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

        }
        private static void RegisterWithConsulProduction(IApplicationBuilder app,
            IHostApplicationLifetime lifetime,
            IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            var uri = configuration.GetValue<Uri>("ConsulConfig:ServiceAddress");
            var serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
            var serviceId = configuration.GetValue<string>("ConsulConfig:ServiceId");

            var registration = new AgentServiceRegistration()
            {
                ID = serviceId,
                Name = serviceName,
                Address = $"{uri.Host}",
                Port = uri.Port,
                Tags = new[] { serviceName, serviceId }
            };

            logger.LogInformation("Registerin with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

        }
    }
}
