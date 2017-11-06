using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasSolution.CacheNames
{
    /// <summary>
    /// 微信控制器相关缓存名称
    /// </summary>
    public class WechatControllerNames
    {
        /// <summary>
        /// 微信 Access Token
        /// </summary>
        public static string CACHE_ACCESS_TOKEN { get { return "gas.controller.wechat.access.token"; } }

        /// <summary>
        /// 阿里云配置
        /// </summary>
        public static string CACHE_SETTINGS_ALIYUN { get { return "gas.controller.setting.aliyun"; } }

        /// <summary>
        /// 公共配置
        /// </summary>
        public static string CACHE_SETTINGS_COMMON { get { return "gas.controller.setting.common"; } }
    }
}
