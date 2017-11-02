using GasSolution.CacheNames;
using GasSolution.Domain.Settings;

namespace GasSolution.Common
{
    /// <summary>
    /// 配置的扩展方法
    /// </summary>
    public static class SettingExtension
    {
        /// <summary>
        /// 获取微信的配置信息
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static WechatSetting GetOrderSettings(this ISettingService _settingService)
        {

            var config = new WechatSetting
            {
                AppId = _settingService.GetSettingByKey<string>(WeChatSettingNames.AppId),
                AppSecret = _settingService.GetSettingByKey<string>(WeChatSettingNames.AppSecret),
                MchId = _settingService.GetSettingByKey<string>(WeChatSettingNames.MchId),
                Notify_Url = _settingService.GetSettingByKey<string>(WeChatSettingNames.NotifyUrl),
                Key = _settingService.GetSettingByKey<string>(WeChatSettingNames.Key),
                Token = _settingService.GetSettingByKey<string>(WeChatSettingNames.Token),
            };
            return config;
        }
        
    }
}
