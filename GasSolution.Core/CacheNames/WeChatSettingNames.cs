namespace GasSolution.CacheNames
{

    /// <summary>
    /// 微信配置
    /// </summary>
    public class WeChatSettingNames
    {
        /// <summary>
        /// AppId
        /// </summary>
        public static string AppId { get { return "gas.setting.wechat.appid"; } }

        /// <summary>
        /// mchid 微信支付Id
        /// </summary>
        public static string MchId { get { return "gas.setting.wechat.mchid"; } }
        /// <summary>
        /// 商户支付Key
        /// </summary>
        public static string Key { get { return "gas.setting.wechat.key"; } }
        /// <summary>
        /// 支付回调地址
        /// </summary>
        public static string NotifyUrl { get { return "gas.setting.wechat.notifyurl"; } }

        /// <summary>
        /// AppSecret
        /// </summary>
        public static string AppSecret { get { return "gas.setting.wechat.secret"; } }

        public static string Token { get { return "gas.setting.wechat.token"; } }
        

    }
}
