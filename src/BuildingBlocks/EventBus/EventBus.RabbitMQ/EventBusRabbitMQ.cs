using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        RabbitMQPersistenConnection rabbitMQPersistenConnection;
        IModel consumerChannel;
        public EventBusRabbitMQ(EventBusConfig eventBusConfig, IServiceProvider serviceProvider) : base(eventBusConfig, serviceProvider)
        {
            rabbitMQPersistenConnection = new RabbitMQPersistenConnection(eventBusConfig.Connection, eventBusConfig.ConnectionRetryCount);
            consumerChannel = CreateComsumerChannel();
            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            eventName = ProccessEventName(eventName);
            if (!rabbitMQPersistenConnection.IsConnected)
            {
                rabbitMQPersistenConnection.TryConnect();
            }
            consumerChannel.QueueBind(queue: GetSubName(eventName), exchange: eventBusConfig.DefaultTopicName, routingKey: eventName);

            if (SubsManager.IsEmpty)
            {
                consumerChannel.Close();
            }
        }

        public override void Publish(IntegrationEvent integrationEvent)
        {
            if (!rabbitMQPersistenConnection.IsConnected)
            {
                rabbitMQPersistenConnection.TryConnect();
            }

            var policy = Policy.Handle<SocketException>().Or<BrokerUnreachableException>()
                       .WaitAndRetry(eventBusConfig.ConnectionRetryCount,
                       retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)), (ex, time) =>
                       {
                           //log here
                       });

            var eventName = integrationEvent.GetType().Name;

            eventName = ProccessEventName(eventName);

            consumerChannel.ExchangeDeclare(exchange: eventBusConfig.DefaultTopicName, type: "direct");

            var message = JsonConvert.SerializeObject(integrationEvent);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = consumerChannel.CreateBasicProperties();

                properties.DeliveryMode = 2;

                consumerChannel.BasicPublish(exchange: eventBusConfig.DefaultTopicName, routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
            });
        }

        public override void Subscribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = ProccessEventName(eventName);

            if (!SubsManager.HasSubscriptionsForEvent(eventName))
            {
                if (!rabbitMQPersistenConnection.IsConnected)
                {
                    rabbitMQPersistenConnection.TryConnect();
                }

                consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                consumerChannel.QueueBind(queue: GetSubName(eventName), exchange: eventBusConfig.DefaultTopicName, routingKey: eventName);
            }
            SubsManager.AddSubscription<T, TH>();
            StartBasicConsumer(eventName);
        }

        public override void UnSubscribe<T, TH>()
        {
            SubsManager.RemoveSubscription<T, TH>();
        }

        private IModel CreateComsumerChannel()
        {
            if (!rabbitMQPersistenConnection.IsConnected)
            {
                rabbitMQPersistenConnection.TryConnect();
            }
            var channel = rabbitMQPersistenConnection.CreateModel();

            channel.ExchangeDeclare(exchange: eventBusConfig.DefaultTopicName, type: "direct");

            return channel;
        }

        private void StartBasicConsumer(string eventName)
        {
            if (consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(consumerChannel);
                consumer.Received += Consumer_Received;
                consumerChannel.BasicConsume(queue: GetSubName(eventName), autoAck: false, consumer: consumer);
            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;

            eventName = ProccessEventName(eventName);

            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception)
            {
            }
            consumerChannel.BasicAck(e.DeliveryTag, false);
        }
    }

}