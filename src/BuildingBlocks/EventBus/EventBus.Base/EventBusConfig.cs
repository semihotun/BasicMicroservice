using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base
{
    public class EventBusConfig
    {
        //Bağlanırken 5 kere dene
        public int ConnectionRetryCount { get; set; } = 5;
        //Topic Pattern
        public string DefaultTopicName { get; set; } = "EventBus";
        public string EventBusConnectionString { get; set; } = String.Empty;
        //Her Service'e 1 client app name
        public string SubscriberClientAppName {get;set;} = String.Empty;
        public string EventNamePrefix {get;set;} = String.Empty;
        public string EventNameSuffix { get; set; } = "IntegrationEvent";
        public EventBusType EventBusType { get; set; } = EventBusType.RabbitMQ;
        public IConnectionFactory Connection {get;set;}
        public bool DeleteEventPrefix => !String.IsNullOrEmpty(EventNamePrefix);
        public bool DeleteEventSuffix => !String.IsNullOrEmpty(EventNameSuffix);

    }
    public enum EventBusType
    {
        RabbitMQ=0,
        AzureServiceBus=1
    }
}
