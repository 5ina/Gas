using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using GasSolution.CacheNames;
using GasSolution.Common;
using GasSolution.Web.Areas.Admin.Models.Setting;
using GasSolution.Web.Framework.DataGrids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    public class SettingController : GasControllerAdminBase
    {
        #region Fields && Ctor

        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;

        public SettingController(ISettingService settingService, ICacheManager cacheManager)
        {
            this._settingService = settingService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method


        public ActionResult WeChat()
        {
            var model = new WeChatSettingModel
            {
                AppId = _settingService.GetSettingByKey<string>(WeChatSettingNames.AppId),
                AppSecret = _settingService.GetSettingByKey<string>(WeChatSettingNames.AppSecret),
                Token = _settingService.GetSettingByKey<string>(WeChatSettingNames.Token),
                MchId = _settingService.GetSettingByKey<string>(WeChatSettingNames.MchId),
                Notify_Url = _settingService.GetSettingByKey<string>(WeChatSettingNames.NotifyUrl),
                Key = _settingService.GetSettingByKey<string>(WeChatSettingNames.Key),
            };

            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        public ActionResult WeChat(WeChatSettingModel model)
        {
            if (String.IsNullOrWhiteSpace(model.AppId))
                model.AppId = "";
            _settingService.SaveSetting(WeChatSettingNames.AppId, model.AppId);

            if (String.IsNullOrWhiteSpace(model.AppSecret))
                model.AppSecret = "";
            _settingService.SaveSetting(WeChatSettingNames.AppSecret, model.AppSecret);

            if (String.IsNullOrWhiteSpace(model.Token))
                model.Token = "";
            _settingService.SaveSetting(WeChatSettingNames.Token, model.Token);

            if (String.IsNullOrWhiteSpace(model.MchId))
                model.MchId = "";
            _settingService.SaveSetting(WeChatSettingNames.MchId, model.MchId);

            if (String.IsNullOrWhiteSpace(model.Key))
                model.Key = "";
            _settingService.SaveSetting(WeChatSettingNames.Key, model.Key);

            if (String.IsNullOrWhiteSpace(model.Notify_Url))
                model.Notify_Url = "";
            _settingService.SaveSetting(WeChatSettingNames.NotifyUrl, model.Notify_Url);

            return View(model);
        }



        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, string Keyword = "")
        {
            var list = _settingService.GetAllSettings();
            var jsonData = new DataSourceResult
            {
                Data = list,
                Total = list.Count
            };
            return AbpJson(jsonData);
        }


        public ActionResult Common()
        {
            var model = _cacheManager.GetCache(WechatControllerNames.CACHE_SETTINGS_ALIYUN).Get(WechatControllerNames.CACHE_SETTINGS_COMMON,
                () =>
                {
                    return new CommonSettingModel
                    {
                        Title = _settingService.GetSettingByKey<string>(CommonSettingNames.Title),
                        Keywords = _settingService.GetSettingByKey<string>(CommonSettingNames.Keywords),
                        Description = _settingService.GetSettingByKey<string>(CommonSettingNames.Description),
                        No_Ninety_Eight = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Eight),
                        No_Ninety_Fine = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Fine),
                        No_Ninety_Two = _settingService.GetSettingByKey<decimal>(CommonSettingNames.No_Ninety_Two),
                        CONTACT_US = _settingService.GetSettingByKey<string>(CommonSettingNames.CONTACT_US),
                        First_Subscribe = _settingService.GetSettingByKey<string>(CommonSettingNames.FIRST_SUBSCRIBE),
                        Keywords_NoMatch = _settingService.GetSettingByKey<string>(CommonSettingNames.KEYWORDS_NOMATCH),

                    };
                });

            return View(model);
        }


        [HttpPost]
        [UnitOfWork]
        public ActionResult Common(CommonSettingModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Title))
                model.Title = "";
            _settingService.SaveSetting(CommonSettingNames.Title, model.Title);

            if (String.IsNullOrWhiteSpace(model.Keywords))
                model.Keywords = "";
            _settingService.SaveSetting(CommonSettingNames.Keywords, model.Keywords);

            if (String.IsNullOrWhiteSpace(model.Description))
                model.Description = "";
            _settingService.SaveSetting(CommonSettingNames.Description, model.Description);
            
            _settingService.SaveSetting(CommonSettingNames.No_Ninety_Two, model.No_Ninety_Two);
            _settingService.SaveSetting(CommonSettingNames.No_Ninety_Eight, model.No_Ninety_Eight);
            _settingService.SaveSetting(CommonSettingNames.No_Ninety_Fine, model.No_Ninety_Fine);

            if (String.IsNullOrWhiteSpace(model.CONTACT_US))
                model.CONTACT_US = "";
            _settingService.SaveSetting(CommonSettingNames.CONTACT_US, model.CONTACT_US);

            if (String.IsNullOrWhiteSpace(model.First_Subscribe))
                model.First_Subscribe = "";
            _settingService.SaveSetting(CommonSettingNames.FIRST_SUBSCRIBE, model.First_Subscribe);

            if (String.IsNullOrWhiteSpace(model.Keywords_NoMatch))
                model.Keywords_NoMatch = "";
            _settingService.SaveSetting(CommonSettingNames.KEYWORDS_NOMATCH, model.Keywords_NoMatch);

            _cacheManager.GetCache(WechatControllerNames.CACHE_SETTINGS_COMMON).Remove(WechatControllerNames.CACHE_SETTINGS_COMMON);

            return View(model);
        }



        public ActionResult Aliyun()
        {
            var model = _cacheManager.GetCache(WechatControllerNames.CACHE_SETTINGS_ALIYUN).Get(WechatControllerNames.CACHE_SETTINGS_ALIYUN,
                 () =>
                 {
                     return new AliyunSettingModel
                     {
                         AccessKeyId = _settingService.GetSettingByKey<string>(AliyunSettingNames.AccessKeyId),
                         AccessKeySecret = _settingService.GetSettingByKey<string>(AliyunSettingNames.AccessKeySecret),
                         Endpoint = _settingService.GetSettingByKey<string>(AliyunSettingNames.Endpoint)
                     };
                 });

            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        public ActionResult Aliyun(AliyunSettingModel model)
        {
            _settingService.SaveSetting(AliyunSettingNames.AccessKeyId, model.AccessKeyId);
            _settingService.SaveSetting(AliyunSettingNames.AccessKeySecret, model.AccessKeySecret);
            _settingService.SaveSetting(AliyunSettingNames.Endpoint, model.Endpoint);
            
            _cacheManager.GetCache(WechatControllerNames.CACHE_SETTINGS_ALIYUN).Remove(WechatControllerNames.CACHE_SETTINGS_ALIYUN);
            return View(model);
        }

        #endregion
    }
}