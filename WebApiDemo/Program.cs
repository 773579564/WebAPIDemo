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

            // 2�����÷���
            //Config.Configuration = builder.Configuration;
            GlobalContext.LogWhenStart(builder.Environment);
            GlobalContext.HostingEnvironment = builder.Environment;
            GlobalContext.Configuration = builder.Configuration;
            GlobalContext.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
            GlobalContext.Services = builder.Services;

            //ע��redis
            builder.Services.AddRedisCacheSetup();

            builder.Services.AddControllers(o =>
            {
                // ȫ���쳣����
                o.Filters.Add(typeof(Filter.GlobalExceptionsFilter));
            }) 
            //MVCȫ������Json���л�����
            .AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //����Model��Ϊnull������
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //���ñ���ʱ�����UTCʱ��
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                //���Enumתstring
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

            //ע�᣺System.Text.Encoding.CodePages��֧�������������ԣ�GB2312��GBK
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                //������Ա�쳣ҳ�м�� , ����Ӧ������ʱ����
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //if (Config.SwaggerIsUse)
            {
                // ���Swagger�й��м��
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
                });
            }


            //Routing,·�ɣ��� UseEndpoints �������ƥ�䣬�м���Լ�����ؼ�Ȩ����
            app.UseRouting();

            // �ȿ�����֤
            app.UseAuthentication();

            //��Ȩ�м��
            app.UseAuthorization();


            //����·�ɹ���
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
