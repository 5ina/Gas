using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Gas;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using GasSolution.Gas.Sort;

namespace GasSolution.Gas
{
    /// <summary>
    /// 促销服务实现类
    /// </summary>
    public class PromotionService : GasSolutionAppServiceBase, IPromotionService
    {

        #region Ctor && Field

        private readonly IRepository<Promotion> _promotionRepository;

        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 促销缓存{0} 为ID
        /// </summary>
        private readonly string CACHE_PROMOTION_ID = "gas.cache.promotion.id-{0}";

        /// <summary>
        /// 促销缓存{0} 为日期
        /// </summary>
        private readonly string CACHE_PROMOTION_DATE = "gas.cache.promotion.date-{0}";

        /// <summary>
        /// 促销缓存{0}日期 {1} 加油站
        /// </summary>

        private readonly string CACHE_PROMOTION_DATE_STATION = "gas.cache.promotion.date-{0}.station-{1}";

        /// <summary>
        /// 促销缓存{0} 加油站ID
        /// </summary>
        private readonly string CACHE_PROMOTION_STATION = "gas.cache.promotion.station-{0}";

        public PromotionService(IRepository<Promotion> promotionRepository,
            ICacheManager cacheManager)
        {
            this._promotionRepository = promotionRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method
        public void DeletePromotion(int promotionId)
        {
            var entity = GetPromotionById(promotionId);
            var key = string.Format(CACHE_PROMOTION_ID, promotionId);
            _cacheManager.GetCache(key).Remove(key);
            var station_key = string.Format(CACHE_PROMOTION_STATION, entity.GasStationId);
            _cacheManager.GetCache(station_key).Remove(station_key);
            if (entity.StartTime.HasValue)
            {
                var date_key = string.Format(CACHE_PROMOTION_DATE,
                    entity.StartTime.Value.ToString("yyyy-MM-dd"));
                _cacheManager.GetCache(date_key).Remove(date_key);

                var da_st_key = string.Format(CACHE_PROMOTION_DATE_STATION, entity.StartTime.Value.ToString("yyyy-MM-dd"), entity.GasStationId);
                _cacheManager.GetCache(date_key).Remove(da_st_key);
            }

            _promotionRepository.Delete(promotionId);
        }

        public IPagedResult<Promotion> GetAllPromotions(int stationId = 0,
            string keywords = "", DateTime? promotionTime = null,
            PromotionSort? sort = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _promotionRepository.GetAll();

            if (stationId > 0)
                query = query.Where(p => p.GasStationId == stationId);

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(p => p.Notice.Contains(keywords));

            if (promotionTime.HasValue)
                query = query.Where(p => p.StartTime < promotionTime.Value && p.EndTime > promotionTime.Value);

            if (sort.HasValue)
            {
                switch (sort.Value)
                {
                    case PromotionSort.Long:
                        query = query.OrderByDescending(p => p.EndTime);
                        break;
                    case PromotionSort.Time:
                    default:
                        query = query.OrderByDescending(p => p.CreationTime);
                        break;

                }
            }
            else
                query = query.OrderByDescending(p => p.CreationTime);
            return new PagedResult<Promotion>(query, pageIndex, pageSize);

        }

        public Promotion GetLastPromotionByStationId(int stationId)
        {
            var key = string.Format(CACHE_PROMOTION_STATION, stationId);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var query = _promotionRepository.GetAll();
                query = query.Where(p => p.GasStationId == stationId);
                query = query.OrderByDescending(p => p.CreationTime);
                return query.FirstOrDefault();
            });
        }

        public Promotion GetLastPromotionByStationIdAndToday(int stationId)
        {

            var key = string.Format(CACHE_PROMOTION_DATE_STATION, DateTime.Now.ToString("yyyy-MM-dd"), stationId);

            return _cacheManager.GetCache(key).Get(key, () =>
            {
                var query = _promotionRepository.GetAll();
                query = query.Where(p => p.GasStationId == stationId);

                query = query.Where(p => p.StartTime < DateTime.Now && p.EndTime > DateTime.Now);
                return query.FirstOrDefault();
            });
        }

        public Promotion GetPromotionById(int promotionId)
        {
            var key = string.Format(CACHE_PROMOTION_ID, promotionId);
            return _cacheManager.GetCache(key).Get(key, () => _promotionRepository.Get(promotionId));
        }

        public IList<Promotion> GetPromotionListByStationId(int stationId)
        {
            throw new NotImplementedException();
        }

        public int InsertPromotion(Promotion promotion)
        {
            if (promotion != null)
                return _promotionRepository.InsertAndGetId(promotion);
            return 0;
        }

        public void UpdatePromotion(Promotion promotion)
        {
            if (promotion != null)
            {
                _promotionRepository.Update(promotion);

                var key = string.Format(CACHE_PROMOTION_ID, promotion.Id);
                _cacheManager.GetCache(key).Remove(key);
                var station_key = string.Format(CACHE_PROMOTION_STATION, promotion.GasStationId);
                _cacheManager.GetCache(station_key).Remove(station_key);
                if (promotion.StartTime.HasValue)
                {
                    var date_key = string.Format(CACHE_PROMOTION_DATE,
                        promotion.StartTime.Value.ToString("yyyy-MM-dd"));
                    _cacheManager.GetCache(date_key).Remove(date_key);

                }
            }

        }
        #endregion
    }
}
