﻿using System;
using FY.Common;
using FY.Common.Helper;
using FY.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApiDemo.Filter
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {

        private readonly ILogger<GlobalExceptionsFilter> _loggerHelper;

        public GlobalExceptionsFilter(ILogger<GlobalExceptionsFilter> loggerHelper)
        {
            _loggerHelper = loggerHelper;
        }
        public void OnException(ExceptionContext context)
        {
            var json = new MessageModel<string>();

            json.msg = context.Exception.Message;//错误信息
            json.status = 500;//500异常 
            var errorAudit = "Unable to resolve service for";
            if (!string.IsNullOrEmpty(json.msg) && json.msg.Contains(errorAudit))
            {
                json.msg = json.msg.Replace(errorAudit, $"（若新添加服务，需要重新编译项目）{errorAudit}");
            }

            if (GlobalContext.HostingEnvironment.EnvironmentName.ToString().Equals("Development"))
            {
                json.msgDev = context.Exception.StackTrace;//堆栈信息
            }
            var res = new ContentResult();
            res.Content = JsonHelper.GetJSON<MessageModel<string>>(json);

            context.Result = res;

            //采用log4net 进行错误日志记录
            _loggerHelper.LogError(json.msg + WriteLog(json.msg, context.Exception));
        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }


}
