using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    public class CommonController : GasControllerAdminBase
    {
        // GET: Admin/Common
        public ActionResult GetMap()
        {
            return View();
        }
    }
}