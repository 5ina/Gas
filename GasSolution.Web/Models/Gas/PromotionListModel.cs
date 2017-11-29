using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Models.Gas
{
    public class PromotionListModel
    {
        public PromotionListModel()
        {
            this.AvailableAreas = new List<SelectListItem>();
        }

        public string keywords { get; set; }
        [UIHint("OptionList")]
        public int areaId { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public List<SelectListItem> AvailableAreas { get; set; }
    }
}