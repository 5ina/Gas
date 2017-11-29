using Abp.Runtime.Caching;
using Abp.UI;
using GasSolution.Common;
using GasSolution.Domain;
using GasSolution.Gas;
using GasSolution.Web.Areas.Admin.Models.GasCustom;
using GasSolution.Web.Framework.DataGrids;
using GasSolution.Web.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    public class GasCustomController : GasControllerAdminBase
    {

        #region ctor && Fields
        private readonly IGasStationCustomService _stationService;
        private readonly IAreaService _areaService;
        private readonly ICacheManager _cacheManager;
        
        public GasCustomController(IGasStationCustomService stationService,
            IAreaService areaService,
            ICacheManager cacheManager)
        {
            this._stationService = stationService;
            this._areaService = areaService;
            this._cacheManager = cacheManager;
        }
        #endregion


        #region Utilities
        [NonAction]
        protected void PrepareGasCustomListModel(GasCustomListModel model)
        {
            if (model == null)
                throw new UserFriendlyException("model");

            var areas = _areaService.GetAreasByParentCode("130101");
            model.AvailableAreas = areas.Select(a => new SelectListItem
            {
                Text = a.Name,
                Selected = model.AreaId == a.Id,
                Value = a.Id.ToString(),

            }).ToList();
            model.AvailableAreas.Insert(0, new SelectListItem
            { Text = "全部区域", Value = "", Selected = true });

            model.AvailableAudits = Audit.None.EnumToDictionary(e => e.GetDescription()).ToList();
            model.AvailableAudits.Insert(0, new SelectListItem
            { Text = "全部状态", Value = "", Selected = true });

        }
        #endregion
        #region Method
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new GasCustomListModel();
            PrepareGasCustomListModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult List(DataSourceRequest command, GasCustomListModel model)
        {
            var gas = _stationService.GetAllStations(keywords: model.Keywords,
                                                    areaId: model.AreaId,
                                                    audit: model.Audit,
                                                    pageIndex: command.Page - 1,
                                                    pageSize: command.PageSize);

            var jsonData = new DataSourceResult
            {
                Data = gas.Items.Select(c => new
                {
                    Id = c.Id,
                    Contacts = c.Contacts,
                    Tel = c.Tel,
                    CreationTime = c.CreationTime,
                    Address = c.Address,
                    FixedPromotion = c.FixedPromotion
                }).ToList(),
                Total = gas.TotalCount
            };
            return AbpJson(jsonData);
        }


        [ChildActionOnly]
        public ActionResult GetGasCostomByAudit(Audit audit = Audit.None)
        {
           var result = _stationService.GetGasCutomByAudit((int)audit);
            return Content(result.ToString());
        }
#endregion
    }
}