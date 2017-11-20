using Abp.AutoMapper;
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

        [ChildActionOnly]
        public ActionResult NewVehicles() {

            return PartialView();
        }


        [HttpPost]
        public ActionResult LoadVehiclesStatistics(string period)
        {
            var result = new List<object>();

            var nowDt = DateTime.Now;

            switch (period)
            {
                case "year":
                    //year statistics
                    var yearAgoDt = nowDt.AddYears(-1).AddMonths(1);
                    var searchYearDateUser = new DateTime(yearAgoDt.Year, yearAgoDt.Month, 1);
                    for (int i = 0; i <= 12; i++)
                    {
                        result.Add(new
                        {
                            date = searchYearDateUser.Date.ToString("Y"),
                            value = _vehicleService.GetAllVehicles(
                                createdFrom: searchYearDateUser,
                                createdTo: searchYearDateUser.AddMonths(1),
                                pageIndex: 0,
                                pageSize: 1).TotalCount.ToString()
                        });

                        searchYearDateUser = searchYearDateUser.AddMonths(1);
                    }
                    break;

                case "month":
                    //month statistics
                    var monthAgoDt = nowDt.AddDays(-30);
                    var searchMonthDateUser = new DateTime(monthAgoDt.Year, monthAgoDt.Month, monthAgoDt.Day);
                    for (int i = 0; i <= 30; i++)
                    {
                        result.Add(new
                        {
                            date = searchMonthDateUser.Date.ToString("M"),
                            value = _vehicleService.GetAllVehicles(
                                createdFrom: (searchMonthDateUser),
                                createdTo: searchMonthDateUser.AddDays(1),
                                pageIndex: 0,
                                pageSize: 1).TotalCount.ToString()
                        });

                        searchMonthDateUser = searchMonthDateUser.AddDays(1);
                    }
                    break;

                case "week":
                default:
                    //week statistics
                    var weekAgoDt = nowDt.AddDays(-7);
                    var searchWeekDateUser = new DateTime(weekAgoDt.Year, weekAgoDt.Month, weekAgoDt.Day);
                    for (int i = 0; i <= 7; i++)
                    {
                        result.Add(new
                        {
                            date = searchWeekDateUser.Date.ToString("d dddd"),
                            value = _vehicleService.GetAllVehicles(
                                createdFrom: searchWeekDateUser,
                                createdTo: searchWeekDateUser.AddDays(1),
                                pageIndex: 0,
                                pageSize: 1).TotalCount.ToString()
                        });

                        searchWeekDateUser = searchWeekDateUser.AddDays(1);
                    }
                    break;
            }

            return Json(result);
        }
        #endregion
    }
}