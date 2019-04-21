namespace Verizon.Connect.Tests.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Verizon.Connect.Domain.Plot.Dto;
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

        public Task<IEnumerable<PlotQueryResultDto>> QueryByTimeFrame(int vId, DateTime initialTimeStamp, DateTime finalTimeStamp)
        {
            throw new System.NotImplementedException();
        }

        private string GetKey(PlotEntity plotEntity)
        {
            return this.GetKey(plotEntity.VId, plotEntity.TimeStamp);
        }

        private string GetKey(int vid, DateTime timeStamp)
        {
            return $"Plot:{vid}:{this.Items.Count}";
        }
    }
}
