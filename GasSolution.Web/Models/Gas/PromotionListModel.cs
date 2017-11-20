using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GasSolution.Web.Models.Gas
{
    public class PromotionListModel
    {
        public string keywords { get; set; }
        public int sort { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}