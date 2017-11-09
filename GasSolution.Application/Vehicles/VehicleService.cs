using System;
using System.Linq;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Vehicles;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;

namespace GasSolution.Vehicles
{
    public class VehicleService : GasSolutionAppServiceBase, IVehicleService
    {

        #region Ctor && Field

        private readonly IRepository<Vehicle> _vehicleRepository;

        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 加油站缓存{0} 为ID
        /// </summary>
        private readonly string CACHE_STATION_ID = "gas.cache.vehicle.id-{0}";

        public VehicleService(IRepository<Vehicle> vehicleRepository,
            ICacheManager cacheManager)
        {
            this._vehicleRepository = vehicleRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method
        public int CreateVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
                return _vehicleRepository.InsertAndGetId(vehicle);

            return 0;
        }

        public void DeleteVehicle(int vehicleId)
        {
            if (vehicleId > 0)
            {
                var key = string.Format(CACHE_STATION_ID, vehicleId);

                _cacheManager.GetCache(key).Remove(key);
                _vehicleRepository.Delete(vehicleId);
            }
        }

        public IPagedResult<Vehicle> GetAllVehicles(string keywords = "",
            int customerId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _vehicleRepository.GetAll();
            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(v => v.EngineNo.Contains(keywords) ||
                                     v.FrameNo.Contains(keywords) ||
                                     v.CartNumber.Contains(keywords));

            if (customerId > 0)
                query = query.Where(v => v.CustomerId == customerId);

            query = query.OrderByDescending(v => v.CreationTime);

            return new PagedResult<Vehicle>(query, pageIndex, pageSize);
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            var key = string.Format(CACHE_STATION_ID, vehicleId);

            return _cacheManager.GetCache(key).Get(key, ()
                =>
            {
                return _vehicleRepository.Get(vehicleId);
            });
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                _vehicleRepository.Update(vehicle);
                var key = string.Format(CACHE_STATION_ID, vehicle.Id);
                _cacheManager.GetCache(key).Remove(key);
            }

        }
        #endregion
    }
}
