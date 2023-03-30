using FY.Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FY.Model.Entity.SystemManage
{
    [Table("SysAutoJob")]
    public class AutoJobEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 任务组名称
        /// </summary>
        public string JobGroupName { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; } = DefaultValue.DefaultValueString; 

        /// <summary>
        /// 任务状态(0禁用 1启用)
        /// </summary>
        public int JobStatus { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// cron表达式
        /// </summary>
        public string CronExpression { get; set; } = DefaultValue.DefaultValueString; 

        /// <summary>
        /// 运行开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 运行结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime? NextStartTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = DefaultValue.DefaultValueString;  
    }
}
