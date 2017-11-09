using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Domain.Vehicles
{
    public class Vehicle :Entity, IHasCreationTime, IHasModificationTime
    {
        public int CustomerId { get; set; }

        /// <summary>
        /// 所属省份
        /// </summary>
        [Required, MaxLength(4)]
        public string CarPhead { get; set; }

        /// <summary>
        /// 车牌尾号
        /// </summary>
        [Required, MaxLength(6)]
        public string CartNumber { get; set; }

        /// <summary>
        /// 车架号后六位
        /// </summary>
        [Required, MaxLength(6)]
        public string FrameNo { get; set; }

        /// <summary>
        /// 发动机号后六位
        /// </summary>
        [Required, MaxLength(6)]
        public string EngineNo { get; set; }

        /// <summary>
        /// 车辆颜色
        /// </summary>
        public int Color { get; set; }

        /// <summary>
        /// 最后修改时间（不需要处理）
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 创建时间（不需要处理）
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
