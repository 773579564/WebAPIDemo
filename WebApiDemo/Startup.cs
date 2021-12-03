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
        /// ��ʼ�����󣬴����Ӧ�ӿڲ�����
        /// IHostingEnvironment �ṩ������Ϣ�� service
        /// IConfiguration �ṩ��ȡconfig.��ʾһ���/ֵӦ�ó�����������
        /// ILoggerFactory ����log����.
        /// IApplicationBuilder����һ�������뵱ǰ������ص����Ժͷ����Ľӿڡ������ڻ�ȡӦ�ó����еĻ���������
        /// </summary>
        /// <param name="configuration">�ṩ��ȡconfig.</param>
        public Startup(IConfiguration configuration)
        {
            Config.Configuration = configuration;
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// �����е�ʱ�򱻵���, Ȼ����IOC��������ע��service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //֧��Newtonsoft.Json��֧���������� Json��ʽ������
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                //��ֵ����
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                //���������һ������ 
                //options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            //֧�� �û���ݱ�ʶ���������
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            //���ύ����
            services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
            {
                //options.ValueCountLimit = Int32.MaxValue;
                //������ֵ�ĳ�������
                options.ValueLengthLimit = Int32.MaxValue;
                //�������ĳ�������
                options.KeyLengthLimit = Int32.MaxValue;
                //options.MultipartBodyLengthLimit = Int32.MaxValue;
                //options.MultipartBoundaryLengthLimit = Int32.MaxValue;
            });

            // ����ʵ�ʵ�ҵ������������С�߳���
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

            //ע�᣺System.Text.Encoding.CodePages��֧�������������ԣ�GB2312��GBK
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// ͨ��Autofac��չ����ע�Ṧ�ܣ���ConfigureServices֮��ִ��
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine(env.IsDevelopment() ? "����������" : "��ʽ������");
            if (env.IsDevelopment())
            {
                //������Ա�쳣ҳ�м�� , ����Ӧ������ʱ����
                app.UseDeveloperExceptionPage();
            }

            //HTTPS �ض����м��, �� HTTP �����ض��� HTTPS
            //app.UseHttpsRedirection();

            //����·�������·���м�� 
            app.UseRouting();

            //������Ȩ�û����ʰ�ȫ��Դ����Ȩ�м��
            app.UseAuthorization();

            #region Swagger
            if (Config.SwaggerIsUse)
            {
                // ���Swagger�й��м��
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
                });
            }
            #endregion

            //���ڽ� Razor Pages �ս����ӵ�����ܵ����ս��·���м��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
