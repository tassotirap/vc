namespace Verizon.Connect.BaseTests.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotInMemoryRepository : InMemoryRepository, IPlotRepository
    {
        private int numPlots = 0;

        public Task<bool> Add(PlotEntity plotEntity)
        {
            var key = this.GetKey(plotEntity);
            base.Add(key, plotEntity);
            this.numPlots++;
            return Task.FromResult(true);
        }

        public Task<IEnumerable<PlotQueryResultDto>> QueryByTimeFrame(int vId, DateTime initialTimeStamp, DateTime finalTimeStamp)
        {
            return Task.FromResult(new List<PlotQueryResultDto>().AsEnumerable());
        }

        public Task<bool> AddIgnitionOn(int vId, DateTime timeStamp)
        {
            base.Add($"Plot:{vId}:LastIgnitionOn", timeStamp);
            return Task.FromResult(true);
        }

        public Task<DateTime?> GetLastIgnitionOn(int vId, DateTime timeStamp)
        {
            return Task.FromResult(this.Get<DateTime?>($"Plot:{vId}:LastIgnitionOn"));
        }

        private string GetKey(PlotEntity plotEntity)
        {
            return this.GetKey(plotEntity.VId, plotEntity.TimeStamp);
        }

        private string GetKey(int vid, DateTime timeStamp)
        {
            return $"Plot:{vid}:{this.numPlots}";
        }
    }
}
