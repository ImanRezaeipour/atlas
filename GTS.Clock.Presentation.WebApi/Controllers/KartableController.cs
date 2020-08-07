using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTS.Clock.Presentation.WebApi.Controllers
{
    public class KartableController : Controller
    {
        // GET: Kartable
        public PartialViewResult List()
        {
            return PartialView();
        }
    }
}