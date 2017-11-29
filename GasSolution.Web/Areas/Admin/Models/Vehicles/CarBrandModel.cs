using System.Collections.Generic;

namespace GasSolution.Web.Areas.Admin.Models.Vehicles
{
    public class CarBrandModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ResultItem> result { get; set; }
        public class ResultItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 奥迪
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string initial { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string parentid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string logo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string depth { get; set; }
        }
    }
}