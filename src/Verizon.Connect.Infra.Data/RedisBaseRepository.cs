namespace Verizon.Connect.Infra.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    using StackExchange.Redis;

    using Verizon.Connect.Infra.Data.Options;

    public sealed class RedisRepository
    {
        private readonly Lazy<ConnectionMultiplexer> lazyConnection;

        public RedisRepository(IOptions<RedisOptions> options)
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = {
                              options.Value.HostName
                           },
                KeepAlive = 10,
                AbortOnConnectFail = false,
                ConfigurationChannel = string.Empty,
                TieBreaker = string.Empty,
                ConfigCheckSeconds = 0,
                AllowAdmin = true
            };

            this.lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        private ConnectionMultiplexer Connection => this.lazyConnection.Value;

        private IDatabase Database => this.Connection.GetDatabase();

        public async Task<bool> Add<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return await this.Database.StringSetAsync(key, json);
        }

        public async Task<bool> AddHash<T>(string key, string subKey, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return await this.Database.HashSetAsync(key, subKey, json);
        }

        public async Task<bool> AddSet<T>(string key, T value, double score)
        {
            var json = JsonConvert.SerializeObject(value);
            return await this.Database.SortedSetAddAsync(key, json, score);
        }

        public async Task<T> Get<T>(string key)
        {
            var json = await this.Database.StringGetAsync(key);
            if (json.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }

        public async Task<IEnumerable<T>> GetAll<T>(RedisKey[] keys)
        {
            var items = await this.Database.StringGetAsync(keys);
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

        public async Task<IEnumerable<T>> GetAllHash<T>(string key)
        {
            var items = await this.Database.HashGetAllAsync(key);
            var list = new List<T>();
            foreach (var item in items)
            {
                list.Add(JsonConvert.DeserializeObject<T>(item.Value));
            }

            return list;
        }

        public async Task<IEnumerable<T>> GetAllSet<T>(string key, double start, double end)
        {
            var items = await this.Database.SortedSetRangeByScoreAsync(key, start, end);

            var list = new List<T>();

            foreach (var item in items)
            {
                list.Add(JsonConvert.DeserializeObject<T>(item));
            }

            return list;
        }

        public async Task<T> GetLastSet<T>(string key, double end)
        {
            var item = (await this.Database
                            .SortedSetRangeByScoreAsync(key, double.NegativeInfinity, end, order: Order.Descending, take: 1))
                            .FirstOrDefault();

            if (item.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(item);
            }

            return default(T);
        }
    }
}