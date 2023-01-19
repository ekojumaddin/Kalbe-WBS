using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Library.Interface;

namespace WBSBE.Common.Library
{
    public class CacheRedis
    {
        private static ICacheService _cacheService;

        public static void ConfigureCache(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
    }

    public class ClsRedisCacheServices : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public ClsRedisCacheServices(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public Task<string> GetCacheValueAsync(string key, int db = -1)
        {
            var redis = _connectionMultiplexer.GetDatabase(db);
            return Task.FromResult(redis.StringGet(key).ToString());
        }

        public void SetChangeValueAsync(string key, string value, TimeSpan? timeSpan = null, int db = -1)
        {
            var redis = _connectionMultiplexer.GetDatabase(db);
            redis.StringSet(key, value, timeSpan);
        }

        public void DeleteCacheRedis(string key, int db = -1)
        {
            var redis = _connectionMultiplexer.GetDatabase(db);
            redis.KeyDelete(key);
        }

        public bool KeyExists(string key, int db = -1)
        {
            var redis = _connectionMultiplexer.GetDatabase(db);
            return redis.KeyExists(key);
        }

        public void SetKeyExpire(string key, DateTime? expiry, int db = -1)
        {
            var redis = _connectionMultiplexer.GetDatabase(db);
            redis.KeyExpire(key, expiry);
        }

        public void SetKeyExpire(string key, TimeSpan? expiry, int db = -1)
        {
            var redis = _connectionMultiplexer.GetDatabase(db);
            redis.KeyExpire(key, expiry);
        }
    }
}
