using GTS.Clock.Presentation.WebApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace GTS.Clock.Presentation.WebApi.Modules
{
    public class ApiBasicAuthHttpModule : System.Web.IHttpModule
    {
        private const string Realm = "WebAPI";
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private static bool AuthenticateUser(string token)
        {
            BToken bToken = new BToken();

            if (!bToken.Validate(token))
                return false;

            var username = bToken.GetUsername(token);
            var identity = new GenericIdentity(username);
            SetPrincipal(new GenericPrincipal(identity, null));

            return true;
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            //TODO//------------------------------------------
            //این قسمت برای تست می باشد
            //باید حذف گردد
            var identity = new GenericIdentity("admin");
            SetPrincipal(new GenericPrincipal(identity, null));
            //-------------------------------------------------
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }
        }

        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
            {
                //response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
            }
            else
            {
                var request = HttpContext.Current.Request;
                var authHeader = request.Headers["Authorization"];
                if (authHeader != null)
                {
                    var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
                    if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                    {
                        BToken bToken = new BToken();
                        var newToken = bToken.Renew(authHeaderVal.Parameter);
                        response.Headers.Add("Authorization", newToken);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}