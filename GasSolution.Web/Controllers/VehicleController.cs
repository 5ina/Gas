using Abp.AutoMapper;
using Abp.UI;
using GasSolution.Domain.Vehicles;
using GasSolution.Vehicles;
using GasSolution.Web.Framework.DataGrids;
using GasSolution.Web.Models.Vehicles;
using System.Linq;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class VehicleController : GasSolutionControllerBase
    {

        #region ctor && Fields
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            this._vehicleService = vehicleService;
        }
        #endregion

        #region Utilities
        [NonAction]
        protected void PrepareVehicleModel(VehicleModel model)
        {
            if (model == null)
                throw new UserFriendlyException("model");


            model.AvailableCarPheads = CommonHelper.GetPheads().Select(p => new SelectListItem
            {
                Text = p,
                Selected = model.CarPhead == p,
                Value = p
            }).ToList();

            model.AvailableColors = CommonHelper.GetCarColors().Select(p => new SelectListItem
            {
                Text = p.Value,
                Selected = model.Color == p.Key,
                Value = p.Key.ToString()
            }).ToList();

        }
        #endregion

        #region Method

        public ActionResult List(int pageIndex = 1, int pageSize = 5)
        {
            var cars = _vehicleService.GetAllVehicles(customerId: this.CustomerId,
                                                      pageIndex: pageIndex - 1,
                                                      pageSize: pageSize);

            var totalPage = cars.TotalCount / pageSize + cars.TotalCount % pageSize > 0 ? 1 : 0;

            var colors = CommonHelper.GetCarColors();
            var jsonData = new DataSourceResult
            {
                Data = cars.Items.Select(i => new VehicleModel
                {
                    Id = i.Id,
                    EngineNo = i.EngineNo,
                    CustomerId = i.CustomerId,
                    CartNumber = i.CartNumber,
                    FrameNo = i.FrameNo,
                    CarPhead = i.CarPhead,
                    ColorString = colors.First(c => c.Key == i.Color).Value
                }),
                Total = cars.TotalCount,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPage = totalPage == 0 ? 1 : totalPage
            };
            return View(jsonData);
        }


        public ActionResult Create()
        {
            var model = new VehicleModel();
            model.CustomerId = this.CustomerId;
            PrepareVehicleModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(VehicleModel model)
        {
            model.CustomerId = this.CustomerId;
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<Vehicle>();
                _vehicleService.CreateVehicle(entity);
                return RedirectToAction("List");
            }
            PrepareVehicleModel(model);
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var entity = _vehicleService.GetVehicleById(id);
            var model = entity.MapTo<VehicleModel>();
            PrepareVehicleModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _vehicleService.DeleteVehicle(id);
            return AbpJson("");
        }
        #endregion


        #region 违章查询
#endregion
    }
}