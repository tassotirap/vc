namespace Verizon.Connect.Infra.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Enums;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotRepository : IPlotRepository
    {
        private readonly RedisRepository redis;

        public PlotRepository(RedisRepository redis)
        {
            this.redis = redis;
        }

        public async Task<bool> Add(PlotEntity plotEntity)
        {
            var key = this.GetKey(plotEntity);
            return await this.redis.AddSet(key, plotEntity, plotEntity.TimeStamp.ToOADate());
        }

        public async Task<IEnumerable<PlotQueryResultDto>> QueryByTimeFrame(int vId, DateTime initialTimeStamp, DateTime finalTimeStamp)
        {
            var key = this.GetKey(vId);
            var plots = await this.redis.GetAllSet<PlotEntity>(key, initialTimeStamp.ToOADate(), finalTimeStamp.ToOADate());
            PlotEntity lastIgnitionOn = null;
            return plots.Select(plot => this.PlotEntityToPlotQueryResultDto(plot, ref lastIgnitionOn));
        }

        private string GetKey(PlotEntity plotEntity)
        {
            return this.GetKey(plotEntity.VId);
        }

        private string GetKey(int vid)
        {
            return $"Plot:{vid}";
        }

        private PlotQueryResultDto PlotEntityToPlotQueryResultDto(PlotEntity plotEntity, ref PlotEntity lastIgnitionOn)
        {
            var result = new PlotQueryResultDto(plotEntity);
            switch (plotEntity.EventCode)
            {
                case EventCode.IgnitionOn:
                    result.JourneyStart = plotEntity.TimeStamp;
                    lastIgnitionOn = plotEntity;
                    break;
                case EventCode.IgnitionOff:
                    result.JourneyStart = lastIgnitionOn?.TimeStamp;
                    result.JourneyEnd = plotEntity.TimeStamp;
                    lastIgnitionOn = null;
                    break;
                case EventCode.Movement:
                    result.JourneyStart = lastIgnitionOn?.TimeStamp;
                    break;
            }

            return result;
        }
    }
}