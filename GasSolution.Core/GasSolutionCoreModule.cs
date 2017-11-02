using System.Reflection;
using Abp.Modules;

namespace GasSolution
{
    public class GasSolutionCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
