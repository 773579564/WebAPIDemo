using FY.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace WebApiDemo.Cache
{
    /// <summary>
    /// Redis缓存 启动服务
    /// </summary>
    public static class RedisCacheSetup
    {
        public static void AddRedisCacheSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (!GlobalContext.SystemConfig.Redis.Enabled)
            {
                services.AddTransient<ICache, RedisBasketRepository>();
                services.AddTransient<ICacheAsync, RedisBasketRepository>();

                // 配置启动Redis服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
                services.AddSingleton<ConnectionMultiplexer>(sp =>
                {
                    var configuration = ConfigurationOptions.Parse(GlobalContext.SystemConfig.Redis.Connection, true);

                    configuration.ResolveDns = true;

                    return ConnectionMultiplexer.Connect(configuration);
                });

            }


        }
    }
}
