using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GasSolution.Web.Framework.WeChat.Plug
{
    public class Weather
    {

        public string GetCityWeather(string city = "石家庄",string citycode = "101090101" ,string cityId = "137")
        {
            string url = "http://jisutianqi.market.alicloudapi.com/weather/query";

            string code = "d1b4fc144bd34b018f61e4e64a47390e";
            var querys = string.Format("city={0}&citycode={1}&cityid={2}", city, citycode, cityId);
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }
            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = "POST";
            httpRequest.Headers.Add("Authorization", "APPCODE " + code);
            //if (0 < bodys.Length)
            //{
            //    byte[] data = Encoding.UTF8.GetBytes(bodys);
            //    using (Stream stream = httpRequest.GetRequestStream())
            //    {
            //        stream.Write(data, 0, data.Length);
            //    }
            //}
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
            return reader.ReadToEnd();
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}