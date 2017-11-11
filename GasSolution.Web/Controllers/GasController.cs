using Abp.Runtime.Caching;
using GasSolution.Gas;
using GasSolution.Gas.Sort;
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
        private readonly ICacheManager _cacheManager;

        public GasController(IGasStationService stationService,
            IPromotionService promotionService,
            ICacheManager cacheManager)
        {
            this._stationService = stationService;
            this._promotionService = promotionService;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Method
        // GET: Gas
        public ActionResult Index(string lat = "38.04885", string lng = "114.518578")
        {
            ViewData["lat"] = lat;
            ViewData["lng"] = lng;
            return View();
        }

        public ActionResult GetLocation(string actionName = "")
        {
            ViewData["action"] = actionName;
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
                        Promotion = "#92现价：" + p.Gasoline_Ninety_Two,
                        Notice = p.Notice
                    };
                    return item;
                }),
                Total = promotions.TotalCount
            };
            return AbpJson(jsonData);
        }
        [HttpPost]
        public ActionResult GetAllGasList()
        {
            string ALL_GAS = "gas.cache.station.all";

            var jsonData = _cacheManager.GetCache(ALL_GAS).Get(ALL_GAS, () =>
            {
                var gas = _stationService.GetAllStations();
                var json = new DataSourceResult
                {
                    Data = gas.Items.Select(p =>
                    {
                        var item = new
                        {
                            Id = p.Id,
                            GasName = p.GasName,
                            Address = p.Address,
                            Dimension = p.Dimension,
                            Longitude = p.Longitude,
                            Notice = p.Notice
                        };
                        return item;
                    }),
                    Total = gas.TotalCount
                };
                return json;
            });
            return AbpJson(jsonData);
        }


        [HttpPost]
        public ActionResult GasPromotions(PromotionListModel model)
        {
            var enumValue = (PromotionSort)model.sort;
            var promotions = _promotionService.GetAllPromotions(
                                                            promotionTime: DateTime.Now,
                                                            keywords: model.keywords,
                                                            sort: enumValue,
                                                            pageIndex: model.pageIndex,
                                                            pageSize: model.pageSize);

            var totalPage = (promotions.TotalCount / model.pageSize) + (promotions.TotalCount % model.pageSize > 0 ? 1 : 0);



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
                        Promotion = "#92现价：" + p.Gasoline_Ninety_Two,
                        Notice = p.Notice
                    };
                    return item;
                }),
                Total = promotions.TotalCount,

            };
            if (model.pageIndex + 1 >= model.pageIndex)
                jsonData.NextPage = model.pageIndex;
            else
                jsonData.NextPage += 1;
            return AbpJson(jsonData);
        }

        public ActionResult AllGas(string lat = "38.04885", string lng = "114.518578")
        {
            ViewData["lat"] = lat;
            ViewData["lng"] = lng;
            return View();
        }
        #endregion
    }
}