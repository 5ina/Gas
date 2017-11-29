using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Vehicles;
using System.Collections.Generic;

namespace GasSolution.Vehicles
{
    /// <summary>
    /// 车辆品牌服务接口
    /// </summary>
    public interface ICarBrandService : IApplicationService
    {
        /// <summary>
        /// 更新车辆品牌
        /// </summary>
        /// <param name="entities"></param>
        void UpdateBrands(List<CarBrand> entities);

        /// <summary>
        /// 清空品牌
        /// </summary>
        void ClearBrands();

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        IPagedResult<CarBrand> GetAllBrands(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
