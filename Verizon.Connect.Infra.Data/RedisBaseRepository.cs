namespace Verizon.Connect.Infra.Data
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Verizon.Connect.Infra.Data.Options;

    public sealed class RedisRepository
    {
        private readonly ConnectionMultiplexer connection;
        private readonly IDatabase database;

        public RedisRepository(IOptions<RedisOptions> options)
        {
            this.connection = ConnectionMultiplexer.Connect(options.Value.HostName);
            this.database = this.connection.GetDatabase();
        }

        public async Task Add<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            await this.database.StringSetAsync(key, json);
        }

        public async Task<T> Get<T>(string key)
        {
            RedisValue json = await this.database.StringGetAsync(key);
            if (json.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }

        public async Task<IEnumerable<T>> GetAll<T>(RedisKey[] keys)
        {
            var items = await this.database.StringGetAsync(keys);
            var list = new List<T>(); 
            foreach (RedisValue item in items)
            {
                if (item.HasValue)
                {
                    list.Add(JsonConvert.DeserializeObject<T>(item));
                }
            }

            return list;
        }
    }
}
