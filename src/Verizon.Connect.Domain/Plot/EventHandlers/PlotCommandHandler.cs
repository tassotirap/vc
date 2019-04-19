namespace Verizon.Connect.Domain.Plot.EventHandlers
{
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotCommandHandler : IEventRecived<RegisterNewPlotEvent>
    {
        private readonly IPlotRepository plotRepository;

        public PlotCommandHandler(IPlotRepository plotRepository)
        {
            this.plotRepository = plotRepository;
        }

        public void EventRecived(RegisterNewPlotEvent @event)
        {
            this.plotRepository.Add(@event.Entity).Wait();
        }

        public async Task EventRecivedAsync(RegisterNewPlotEvent @event)
        {
            await this.plotRepository.Add(@event.Entity);
        }
    }
}
