using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Domain.Gas
{
    public  class Promotion:Entity, IHasCreationTime
    {
        /// <summary>
        /// 所属加油站
        /// </summary>
        public int GasStationId { get; set; }

        /// <summary>
        /// 降价开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 降价结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 降价公告
        /// </summary>
        [MaxLength(1000)]
        public string Notice { get; set; }
        
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

        /// <summary>
        /// 创建时间（不需要处理）
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
