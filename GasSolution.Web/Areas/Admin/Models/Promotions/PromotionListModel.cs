using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Models.Promotions
{
    public class PromotionListModel
    {
        public string Keywords { get; set; }


        /// <summary>
        /// 活动时间
        /// </summary>
        [UIHint("DateNullable")]
        public DateTime? Time { get; set; }


    }
}