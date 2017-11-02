using Abp.Web.Mvc.Views;

namespace GasSolution.Web.Views
{
    public abstract class GasSolutionWebViewPageBase : GasSolutionWebViewPageBase<dynamic>
    {

    }

    public abstract class GasSolutionWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected GasSolutionWebViewPageBase()
        {
            LocalizationSourceName = GasSolutionConsts.LocalizationSourceName;
        }
    }
}