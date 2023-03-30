using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FY.Common.Helper;
using FY.Common.Extensions;

namespace FY.Common
{
    /// <summary>
    /// 程序启动配置
    /// </summary>
    public class GlobalContext
    {
        public static IServiceCollection Services { get; set; }

        /// <summary>
        /// Configured service provider.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        public static IConfiguration Configuration { get; set; }

        public static IWebHostEnvironment HostingEnvironment { get; set; }

        public static SystemConfig SystemConfig { get; set; }

        public static string GetVersion()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            return version.Major + "." + version.Minor;
        }

        /// <summary>
        /// 程序启动时，记录目录
        /// </summary>
        /// <param name="env"></param>
        public static void LogWhenStart(IWebHostEnvironment env)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("程序启动");
            sb.AppendLine("----ContentRootPath:" + env.ContentRootPath);
            sb.AppendLine("----WebRootPath:" + env.WebRootPath);
            sb.AppendLine("----IsDevelopment:" + env.IsDevelopment());
            if (env.IsDevelopment())
            {
                sb.Append($"({"开发环境"})");
            }
            else
            {
                sb.Append($"({"运行环境"})");
            }
            LogHelper.Debug(sb.ToString());
            ConsoleWriteExtensions.WriteSuccessLine(sb.ToString());
        }

    }
}
