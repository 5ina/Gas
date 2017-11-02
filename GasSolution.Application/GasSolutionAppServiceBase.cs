using Abp.Application.Services;

namespace GasSolution
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class GasSolutionAppServiceBase : ApplicationService
    {
        protected GasSolutionAppServiceBase()
        {
            LocalizationSourceName = GasSolutionConsts.LocalizationSourceName;
        }
    }
}