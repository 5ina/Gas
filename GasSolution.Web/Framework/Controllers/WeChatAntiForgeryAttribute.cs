using System;
using System.Web.Mvc;

namespace GasSolution.Web.Framework.Controllers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class WeChatAntiForgeryAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var User_Agent = filterContext.RequestContext.HttpContext.Request.UserAgent;
            //判定是否微信打开
            if (!User_Agent.ToLower().Contains("micromessenger"))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            

            var accountControllerName = string.Concat("GasSolution.Web.Controllers", ".", "WechatController");

            //判定是否微信控制器
            string controllerName = filterContext.Controller.ToString();
            if (controllerName.Equals(accountControllerName, StringComparison.InvariantCultureIgnoreCase))
            {
                base.OnActionExecuting(filterContext);
                return;
            }


            //判定是否子方法
            if (filterContext.IsChildAction)
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            
        }
    }

}