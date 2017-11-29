using System;
using System.Collections.Generic;
using GasSolution.Domain.Vehicles;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using GasSolution.Data;
using System.Linq;
using Abp.Application.Services.Dto;

namespace GasSolution.Vehicles
{
    public  class CarBrandService: GasSolutionAppServiceBase , ICarBrandService
    {

        #region Ctor && Field

        private readonly IRepository<CarBrand> _brandRepository;
        private readonly ISqlExecuter _sqlExecuter;

        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 加油站缓存{0} 为ID
        /// </summary>
        private readonly string CACHE_STATION_ID = "gas.cache.vehicle.id-{0}";

        public CarBrandService(IRepository<CarBrand> brandRepository, 
            ISqlExecuter sqlExecuter,
            ICacheManager cacheManager)
        {
            this._brandRepository = brandRepository;
            this._sqlExecuter = sqlExecuter;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Method

        public void ClearBrands()
        {
            _sqlExecuter.Execute("truncate table CarBrands");
        }

        public IPagedResult<CarBrand> GetAllBrands(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _brandRepository.GetAll();

            query = query.OrderBy(b => b.Id);

            return new PagedResult<CarBrand>(query, pageIndex, pageSize);

        }

        public void UpdateBrands(List<CarBrand> entities)
        {
            ClearBrands();
            entities.ForEach(b => _brandRepository.Insert(b));
        }
                
        #endregion
    }
}
