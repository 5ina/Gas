using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Domain.Common
{
    /// <summary>
    /// 关键字字库
    /// </summary>
    public class KeyFont :Entity
    {
        [Required, MaxLength(40)]
        public string Keyword { get; set; }

        public string Relpys { get; set; }

        /// <summary>
        /// 回复方式
        /// </summary>
        public int KeyFontType { get; set; }
    }
}
