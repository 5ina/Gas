using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GasSolution.Web.Framework.Aliyun
{
    public class AliyunRequestData
    {

        private readonly String _host;

        private readonly String _method;
        private readonly String _appcode;
        /// <summary>
        /// 阿里云API接口
        /// </summary>
        /// <param name="host">请求地址</param>
        /// <param name="method">请求方式（POST,GET)</param>
        /// <param name="appCode">授权Code</param>
        public AliyunRequestData(string host, string method = "POST", string appCode = "d1b4fc144bd34b018f61e4e64a47390e")
        {
            this._host = host;
            this._method = method;
            this._appcode = appCode;
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <typeparam name="T">返回对象的类型</typeparam>
        /// <returns>返回的数据对象实体</returns>
        public T RequestApi<T>()
        {
            String bodys = "";
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            
            if (_host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(_host));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(_host);
            }
            httpRequest.Method = _method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + _appcode);
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var result = reader.ReadToEnd();

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
            return model;
        }


        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}