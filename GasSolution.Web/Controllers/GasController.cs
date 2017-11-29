using Abp.AutoMapper;
using Abp.Runtime.Caching;
using GasSolution.CacheNames;
using GasSolution.Common;
using GasSolution.Domain.Gas;
using GasSolution.Gas;
using GasSolution.Gas.Sort;
using GasSolution.Web.Framework.DataGrids;
using GasSolution.Web.Models.Common;
using GasSolution.Web.Models.Gas;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class GasController : GasSolutionControllerBase
    {
        #region ctor && Fields
        private readonly IGasStationService _stationService;
        private readonly IPromotionService _promotionService;
        private readonly IGasStationCustomService _customService;
        private readonly IAreaService _areaService;
        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;

        public GasController(IGasStationService stationService,
            IPromotionService promotionService,
            IAreaService areaService, 
            ISettingService settingService,
            IGasStationCustomService customService,
            ICacheManager cacheManager)
        {
            this._stationService = stationService;
            this._promotionService = promotionService;
            this._areaService = areaService;
            this._settingService = settingService;
            this._customService = customService;
            this._cacheManager = cacheManager;
        }
        #endregion

        private void PreparePromotionModel(PromotionModel model)
        {
            var areas = _areaService.GetAreasByParentCode("130101");
            model.AvailableAreas = areas.Select(a => new SelectListItem
            {
                Text = a.Name,
                Selected = model.AreaId == a.Id,
                Value = a.Id.ToString(),

            }).ToList();
            model.AvailableAreas.Add(new SelectListItem
            {
                Text = "请选择区县",
                Value = "0"
            });

            model.Gasoline_Ninety_Two = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Two);
            model.Gasoline_Ninety_Fine = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Fine);
            model.Gasoline_Ninety_Eight = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Eight);
        }


        private void PrepareGasStationCustomModel(GasStationCustomModel model)
        {
            var areas = _areaService.GetAreasByParentCode("130101");
            model.AvailableAreas = areas.Select(a => new SelectListItem
            {
                Text = a.Name,
                Selected = model.AreaId == a.Id,
                Value = a.Id.ToString(),

            }).ToList();
            model.AvailableAreas.Add(new SelectListItem
            {
                Text = "请选择区县",
                Value = "0"
            });

            model.Gasoline_Ninety_Two = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Two);
            model.Gasoline_Ninety_Fine = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Fine);
            model.Gasoline_Ninety_Eight = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Eight);
        }

        [HttpPost]
        public ActionResult GetGasByAreaId(int areaId)
        {
            var gas = _stationService.GetAllStations(areaId: areaId);
            var list = gas.Items.Select(g => new {
                Value=g.Id.ToString(),
                Text = g.GasName
            }).ToList();
            return AbpJson(list);
        }

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
                        Promotion = FormatPromotion(g),
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
            var gasList = _stationService.GetAllStations(
                                                            promotionTime: DateTime.Now,
                                                            keywords: model.keywords,
                                                            areaId:model.areaId,
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
                        Promotion = FormatPromotion(g),
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

        [NonAction]
        private string FormatPromotion(Domain.Gas.GasStation station)
        {
            var sb = new StringBuilder();
            if (station.Gasoline_Ninety_Two > 0)
                sb.Append("92#：" + station.Gasoline_Ninety_Two);
            if (station.Gasoline_Ninety_Fine > 0)
                sb.Append("95#：" + station.Gasoline_Ninety_Fine);
            if (station.Gasoline_Ninety_Eight > 0)
                sb.Append("98#：" + station.Gasoline_Ninety_Eight);

            if (!String.IsNullOrWhiteSpace(station.PromotionNotice))
                sb.Append(station.PromotionNotice);
            return sb.ToString();

        }
        #endregion

        #region 上报消息 我要爆料
        public ActionResult Promotion(int customer)
        {
            var model = new PromotionModel();
            model.CustomerId = customer;
            PreparePromotionModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Promotion(PromotionModel model)
        {
            if (model.GasStationId == 0)
            {
                ModelState.AddModelError("", "请选择加油站");
            }
            if (model.GasStationId == -1)
            {
                if (String.IsNullOrWhiteSpace(model.GasName) || String.IsNullOrWhiteSpace(model.Address))
                    ModelState.AddModelError("", "新增加油站请填写加油站名称和地址");
            }
            var promotions = _promotionService.GetAllPromotions(stationId: model.GasStationId);
            var items = promotions.Items.Where(p => p.StartTime < DateTime.Now && p.EndTime > DateTime.Now);
            if (items.Count() > 0)
            {
                ModelState.AddModelError("", "当前加油站信息已被添加");
                ViewData["Promotions"] = items;
            }
            if (ModelState.IsValid)
            {
                model.CreationTime = DateTime.Now;
                var entity = model.MapTo<Promotion>();
                _promotionService.InsertPromotion(entity);

                var result = new SumbitResultModel
                {
                    Content = "油价信息已经提交，等待管理员审核",
                    ReturnUrl = Url.Action("Promotion", "Gas", new { customer = model.CustomerId }),
                    Success = true,
                };

                return RedirectToAction("Result", "Common", result);
            }
            PreparePromotionModel(model);
            return View(model);
        }
        #endregion

        #region 上报消息 添加加油站
        public ActionResult NewGas(int customer)
        {
            var model = new GasStationCustomModel();
            model.CustomerId = customer;
            PrepareGasStationCustomModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult NewGas(GasStationCustomModel model)
        {
            if (model.AreaId <= 0)
            {
                ModelState.AddModelError("", "请选择区县");
            }
            if (String.IsNullOrWhiteSpace(model.Address))
            {
                ModelState.AddModelError("", "请选择加油站地址");
            }

            if (String.IsNullOrWhiteSpace(model.Longitude)||
                String.IsNullOrWhiteSpace(model.Dimension))
            {
                ModelState.AddModelError("", "请在地图选择加油站坐标");
            }
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<GasStationCustom>();
                _customService.InsertStation(entity);
                var result = new SumbitResultModel
                {
                    Content = "油价信息已经提交，等待管理员审核",
                    ReturnUrl = Url.Action("NewGas", "Gas", new { customer = model.CustomerId }),
                    Success = true,
                };

                return RedirectToAction("Result", "Common", result);
            }
            PrepareGasStationCustomModel(model);
            return View(model);
        }
        #endregion

    }
}