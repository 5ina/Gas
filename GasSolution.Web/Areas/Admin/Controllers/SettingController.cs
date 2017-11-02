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

        #endregion
    }
}