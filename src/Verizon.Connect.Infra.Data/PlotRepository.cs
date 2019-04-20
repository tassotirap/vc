namespace Verizon.Connect.Infra.Data
{
    using StackExchange.Redis;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task<bool> AddLastIgnitionOn(string vId, string timeStamp)
        {
            return await this.redis.Add($"Plot:{vId}:LastIgnitionOn", $"{timeStamp}");
        }

        public async Task<string> GetLastIgnitionOn(string vId)
        {
            return await this.redis.Get<string>($"Plot:{vId}:LastIgnitionOn");
        }

        public async Task<IEnumerable<PlotEntity>> QueryByTimeFrame(int vId, int initialTimeStamp, int finalTimeStamp)
        {
            var keys = Enumerable.Range(initialTimeStamp, finalTimeStamp + 1).Select(item => (RedisKey)this.GetKey(vId, item)).ToArray();
            return await this.redis.GetAll<PlotEntity>(keys);
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
