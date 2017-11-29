using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Gas;

namespace GasSolution.Gas
{
    /// <summary>
    /// 用户上传的加油站
    /// </summary>
    public interface IGasStationCustomService : IApplicationService
    {
        /// <summary>
        /// 新增加油站
        /// </summary>
        /// <param name="station"></param>
        int InsertStation(GasStationCustom station);

        /// <summary>
        /// 更新加油站
        /// </summary>
        /// <param name="station"></param>
        void UpdateStation(GasStationCustom station);

        /// <summary>
        /// 删除加油站
        /// </summary>
        /// <param name="stationId"></param>
        void DeleteStation(int stationId);

        /// <summary>
        /// 查询加油站
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        GasStationCustom GetStationById(int stationId);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="areaId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<GasStationCustom> GetAllStations(string keywords = "", 
            int? areaId = null, int? audit= null, int pageIndex = 0, int pageSize = int.MaxValue);

        int GetGasCutomByAudit(int audit);
    }
}
