using GasSolution.Gas;
using GasSolution.Web.Framework.DataGrids;
using GasSolution.Web.Models.Gas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class GasController : GasSolutionControllerBase
    {


        #region ctor && Fields
        private readonly IGasStationService _stationService;
        private readonly IPromotionService _promotionService;

        public GasController(IGasStationService stationService, IPromotionService promotionService)
        {
            this._stationService = stationService;
            this._promotionService = promotionService;
        }
        #endregion

        #region Method
        // GET: Gas
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult GasList()
        {
            var promotions = _promotionService.GetAllPromotions(promotionTime: DateTime.Now);

            var jsonData = new DataSourceResult
            {
                Data = promotions.Items.Select(p =>
                {

                    var gas = _stationService.GetStationById(p.GasStationId);
                    var item = new
                    {
                        Id = gas.Id,
                        GasName = gas.GasName,
                        Address = gas.Address,
                        Dimension = gas.Dimension,
                        Longitude = gas.Longitude,
                        Promotion = "#92现价：" + p.Gasoline_Price_Ninety_Two,
                    };
                    return item;
                }),
                Total = promotions.TotalCount
            };
            return AbpJson(jsonData);
        }


        [HttpPost]
        public ActionResult GasPromotions(int pageIndex = 0,int pageSize = 20)
        {
            var promotions = _promotionService.GetAllPromotions(
                                                            promotionTime: DateTime.Now,
                                                            pageIndex: pageIndex,
                                                            pageSize: pageSize);

            var totalPage = (promotions.TotalCount / pageSize) + (promotions.TotalCount % pageSize > 0 ? 1 : 0);



            var jsonData = new DataSourceResult
            {
                Data = promotions.Items.Select(p =>
                {
                    var gas = _stationService.GetStationById(p.GasStationId);
                    var item = new
                    {
                        Id = gas.Id,
                        GasName = gas.GasName,
                        Address = gas.Address,
                        Start = p.StartTime.Value.ToString("yyyy/MM/dd"),
                        End = p.EndTime.Value.ToString("yyyy/MM/dd"),
                        Promotion = "#92现价：" + p.Gasoline_Price_Ninety_Two,
                        Notice = p.Notice
                    };
                    return item;
                }),
                Total = promotions.TotalCount,

            };
            if (pageIndex + 1 >= pageIndex)
                jsonData.NextPage = pageIndex;
            else
                jsonData.NextPage += 1;
            return AbpJson(jsonData);
        }
        #endregion
    }
}