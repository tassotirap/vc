namespace Verizon.Connect.Application.Tests
{
    using System;

    using NSubstitute;

    using Verizon.Connect.Application.Services;
    using Verizon.Connect.BaseTests.Logger;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Enums;
    using Verizon.Connect.Domain.Plot.Events;
    using Verizon.Connect.Domain.Plot.Models;

    using Xunit;
    using Xunit.Abstractions;

    public class PlotAppServiceTest
    {
        private const int Vehicleid = 10;

        private readonly IEventEmitter<RegisterPlotEvent> emitter;

        private readonly PlotAppService plotAppService;

        public PlotAppServiceTest(ITestOutputHelper output)
        {
            this.emitter = Substitute.For<IEventEmitter<RegisterPlotEvent>>();

            var logger = LoggerFactory.CreateLogger<PlotAppService>(output);

            this.plotAppService = new PlotAppService(this.emitter, logger);
        }

        [Fact(DisplayName = "[Success] - Register event IgnitionOff")]
        public void RegisterIgnitionOffSuccess()
        {
            var entity = new PlotEntity(Vehicleid, 12, 12, DateTime.Now, EventCode.IgnitionOff);

            this.plotAppService.Register(entity);

            this.emitter.Received(1).Emit(Arg.Any<RegisterPlotEvent>());
        }

        [Fact(DisplayName = "[Success] - Register event IgnitionOn")]
        public void RegisterIgnitionOnSuccess()
        {
            var entity = new PlotEntity(Vehicleid, 10, 10, DateTime.Now, EventCode.IgnitionOn);

            this.plotAppService.Register(entity);

            this.emitter.Received(1).Emit(Arg.Any<RegisterPlotEvent>());
        }

        [Fact(DisplayName = "[Success] - Register event Movement")]
        public void RegisterMovementSuccess()
        {
            var entity = new PlotEntity(Vehicleid, 11, 11, DateTime.Now, EventCode.Movement);

            this.plotAppService.Register(entity);

            this.emitter.Received(1).Emit(Arg.Any<RegisterPlotEvent>());
        }
    }
}