namespace Verizon.Connect.Application.Services
{
    using System.Threading.Tasks;
    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Commands;
    using Verizon.Connect.Domain.Plot.Models;

    public class PlotAppService : IPlotAppService
    {
        private readonly IMediatorHandler bus;

        public PlotAppService(IMediatorHandler bus)
        {
            this.bus = bus;
        }

        public async Task Register(PlotEntity plotEntity)
        {
            await bus.SendCommand(new RegisterNewPlotCommand(plotEntity));
        }
    }
}
