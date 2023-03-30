using FY.Common;
using SqlSugar;

namespace FY.Model.Entity.OrganizationManage
{
    [SugarTable("SysPosition")]
    public class PositionEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 职位排序
        /// </summary>
        public int PositionSort { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 职位状态(0禁用 1启用)
        /// </summary>
        public int PositionStatus { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = DefaultValue.DefaultValueString;
    }
}
