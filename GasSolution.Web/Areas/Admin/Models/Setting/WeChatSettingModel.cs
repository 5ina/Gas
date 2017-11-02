using System.ComponentModel;

namespace GasSolution.Web.Areas.Admin.Models.Setting
{
    /// <summary>
    /// 微信配置实体
    /// </summary>
    public class WeChatSettingModel
    {


        [DisplayName("微信AppID")]
        public string AppId { get; set; }

        [DisplayName("微信AppSecret")]
        public string AppSecret { get; set; }
        [DisplayName("Token")]
        public string Token { get; set; }
        [DisplayName("微信支付商户Id")]
        public string MchId { get; set; }
        [DisplayName("微信商户支付秘钥")]
        public string Key { get; set; }
        [DisplayName("支付回调")]
        public string Notify_Url { get; set; }

    }
}