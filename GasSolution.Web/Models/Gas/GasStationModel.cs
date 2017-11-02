using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GasSolution.Domain.Gas;
using System;

namespace GasSolution.Web.Models.Gas
{

    [AutoMap(typeof(GasStation))]
    public class GasStationModel : EntityDto
    {
        /// <summary>
        /// 加油站名称
        /// </summary>
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
        public string Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public string Dimension { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClose { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int DisplayOrder { get; set; }

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