namespace Verizon.Connect.QueryService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Repositories;
    using Verizon.Connect.QueryService.ViewModel;

    [Route("api/[controller]")]
    [ApiController]
    public class PlotController : ControllerBase
    {
        private readonly IPlotRepository plotRepository;

        private readonly ILogger<PlotController> logger;

        public PlotController(IPlotRepository plotRepository,
                              ILogger<PlotController> logger)
        {
            this.plotRepository = plotRepository;
            this.logger = logger;
        }

        [HttpGet("{VId}/{StartDateTime}/{EndDateTime}")]
        public async Task<ActionResult<IEnumerable<PlotQueryResultDto>>> Get([FromRoute]PlotQueryRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.logger.LogTrace($"Getting id:{request.VId}, start:{request.StartDateTime}, end:{request.EndDateTime}");

            var result = await this.plotRepository.QueryByTimeFrame(request.VId.GetValueOrDefault(), request.StartDateTime.GetValueOrDefault(), request.EndDateTime.GetValueOrDefault());

            this.logger.LogTrace($"Got id:{request.VId}, start:{request.StartDateTime}, end:{request.EndDateTime}");

            return this.Ok(result);
        }
    }
}