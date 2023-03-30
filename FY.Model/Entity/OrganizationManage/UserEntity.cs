using FY.Common;
using SqlSugar;

namespace FY.Model.Entity.OrganizationManage
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("SysUser")]
    public class UserEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 密码盐值
        /// </summary>
        public string Salt { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 性别(0未知 1男 2女)
        /// </summary>
        public int Gender { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 头像
        /// </summary>
        public string Portrait { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 用户状态(0禁用 1启用)
        /// </summary>
        public int UserStatus { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 系统用户(0不是 1是[系统用户拥有所有的权限])
        /// </summary>
        public int IsSystem { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 在线(0不是 1是)
        /// </summary>
        public int IsOnline { get; set; } = DefaultValue.DefaultValueInt;

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime? FirstVisit { get; set; }

        /// <summary>
        /// 上一次登录时间
        /// </summary>
        public DateTime? PreviousVisit { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastVisit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 后台Token
        /// </summary>
        public string WebToken { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// ApiToken
        /// </summary>
        public string ApiToken { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 所属部门Id
        /// </summary>
        public long DepartmentId { get; set; } = DefaultValue.DefaultValueLong;

        /// <summary>
        /// 所属部门名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string DepartmentName { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 岗位Id
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string PositionIds { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string RoleIds { get; set; } = DefaultValue.DefaultValueString;
    }
}
