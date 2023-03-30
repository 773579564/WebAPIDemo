using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    [SugarTable("SysMenuAuthorize")]
    public class MenuAuthorizeEntity : BaseCreateEntity
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; } = Common.DefaultValue.DefaultValueLong;

        /// <summary>
        /// 授权Id(角色Id或者用户Id)
        /// </summary>
        public long AuthorizeId { get; set; } = Common.DefaultValue.DefaultValueLong;

        /// <summary>
        /// 授权类型(1角色 2用户)
        /// </summary>
        public int AuthorizeType { get; set; } = Common.DefaultValue.DefaultValueInt;

        [SugarColumn(IsIgnore = true)]
        public string AuthorizeIds { get; set; } = Common.DefaultValue.DefaultValueString;
    }
}
