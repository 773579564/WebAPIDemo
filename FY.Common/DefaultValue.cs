using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FY.Common
{
    /// <summary>
    /// 设置数据库模板默认值
    /// </summary>
    public static class DefaultValue
    {
        /// <summary>
        /// 字符串默认值
        /// </summary>
        public const string DefaultValueString = "";

        /// <summary>
        /// long默认值
        /// </summary>
        public const long DefaultValueLong = 0L;

        /// <summary>
        /// int 默认值
        /// </summary>
        public const int DefaultValueInt = 0;

        /// <summary>
        /// datetime 时间默认值
        /// </summary>
        public static readonly DateTime DefaultValueDateTime = DateTime.MinValue;


        /// <summary>
        /// bool类型默认值false
        /// </summary>
        public const bool DefaultValueBoolFalse = false;

        /// <summary>
        /// bool类型默认值true
        /// </summary>
        public const bool DefaultValueBoolTrue = true;
    }
}
