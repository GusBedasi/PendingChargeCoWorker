using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using WorkerService1.Repository;
using WorkerService1.Helpers.SerializerHelper;
using WorkerService1.Models;

namespace WorkerService1.Infrastructure.Rabbit
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private static readonly object _sync = new object();
        public string _connectionString;
        public IChargeRepository _repository;
        public RabbitMqConnection(string connectionString, IChargeRepository repository)
        {
            _connectionString = connectionString;
            _repository = repository;
        }

        private IConnection Connection {
            get
            {
                lock(_sync)
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

        public void ConsumeQueue()
        {
            var channel = Connection.CreateModel();

            channel.QueueDeclare(queue: "charges.pending.cancellation",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();

                var charge = Serializer.Deserialize<Charge>(body);
                _repository.Update(charge);

                Console.WriteLine(" [x] Received {0}", charge);
            };

            channel.BasicConsume(queue: "charges.pending.cancellation",
                autoAck: true,
                consumer: consumer);
        }
    }
}
