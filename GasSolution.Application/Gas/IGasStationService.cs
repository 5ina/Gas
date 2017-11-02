using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Gas;
using System;

namespace GasSolution.Gas
{
    /// <summary>
    /// 加油站服务接口
    /// </summary>
    public interface IGasStationService : IApplicationService
    {
        /// <summary>
        /// 新增加油站
        /// </summary>
        /// <param name="station"></param>
        int InsertStation(GasStation station);

        /// <summary>
        /// 更新加油站
        /// </summary>
        /// <param name="station"></param>
        void UpdateStation(GasStation station);

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
        GasStation GetStationById(int stationId);

        /// <summary>
        /// 获取所有的加油站
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="isGasoLine"></param>
        /// <param name="isDieselOil"></param>
        /// <param name="isNatural"></param>
        /// <param name="areaId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<GasStation> GetAllStations(string keywords = "", bool? isGasoLine = null,
            Boolean? isDieselOil = null, bool? isNatural = null,int? areaId = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
