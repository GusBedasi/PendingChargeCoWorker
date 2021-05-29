using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService1.Infrastructure.Rabbit
{
    public interface IRabbitMqConnection
    {
        void ConsumeQueue();
    }
}
