using System.Web.Mvc;

namespace GasSolution.Web.Controllers
{
    public class HomeController : GasSolutionControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}