using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Setup;
using EventBus.Base.Abstraction;
using EventBustUnitTest.IntegrationEvents.Test;
using EventBus.Factory;
using EventBus.Base;
using RabbitMQ.Client;
using System;

namespace EventBustUnitTest
{
    [TestClass]
    public class EventBusTest
    {

        private ServiceCollection services;
        public IConfiguration Configuration { get; }
        public EventBusTest()
        {
            services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());
        }

        [TestMethod]
        public void PublishRabbitMqQue()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "serviceName",
                    Connection = new ConnectionFactory()
                    {
                        Uri = new Uri("amqp://guest:guest@localhost:5672")
                    },
                    EventBusType = EventBusType.RabbitMQ
                };
                return EventBusFactory.Create(config, sp);
            });

            var sp = services.BuildServiceProvider();
            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Publish(new TestIntegration(true));
        }
    }
}


