using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkerService1.Helpers.SerializerHelper;
using WorkerService1.Infrastructure.Rabbit;
using WorkerService1.Models;
using WorkerService1.Repository;

namespace WorkerService1.Infrastructure.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IRabbitMqConnection _connection;
        public IChargeRepository _chargeRepository;

        public EventBus(IRabbitMqConnection connection, IChargeRepository chargeRepository)
        {
            _connection = connection;
            _chargeRepository = chargeRepository;
        }


        public void ConsumeQueue()
        {
            var channel = _connection.Connection.CreateModel();

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
                _chargeRepository.Update(charge);
            };

            channel.BasicConsume(queue: "charges.pending.cancellation",
                autoAck: true,
                consumer: consumer);
        }
    }
}
