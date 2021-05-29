using WorkerService1.Infrastructure.Rabbit;

namespace WorkerService1.Infrastructure.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IRabbitMqConnection _connection;
        public EventBus(IRabbitMqConnection connection)
        {
            _connection = connection;
        }
        public void ConsumeQueue()
        {
            _connection.ConsumeQueue();
        }
    }
}
