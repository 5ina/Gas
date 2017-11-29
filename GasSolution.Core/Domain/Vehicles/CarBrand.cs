using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GasSolution.Domain.Vehicles
{
    /// <summary>
    /// 车辆车系
    /// </summary>
    public class CarBrand :Entity
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        [Required, MaxLength(2)]
        public string Initial { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [Required, MaxLength(200)]
        public string Logo { get; set; }
    }
}
