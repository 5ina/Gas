using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GasSolution.Web.Models.Common
{
    public class WeatherModel
    {

        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Info result { get; set; }


        public class Info
        {
            /// <summary>
            /// 石家庄
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cityid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string citycode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string date { get; set; }
            /// <summary>
            /// 星期六
            /// </summary>
            public string week { get; set; }
            /// <summary>
            /// 晴
            /// </summary>
            public string weather { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string temp { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string temphigh { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string templow { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string img { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string humidity { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pressure { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string windspeed { get; set; }
            /// <summary>
            /// 东风
            /// </summary>
            public string winddirect { get; set; }
            /// <summary>
            /// 2级
            /// </summary>
            public string windpower { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string updatetime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<IndexItem> index { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Aqi aqi { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<DailyItem> daily { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<HourlyItem> hourly { get; set; }
        }

        public class HourlyItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string time { get; set; }
            /// <summary>
            /// 晴
            /// </summary>
            public string weather { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string temp { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string img { get; set; }
        }


        public class DailyItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string date { get; set; }
            /// <summary>
            /// 星期六
            /// </summary>
            public string week { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sunrise { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sunset { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Night night { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Day day { get; set; }
        }


        public class Day
        {
            /// <summary>
            /// 晴
            /// </summary>
            public string weather { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string temphigh { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string img { get; set; }
            /// <summary>
            /// 西风
            /// </summary>
            public string winddirect { get; set; }
            /// <summary>
            /// 3-4 级
            /// </summary>
            public string windpower { get; set; }
        }


        public class Night
        {
            /// <summary>
            /// 晴
            /// </summary>
            public string weather { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string templow { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string img { get; set; }
            /// <summary>
            /// 西风
            /// </summary>
            public string winddirect { get; set; }
            /// <summary>
            /// 微风
            /// </summary>
            public string windpower { get; set; }
        }


        public class Aqi
        {
            /// <summary>
            /// 
            /// </summary>
            public string so2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string so224 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string no2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string no224 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string co { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string co24 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string o3 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string o38 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string o324 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm10 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm1024 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm2_5 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm2_524 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string iso2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ino2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ico { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string io3 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string io38 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ipm10 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ipm2_5 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string aqi { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string primarypollutant { get; set; }
            /// <summary>
            /// 良
            /// </summary>
            public string quality { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string timepoint { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Aqiinfo aqiinfo { get; set; }
        }



        public class Aqiinfo
        {
            /// <summary>
            /// 二级
            /// </summary>
            public string level { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string color { get; set; }
            /// <summary>
            /// 空气质量可接受，但某些污染物可能对极少数异常敏感人群健康有较弱影响
            /// </summary>
            public string affect { get; set; }
            /// <summary>
            /// 极少数异常敏感人群应减少户外活动
            /// </summary>
            public string measure { get; set; }
        }
        public class IndexItem
        {
            /// <summary>
            /// 空调指数
            /// </summary>
            public string iname { get; set; }
            /// <summary>
            /// 开启制暖空调
            /// </summary>
            public string ivalue { get; set; }
            /// <summary>
            /// 您将感到有些冷，可以适当开启制暖空调调节室内温度，以免着凉感冒。
            /// </summary>
            public string detail { get; set; }
        }
    }
}