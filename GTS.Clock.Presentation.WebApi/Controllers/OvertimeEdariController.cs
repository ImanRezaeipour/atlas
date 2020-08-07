using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTS.Clock.Presentation.WebApi.Controllers
{
    //[Authorize]
    public class OvertimeEdariController : Controller
    {
         
        public PartialViewResult List()
        {
            return PartialView();
        }

        public PartialViewResult Edit()
        {
            return PartialView();
        }
    }
}