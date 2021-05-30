using RabbitMQ.Client;

namespace WorkerService1.Infrastructure.Rabbit
{
    public interface IRabbitMqConnection
    {
        public IConnection Connection { get; }
    }
}
