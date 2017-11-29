using System;
using System.Web.Mvc;
namespace GasSolution.Web.Framework.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterBasedOnFormNameAttribute : FilterAttribute,  IActionFilter
    {
        #region Fields

        private readonly string _formKeyName;
        private readonly string _actionParameterName;

        #endregion

        #region Ctor

        public ParameterBasedOnFormNameAttribute(string formKeyName, string actionParameterName)
        {
            this._formKeyName = formKeyName;
            this._actionParameterName = actionParameterName;
        }

        #endregion

        #region Methods
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
                return;
            var formValue = filterContext.RequestContext.HttpContext.Request.Form[_formKeyName];
            filterContext.ActionParameters[_actionParameterName] = !string.IsNullOrEmpty(formValue);

        }

        #endregion
    }
}