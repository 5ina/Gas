using Abp.AutoMapper;
using GasSolution.Customers;
using GasSolution.Domain.Customers;
using GasSolution.Web.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class CustomerController : GasSolutionControllerBase
    {
        #region ctor && Fields
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        #endregion

        #region Utilities
        #endregion

        #region Method
        public ActionResult Info()
        {
            if (this.CustomerId > 0)
            {
                var customer = _customerService.GetCustomerId(this.CustomerId);
                if (customer != null)
                {
                    var model = customer.MapTo<CustomerModel>();

                    model.CityName = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.CityName);
                    model.ProvinceName = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.ProvinceName);
                    model.Consignee = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.Consignee);
                    model.CountryName = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.CountryName);
                    model.DetailInfo = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.DetailInfo);
                    model.TelNumber = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.TelNumber);
                    return View(model);
                }
            }
            return RedirectToAction("NotLoggen", "Common");
        }

        public ActionResult SaveInfo(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerId(model.Id);
                customer = model.MapTo<CustomerModel, Customer>(customer);
                _customerService.UpdateCustomer(customer);

                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.ProvinceName, model.ProvinceName);
                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.CityName, model.CityName);
                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.CountryName, model.CountryName);
                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.DetailInfo, model.DetailInfo);
                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.Consignee, model.Consignee);
                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.TelNumber, model.TelNumber);
                
                ViewBag["result"] = true;
                return View(model);

            }
            return View(model);
        }

        #endregion
    }
}