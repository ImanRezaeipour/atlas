using System;
using System.Web;
using System.Web.SessionState;
using DotNetNuke.Web.Api;

using System.Web.Script.Serialization;

namespace Atlas.Api.Controllers
{

    public class BaseController : DnnApiController, IRequiresSessionState
    {
         

        public BaseController()
        {
        
        }
    }
}