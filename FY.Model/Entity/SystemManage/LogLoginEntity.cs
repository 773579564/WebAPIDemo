using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    [SugarTable("SysLogLogin")]
    public class LogLoginEntity : BaseCreateEntity
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
        /// 浏览器
        /// </summary>
        public string Browser { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 操作系统
        /// </summary>
        public string OS { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 额外备注
        /// </summary>
        public string ExtraRemark { get; set; } = Common.DefaultValue.DefaultValueString;

        [SugarColumn(IsIgnore = true)]
        public string UserName { get; set; } = Common.DefaultValue.DefaultValueString;
    }
}
