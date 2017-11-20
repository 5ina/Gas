using System;
using System.Linq;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Common;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;

namespace GasSolution.Common
{
    public class KeyFontService : GasSolutionAppServiceBase, IKeyFontService
    {

        #region Fields
        private readonly IRepository<KeyFont> _fontRepository;
        private readonly ICacheManager _cacheManager;

        private const string CACHE_FONT_ID = "gas.cache.font.id-{0}";

        #endregion

        #region Ctor


        public KeyFontService(IRepository<KeyFont> fontRepository,
            ICacheManager cacheManager)
        {
            this._fontRepository = fontRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method
        public IPagedResult<KeyFont> GetAllKeyFonts(string keywords, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _fontRepository.GetAll();
            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(f => f.Keyword.Contains(keywords));
            query = query.OrderByDescending(p => p.Id);

            return new PagedResult<KeyFont>(query, pageIndex, pageSize);        
        }

        public KeyFont GetKeyFontById(int fontId)
        {
            if (fontId > 0)
            {
                var key = string.Format(CACHE_FONT_ID, fontId);
                return _cacheManager.GetCache(key).Get(key, () => _fontRepository.Get(fontId));
            }
            return null;
        }

        public int InsertKeyFont(KeyFont key)
        {
            if (key != null)
                return _fontRepository.InsertAndGetId(key);

            return 0;
        }

        public void UpdateKeyFont(KeyFont key)
        {
            if (key != null && key.Id > 0)
            {
                var cache = string.Format(CACHE_FONT_ID, key.Id);
                _fontRepository.Update(key);
                _cacheManager.GetCache(cache).Remove(cache);
            }
        }

        #endregion
    }
}
