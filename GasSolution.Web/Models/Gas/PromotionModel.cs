using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GasSolution.Domain.Gas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GasSolution.Web.Models.Gas
{
    [AutoMap(typeof(Promotion))]
    public class PromotionModel : EntityDto
    {
        public PromotionModel()
        {
            this.AvailableAreas = new List<SelectListItem>();
        }


        [DisplayName("区县")]
        [Required]
        public int AreaId { get; set; }
        
        /// <summary>
        /// 所属加油站
        /// </summary>
        [DisplayName("加油站")]
        public int GasStationId { get; set; }

        [DisplayName("固定时间")]
        public bool FixedPromotion { get; set; }

        /// <summary>
        /// 降价开始时间
        /// </summary>
        [DisplayName("活动时间")]
        [UIHint("DateNullable")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 降价结束时间
        /// </summary>
        [DisplayName("活动时间")]
        [UIHint("DateNullable")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 降价公告
        /// </summary>
        [DisplayName("降价公告")]
        public string Notice { get; set; }

        /// <summary>
        /// #89现价
        /// </summary>
        [DisplayName("89#")]
        [UIHint("Number")]
        public decimal Gasoline_Eighty_Nine { get; set; }

        /// <summary>
        /// #92现价
        /// </summary>
        [UIHint("Number")]
        [DisplayName("92#")]
        public decimal Gasoline_Ninety_Two { get; set; }

        /// <summary>
        /// #95现价
        /// </summary>
        [UIHint("Number")]
        [DisplayName("95#")]
        public decimal Gasoline_Ninety_Fine { get; set; }

        /// <summary>
        /// #98现价
        /// </summary>
        [UIHint("Number")]
        [DisplayName("98#")]
        public decimal Gasoline_Ninety_Eight { get; set; }
        /// <summary>
        /// 天然气现价
        /// </summary>
        [UIHint("Number")]
        [DisplayName("98天然气")]
        public decimal Natural { get; set; }

        /// <summary>
        /// 加油站名称 
        /// </summary>
        [DisplayName("加油站名称")]
        public string GasName { get; set; }

        [DisplayName("地址")]
        public string Address { get; set; }

        public int CustomerId { get; set; }
        public DateTime CreationTime { get; set; }


        public List<SelectListItem> AvailableAreas { get; set; }

    }
}