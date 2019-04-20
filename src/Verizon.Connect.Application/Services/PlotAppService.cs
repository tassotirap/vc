namespace Verizon.Connect.Application.Services
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Domain.Plot.Models;

    public class PlotAppService : IPlotAppService
    {
        private readonly IEventEmitter<RegisterPlotEvent> eventEmitter;

        private readonly ILogger<PlotAppService> logger;

        public PlotAppService(IEventEmitter<RegisterPlotEvent> eventEmitter, 
                              ILogger<PlotAppService> logger)
        {
            this.eventEmitter = eventEmitter;
            this.logger = logger;
        }

        public Task Register(PlotEntity plotEntity)
        {
            this.logger.LogTrace("Registering Event");

            this.eventEmitter.Emit(new RegisterPlotEvent(plotEntity));

            this.logger.LogTrace("Registered Event");

            return Task.CompletedTask;
        }
    }
}
