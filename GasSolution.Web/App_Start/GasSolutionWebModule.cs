using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.AutoMapper;
using System;

namespace GasSolution.Web
{
    [DependsOn(
        typeof(AbpWebMvcModule),
        typeof(GasSolutionDataModule), 
        typeof(GasSolutionApplicationModule), 
        typeof(GasSolutionWebApiModule),
        typeof(AbpAutoMapperModule))]
    public class GasSolutionWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));            
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn"));
            
            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    GasSolutionConsts.LocalizationSourceName,
                    new XmlFileLocalizationDictionaryProvider(
                        HttpContext.Current.Server.MapPath("~/Localization/GasSolution")
                        )
                    )
                );

            //为特定的缓存配置有效期
            Configuration.Caching.Configure(GasSolutionConsts.CACHE_WEATHER_DATE, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(3);
            });
            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<GasSolutionNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
