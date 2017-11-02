using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Domain.Gas
{
    /// <summary>
    /// 加油站信息
    /// </summary>
    public class GasStation : Entity, IHasCreationTime, IHasModificationTime
    {
        /// <summary>
        /// 加油站名称
        /// </summary>
        [Required, MaxLength(120)]
        public string GasName { get; set; }

        /// <summary>
        /// 是否可以加汽油
        /// </summary>
        public bool IsGasoLine { get; set; }
        /// <summary>
        /// 加油机数量
        /// </summary>
        public int GasoLineLength { get; set; }


        /// <summary>
        /// 是否可以加柴油
        /// </summary>
        public bool IsDieselOil { get; set; }
        /// <summary>
        /// 加柴油机数量
        /// </summary>
        public int DieselOilLength { get; set; }

        /// <summary>
        /// 是否可以加气
        /// </summary>
        public bool IsNatural { get; set; }

        /// <summary>
        /// 加气机器数量
        /// </summary>
        public int NaturalLength { get; set; }

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
        /// 权重
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Required,MaxLength(30)]
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required, MaxLength(30)]
        public string Tel { get; set; }

        /// <summary>
        /// 行政区域隶属
        /// </summary>
        public int AreaId { get; set; }

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
