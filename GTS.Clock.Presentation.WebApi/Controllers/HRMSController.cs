using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTS.Clock.Presentation.WebApi.Controllers
{
    public class HRMSController : Controller
    {
        // GET: HRMS
        public PartialViewResult Index()
        {
            return PartialView();
        }
    }
}