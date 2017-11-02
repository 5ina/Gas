using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GasSolution.Domain.Common;
using Abp.Domain.Repositories;
using GasSolution.Domain.Settings;
using Abp.Runtime.Caching;

namespace GasSolution.Common
{
    public class AreaService : GasSolutionAppServiceBase, IAreaService
    {

        #region Fields
        private readonly IRepository<Area> _areaRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor


        public AreaService(IRepository<Area> areaRepository,
            ICacheManager cacheManager)
        {
            this._areaRepository = areaRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method
        public Area GetAreaByCode(string code)
        {
            var key = "gas.cache.area.code-{0}";
            return _cacheManager.GetCache(string.Format(key, code)).Get(string.Format(key, code),
                () => {
                    return _areaRepository.FirstOrDefault(a => a.Code == code);
                });
        }

        public Area GetAreaById(int areaId)
        {
            var key = "gas.cache.area.id-{0}";
            return _cacheManager.GetCache(string.Format(key, areaId)).Get(string.Format(key, areaId),
                () =>
                {
                    return _areaRepository.Get(areaId);
                });
        }

        public IList<Area> GetAreasByParentCode(string code)
        {

            var key = "gas.cache.area.list.code-{0}";
            return _cacheManager.GetCache(string.Format(key, code))
                .Get(string.Format(key, code),
                () =>
                {
                    return _areaRepository.GetAllList(a => a.ParendCode == code);
                });
        }

        public IList<Area> GetAreasByParentId(int areaId)
        {
            var key = "gas.cache.area.list.id-{0}";
            return _cacheManager.GetCache(string.Format(key, areaId))
                .Get(string.Format(key, areaId),
                () =>
                {
                    var area = GetAreaById(areaId);
                    return _areaRepository.GetAllList(a => a.ParendCode == area.Code);
                });
        }

        #endregion
    }
}
