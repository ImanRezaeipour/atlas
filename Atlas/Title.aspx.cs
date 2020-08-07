using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Title : GTSBasePage
{
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    enum Scripts
    {
        Title_onPageLoad,
        Title_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page TitleForm = this;
        Ajax.Utility.GenerateMethodScripts(TitleForm);
        this.SetReportParameterID_Title();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }

    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    private void SetCurrentCultureResObjs(string LangID)
    {
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    private void SetReportParameterID_Title()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportParametersID"))
            this.ReportParameterID.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportParametersID"]);
    }

    [Ajax.AjaxMethod("Register_TitlePage", "Register_TitlePage_onCallBack", null, null)]
    public string[] Register_TitlePage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string Title)
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal reportParameterID = decimal.Parse(this.StringBuilder.CreateString(ReportParameterID), CultureInfo.InvariantCulture);
            ReportParameterActionID = this.StringBuilder.CreateString(ReportParameterActionID);
            decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID), CultureInfo.InvariantCulture);
            Title = this.StringBuilder.CreateString(Title);

            retMessage[0] = HttpContext.GetLocalResourceObject("~/Title.aspx", "RetSuccessType").ToString();
            retMessage[1] = HttpContext.GetLocalResourceObject("~/Title.aspx", "EditComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = "@txtValue=" + Title + ";";

            return retMessage;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            return retMessage;
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            return retMessage;
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            return retMessage;
        }
    }


}