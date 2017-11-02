using System;
using System.Linq;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Gas;
using Abp.Runtime.Caching;
using Abp.Domain.Repositories;

namespace GasSolution.Gas
{
    /// <summary>
    /// 加油站服务实现类
    /// </summary>
    public class GasStationService : GasSolutionAppServiceBase, IGasStationService
    {
        #region Ctor && Field

        private readonly IRepository<GasStation> _stationRepository;

        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 加油站缓存{0} 为ID
        /// </summary>
        private readonly string CACHE_STATION_ID = "gas.cache.station.id-{0}";

        public GasStationService(IRepository<GasStation> stationRepository, 
            ICacheManager cacheManager)
        {
            this._stationRepository = stationRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method
        public void DeleteStation(int stationId)
        {
            var key = string.Format(CACHE_STATION_ID, stationId);
            _cacheManager.GetCache(key).Remove(key);
            _stationRepository.Delete(stationId);
        }

        public IPagedResult<GasStation> GetAllStations(string keywords = "", bool? isGasoLine = null,
            bool? isDieselOil = null, bool? isNatural = null, int? areaId = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _stationRepository.GetAll();
            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(s => s.GasName.Contains(keywords) ||
                                        s.Contacts.Contains(keywords) ||
                                        s.Tel.Contains(keywords) ||
                                        s.Address.Contains(keywords));

            if (isGasoLine.HasValue)
                query = query.Where(s => s.IsGasoLine == isGasoLine.Value);

            if (isDieselOil.HasValue)
                query = query.Where(s => s.IsDieselOil == isDieselOil.Value);

            if (isNatural.HasValue)
                query = query.Where(s => s.IsNatural == isNatural.Value);

            if(areaId.HasValue)
                query = query.Where(s => s.AreaId == areaId.Value);

            query = query.OrderByDescending(s => s.DisplayOrder);

            return new PagedResult<GasStation>(query, pageIndex, pageSize);
        }

        public GasStation GetStationById(int stationId)
        {
            var key = string.Format(CACHE_STATION_ID, stationId);
            return _cacheManager.GetCache(key).Get(key, 
                () => _stationRepository.Get(stationId));
        }

        public int InsertStation(GasStation station)
        {
            if (station != null)
                return  _stationRepository.InsertAndGetId(station);
            return 0;
        }

        public void UpdateStation(GasStation station)
        {
            if (station != null && station.Id > 0)
            {
                _stationRepository.Update(station);
                var key = string.Format(CACHE_STATION_ID, station.Id);
                _cacheManager.GetCache(key).Remove(key);
            }
        }
        #endregion
    }
}
