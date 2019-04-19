namespace Verizon.Connect.Sender.DI
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Application.Services;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus;

    public class DIBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));

            services.AddSingleton<IEventEmitter<RegisterNewPlotEvent>, RabbitMQEventEmitter<RegisterNewPlotEvent>>();

            // Application
            services.AddSingleton<IPlotAppService, PlotAppService>();

            // Application
            services.AddSingleton<SenderApplication>();

            
        }
    }
}
