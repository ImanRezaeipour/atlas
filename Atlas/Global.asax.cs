using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Christoc.Modules.Atlas
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //DNN// Use AtlasModule(HttpModule) To Handle Application_Start

            // Code that runs on application startup
            GTS.Clock.Infrastructure.ConfigurationHelper.SetNHibernateSessionFactoryProps();
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };
            Application["UserOnlineCount"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //string applicationIntanceName = System.Web.Hosting.HostingEnvironment.ApplicationID.Replace('/', '_');
            //System.Diagnostics.PerformanceCounter performanceCounter = new System.Diagnostics.PerformanceCounter("ASP.NET Applications", "Sessions Active", applicationIntanceName);
            //var i = performanceCounter.NextValue().ToString();

            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            Application["UserOnlineCount"] = Convert.ToInt32(Application["UserOnlineCount"]) - 1;
            //this.OrganizeTemp(GTS.Clock.Infrastructure.OrganizeTempState.SessionEnd);
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        void OrganizeTemp(GTS.Clock.Infrastructure.OrganizeTempState OTS)
        {
            if (Convert.ToInt32(Application["UserOnlineCount"]) <= 0)
            {
                Application.Lock();
                switch (OTS)
                {
                    case GTS.Clock.Infrastructure.OrganizeTempState.SessionStart:
                        GTS.Clock.Infrastructure.Repository.TempRepository tempRepository = new GTS.Clock.Infrastructure.Repository.TempRepository(false);
                        tempRepository.OrganizeTemp();
                        tempRepository.ClearChartTempImagesDirectory();
                        break;
                    case GTS.Clock.Infrastructure.OrganizeTempState.SessionEnd:
                        GTS.Clock.Business.Temp.BTemp bTemp = new GTS.Clock.Business.Temp.BTemp();
                        bTemp.OrganizeTemp();
                        bTemp.ClearChartTempImagesDirectory();
                        break;
                }
                Application.UnLock();
            }
        }
    }
}