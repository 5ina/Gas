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


        //获取当日促销加油站JSON
        [HttpPost]
        public ActionResult GasList()
        {
            var gasList = _stationService.GetAllStations(promotionTime: DateTime.Now);
            var jsonData = new DataSourceResult
            {
                Data = gasList.Items.Select(g =>
                {
                    var item = new
                    {
                        Id = g.Id,
                        GasName = g.GasName,
                        Address = g.Address,
                        Dimension = g.Dimension,
                        Longitude = g.Longitude,
                        Promotion = "#92现价：" + g.Gasoline_Ninety_Two,
                        Notice = g.PromotionNotice
                    };
                    return item;
                }),
                Total = gasList.TotalCount
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
            var enumValue = (GasSort)model.sort;
            var gasList = _stationService.GetAllStations(
                                                            promotionTime: DateTime.Now,
                                                            keywords: model.keywords,
                                                            sort: enumValue,
                                                            pageIndex: model.pageIndex,
                                                            pageSize: model.pageSize);

            var totalPage = (gasList.TotalCount / model.pageSize) + (gasList.TotalCount % model.pageSize > 0 ? 1 : 0);


            var jsonData = new DataSourceResult
            {
                Data = gasList.Items.Select(g =>
                {
                    var times = "";
                    if (g.FixedPromotion)
                        times = "长期活动";
                    else
                        times = "开始时间：" + CommonHelper.ConvertToTime(g.StartTime) + " 结束时间：" + CommonHelper.ConvertToTime(g.EndTime);
                    var item = new
                    {
                        Id = g.Id,
                        GasName = g.GasName,
                        Address = g.Address,
                        Times = times,
                        Promotion = "#92现价：" + g.Gasoline_Ninety_Two,
                        Notice = g.PromotionNotice
                    };
                    return item;
                }),
                Total = gasList.TotalCount,
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