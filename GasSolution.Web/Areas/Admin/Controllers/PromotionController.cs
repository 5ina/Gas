using Abp.AutoMapper;
using GasSolution.Domain.Gas;
using GasSolution.Gas;
using GasSolution.Web.Areas.Admin.Models.Promotions;
using GasSolution.Web.Framework.Controllers;
using GasSolution.Web.Framework.DataGrids;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public PromotionController(IPromotionService promotionService, 
            IGasStationService gasService)
        {
            this._promotionService = promotionService;
            this._gasService = gasService;
        }
        #endregion



        #region Method

        public ActionResult List()
        {
            var model = new PromotionListModel();
            return View(model);
        }


        [HttpPost]
        public ActionResult List(DataSourceRequest command, PromotionListModel model)
        {
            var promotions = _promotionService.GetAllPromotions(keywords: model.Keywords,
                                                                promotionTime: model.Time,
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
                        Gasoline_Price_Ninety_Eight = p.Gasoline_Price_Ninety_Eight,
                        Gasoline_Price_Ninety_Fine = p.Gasoline_Price_Ninety_Fine,
                        Gasoline_Price_Ninety_Two = p.Gasoline_Price_Ninety_Two,
                        Natural_Price = p.Natural_Price,
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
                var entity = model.MapTo<Promotion>();
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
    }
}