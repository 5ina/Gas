using Abp.Domain.Repositories;
using GasSolution.Domain.Gas;
using System;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Runtime.Caching;

namespace GasSolution.Gas
{
    public class GasStationCustomService : GasSolutionAppServiceBase, IGasStationCustomService
    {
        #region Ctor && Field

        private readonly IRepository<GasStationCustom> _customRepository;
        private readonly ICacheManager _cacheManager;
        /// <summary>
        /// 促销缓存{0} 为日期
        /// </summary>
        private readonly string CACHE_CUSTOM_AUDIT = "gas.cache.gas.custom.audit-{0}";


        public GasStationCustomService(IRepository<GasStationCustom> customRepository,
            ICacheManager cacheManager)
        {
            this._customRepository = customRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method


        public void DeleteStation(int stationId)
        {
            var gas = _customRepository.Get(stationId);
            string key = string.Format(CACHE_CUSTOM_AUDIT, gas.Audit);
            _cacheManager.GetCache(key).Remove(key);
            _customRepository.Delete(stationId);
        }

        public IPagedResult<GasStationCustom> GetAllStations(string keywords = "",
            int? areaId = null, int? audit = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customRepository.GetAll();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(c => c.Notice.Contains(keywords) ||
                                     c.Address.Contains(keywords));
            if (areaId.HasValue)
                query = query.Where(c => c.AreaId == areaId.Value);
            if (audit.HasValue)
                query = query.Where(c => c.Audit == audit.Value);

            query = query.OrderByDescending(c => c.CreationTime);

            return new PagedResult<GasStationCustom>(query, pageIndex, pageSize);

        }

        public int GetGasCutomByAudit(int audit)
        {
            var key = string.Format(CACHE_CUSTOM_AUDIT, audit);
            return _cacheManager.GetCache(key).Get(key, () => {
                var items = _customRepository.GetAllList(p => p.Audit == audit);

                return items.Count();
            });
        }

        public GasStationCustom GetStationById(int stationId)
        {
            return _customRepository.Get(stationId);
        }

        public int InsertStation(GasStationCustom station)
        {
            if (station != null)
            {
                string key = string.Format(CACHE_CUSTOM_AUDIT, station.Audit);
                _cacheManager.GetCache(key).Remove(key);
                return _customRepository.InsertAndGetId(station);
            }
            return 0;
        }

        public void UpdateStation(GasStationCustom station)
        {
            if (station != null || station.Id > 0)
            {
                string key = string.Format(CACHE_CUSTOM_AUDIT, station.Audit);
                _cacheManager.GetCache(key).Remove(key);
                _customRepository.Update(station);
            }
        }

        #endregion
    }
}
