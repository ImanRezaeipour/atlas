using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
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

public partial class FromTime_ToTime_ReportParameter : GTSBasePage
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
        FromTime_ToTime_ReportParameter_onPageLoad,
        FromTime_ToTime_ReportParameter_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page FromTime_ToTime_ReportParameterFrom = this;

        Ajax.Utility.GenerateMethodScripts(FromTime_ToTime_ReportParameterFrom);
        this.SetReportParameterID_FromTime_ToTime_ReportParameter();
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
    private void SetReportParameterID_FromTime_ToTime_ReportParameter()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportParametersID"))
            this.ReportParameterID.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportParametersID"]);
    }
    public string GetParameterValue(decimal fileId, decimal uiParamerId, string actionId, string from, string to)
    {
        try
        {
            int fromTime = 0;
            int toTime = 0;
            fromTime = Utility.RealTimeToIntTime(from);
            toTime = Utility.RealTimeToIntTime(to);
            string result = String.Format("@fromTime={0};@toTime={1};", Utility.ToString(fromTime), Utility.ToString(toTime));
            return result;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    [Ajax.AjaxMethod("Register_FromTime_ToTime_ReportParameterPage", "Register_FromTime_ToTime_ReportParameterPage_onCallBack", null, null)]
    public string[] Register_FromTime_ToTime_ReportParameterPage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string from, string to)
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
            string fromTime = this.StringBuilder.CreateString(from);
            string toTime = this.StringBuilder.CreateString(to);


            RetValue = this.GetParameterValue(reportFileID, reportParameterID, ReportParameterActionID, fromTime, toTime);

            retMessage[0] = HttpContext.GetLocalResourceObject("~/FromTime_ToTime_ReportParameter.aspx", "RetSuccessType").ToString();
            retMessage[1] = HttpContext.GetLocalResourceObject("~/FromTime_ToTime_ReportParameter.aspx", "EditComplete").ToString();
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