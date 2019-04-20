namespace Verizon.Connect.Domain.Plot.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Plot.Models;

    public interface IPlotRepository
    {
        Task<bool> Add(PlotEntity plotEntity);

        Task<IEnumerable<PlotEntity>> QueryByTimeFrame(int vId, int initialTimeStamp, int finalTimeStamp);

        Task<bool> AddLastIgnitionOn(string vId, string timeStamp);

        Task<string> GetLastIgnitionOn(string vId);
    }
}
