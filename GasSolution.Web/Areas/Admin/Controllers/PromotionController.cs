using Abp.AutoMapper;
using Abp.Runtime.Caching;
using Abp.Web.Security.AntiForgery;
using GasSolution.Domain;
using GasSolution.Domain.Gas;
using GasSolution.ExportImport;
using GasSolution.Gas;
using GasSolution.Web.Areas.Admin.Models.Promotions;
using GasSolution.Web.Framework.Controllers;
using GasSolution.Web.Framework.DataGrids;
using GasSolution.Web.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class PromotionController : GasControllerAdminBase
    {
        #region ctor && Fields
        private readonly IPromotionService _promotionService;
        private readonly IGasStationService _gasService;
        private readonly ICacheManager _cacheManager;
        private const string CACHE_PROMOTION_STATISTICAL_OVERVIEW = "gas.cache.promotion.statistical.overview";
        
        public PromotionController(IPromotionService promotionService, 
            IGasStationService gasService,
            ICacheManager cacheManager)
        {
            this._promotionService = promotionService;
            this._gasService = gasService;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Method

        public ActionResult List()
        {
            var model = new PromotionListModel();
            model.AvailableAudits = Audit.None.ToSelectListItem();
            model.AvailableAudits.Insert(0, new SelectListItem
            { Text = "全部状态", Value = "", Selected = true });
            return View(model);
        }


        [HttpPost]
        public ActionResult List(DataSourceRequest command, PromotionListModel model)
        {
            var promotions = _promotionService.GetAllPromotions(
                pageIndex: command.Page - 1,                                                                
                pageSize: command.PageSize);

            var jsonData = new DataSourceResult
            {
                Data = promotions.Items.Select(p =>  {
                    var gas = _gasService.GetStationById(p.GasStationId);
                    return new {
                        Gas = gas.GasName,
                        Address = gas.Address,
                        StartTime = p.StartTime,
                        EndTime = p.EndTime,
                        Gasoline_Ninety_Eight = p.Gasoline_Ninety_Eight,
                        Gasoline_Ninety_Fine = p.Gasoline_Ninety_Fine,
                        Gasolin_Ninety_Two = p.Gasoline_Ninety_Two,
                        Natural = p.Natural,
                        Id = p.Id,
                    };
                }),
                Total = promotions.TotalCount
            };
            return AbpJson(jsonData);
        }

        public ActionResult Create(int stationId)
        {
            var model = new PromotionModel();
            model.GasStationId = stationId;
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(PromotionModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<Promotion>();
                model.Id = _promotionService.InsertPromotion(entity);
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
            }
            return View(model);
        }


        public ActionResult Edit(int Id)
        {
            var entity = _promotionService.GetPromotionById(Id);
            var model = entity.MapTo<PromotionModel>();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(PromotionModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var entity = _promotionService.GetPromotionById(model.Id);
                entity = model.MapTo<PromotionModel, Promotion>(entity);
                _promotionService.UpdatePromotion(entity);
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            _promotionService.DeletePromotion(Id);
            return AbpJson("ok");
        }


        #endregion

        #region Statistical Report

        [ChildActionOnly]
        public ActionResult PromotionStatisticalOverview()
        {
            var model = _cacheManager.GetCache(CACHE_PROMOTION_STATISTICAL_OVERVIEW)
                  .Get(CACHE_PROMOTION_STATISTICAL_OVERVIEW, () => {

                      var promotions = _promotionService.GetAllPromotions(audit:(int)Audit.None);
                      var view = new PromotionStatisticalOverviewModel();
                      view.TotalCountNone = promotions.TotalCount;
                      return view;
                  });
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult GetPromotionByAudit(Audit audit = Audit.None)
        {
            var result = _promotionService.GetPromotionByAudit((int)audit);
            return Content(result.ToString());
        }
        #endregion


    }
}