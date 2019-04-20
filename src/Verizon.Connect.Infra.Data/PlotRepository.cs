namespace Verizon.Connect.Infra.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StackExchange.Redis;

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
            return await this.redis.Add(key, plotEntity);
        }

        public async Task<IEnumerable<PlotQueryResultDto>> QueryByTimeFrame(int vId, int initialTimeStamp, int finalTimeStamp)
        {
            var keys = Enumerable.Range(initialTimeStamp, finalTimeStamp + 1).Select(item => (RedisKey)this.GetKey(vId, item)).ToArray();

            var plots = await this.redis.GetAll<PlotEntity>(keys);

            PlotEntity lastIgnitionOn = null;
            return plots.Select(plot => this.PlotEntityToPlotQueryResultDto(plot, ref lastIgnitionOn));
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

        private PlotQueryResultDto PlotEntityToPlotQueryResultDto(PlotEntity plotEntity, ref PlotEntity lastIgnitionOn)
        {
            var result = new PlotQueryResultDto(plotEntity);
            if (plotEntity.EventCode == EventCode.IgnitionOn)
            {
                result.JourneyStart = plotEntity.TimeStamp;
                lastIgnitionOn = plotEntity;
            }
            else if (plotEntity.EventCode == EventCode.IgnitionOff)
            {
                result.JourneyStart = lastIgnitionOn?.TimeStamp;
                result.JourneyEnd = plotEntity.TimeStamp;
                lastIgnitionOn = null;
            }
            else if (plotEntity.EventCode == EventCode.Movement)
            {
                result.JourneyStart = lastIgnitionOn?.TimeStamp;
            }

            return result;
        }
    }
}