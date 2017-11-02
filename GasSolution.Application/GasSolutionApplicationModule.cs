using System.Reflection;
using Abp.Modules;
using Abp.AutoMapper;
using GasSolution.Domain.Customers;
using Microsoft.Owin.Security;
using GasSolution.Authentication.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using GasSolution.Authentication;

namespace GasSolution
{
    [DependsOn(typeof(GasSolutionCoreModule), typeof(AbpAutoMapperModule))]
    public class GasSolutionApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                mapper.CreateMap<Customer, CustomerDto>();
            });
            IocManager.Register<IAuthenticationManager, AuthenticationManager>();
            IocManager.Register<IUserStore<CustomerDto, int>, CustomerStore>();
            IocManager.Register<UserManager<CustomerDto, int>, CustomerManager>();
            IocManager.Register<SignInManager<CustomerDto, int>, LoginManager>();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
