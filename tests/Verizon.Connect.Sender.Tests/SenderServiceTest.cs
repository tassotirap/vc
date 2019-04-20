namespace Verizon.Connect.Sender.Tests
{
    using System.Threading.Tasks;

    using NSubstitute;

    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Application.Services;
    using Verizon.Connect.Domain.Plot.Enums;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Tests.Logger;

    using Xunit;
    using Xunit.Abstractions;

    public class SenderServiceTest
    {
        private const int Vehicleid = 10;

        private readonly IPlotAppService plotAppService;

        private readonly SenderService senderService;

        public SenderServiceTest(ITestOutputHelper output)
        {
            this.plotAppService = Substitute.For<IPlotAppService>();

            var logger = LoggerFactory.CreateLogger<SenderService>(output);

            this.senderService = new SenderService(this.plotAppService, logger);
        }

        [Fact(DisplayName = "[Success] - Create third event")]
        public async Task CreateAfterIgnitionOffEventSuccess()
        {
            PlotEntity plotEntity = null;

            this.plotAppService.When(method => method.Register(Arg.Any<PlotEntity>())).Do(args => plotEntity = (PlotEntity)args[0]);

            await this.senderService.GenerateRandomPlot(Vehicleid);

            await this.senderService.GenerateRandomPlot(Vehicleid, 1);

            await this.senderService.GenerateRandomPlot(Vehicleid, 0);

            await this.senderService.GenerateRandomPlot(Vehicleid);

            Assert.NotNull(plotEntity);
            Assert.Equal(EventCode.IgnitionOn, plotEntity.EventCode);
        }

        [Fact(DisplayName = "[Success] - Create first event")]
        public async Task CreateFirstEventSuccess()
        {
            PlotEntity plotEntity = null;

            this.plotAppService.When(method => method.Register(Arg.Any<PlotEntity>())).Do(args => plotEntity = (PlotEntity)args[0]);

            await this.senderService.GenerateRandomPlot(Vehicleid);

            Assert.NotNull(plotEntity);
            Assert.Equal(EventCode.IgnitionOn, plotEntity.EventCode);
        }

        [Fact(DisplayName = "[Success] - Create second event")]
        public async Task CreateSecondEventSuccess()
        {
            PlotEntity plotEntity = null;

            this.plotAppService.When(method => method.Register(Arg.Any<PlotEntity>())).Do(args => plotEntity = (PlotEntity)args[0]);

            await this.senderService.GenerateRandomPlot(Vehicleid);

            await this.senderService.GenerateRandomPlot(Vehicleid, 1);

            Assert.NotNull(plotEntity);
            Assert.Equal(EventCode.Movement, plotEntity.EventCode);
        }

        [Fact(DisplayName = "[Success] - Create third event")]
        public async Task CreateThirdEventSuccess()
        {
            PlotEntity plotEntity = null;

            this.plotAppService.When(method => method.Register(Arg.Any<PlotEntity>())).Do(args => plotEntity = (PlotEntity)args[0]);

            await this.senderService.GenerateRandomPlot(Vehicleid);

            await this.senderService.GenerateRandomPlot(Vehicleid, 1);

            await this.senderService.GenerateRandomPlot(Vehicleid, 0);

            Assert.NotNull(plotEntity);
            Assert.Equal(EventCode.IgnitionOff, plotEntity.EventCode);
        }
    }
}