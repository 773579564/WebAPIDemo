using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FY.Common;
using FY.Common.Helper;

namespace WebApiDemo.Cache
{
    public class RedisBasketRepository : ICache, ICacheAsync
    {
        private readonly ILogger<RedisBasketRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisBasketRepository(ILogger<RedisBasketRepository> logger, ConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = redis.GetDatabase(GlobalContext.SystemConfig.Redis.DefaultDB);
        }



        public async Task<bool> ExistAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan? cacheTime)
        {
            if (value != null)
            {
                if (value is string cacheValue)
                {
                    // 字符串无需序列化
                    await _database.StringSetAsync(key, cacheValue, cacheTime);
                }
                else
                {
                    //序列化，将object值生成RedisValue
                    await _database.StringSetAsync(key, SerializeHelper.Serialize(value), cacheTime);
                }
            }
        }

        public async Task<TEntity> GetAsync<TEntity>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return SerializeHelper.Deserialize<TEntity>(value);
            }
            else
            {
                return default(TEntity);
            }
        }

        public string GetValue(string key)
        {
            return _database.StringGet(key);
        }

        public TEntity Get<TEntity>(string key)
        {
            var value = _database.StringGet(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return SerializeHelper.Deserialize<TEntity>(value);
            }
            else
            {
                return default(TEntity);
            }
        }

        public void Set(string key, object value, TimeSpan? cacheTime)
        {
            if (value != null)
            {
                if (value is string cacheValue)
                {
                    // 字符串无需序列化
                    _database.StringSet(key, cacheValue, cacheTime);
                }
                else
                {
                    //序列化，将object值生成RedisValue
                    _database.StringSet(key, SerializeHelper.Serialize(value), cacheTime);
                }
            }
        }

        public bool Exist(string key)
        {
            return _database.KeyExists(key);
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
