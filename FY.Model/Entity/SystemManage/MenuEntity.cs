using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    [SugarTable("SysMenu")]
    public class MenuEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 父菜单Id(0表示是根菜单)
        /// </summary>
        public long ParentId { get; set; } = Common.DefaultValue.DefaultValueLong;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 菜单Url
        /// </summary>
        public string MenuUrl { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 链接打开方式
        /// </summary>
        public string MenuTarget { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int MenuSort { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 菜单类型(1目录 2页面 3按钮)
        /// </summary>
        public int MenuType { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 菜单状态(0禁用 1启用)
        /// </summary>
        public int MenuStatus { get; set; } = Common.DefaultValue.DefaultValueInt;

        /// <summary>
        /// 菜单权限标识
        /// </summary>
        public string Authorize { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = Common.DefaultValue.DefaultValueString;

        /// <summary>
        /// 父菜单名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ParentName { get; set; } = Common.DefaultValue.DefaultValueString;
    }
}
