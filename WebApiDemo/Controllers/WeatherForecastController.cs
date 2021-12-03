using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
