using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Domain.Common
{
    public class Area : Entity
    {
        /// <summary>
        /// 行政代码
        /// </summary>
        [Required, MaxLength(6)]
        public string Code { get; set; }

        public string Name { get; set; }

        public string ParendCode { get; set; }
    }
}
