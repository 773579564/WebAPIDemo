using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    /// <summary>
    /// 字典数据表
    /// </summary>
    [SugarTable("SysDataDictDetail")]
    public class DataDictDetailEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 字典类型(外键)
        /// </summary>
        public string DictType { get; set; } = Common.DefaultValue.DefaultValueString; 

        /// <summary>
        /// 字典排序
        /// </summary>
        public int DictSort { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 字典键(一般从1开始)
        /// </summary>
        public int DictKey { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 字典值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 显示样式(default primary success info warning danger)
        /// </summary>
        public string ListClass { get; set; } = Common.DefaultValue.DefaultValueString; 

        /// <summary>
        /// 字典状态(0禁用 1启用)
        /// </summary>
        public int DictStatus { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 默认选中(0不是 1是)
        /// </summary>
        public int IsDefault { get; set; } = Common.DefaultValue.DefaultValueInt;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; } = Common.DefaultValue.DefaultValueString; 
    }
}
