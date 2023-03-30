using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FY.Common
{
    public class XmlHelper
    {
        /// <summary>
        /// 获取节点属性的值，不存在不赋值，设置到传入 ref 地址，不存在节点属性不赋值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="strAttr">属性名</param>
        /// <param name="strNameValue">要设置的地址</param>
        public static void GetValueByXmlAttr(XmlNode node, string strAttr, ref string strNameValue)
        {
            if (node != null)
            {
                XmlAttribute xmlAttr = node.Attributes[strAttr];
                if (xmlAttr != null)
                {
                    strNameValue = xmlAttr.Value;
                }
            }
        }

        /// <summary>
        /// 获取节点属性的值，不存在返回空字符串
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="strAttr">属性名</param>
        /// <param name="strNameValue">要设置的地址</param>
        public static string GetValueByXmlAttr(XmlNode node, string strAttr)
        {
            if (node != null)
            {
                XmlAttribute xmlAttr = node.Attributes[strAttr];
                if (xmlAttr != null)
                {
                    string setValue = xmlAttr.Value;
                    return setValue;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取对应节点的值，设置到传入 ref 地址，不存在节点不赋值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="str节点名称"></param>
        /// <param name="strNameValue"></param>
        public static void GetValueByXmlNode(XmlNode node, string str节点名称, ref string strNameValue)
        {
            if (node != null)
            {
                XmlNode n = node.SelectSingleNode(str节点名称);
                if (n != null)
                {
                    strNameValue = n.InnerText;
                }
            }
        }


        /// <summary>
        /// 获取对应节点的值，不存在返回空字符串
        /// </summary>
        /// <param name="node"></param>
        /// <param name="str节点名称"></param>
        /// <param name="strNameValue"></param>
        public static string GetValueByXmlNode(XmlNode node, string str节点名称)
        {
            if (node != null)
            {
                XmlNode n = node.SelectSingleNode(str节点名称);
                if (n != null)
                {
                    return n.InnerText;
                }
            }
            return "";
        }


        /// <summary>
        /// 设置节点属性的值，不存在不赋值，设置到传入 ref 地址，不存在节点属性不赋值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="strAttr">属性名</param>
        /// <param name="strNameValue">要设置的地址</param>
        public static void SetValueByXmlAttr(XmlNode node, string strAttr, string strNameValue)
        {
            if (node != null)
            {
                XmlElement nodeElement = (XmlElement)node;
                nodeElement.SetAttribute(strAttr, strNameValue);
            }
        }

        /// <summary>
        /// 获取对应节点的值，设置到传入 ref 地址，不存在节点不赋值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="str节点名称"></param>
        /// <param name="strNameValue"></param>
        public static void SetValueByXmlNode(XmlNode node, string str节点名称, string strNameValue)
        {
            if (node != null)
            {
                XmlNode n = node.SelectSingleNode(str节点名称);
                if (n != null)
                {
                    n.InnerText = strNameValue;
                }
            }
        }

        #region xml写入
        /// <summary>
        /// 将字符串转为xml可写入字符串
        /// </summary>
        public static string SanitizeXmlString(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return xml;
            }

            var buffer = new StringBuilder(xml.Length);

            int xmllen1 = xml.Length - 1;    //减一，防止计算增补规则是超出数组
            int index = 0;
            for (index = 0; index < xmllen1; index++)
            {
                if (IsLegalXmlChar(xml[index]))
                {
                    buffer.Append(xml[index]);
                }
                else if (IsLegalHighXmlChar(xml[index])) //增补字符编码   高代理 范围（0xD800 至 0xDBFF）
                {
                    if (IsLegalLowXmlChar(xml[index + 1]))  //增补字符编码   低代理 范围（0xDC00 至 0xDFFF）
                    {
                        buffer.Append(xml[index]);
                        buffer.Append(xml[index + 1]);
                    }
                    else if (IsLegalXmlChar(xml[index]))
                    {
                        buffer.Append(xml[index + 1]);  //不是低代理增补字符的判断
                    }
                    index++;
                }
            }

            if (index == xmllen1)
            {
                if (IsLegalXmlChar(xml[index]))
                {
                    buffer.Append(xml[index]);
                }
            }

            return buffer.ToString();
        }

        // <summary>
        /// 判断是否xml可写入字符
        /// </summary>
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '/t' == 9   */        ||
                 character == 0xA /* == '/n' == 10  */        ||
                 character == 0xD /* == '/r' == 13  */        ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD)
            );
        }

        /// <summary>
        /// 判断字符是否在代理范围
        /// 第一个单元来自于高代理（high surrogate）范围（0xD800 至 0xDBFF），
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsLegalHighXmlChar(int character)
        {
            //先判断是否在代理范围（surrogate blocks）
            //增补字符编码为两个代码单元，
            //第一个单元来自于高代理（high surrogate）范围（0xD800 至 0xDBFF），
            //第二个单元来自于低代理（low surrogate）范围（0xDC00 至 0xDFFF）。
            //unicode说明定义的算法 计算出增补字符范围0x10000 至 0x10FFFF
            //即若result是增补字符集，应该在0x10000到0x10FFFF之间，result = (high - 0xD800) * 0x400 + (low - 0xDC00) + 0x10000;
            return character >= 0xD800 && character <= 0xDBFF;
        }

        /// <summary>
        /// 判断字符是否在代理范围
        /// 第二个单元来自于低代理（low surrogate）范围（0xDC00 至 0xDFFF）。
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsLegalLowXmlChar(int character)
        {
            //先判断是否在代理范围（surrogate blocks）
            //增补字符编码为两个代码单元，
            //第一个单元来自于高代理（high surrogate）范围（0xD800 至 0xDBFF），
            //第二个单元来自于低代理（low surrogate）范围（0xDC00 至 0xDFFF）。
            //unicode说明定义的算法 计算出增补字符范围0x10000 至 0x10FFFF
            //即若result是增补字符集，应该在0x10000到0x10FFFF之间，result = (high - 0xD800) * 0x400 + (low - 0xDC00) + 0x10000;
            return character >= 0xDC00 && character <= 0xDFFF;
        }
        #endregion
    }
}
