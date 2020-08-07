using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTS.Clock.Presentation.WebApi.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Home()
        {
            return PartialView();
        }
        public PartialViewResult Menu()
        {
            return PartialView();
        }
    }
}
