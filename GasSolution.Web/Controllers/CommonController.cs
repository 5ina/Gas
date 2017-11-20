using Abp.Runtime.Caching;
using GasSolution.Common;
using GasSolution.Customers;
using GasSolution.Messages;
using GasSolution.Vehicles;
using GasSolution.Web.Models.Vehicles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class CommonController : GasSolutionControllerBase
    {
        #region ctor && Fields
        private readonly ICustomerService _customerService;
        private readonly ISMSMessageService _messageService;


        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;

        private readonly IVehicleService _vehicleService;

        private readonly string CACHE_VEHICLE_CUSTOMER = "gas.cache.vehicles.customer.{0}";

        /// <summary>
        /// 天气的key
        /// </summary>
        private const string weatherkey = "672ded22be42ff699fff410510852bf9";
        /// <summary>
        /// 石家庄代码
        /// </summary>
        private const string siteCode = "101090101";
        public CommonController(ICustomerService customerService,
                                ISMSMessageService messageService, 
                                ISettingService settingService,
                                IVehicleService vehicleService,
            ICacheManager cacheManager)
        {
            this._customerService = customerService;
            this._messageService = messageService;
            this._vehicleService = vehicleService;
            this._settingService = settingService;
            this._cacheManager = cacheManager;
        }
        #endregion

        
        #region Method

        /// <summary>
        /// 用户未登录
        /// </summary>
        /// <returns></returns>
        public ActionResult NotLogggen()
        {
            return View();
        }
        


        [HttpGet]
        public ActionResult Captcha(string mobile)
        {
            var captcha = CommonHelper.GenerateNumber(4);
            var customer = _customerService.GetCustomerId(this.CustomerId);
            customer.VerificationCode = captcha;
            _customerService.UpdateCustomer(customer);
            _messageService.SendMobileCode(captcha, mobile);
            return Content("验证码已发送");
        }

        #endregion


        public ActionResult Weather()
        {
            string url = "http://api.weatherdt.com/common/?area={0}&type=alarm&key={1}";
            string postUrl = string.Format(url, siteCode, weatherkey);
            Uri httpURL = new Uri(postUrl, true);
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            Stream respStream = httpResp.GetResponseStream();
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            string jsonContent = respStreamReader.ReadToEnd();

            var json = JsonConvert.DeserializeAnonymousType(jsonContent, new { c = new int[0], d = new Dictionary<string, string>() });

            //var json = JsonConvert.SerializeObject(jsonContent);
            //ViewData["json"] = jsonContent;
            return View(json);
        }


        #region 查询违章
        public ActionResult Illegal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Illegal(string number,string engine,string frame)
        {
            return AbpJson(" number: " + number + "/r/nengine:" + engine + "/r/n frame:" + frame);
        }

        [HttpPost]
        public ActionResult CustomerCarList()
        {
            var key = string.Format(CACHE_VEHICLE_CUSTOMER, this.CustomerId);
            var model = _cacheManager.GetCache(key).Get(key, () =>
            {
                var list = _vehicleService.GetAllVehicles(customerId: this.CustomerId);

                var vehicles = list.Items.ToList().Select(v => new VehicleModel
                {
                    CustomerId = this.CustomerId,
                    CarPhead = v.CarPhead,
                    CartNumber = v.CartNumber,
                    EngineNo = v.EngineNo,
                    FrameNo = v.FrameNo,
                    Color = v.Color,
                    Id = v.Id
                });
                return vehicles.ToList();
            });
            return AbpJson(model);
        }
        #endregion

    }
}