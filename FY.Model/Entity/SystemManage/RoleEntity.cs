using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    [SugarTable("SysRole")]
    public class RoleEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 角色排序
        /// </summary>
        public int RoleSort { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 角色状态(0禁用 1启用)
        /// </summary>
        public int RoleStatus { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = Common.DefaultValue.DefaultValueString;

        /// 角色对应的菜单，页面和按钮
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string MenuIds { get; set; } = Common.DefaultValue.DefaultValueString;

    }
}
