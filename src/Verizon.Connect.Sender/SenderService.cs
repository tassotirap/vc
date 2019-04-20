namespace Verizon.Connect.Sender
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Domain.Plot.Enums;
    using Verizon.Connect.Domain.Plot.Models;

    public class SenderService
    {
        private readonly IPlotAppService plotAppService;

        private readonly ILogger<SenderService> logger;

        private readonly Random random;

        private EventCode currentEventCode;

        private int lastTimeStamp;

        public SenderService(IPlotAppService plotAppService, 
                             ILogger<SenderService> logger)
        {
            this.plotAppService = plotAppService;
            this.logger = logger;
            this.random = new Random();
        }

        /// <summary>
        /// Generate a new RandomPlot
        /// </summary>
        /// <param name="vehicleId">Vehicle Id</param>
        /// <param name="movementStateChange">Chance to do Movement instead of IgnitionOff</param>
        /// <returns>Async Task</returns>
        public async Task GenerateRandomPlot(int vehicleId, double movementStateChange = 0.95)
        {
            this.logger.LogTrace("Generating a new Plot");

            PlotEntity plotEntity;
            if (this.lastTimeStamp == 0)
            {
                this.currentEventCode = EventCode.IgnitionOn;
                plotEntity = this.GeneratePlot(vehicleId, EventCode.IgnitionOn);
            }
            else
            {
                switch (this.currentEventCode)
                {
                    case EventCode.IgnitionOff:
                        this.currentEventCode = EventCode.IgnitionOn;
                        plotEntity = this.GeneratePlot(vehicleId, EventCode.IgnitionOn);
                        break;
                    default:
                        {
                            if (this.random.NextDouble() < movementStateChange)
                            {
                                this.currentEventCode = EventCode.Movement;
                                plotEntity = this.GeneratePlot(vehicleId, EventCode.Movement);
                            }
                            else
                            {
                                this.currentEventCode = EventCode.IgnitionOff;
                                plotEntity = this.GeneratePlot(vehicleId, EventCode.IgnitionOff);
                            }

                            break;
                        }
                }
            }

            await this.plotAppService.Register(plotEntity);

            this.logger.LogTrace("Generated a new Plot");
        }

        private PlotEntity GeneratePlot(int vehicleId, EventCode eventCode)
        {
            var plotEntity = new PlotEntity(vehicleId, this.lastTimeStamp, eventCode);

            this.lastTimeStamp++;

            return plotEntity;
        }
    }
}