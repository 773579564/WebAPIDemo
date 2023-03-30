using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    [SugarTable("SysLogOperate")]
    public class LogOperateEntity : BaseCreateEntity
    {
        /// <summary>
        /// 执行状态(0失败 1成功)
        /// </summary>
        public int LogStatus { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// ip地址
        /// </summary>
        public string IpAddress { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// ip位置
        /// </summary>
        public string IpLocation { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 日志类型(暂未用到)
        /// </summary>
        public string LogType { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 业务类型(暂未用到)
        /// </summary>
        public string BusinessType { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 页面地址
        /// </summary>
        public string ExecuteUrl { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 请求参数
        /// </summary>
        public string ExecuteParam { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 请求结果
        /// </summary>
        public string ExecuteResult { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 执行时间
        /// </summary>
        public int ExecuteTime { get; set; } = Common.DefaultValue.DefaultValueInt;

        [SugarColumn(IsIgnore = true)]
        public string UserName { get; set; } = Common.DefaultValue.DefaultValueString;

        [SugarColumn(IsIgnore = true)]
        public string DepartmentName { get; set; } = Common.DefaultValue.DefaultValueString;

    }
}
