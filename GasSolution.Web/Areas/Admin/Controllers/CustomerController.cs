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
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
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
    }
}