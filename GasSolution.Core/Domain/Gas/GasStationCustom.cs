using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasSolution.Domain.Gas
{
    public class GasStationCustom : Entity, IHasCreationTime
    {       

        /// <summary>
        /// 经度
        /// </summary>
        [Required, MaxLength(120)]
        public string Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        [Required, MaxLength(120)]
        public string Dimension { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClose { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        [MaxLength(1000)]
        public string Notice { get; set; }
        
        /// <summary>
        /// 负责人
        /// </summary>
        [ MaxLength(30)]
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [ MaxLength(30)]
        public string Tel { get; set; }

        /// <summary>
        /// 行政区域隶属
        /// </summary>
        public int AreaId { get; set; }

        #region Promotion

        /// <summary>
        /// 是否固定活动日期
        /// </summary>
        public bool FixedPromotion { get; set; }



        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 降价公告
        /// </summary>
        [MaxLength(1000)]
        public string PromotionNotice { get; set; }

        /// <summary>
        /// #89现价
        /// </summary>
        public decimal Gasoline_Eighty_Nine { get; set; }

        /// <summary>
        /// #92现价
        /// </summary>
        public decimal Gasoline_Ninety_Two { get; set; }

        /// <summary>
        /// #95现价
        /// </summary>
        public decimal Gasoline_Ninety_Fine { get; set; }

        /// <summary>
        /// #98现价
        /// </summary>
        public decimal Gasoline_Ninety_Eight { get; set; }
        /// <summary>
        /// 天然气现价
        /// </summary>
        public decimal Natural { get; set; }
        #endregion
        
        /// <summary>
        /// 创建时间（不需要处理）
        /// </summary>
        public DateTime CreationTime { get; set; }

        public int CustomerId { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        public int Audit { get; set; }

        [MaxLength(500)]
        public string AuditReason { get; set; }
    }
}
