using FY.Common;
using SqlSugar;

namespace FY.Model.Entity.OrganizationManage
{
    /// <summary>
    /// 用户所属表
    /// </summary>
    [SugarTable("SysUserBelong")]
    public class UserBelongEntity : BaseCreateEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; } = DefaultValue.DefaultValueLong;

        /// <summary>
        /// 职位Id或者角色Id
        /// </summary>
        public long BelongId { get; set; } = DefaultValue.DefaultValueLong;

        /// <summary>
        /// 所属类型(1职位 2角色)
        /// </summary>
        public int BelongType { get; set; } = DefaultValue.DefaultValueInt;
    }
}
