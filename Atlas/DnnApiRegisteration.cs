using System;
using System.Web.Http;
using DotNetNuke.Web.Api;
//using System.Web.SessionState;
//using System.Web;
//using System.Web.Routing;
//using System.Web.Http.WebHost;


namespace Atlas.Api.Controllers
{
    public class DnnApiRegistration : IServiceRouteMapper
    {
        public DnnApiRegistration()
        {
        }
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
                moduleFolderName: "Atlas",
                routeName: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                namespaces: new[] { "Atlas.Api.Controllers" });

            //foreach (var item in route)
            //{

            //    item.RouteHandler = new MyHttpControllerRouteHandler();
            //}
        }
    }
    //public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    //{
    //    public MyHttpControllerHandler(RouteData routeData)
    //        : base(routeData)
    //    {
    //    }
    //}
    //public class MyHttpControllerRouteHandler : HttpControllerRouteHandler
    //{
    //    protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
    //    {
    //        return new MyHttpControllerHandler(requestContext.RouteData);
    //    }
    //}

}