namespace Verizon.Connect.Sender.DI
{
    using Microsoft.Extensions.DependencyInjection;
    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Application.Services;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Infra.CrossCutting.Bus;
    using MediatR;

    public class DIBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Bus
            services.AddSingleton<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddSingleton<IPlotAppService, PlotAppService>();

            // Application
            services.AddSingleton<SenderApplication>();

            services.AddMediatR(typeof(DIBootStrapper));
        }
    }
}
