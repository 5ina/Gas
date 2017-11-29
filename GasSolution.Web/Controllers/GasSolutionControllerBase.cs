using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Controllers.Results;
using GasSolution.Customers;
using GasSolution.Web.Framework.Controllers;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    //[RequireHttps]
    [WeChatAntiForgery]
    public abstract class GasSolutionControllerBase : AbpController
    {
        private const string CUSTOMER_COOKIE_NAME = "Gas.customer";
        protected GasSolutionControllerBase()
        {
            LocalizationSourceName = GasSolutionConsts.LocalizationSourceName;
        }

        public int CustomerId
        {
            get
            {
                return Convert.ToInt32(AbpSession.UserId);
            }
        }
        

        protected override AbpJsonResult AbpJson(object data, string contentType = null,
            Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet,
            bool wrapResult = true, bool camelCase = false, bool indented = false)
        {
            return new AbpJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue,
                CamelCase = camelCase,
                Indented = indented,
            };
        }


        protected virtual string GetCustomerCookie()
        {
            if (HttpContext == null || HttpContext.Request == null)
                return null;


            var cookies = HttpContext.Request.Cookies[CUSTOMER_COOKIE_NAME];
            Logger.Debug("Cookies:" + cookies);
            return cookies.Value;
        }


        protected virtual void SetCustomerCookie(Guid customerGuid)
        {
            if (HttpContext == null || HttpContext.Response == null)
                return;

            //delete current cookie value
            HttpContext.Response.Cookies.Remove(CUSTOMER_COOKIE_NAME);

            //get date of cookie expiration
            var cookieExpires = 24 * 365; //TODO make configurable
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (customerGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            HttpCookie cook = new HttpCookie(CUSTOMER_COOKIE_NAME);
            cook.Value = customerGuid.ToString();
            cook.Expires.AddDays(365);
            Response.SetCookie(cook);//若已有此cookie，更新内容  
            Response.Cookies.Add(cook);//添加此cookie  
            
        }
    }
}