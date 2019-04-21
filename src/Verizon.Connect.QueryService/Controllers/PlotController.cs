namespace Verizon.Connect.QueryService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Domain.Plot.Repositories;

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

        [HttpGet("{id}/{start}/{end}")]
        public async Task<ActionResult<IEnumerable<PlotQueryResultDto>>> Get(int id, DateTime start, DateTime end)
        {
            this.logger.LogTrace($"Getting id:{id}, start:{start}, end:{end}");

            var result = await this.plotRepository.QueryByTimeFrame(id, start, end);

            this.logger.LogTrace($"Got id:{id}, start:{start}, end:{end}");

            return this.Ok(result);
        }
    }
}