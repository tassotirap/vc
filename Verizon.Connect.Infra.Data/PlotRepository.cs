namespace Verizon.Connect.Infra.Data
{
    using StackExchange.Redis;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Models;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotRepository : IPlotRepository
    {
        private readonly RedisRepository redis;

        public PlotRepository(RedisRepository redis)
        {
            this.redis = redis;
        }

        public async Task Add(PlotEntity plotEntity)
        {
            string key = this.GetKey(plotEntity);
            await this.redis.Add(key, plotEntity);
        }

        public async Task<IEnumerable<PlotQueryDto>> QueryByTimeFrame(int vId, int initialTimeStamp, int finalTimeStamp)
        {
            RedisKey[] keys = Enumerable.Range(initialTimeStamp, finalTimeStamp).Select(item => (RedisKey)this.GetKey(vId, item)).ToArray();
            IEnumerable<PlotEntity> result = await this.redis.GetAll<PlotEntity>(keys);
            return result.Select(t => new PlotQueryDto
            {
                EventCode = t.EventCode,
                Lat = t.Lat,
                Lon = t.Lon,
                TimeStamp = t.TimeStamp,
                VId = t.VId
            });
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
