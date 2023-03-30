using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    [SugarTable("SysAutoJobLog")]
    public class AutoJobLogEntity : BaseCreateEntity
    {
        /// <summary>
        /// 任务组名称
        /// </summary>
        /// <returns></returns>
        public string JobGroupName { get; set; } = Common.DefaultValue.DefaultValueString; 
        /// <summary>
        /// 任务名称
        /// </summary>
        /// <returns></returns>
        public string JobName { get; set; } = Common.DefaultValue.DefaultValueString;
        /// <summary>
        /// 执行状态(0失败 1成功)
        /// </summary>
        /// <returns></returns>
        public int LogStatus { get; set; } = Common.DefaultValue.DefaultValueInt;
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; } = Common.DefaultValue.DefaultValueString;  
    }
}
