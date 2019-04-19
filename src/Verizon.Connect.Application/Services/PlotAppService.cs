namespace Verizon.Connect.Application.Services
{
    using System.Threading.Tasks;
    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Domain.Plot.Models;

    public class PlotAppService : IPlotAppService
    {
        private readonly IEventEmitter<RegisterNewPlotEvent> eventEmitter;

        public PlotAppService(IEventEmitter<RegisterNewPlotEvent> eventEmitter)
        {
            this.eventEmitter = eventEmitter;
        }

        public async Task Register(PlotEntity plotEntity)
        {
            await this.eventEmitter.EmitAsync(new RegisterNewPlotEvent(plotEntity));
        }
    }
}
