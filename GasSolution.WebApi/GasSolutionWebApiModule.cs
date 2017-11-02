using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace GasSolution
{
    [DependsOn(typeof(AbpWebApiModule), typeof(GasSolutionApplicationModule))]
    public class GasSolutionWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(GasSolutionApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
