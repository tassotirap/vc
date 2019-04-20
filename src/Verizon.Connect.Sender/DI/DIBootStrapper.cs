namespace Verizon.Connect.Sender.DI
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Application.Services;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus.Options;

    public static class DIBootStrapper
    {
        public static void AddSenderServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));

            services.AddSingleton<IEventEmitter<RegisterPlotEvent>, RabbitMQEventEmitter<RegisterPlotEvent>>();

            // Application
            services.AddSingleton<IPlotAppService, PlotAppService>();
        }
    }
}