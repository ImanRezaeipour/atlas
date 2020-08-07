using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using GTS.Clock.Business.Security;

/// <summary>
/// Summary description for AttackDefender
/// </summary>
public static class AttackDefender
{
    private static ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public static void CSRFDefender(Page page)
    {
        try
        {
            string pageKey = string.Empty;
            if (!HttpContext.Current.Request.Headers.AllKeys.Contains("ajax-method"))
            {
                if (!HttpContext.Current.Request.Url.ToString().Contains(".ashx"))
                    pageKey = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
                else
                    pageKey = HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];
            }
            else
                pageKey = HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];

            if (HttpContext.Current.Session["CSRF"] == null ||
               (HttpContext.Current.Session["CSRF"] != null && !((Dictionary<string, string>)HttpContext.Current.Session["CSRF"]).ContainsKey(pageKey)) ||
               !ValidatePageKey(pageKey) ||
               !ValidateAjaxRequest(pageKey))
                Defend(page);
        }
        catch (Exception)
        {
            Defend(page);
        }
    }

    private static bool ValidatePageKey(string pageKey)
    {
        bool isValid = false;
        if (string.Compare(CreateHashData(pageKey, true), ((Dictionary<string, string>)HttpContext.Current.Session["CSRF"])[pageKey]) == 0)
            isValid = true;
        return isValid;
    }

    private static bool ValidateAjaxRequest(string pageKey) 
    {
        bool isValid = true;
        if((HttpContext.Current.Request.Headers.AllKeys.Contains("ajax-method") && !HttpContext.Current.Request.Headers.AllKeys.Contains("key")) ||
           (HttpContext.Current.Request.Headers.AllKeys.Contains("ajax-method") && HttpContext.Current.Request.Headers.AllKeys.Contains("key") && HttpContext.Current.Request.Headers["key"] != ((Dictionary<string, string>)HttpContext.Current.Session["CSRF"])[pageKey]))
        isValid = false;
        return isValid;
    }

    private static string CreateHashData(string data, bool isComplex)
    {
        SHA1 sha1 = SHA1.Create();
        if (isComplex)
            data = data + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.UserHostAddress;
        byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
        StringBuilder returnValue = new StringBuilder();
        for (int i = 0; i < hashData.Length; i++)
        {
            returnValue.Append(hashData[i].ToString());
        }
        return returnValue.ToString();
    }

    private static void Defend(Page page)
    {
        string[] retMessage = new string[4];
        Exception exception = new Exception("CSRF Attack!");
        retMessage = exceptionHandler.HandleException(page, ExceptionTypes.Exception, exception, retMessage);
        LogOut();
        throw exception; 
    }

    private static void LogOut()
    {
        BUser.ClearUserCach();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}