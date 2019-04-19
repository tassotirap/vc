namespace Verizon.Connect.Domain.Plot.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Models;

    public interface IPlotRepository
    {
        Task Add(PlotEntity plotEntity);

        Task<IEnumerable<PlotQueryDto>> QueryByTimeFrame(int vId, int initialTimeStamp, int finalTimeStamp);
    }
}
