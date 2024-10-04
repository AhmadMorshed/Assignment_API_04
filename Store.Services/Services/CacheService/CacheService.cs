using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Services.Services.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string> GetCacheResponseAsync(string Key)
        {
            var cacheResponse = await _database.StringGetAsync(Key);
            if (cacheResponse.IsNullOrEmpty)
                return null;
            return cacheResponse.ToString();
        }

        public async Task SetCacheResponseAsync(string Key, object response, TimeSpan timeToLive)
        {
            if (response == null)
                return;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serliazedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(Key, serliazedResponse, timeToLive);
        }
    }
}
