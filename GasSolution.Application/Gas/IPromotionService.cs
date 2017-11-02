using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GasSolution.Domain.Gas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasSolution.Gas
{
    public interface IPromotionService: IApplicationService
    {
        /// <summary>
        /// 新增促销信息
        /// </summary>
        /// <param name="promotion"></param>
        /// <returns></returns>
        int InsertPromotion(Promotion promotion);

        /// <summary>
        /// 更新促销信息
        /// </summary>
        /// <param name="promotion"></param>
        void UpdatePromotion(Promotion promotion);

        /// <summary>
        /// 删除促销信息
        /// </summary>
        /// <param name="promotionId"></param>
        void DeletePromotion(int promotionId);

        /// <summary>
        /// 根据主键获取促销信息
        /// </summary>
        /// <param name="promotionId"></param>
        /// <returns></returns>
        Promotion GetPromotionById(int promotionId);

        /// <summary>
        /// 获取加油站最新的促销信息
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        Promotion GetLastPromotionByStationId(int stationId);
        /// <summary>
        /// 获取当前日期加油站促销信息
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        Promotion GetLastPromotionByStationIdAndToday(int stationId);

        /// <summary>
        /// 获取加油站所有促销信息
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        IList<Promotion> GetPromotionListByStationId(int stationId);

        /// <summary>
        /// 获取所有促销信息
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="keywords"></param>
        /// <param name="promotionTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<Promotion> GetAllPromotions(int stationId = 0, string keywords = "",
            DateTime? promotionTime = null, int pageIndex = 0, int pageSize = int.MaxValue);
            
    }
}
