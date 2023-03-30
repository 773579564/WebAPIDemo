using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace FY.Common.Helper
{
    public class HttpHelper
    {
        /// <summary>
        /// 等待请求开始返回的超时时间.从发出请求开始算起，到与服务器建立连接的时间
        /// </summary>
        private int _timeout = 10000;

        /// <summary>
        /// 等待读取数据完成的超时时间。设置的是从建立连接开始，到下载数据完毕所历经的时间
        /// </summary>
        private int _readWriteTimeout = 300000;

        /// <summary>
        /// 是否忽略SSL检查
        /// </summary>
        private bool _ignoreSSLCheck = true;

        /// <summary>
        /// 是否禁用本地代理
        /// </summary>
        private bool _disableWebProxy = false;

        public const string CONTENT_ENCODING_GZIP = "gzip";//是否是压缩数据

        #region url请求属性设置

        /// <summary>
        /// 等待请求开始返回的超时时间
        /// </summary>
        public int Timeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }

        /// <summary>
        /// 等待读取数据完成的超时时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return this._readWriteTimeout; }
            set { this._readWriteTimeout = value; }
        }

        /// <summary>
        /// 是否忽略SSL检查
        /// </summary>
        public bool IgnoreSSLCheck
        {
            get { return this._ignoreSSLCheck; }
            set { this._ignoreSSLCheck = value; }
        }

        /// <summary>
        /// 是否禁用本地代理
        /// </summary>
        public bool DisableWebProxy
        {
            get { return this._disableWebProxy; }
            set { this._disableWebProxy = value; }
        }

        #endregion


        #region http post请求调用方法
        /// <summary>
        ///  忽略SSL证书检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool TrustAllValidationCallback(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors errors)
        {
            return true; // 忽略SSL证书检查
        }
        /// <summary>
        /// url请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <param name="method">请求方式</param>
        /// <returns></returns>
        public HttpWebRequest GetWebRequest(string url, string method, IDictionary<string, string> headerParams)
        {
            HttpWebRequest req;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //忽略SSL检查
                if (_ignoreSSLCheck)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(TrustAllValidationCallback);
                }
                req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(url);
            }
            //是否禁用本地代理
            if (this._disableWebProxy)
            {
                req.Proxy = null;
            }

            //请求头部参数
            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (string key in headerParams.Keys)
                {
                    req.Headers.Add(key, headerParams[key]);
                }
            }

            //req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            //req.KeepAlive = false;
            //req.UserAgent = "top-sdk-net";
            //req.Accept = "text/xml,text/javascript";
            req.Timeout = this._timeout;
            req.ReadWriteTimeout = this._readWriteTimeout;
            //设置链接数量
            ServicePointManager.DefaultConnectionLimit = 500;

            return req;
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, string> headerParams = null)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8;";
            req.KeepAlive = true;

            //POST提交数据
            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(textParams));
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();
            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, Encoding.UTF8);
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, string content, IDictionary<string, string> headerParams = null)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8;";
            req.KeepAlive = true;

            //POST提交数据
            byte[] postData = Encoding.UTF8.GetBytes(content);
            req.ContentLength = postData.Length;
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, Encoding.UTF8);
        }

        /// <summary>
        /// 执行HTTP POST请求（Json对象）。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPostByJson(string url, string content, IDictionary<string, string> headerParams = null)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/json;charset=UTF-8;";
            req.KeepAlive = true;

            //POST提交数据
            byte[] postData = Encoding.UTF8.GetBytes(content);
            req.ContentLength = postData.Length;
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, Encoding.UTF8);
        }

        /// <summary>
        /// 执行HTTP Get请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(string url, IDictionary<string, string> headerParams = null)
        {
            HttpWebRequest req = GetWebRequest(url, "GET", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8;";
            req.KeepAlive = false;

            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, Encoding.UTF8);
        }



        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                if (CONTENT_ENCODING_GZIP.Equals(rsp.ContentEncoding, StringComparison.OrdinalIgnoreCase))
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }
        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return "";
            }

            StringBuilder query = new StringBuilder();
            bool hasParam = false;

            foreach (KeyValuePair<string, string> kv in parameters)
            {
                string name = kv.Key;
                string value = kv.Value;
                // 忽略参数名为空的参数
                if (!string.IsNullOrEmpty(name))
                {
                    if (hasParam)
                    {
                        query.Append("&");
                    }

                    query.Append(name);
                    query.Append("=");
                    query.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    hasParam = true;
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public HttpWebResponse DoPostResponse(string url, string strcontent, IDictionary<string, string> headerParams = null)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;";
            req.KeepAlive = true;

            //POST提交数据
            byte[] postData = Encoding.UTF8.GetBytes(strcontent);
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return rsp;
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public HttpWebResponse DoPostResponse(string url, IDictionary<string, string> textParams, IDictionary<string, string> headerParams = null)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/x-www-form-urlencoded;";
            req.KeepAlive = true;

            //POST提交数据
            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(textParams));
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return rsp;
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="headerParams">请求头部参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPostJson(string url, string content, IDictionary<string, string> headerParams = null)
        {
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "application/json;charset=UTF-8;";
            req.KeepAlive = true;

            //POST提交数据
            byte[] postData = Encoding.UTF8.GetBytes(content);
            req.ContentLength = postData.Length;
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, Encoding.UTF8);
        }
        #region 模拟表单提交 
        /// <summary>
        /// 写入文本对象
        /// </summary>
        /// <param name="memStream"></param>
        /// <param name="textParams"></param>
        private void WriteformdataText(MemoryStream memStream, string beginBoundary, IDictionary<string, string> textParams)
        {
            if (textParams == null)
            {
                return;
            }
            //提交文本数据
            foreach (var kv in textParams)
            {
                String str = String.Format("{0}Content-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", beginBoundary, kv.Key, kv.Value);
                byte[] formitembytes = Encoding.UTF8.GetBytes(str);
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }
        }

        /// <summary>
        /// 写入文件对象
        /// </summary>
        /// <param name="memStream"></param>
        /// <param name="textParams"></param>
        private void WriteformdataFile(MemoryStream memStream, string beginBoundary, IDictionary<string, MyPostFile> fileParams)
        {
            if (fileParams == null)
            {
                return;
            }
            //提交文件数据
            foreach (var kv in fileParams)
            {
                String str = String.Format("{0}Content-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: application/octet-stream\r\n\r\n", beginBoundary, kv.Key, kv.Value.Name);
                //写入文件头
                byte[] formitembytes = Encoding.UTF8.GetBytes(str);
                memStream.Write(formitembytes, 0, formitembytes.Length);
                //写入文件数据
                memStream.Write(kv.Value.Byte, 0, kv.Value.Byte.Length);
                //写入换行符
                byte[] bFileEnd = System.Text.Encoding.UTF8.GetBytes("\r\n");
                memStream.Write(bFileEnd, 0, bFileEnd.Length);
            }
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="textParams">提交文本对象</param>
        /// <param name="fileParams">提交文件对象</param>
        /// <param name="headerParams">请求头部设置</param>
        /// <returns></returns>
        public string DoPostformdata(string url, IDictionary<string, string> textParams, IDictionary<string, MyPostFile> fileParams, IDictionary<string, string> headerParams = null)
        {
            // 边界符
            string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            // 边界符
            var beginBoundary = "--" + boundary + "\r\n";
            // 最后的结束符
            var endBoundary = Encoding.UTF8.GetBytes("--" + boundary + "--\r\n");

            //开始请求
            HttpWebRequest req = GetWebRequest(url, "POST", headerParams);
            req.ContentType = "multipart/form-data;charset=UTF-8;boundary=" + boundary;

            //POST提交数据
            using (var memStream = new MemoryStream())
            {
                //写入文本对象
                WriteformdataText(memStream, beginBoundary, textParams);

                //写入文件对象
                WriteformdataFile(memStream, beginBoundary, fileParams);

                // 写入最后的结束边界符
                memStream.Write(endBoundary, 0, endBoundary.Length);

                //获取二进制数据
                byte[] postBytes = memStream.ToArray();
                //设置数据长度
                req.ContentLength = postBytes.Length;
                //写入上传请求数据
                using (System.IO.Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                    reqStream.Close();
                }
            }
            //获取返回结果
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, Encoding.UTF8);
        }
        #endregion
        #endregion
    }

    /// <summary>
    /// 自定义post提交文件类
    /// </summary>
    public class MyPostFile
    {
        public MyPostFile(string _name, byte[] _byte)
        {
            Name = _name;
            Byte = _byte;
        }
        public string Name { get; set; }

        public byte[] Byte { get; set; }
    }
}
