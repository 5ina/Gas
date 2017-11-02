using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Models.Gas
{
    public class GasStationListModel
    {

        public GasStationListModel()
        {
            this.Areas = new List<SelectListItem>();
        }

        public string Keywords { get; set; }


        /// <summary>
        /// 是否可以加汽油
        /// </summary>
        public bool? IsGasoLine { get; set; }


        /// <summary>
        /// 是否可以加柴油
        /// </summary>
        public bool? IsDieselOil { get; set; }

        /// <summary>
        /// 是否可以加气
        /// </summary>
        public bool? IsNatural { get; set; }

        /// <summary>
        /// 区县位置
        /// </summary>
        public int? AreaId { get; set; }

        public IList<SelectListItem> Areas { get; set; }
    }
}