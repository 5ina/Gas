using Abp.Runtime.Caching;
using GasSolution.Common;
using GasSolution.Customers;
using GasSolution.Messages;
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
        public CommonController(ICustomerService customerService,
                                ISMSMessageService messageService, 
                                ISettingService settingService,
            ICacheManager cacheManager)
        {
            this._customerService = customerService;
            this._messageService = messageService;

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


    }
}