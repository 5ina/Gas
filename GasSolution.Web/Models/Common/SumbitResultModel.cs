using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GasSolution.Web.Models.Common
{
    public class SumbitResultModel
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }

        public bool Success { get; set; }

        public string Content { get; set; }
    }
}