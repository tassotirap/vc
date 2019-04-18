using System.Threading.Tasks;
using Verizon.Connect.Domain.Plot.Models;

namespace Verizon.Connect.Application.Interfaces
{
    public interface IPlotAppService
    {
        Task Register(PlotEntity plotEntity);
    }
}
