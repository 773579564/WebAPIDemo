using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NLog.Web;
using WebApiDemo.Cache;
using FY.Common;

namespace WebApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 2、配置服务
            //Config.Configuration = builder.Configuration;
            GlobalContext.LogWhenStart(builder.Environment);
            GlobalContext.HostingEnvironment = builder.Environment;
            GlobalContext.Configuration = builder.Configuration;
            GlobalContext.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
            GlobalContext.Services = builder.Services;

            //注册redis
            builder.Services.AddRedisCacheSetup();

            builder.Services.AddControllers(o =>
            {
                // 全局异常过滤
                o.Filters.Add(typeof(Filter.GlobalExceptionsFilter));
            }) 
            //MVC全局配置Json序列化处理
            .AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //忽略Model中为null的属性
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //设置本地时间而非UTC时间
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                //添加Enum转string
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });


            #region Swagger
            //if (Config.SwaggerIsUse)
            {
                builder.Services.AddSwaggerGen(options =>
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Demo", Version = "v1" });
                    options.IncludeXmlComments(xmlPath, true);
                });
            }
            #endregion

            //注册：System.Text.Encoding.CodePages；支持其他编码语言：GB2312、GBK
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                //开发人员异常页中间件 , 报告应用运行时错误
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //if (Config.SwaggerIsUse)
            {
                // 添加Swagger有关中间件
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
                });
            }


            //Routing,路由，对 UseEndpoints 规则进行匹配，中间可以加入相关鉴权处理
            app.UseRouting();

            // 先开启认证
            app.UseAuthentication();

            //授权中间件
            app.UseAuthorization();


            //定义路由规则
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=WeatherForecast}/{action=Get}/{id?}");

                endpoints.MapControllers();
            });

            app.Run();

        }
    }
}
