namespace Verizon.Connect.Tests.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotInMemoryRepository : InMemoryRepository, IPlotRepository
    {
        public Task<bool> Add(PlotEntity plotEntity)
        {
            var key = this.GetKey(plotEntity);
            base.Add(key, plotEntity);
            return Task.FromResult(true);
        }

        public Task<bool> AddLastIgnitionOn(string vId, string timeStamp)
        {
            base.Add($"Plot:{vId}:LastIgnitionOn", timeStamp);
            return Task.FromResult(true);
        }

        public Task<string> GetLastIgnitionOn(string vId)
        {
            return Task.FromResult(base.Get<string>($"Plot:{vId}:LastIgnitionOn"));
        }

        public Task<IEnumerable<PlotEntity>> QueryByTimeFrame(int vId, int initialTimeStamp, int finalTimeStamp)
        {
            throw new System.NotImplementedException();
        }

        private string GetKey(PlotEntity plotEntity)
        {
            return this.GetKey(plotEntity.VId, plotEntity.TimeStamp);
        }

        private string GetKey(string vid, string timeStamp)
        {
            return $"Plot:{vid}:{timeStamp}";
        }

        private string GetKey(int vid, int timeStamp)
        {
            return $"Plot:VId{vid}:t{timeStamp}";
        }
    }
}
