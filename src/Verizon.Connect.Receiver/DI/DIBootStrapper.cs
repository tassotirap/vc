namespace Verizon.Connect.Receiver.DI
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.EventHandlers;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Domain.Plot.Repositories;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus.Options;
    using Verizon.Connect.Infra.Data;
    using Verizon.Connect.Infra.Data.Options;

    public static class DIBootStrapper
    {
        public static void AddReceiverServices(this IServiceCollection services, IConfiguration configuration)
        {
            //RabbitMQ
            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));
            services.AddSingleton<IEventSubscriber<RegisterPlotEvent>, RabbitMQEventSubscriber<RegisterPlotEvent>>();
            services.AddSingleton<IEventReceived<RegisterPlotEvent>, PlotEventHandler>();

            //Redis
            services.Configure<RedisOptions>(configuration.GetSection("Redis"));
            services.AddSingleton<RedisRepository>();
            services.AddSingleton<IPlotRepository, PlotRepository>();


        }
    }
}
