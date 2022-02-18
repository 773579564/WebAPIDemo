using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace WebApiDemo.Controllers
{
    /// <summary>
    /// 测试测试测试
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;


        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// xxxxxx
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<String> Get()
        {
            nlog.WithProperty("filename", "Debug").Log(NLog.LogLevel.Debug, $"测试测试Debug日志");
            nlog.WithProperty("filename", "Info").Log(NLog.LogLevel.Info, $"测试测试Info日志");
            try
            {
                throw new Exception($"测试故意抛出的异常");
            }
            catch (Exception ex)
            {

                nlog.WithProperty("filename", "Error").Log(NLog.LogLevel.Error, ex, $"yilezhu异常的额外信息");
            }
            return "测试的返回信息";
        }

        /// <summary>
        /// 表单提交文件
        /// </summary>
        /// <param name="FaceImage">人脸图片</param>
        /// <param name="FaceDataRecord">人脸数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<String> Post([FromForm] IFormCollection FaceImage, [FromForm] string FaceDataRecord)
        {
            nlog.WithProperty("filename", "Debug").Log(NLog.LogLevel.Debug, $"测试测试Debug日志");
            nlog.WithProperty("filename", "Info").Log(NLog.LogLevel.Info, $"测试测试Info日志");
            try
            {
                string strJson = FaceDataRecord;
                Dictionary<string, string> objStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(strJson);
           
                String filepath = @"G:\demo学习测试\新建文件夹";

                FormFileCollection fileCollection = (FormFileCollection)FaceImage.Files;
                foreach (IFormFile itemfile in fileCollection)
                {
                    StreamReader reader = new StreamReader(itemfile.OpenReadStream());
                    String content = reader.ReadToEnd();
                    String name = itemfile.FileName;

                    String newfilename = Guid.NewGuid().ToString() + name.Substring(name.LastIndexOf("."));
                    String fileFullname = Path.Combine(filepath, newfilename);

                    if (System.IO.File.Exists(fileFullname))
                    {
                        System.IO.File.Delete(fileFullname);
                    }
                    using (FileStream fs = System.IO.File.Create(fileFullname))
                    {
                        // 复制文件                    
                        itemfile.CopyTo(fs);
                        // 清空缓冲区数据                    
                        fs.Flush();
                    }
                }
            }
            catch (Exception ex)
            {

                nlog.WithProperty("filename", "Error").Log(NLog.LogLevel.Error, ex, $"yilezhu异常的额外信息");
            }
            return "测试的返回信息";
        }
    }
}
