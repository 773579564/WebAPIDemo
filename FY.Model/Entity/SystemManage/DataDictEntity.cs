using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FY.Model.Entity.SystemManage
{
    /// <summary>
    /// 字典类型表
    /// </summary>
    [Table("SysDataDict")]
    public class DataDictEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = Common.DefaultValue.DefaultValueString; 

        /// <summary>
        /// 字典排序
        /// </summary>
        public int DictSort { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = Common.DefaultValue.DefaultValueString; 
    }
}
