using Castle.Core.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace GasSolution.Web.Framework
{
    public class HttpUtility
    {
        public static T Get<T>(string url, ILogger Logger = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "get";
            request.Timeout = 4000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);

            string result = sr.ReadToEnd();
            if (Logger != null)
                Logger.Debug("result 返回值 ：" + result);
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "get";
            request.Timeout = 4000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);

            string result = sr.ReadToEnd();
            return result;
        }

        public static string Post(string url, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;

            byte[] data = encoding.GetBytes(postData);
            // 准备请求...  
            try
            {
                // 设置参数  
                request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据  
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求  
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码  
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="contentType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CreatePostHttpResponse(string url, int? timeout = null, string contentType = "application/x-www-form-urlencoded", string data = null)
        {
            HttpWebRequest hwr = WebRequest.Create(url) as HttpWebRequest;
            hwr.Method = "POST";
            hwr.ContentType = contentType;
            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(data);//通过UTF-8编码  
            hwr.ContentLength = bytes.Length;
            Stream writer = hwr.GetRequestStream();
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();
            var result = hwr.GetResponse() as HttpWebResponse; //此句是获得上面URl返回的数据  
            string strMsg = WebResponseGet(result);
            return strMsg;
        }

        public static string WebResponseGet(HttpWebResponse webResponse)
        {
            StreamReader responseReader = null;
            string responseData = "";
            try
            {
                responseReader = new StreamReader(webResponse.GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                webResponse.GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }
            return responseData;
        }
    }
}