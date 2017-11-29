using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Controllers.Results;
using System;
using System.Text;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 管理台控制器基础
    /// </summary>
    [RouteArea("Admin")]
    public class GasControllerAdminBase : AbpController
    {
        protected GasControllerAdminBase()
        {
            LocalizationSourceName = GasSolutionConsts.LocalizationSourceName;

        }



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //子节点抛出
            //if (filterContext.IsChildAction)
            //    return;
            //if (AbpSession.UserId == 0)
            //{

            //}
            //var accountControllerName = string.Concat(this.GetType().Namespace, ".", "AccountController");

            //string controllerName = filterContext.Controller.ToString();

            //if (!controllerName.Equals(accountControllerName, StringComparison.InvariantCultureIgnoreCase) &&
            //    !filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    var returnUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
            //    filterContext.Result = new RedirectResult("/Admin/Account/Login?returnUrl=" + returnUrl);

            //}

            base.OnActionExecuting(filterContext);
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


        /// <summary>
        /// 保存选择的tabl
        /// </summary>
        /// <param name="tabName">Tab name to save; empty to automatically detect it</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void SaveSelectedTabName(string tabName = "", bool persistForTheNextRequest = true)
        {
            //keep this method synchronized with
            //"GetSelectedTabName" method of \Nop.Web.Framework\HtmlExtensions.cs
            if (string.IsNullOrEmpty(tabName))
            {
                tabName = this.Request.Form["selected-tab-name"];
            }

            if (!string.IsNullOrEmpty(tabName))
            {
                const string dataKey = "gas.selected-tab-name";
                if (persistForTheNextRequest)
                {
                    TempData[dataKey] = tabName;
                }
                else
                {
                    ViewData[dataKey] = tabName;
                }
            }
        }
    }
}