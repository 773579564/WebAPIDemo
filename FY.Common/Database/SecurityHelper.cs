using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FY.Common.Database
{
    public class SecurityHelper
    {
        /// <summary>
        /// 获取Guid
        /// </summary>
        /// <param name="replaceDash"></param>
        /// <returns></returns>
        public static string GetGuid(bool replaceDash = false)
        {
            string guid = Guid.NewGuid().ToString();
            if (replaceDash)
            {
                guid = guid.Replace("-", string.Empty);
            }
            return guid;
        }

        /// <summary>
        /// 数据库参数注入校验
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSafeSqlParam(string value)
        {
            return !Regex.IsMatch(value, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
    }
}
