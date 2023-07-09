using Polly;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistentConnection : IDisposable
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly int retryCount;
        private IConnection connection;
        private object lock_object = new object();
        private bool _disposed;


        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            this.connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",//guest kullanıcı adı ya da yeni eklediğiniz kullanıcı adı
                Password = "guest",//guest şifresi ya da belirlediğiniz şifre
                VirtualHost = "/",
                //Port = -1 yapmalısınız ya da Port kısmını yazmadan kullanabilirsiniz
            };
            this.retryCount = retryCount;
        }

        public bool IsConnected => connection != null && connection.IsOpen;


        public IModel CreateModel()
        {
            return connection.CreateModel();
        }

        public void Dispose()
        {
            _disposed = true;
            connection.Dispose();
        }


        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                    }
                );

                policy.Execute(() =>
                {
                    connection = connectionFactory.CreateConnection();
                });

                if (IsConnected)
                {
                    connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    connection.CallbackException += Connection_CallbackException;
                    connection.ConnectionBlocked += Connection_ConnectionBlocked;
                    // log

                    return true;
                }

                return false;
            }
        }

        private void Connection_ConnectionBlocked(object sender, global::RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_CallbackException(object sender, global::RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            // log Connection_ConnectionShutdown

            if (_disposed) return;

            TryConnect();
        }
    }
}


