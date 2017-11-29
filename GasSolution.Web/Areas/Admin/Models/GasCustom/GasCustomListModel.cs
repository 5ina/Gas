using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Models.GasCustom
{
    public class GasCustomListModel
    {
        public GasCustomListModel()
        {
            this.AvailableAudits = new List<SelectListItem>();
            this.AvailableAreas = new List<SelectListItem>();
        }

        public string Keywords { get; set; }
        public int? Audit { get; set; }

        public int? AreaId { get; set; }

        public IList<SelectListItem> AvailableAreas { get; set; }
        public IList<SelectListItem> AvailableAudits { get; set; }
    }
}