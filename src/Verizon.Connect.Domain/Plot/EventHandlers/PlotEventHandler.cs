namespace Verizon.Connect.Domain.Plot.EventHandlers
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotEventHandler : IEventRecived<RegisterPlotEvent>
    {
        private readonly IPlotRepository plotRepository;

        private readonly ILogger<PlotEventHandler> logger;

        public PlotEventHandler(IPlotRepository plotRepository,
                                ILogger<PlotEventHandler> logger)
        {
            this.plotRepository = plotRepository;
            this.logger = logger;
        }

        public async Task<bool> EventReceived(RegisterPlotEvent @event)
        {
            try
            {
                this.logger.LogTrace("Receiving event");

                var result = false;
                switch (@event.Entity.EventCode)
                {
                    case Enums.EventCode.IgnitionOn:
                        result = await this.EventIgnitionOnReceived(@event.Entity);
                        break;
                    case Enums.EventCode.IgnitionOff:
                        result = await this.EventIgnitionOffReceived(@event.Entity);
                        break;
                    case Enums.EventCode.Movement:
                        result = await this.EventMovementReceived(@event.Entity);
                        break;
                }

                this.logger.LogTrace("Received event");

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error receiving event '{JsonConvert.SerializeObject(@event)}");
                return false;
            }
        }

        private async Task<bool> EventIgnitionOnReceived(PlotEntity entity)
        {
            entity.JourneyStart = entity.TimeStamp;
            return
                await this.plotRepository.AddLastIgnitionOn(entity.VId, entity.TimeStamp) &&
                await this.plotRepository.Add(entity);
        }

        private async Task<bool> EventIgnitionOffReceived(PlotEntity entity)
        {
            var lastIgnitionOn = await this.plotRepository.GetLastIgnitionOn(entity.VId);
            entity.JourneyStart = lastIgnitionOn;
            entity.JourneyEnd = entity.TimeStamp;
            return await this.plotRepository.Add(entity);
        }

        private async Task<bool> EventMovementReceived(PlotEntity entity)
        {
            var lastIgnitionOn = await this.plotRepository.GetLastIgnitionOn(entity.VId);
            entity.JourneyStart = lastIgnitionOn;
            return await this.plotRepository.Add(entity);
        }
    }
}
