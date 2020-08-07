using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Date_Time_ReportParameter : GTSBasePage
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
        Date_Time_ReportParameter_onPageLoad,
        Date_Time_ReportParameter_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page FromDate_ToDate_ReportParameterFrom = this;

        this.ViewCurrentLangCalendars_Date_Time_ReportParameter();
        Ajax.Utility.GenerateMethodScripts(FromDate_ToDate_ReportParameterFrom);
        this.SetCurrentDate_Date_Time_ReportParameter();
        this.SetReportParameterID_Date_Time_ReportParameter();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
    }
    private void SetCurrentDate_Date_Time_ReportParameter()
    {
        string strCurrentDate = string.Empty;

        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "en-US":
                strCurrentDate = DateTime.Now.ToShortDateString();
                break;
            case "fa-IR":
                strCurrentDate = this.LangProv.GetSysDateString(DateTime.Now);
                break;
        }
        this.hfCurrentDate_Date_Time_ReportParameter.Value = strCurrentDate;

    }
    private void ViewCurrentLangCalendars_Date_Time_ReportParameter()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_Date_Time_ReportParameter.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_Date_Time_ReportParameter.Visible = true;
                break;
        }
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
    private void SetReportParameterID_Date_Time_ReportParameter()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportParametersID"))
            this.ReportParameterID.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportParametersID"]);
    }
    public string GetParameterValue(decimal fileId, decimal uiParamerId, string actionId, string date, string time)
    {
        try
        {
            DateTime dateObj = new DateTime();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {

                dateObj = Utility.ToMildiDate(date);
            }
            else
            {
                dateObj = DateTime.Parse(date);
            }
            int timeObj = Utility.RealTimeToIntTime(time);
            string result = String.Format("@date={0};@time={1};", Utility.ToString(dateObj), Utility.ToString(timeObj));
            return result;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    [Ajax.AjaxMethod("Register_Date_Time_ReportParameterPage", "Register_Date_Time_ReportParameterPage_onCallBack", null, null)]
    public string[] Register_Date_Time_ReportParameterPage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string Date, string Time)
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            AttackDefender.CSRFDefender(this.Page);
            string RetValue = string.Empty;
            decimal reportParameterID = decimal.Parse(this.StringBuilder.CreateString(ReportParameterID), CultureInfo.InvariantCulture);
            ReportParameterActionID = this.StringBuilder.CreateString(ReportParameterActionID);
            decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID), CultureInfo.InvariantCulture);
            string date = this.StringBuilder.CreateString(Date);
            string time = this.StringBuilder.CreateString(Time);


            RetValue = this.GetParameterValue(reportFileID, reportParameterID, ReportParameterActionID, date, time);

            retMessage[0] = HttpContext.GetLocalResourceObject("~/DesktopModules/Atlas/Date_Time_ReportParameter.aspx", "RetSuccessType").ToString();
            retMessage[1] = HttpContext.GetLocalResourceObject("~/DesktopModules/Atlas/Date_Time_ReportParameter.aspx", "EditComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = RetValue;

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