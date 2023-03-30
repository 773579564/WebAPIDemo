using FY.Common;
using Newtonsoft.Json;using SqlSugar;

namespace FY.Model.Entity.OrganizationManage
{
    /// <summary>
    /// 部门表
    /// </summary>
    [SugarTable("SysDepartment")]
    public class DepartmentEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 父部门Id(0表示是根部门)
        /// </summary>
        public long ParentId { get; set; } = DefaultValue.DefaultValueLong;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 部门电话
        /// </summary>
        public string Telephone { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 部门传真
        /// </summary>
        public string Fax { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 部门Email
        /// </summary>
        public string Email { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 部门负责人Id
        /// </summary>
        public long PrincipalId { get; set; } = DefaultValue.DefaultValueLong;

        /// <summary>
        /// 部门排序
        /// </summary>
        public int DepartmentSort { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 负责人名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string PrincipalName { get; set; } = DefaultValue.DefaultValueString;
    }
}
