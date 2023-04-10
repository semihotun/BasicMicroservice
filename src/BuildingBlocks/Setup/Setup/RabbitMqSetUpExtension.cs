using ConsulConfig;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using IdentityEx;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Setup.Ioc;
using SwaggerEx;
using System;

namespace Setup
{
    public static class RabbitMqSetUpExtension
    {
        public static IServiceCollection RabbitMqSetUp(this IServiceCollection services,
            IConfiguration configuration, string serviceName)
        {
            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddMvcCore(opt =>  // or AddMvc()
            {
                // remove formatter that turns nulls into 204 - No Content responses
                // this formatter breaks Angular's Http response JSON parsing
                opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            }).AddAuthorization();

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "serviceName",
                    Connection = new ConnectionFactory()
                    {
                        Uri = new Uri("amqp://guest:guest@s_rabbitmq:5672")
                    },
                    EventBusType = EventBusType.RabbitMQ
                };
                return EventBusFactory.Create(config, sp);
            });
            services.ConfigureConsul(configuration);
            services.AddIdentitySettings(configuration, JwtBearerDefaults.AuthenticationScheme);
            services.AddCustomSwaggerGen(serviceName, serviceName);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            ServiceTool.Create(services);
            return services;
        }
    }
}
