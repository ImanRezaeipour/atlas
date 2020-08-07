using System;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Use HttpModule for implement Application_start for DNN
/// Must Register HttModule in Atlas And DNN WebConfig
/// <add name="AtlasModule" type="AtlasModule" preCondition="managedHandler"/>
/// </summary>
public class AtlasModule : IHttpModule
{

    #region IHttpModule Members

    private static bool isStarted = false;
    private static object moduleStart = new Object();
    private HttpApplication _app;
    public void Dispose()
    {
        //clean-up code here.
        //this._app.PostAuthorizeRequest -= new EventHandler(this.PostAuthorizeRequest);
    }

    public void Init(HttpApplication context)
    {
        this._app = context;
        this._app.PostAuthorizeRequest += new EventHandler(this.PostAuthorizeRequest);
        // use static moduleStart for run this code for once
        if (!isStarted)
        {
            lock (moduleStart)
            {
                if (!isStarted)
                {
                    // handle aplication start
                    GTS.Clock.Infrastructure.ConfigurationHelper.SetNHibernateSessionFactoryProps();
                    System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

                    isStarted = true;
                }
            }
        }

    }

    private void PostAuthorizeRequest(object sender, EventArgs e)
    {
        HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
    }

    #endregion

}

