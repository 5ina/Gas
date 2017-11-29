using System.Collections.Generic;

namespace GasSolution.Web.Framework.Aliyun.Models
{
    /// <summary>
    /// 阿里云请求今日油价实体
    /// </summary>
    public class OilModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int showapi_res_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showapi_res_error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Showapi_res_body showapi_res_body { get; set; }

        public class Showapi_res_body
        {
            /// <summary>
            /// 
            /// </summary>
            public int ret_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ListItem> list { get; set; }

            public class ListItem
            {
                /// <summary>
                /// 
                /// </summary>
                public string prov { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string p90 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string p0 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string p95 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string p97 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string p89 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string p92 { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string ct { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string p93 { get; set; }
            }
        }
    }


}