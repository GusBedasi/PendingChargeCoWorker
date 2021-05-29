using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerService1.Infrastructure.EventBus;
using WorkerService1.Infrastructure.Rabbit;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<Worker> _logger;

        public Worker(IEventBus eventBus, ILogger<Worker> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _eventBus.ConsumeQueue();
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
