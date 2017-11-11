﻿using Abp.AutoMapper;
using Abp.Runtime.Caching;
using Abp.UI;
using GasSolution.Vehicles;
using GasSolution.Web.Areas.Admin.Models.Vehicles;
using GasSolution.Web.Framework.DataGrids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    public class VehicleController : GasControllerAdminBase
    {

        #region ctor && Fields
        private readonly IVehicleService _vehicleService;
        private readonly ICacheManager _cacheManager;

        private const string CACHE_VEHICLE_STATISTICAL_OVERVIEW = "gas.cache.vehicle.statistical.overview";

        public VehicleController(IVehicleService vehicleService,
            ICacheManager cacheManager)
        {
            this._vehicleService = vehicleService;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Utilities
        [NonAction]
        protected void PrepareVehicleModel(VehicleModel model)
        {
            if (model == null)
                throw new UserFriendlyException("model");

            
            model.AvailableCarPheads = CommonHelper.GetPheads().Select(p => new SelectListItem
            {
                Text = p,
                Selected = model.CarPhead == p,
                Value = p
            }).ToList();

            model.AvailableCarPheads = CommonHelper.GetCarColors().Select(p => new SelectListItem
            {
                Text = p.Value,
                Selected = model.Color == p.Key,
                Value = p.Key.ToString()
            }).ToList();

        }
        #endregion

        #region Method

        public ActionResult List()
        {
            return View();
        }


        [HttpPost]
        public ActionResult List(DataSourceRequest command, string keywords)
        {
            var cars = _vehicleService.GetAllVehicles(keywords: keywords,
                                                      pageIndex: command.Page - 1,
                                                      pageSize: command.PageSize);

            var jsonData = new DataSourceResult
            {
                Data = cars.Items.MapTo<List<VehicleModel>>(),
                Total = cars.TotalCount
            };
            return AbpJson(jsonData);
        }


        public ActionResult Create(int customerId)
        {
            var model = new VehicleModel();
            model.CustomerId = customerId;
            PrepareVehicleModel(model);
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var entity= _vehicleService.GetVehicleById(id);
            var model = entity.MapTo<VehicleModel>();
            PrepareVehicleModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _vehicleService.DeleteVehicle(id);
            return AbpJson("");
        }
        #endregion


        #region Statistical Report

        [ChildActionOnly]
        public ActionResult VehicleStatisticalOverview()
        {
            var model = _cacheManager.GetCache(CACHE_VEHICLE_STATISTICAL_OVERVIEW)
                .Get(CACHE_VEHICLE_STATISTICAL_OVERVIEW, () => {

                    var vehicle = _vehicleService.GetAllVehicles();
                    var view = new VehicleStatisticalOverviewModel();
                    view.VehicleTotalCount = vehicle.TotalCount;
                    return view;
                });
            return PartialView(model);
        }
        #endregion
    }
}