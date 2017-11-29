using Abp.Web.Mvc.Controllers.Results;

namespace GasSolution.Web.Framework.Mvc
{
    public class NullJsonResult : AbpJsonResult
    {
        public NullJsonResult() : base(null)
        {
            //TODO test
        }
    }
}