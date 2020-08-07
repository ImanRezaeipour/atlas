using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for MonthlyOperationSchema
/// </summary>
public class MonthlyOperationSchemaHelper
{
    public static BUserSettings UserSettingsBusiness
    {
        get
        {
            return new BUserSettings();
        }
    }

    public static ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }
    public MonthlyOperationSchemaHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static int InitializeMonthlyOperationSchema(Page page)
    {
        int Schema = GetCurrentMounthlyOperationSchema(page);
        return Schema;
    }
    public static void SetCurrentMounthlyOperationSchema(int Schema)
    {
        UserSettingsBusiness.SetCurrentMounthlyOperationSchema(Schema);
    }

    private static int GetCurrentMounthlyOperationSchema(Page page)
    {
        string[] retMessage = new string[4];
        int CurrentSchema = 0;
        try
        {
            CurrentSchema = BUserSettings.CurrentMonthlyOperationReportSchema;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = exceptionHandler.HandleException(page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            HttpContext.Current.Response.Redirect("WhitePage.aspx?Error=" + exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (UIBaseException ex)
        {
            retMessage = exceptionHandler.HandleException(page, ExceptionTypes.UIBaseException, ex, retMessage);
            HttpContext.Current.Response.Redirect("WhitePage.aspx?Error=" + exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (Exception ex)
        {
            retMessage = exceptionHandler.HandleException(page, ExceptionTypes.Exception, ex, retMessage);
            HttpContext.Current.Response.Redirect("WhitePage.aspx?Error=" + exceptionHandler.CreateErrorMessage(retMessage));
        }
        return CurrentSchema;
    }

}