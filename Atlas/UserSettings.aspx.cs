﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business.AppSettings;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Infrastructure;

public partial class UserSettings : GTSBasePage
{
    public BUserSettings UserSettingsBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BUserSettings>();
        }
    }

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
    public BDashboards DashboardBusiness
    {
        get
        {
            return new BDashboards();
        }
    }
    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public enum SettingsState
    {
        EmailSMS,
        Dashboard,
        CollectiveRequestRegistType
    }

    public enum EmailSMSTransferState
    {
        Daily,
        Hourly
    }

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
        }
    }

    public enum LoadState
    {
        Normal,
        Search,
        AdvancedSearch
    }

    public enum PersonnelCountState
    {
        Single,
        Group
    }

    public enum Caller
    {
        Personal,
        Management
    }

    public ISearchPerson PersonnelBusiness
    {
        get
        {
            return (ISearchPerson)(new BPerson());
        }
    }

    public class ObjEmailSMSSettingsDefaultTime
    {
        public string DailyDay
        {
            get
            {
                return "1";
            }
        }
        public string DailyTime
        {
            get
            {
                return "01:00";
            }
        }

        public string HourlyTime
        {
            get
            {
                return "00:00";
            }
        }
    }

    public ObjEmailSMSSettingsDefaultTime objEmailSMSSettingsDefaultTime
    {
        get
        {
            return new ObjEmailSMSSettingsDefaultTime();
        }
    }

    internal class EmailSMSSettingsObj
    {
        public string SettingType { get; set; }
        public string IsSendEmail { get; set; }
        public string IsSendSMS { get; set; }
        public string SendEmailState { get; set; }
        public string SendSMSState { get; set; }
        public string DailyEmailDay { get; set; }
        public string DailyEmailHour { get; set; }
        public string DailyEmailMinute { get; set; }
        public string HourlyEmailHour { get; set; }
        public string HourlyEmailMinute { get; set; }
        public string DailySMSDay { get; set; }
        public string DailySMSHour { get; set; }
        public string DailySMSMinute { get; set; }
        public string HourlySMSHour { get; set; }
        public string HourlySMSMinute { get; set; }
    }
    internal class DashboardSettingsObj
    {
        public string SettingType { get; set; }
        public string DashboardID1 { get; set; }

        public string DashboardID2 { get; set; }
        public string DashboardID3 { get; set; }
        public string DashboardID4 { get; set; }
        public string DashboardTitle1 { get; set; }

        public string DashboardTitle2 { get; set; }
        public string DashboardTitle3 { get; set; }
        public string DashboardTitle4 { get; set; }
    }
    internal class SettingsBatchObj
    {
        public string SettingType { get; set; }
        public string SettingTerms { get; set; }
    }
    internal class MonthlyOperationSchemaObj
    {
        public string Schema { get; set; }
    }
    internal class OperatorCollectiveRequestRegistType
    {
        public string SettingType { get; set; }
        public string CollectiveRequestRegistType { get; set; }
    }
    enum Scripts
    {
        UserSettings_onPageLoad,
        DialogUserSettings_Operations,
        HelpForm_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbPersonnel_UserSettings.IsCallback)
        {
            Page UserSettingsPage = this;
            Ajax.Utility.GenerateMethodScripts(UserSettingsPage);

            this.CheckUserSettingsLoadAccess_UserSettings();
            this.CustomizeUserSettings_UserSettings();
            //this.GetEmailSMSSettingsDefaultTime_UserSettings();
            this.SetPersonnelPageSize_cmbPersonnel_UserSettings();
            this.SetPersonnelPageCount_cmbPersonnel_UserSettings(LoadState.Normal, this.cmbPersonnel_UserSettings.DropDownPageSize, string.Empty);
            this.Fill_cmbSkins_SkinsSettings_UserSettings();
            this.Fill_cmbControls_EmailSMSSettings_UserSettings();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckUserSettingsLoadAccess_UserSettings()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
        {
            string[] retMessage = new string[4];
            try
            {
                Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));
                switch (caller)
                {
                    case Caller.Personal:
                        this.UserSettingsBusiness.CheckPersonnelUserSettingsLoadAccess();
                        break;
                    case Caller.Management:
                        this.UserSettingsBusiness.CheckManagementUserSettingsLoadAccess();
                        break;
                }
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            } 
        }
    }

    private void GetEmailSMSSettingsDefaultTime_UserSettings()
    {
        this.hfEmailSMSSettingsDefaultTime_UserSettings.Value = this.JsSerializer.Serialize(this.objEmailSMSSettingsDefaultTime);
    }

    void TlbSkinsSettings_ItemCommand(object sender, ComponentArt.Web.UI.ToolBarItemEventArgs e)
    {
        decimal SkinID = decimal.Parse(this.cmbSkins_SkinsSettings_UserSettings.SelectedItem.Id, CultureInfo.InvariantCulture);
        SkinHelper.SetCurrentSkin(SkinID);
    }

    [Ajax.AjaxMethod("ChangeSkin_UserSettingsPage", "ChangeSkin_UserSettingsPage_onCallBack", null, null)]
    public string[] ChangeSkin_UserSettingsPage(string SkinID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        decimal skinID = decimal.Parse(this.StringBuilder.CreateString(SkinID), CultureInfo.InvariantCulture);

        try
        {
            AttackDefender.CSRFDefender(this.Page);

            SkinHelper.SetCurrentSkin(skinID);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("SkinChangeComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = skinID.ToString();
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

    private void CustomizeUserSettings_UserSettings()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
        {
            Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));
            switch (caller)
            {
                case Caller.Personal:
                    this.Container_PersonnelSelect_UserSettings.Visible = false;
                    this.TlbUserSettings.Items[0].Visible = false;
                    break;
                case Caller.Management:                    
                    this.Container_SkinsSettings_UserSettings.Visible = false;
                    this.Container_MonthlyOperationReport_UserSettings.Visible = false;
                    this.TlbUserSettings.Items[0].Visible = true;
                    break;
            }
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

    private void SetPersonnelPageSize_cmbPersonnel_UserSettings()
    {
        this.hfPersonnelPageSize_UserSettings.Value = this.cmbPersonnel_UserSettings.DropDownPageSize.ToString();
    }
    private void SetPersonnelPageCount_cmbPersonnel_UserSettings(LoadState Ls, int pageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int PersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelCount = this.PersonnelBusiness.GetPersonCount();
                    break;
                case LoadState.Search:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                    break;
                default:
                    break;
            }
            this.hfPersonnelCount_UserSettings.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_UserSettings.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_UserSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_UserSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_UserSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbPersonnel_UserSettings_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.cmbPersonnel_UserSettings.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_UserSettings((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_UserSettings((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.cmbPersonnel_UserSettings.RenderControl(e.Output);
        this.hfPersonnelCount_UserSettings.RenderControl(e.Output);
        this.hfPersonnelPageCount_UserSettings.RenderControl(e.Output);
        this.hfPersonnelSelectedCount_UserSettings.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_UserSettings.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_UserSettings(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.GetAllPerson(pageIndex, pageSize);
                    hfPersonnelSelectedCount_UserSettings.Value = PersonnelBusiness.GetPersonInQuickSearchCount(string.Empty).ToString();
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm);
                    hfPersonnelSelectedCount_UserSettings.Value = PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm).ToString();
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                    hfPersonnelSelectedCount_UserSettings.Value = PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm)).ToString();
                    break;
            }
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                personCmbItem.Id = personItem.ID.ToString();
                this.cmbPersonnel_UserSettings.Items.Add(personCmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_UserSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_UserSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_UserSettings.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_cmbSkins_SkinsSettings_UserSettings()
    {
        IList<UISkin> SkinsList = SkinHelper.GetAllSkins();
        foreach (UISkin skinItem in SkinsList)
        {
            ComboBoxItem cmbSkinItem = new ComboBoxItem();
            string skinItemText = string.Empty;
            switch (this.LangProv.GetCurrentLanguage())
            {
                case "fa-IR":
                    skinItemText = skinItem.FnName;
                    break;
                case "en-US":
                    skinItemText = skinItem.EnName;
                    break;
            }
            cmbSkinItem.Text = skinItemText;
            cmbSkinItem.Value = skinItem.EnName;
            cmbSkinItem.Id = skinItem.ID.ToString();
            this.cmbSkins_SkinsSettings_UserSettings.Items.Add(cmbSkinItem);
        }
    }

    private void Fill_cmbControls_EmailSMSSettings_UserSettings()
    {
        for (int i = 1; i <= 7; i++)
        {
            ComboBoxItem cmbDayItem = new ComboBoxItem();
            cmbDayItem.Text = cmbDayItem.Value = i.ToString();

            this.cmbDay_DayEmail_EmailSMSSettings_UserSettings.Items.Add(cmbDayItem);
            this.cmbDay_DaySMS_EmailSMSSettings_UserSettings.Items.Add(cmbDayItem);
        }

        for (int j = 0; j <= 23; j++)
        {
            ComboBoxItem cmbHourItem = new ComboBoxItem();
            cmbHourItem.Text = cmbHourItem.Value = j.ToString().Length == 1 ? j.ToString().PadLeft(2, '0') : j.ToString();

            if (j != 0)
            {
                this.cmbHour_DayEmail_EmailSMSSettings_UserSettings.Items.Add(cmbHourItem);
                this.cmbHour_DaySMS_EmailSMSSettings_UserSettings.Items.Add(cmbHourItem);
            }
            this.cmbHour_HourEmail_EmailSMSSettings_UserSettings.Items.Add(cmbHourItem);
            this.cmbHour_HourSMS_EmailSMSSettings_UserSettings.Items.Add(cmbHourItem);
        }

        for (int k = 0; k <= 59; k++)
        {
            ComboBoxItem cmbMinuteItem = new ComboBoxItem();
            cmbMinuteItem.Text = cmbMinuteItem.Value = k.ToString().Length == 1 ? k.ToString().PadLeft(2, '0') : k.ToString();

            this.cmbMinute_DayEmail_EmailSMSSettings_UserSettings.Items.Add(cmbMinuteItem);
            this.cmbMinute_DaySMS_EmailSMSSettings_UserSettings.Items.Add(cmbMinuteItem);
            this.cmbMinute_HourEmail_EmailSMSSettings_UserSettings.Items.Add(cmbMinuteItem);
            this.cmbMinute_HourSMS_EmailSMSSettings_UserSettings.Items.Add(cmbMinuteItem);
        }
    }

    [Ajax.AjaxMethod("GetSettings_UserSettingsPage", "GetSettings_UserSettingsPage_onCallBack", null, null)]
    public string[] GetSettings_UserSettingsPage(string caller, string PersonnelID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        Caller SettingsCaller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(caller));
        decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
        List<object> SettingsBatchObjList = new List<object>();
        try
        {
            AttackDefender.CSRFDefender(this.Page);
            EmailSettings emailSettings = null;
            SMSSettings smsSettings = null;
            DashboardSettings dashboardSettingsObj = null;
            EmailSMSSettingsObj emailSMSSettingsObj = new EmailSMSSettingsObj();
            emailSMSSettingsObj.SettingType = "EmailSMS";
            CollectiveRequestRegistType collectiveRequestRegistTypeObj =CollectiveRequestRegistType.Business;
            switch (SettingsCaller)
            {
                case Caller.Personal:
                    emailSettings = this.UserSettingsBusiness.GetEmailSetting();
                    smsSettings = this.UserSettingsBusiness.GetSMSSetting();
                    dashboardSettingsObj = this.UserSettingsBusiness.GetDashboardSetting();
                    collectiveRequestRegistTypeObj = this.UserSettingsBusiness.GetOperatorCollectiveRequestRegistTypeSetting();
                    break;
                case Caller.Management:
                    if (personnelID != 0)
                    {
                        emailSettings = this.UserSettingsBusiness.GetEmailSetting(personnelID);
                        smsSettings = this.UserSettingsBusiness.GetSMSSetting(personnelID);
                        dashboardSettingsObj = this.UserSettingsBusiness.GetDashboardSetting(personnelID);
                        collectiveRequestRegistTypeObj = this.UserSettingsBusiness.GetOperatorCollectiveRequestRegistTypeSetting(personnelID);
                    }
                    break;
            }

            emailSMSSettingsObj.IsSendEmail = emailSettings != null ? emailSettings.Active.ToString().ToLower() : false.ToString().ToLower();
            emailSMSSettingsObj.SendEmailState = emailSettings != null ? emailSettings.SendByDay ? EmailSMSTransferState.Daily.ToString() : EmailSMSTransferState.Hourly.ToString() : EmailSMSTransferState.Daily.ToString();
            emailSMSSettingsObj.DailyEmailDay = emailSettings != null ? emailSettings.DayCount > 0 ? emailSettings.DayCount.ToString() : this.objEmailSMSSettingsDefaultTime.DailyDay : this.objEmailSMSSettingsDefaultTime.DailyDay;
            string DailyEmailTime = emailSettings != null ? emailSettings.TheDayHour != string.Empty ? emailSettings.TheDayHour : this.objEmailSMSSettingsDefaultTime.DailyTime : this.objEmailSMSSettingsDefaultTime.DailyTime;
            string HourlyEmailTime = emailSettings != null ? emailSettings.TheHour != string.Empty ? emailSettings.TheHour : this.objEmailSMSSettingsDefaultTime.HourlyTime : this.objEmailSMSSettingsDefaultTime.HourlyTime;
            Dictionary<string, string> DailyEmailTimeDic = this.GetTimeParts_UserSettings(DailyEmailTime);
            Dictionary<string, string> HourlyEmailTimeDic = this.GetTimeParts_UserSettings(HourlyEmailTime);
            emailSMSSettingsObj.DailyEmailHour = DailyEmailTimeDic["Hour"];
            emailSMSSettingsObj.DailyEmailMinute = DailyEmailTimeDic["Minute"];
            emailSMSSettingsObj.HourlyEmailHour = HourlyEmailTimeDic["Hour"];
            emailSMSSettingsObj.HourlyEmailMinute = HourlyEmailTimeDic["Minute"];

            emailSMSSettingsObj.IsSendSMS = smsSettings != null ? smsSettings.Active.ToString().ToLower() : false.ToString().ToLower();
            emailSMSSettingsObj.SendSMSState = smsSettings != null ? smsSettings.SendByDay ? EmailSMSTransferState.Daily.ToString() : EmailSMSTransferState.Hourly.ToString() : EmailSMSTransferState.Daily.ToString();
            emailSMSSettingsObj.DailySMSDay = smsSettings != null ? smsSettings.DayCount > 0 ? smsSettings.DayCount.ToString() : this.objEmailSMSSettingsDefaultTime.DailyDay : this.objEmailSMSSettingsDefaultTime.DailyDay;
            string DailySMSTime = smsSettings != null ? smsSettings.TheDayHour != string.Empty ? smsSettings.TheDayHour : this.objEmailSMSSettingsDefaultTime.DailyTime : this.objEmailSMSSettingsDefaultTime.DailyTime;
            string HourlySMSTime = smsSettings != null ? smsSettings.TheHour != string.Empty ? smsSettings.TheHour : this.objEmailSMSSettingsDefaultTime.HourlyTime : this.objEmailSMSSettingsDefaultTime.HourlyTime;
            Dictionary<string, string> DailySMSTimeDic = this.GetTimeParts_UserSettings(DailySMSTime);
            Dictionary<string, string> HourlySMSTimeDic = this.GetTimeParts_UserSettings(HourlySMSTime);
            emailSMSSettingsObj.DailySMSHour = DailySMSTimeDic["Hour"];
            emailSMSSettingsObj.DailySMSMinute = DailySMSTimeDic["Minute"];
            emailSMSSettingsObj.HourlySMSHour = HourlySMSTimeDic["Hour"];
            emailSMSSettingsObj.HourlySMSMinute = HourlySMSTimeDic["Minute"];

            SettingsBatchObjList.Add(emailSMSSettingsObj);

            DashboardSettingsObj dashboardObj = new DashboardSettingsObj();
            dashboardObj.SettingType = "Dashboard";
            if (dashboardSettingsObj != null)
            {
                dashboardObj.DashboardID1 = dashboardSettingsObj.Dashboard1 != null ? dashboardSettingsObj.Dashboard1.ID.ToString() : "0";
                dashboardObj.DashboardTitle1 = dashboardSettingsObj.Dashboard1 != null ? dashboardSettingsObj.Dashboard1.Title : "";

                dashboardObj.DashboardID2 = dashboardSettingsObj.Dashboard2 != null ? dashboardSettingsObj.Dashboard2.ID.ToString() : "0";
                dashboardObj.DashboardTitle2 = dashboardSettingsObj.Dashboard2 != null ? dashboardSettingsObj.Dashboard2.Title : "";

                dashboardObj.DashboardID3 = dashboardSettingsObj.Dashboard3 != null ? dashboardSettingsObj.Dashboard3.ID.ToString() : "0";
                dashboardObj.DashboardTitle3 = dashboardSettingsObj.Dashboard3 != null ? dashboardSettingsObj.Dashboard3.Title : "";

                dashboardObj.DashboardID4 = dashboardSettingsObj.Dashboard4 != null ? dashboardSettingsObj.Dashboard4.ID.ToString() : "0";
                dashboardObj.DashboardTitle4 = dashboardSettingsObj.Dashboard4 != null ? dashboardSettingsObj.Dashboard4.Title : "";
            }
            else
            {
                dashboardObj.DashboardID1 = "0";
                dashboardObj.DashboardTitle1 = "";

                dashboardObj.DashboardID2 = "0";
                dashboardObj.DashboardTitle2 = "";

                dashboardObj.DashboardID3 ="0";
                dashboardObj.DashboardTitle3 =  "";

                dashboardObj.DashboardID4 =  "0";
                dashboardObj.DashboardTitle4 = "";
            }
            SettingsBatchObjList.Add(dashboardObj);
            MonthlyOperationSchemaObj monthlyOperationSchemaObj = new MonthlyOperationSchemaObj();
            monthlyOperationSchemaObj.Schema = ((MonthlyOperationSchema)MonthlyOperationSchemaHelper.InitializeMonthlyOperationSchema(this.Page)).ToString();
            SettingsBatchObjList.Add(monthlyOperationSchemaObj);
            OperatorCollectiveRequestRegistType collectiveRequestTypeObj = new OperatorCollectiveRequestRegistType();
            collectiveRequestTypeObj.SettingType = "CollectiveRequestRegistType";
            collectiveRequestTypeObj.CollectiveRequestRegistType = collectiveRequestRegistTypeObj.ToString();
            SettingsBatchObjList.Add(collectiveRequestTypeObj);
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("OperationCompleted").ToString();
            retMessage[2] = "success";
            retMessage[3] = this.JsSerializer.Serialize(SettingsBatchObjList);
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

    private Dictionary<string, string> GetTimeParts_UserSettings(string strDefaultTime)
    {
        Dictionary<string, string> TimePartsDic = new Dictionary<string, string>();
        string[] TimeParts = strDefaultTime.Split(new char[] { ':' });
        string Hour = TimeParts[0].Length == 1 ? TimeParts[0].PadLeft(2, '0') : TimeParts[0];
        string Minute = TimeParts[1].Length == 1 ? TimeParts[1].PadLeft(2, '0') : TimeParts[1];
        TimePartsDic.Add("Hour", Hour);
        TimePartsDic.Add("Minute", Minute);
        return TimePartsDic;
    }

    [Ajax.AjaxMethod("UpdateSettings_UserSettingsPage", "UpdateSettings_UserSettingsPage_onCallBack", null, null)]
    public string[] UpdateSettings_UserSettingsPage(string caller, string settingsState, string personnelLoadState, string personnelCountState, string PersonnelID, string PersonnelSearchTerms, string SettingsTerms)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        Caller settingCaller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(caller));
        SettingsState SS = (SettingsState)Enum.Parse(typeof(SettingsState), this.StringBuilder.CreateString(settingsState));
        LoadState PLS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(personnelLoadState));
        PersonnelCountState PCS = (PersonnelCountState)Enum.Parse(typeof(PersonnelCountState), this.StringBuilder.CreateString(personnelCountState));
        decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
        PersonnelSearchTerms = this.StringBuilder.CreateString(PersonnelSearchTerms);
        SettingsTerms = this.StringBuilder.CreateString(SettingsTerms);
        

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            switch (SS)
            {
                case SettingsState.EmailSMS:
                    EmailSMSSettingsObj emailSMSSettingsObj = this.JsSerializer.Deserialize<EmailSMSSettingsObj>(SettingsTerms);
                    bool IsSendEmail = bool.Parse(emailSMSSettingsObj.IsSendEmail);
                    bool IsSendSMS = bool.Parse(emailSMSSettingsObj.IsSendSMS);
                    EmailSMSTransferState SendEmailState = (EmailSMSTransferState)Enum.Parse(typeof(EmailSMSTransferState), emailSMSSettingsObj.SendEmailState);
                    EmailSMSTransferState SendSMSState = (EmailSMSTransferState)Enum.Parse(typeof(EmailSMSTransferState), emailSMSSettingsObj.SendSMSState);
                    int EmailDay = int.Parse(emailSMSSettingsObj.DailyEmailDay, CultureInfo.InvariantCulture);
                    string DailyEmailTime = emailSMSSettingsObj.DailyEmailHour + ":" + emailSMSSettingsObj.DailyEmailMinute;
                    string HourlyEmailTime = emailSMSSettingsObj.HourlyEmailHour + ":" + emailSMSSettingsObj.HourlyEmailMinute;
                    int SMSDay = int.Parse(emailSMSSettingsObj.DailySMSDay, CultureInfo.InvariantCulture);
                    string DailySMSTime = emailSMSSettingsObj.DailySMSHour + ":" + emailSMSSettingsObj.DailySMSMinute;
                    string HourlySMSTime = emailSMSSettingsObj.HourlySMSHour + ":" + emailSMSSettingsObj.HourlySMSMinute;

                    EmailSettings emailSettings = new EmailSettings();
                    emailSettings.Active = IsSendEmail;
                    switch (SendEmailState)
                    {
                        case EmailSMSTransferState.Daily:
                            emailSettings.SendByDay = true;
                            break;
                        case EmailSMSTransferState.Hourly:
                            emailSettings.SendByDay = false;
                            break;
                    }
                    emailSettings.DayCount = EmailDay;
                    emailSettings.TheDayHour = DailyEmailTime;
                    emailSettings.TheHour = HourlyEmailTime;

                    SMSSettings smsSettings = new SMSSettings();
                    smsSettings.Active = IsSendSMS;
                    switch (SendSMSState)
                    {
                        case EmailSMSTransferState.Daily:
                            smsSettings.SendByDay = true;
                            break;
                        case EmailSMSTransferState.Hourly:
                            smsSettings.SendByDay = false;
                            break;
                    }
                    smsSettings.DayCount = SMSDay;
                    smsSettings.TheDayHour = DailySMSTime;
                    smsSettings.TheHour = HourlySMSTime;


                    switch (PCS)
                    {
                        case PersonnelCountState.Single:
                            switch (settingCaller)
	                        {
                                case Caller.Personal:
                                    this.UserSettingsBusiness.SaveEmailSetting(emailSettings);
                                    this.UserSettingsBusiness.SaveSMSSetting(smsSettings);
                                    break;
                                case Caller.Management:
                                    this.UserSettingsBusiness.SaveEmailSetting(emailSettings, personnelID);
                                    this.UserSettingsBusiness.SaveSMSSetting(smsSettings, personnelID);
                                    break;
                                default:
                                    break;
	                        }
                            break;
                        case PersonnelCountState.Group:
                            switch (PLS)
                            {
                                case LoadState.Normal:
                                    this.UserSettingsBusiness.SaveEmailSetting(emailSettings, personnelID);
                                    this.UserSettingsBusiness.SaveSMSSetting(smsSettings, personnelID);
                                    break;
                                case LoadState.Search:
                                    this.UserSettingsBusiness.SaveEmailSetting(emailSettings, PersonnelSearchTerms);
                                    this.UserSettingsBusiness.SaveSMSSetting(smsSettings, PersonnelSearchTerms);
                                    break;
                                case LoadState.AdvancedSearch:
                                    this.UserSettingsBusiness.SaveEmailSetting(emailSettings, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerms));
                                    this.UserSettingsBusiness.SaveSMSSetting(smsSettings, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerms));
                                    break;
                            }
                            break;
                    }
                    break;

                case SettingsState.Dashboard:
                    DashboardSettingsObj dashboardSettingsObj = this.JsSerializer.Deserialize<DashboardSettingsObj>(SettingsTerms);
                    IList<Dashboards> dashboardList = DashboardBusiness.GetAll();

                    decimal dashboardID1 = dashboardSettingsObj.DashboardID1 == "" ? 0 : Convert.ToDecimal(dashboardSettingsObj.DashboardID1,CultureInfo.InvariantCulture);
                    decimal dashboardID2 = dashboardSettingsObj.DashboardID2 == "" ? 0 : Convert.ToDecimal(dashboardSettingsObj.DashboardID2, CultureInfo.InvariantCulture);
                    decimal dashboardID3 = dashboardSettingsObj.DashboardID3 == "" ? 0 : Convert.ToDecimal(dashboardSettingsObj.DashboardID3, CultureInfo.InvariantCulture);
                    decimal dashboardID4 = dashboardSettingsObj.DashboardID4 == "" ? 0 : Convert.ToDecimal(dashboardSettingsObj.DashboardID4, CultureInfo.InvariantCulture);

                    DashboardSettings dashboardObj = new DashboardSettings();
                    dashboardObj.Dashboard1 = dashboardList.SingleOrDefault(d => d.ID == dashboardID1);
                    dashboardObj.Dashboard2 = dashboardList.SingleOrDefault(d => d.ID == dashboardID2);
                    dashboardObj.Dashboard3 = dashboardList.SingleOrDefault(d => d.ID == dashboardID3);
                    dashboardObj.Dashboard4 = dashboardList.SingleOrDefault(d => d.ID == dashboardID4);
                    
                switch (PCS)
                    {
                        case PersonnelCountState.Single:
                            switch (settingCaller)
                            {
                                case Caller.Personal:
                                    this.UserSettingsBusiness.SaveDashboardSetting(dashboardObj);
                                    
                                    break;
                                case Caller.Management:
                                    this.UserSettingsBusiness.SaveDashboardSetting(dashboardObj, personnelID);
                                   
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case PersonnelCountState.Group:
                            switch (PLS)
                            {
                                case LoadState.Normal:
                                    this.UserSettingsBusiness.SaveDashboardSetting(dashboardObj, personnelID);
                                    
                                    break;
                                case LoadState.Search:
                                    this.UserSettingsBusiness.SaveDashboardSetting(dashboardObj, PersonnelSearchTerms);
                                    
                                    break;
                                case LoadState.AdvancedSearch:
                                    this.UserSettingsBusiness.SaveDashboardSetting(dashboardObj, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerms));
                                    
                                    break;
                            }
                            break;
                    }
                    break;
                case SettingsState.CollectiveRequestRegistType:
                    OperatorCollectiveRequestRegistType operatorCollectiveRequestTypeObj = this.JsSerializer.Deserialize<OperatorCollectiveRequestRegistType>(SettingsTerms);
                    CollectiveRequestRegistType collectiveRequestRegistTypeObj = (CollectiveRequestRegistType)Enum.Parse(typeof(CollectiveRequestRegistType), operatorCollectiveRequestTypeObj.CollectiveRequestRegistType);

                    switch (PCS)
                    {
                        case PersonnelCountState.Single:
                            switch (settingCaller)
                            {
                                case Caller.Personal:
                                    this.UserSettingsBusiness.SaveOperatorCollectiveRequestRegistType(collectiveRequestRegistTypeObj);

                                    break;
                                case Caller.Management:
                                    this.UserSettingsBusiness.SaveOperatorCollectiveRequestRegistType(collectiveRequestRegistTypeObj, personnelID);

                                    break;
                                default:
                                    break;
                            }
                            break;
                        case PersonnelCountState.Group:
                            switch (PLS)
                            {
                                case LoadState.Normal:
                                    this.UserSettingsBusiness.SaveOperatorCollectiveRequestRegistType(collectiveRequestRegistTypeObj, personnelID);

                                    break;
                                case LoadState.Search:
                                    this.UserSettingsBusiness.SaveOperatorCollectiveRequestRegistType(collectiveRequestRegistTypeObj, PersonnelSearchTerms);

                                    break;
                                case LoadState.AdvancedSearch:
                                    this.UserSettingsBusiness.SaveOperatorCollectiveRequestRegistType(collectiveRequestRegistTypeObj, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerms));

                                    break;
                            }
                            break;
                    }
                    break;
            }
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("OperationCompleted").ToString();
            retMessage[2] = "success";
            retMessage[3] = SS.ToString();
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
    protected void CallBack_cmbDashboard_DashboradSettings_UserSettings_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        ComponentArt.Web.UI.CallBack callbackObj = (CallBack)sender;
        switch (callbackObj.ID)
        {
            case "CallBack_cmbDashboard1_DashboradSettings_UserSettings":
                this.cmbDashboard1_DashboradSettings_UserSettings.Dispose();
                this.Fill_cmbDashboard_DashboradSettings_UserSettings(cmbDashboard1_DashboradSettings_UserSettings,ErrorHiddenField_Dashboard1_DashboradSettings_UserSettings);
                this.ErrorHiddenField_Dashboard1_DashboradSettings_UserSettings.RenderControl(e.Output);
                this.cmbDashboard1_DashboradSettings_UserSettings.RenderControl(e.Output);
                break;
            case "CallBack_cmbDashboard2_DashboradSettings_UserSettings":
                this.cmbDashboard2_DashboradSettings_UserSettings.Dispose();
                this.Fill_cmbDashboard_DashboradSettings_UserSettings(cmbDashboard2_DashboradSettings_UserSettings,ErrorHiddenField_Dashboard2_DashboradSettings_UserSettings);
                this.ErrorHiddenField_Dashboard2_DashboradSettings_UserSettings.RenderControl(e.Output);
                this.cmbDashboard2_DashboradSettings_UserSettings.RenderControl(e.Output);
                break;
            case "CallBack_cmbDashboard3_DashboradSettings_UserSettings":
                this.cmbDashboard3_DashboradSettings_UserSettings.Dispose();
                this.Fill_cmbDashboard_DashboradSettings_UserSettings(cmbDashboard3_DashboradSettings_UserSettings,ErrorHiddenField_Dashboard3_DashboradSettings_UserSettings);
                this.ErrorHiddenField_Dashboard3_DashboradSettings_UserSettings.RenderControl(e.Output);
                this.cmbDashboard3_DashboradSettings_UserSettings.RenderControl(e.Output);
                break;
            case "CallBack_cmbDashboard4_DashboradSettings_UserSettings":
                this.cmbDashboard4_DashboradSettings_UserSettings.Dispose();
               this.Fill_cmbDashboard_DashboradSettings_UserSettings(cmbDashboard4_DashboradSettings_UserSettings,ErrorHiddenField_Dashboard4_DashboradSettings_UserSettings);
                this.ErrorHiddenField_Dashboard4_DashboradSettings_UserSettings.RenderControl(e.Output);
                this.cmbDashboard4_DashboradSettings_UserSettings.RenderControl(e.Output);
                break;
            default:
                break;
        }


        
    }

    private void Fill_cmbDashboard_DashboradSettings_UserSettings(ComboBox comboboxDashboard,HiddenField ErrorHiddenFieldDashboard)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            IList<GTS.Clock.Model.BaseInformation.Dashboards> dashboardList = DashboardBusiness.GetAll();
            comboboxDashboard.DataSource = dashboardList;
            comboboxDashboard.DataBind();
            comboboxDashboard.Items.Insert(0, new ComboBoxItem() { Value = "0", Text = GetLocalResourceObject("cmbDashboard_DashboradSettings_UserSettings_ItemNone").ToString() });

        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            ErrorHiddenFieldDashboard.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            ErrorHiddenFieldDashboard.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            ErrorHiddenFieldDashboard.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod(" ChangeMonthlyOperationSchema_UserSettingsPage", " ChangeMonthlyOperationSchema_UserSettingsPage_onCallBack", null, null)]
    public string[] ChangeMonthlyOperationSchema_UserSettingsPage(string schema)
    {
        this.InitializeCulture();
        string[] retMessage = new string[4];

        try
        {
            
            AttackDefender.CSRFDefender(this.Page);
            MonthlyOperationSchema uam = (MonthlyOperationSchema)Enum.Parse(typeof(MonthlyOperationSchema), this.StringBuilder.CreateString(schema));
             int Schema = Convert.ToInt32(uam);
             MonthlyOperationSchemaHelper.SetCurrentMounthlyOperationSchema(Schema);
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("MonthlyOperationSchemaChangeComplete").ToString();
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