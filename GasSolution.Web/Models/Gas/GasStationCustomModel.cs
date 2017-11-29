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
    [AutoMap(typeof(GasStationCustom))]
    public class GasStationCustomModel : EntityDto
    {
        public GasStationCustomModel()
        {
            this.AvailableAreas = new List<SelectListItem>();
        }
        /// <summary>
        /// 经度
        /// </summary>
        [MaxLength(120)]
        public string Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        [MaxLength(120)]
        public string Dimension { get; set; }

        [DisplayName("位置")]
        [MaxLength(500)]
        public string Address { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        [DisplayName("关闭状态")]
        public bool IsClose { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        [MaxLength(1000)]
        [DisplayName("公告")]
        public string Notice { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [MaxLength(30)]
        [DisplayName("负责人")]
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(30)]
        [UIHint("Tel")]
        public string Tel { get; set; }

        /// <summary>
        /// 行政区域隶属
        /// </summary>
        [DisplayName("区县")]
        public int AreaId { get; set; }

        #region Promotion

        /// <summary>
        /// 是否固定活动日期
        /// </summary>
        [DisplayName("是否固定活动")]
        public bool FixedPromotion { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        [DisplayName("活动时间")]
        [UIHint("DateNullable")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        [DisplayName("活动时间")]
        [UIHint("DateNullable")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 降价公告
        /// </summary>
        [MaxLength(1000)]
        [DisplayName("活动公告")]
        public string PromotionNotice { get; set; }

        /// <summary>
        /// #89现价
        /// </summary>
        [DisplayName("89#")]
        public decimal Gasoline_Eighty_Nine { get; set; }

        /// <summary>
        /// #92现价
        /// </summary>
        [DisplayName("92#")]
        public decimal Gasoline_Ninety_Two { get; set; }

        /// <summary>
        /// #95现价
        /// </summary>
        [DisplayName("95#")]
        public decimal Gasoline_Ninety_Fine { get; set; }

        /// <summary>
        /// #98现价
        /// </summary>
        [DisplayName("98#")]
        public decimal Gasoline_Ninety_Eight { get; set; }
        /// <summary>
        /// 天然气现价
        /// </summary>
        [DisplayName("天然气")]
        public decimal Natural { get; set; }
        #endregion
        

        public int CustomerId { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public int Audit { get; set; }

        [MaxLength(500)]
        public string AuditReason { get; set; }
        public List<SelectListItem> AvailableAreas { get; set; }
    }
}