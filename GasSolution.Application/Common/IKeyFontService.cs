using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Common;

namespace GasSolution.Common
{
    /// <summary>
    /// 关键字库服务接口
    /// </summary>
    public interface IKeyFontService: IApplicationService
    {
        /// <summary>
        /// 新增字库
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int InsertKeyFont(KeyFont key);

        /// <summary>
        /// 更新字库
        /// </summary>
        /// <param name="key"></param>
        void UpdateKeyFont(KeyFont key);

        /// <summary>
        /// 根据关键字Id获取字库
        /// </summary>
        /// <param name="fontId"></param>
        /// <returns></returns>
        KeyFont GetKeyFontById(int fontId);

        /// <summary>
        /// 获取所有的关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<KeyFont> GetAllKeyFonts(string keywords, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
