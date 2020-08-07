using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Web.Script.Serialization;
using System.Collections;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;
using GTS.Clock.Model;
using System.Diagnostics;

public enum ExceptionTypes
{
    UIValidationExceptions,
    UIBaseException,
    Exception
}

/// <summary>
/// Summary description for ExceptionHandler
/// </summary>
public class ExceptionHandler
{
    public string CurrentCulture { get; set; }
    public string CurrentPage { get; set; }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }
    internal class WarningObj
    {
        public bool IsWarning { get; set; }
    }
    public JavaScriptSerializer jsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public string[] HandleException(Page currentPage, ExceptionTypes exceptionType, Exception ex, string[] messageCol)
    {
        string[] RetMessage = null;
        string page = string.Empty;
        bool ValidationExceptionsAvailable = false;
        if (currentPage != null)
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
                page = "/DesktopModules/Atlas/" + HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];
            else
            {
                if (HttpContext.Current.Request.Url != null)
                    page = "/DesktopModules/Atlas/" + HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
                else
                    page = "/DesktopModules/Atlas/" + currentPage.ToString().Replace("ASP.", "").Replace("_aspx", ".aspx");
            }

            this.CurrentCulture = this.LangProv.GetCurrentLanguage();
        }
        else
            page = "/DesktopModules/Atlas/" + this.CurrentPage;

        CultureInfo cultureInfo = new CultureInfo(this.CurrentCulture);

        switch (exceptionType)
        {
            case ExceptionTypes.UIValidationExceptions:
                messageCol[0] = HttpContext.GetLocalResourceObject(page, "RetErrorType", cultureInfo).ToString();
                if (ex is UIValidationExceptions)
                {
                    if (((UIValidationExceptions)ex).ExceptionList.Any(x => !(x is ValidationWarning)))
                    {
                        ValidationExceptionsAvailable = true;
                    }
                    foreach (ValidationException exception in ((UIValidationExceptions)ex).ExceptionList)
                    {
                        if (HttpContext.GetLocalResourceObject(page, exception.ResourceKey.ToString(), cultureInfo) != null)
                        {
                            string exceptionData = exception.Data != null && exception.Data.Keys.Count > 0 && exception.Data["Info"] != null ? " " + exception.Data["Info"].ToString() : string.Empty;
                            messageCol[1] += HttpContext.GetLocalResourceObject(page, exception.ResourceKey.ToString(), cultureInfo).ToString() + exceptionData + " --- ";
                        }
                        else
                            messageCol[1] += exception.Message + " --- ";
                        messageCol[2] = "error";

                        if (exception is ValidationWarning && !ValidationExceptionsAvailable)
                        {
                            WarningObj warningObj = new WarningObj() { IsWarning = true };
                            messageCol[messageCol.Length - 1] = this.jsSerializer.Serialize(warningObj);
                        }
                        else
                        {
                            if (exception is ValidationWarning && ValidationExceptionsAvailable)
                            {
                                WarningObj warningObj = new WarningObj() { IsWarning = false };
                                messageCol[messageCol.Length - 1] = this.jsSerializer.Serialize(warningObj);
                            }
                        }
                    }
                }
                else
                {
                    messageCol[1] += ex.Message + " --- ";
                    messageCol[2] = "error";
                }
                RetMessage = messageCol;
                break;
            case ExceptionTypes.UIBaseException:
                string ErrorType = "RetErrorType";
                string ErrorBody = ((UIBaseException)ex).Message;
                if (((UIBaseException)ex).ExceptionType == UIExceptionTypes.Fatal)
                {
                    ErrorType = "RetFatalErrorType";
                    string fatalExceptionIdentifier = ((UIBaseException)ex).FatalExceptionIdentifier.ToString();
                    if (HttpContext.GetLocalResourceObject(page, ((UIBaseException)ex).FatalExceptionIdentifier.ToString(), cultureInfo) != null)
                        fatalExceptionIdentifier = HttpContext.GetLocalResourceObject(page, ((UIBaseException)ex).FatalExceptionIdentifier.ToString(), cultureInfo).ToString();
                    ErrorBody = fatalExceptionIdentifier + " " + HttpContext.GetLocalResourceObject(page, "RetFatalErrorIdentifier", cultureInfo).ToString() + " " + ((int)((UIBaseException)ex).FatalExceptionIdentifier).ToString();
                }

                messageCol[0] = HttpContext.GetLocalResourceObject(page, ErrorType, cultureInfo).ToString();
                messageCol[1] += ErrorBody;
                messageCol[2] = "error";
                messageCol[3] = ((UIBaseException)ex).ExceptionType.ToString();
                RetMessage = messageCol;
                break;
            case ExceptionTypes.Exception:
                messageCol[0] = HttpContext.GetLocalResourceObject(page, "RetErrorType", cultureInfo).ToString();
                messageCol[1] += HttpContext.GetLocalResourceObject(page, "OperationError", cultureInfo).ToString();
                messageCol[2] = "error";
                RetMessage = messageCol;
                break;
            default:
                break;
        }
        messageCol[1] = messageCol[1].Replace("\n", string.Empty).Replace("\r", string.Empty);

        this.LogException(ex, page);

        return RetMessage;
    }

    public void ApiHandleException(string ControllerName, Exception ex)
    {
        this.LogException(ex, ControllerName);
    }

    public string[] HandleException(Page currentPage, Exception ex, string[] messageCol)
    {
        string[] RetMessage = null;
        string page = string.Empty;
        if (currentPage != null)
        {
            page = "~/" + currentPage.ToString().Replace("ASP.", "").Replace("_aspx", ".aspx");
            this.CurrentCulture = this.LangProv.GetCurrentLanguage();
        }
        else
            page = "~/" + this.CurrentPage;

        CultureInfo cultureInfo = new CultureInfo(this.CurrentCulture);
        messageCol[0] = HttpContext.GetLocalResourceObject(page, "RetErrorType", cultureInfo).ToString();
        messageCol[1] += HttpContext.GetLocalResourceObject(page, "OperationError", cultureInfo).ToString();
        messageCol[2] = "error";
        messageCol[3] = "Reload";
        RetMessage = messageCol;

        this.LogException(ex, page);

        return RetMessage;
    }

    private void LogException(Exception ex, string page)
    {
        if (ex.StackTrace != null)
            BaseBusiness<Entity>.LogException(ex, ex.StackTrace.ToString());
        else
            BaseBusiness<Entity>.LogException(ex, page);
    }

    public string CreateErrorMessage(string[] messageCol)
    {
        string retMessage = this.jsSerializer.Serialize(messageCol);
        return retMessage;
    }




}