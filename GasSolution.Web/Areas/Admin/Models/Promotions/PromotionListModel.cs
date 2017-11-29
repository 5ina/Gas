using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Models.Promotions
{
    public class PromotionListModel
    {
        public PromotionListModel()
        {
            this.AvailableAudits = new List<SelectListItem>();
        }
        public int? AuditId { get; set; }

        /// <summary>
        /// 活动时间
        /// </summary>
        [UIHint("DateNullable")]
        public DateTime? Time { get; set; }


        public IList<SelectListItem> AvailableAudits { get; set; }
    }
}