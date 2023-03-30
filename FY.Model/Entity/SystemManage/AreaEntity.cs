using FY.Common;
using SqlSugar;

namespace FY.Model.Entity.SystemManage
{
    /// <summary>
    /// 中国省市县表
    /// </summary>
    [SugarTable("SysArea")]
    public class AreaEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 地区编码
        /// </summary>
        public string AreaCode { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 父地区编码
        /// </summary>
        public string ParentAreaCode { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 地区名称
        /// </summary>
        public string AreaName { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; } = DefaultValue.DefaultValueString;

        /// <summary>
        /// 地区层级(1省份 2城市 3区县)
        /// </summary>
        public int AreaLevel { get; set; } = DefaultValue.DefaultValueInt;
    }

    /// <summary>
    /// 此类给其他需要省市县的业务表继承
    /// </summary>
    public class BaseAreaEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 省份ID
        /// </summary>
        public long ProvinceId { get; set; } = DefaultValue.DefaultValueLong;

        /// <summary>
        /// 城市ID
        /// </summary>
        public long CityId { get; set; } = DefaultValue.DefaultValueLong;

        /// <summary>
        /// 区域ID
        /// </summary>
        public long CountyId { get; set; } = DefaultValue.DefaultValueLong;

        [SugarColumn(IsIgnore = true)]
        public string ProvinceName { get; set; } = DefaultValue.DefaultValueString;

        [SugarColumn(IsIgnore = true)]
        public string CityName { get; set; } = DefaultValue.DefaultValueString; 

        [SugarColumn(IsIgnore = true)]
        public string CountryName { get; set; } = DefaultValue.DefaultValueString;

        [SugarColumn(IsIgnore = true)]
        public string AreaId { get; set; } = DefaultValue.DefaultValueString;
    }
}
