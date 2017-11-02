using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using GasSolution.EntityFramework;

namespace GasSolution
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(GasSolutionCoreModule))]
    public class GasSolutionDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<GasSolutionDbContext>(null);
        }
    }
}
