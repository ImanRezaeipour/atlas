using GTS.Clock.Business.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Report;
using Stimulsoft.Report;
using System.Data;
using GTS.Clock.Business;
using GTS.Clock.Model;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business.Engine;

public partial class SystemReports : GTSBasePage
{
    public BSystemReports SystemReportsBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BSystemReports>();
        }
    }

    public BEngineCalculator EngineCalculateBusiness
    {
        get
        {
            return new BEngineCalculator();
        }
    }

    public BPerson PersonBusiness
    {
        get
        {
            return new BPerson();
        }
    }

    public BApplicationSettings ApplicationSettingsBusiness
    {
        get
        {
            return new BApplicationSettings();
        }
    }

    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    internal class RuleDebugPersonnelFeatures
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public bool IsRuleDebugActive { get; set; }
    }

    enum PageState
    {
        View,
        DeleteAll
    }

    internal class CurrentSystemReportTypeObj
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    enum Scripts
    {
        SystemReports_onPageLoad,
        DialogSystemReports_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridSystemReportType_SystemReports.IsCallback)
        {
            Page SystemReportsPage = this;
            Ajax.Utility.GenerateMethodScripts(SystemReportsPage);

            this.SetSystemReportTypePageSize_SystemReports(SystemReportType.SystemBusinessReport);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckSystemReportsLoadAccess_SystemReports();
            this.ViewCurrentCalendars_SystemReports();
            this.SetCurrentDate_SystemReports();
            this.Fill_cmbSystemReportType_SystemReports();
            this.GetPersonnelRuleDebugSettings_SystemReportsPage();
        }
    }

    private void SetCurrentDate_SystemReports()
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
        this.hfCurrentDate_SystemReports.Value = strCurrentDate;
    }

    private void GetPersonnelRuleDebugSettings_SystemReportsPage()
    {
        try
        {
            ApplicationSettings applicationSettings = BApplicationSettings.CurrentApplicationSettings;
            if (applicationSettings.PersonCodeForDebug != null && applicationSettings.PersonCodeForDebug != string.Empty)
            {
                Person person = this.PersonBusiness.GetByBarcode(applicationSettings.PersonCodeForDebug);
                RuleDebugPersonnelFeatures RDBF = new RuleDebugPersonnelFeatures()
                {
                    ID = person.ID,
                    Name = person.Name,
                    Barcode = person.BarCode,
                    IsRuleDebugActive = applicationSettings.RuleDebug
                };
                this.hfCurrentRuleDebugPersonnelFeatures_SystemReports.Value = this.JsSerializer.Serialize(RDBF);
            }
        }
        catch (Exception)
        {            
        }
    }

    private void CheckSystemReportsLoadAccess_SystemReports()
    {
        string[] retMessage = new string[4];
        try
        {
            this.SystemReportsBusiness.CheckSystemReportsLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void ViewCurrentCalendars_SystemReports()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_SystemReports.Visible = true;
                this.Container_pdpToDate_SystemReports.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_SystemReports.Visible = true;
                this.Container_gdpFromDate_SystemReports.Visible = true;
                break;
        }
    }


    private void SetSystemReportTypePageSize_SystemReports(SystemReportType SRT)
    {
        int PageSize = 0;
        switch (SRT)
        {
            case SystemReportType.SystemBusinessReport:
                PageSize = this.GridSystemBusinessReport_SystemReports.PageSize;
                break;
            case SystemReportType.SystemEngineReport:
                PageSize = this.GridSystemEngineReport_SystemReports.PageSize;
                break;
            case SystemReportType.SystemWindowsServiceReport:
                PageSize = this.GridSystemWindowsServiceReport_SystemReports.PageSize;
                break;
            case SystemReportType.SystemUserActionReport:
                PageSize = this.GridSystemUserActionReport_SystemReports.PageSize;
                break;

            case SystemReportType.SystemEngineDebugReport:
                PageSize = this.GridSystemEngineDebugReport_SystemReports.PageSize;
                break;
        }
        this.hfSystemReportTypePageSize_SystemReports.Value = PageSize.ToString();
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

    private void Fill_cmbSystemReportType_SystemReports()
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        foreach (SystemReportType systemReportTypeItem in Enum.GetValues(typeof(SystemReportType)))
        {
            ComboBoxItem cmbItemReportType = new ComboBoxItem(GetLocalResourceObject(systemReportTypeItem.ToString()).ToString());
            cmbItemReportType.Value = systemReportTypeItem.ToString();
            this.cmbSystemReportType_SystemReports.Items.Add(cmbItemReportType);
        }
        this.cmbSystemReportType_SystemReports.SelectedIndex = 0;
        this.hfCurrentSystemReportType_SystemReports.Value = this.JsSerializer.Serialize(new CurrentSystemReportTypeObj() {Text = this.cmbSystemReportType_SystemReports.SelectedItem.Text, Value = this.cmbSystemReportType_SystemReports.SelectedItem.Value });
    }

    protected void CallBack_GridSystemReportType_SystemReports_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        SystemReportType SRT = (SystemReportType)Enum.Parse(typeof(SystemReportType), this.StringBuilder.CreateString(e.Parameters[0]));
        int PageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
        int PageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture);
        SystemReportTypeFilterConditions SrtFilterConditions = this.JsSerializer.Deserialize<SystemReportTypeFilterConditions>(this.StringBuilder.CreateString(e.Parameters[3]));

        this.SetSystemReportsPageCount_SystemReports(SRT, PageSize, SrtFilterConditions);
        this.Fill_GridSystemReportType_SystemReports(SRT, PageSize, PageIndex, SrtFilterConditions);
        this.hfSystemReportTypePageCount_SystemReports.RenderControl(e.Output);
        this.ErrorHiddenField_GridSystemReportType_SystemReports.RenderControl(e.Output);
        this.RenderGridSystemReportType_SystemReports(SRT, e);
    }

    private void RenderGridSystemReportType_SystemReports(SystemReportType SRT, CallBackEventArgs e)
    {
        switch (SRT)
        {
            case SystemReportType.SystemBusinessReport:
                this.GridSystemBusinessReport_SystemReports.Visible = true;
                this.GridSystemBusinessReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemEngineReport:
                this.GridSystemEngineReport_SystemReports.Visible = true;
                this.GridSystemEngineReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemWindowsServiceReport:
                this.GridSystemWindowsServiceReport_SystemReports.Visible = true;
                this.GridSystemWindowsServiceReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemUserActionReport:
                this.GridSystemUserActionReport_SystemReports.Visible = true;
                this.GridSystemUserActionReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemEngineDebugReport:
                this.GridSystemEngineDebugReport_SystemReports.Visible = true;
                this.GridSystemEngineDebugReport_SystemReports.RenderControl(e.Output);
                break;
            case SystemReportType.SystemDataCollectorReport:
                this.GridSystemDataCollectorReport_SystemReports.Visible = true;
                this.GridSystemDataCollectorReport_SystemReports.RenderControl(e.Output);
                break;
        }
    }

    private void SetSystemReportsPageCount_SystemReports(SystemReportType SRT, int PageSize, SystemReportTypeFilterConditions SrtFilterConditions)
    {
        int SystemReportTypeCount = this.SystemReportsBusiness.GetSystemReportTypeCount(SRT, SrtFilterConditions);
        this.hfSystemReportTypePageCount_SystemReports.Value = Utility.GetPageCount(SystemReportTypeCount, PageSize).ToString();
    }

    private void Fill_GridSystemReportType_SystemReports(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            switch (SRT)
            {
                case SystemReportType.SystemBusinessReport:
                    IList<SystemBusinessReport> SystemBusinessReportList = this.SystemReportsBusiness.GetSystemBusinessReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemBusinessReport_SystemReports.DataSource = SystemBusinessReportList;
                    this.GridSystemBusinessReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemEngineReport:
                    IList<SystemEngineReport> SystemEngineReportList = this.SystemReportsBusiness.GetSystemEngineReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemEngineReport_SystemReports.DataSource = SystemEngineReportList;
                    this.GridSystemEngineReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemWindowsServiceReport:
                    IList<SystemWindowsServiceReport> SystemWindowsServiceReportList = this.SystemReportsBusiness.GetSystemWindowsServiceReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemWindowsServiceReport_SystemReports.DataSource = SystemWindowsServiceReportList;
                    this.GridSystemWindowsServiceReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemUserActionReport:
                    IList<SystemUserActionReport> SystemUserActionReportList = this.SystemReportsBusiness.GetSystemUserActionReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemUserActionReport_SystemReports.DataSource = SystemUserActionReportList;
                    this.GridSystemUserActionReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemEngineDebugReport:
                    IList<SystemEngineDebugReport> SystemEngineDebugReportList = this.SystemReportsBusiness.GetSystemEngineDebugReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemEngineDebugReport_SystemReports.DataSource = SystemEngineDebugReportList;
                    this.GridSystemEngineDebugReport_SystemReports.DataBind();
                    break;
                case SystemReportType.SystemDataCollectorReport:
                    IList<SystemDataCollectorReport> SystemDataCollectorReportList = this.SystemReportsBusiness.GetSystemDataCollectorReportList(SRT, PageSize, PageIndex, SrtFilterConditions);
                    this.GridSystemDataCollectorReport_SystemReports.DataSource = SystemDataCollectorReportList;
                    this.GridSystemDataCollectorReport_SystemReports.DataBind();
                    break;

            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GridSystemReportType_SystemReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateSystemReport_SystemReportsPage", "UpdateSystemReport_SystemReportsPage_onCallBack", null, null)]
    public string[] UpdateSystemReport_SystemReportsPage(string state, string systemReportType)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            PageState PS = (PageState)Enum.Parse(typeof(PageState), this.StringBuilder.CreateString(state));
            SystemReportType SRT = (SystemReportType)Enum.Parse(typeof(SystemReportType), this.StringBuilder.CreateString(systemReportType));
            switch (PS)
            {
                case PageState.DeleteAll:
                    switch (SRT)
                    {
                        case SystemReportType.SystemBusinessReport:
                            this.SystemReportsBusiness.DeleteAllSystemBusinessReport();
                            break;
                        case SystemReportType.SystemEngineReport:
                            this.SystemReportsBusiness.DeleteAllSystemEngineReport();
                            break;
                        case SystemReportType.SystemWindowsServiceReport:
                            this.SystemReportsBusiness.DeleteAllSystemWindowsServiceReport();
                            break;
                        case SystemReportType.SystemUserActionReport:
                            this.SystemReportsBusiness.DeleteAllSystemUserActionReport();
                            break;
                        case SystemReportType.SystemEngineDebugReport:
                            this.SystemReportsBusiness.DeleteAllEngineDebugReport();
                            break;
                        case SystemReportType.SystemDataCollectorReport:
                            this.SystemReportsBusiness.DeleteAllDataCollectorReport();
                            break;
                    }
                    break;
            }

            string action = "Delete All Records From " + SRT.ToString() + "";
            string objectInfo = GetLocalResourceObject("DeleteAllRecordsFrom").ToString() + " " + GetLocalResourceObject(SRT.ToString()).ToString();
            this.SystemReportsBusiness.InsertSystemReportCurrentUserActivity(this.Page, action, objectInfo);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (PS)
            {
                case PageState.DeleteAll:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
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
	[Ajax.AjaxMethod("GetReport_SystemReportsPage", "GetReport_SystemReportsPage_onCallBack", null, null)]
	public string[] GetReport_SystemReportsPage(string Srt, string StrFilterCondition)
	{
		this.InitializeCulture();
		string[] retMessage = new string[5];
		try
		{
            AttackDefender.CSRFDefender(this.Page);

			SystemReportType SRT = (SystemReportType)Enum.Parse(typeof(SystemReportType), this.StringBuilder.CreateString(Srt));
			SystemReportTypeFilterConditions SrtFilterConditions = this.JsSerializer.Deserialize<SystemReportTypeFilterConditions>(this.StringBuilder.CreateString(StrFilterCondition));
			string stiReportGUID = Guid.NewGuid().ToString();
			
			StiReport stiReport = this.SystemReportsBusiness.GetReport(SRT, SrtFilterConditions);

			Dictionary<string, StiReport> SysReportsDic = new Dictionary<string, StiReport>();

			if (Session["SysReports"] == null)
				Session.Add("SysReports", SysReportsDic);
			SysReportsDic = (Dictionary<string, StiReport>)Session["SysReports"];
			if (!SysReportsDic.Keys.Contains(stiReportGUID))
				SysReportsDic.Add(stiReportGUID, stiReport);
			Session["SysReports"] = SysReportsDic;

			string currentPage = "~/" + HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];
			retMessage[0] = HttpContext.GetLocalResourceObject(currentPage, "RetSuccessType").ToString();
			retMessage[1] = HttpContext.GetLocalResourceObject(currentPage, "EditComplete").ToString();
			retMessage[2] = "success";
			retMessage[3] = stiReportGUID;
			
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

    [Ajax.AjaxMethod("RegisterPersonnelRuleDebugSettings_SystemReportsPage", "RegisterPersonnelRuleDebugSettings_SystemReportsPage_onCallBack", null, null)]
    public string[] RegisterPersonnelRuleDebugSettings_SystemReportsPage(string PersonnelCode, string IsRuleDebugActive) 
    {
        this.InitializeCulture();
        string[] retMessage = new string[4];
        try
        {
            AttackDefender.CSRFDefender(this.Page);

            PersonnelCode = this.StringBuilder.CreateString(PersonnelCode);
            bool isRuleDebugActive = bool.Parse(this.StringBuilder.CreateString(IsRuleDebugActive));
            Person person = this.PersonBusiness.GetByBarcode(PersonnelCode);
            if (person == null)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("PersonnelNotExistsWithThisBarcode").ToString()), retMessage);
                return retMessage;
            }

            ApplicationSettings applicationSettings = BApplicationSettings.CurrentApplicationSettings;
            applicationSettings.RuleDebug = isRuleDebugActive;
            applicationSettings.PersonCodeForDebug = person.BarCode;
            ApplicationSettingsBusiness.SaveChanges(applicationSettings, UIActionType.EDIT);

            this.SystemReportsBusiness.DeleteAllEngineDebugReport();

            RuleDebugPersonnelFeatures RDBF = new RuleDebugPersonnelFeatures()
            {
                ID = person.ID,
                Name = person.Name,
                Barcode = person.BarCode,
                IsRuleDebugActive = isRuleDebugActive
            };

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = this.JsSerializer.Serialize(RDBF);

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

    [Ajax.AjaxMethod("Calculate_SystemReportsPage", "Calculate_SystemReportsPage_onCallBack", null, null)]
    public string[] Calculate_SystemReportsPage(string PersonnelID, string FromDate, string ToDate)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            bool ResultCalculate = false;
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);

            if(personnelID == 0)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("PersonnelNotExistsWithThisBarcode").ToString()), retMessage);
                return retMessage;
            }

            this.SystemReportsBusiness.DeleteAllEngineDebugReport();

            ResultCalculate = EngineCalculateBusiness.Calculate(personnelID, FromDate, ToDate, true);
            if (!ResultCalculate)
            {
                retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
                retMessage[1] = GetLocalResourceObject("CalculationsRunningError").ToString();
                retMessage[2] = "error";
                return retMessage;
            }

            int Progress = 0;
            int AllPersonnelCount = 0;
            int NotCalculatedPersonnelCount = 0;
            int ErrorCalculatedPersonnelCount = 0;
            while (Progress < 100 && ErrorCalculatedPersonnelCount == 0)
            {
                AllPersonnelCount = this.EngineCalculateBusiness.GetTotalCountInCalculating();
                NotCalculatedPersonnelCount = this.EngineCalculateBusiness.GetRemainCountInCalculating();
                ErrorCalculatedPersonnelCount = this.EngineCalculateBusiness.GetErrorCountInCalculating();
                int CalculatedPersonnelCount = AllPersonnelCount - NotCalculatedPersonnelCount;
                Progress = AllPersonnelCount > 0 ? Math.DivRem(CalculatedPersonnelCount * 100, AllPersonnelCount, out Progress) : 0;
            }
            if (ErrorCalculatedPersonnelCount > 0)
            {
                retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
                retMessage[1] = GetLocalResourceObject("CalculationsRunningError").ToString();
                retMessage[2] = "error";
                return retMessage;
            }
           
            if(Progress == 100)
               this.EngineCalculateBusiness.ClearTotalCountInCalculating();

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("CalculationsOperationComplete").ToString();
            retMessage[2] = "success";
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