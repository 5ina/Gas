using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GasSolution.Domain.Gas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Models.Gas
{
    [AutoMap(typeof(GasStation))]
    public class GasStationModel : EntityDto
    {
        public GasStationModel()
        {
            this.Areas = new List<SelectListItem>();
        }
        /// <summary>
        /// 加油站名称
        /// </summary>
        [DisplayName("加油站名称")]
        public string GasName { get; set; }

        /// <summary>
        /// 是否可以加汽油
        /// </summary>
        [DisplayName("可以加油")]
        public bool IsGasoLine { get; set; }
        /// <summary>
        /// 加油机数量
        /// </summary>
        [DisplayName("加油机数量")]
        public int GasoLineLength { get; set; }


        /// <summary>
        /// 是否可以加柴油
        /// </summary>
        [DisplayName("可以加柴油")]
        public bool IsDieselOil { get; set; }
        /// <summary>
        /// 加柴油机数量
        /// </summary>
        [DisplayName("柴油机数量")]
        public int DieselOilLength { get; set; }

        /// <summary>
        /// 是否可以加气
        /// </summary>
        [DisplayName("可以加气")]
        public bool IsNatural { get; set; }

        /// <summary>
        /// 加气机器数量
        /// </summary>
        [DisplayName("气数量")]
        public int NaturalLength { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        /// 
        [DisplayName("经度")]
        public string Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        [DisplayName("维度")]
        public string Dimension { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        [DisplayName("是否关闭")]
        public bool IsClose { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        [DisplayName("公告")]
        public string Notice { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        [DisplayName("权重")]
        public int DisplayOrder { get; set; }


        /// <summary>
        /// 负责人
        /// </summary>
        [DisplayName("联系人")]
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public string Tel { get; set; }
        /// <summary>
        /// 区县位置
        /// </summary>
        [DisplayName("区县位置")]
        public int AreaId { get; set; }
        public IList<SelectListItem> Areas { get; set; }
    }
}