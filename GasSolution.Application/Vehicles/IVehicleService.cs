using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Vehicles;
using System;

namespace GasSolution.Vehicles
{
    /// <summary>
    /// 车辆服务接口
    /// </summary>
    public interface IVehicleService : IApplicationService
    {

        /// <summary>
        /// 新增车辆
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        int CreateVehicle(Vehicle vehicle);

        /// <summary>
        /// 更新车辆
        /// </summary>
        /// <param name="vehicle"></param>
        void UpdateVehicle(Vehicle vehicle);

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="vehicleId"></param>
        void DeleteVehicle(int vehicleId);

        /// <summary>
        /// 根据主键获取车辆信息
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Vehicle GetVehicleById(int vehicleId);

        /// <summary>
        /// 获取所有车辆信息
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="createdFrom"></param>
        /// <param name="createdTo"></param>
        /// <param name="customerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<Vehicle> GetAllVehicles(string keywords = "",
            DateTime? createdFrom = null,
            DateTime? createdTo = null, 
            int customerId = 0, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
