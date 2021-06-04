using System;
using RabbitMQ.Client;

namespace WorkerService1.Infrastructure.Rabbit
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private static readonly object Sync = new object();
        private readonly string _connectionString;

        public RabbitMqConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IConnection Rabbit
        {
            get
            {
                lock (Sync)
                {

                    var factory = new ConnectionFactory()
                    {
                        Uri = new Uri(_connectionString),
                        AutomaticRecoveryEnabled = true
                    };

                    return factory.CreateConnection();
                }
            }
        }
    }
}
