using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace WebApiDemo
{
    public static class Config
    {
        public static IConfiguration Configuration;

        public static List<string> IP白名单列表
        {
            get
            {
                if (!string.IsNullOrEmpty(Configuration["AppSettings:IP白名单列表"]))
                {
                    return new List<string>(Configuration["AppSettings:IP白名单列表"].Split(','));
                }

                return new List<string>();
            }
        }
        public static string 日志文件全路径
        {
            get
            {
                if (!string.IsNullOrEmpty(Configuration["AppSettings:日志文件全路径"]))
                {
                    return Configuration["AppSettings:日志文件全路径"];
                }

                return System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Logs");
            }
        }
        /// <summary>
        /// 服务版本
        /// </summary>
        public static string Version
        {
            get
            {
                return Configuration["AppSettings:Version"];
            }
        }

        /// <summary>
        /// 服务发布时间
        /// </summary>
        public static string VersionTime
        {
            get
            {
                return Configuration["AppSettings:VersionTime"];
            }
        }
        
        /// <summary>
        /// 服务主题
        /// </summary>
        public static string WebTitle
        {
            get
            {
                return Configuration["AppSettings:WebTitle"];
            }
        }

        /// <summary>
        /// 跨越访问
        /// </summary>
        public static string SubSystemName
        {
            get
            {
                return Configuration["AppSettings:SubSystemName"];
            }
        }

        #region Swagger 配置
        //是否使用 Swagger
        public static bool SwaggerIsUse
        {
            get
            {
                return Configuration["Swagger:IsUse"] == "true" || Configuration["Swagger:IsUse"] == "1";
            }
        }

        /// <summary>
        /// 访问url 默认未空字符串 全部请求可以访问
        /// </summary>
        public static string SwaggerUrl
        {
            get
            {
                return Configuration["Swagger:Url"];
            }
        }
        #endregion

        #region JWT 配置
        public static String JWTIssuer
        {
            get
            {
                return Configuration["JWT:Issuer"];
            }
        }

        public static String JWTAudience
        {
            get
            {
                return Configuration["JWT:Audience"] ;
            }
        }

        public static String JWTSecurityKey
        {
            get
            {
                return Configuration["JWT:SecurityKey"];
            }
        }
        #endregion
    }
}
