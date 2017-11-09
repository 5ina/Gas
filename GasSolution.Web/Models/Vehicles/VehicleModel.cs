using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GasSolution.Domain.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Models.Vehicles
{

    [AutoMap(typeof(Vehicle))]
    public class VehicleModel : EntityDto
    {
        public VehicleModel()
        {
            AvailableCarPheads = new List<SelectListItem>();
            AvailableColors = new List<SelectListItem>();
        }

        public int CustomerId { get; set; }

        /// <summary>
        /// 所属省份
        /// </summary>
        public string CarPhead { get; set; }

        /// <summary>
        /// 车牌尾号
        /// </summary>
        [DisplayName("车牌号")]
        [Required, MaxLength(4)]
        public string CartNumber { get; set; }

        /// <summary>
        /// 车架号后六位
        /// </summary>
        [DisplayName("车架号")]
        [Required, MaxLength(6)]
        public string FrameNo { get; set; }

        /// <summary>
        /// 发动机号后六位
        /// </summary>
        [DisplayName("发动机号")]
        [Required, MaxLength(6)]
        public string EngineNo { get; set; }

        /// <summary>
        /// 车辆颜色
        /// </summary>
        [DisplayName("车辆颜色")]
        public int Color { get; set; }
        public string ColorString { get; set; }

        /// <summary>
        /// 省份选择
        /// </summary>
        public IList<SelectListItem> AvailableCarPheads { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public IList<SelectListItem> AvailableColors { get; set; }
    }
}