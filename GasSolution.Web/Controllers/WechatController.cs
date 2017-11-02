using Abp.AutoMapper;
using Abp.Runtime.Caching;
using GasSolution.Authentication;
using GasSolution.Authentication.Dto;
using GasSolution.CacheNames;
using GasSolution.Common;
using GasSolution.Customers;
using GasSolution.Domain.Customers;
using GasSolution.Security;
using GasSolution.Web.Models.Wechat;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GasSolution.Web.Controllers
{
    public class WechatController : GasSolutionControllerBase
    {
        #region ctor && Fields
        private readonly ISettingService _settingService;
        private readonly ICustomerService _customerService;
        private readonly LoginManager _loginManager;
        private readonly ICacheManager _cacheManager;

        public WechatController(ISettingService settingService,
            ICustomerService customerService,
            LoginManager loginManager,
            ICacheManager cacheManager)
        {
            this._settingService = settingService;
            this._customerService = customerService;
            this._loginManager = loginManager;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Utilities
        [NonAction]
        public AccessToken GetAccessToken(string appId, string appSecret)
        {
            AccessToken token = Framework.HttpUtility.Get<AccessToken>(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appSecret));
            return token;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        #region Method


        /// <summary>
        /// 对微信统一接口
        /// </summary>
        /// <param name="echoStr"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public ActionResult OpenApi(string echoStr, string signature, string timestamp, string nonce)
        {
            var token = _settingService.GetSettingByKey<string>(WeChatSettingNames.Token);
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                //微信服务器对接口消息
                using (Stream stream = HttpContext.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    var postString = Encoding.UTF8.GetString(postBytes);
                    Logger.Debug("postString:" + postString);
                    Handle(postString);
                    return Content("");
                }

            }
            else//微信进行的Get测试(开发者认证)
            {
                Logger.Debug("开发者认证");
                return WxAuth(token);
            }

        }
        public ActionResult WxAuth(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Content("");
            }
            string echoString = HttpContext.Request.QueryString["echostr"];
            string signature = HttpContext.Request.QueryString["signature"];
            string timestamp = HttpContext.Request.QueryString["timestamp"];
            string nonce = HttpContext.Request.QueryString["nonce"];
            if (CheckSignature(token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    return Content(echoString);
                }
            }
            return Content("");
        }


        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        [NonAction]
        private bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region  Oauth 父控制器验证
        public ActionResult OAuth()
        {
            var appId = _settingService.GetSettingByKey<string>(WeChatSettingNames.AppId);
            var appSecret = _settingService.GetSettingByKey<string>(WeChatSettingNames.AppSecret);

            string code = Request.QueryString["code"];
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    OAuthToken oauthToken = Framework.HttpUtility.Get<OAuthToken>(string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code));

                    var accessToken = _cacheManager.GetCache(WechatControllerNames.CACHE_ACCESS_TOKEN)
                        .Get(WechatControllerNames.CACHE_ACCESS_TOKEN, () => GetAccessToken(appId, appSecret));
                    var token = "";
                    if (accessToken != null && !string.IsNullOrEmpty(accessToken.access_token))
                    {
                        token = accessToken.access_token;
                    }

                    if (oauthToken != null && !string.IsNullOrEmpty(oauthToken.openid))
                    {
                        OAuthUserInfo userInfo = Framework.HttpUtility.Get<OAuthUserInfo>(string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", token, oauthToken.openid), Logger);                        
                        if (userInfo != null)
                        {
                            try
                            {
                                var customer = _customerService.GetCustomerByOpenId(userInfo.openid);

                                #region 假设用户不存在
                                if (customer == null || customer.Id == 0) // 判定用户是否存在
                                {
                                    var salt = CommonHelper.GenerateCode(6);
                                    var _encryptionService = Abp.Dependency.IocManager.Instance.Resolve<IEncryptionService>();
                                    var password = _encryptionService.CreatePasswordHash("123456", salt);

                                    customer = new Customer
                                    {
                                        Mobile = "",
                                        Password = password,
                                        OpenId = userInfo.openid,
                                        CustomerRoleId = (int)CustomerRole.Member,
                                        NickName = userInfo.nickname,
                                        PasswordSalt = salt,
                                        IsSubscribe = userInfo.subscribe == 1,
                                        CreationTime = DateTime.Now,
                                        LastModificationTime = DateTime.Now,
                                        VerificationCode = ""
                                    };
                                    customer.Id = _customerService.CreateCustomer(customer);
                                    if (customer.IsSubscribe)
                                    {

                                        //订阅
                                        //SendMessageToUserFirstSub(customer);
                                    }
                                }
                                #endregion

                                #region 用户存在

                                else if (userInfo.subscribe == 1)
                                {
                                    customer.NickName = userInfo.nickname;
                                    customer.SaveCustomerAttribute<string>(CustomerAttributeNames.Avatar, userInfo.headimgurl);
                                    customer.SaveCustomerAttribute<int>(CustomerAttributeNames.Sex, userInfo.sex);
                                    customer.NickName = userInfo.nickname;
                                    _customerService.UpdateCustomer(customer);
                                }
                                var dto = customer.MapTo<CustomerDto>();
                                var identity = _loginManager.CreateUserIdentity(dto);

                                //用户登录
                                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                                #endregion
                            }
                            catch (Exception e)
                            {
                                Logger.Debug("错误信息:" + e.Message);
                                Logger.Debug("错误内容：" + userInfo.openid + userInfo.nickname + userInfo.subscribe);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
            }
            return Redirect(Request.QueryString["state"]);
        }
        #endregion


        #region 
        /// <summary>
        /// 处理信息并应答
        /// </summary>
        [NonAction]
        private void Handle(string postStr)
        {
            Framework.WeChat.WechatRequest wr = new Framework.WeChat.WechatRequest(postStr);
            var eventStr = wr.LoadEvent(Logger);
            Hashtable parameters;
            switch (eventStr)
            {
                //case "scan"://关注
                case "subscribe":
                    parameters = wr.LoadXml();
                    //新用户
                    break;
                case "unsubscribe":
                    parameters = wr.LoadXml();
                    //退订关注
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}