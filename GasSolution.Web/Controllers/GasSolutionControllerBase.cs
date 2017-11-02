using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Controllers.Results;
using GasSolution.Web.Framework.Controllers;
using System.Text;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    //[RequireHttps]
    [WeChatAntiForgery]
    public abstract class GasSolutionControllerBase : AbpController
    {
        protected GasSolutionControllerBase()
        {
            LocalizationSourceName = GasSolutionConsts.LocalizationSourceName;
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
    }
}