namespace Verizon.Connect.QueryService.Tests
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Verizon.Connect.BaseTests.Logger;
    using Verizon.Connect.BaseTests.Repository;
    using Verizon.Connect.QueryService.Controllers;
    using Verizon.Connect.QueryService.ViewModel;
    using Xunit;
    using Xunit.Abstractions;

    public class PlotControllerTest
    {
        private readonly PlotInMemoryRepository plotRepository;

        private readonly PlotController plotController;

        public PlotControllerTest(ITestOutputHelper output)
        {
            this.plotRepository = new PlotInMemoryRepository();

            var logger = LoggerFactory.CreateLogger<PlotController>(output);

            this.plotController = new PlotController(this.plotRepository, logger);
        }

        [Fact(DisplayName = "[Success] - Successful request")]
        public async Task SuccessRequest()
        {
            var request = new PlotQueryRequest { VId = 10, StartDateTime = DateTime.Now, EndDateTime = DateTime.Now };

            var result = await this.plotController.Get(request);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Theory(DisplayName = "[Fail] - Failure request")]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public async Task EmptyDataRequest(bool hasVId, bool hasStartDateTime, bool hasEndDateTime)
        {
            var request = new PlotQueryRequest
            {
                VId = hasVId ? 10 : (int?)null,
                StartDateTime = hasStartDateTime ? DateTime.Now : (DateTime?)null,
                EndDateTime = hasEndDateTime ? DateTime.Now : (DateTime?)null
            };

            this.plotController.BindViewModel(request);

            var result = await this.plotController.Get(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
