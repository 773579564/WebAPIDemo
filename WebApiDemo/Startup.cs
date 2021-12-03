using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace WebApiDemo
{
    public class Startup
    {
        /// <summary>
        /// 初始化对象，传入对应接口参数：
        /// IHostingEnvironment 提供环境信息的 service
        /// IConfiguration 提供读取config.表示一组键/值应用程序配置属性
        /// ILoggerFactory 创建log对象.
        /// IApplicationBuilder：是一个包含与当前环境相关的属性和方法的接口。它用于获取应用程序中的环境变量。
        /// </summary>
        /// <param name="configuration">提供读取config.</param>
        public Startup(IConfiguration configuration)
        {
            Config.Configuration = configuration;
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// 在运行的时候被调用, 然后向IOC的容器中注入service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //支持Newtonsoft.Json；支持输入和输出 Json格式化程序
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                //空值处理
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                //解决命名不一致问题 
                //options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            //支持 用户身份标识，请求对象
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            //表单提交限制
            services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
            {
                //options.ValueCountLimit = Int32.MaxValue;
                //单个表单值的长度限制
                options.ValueLengthLimit = Int32.MaxValue;
                //单个键的长度限制
                options.KeyLengthLimit = Int32.MaxValue;
                //options.MultipartBodyLengthLimit = Int32.MaxValue;
                //options.MultipartBoundaryLengthLimit = Int32.MaxValue;
            });

            // 根据实际的业务量来设置最小线程数
            System.Threading.ThreadPool.SetMinThreads(10, 10);

            #region Swagger
            if (Config.SwaggerIsUse)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Demo", Version = "v1" });
                    
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                });
            }
            #endregion

            //注册：System.Text.Encoding.CodePages；支持其他编码语言：GB2312、GBK
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 通过Autofac扩展服务注册功能，在ConfigureServices之后执行
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine(env.IsDevelopment() ? "开发环境！" : "正式环境！");
            if (env.IsDevelopment())
            {
                //开发人员异常页中间件 , 报告应用运行时错误
                app.UseDeveloperExceptionPage();
            }

            //HTTPS 重定向中间件, 将 HTTP 请求重定向到 HTTPS
            //app.UseHttpsRedirection();

            //用于路由请求的路由中间件 
            app.UseRouting();

            //用于授权用户访问安全资源的授权中间件
            app.UseAuthorization();

            #region Swagger
            if (Config.SwaggerIsUse)
            {
                // 添加Swagger有关中间件
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
                });
            }
            #endregion

            //用于将 Razor Pages 终结点添加到请求管道的终结点路由中间件
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
