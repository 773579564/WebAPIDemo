using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FY.Common.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="isUseTextJson">是否使用textjson</param>
        /// <returns>返回json字符串</returns>
        public static string ObjToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// json反序列化obj
        /// </summary>
        /// <typeparam name="T">反序列类型</typeparam>
        /// <param name="strJson">json</param>
        /// <param name="isUseTextJson">是否使用textjson</param>
        /// <returns>返回对象</returns>
        public static T JsonToObj<T>(string strJson)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strJson);
        }

        /// <summary>
        /// 转换对象为JSON格式数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>字符格式的JSON数据</returns>
        public static string GetJSON<T>(object obj)
        {
            string result = String.Empty;
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
