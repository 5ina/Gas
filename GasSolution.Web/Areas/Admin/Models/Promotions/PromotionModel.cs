using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GasSolution.Domain.Gas;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Web.Areas.Admin.Models.Promotions
{
    [AutoMap(typeof(Promotion))]
    public class PromotionModel : EntityDto
    {
        /// <summary>
        /// 所属加油站
        /// </summary>
        public int GasStationId { get; set; }

        /// <summary>
        /// 降价开始时间
        /// </summary>
        [DisplayName("开始时间")]
        [UIHint("DateNullable")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 降价结束时间
        /// </summary>
        [DisplayName("结束时间")]
        [UIHint("DateNullable")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 降价公告
        /// </summary>
        [DisplayName("降价公告")]
        public string Notice { get; set; }
        

        /// <summary>
        /// #92原价
        /// </summary>
        [DisplayName("#92原价")]
        public decimal Gasoline_Market_Ninety_Two { get; set; }
        /// <summary>
        /// #92促销价
        /// </summary>
        [DisplayName("#92促销价")]
        public decimal Gasoline_Price_Ninety_Two { get; set; }


        /// <summary>
        /// #95原价
        /// </summary>
        [DisplayName("#95原价")]
        public decimal Gasoline_Market_Ninety_Fine { get; set; }
        /// <summary>
        /// #95促销价
        /// </summary>
        [DisplayName("#95促销价")]
        public decimal Gasoline_Price_Ninety_Fine { get; set; }


        /// <summary>
        /// #98原价
        /// </summary>
        [DisplayName("#98原价")]
        public decimal Gasoline_Market_Ninety_Eight { get; set; }
        /// <summary>
        /// #98促销价
        /// </summary>
        [DisplayName("#98促销价")]
        public decimal Gasoline_Price_Ninety_Eight { get; set; }
        /// <summary>
        /// 天然气原价
        /// </summary>
        [DisplayName("天然气原价")]
        public decimal Natural_Market { get; set; }
        /// <summary>
        /// 天然气现价
        /// </summary>
        [DisplayName("天然气促销价")]
        public decimal Natural_Price { get; set; }

        /// <summary>
        /// 创建时间（不需要处理）
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}