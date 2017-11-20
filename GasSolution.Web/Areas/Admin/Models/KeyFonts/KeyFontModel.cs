using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GasSolution.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Models.KeyFonts
{
    [AutoMap(typeof(KeyFont))]
    public class KeyFontModel : EntityDto
    {
        public KeyFontModel()
        {
            this.AvailableType = new List<SelectListItem>();
        }
        [DisplayName("关键字")]
        [Required, MaxLength(40)]
        public string Keyword { get; set; }

        [DisplayName("回复的内容")]
        public string Relpys { get; set; }

        /// <summary>
        /// 回复方式
        /// </summary>
        [DisplayName("回复方式")]
        public int KeyFontType { get; set; }

        public List<SelectListItem> AvailableType { get; set; }

    }
}