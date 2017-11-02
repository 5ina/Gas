﻿namespace GasSolution.Web.Models.Wechat
{
    /// <summary>
    /// 微信实体
    /// </summary>
    public class OAuthToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }

    }
}