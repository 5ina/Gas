using Abp.AutoMapper;
using Abp.Runtime.Caching;
using GasSolution.Common;
using GasSolution.Domain.Gas;
using GasSolution.ExportImport;
using GasSolution.Gas;
using GasSolution.Web.Areas.Admin.Models.Gas;
using GasSolution.Web.Framework.Controllers;
using GasSolution.Web.Framework.DataGrids;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{

    [RouteArea("Admin")]
    public class GasController : GasControllerAdminBase
    {

        #region ctor && Fields
        private readonly IGasStationService _stationService;
        private readonly IAreaService _areaService;
        private readonly ICacheManager _cacheManager;
        private readonly IPromotionService _promotionService;
        private readonly IExportManager _exportManager;
        


        private const string CACHE_GAS_STATISTICAL_OVERVIEW = "gas.cache.gas.statistical.overview";


        public GasController(IGasStationService stationService,
            IAreaService areaService, 
            IPromotionService promotionService,
            IExportManager exportManager,
            ICacheManager cacheManager)
        {
            this._stationService = stationService;
            this._areaService = areaService;
            this._promotionService = promotionService;
            this._cacheManager = cacheManager;
            this._exportManager = exportManager;
        }
        #endregion

        #region Utilities

        [NonAction]
        private bool IsValidMobileOrTel(string tel)
        {
            return CommonHelper.IsValidMobileOrTel(tel);
        }

        private void PrepareGasStationModel(GasStationModel model)
        {
            var areas = _areaService.GetAreasByParentCode("130101");
            model.Areas = areas.Select(a => new SelectListItem
            {
                Text = a.Name,
                Selected = model.AreaId == a.Id,
                Value = a.Id.ToString(),

            }).ToList();
        }
        #endregion

        #region  Method
        // GET: Admin/Gas
        public ActionResult List()
        {
            var model = new GasStationListModel();
            var areas = _areaService.GetAreasByParentCode("130101");
            model.Areas = areas.Select(a => new SelectListItem
            {
                Text = a.Name,
                Selected = model.AreaId == a.Id,
                Value = a.Id.ToString(),

            }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, GasStationListModel model)
        {
            var stations = _stationService.GetAllStations(keywords: model.Keywords,
                                                        isGasoLine: model.IsGasoLine,
                                                        isDieselOil: model.IsDieselOil,
                                                        isNatural: model.IsNatural,
                                                        areaId: model.AreaId,
                                                        pageIndex: command.Page - 1,
                                                        pageSize: command.PageSize);

            var jsonData = new DataSourceResult
            {
                Data = stations.Items.Select(b => new
                {
                    Id = b.Id,
                    GasName = b.GasName,
                    DisplayOrder = b.DisplayOrder,
                    Address = b.Address,
                    Tel = b.Tel,
                    Contacts = b.Contacts,
                    IsClose = b.IsClose
                }),
                Total = stations.TotalCount
            };
            return AbpJson(jsonData);
        }

        public ActionResult Create()
        {
            var model = new GasStationModel();
            model.DisplayOrder = 999;
            model.IsGasoLine = true;
            PrepareGasStationModel(model);
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(GasStationModel model, bool continueEditing)
        {
            if (!IsValidMobileOrTel(model.Tel))
                ModelState.AddModelError("", "联系电话输入错误");

            if (ModelState.IsValid)
            {
                var entity = model.MapTo<GasStation>();
                model.Id = _stationService.InsertStation(entity);
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
            }

            PrepareGasStationModel(model);
            return View(model);
        }


        public ActionResult Edit(int Id)
        {
            var entity = _stationService.GetStationById(Id);
            var model = entity.MapTo<GasStationModel>();
            PrepareGasStationModel(model);
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(GasStationModel model, bool continueEditing)
        {
            if (!IsValidMobileOrTel(model.Tel))
                ModelState.AddModelError("", "联系电话输入错误");

            if (ModelState.IsValid)
            {
                var entity = _stationService.GetStationById(model.Id);
                entity = model.MapTo<GasStationModel, GasStation>(entity);
                _stationService.UpdateStation(entity);
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
            }
            PrepareGasStationModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int stationId)
        {
            _stationService.DeleteStation(stationId);
            return AbpJson("ok");
        }

        /// <summary>
        /// 地图展示
        /// </summary>
        /// <returns></returns>
        public ActionResult Map()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GasList()
        {

            var stations = _stationService.GetAllStations();

            var jsonData = new DataSourceResult
            {
                Data = stations.Items.Select(b => new
                {
                    Id = b.Id,
                    GasName = b.GasName,
                    Address = b.Address,
                    IsClose = b.IsClose,
                    Dimension = b.Dimension,
                    Longitude = b.Longitude
                }),
                Total = stations.TotalCount
            };
            return AbpJson(jsonData);
        }

        #endregion

        #region Statistical Report
        [ChildActionOnly]
        public ActionResult GasStatisticalOverview()
        {
            var model = _cacheManager.GetCache(CACHE_GAS_STATISTICAL_OVERVIEW)
                .Get(CACHE_GAS_STATISTICAL_OVERVIEW, () => {

                    var gas = _stationService.GetAllStations();
                    var promotions = _promotionService.GetAllPromotions();
                    var view = new GasStatisticalOverviewModel();
                    view.GasTotalCount = gas.TotalCount;
                    view.PromotionCount = promotions.TotalCount;
                    return view;
                });
            return PartialView(model);
        }
        #endregion


        #region  Export



        [HttpPost]
        public virtual ActionResult ExportExcelSelectedToday()
        {
            var gas = _stationService.GetAllStations(promotionTime: DateTime.Now);
            try
            {
                var bytes = _exportManager.ExportPromotionsToXlsx(gas.Items);

                return File(bytes, MimeTypes.TextXlsx, "promotion.xlsx");
            }
            catch (Exception exc)
            {
                return RedirectToAction("List");
            }
        }


        [HttpPost]
        public virtual ActionResult ExportExcelSelectedAll()
        {
            var gas = _stationService.GetAllStations();
            try
            {
                var bytes = _exportManager.ExportPromotionsToXlsx(gas.Items);

                return File(bytes, MimeTypes.TextXlsx, "promotion.xlsx");
            }
            catch (Exception exc)
            {
                return RedirectToAction("List");
            }
        }
        #endregion

    }
}