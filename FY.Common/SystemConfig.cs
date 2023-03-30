using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FY.Common
{
    public class SystemConfig
    {
        public SystemConfig()
        {
            Demo = false;
            Debug = false;
            LoginMultiple = false;

        }

        /// <summary>
        /// 是否是Demo模式
        /// </summary>
        public bool Demo { get; set; }

        /// <summary>
        /// 是否是调试模式
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// 允许一个用户在多个电脑同时登录
        /// </summary>
        public bool LoginMultiple { get; set; }

        public string LoginProvider { get; set; }

        /// <summary>
        /// 雪花算法ID
        /// Snow Flake Worker Id
        /// </summary>
        public int SnowFlakeWorkerId { get; set; }

        /// <summary>
        /// api地址
        /// </summary>
        public string ApiSite { get; set; }

        /// <summary>
        /// 允许跨域的站点
        /// </summary>
        public string AllowCorsSite { get; set; }

        /// <summary>
        /// 网站虚拟目录
        /// </summary>
        public string VirtualDirectory { get; set; }

        public string DBProvider { get; set; }

        public string DBConnectionString { get; set; }


        public string CacheProvider { get; set; }

        public RedisConnection Redis { get; set; }
    }

    public class RedisConnection
    {
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// 连接IP：端口
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 默认redis数据库
        /// </summary>
        public int DefaultDB { get; set; } = 0;
    }

}
