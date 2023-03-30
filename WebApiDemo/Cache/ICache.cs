using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace WebApiDemo.Cache
{
    public interface ICache
    {
        //获取 Reids 缓存值
        string GetValue(string key);

        //获取值，并序列化
        TEntity Get<TEntity>(string key);

        //保存
        void Set(string key, object value, TimeSpan? cacheTime);

        //判断是否存在
        bool Exist(string key);

        //移除某一个缓存值
        void Remove(string key);

        #region Hash

        #endregion
    }
}
