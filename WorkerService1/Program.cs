using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkerService1.Infrastructure.Database;
using WorkerService1.Infrastructure.EventBus;
using WorkerService1.Infrastructure.Rabbit;
using WorkerService1.Repository;

namespace WorkerService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IChargeRepository, ChargeRepository>(x =>
                        ActivatorUtilities.CreateInstance<ChargeRepository>(
                            x,
                            hostContext.Configuration.GetSection("Database").Get<DatabaseOptions>().ConnectionString)
                        );
                    services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>(x =>
                        ActivatorUtilities.CreateInstance<RabbitMqConnection>(
                            x, 
                            hostContext.Configuration.GetSection("Rabbit").Get<RabbitMqConfiguration>().URI)
                    );
                    services.AddSingleton<IEventBus, EventBus>();
                });
    }
}
