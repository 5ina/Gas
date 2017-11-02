using Abp.Application.Services;
using GasSolution.Domain.Common;
using System.Collections.Generic;

namespace GasSolution.Common
{
    /// <summary>
    /// 区县位置服务接口
    /// </summary>
    public interface IAreaService: IApplicationService
    {
        /// <summary>
        /// 获取区县
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        Area GetAreaById(int areaId);

        /// <summary>
        /// 获取区县
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Area GetAreaByCode(string code);

        IList<Area> GetAreasByParentCode(string code);


        IList<Area> GetAreasByParentId(int areaId);



    }
}
