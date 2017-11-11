using Abp.Runtime.Caching;
using Abp.UI;
using GasSolution.Customers;
using GasSolution.Domain.Customers;
using GasSolution.Web.Areas.Admin.Models.Customers;
using GasSolution.Web.Framework.DataGrids;
using GasSolution.Web.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    public class CustomerController : GasControllerAdminBase
    {

        #region ctor && Fields
        private readonly ICustomerService _customerService;
        private readonly ICacheManager _cacheManager;

        private const string CACHE_CUSTOMER_STATISTICAL_OVERVIEW = "gas.cache.customer.statistical.overview";
        public CustomerController(ICustomerService customerService,
            ICacheManager cacheManager)
        {
            this._customerService = customerService;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Utilities
        [NonAction]
        protected void PrepareCustomerListModel(CustomerListModel model)
        {
            if (model == null)
                throw new UserFriendlyException("model");           

        }
        #endregion


        #region Method
        public ActionResult List()
        {
            var model = new CustomerListModel();
            PrepareCustomerListModel(model);
            return View();
        }


        [HttpPost]
        public ActionResult List(DataSourceRequest command, CustomerListModel model)
        {
            CustomerRole? role = null;
            if (model.RoleId.HasValue)
                role = (CustomerRole)model.RoleId;
            var customer = _customerService.GetAllCustomers(keywords: model.Keywords,
                roleId: role,
                isSub: model.Sub,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            var jsonData = new DataSourceResult
            {
                Data = customer.Items.Select(c => new
                {
                    Id = c.Id,
                    Name = c.NickName,
                    Mobile = c.Mobile,
                    CreationTime = c.CreationTime,
                    IsSubscribe = c.IsSubscribe,
                    CustomerRole = ((CustomerRole)c.CustomerRoleId).GetDescription(),
                }).ToList(),
                Total = customer.TotalCount
            };
            return AbpJson(jsonData);
        }
        #endregion

        #region StatisticalOverview
        [ChildActionOnly]
        public ActionResult NewCustomer()
        {
            return PartialView();
        }

        #endregion

        #region Statistical Report

        [ChildActionOnly]
        public ActionResult CustomerStatisticalOverview()
        {
            var model = _cacheManager.GetCache(CACHE_CUSTOMER_STATISTICAL_OVERVIEW)
                .Get(CACHE_CUSTOMER_STATISTICAL_OVERVIEW, () => {

                    var customer = _customerService.GetAllCustomers(showHidden: true);
                    var view = new CustomerStatisticalOverviewModel();
                    view.CustomerTotalCount = customer.TotalCount;
                    view.CustomerWeekNewCount = customer.Items.Where(c => c.CreationTime < DateTime.Now.AddDays(-7)).Count();
                    view.CustomerWeekNewCount = customer.Items.Where(c => c.CreationTime < DateTime.Now.AddDays(-30)).Count();
                    return view;
                });
            return PartialView(model);
        }



        [HttpPost]
        public ActionResult LoadCustomerStatistics(string period)
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
                            value = _customerService.GetAllCustomers(
                                createdFrom: searchYearDateUser,
                                createdTo: searchYearDateUser.AddMonths(1),
                                roleId: CustomerRole.Member,
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
                            value = _customerService.GetAllCustomers(
                                createdFrom: (searchMonthDateUser),
                                createdTo: searchMonthDateUser.AddDays(1),
                                roleId: CustomerRole.Member,
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
                            value = _customerService.GetAllCustomers(
                                createdFrom: searchWeekDateUser,
                                createdTo: searchWeekDateUser.AddDays(1),
                                roleId: CustomerRole.Member,
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