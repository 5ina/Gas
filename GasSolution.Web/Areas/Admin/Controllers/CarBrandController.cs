using Abp.Runtime.Caching;
using GasSolution.Domain.Vehicles;
using GasSolution.Vehicles;
using GasSolution.Web.Areas.Admin.Models.Vehicles;
using GasSolution.Web.Framework.DataGrids;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    public class CarBrandController : GasControllerAdminBase
    {

        #region ctor && Fields
        private readonly ICarBrandService _brandService;
        private readonly ICacheManager _cacheManager;

        private const string CACHE_GAS_STATISTICAL_OVERVIEW = "gas.cache.car.brands.all";


        public CarBrandController(ICarBrandService brandService,
            ICacheManager cacheManager)
        {
            this._brandService = brandService;
            this._cacheManager = cacheManager;
        }
        #endregion


        #region Utilities

        [NonAction]
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        #endregion

        #region Method

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {
            var brands = _brandService.GetAllBrands(command.Page - 1, command.PageSize);
            var jsonData = new DataSourceResult
            {
                Data = brands.Items.Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    Logo = c.Logo,
                }).ToList(),
                Total = brands.TotalCount
            };
            return AbpJson(jsonData);
        }

        [HttpPost]
        public ActionResult Update()
        {
            var code = "d1b4fc144bd34b018f61e4e64a47390e";
            var url = "http://jisucxdq.market.alicloudapi.com/car/brand";
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }

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
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<CarBrandModel>(result);

            var entities = model.result.Select(i => new CarBrand
            {
                Id = Convert.ToInt32(i.id),
                Initial = i.initial,
                Logo = i.logo,
                Name = i.name
            }).ToList();

            _brandService.UpdateBrands(entities);
            return RedirectToAction("List");
        }
        #endregion
    }
}