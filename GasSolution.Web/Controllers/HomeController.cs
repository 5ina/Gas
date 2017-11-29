using GasSolution.Common;
using GasSolution.Gas.Sort;
using GasSolution.Web.Models.Gas;
using System.Linq;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class HomeController : GasSolutionControllerBase
    {
        #region ctor && Fields
        private readonly IAreaService _areaService;
        public HomeController(IAreaService areaService)
        {
            this._areaService = areaService;
        }
        #endregion

        #region Utilities
        private void PreparePromotionListModel(PromotionListModel model)
        {
            var areas = _areaService.GetAreasByParentCode("130101");
            model.AvailableAreas = areas.Select(a => new SelectListItem
            {
                Text = a.Name,
                Selected = false,
                Value = a.Id.ToString(),

            }).ToList();
            model.AvailableAreas.Insert(0, new SelectListItem
            {
                Text = "全部",
                Value = "0",
                Selected = true
            });
        }
        #endregion

        public ActionResult Index()
        {
            var model = new PromotionListModel();
            model.pageIndex = 0;
            model.keywords = "";
            model.pageSize = 20;
            PreparePromotionListModel(model);
            return View(model);
        }
	}
}