using System.Threading.Tasks;
using System;

namespace WebApiDemo.Cache
{
    public interface ICacheAsync
    {
        //获取 Reids 缓存值
        Task<string> GetValueAsync(string key);

        //获取值，并序列化
        Task<TEntity> GetAsync<TEntity>(string key);

        //保存
        Task SetAsync(string key, object value, TimeSpan? cacheTime);

        //判断是否存在
        Task<bool> ExistAsync(string key);

        //移除某一个缓存值
        Task RemoveAsync(string key);
    }
}
