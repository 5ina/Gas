using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GasSolution.Domain.Customers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Web.Models.Customers
{
    /// <summary>
    /// 用户实体
    /// </summary>
    [AutoMap(typeof(Customer))]
    public class CustomerModel : EntityDto
    {
        [Required]
        [DisplayName("手机号")]
        public string Mobile { get; set; }

        [DisplayName("微信昵称")]
        public string NickName { get; set; }


        [DisplayName("OPENID")]
        public string OpenId { get; set; }

        /// <summary>
        /// 创建时间（不需要处理）
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 收货人姓名
        /// </summary>
        [Required]
        public string Consignee { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [Required]
        public string TelNumber { get; set; }

        /// <summary>
        /// 收货省份
        /// </summary>
        [Required]
        public string ProvinceName { get; set; }
        /// <summary>
        /// 收货城市
        /// </summary>
        [Required]
        public string CityName { get; set; }

        /// <summary>
        /// 收货区县
        /// </summary>
        [Required]
        public string CountryName { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        [Required]
        public string DetailInfo { get; set; }

        [DisplayName("验证码")]
        public string Captcha { get; set; }
    }
}