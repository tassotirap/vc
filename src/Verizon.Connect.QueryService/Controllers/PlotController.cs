namespace Verizon.Connect.QueryService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Repositories;

    [Route("api/[controller]")]
    [ApiController]
    public class PlotController : ControllerBase
    {
        private readonly IPlotRepository plotRepository;

        public PlotController(IPlotRepository plotRepository)
        {
            this.plotRepository = plotRepository;
        }

        // GET api/values/5
        [HttpGet("{id}/{start}/{end}")]
        public async Task<ActionResult<IEnumerable<PlotQueryDto>>> Get(int id, int start, int end)
        {
            var result = await this.plotRepository.QueryByTimeFrame(id, start, end);
            return Ok(result);
        }
    }
}
