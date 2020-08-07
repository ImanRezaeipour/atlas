using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using System.Collections;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Model.UI;
using GTS.Clock.Business.GridSettings;
using System.Reflection;
using GTS.Clock.Business;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using System.IO;

public partial class Kartable : GTSBasePage
{
    public enum RequestCaller
    {
        Kartable,
        Survey,
        Sentry,
        SpecialKartable,
        RequestSubstituteKartable
    }
    public enum Call
    {
        Kartable,
        SpecialKartable
    }
    public enum Applicant
    {
        PrivateNews,
        None
    }

    public BKartableGridClientSettings BkartableGridClientSettings
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BKartableGridClientSettings>();
        }
    }

    public IKartablRequests KartableBusiness
    {
        get
        {
            return (IKartablRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());

        }
    }

    public IReviewedRequests SurveyBusiness
    {
        get
        {
            return (IReviewedRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
        }
    }

    public BSentryPermits SentryBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BSentryPermits>();
        }
    }

    public BDepartment DepartmentBusiness
    {
        get
        {
            return new BDepartment();
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
    public BKartabl bKartable
    {
        get
        {
            return new BKartabl();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get
        {
            return new OperationYearMonthProvider();
        }
    }

    enum Scripts
    {
        DialogKartable_Operations,
        Kartable_onPageLoad,
        DialogKartableFilter_onPageLoad,
        DialogHistory_onPageLoad,
        DialogRequestsState_onPageLoad,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations,

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridKartable_Kartable.IsCallback && !this.CallBack_GridKartable_Kartable.CausedCallback && !this.CallBack_GridSettings_Kartable.IsCallback && !this.CallBack_trvParentDepartments_Kartable.IsCallback)
        {
            Page KartablePage = this;
            Ajax.Utility.GenerateMethodScripts(KartablePage);

            this.CheckLoadAccess_Kartable();
            this.Fill_DateControls_Kartable();
            this.CustomizeControls_Kartable();
            this.Fill_cmbSortBy_Kartable();
            this.SetRequestStatesStr_Kartable();
            this.SetRequestTypesStr_Kartable();
            this.SetRequestSourcesStr_Kartable();
            this.SetKartablePageSize_Personnel();
            this.CustomizeTlbKartable_Kartable();
            this.CustomizeTlbKartableFilter_Kartable();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckLoadAccess_Kartable()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
            {
                RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        this.KartableBusiness.CheckKartableLoadAccess();
                        break;
                    case RequestCaller.Survey:
                        this.SurveyBusiness.CheckSurveyedRequestsLoadAccess();
                        break;
                    case RequestCaller.Sentry:
                        this.SentryBusiness.CheckSentryLoadAccess();
                        break;
                    case RequestCaller.SpecialKartable:
                        this.KartableBusiness.CheckSpecialKartableLoadAccess();
                        break;
                    case RequestCaller.RequestSubstituteKartable:
                        this.KartableBusiness.CheckRequestSubstituteKartableLoadAccess();
                        break;
                }
            }
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void Fill_DateControls_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Survey || RC == RequestCaller.SpecialKartable || RC == RequestCaller.RequestSubstituteKartable)
            {
                this.Fill_cmbYear_Kartable();
                this.Fill_cmbMonth_Kartable();
            }
            if (RC == RequestCaller.Sentry)
                this.SetCurrentDate_Kartable();
        }
    }

    private void SetCurrentDate_Kartable()
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
        this.hfCurrentDate_Kartable.Value = strCurrentDate;
    }

    private void CustomizeControls_Kartable()
    {

        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            Applicant applicant = Applicant.None;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
            {
                applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));
            }
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if ((RC == RequestCaller.Kartable || RC == RequestCaller.Survey || RC == RequestCaller.SpecialKartable || RC == RequestCaller.RequestSubstituteKartable) && applicant == Applicant.None)
            {
                this.tblViewCordinateYearAndMonth_Kartable.Visible = true;
                this.tblViewCordinateDate_Kartable.Visible = true;
                ViewCurrentLangCalendars_Kartable(RC);
                if (RC == RequestCaller.Kartable || RC == RequestCaller.SpecialKartable || RC == RequestCaller.RequestSubstituteKartable)
                    this.SelectAllinthisPageBox_Kartable.Visible = true;
            }
            if (RC == RequestCaller.Sentry)
            {
                this.EndFlowRequestsViewBox_Kartable.Visible = true;
                this.tblDate_Kartable.Visible = true;
                this.ViewCurrentLangCalendars_Kartable(RC);
            }
            if (RC == RequestCaller.Kartable && applicant == Applicant.PrivateNews)
                this.SelectAllinthisPageBox_Kartable.Visible = true;
        }
    }

    private void ViewCurrentLangCalendars_Kartable(RequestCaller Rc)
    {
        switch (Rc)
        {
            case RequestCaller.Sentry:
                switch (this.LangProv.GetCurrentSysLanguage())
                {
                    case "fa-IR":
                        this.Container_pdpDate_Kartable.Visible = true;
                        break;
                    case "en-US":
                        this.Container_gdpDate_Kartable.Visible = true;
                        break;
                }
                break;
            case RequestCaller.Kartable:
            case RequestCaller.SpecialKartable:
            case RequestCaller.Survey:
            case RequestCaller.RequestSubstituteKartable:
                switch (this.LangProv.GetCurrentSysLanguage())
                {
                    case "fa-IR":
                        this.Container_pdpFromDate_Kartable.Visible = true;
                        this.Container_pdpToDate_Kartable.Visible = true;
                        break;
                    case "en-US":
                        this.Container_gdpFromDate_Kartable.Visible = true;
                        this.Container_gdpToDate_Kartable.Visible = true;
                        break;
                }
                break;
            default:
                break;
        }
    }

    private void SetKartablePageSize_Personnel()
    {
        this.hfKartablePageSize_Kartable.Value = this.GridKartable_Kartable.PageSize.ToString();
    }

    private void SetKartablePageCount_Kartable(CallBackEventArgs e)
    {
        RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0]));
        string LoadState = this.StringBuilder.CreateString(e.Parameters[1]);
        switch (LoadState)
        {
            case "CustomFilter":
                this.SetKartablePageCount_Kartable(LoadState, RC, int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
                break;
            case "Search":
                this.SetKartablePageCount_Kartable(LoadState, RC, int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]));
                break;
            default:
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        this.SetKartablePageCount_Kartable((RequestType)Enum.Parse(typeof(RequestType), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture));
                        break;
                    case RequestCaller.Survey:
                        this.SetKartablePageCount_Kartable((RequestState)Enum.Parse(typeof(RequestState), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture));
                        break;
                    case RequestCaller.Sentry:
                        this.SetKartablePageCount_Kartable((RequestType)Enum.Parse(typeof(RequestType), LoadState), this.StringBuilder.CreateString(e.Parameters[4]));
                        break;
                    case RequestCaller.SpecialKartable:
                        break;
                    case RequestCaller.RequestSubstituteKartable:
                        break;
                }
                break;
        }
    }

    private void SetKartablePageCount_Kartable(RequestType RT, int Year, int Month)
    {
        int KartableCount = this.KartableBusiness.GetRequestCount(RT, Year, Month);
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private void SetKartablePageCount_Kartable(RequestState RS, int Year, int Month)
    {
        int KartableCount = this.SurveyBusiness.GetRequestCount(RS, Year, Month);
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private void SetKartablePageCount_Kartable(RequestType RT, string date)
    {
        int KartableCount = this.SentryBusiness.GetPermitCount(RT, date);
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private void SetKartablePageCount_Kartable(string LoadState, RequestCaller RC, int year, int month, string date, string StrFilterConditions)
    {
        int KartableCount = 0;
        switch (LoadState)
        {
            case "CustomFilter":
                IList<RequestFliterProxy> CustomFilterList = GetKartableCustomFilterList_Kartable(StrFilterConditions);
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        KartableCount = this.KartableBusiness.GetRequestsByFilterCount(CustomFilterList);
                        break;
                    case RequestCaller.Survey:
                        //    KartableCount = this.SurveyBusiness.GetRequestsByFilterCount(CustomFilterList);
                        break;
                }
                break;
            case "Search":
                switch (RC)
                {
                    case RequestCaller.Kartable:
                        KartableCount = this.KartableBusiness.GetRequestCount(StrFilterConditions, year, month);
                        break;
                    case RequestCaller.Survey:
                        KartableCount = this.SurveyBusiness.GetRequestCount(StrFilterConditions, year, month);
                        break;
                    case RequestCaller.Sentry:
                        KartableCount = this.SentryBusiness.GetPermitCount(StrFilterConditions, date);
                        break;
                }
                break;
        }
        this.hfKartableCount_Kartable.Value = KartableCount.ToString();
        this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(KartableCount, this.GridKartable_Kartable.PageSize).ToString();
    }

    private IList<RequestFliterProxy> GetKartableCustomFilterList_Kartable(string StrRequestFliterProxyList)
    {
        IList<RequestFliterProxy> CustomFilterList = new List<RequestFliterProxy>();
        return CustomFilterList;
    }

    private void SetRequestStatesStr_Kartable()
    {
        string strRequestStates = string.Empty;
        foreach (RequestState requestStateItem in Enum.GetValues(typeof(RequestState)))
        {
            strRequestStates += "#" + GetLocalResourceObject(requestStateItem.ToString()).ToString() + ":" + ((int)requestStateItem).ToString();
        }
        this.hfRequestStates_Kartable.Value = strRequestStates;
    }

    private void SetRequestTypesStr_Kartable()
    {
        string strRequestTypes = string.Empty;
        foreach (RequestType requestTypeItem in Enum.GetValues(typeof(RequestType)))
        {
            strRequestTypes += "#" + GetLocalResourceObject(requestTypeItem.ToString()).ToString() + ":" + ((int)requestTypeItem).ToString();
        }
        this.hfRequestTypes_Kartable.Value = strRequestTypes;
    }

    private void SetRequestSourcesStr_Kartable()
    {
        string strRequestSources = string.Empty;
        foreach (RequestSource requestSourceItem in Enum.GetValues(typeof(RequestSource)))
        {
            strRequestSources += "#" + GetLocalResourceObject(requestSourceItem.ToString()).ToString() + ":" + ((int)requestSourceItem).ToString();
        }
        this.hfRequestSources_Kartable.Value = strRequestSources;
    }

    private void Fill_cmbYear_Kartable()
    {
        Applicant applicant = Applicant.None;
        int applicantSendedYear = 0;
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
        {
            applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));
            if (applicant == Applicant.PrivateNews)
            {
                DateTime lastRecordDate = DateTime.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LastRequestDate"]));
                applicantSendedYear = new System.Globalization.PersianCalendar().GetYear(lastRecordDate);
            }
        }
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_Kartable, this.hfCurrentYear_Kartable, applicantSendedYear);
    }

    private void Fill_cmbMonth_Kartable()
    {
        Applicant applicant = Applicant.None;
        int applicantSendedMonth = 0;
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
        {
            applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));
            if (applicant == Applicant.PrivateNews)
            {
                DateTime lastRecordDate = DateTime.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["LastRequestDate"]));
                applicantSendedMonth = new System.Globalization.PersianCalendar().GetMonth(lastRecordDate);
            }


        }
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_Kartable, this.hfCurrentMonth_Kartable, applicantSendedMonth);
    }

    private void Fill_cmbSortBy_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            ComboBoxItem cmbItemSortBy = null;
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Survey || RC == RequestCaller.SpecialKartable || RC == RequestCaller.RequestSubstituteKartable)
            {
                foreach (KartablOrderBy cartablOrderByItem in Enum.GetValues(typeof(KartablOrderBy)))
                {
                    cmbItemSortBy = new ComboBoxItem();
                    cmbItemSortBy.Text = GetLocalResourceObject(cartablOrderByItem.ToString()).ToString();
                    cmbItemSortBy.Value = cartablOrderByItem.ToString();
                    cmbItemSortBy.Id = ((int)cartablOrderByItem).ToString();
                    this.cmbSortBy_Kartable.Items.Add(cmbItemSortBy);
                }
            }
            if (RC == RequestCaller.Sentry)
            {
                foreach (SentryPermitsOrderBy sentryPermitsOrderByItem in Enum.GetValues(typeof(SentryPermitsOrderBy)))
                {
                    cmbItemSortBy = new ComboBoxItem();
                    cmbItemSortBy.Text = GetLocalResourceObject(sentryPermitsOrderByItem.ToString()).ToString();
                    cmbItemSortBy.Value = sentryPermitsOrderByItem.ToString();
                    cmbItemSortBy.Id = ((int)sentryPermitsOrderByItem).ToString();
                    this.cmbSortBy_Kartable.Items.Add(cmbItemSortBy);
                }
            }
            this.cmbSortBy_Kartable.SelectedIndex = 0;
            this.hfCurrentSortBy_Kartable.Value = this.cmbSortBy_Kartable.SelectedItem.Value;
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

    private void CustomizeTlbKartableFilter_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Sentry || RC == RequestCaller.SpecialKartable || RC == RequestCaller.RequestSubstituteKartable)
            {
                ToolBarItem tlbItemAllRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemAllRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemAllRequests_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemAllRequests_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemAllRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemAllRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemAllRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemAllRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemAllRequests_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemAllRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemAllRequests_TlbKartableFilter_Kartable.ImageUrl = "all.png";

                ToolBarItem tlbItemDailyRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemDailyRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemDailyRequests_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemDailyRequests_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemDailyRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemDailyRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemDailyRequests_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDailyRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemDailyRequests_TlbKartableFilter_Kartable.ImageUrl = "day.png";

                ToolBarItem tlbItemHourlyRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemHourlyRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemHourlyRequests_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemHourlyRequests_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemHourlyRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemHourlyRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemHourlyRequests_TlbKartableFilter_Kartable.ImageUrl = "clock.png";

                ToolBarItem tlbItemOverTimeJustification_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemOverTimeJustification_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemOverTimeJustification_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemOverTimeJustification_TlbKartableFilter_Kartable").ToString();
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemOverTimeJustification_TlbKartableFilter_Kartable_onClick();";
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemOverTimeJustification_TlbKartableFilter_Kartable.ImageUrl = "Permission.png";

                ToolBarItem tlbItemImperative_TlbKartableFilter_Kartable = new ToolBarItem();

                if (RC != RequestCaller.SpecialKartable)
                    tlbItemImperative_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemImperative_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemImperative_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemImperative_TlbKartableFilter_Kartable").ToString();
                tlbItemImperative_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemImperative_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemImperative_TlbKartableFilter_Kartable_onClick();";
                tlbItemImperative_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemImperative_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemImperative_TlbKartableFilter_Kartable.ImageUrl = "imperative.png";


                ToolBarItem tlbItemTerminate_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemTerminate_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemTerminate_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemTerminate_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemTerminate_TlbKartableFilter_Kartable").ToString();
                tlbItemTerminate_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemTerminate_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemTerminate_TlbKartableFilter_Kartable_onClick();";
                tlbItemTerminate_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemTerminate_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemTerminate_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemTerminate_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemTerminate_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemTerminate_TlbKartableFilter_Kartable.ImageUrl = "down.png";


                TlbKartableFilter_Kartable.Items.Add(tlbItemAllRequests_TlbKartableFilter_Kartable);

                if (RC != RequestCaller.RequestSubstituteKartable)
                {
                    TlbKartableFilter_Kartable.Items.Add(tlbItemDailyRequests_TlbKartableFilter_Kartable);
                    TlbKartableFilter_Kartable.Items.Add(tlbItemHourlyRequests_TlbKartableFilter_Kartable);
                    TlbKartableFilter_Kartable.Items.Add(tlbItemOverTimeJustification_TlbKartableFilter_Kartable);

                    if (RC != RequestCaller.Sentry)
                    {
                        TlbKartableFilter_Kartable.Items.Add(tlbItemTerminate_TlbKartableFilter_Kartable);
                        TlbKartableFilter_Kartable.Items.Add(tlbItemImperative_TlbKartableFilter_Kartable);
                    }
                }
            }
            if (RC == RequestCaller.Survey || RC == RequestCaller.SpecialKartable || RC == RequestCaller.RequestSubstituteKartable)
            {
                ToolBarItem tlbItemConfirmedRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemConfirmedRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemConfirmedRequests_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemConfirmedRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemConfirmedRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemConfirmedRequests_TlbKartableFilter_Kartable.ImageUrl = "save.png";

                ToolBarItem tlbItemRejectedRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemRejectedRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemRejectedRequests_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemRejectedRequests_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemRejectedRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemRejectedRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemRejectedRequests_TlbKartableFilter_Kartable.ImageUrl = "cancel.png";

                ToolBarItem tlbItemDeletedRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemDeletedRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemDeletedRequests_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemDeletedRequests_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemDeletedRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemDeletedRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemDeletedRequests_TlbKartableFilter_Kartable.ImageUrl = "remove.png";

                ToolBarItem tlbItemUnderReviewRequests_TlbKartableFilter_Kartable = new ToolBarItem();
                if (RC != RequestCaller.SpecialKartable)
                    tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.Text = GetLocalResourceObject("tlbItemUnderReviewRequests_TlbKartableFilter_Kartable").ToString();
                else
                    tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.ToolTip = GetLocalResourceObject("tlbItemUnderReviewRequests_TlbKartableFilter_Kartable").ToString();
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.ItemType = ToolBarItemType.Command;
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.ClientSideCommand = "tlbItemUnderReviewRequests_TlbKartableFilter_Kartable_onClick();";
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.TextImageSpacing = 5;
                tlbItemUnderReviewRequests_TlbKartableFilter_Kartable.ImageUrl = "waiting_flow.png";

                TlbKartableFilter_Kartable.Items.Add(tlbItemConfirmedRequests_TlbKartableFilter_Kartable);
                TlbKartableFilter_Kartable.Items.Add(tlbItemRejectedRequests_TlbKartableFilter_Kartable);
                if (RC != RequestCaller.RequestSubstituteKartable)
                    TlbKartableFilter_Kartable.Items.Add(tlbItemDeletedRequests_TlbKartableFilter_Kartable);
                if (RC == RequestCaller.SpecialKartable || RC == RequestCaller.RequestSubstituteKartable)
                    TlbKartableFilter_Kartable.Items.Add(tlbItemUnderReviewRequests_TlbKartableFilter_Kartable);
            }
        }
    }

    private void CustomizeTlbKartable_Kartable()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            switch (RC)
            {
                case RequestCaller.Kartable:
                case RequestCaller.RequestSubstituteKartable:
                    ToolBarItem tlbItemEndorsement_TlbKartable = new ToolBarItem();
                    tlbItemEndorsement_TlbKartable.Text = GetLocalResourceObject("tlbItemEndorsement_TlbKartable").ToString();
                    tlbItemEndorsement_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemEndorsement_TlbKartable.ClientSideCommand = "tlbItemEndorsement_TlbKartable_onClick();";
                    tlbItemEndorsement_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsement_TlbKartable.TextImageSpacing = 5;
                    tlbItemEndorsement_TlbKartable.ImageUrl = "save.png";

                    ToolBarItem tlbItemReject_TlbKartable = new ToolBarItem();
                    tlbItemReject_TlbKartable.Text = GetLocalResourceObject("tlbItemReject_TlbKartable").ToString();
                    tlbItemReject_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemReject_TlbKartable.ClientSideCommand = "tlbItemReject_TlbKartable_onClick();";
                    tlbItemReject_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemReject_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemReject_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemReject_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemReject_TlbKartable.TextImageSpacing = 5;
                    tlbItemReject_TlbKartable.ImageUrl = "cancel.png";

                    TlbKartable.Items.Add(tlbItemEndorsement_TlbKartable);
                    TlbKartable.Items.Add(tlbItemReject_TlbKartable);
                    break;
                case RequestCaller.Survey:
                    ToolBarItem tlbItemDelete_TlbKartable = new ToolBarItem();
                    tlbItemDelete_TlbKartable.Text = GetLocalResourceObject("tlbItemDelete_TlbKartable").ToString();
                    tlbItemDelete_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemDelete_TlbKartable.ClientSideCommand = "tlbItemDelete_TlbKartable_onClick();";
                    tlbItemDelete_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDelete_TlbKartable.TextImageSpacing = 5;
                    tlbItemDelete_TlbKartable.ImageUrl = "remove.png";

                    TlbKartable.Items.Add(tlbItemDelete_TlbKartable);
                    break;
                case RequestCaller.SpecialKartable:
                    ToolBarItem tlbItemEndorsementSpecial_TlbKartable = new ToolBarItem();
                    tlbItemEndorsementSpecial_TlbKartable.Text = GetLocalResourceObject("tlbItemEndorsement_TlbKartable").ToString();
                    tlbItemEndorsementSpecial_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemEndorsementSpecial_TlbKartable.ClientSideCommand = "tlbItemEndorsement_TlbKartable_onClick();";
                    tlbItemEndorsementSpecial_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsementSpecial_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsementSpecial_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsementSpecial_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemEndorsementSpecial_TlbKartable.TextImageSpacing = 5;
                    tlbItemEndorsementSpecial_TlbKartable.ImageUrl = "save.png";

                    ToolBarItem tlbItemRejectSpecial_TlbKartable = new ToolBarItem();
                    tlbItemRejectSpecial_TlbKartable.Text = GetLocalResourceObject("tlbItemReject_TlbKartable").ToString();
                    tlbItemRejectSpecial_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemRejectSpecial_TlbKartable.ClientSideCommand = "tlbItemReject_TlbKartable_onClick();";
                    tlbItemRejectSpecial_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemRejectSpecial_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemRejectSpecial_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemRejectSpecial_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemRejectSpecial_TlbKartable.TextImageSpacing = 5;
                    tlbItemRejectSpecial_TlbKartable.ImageUrl = "cancel.png";

                    TlbKartable.Items.Add(tlbItemEndorsementSpecial_TlbKartable);
                    TlbKartable.Items.Add(tlbItemRejectSpecial_TlbKartable);



                    ToolBarItem tlbItemDeleteSpecial_TlbKartable = new ToolBarItem();
                    tlbItemDeleteSpecial_TlbKartable.Text = GetLocalResourceObject("tlbItemDelete_TlbKartable").ToString();
                    tlbItemDeleteSpecial_TlbKartable.ItemType = ToolBarItemType.Command;
                    tlbItemDeleteSpecial_TlbKartable.ClientSideCommand = "tlbItemDelete_TlbKartable_onClick();";
                    tlbItemDeleteSpecial_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDeleteSpecial_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDeleteSpecial_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDeleteSpecial_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                    tlbItemDeleteSpecial_TlbKartable.TextImageSpacing = 5;
                    tlbItemDeleteSpecial_TlbKartable.ImageUrl = "remove.png";

                    TlbKartable.Items.Add(tlbItemDeleteSpecial_TlbKartable);
                    break;
            }

            ToolBarItem tlbItemHistory_TlbKartable = new ToolBarItem();
            tlbItemHistory_TlbKartable.Text = GetLocalResourceObject("tlbItemHistory_TlbKartable").ToString();
            tlbItemHistory_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemHistory_TlbKartable.ClientSideCommand = "tlbItemHistory_TlbKartable_onClick();";
            tlbItemHistory_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHistory_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHistory_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHistory_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHistory_TlbKartable.TextImageSpacing = 5;
            tlbItemHistory_TlbKartable.ImageUrl = "history.png";

            ToolBarItem tlbItemFilter_TlbKartable = new ToolBarItem();
            tlbItemFilter_TlbKartable.Text = GetLocalResourceObject("tlbItemFilter_TlbKartable").ToString();
            tlbItemFilter_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemFilter_TlbKartable.ClientSideCommand = "tlbItemFilter_TlbKartable_onClick();";
            tlbItemFilter_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFilter_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFilter_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFilter_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFilter_TlbKartable.TextImageSpacing = 5;
            tlbItemFilter_TlbKartable.ImageUrl = "filter.png";
            tlbItemFilter_TlbKartable.Enabled = false;

            ToolBarItem tlbItemHelp_TlbKartable = new ToolBarItem();
            tlbItemHelp_TlbKartable.Text = GetLocalResourceObject("tlbItemHelp_TlbKartable").ToString();
            tlbItemHelp_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemHelp_TlbKartable.ClientSideCommand = "tlbItemHelp_TlbKartable_onClick();";
            tlbItemHelp_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHelp_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHelp_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHelp_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemHelp_TlbKartable.TextImageSpacing = 5;
            tlbItemHelp_TlbKartable.ImageUrl = "help.gif";

            ToolBarItem tlbItemFormReconstruction_TlbKartable = new ToolBarItem();
            tlbItemFormReconstruction_TlbKartable.Text = GetLocalResourceObject("tlbItemFormReconstruction_TlbKartable").ToString();
            tlbItemFormReconstruction_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemFormReconstruction_TlbKartable.ClientSideCommand = "tlbItemFormReconstruction_TlbKartable_onClick();";
            tlbItemFormReconstruction_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemFormReconstruction_TlbKartable.TextImageSpacing = 5;
            tlbItemFormReconstruction_TlbKartable.ImageUrl = "refresh.png";


            ToolBarItem tlbItemExit_TlbKartable = new ToolBarItem();
            tlbItemExit_TlbKartable.Text = GetLocalResourceObject("tlbItemExit_TlbKartable").ToString();
            tlbItemExit_TlbKartable.ItemType = ToolBarItemType.Command;
            tlbItemExit_TlbKartable.ClientSideCommand = "tlbItemExit_TlbKartable_onClick();";
            tlbItemExit_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemExit_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemExit_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemExit_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
            tlbItemExit_TlbKartable.TextImageSpacing = 5;
            tlbItemExit_TlbKartable.ImageUrl = "exit.png";
            if (RC == RequestCaller.Kartable || RC == RequestCaller.SpecialKartable || RC == RequestCaller.Survey || RC == RequestCaller.RequestSubstituteKartable)
            {
                ToolBarItem tlbItemGridSetting_TlbKartable = new ToolBarItem();
                tlbItemGridSetting_TlbKartable.Text = GetLocalResourceObject("tlbItemGridSetting_TlbKartable").ToString();
                tlbItemGridSetting_TlbKartable.ItemType = ToolBarItemType.Command;
                tlbItemGridSetting_TlbKartable.ClientSideCommand = "tlbItemGridSetting_TlbKartable_onClick();";
                tlbItemGridSetting_TlbKartable.DropDownImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemGridSetting_TlbKartable.DropDownImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemGridSetting_TlbKartable.ImageHeight = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemGridSetting_TlbKartable.ImageWidth = System.Web.UI.WebControls.Unit.Pixel(16);
                tlbItemGridSetting_TlbKartable.TextImageSpacing = 5;
                tlbItemGridSetting_TlbKartable.ImageUrl = "package_settings.png";
                TlbKartable.Items.Add(tlbItemGridSetting_TlbKartable);
            }
            if (RC == RequestCaller.Kartable || RC == RequestCaller.Survey)
            {
                TlbKartable.Items.Add(tlbItemHistory_TlbKartable);
                //TlbKartable.Items.Add(tlbItemFilter_TlbKartable);
            }
            TlbKartable.Items.Add(tlbItemHelp_TlbKartable);
            TlbKartable.Items.Add(tlbItemFormReconstruction_TlbKartable);
            TlbKartable.Items.Add(tlbItemExit_TlbKartable);

        }

    }

    protected void CallBack_GridKartable_Kartable_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Customize_GridKartable_Kartable((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])));
        this.Fill_GridKartable_Kartable((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[4]), this.StringBuilder.CreateString(e.Parameters[5]), bool.Parse(this.StringBuilder.CreateString(e.Parameters[6])), this.StringBuilder.CreateString(e.Parameters[7]), int.Parse(this.StringBuilder.CreateString(e.Parameters[8]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[9]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[10]), this.StringBuilder.CreateString(e.Parameters[11]), this.StringBuilder.CreateString(e.Parameters[12]));
        //this.SetKartablePageCount_Kartable(e);
        this.GridKartable_Kartable.RenderControl(e.Output);
        this.hfKartableCount_Kartable.RenderControl(e.Output);
        this.hfKartablePageCount_Kartable.RenderControl(e.Output);
        this.ErrorHiddenField_Kartable.RenderControl(e.Output);
    }

    private void Fill_GridKartable_Kartable(RequestCaller RC, string LoadState, int year, int month, string date, string filterString, bool isEndFlowRequestsView, string sortBy, int pageSize, int pageIndex, string viewState, string fromDate, string toDate)
    {
        string[] retMessage = new string[4];
        IList<KartablProxy> KartableList = null;
        RequestType requestType = RequestType.None;
        RequestState requestState = RequestState.UnKnown;
        KartableGridClientSettings kartableGridClientSetting = new KartableGridClientSettings();
        int count = 0;
        GridSettingCaller Caller = GridSettingCaller.Kartable;
        Applicant applicant = Applicant.None;
        KartablSummaryItems itemSummary = KartablSummaryItems.UnKnown;
        try
        {
            switch (LoadState)
            {
                case "CustomFilter":
                    IList<RequestFliterProxy> CustomFilterList = this.GetKartableCustomFilterList_Kartable(filterString);
                    switch (RC)
                    {
                        case RequestCaller.Kartable:
                            KartableList = this.KartableBusiness.GetAllRequestsByFilter(CustomFilterList, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                        case RequestCaller.Survey:
                            Caller = GridSettingCaller.Survey;
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            //  KartableList = this.SurveyBusiness.GetAllRequestsByFilter(CustomFilterList, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy));
                            break;
                    }
                    break;
                case "Search":
                    switch (RC)
                    {
                        case RequestCaller.Kartable:
                            itemSummary = KartablSummaryItems.UnKnown;
                            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
                            {
                                applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));
                                if (applicant == Applicant.PrivateNews)
                                {
                                    itemSummary = (KartablSummaryItems)Enum.Parse(typeof(KartablSummaryItems), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["KeyApplicant"]));
                                }
                            }
                            KartableList = this.KartableBusiness.GetAllRequests(filterString, year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), out count, itemSummary, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                        case RequestCaller.Survey:
                            KartableList = this.SurveyBusiness.GetAllRequests(filterString, year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), out count, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            Caller = GridSettingCaller.Survey;
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                        case RequestCaller.Sentry:
                            KartableList = this.SentryBusiness.GetAllPermits(filterString, date, isEndFlowRequestsView, pageIndex, pageSize, (SentryPermitsOrderBy)Enum.Parse(typeof(SentryPermitsOrderBy), sortBy), out count);
                            break;
                        case RequestCaller.SpecialKartable:
                            if (!Enum.TryParse<RequestType>(LoadState, out requestType))
                                requestType = RequestType.None;
                            if (!Enum.TryParse<RequestState>(LoadState, out requestState))
                                requestState = RequestState.UnKnown;
                            count = this.KartableBusiness.GetAllSpecialKartableRequestsCount(requestType, requestState, year, month, filterString, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            KartableList = this.KartableBusiness.GetAllSpecialKartableRequests(requestType, requestState, year, month, pageIndex, pageSize, filterString, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            Caller = GridSettingCaller.SpecialKartable;
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                        case RequestCaller.RequestSubstituteKartable:
                            if (!Enum.TryParse<RequestState>(LoadState, out requestState))
                                requestState = RequestState.UnKnown;
                            count = this.KartableBusiness.GetAllRequestSubstituteKartableRequestsCount(requestState, year, month, filterString, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            KartableList = this.KartableBusiness.GetAllRequestSubstituteKartableRequests(requestState, year, month, pageIndex, pageSize, filterString, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            Caller = GridSettingCaller.RequestSubstituteKartable;
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                    }
                    break;
                default:
                    switch (RC)
                    {
                        case RequestCaller.Kartable:
                            itemSummary = KartablSummaryItems.UnKnown;
                            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
                            {
                                applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));
                                if (applicant == Applicant.PrivateNews)
                                {
                                    itemSummary = (KartablSummaryItems)Enum.Parse(typeof(KartablSummaryItems), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["KeyApplicant"]));
                                }
                            }
                            KartableList = this.KartableBusiness.GetAllRequests((RequestType)Enum.Parse(typeof(RequestType), LoadState), year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), out count, itemSummary, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                        case RequestCaller.Survey:
                            KartableList = this.SurveyBusiness.GetAllRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), out count, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            Caller = GridSettingCaller.Survey;
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                        case RequestCaller.Sentry:
                            KartableList = this.SentryBusiness.GetAllPermits((RequestType)Enum.Parse(typeof(RequestType), LoadState), date, isEndFlowRequestsView, pageIndex, pageSize, (SentryPermitsOrderBy)Enum.Parse(typeof(SentryPermitsOrderBy), sortBy), out count);
                            break;
                        case RequestCaller.SpecialKartable:
                            if (!Enum.TryParse<RequestType>(LoadState, out requestType))
                                requestType = RequestType.None;
                            if (!Enum.TryParse<RequestState>(LoadState, out requestState))
                                requestState = RequestState.UnKnown;
                            count = this.KartableBusiness.GetAllSpecialKartableRequestsCount(requestType, requestState, year, month, filterString, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            KartableList = this.KartableBusiness.GetAllSpecialKartableRequests(requestType, requestState, year, month, pageIndex, pageSize, filterString, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            Caller = GridSettingCaller.SpecialKartable;
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                        case RequestCaller.RequestSubstituteKartable:
                            if (!Enum.TryParse<RequestState>(LoadState, out requestState))
                                requestState = RequestState.UnKnown;
                            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
                            {
                                applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));
                                if (applicant == Applicant.PrivateNews)
                                {
                                    itemSummary = (KartablSummaryItems)Enum.Parse(typeof(KartablSummaryItems), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["KeyApplicant"]));
                                    switch (itemSummary)
                                    {
                                        case KartablSummaryItems.UnderReviewRequestSubstituteRequestsCount:
                                            requestState = RequestState.UnderReview;
                                            break;
                                    }
                                }
                            }
                            count = this.KartableBusiness.GetAllRequestSubstituteKartableRequestsCount(requestState, year, month, filterString, (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            KartableList = this.KartableBusiness.GetAllRequestSubstituteKartableRequests(requestState, year, month, pageIndex, pageSize, filterString, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), sortBy), (ViewState)Enum.Parse(typeof(ViewState), viewState), fromDate, toDate);
                            Caller = GridSettingCaller.RequestSubstituteKartable;
                            kartableGridClientSetting = bKartable.GetKartableGridClientSettings(Caller);
                            break;
                    }
                    break;
            }
            if (RC != RequestCaller.Sentry)
                this.operationYearMonthProvider.SetOperationYearMonth(year, month);
            if (kartableGridClientSetting != null)
                GetColumnGridKartable_Kartable(kartableGridClientSetting, RC);
            this.hfKartableCount_Kartable.Value = count.ToString();
            this.hfKartablePageCount_Kartable.Value = Utility.GetPageCount(count, this.GridKartable_Kartable.PageSize).ToString();
            this.GridKartable_Kartable.DataSource = KartableList;
            this.GridKartable_Kartable.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    private void GetColumnGridKartable_Kartable(KartableGridClientSettings kartableGridClientSetting, RequestCaller RC)
    {
        GridColumnCollection gridColumnCollection = new GridColumnCollection();
        switch (RC)
        {
            case RequestCaller.Kartable:

                gridColumnCollection = GridKartable_Kartable.Levels[0].Columns;
                for (int i = 0; i < gridColumnCollection.Count; i++)
                {
                    foreach (PropertyInfo PInfo in typeof(KartableGridClientSettings).GetProperties())
                    {
                        foreach (KartablGridSetting Setting in Enum.GetValues(typeof(KartablGridSetting)))
                        {
                            if (gridColumnCollection[i].DataField == PInfo.Name && PInfo.Name == Setting.ToString())
                            {
                                gridColumnCollection[i].Visible = (bool)PInfo.GetValue(kartableGridClientSetting, null);
                                break;
                            }
                        }
                    }
                }
                break;
            case RequestCaller.SpecialKartable:
                gridColumnCollection = GridKartable_Kartable.Levels[0].Columns;
                for (int i = 0; i < gridColumnCollection.Count; i++)
                {
                    foreach (PropertyInfo PInfo in typeof(KartableGridClientSettings).GetProperties())
                    {
                        foreach (SpecialKartablGridSetting Setting in Enum.GetValues(typeof(SpecialKartablGridSetting)))
                        {
                            if (gridColumnCollection[i].DataField == PInfo.Name && PInfo.Name == Setting.ToString())
                            {
                                gridColumnCollection[i].Visible = (bool)PInfo.GetValue(kartableGridClientSetting, null);
                                break;
                            }
                        }
                    }
                }
                break;
            case RequestCaller.Survey:
                gridColumnCollection = GridKartable_Kartable.Levels[0].Columns;
                for (int i = 0; i < gridColumnCollection.Count; i++)
                {
                    foreach (PropertyInfo PInfo in typeof(KartableGridClientSettings).GetProperties())
                    {
                        foreach (ServeyKartablGridSetting Setting in Enum.GetValues(typeof(ServeyKartablGridSetting)))
                        {
                            if (gridColumnCollection[i].DataField == PInfo.Name && PInfo.Name == Setting.ToString())
                            {
                                gridColumnCollection[i].Visible = (bool)PInfo.GetValue(kartableGridClientSetting, null);
                                break;
                            }
                        }
                    }
                }
                break;
            case RequestCaller.RequestSubstituteKartable:
                gridColumnCollection = GridKartable_Kartable.Levels[0].Columns;
                for (int i = 0; i < gridColumnCollection.Count; i++)
                {
                    foreach (PropertyInfo PInfo in typeof(KartableGridClientSettings).GetProperties())
                    {
                        foreach (RequestSubstituteKartableGridSetting Setting in Enum.GetValues(typeof(RequestSubstituteKartableGridSetting)))
                        {
                            if (gridColumnCollection[i].DataField == PInfo.Name && PInfo.Name == Setting.ToString())
                            {
                                gridColumnCollection[i].Visible = (bool)PInfo.GetValue(kartableGridClientSetting, null);
                                break;
                            }
                        }
                    }
                }
                break;
        }
    }
    private void Customize_GridKartable_Kartable(RequestCaller RC)
    {
        switch (RC)
        {
            case RequestCaller.Kartable:
                //this.GridKartable_Kartable.Levels[0].Columns[2].Visible = false;
                this.GridKartable_Kartable.Levels[0].Columns.Cast<GridColumn>().Where(x => x.DataField == "FlowStatus").SingleOrDefault().Visible = false;
                break;
            case RequestCaller.Survey:
                //this.GridKartable_Kartable.Levels[0].Columns[6].Visible = false;
                this.GridKartable_Kartable.Levels[0].Columns.Cast<GridColumn>().Where(x => x.DataField == "Select").SingleOrDefault().Visible = false;
                break;
            case RequestCaller.Sentry:
                //int[] hiddenSentryColumnsIndexList = new int[] { 2, 3, 5, 6, 15, 16, 17, 19, 20 };
                //foreach (int hiddenSentryColumnsIndexListItem in hiddenSentryColumnsIndexList)
                //{
                //    this.GridKartable_Kartable.Levels[0].Columns[hiddenSentryColumnsIndexListItem].Visible = false;
                //}
                string[] hiddenSentryColumnsDataFieldList = new string[] { "FlowStatus", "FlowLevels", "RequestSource", "Select", "TheDuration", "RegistrationDate", "OperatorUser", "AttachmentFile", "RequestHistory" };
                this.GridKartable_Kartable.Levels[0].Columns.Cast<GridColumn>().Where(x => hiddenSentryColumnsDataFieldList.Contains(x.DataField)).ToList().ForEach(x => x.Visible = false);
                break;
            case RequestCaller.SpecialKartable:
                break;
            case RequestCaller.RequestSubstituteKartable:
                //int[] hiddenRequestSubstituteKartableColumnsIndexList = new int[] { 5, 19, 20 };
                //foreach (int hiddenRequestSubstituteKartableColumnsIndexListItem in hiddenRequestSubstituteKartableColumnsIndexList)
                //{
                //    this.GridKartable_Kartable.Levels[0].Columns[hiddenRequestSubstituteKartableColumnsIndexListItem].Visible = false;
                //}
                string[] hiddenRequestSubstituteKartableColumnsDataFieldList = new string[] { "RequestSource", "AttachmentFile", "RequestHistory" };
                this.GridKartable_Kartable.Levels[0].Columns.Cast<GridColumn>().Where(x => hiddenRequestSubstituteKartableColumnsDataFieldList.Contains(x.DataField)).ToList().ForEach(x => x.Visible = false);
                break;
        }
    }

    [Ajax.AjaxMethod("UpdateKartable_KartablePage", "UpdateKartable_KartablePage_onCallBack", null, null)]
    public string[] UpdateKartable_KartablePage(string Caller, string PageState, string StrSelectedRequestsList, string ActionDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        string[] retValidationMessage = new string[4];

        bool State = false;
        ActionDescription = this.StringBuilder.CreateString(ActionDescription);
        IList<RequestKartablValidationProxy> requestValidationProxyList = new List<RequestKartablValidationProxy>();
        string WarningMessage = string.Empty;
        string splitter = " --- ";
        string space = " ";
        decimal applicatorID = BUser.CurrentUser.Person.ID;
        try
        {
            AttackDefender.CSRFDefender(this.Page);
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(Caller));
            RequestState RS = (RequestState)Enum.Parse(typeof(RequestState), this.StringBuilder.CreateString(PageState));
            IList<KartableSetStatusProxy> KartableSetStatusProxyList = this.CreateSelectedRequestsList_Kartable(RC, this.StringBuilder.CreateString(StrSelectedRequestsList));
            if (KartableSetStatusProxyList.Count == 0)
            {
                switch (RS)
                {
                    case RequestState.Confirmed:
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRequestSelectedforConfirm").ToString()), retMessage);
                        return retMessage;
                    case RequestState.Unconfirmed:
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRequestSelectedforReject").ToString()), retMessage);
                        return retMessage;
                    case RequestState.Deleted:
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRequestSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                }
            }
            switch (RC)
            {
                case RequestCaller.Kartable:
                    switch (RS)
                    {
                        case RequestState.Confirmed:
                            State = this.KartableBusiness.ConfirmRequest(KartableSetStatusProxyList, RS, ActionDescription, out requestValidationProxyList, applicatorID);
                            break;
                        case RequestState.Unconfirmed:
                            State = this.KartableBusiness.UnconfirmRequest(KartableSetStatusProxyList, RS, ActionDescription, out requestValidationProxyList, applicatorID);
                            break;
                    }
                    break;
                case RequestCaller.Survey:
                    SurveyBusiness.DeleteRequst(KartableSetStatusProxyList, ActionDescription, applicatorID);
                    State = true;
                    break;
                case RequestCaller.SpecialKartable:
                    switch (RS)
                    {
                        case RequestState.Confirmed:
                            State = KartableBusiness.ConfirmSpecialRequest(KartableSetStatusProxyList, RS, ActionDescription, out requestValidationProxyList, applicatorID);
                            break;
                        case RequestState.Unconfirmed:
                            State = this.KartableBusiness.UnconfirmSpecialRequest(KartableSetStatusProxyList, RS, ActionDescription, out requestValidationProxyList, applicatorID);
                            break;
                        case RequestState.Deleted:
                            this.KartableBusiness.DeleteSpecialRequest(KartableSetStatusProxyList, ActionDescription, out requestValidationProxyList, applicatorID, (Caller)Enum.Parse(typeof(Caller), "SpecialKartable"));
                            break;
                    }
                    break;
                case RequestCaller.RequestSubstituteKartable:
                    switch (RS)
                    {
                        case RequestState.Confirmed:
                            State = this.KartableBusiness.ConfirmRequestSubstituteRequest(KartableSetStatusProxyList, RS, ActionDescription, out requestValidationProxyList);
                            break;
                        case RequestState.Unconfirmed:
                            State = this.KartableBusiness.UnconfirmRequestSubstituteRequest(KartableSetStatusProxyList, RS, ActionDescription, out requestValidationProxyList);
                            break;
                    }
                    break;
            }
            foreach (RequestKartablValidationProxy requestValidationProxyItem in requestValidationProxyList)
            {
                retValidationMessage = new string[4];
                if (requestValidationProxyItem.Exception is UIValidationExceptions)
                {
                    retValidationMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, (UIValidationExceptions)requestValidationProxyItem.Exception, retValidationMessage);
                }
                else if (requestValidationProxyItem.Exception is UIBaseException)
                {
                    retValidationMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, (UIBaseException)requestValidationProxyItem.Exception, retValidationMessage);
                }
                else if (requestValidationProxyItem.Exception is Exception)
                {
                    retValidationMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, (Exception)requestValidationProxyItem.Exception, retValidationMessage);
                }

                retValidationMessage[1] = retValidationMessage[1].Replace("-", "");
                WarningMessage += GetLocalResourceObject("Request").ToString() + space + requestValidationProxyItem.PrecardName + space + GetLocalResourceObject("Date").ToString() + space + requestValidationProxyItem.Date + space + GetLocalResourceObject("Person").ToString() + space + requestValidationProxyItem.PersonName + " ( " + retValidationMessage[1] + " ) " + splitter;


            }
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (RS)
            {
                case RequestState.Confirmed:
                    SuccessMessageBody = GetLocalResourceObject("ConfirmComplete").ToString();
                    break;
                case RequestState.Unconfirmed:
                    SuccessMessageBody = GetLocalResourceObject("RejectComplete").ToString();
                    break;
                case RequestState.Deleted:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            if (WarningMessage != string.Empty)
            {
                retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                retMessage[1] = WarningMessage;
                retMessage[2] = "warning";
            }
            else
            {
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = State.ToString().ToLower();
            }

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

    private IList<KartableSetStatusProxy> CreateSelectedRequestsList_Kartable(RequestCaller RC, string StrSelectedRequestsList)
    {
        IList<KartableSetStatusProxy> KartableSetStatusProxyList = new List<KartableSetStatusProxy>();
        StrSelectedRequestsList = StrSelectedRequestsList.Replace("RID=", string.Empty).Replace("MFID=", string.Empty);
        if (RC == RequestCaller.RequestSubstituteKartable)
            StrSelectedRequestsList = StrSelectedRequestsList.Replace("RSID=", string.Empty);
        string[] SelectedRequestsCol = StrSelectedRequestsList.Split(new char[] { '#' });
        foreach (string SelectedRequestsColPart in SelectedRequestsCol)
        {
            if (SelectedRequestsColPart != string.Empty)
            {
                string[] SelectedRequestsColPartDetails = SelectedRequestsColPart.Split(new char[] { '%' });
                KartableSetStatusProxy kartableSetStatusProxy = new KartableSetStatusProxy();
                kartableSetStatusProxy.RequestID = decimal.Parse(SelectedRequestsColPartDetails[0], CultureInfo.InvariantCulture);
                kartableSetStatusProxy.ManagerFlowID = decimal.Parse(SelectedRequestsColPartDetails[1], CultureInfo.InvariantCulture);
                if (RC == RequestCaller.RequestSubstituteKartable)
                    kartableSetStatusProxy.RequestSubstituteID = decimal.Parse(SelectedRequestsColPartDetails[2], CultureInfo.InvariantCulture);
                KartableSetStatusProxyList.Add(kartableSetStatusProxy);
            }
        }
        return KartableSetStatusProxyList;
    }
    protected void CallBack_GridSettings_Kartable_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        string[] retMessage = new string[4];
        try
        {
            AttackDefender.CSRFDefender(this.Page);
            Dictionary<string, string> MasterColArray = this.CreateRecievedColumnsArray_Kartable(e.Parameters[0], "Get");
            Dictionary<string, string> SettingsColArray = null;
            GridSettingCaller RquestCaller = (GridSettingCaller)Enum.Parse(typeof(GridSettingCaller), this.StringBuilder.CreateString(e.Parameters[3]));
            switch (e.Parameters[1])
            {
                case "Get":
                    this.CheckLoadAccess_GridSettings_Kartable(RquestCaller);
                    IList<KartableGridClientSettingsProxy> GridClintSettingProxyList = this.BkartableGridClientSettings.GetKartableGridClientSettings(RquestCaller);
                    this.GridSettings_Kartable.DataSource = this.GetLocalResource(GridClintSettingProxyList);
                    this.GridSettings_Kartable.DataBind();
                    this.hfExist_GridSettings_Kartable.Value = GridClintSettingProxyList[0].Exist.ToString();
                    this.hfCurrentId_GridSettings_Kartable.Value = GridClintSettingProxyList[0].ID.ToString();
                    this.hfCurrentUserSettingId_GridSettings_Kartable.Value = GridClintSettingProxyList[0].CurrentUserSettingID.ToString();
                    break;
                case "Set":
                    if (e.Parameters[2] != string.Empty)
                        SettingsColArray = this.CreateRecievedColumnsArray_Kartable(e.Parameters[2], "Set");
                    this.BkartableGridClientSettings.UpdateGridSettings_Kartable(RquestCaller, MasterColArray, SettingsColArray, this.StringBuilder.CreateString(e.Parameters[4]), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[5]), CultureInfo.InvariantCulture), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[6]), CultureInfo.InvariantCulture));
                    break;
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GridSettings_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GridSettings_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GridSettings_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

        this.ErrorHiddenField_GridSettings_Kartable.RenderControl(e.Output);
        this.hfExist_GridSettings_Kartable.RenderControl(e.Output);
        this.hfCurrentId_GridSettings_Kartable.RenderControl(e.Output);
        this.hfCurrentUserSettingId_GridSettings_Kartable.RenderControl(e.Output);
        this.GridSettings_Kartable.RenderControl(e.Output);
    }
    private void CheckLoadAccess_GridSettings_Kartable(GridSettingCaller requestCaller)
    {
        switch (requestCaller)
        {
            case GridSettingCaller.Kartable:
                this.KartableBusiness.CheckKartablSettingLoadAccess();
                break;
            case GridSettingCaller.SpecialKartable:
                this.KartableBusiness.CheckSpecialKartableSettingLoadAccess();
                break;
            case GridSettingCaller.Survey:
                this.KartableBusiness.CheckSurveySettingLoadAccess();
                break;
            case GridSettingCaller.RequestSubstituteKartable:
                this.KartableBusiness.CheckRequestSubstituteKartableSettingLoadAccess();
                break;
        }
    }
    private IList<KartableGridClientSettingsProxy> GetLocalResource(IList<KartableGridClientSettingsProxy> GridClintSettingProxyList)
    {
        IList<KartableGridClientSettingsProxy> KartableGridClientSettingsList = new List<KartableGridClientSettingsProxy>();
        foreach (KartableGridClientSettingsProxy setting in GridClintSettingProxyList)
        {
            KartableGridClientSettingsProxy kartableGridClientSettingProxy = new KartableGridClientSettingsProxy();
            kartableGridClientSettingProxy.GridColumn = GetLocalResourceObject(setting.GridColumn).ToString();
            kartableGridClientSettingProxy.ViewState = setting.ViewState;
            kartableGridClientSettingProxy.ID = setting.ID;
            kartableGridClientSettingProxy.CurrentUserSettingID = setting.CurrentUserSettingID;
            kartableGridClientSettingProxy.Exist = setting.Exist;
            KartableGridClientSettingsList.Add(kartableGridClientSettingProxy);
        }
        return KartableGridClientSettingsList;
    }
    private Dictionary<string, string> CreateRecievedColumnsArray_Kartable(string RecievedStr, string State)
    {
        string[] ColStrArray = RecievedStr.Split(new char[] { ':' });
        Dictionary<string, string> ColArray = new Dictionary<string, string>();
        string[] ColStr;
        foreach (string Col in ColStrArray)
        {
            switch (State)
            {
                case "Get":
                    ColStr = Col.Split(new char[] { '%' });
                    ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), this.StringBuilder.CreateString(ColStr[1]));
                    break;
                case "Set":
                    ColStr = Col.Split(new char[] { '%' });
                    ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), ColStr[1]);
                    break;
            }
        }
        return ColArray;
    }
    //private void UpdateGridSettings_Kartable(GridSettingCaller requestCaller, Dictionary<string, string> MasterColArray, Dictionary<string, string> SettingsColArray, decimal CurrentSettingID)
    //{
    //    foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
    //    {
    //        KartableGridClientSettings kartablSettingGridSetting = new KartableGridClientSettings();
    //        switch (requestCaller)
    //        {
    //            case GridSettingCaller.Kartable:
    //                foreach (KartablGridSetting setting in Enum.GetValues(typeof(KartablGridSetting)))
    //                {
    //                    if (pInfo.Name == setting.ToString())
    //                    {
    //                        kartablSettingGridSetting.ID = CurrentSettingID;
    //                        pInfo.SetValue(kartablSettingGridSetting, SettingsColArray[MasterColArray[pInfo.Name]], null);
    //                    }
    //                }
    //                break;
    //            case GridSettingCaller.SpecialKartable:
    //                foreach (SpecialKartablGridSetting setting in Enum.GetValues(typeof(SpecialKartablGridSetting)))
    //                {
    //                    if (pInfo.Name == setting.ToString())
    //                    {
    //                        kartablSettingGridSetting.ID = CurrentSettingID;
    //                        pInfo.SetValue(kartablSettingGridSetting, SettingsColArray[MasterColArray[pInfo.Name]], null);
    //                    }
    //                }
    //                break;
    //            case GridSettingCaller.Survey:
    //                foreach (ServeyKartablGridSetting setting in Enum.GetValues(typeof(ServeyKartablGridSetting)))
    //                {
    //                    if (pInfo.Name == setting.ToString())
    //                    {
    //                        kartablSettingGridSetting.ID = CurrentSettingID;
    //                        pInfo.SetValue(kartablSettingGridSetting, SettingsColArray[MasterColArray[pInfo.Name]], null);
    //                    }
    //                }
    //                break;
    //        }
    //        //BkartableGridClientSettings.GridSettingSaveChanges(kartablSettingGridSetting, UIActionType.ADD);                                
    //        // BkartableGridClientSettings.SaveChanges_onPersonnelState(kartablSettingGridSetting, Business.UIActionType.EDIT);                                
    //    }
    //}

    protected void CallBack_trvParentDepartments_Kartable_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_trvParentDepartments_Kartable(decimal.Parse(this.StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture));
        this.ErrorHiddenField_ParentDepartments_Kartable.RenderControl(e.Output);
        this.trvParentDepartments_Kartable.RenderControl(e.Output);
    }

    private void Fill_trvParentDepartments_Kartable(decimal DepartmentID)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();

            IList<Department> DepartmentsList = this.DepartmentBusiness.GetAllDepartmentParents(DepartmentID);

            TreeViewNode trvNodeParentDepartment = null;

            foreach (Department department in DepartmentsList)
            {
                TreeViewNode trvNodeDepartment = new TreeViewNode();
                string trvNodeDepartmentText = string.Empty;
                if (department.Parent == null && GetLocalResourceObject("OrgNode_trvParentDepartments_Kartable") != null)
                    trvNodeDepartmentText = GetLocalResourceObject("OrgNode_trvParentDepartments_Kartable").ToString();
                else
                    trvNodeDepartmentText = department.Name;
                trvNodeDepartment.Text = trvNodeDepartmentText;
                trvNodeDepartment.ID = department.ID.ToString();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    trvNodeDepartment.ImageUrl = "Images/TreeView/folder.gif";
                if (trvNodeParentDepartment == null)
                    this.trvParentDepartments_Kartable.Nodes.Add(trvNodeDepartment);
                else
                    trvNodeParentDepartment.Nodes.Add(trvNodeDepartment);
                trvNodeDepartment.Expanded = true;
                trvNodeParentDepartment = trvNodeDepartment;
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ParentDepartments_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ParentDepartments_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ParentDepartments_Kartable.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


}


