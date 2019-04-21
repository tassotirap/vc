namespace Verizon.Connect.Domain.Plot.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Models;

    public interface IPlotRepository
    {
        Task<bool> Add(PlotEntity plotEntity);

        Task<IEnumerable<PlotQueryResultDto>> QueryByTimeFrame(int vId, DateTime initialTimeStamp, DateTime finalTimeStamp);
    }
}
