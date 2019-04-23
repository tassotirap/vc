namespace Verizon.Connect.Domain.Plot.EventHandlers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Enums;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotEventHandler : IEventReceived<RegisterPlotEvent>
    {
        private readonly ILogger<PlotEventHandler> logger;

        private readonly IPlotRepository plotRepository;

        public PlotEventHandler(IPlotRepository plotRepository, ILogger<PlotEventHandler> logger)
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
                    case EventCode.IgnitionOn:
                        result = await this.EventIgnitionOnReceived(@event.Entity);
                        break;
                    case EventCode.IgnitionOff:
                        result = await this.EventIgnitionOffReceived(@event.Entity);
                        break;
                    case EventCode.Movement:
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
            return
                await this.plotRepository.AddIgnitionOn(entity.VId, entity.TimeStamp) &&
                await this.plotRepository.Add(entity);
        }

        private async Task<bool> EventIgnitionOffReceived(PlotEntity entity)
        {
            return await this.plotRepository.Add(entity);
        }

        private async Task<bool> EventMovementReceived(PlotEntity entity)
        {
            return await this.plotRepository.Add(entity);
        }
    }
}