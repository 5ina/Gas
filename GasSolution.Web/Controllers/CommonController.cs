using Abp.Runtime.Caching;
using GasSolution.Common;
using GasSolution.Customers;
using GasSolution.Messages;
using GasSolution.Vehicles;
using GasSolution.Web.Models.Common;
using GasSolution.Web.Models.Vehicles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly IAreaService _areaService;


        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;

        private readonly IVehicleService _vehicleService;

        private readonly string CACHE_VEHICLE_CUSTOMER = "gas.cache.vehicles.customer.{0}";
        public CommonController(ICustomerService customerService,
                                ISMSMessageService messageService, 
                                ISettingService settingService,
                                IVehicleService vehicleService, 
                                IAreaService areaService,
            ICacheManager cacheManager)
        {
            this._customerService = customerService;
            this._messageService = messageService;
            this._vehicleService = vehicleService;
            this._settingService = settingService;
            this._areaService = areaService;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region 

        private WeatherModel GetTodyWeather()
        {
            var key = string.Format(GasSolutionConsts.CACHE_WEATHER_DATE,DateTime.Now.ToString("YYYY.mm.dd"));

            return _cacheManager.GetCache(key).Get(key, () => {
                string url = "http://jisutianqi.market.alicloudapi.com/weather/query";

                string code = "d1b4fc144bd34b018f61e4e64a47390e";
                var querys = string.Format("city={0}&citycode={1}&cityid={2}", "石家庄", "101090101", "137");
                HttpWebRequest httpRequest = null;
                HttpWebResponse httpResponse = null;

                if (0 < querys.Length)
                {
                    url = url + "?" + querys;
                }
                httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Method = "GET";
                httpRequest.Headers.Add("Authorization", "APPCODE " + code);
                try
                {
                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    httpResponse = (HttpWebResponse)ex.Response;
                }

                Stream st = httpResponse.GetResponseStream();
                StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
                var result = reader.ReadToEnd();

                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherModel>(result);
                return model;
            });
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
            var model = GetTodyWeather();
            return View(model);
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

        public ActionResult ScanCode(int promotionId)
        {
            return View();
        }



        #endregion

        #region 区县地址
        [HttpPost]
        public ActionResult PreparePromotionListModel()
        {
            var areas = _areaService.GetAreasByParentCode("130101");
            return AbpJson(areas);

        }
        #endregion

        #region Map
        public ActionResult Map(string lat = "38.04885", string lng = "114.518578")
        {
            var model = new MapModel
            {
                lat = lat,
                lng = lng,
            };
            return View(model);
        }
        #endregion

        #region 爆料反馈

        public ActionResult Result(SumbitResultModel model)
        {
            return View(model);
        }
        #endregion

        #region 限行
        public ActionResult LimitLine()
        {
            return View();
            //var limit = Convert.ToBoolean(ConfigurationManager.AppSettings["limitroutine"]);
            //if (limit)
            //{
            //    var week = DateTime.Now.DayOfWeek;//判断星期几
            //    switch(week){
            //        case DayOfWeek.Friday:


            //    }
            //}
            //else {
            //    var single = DateTime.Now.Day % 2 == 1; //判断是否单双号
            //    if (single)
            //    {

            //    }
            //}

            //return View();
        }
        #endregion
    }
}