using GasSolution.Gas.Sort;
using GasSolution.Web.Models.Gas;
using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class HomeController : GasSolutionControllerBase
    {
        public ActionResult Index()
        {
            var model = new PromotionListModel();
            model.pageIndex = 0;
            model.keywords = "";
            model.sort = (int)GasSort.Time;
            model.pageSize = 20;
            return View(model);
        }
	}
}