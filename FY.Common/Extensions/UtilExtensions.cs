using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace FY.Common.Extensions
{
    public static class UtilExtensions
    {

        /// <summary>
        /// 对象转int值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue, int errorValue = 0)
        {
            int reval;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }

            return errorValue;
        }


        /// <summary>
        /// 返回long类型值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static long ObjToLong(this object thisValue, long errorValue = 0L)
        {
            long reval;
            if (thisValue != null && thisValue != DBNull.Value && long.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }

            return errorValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ObjToDouble(this object thisValue, double errorValue = 0.0)
        {
            double reval;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }

            return errorValue;
        }


        /// <summary>
        /// 判断是否为空、undefined、null
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this object thisValue)
        {
            switch (thisValue.ObjToString())
            {
                case "":
                case "undefined":
                case "null":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 对象转字符串，并去掉前后空格
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue, string errorValue = "")
        {
            if (thisValue != null)
            {
                return thisValue.ToString().Trim();
            }
            return errorValue;
        }

        /// <summary>
        /// 判断是否为空/空字符串
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object thisValue)
        {
            return thisValue == null || thisValue == DBNull.Value || string.IsNullOrWhiteSpace(thisValue.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static decimal ObjToDecimal(this object thisValue, decimal errorValue = 0)
        {
            decimal reval;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }

            return errorValue;
        }

        /// <summary>
        /// 字符串转时间DateTime
        /// 转换失败返回 DateTime.MinValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDateTime(this string thisValue)
        {
            return thisValue.ObjToDateTime(DateTime.MinValue);
        }

        /// <summary>
        /// 字符串转时间DateTime
        /// 转换失败返回 传入默认值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDateTime(this string thisValue, DateTime errorValue)
        {
            if (string.IsNullOrWhiteSpace(thisValue))
            {
                return errorValue;
            }

            try
            {
                if (thisValue.Contains("-") || thisValue.Contains("/"))
                {
                    return DateTime.Parse(thisValue);
                }

                switch (thisValue.Length)
                {
                    case 4:
                        return DateTime.ParseExact(thisValue, "yyyy", CultureInfo.CurrentCulture);
                    case 6:
                        return DateTime.ParseExact(thisValue, "yyyyMM", CultureInfo.CurrentCulture);
                    case 8:
                        return DateTime.ParseExact(thisValue, "yyyyMMdd", CultureInfo.CurrentCulture);
                    case 10:
                        return DateTime.ParseExact(thisValue, "yyyyMMddHH", CultureInfo.CurrentCulture);
                    case 12:
                        return DateTime.ParseExact(thisValue, "yyyyMMddHHmm", CultureInfo.CurrentCulture);
                    case 14:
                        return DateTime.ParseExact(thisValue, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                    default:
                        return DateTime.Parse(thisValue);
                }
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// 对象转bool类型：
        /// --  1，是，true的 返回 true
        /// --  0， false，否 的 返回false
        /// --  其他返回默认值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object thisValue, bool errorValue = false)
        {
            string strValue = thisValue.ObjToString().ToLower();
            switch (strValue)
            {
                case "1":
                case "true":
                case "是":
                    return true;
                case "0":
                case "false":
                case "否":
                    return false;
            }

            return errorValue;
        }


        /// <summary>
        /// 获取毫秒时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTimestampByMilliseconds(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeMilliseconds();
        }

        /// <summary>
        ///  时间戳转本地时间-时间戳精确到毫秒
        /// </summary> 
        public static DateTime ToLocalTimeDateByMilliseconds(this long unix)
        {
            var dto = DateTimeOffset.FromUnixTimeMilliseconds(unix);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///  时间戳转本地时间-时间戳精确到秒
        /// </summary> 
        public static DateTime ToLocalTimeDateBySeconds(this long unix)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///  时间转时间戳Unix-时间戳精确到秒
        /// </summary> 
        public static long ToUnixTimestampBySeconds(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取最初的异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }

        #region 强制转换类型
        /// <summary>
        /// 强制转换类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> CastSuper<TResult>(this IEnumerable source)
        {
            foreach (object item in source)
            {
                yield return (TResult)Convert.ChangeType(item, typeof(TResult));
            }
        }
        #endregion
    }
}
