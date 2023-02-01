using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistenConnection : IDisposable
    {
        private readonly IConnectionFactory connectionFactory;

        private IConnection connection;
        private readonly int retryCount;

        private object lock_object = new object();

        private bool _disposed = false;
        public RabbitMQPersistenConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            this.connectionFactory = connectionFactory;
            
            this.retryCount = retryCount;
        }

        public bool IsConncected => connection != null && connection.IsOpen;

        public IModel CreateModel()
        {
            return connection.CreateModel();
        }

        public bool TryConnect()
        {
            //lock-->Bir diğer connect işlemi bitesiye kadar bekler  
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>().Or<BrokerUnreachableException>()
                        .WaitAndRetry(retryCount, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)), (ex, time) =>
                        {

                        });

                policy.Execute(() =>
                {
                    connection = connectionFactory.CreateConnection();

                });
                if (IsConncected)
                {
                    connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    connection.CallbackException += Connection_CallbackException;
                    connection.ConnectionBlocked += Connection_ConnectionBlocked;
                    return true;
                }
                return false;
            }
        }

        private void Connection_ConnectionBlocked(object sender, global::RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            //Log here
            if (_disposed) return;
            TryConnect();
        }

        private void Connection_CallbackException(object sender, global::RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            //Log here
            if (_disposed) return;
            TryConnect();
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            //Log here

            if (_disposed) return;
            TryConnect();
        }

        public void Dispose()
        {
            _disposed = true;
            connection.Dispose();
        }
    }
}
