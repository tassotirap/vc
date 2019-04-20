namespace Verizon.Connect.Infra.Data
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Verizon.Connect.Infra.Data.Options;

    public sealed class RedisRepository
    {
        private readonly Lazy<ConnectionMultiplexer> lazyConnection;

        private ConnectionMultiplexer connection => this.lazyConnection.Value;

        private IDatabase database => this.connection.GetDatabase();


        public RedisRepository(IOptions<RedisOptions> options)
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { options.Value.HostName },
                KeepAlive = 10,
                AbortOnConnectFail = false,
                ConfigurationChannel = "",
                TieBreaker = "",
                ConfigCheckSeconds = 0,
                CommandMap = CommandMap.Create(new HashSet<string>
                { // EXCLUDE a few commands
                    "SUBSCRIBE", "UNSUBSCRIBE", "CLUSTER"
                }, available: false),
                AllowAdmin = true
            };

            this.lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public async Task<bool> Add<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return await this.database.StringSetAsync(key, json);
        }

        public async Task<T> Get<T>(string key)
        {
            var json = await this.database.StringGetAsync(key);
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
            foreach (var item in items)
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
