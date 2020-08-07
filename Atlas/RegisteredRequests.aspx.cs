using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business;
using GTS.Clock.Model;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using GTS.Clock.Business.Security;
using System.Web.Configuration;

public partial class RegisteredRequests : GTSBasePage
{
    public enum Applicant
    {
        PrivateNews,
        None
    }
    public IRegisteredRequests RegisteredRequestsBusiness
    {
        get
        {
            return (IRegisteredRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
        }
    }

    public BRequest MasterRequestBusiness
    {
        get
        {
            return new BRequest();
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

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    enum CurrentUserState
    {
        NormalUser,
        Operator
    }

    public enum LoadState
    {
        Normal,
        Search,
        AdvancedSearch
    }

    internal class PersonnelDetails
    {
        public string ID { get; set; }
        public string OrganizationPostID { get; set; }
        public string OrganizationPostName { get; set; }
    }

    public JavaScriptSerializer JsSeializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public ISearchPerson PersonnelBusiness
    {
        get
        {
            return (ISearchPerson)(new BPerson());
        }
    }

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
        }
    }

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get
        {
            return new OperationYearMonthProvider();
        }
    }

    enum RegisteredRequestsCaller
    {
        MainPage,
        MonthlyOperationGridSchema,
        MonthlyOperationGanttChartSchema
    }

    enum Scripts
    {
        RegisteredRequests_onPageLoad,
        DialogRegisteredRequests_Operations,
        DialogRequestRegister_onPageLoad,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridRegisteredRequests_RegisteredRequests.IsCallback && !CallBack_cmbRequestType_RegisteredRequests.IsCallback && !CallBack_cmbExporter_RegisteredRequests.IsCallback && !CallBack_cmbPersonnel_RegisteredRequests.IsCallback)
        {
            Page RegisteredRequestsPage = this;
            Ajax.Utility.GenerateMethodScripts(RegisteredRequestsPage);

            this.CheckRegisteredRequestsLoadAccess_RegisteredRequests();
            this.ViewCurrentLangCalendars_RegisteredRequests();
            this.Customize_TlbRegisteredRequests_RegisteredRequests(this.GetCurrentUserState_RegisteredRequests());
            this.Fill_cmbYear_RegisteredRequests();
            this.Fill_cmbMonth_RegisteredRequests();
            this.SetRequestStatesStr_RegisteredRequests();
            this.SetRequestTypesStr_RegisteredRequests();
            this.SetRegisteredRequestsPageSize_RegisteredRequests();
            this.SetPersonnelPageSize_cmbPersonnel_RegisteredRequests();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckRegisteredRequestsLoadAccess_RegisteredRequests()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
            {
                RegisteredRequestsCaller RRC = (RegisteredRequestsCaller)Enum.Parse(typeof(RegisteredRequestsCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));
                switch (RRC)
                {
                    case RegisteredRequestsCaller.MainPage:
                        this.RegisteredRequestsBusiness.CheckRegisteredRequestsLoadAccess_onMainPage();
                        break;
                    case RegisteredRequestsCaller.MonthlyOperationGridSchema:
                        this.RegisteredRequestsBusiness.CheckRegisteredRequestsLoadAccess_onMonthlyOperationGridSchema();
                        break;
                    case RegisteredRequestsCaller.MonthlyOperationGanttChartSchema:
                        this.RegisteredRequestsBusiness.CheckRegisteredRequestsLoadAccess_onMonthlyOperationGanttChartSchema();
                        break;
                }
            }
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void ViewCurrentLangCalendars_RegisteredRequests()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_RegisteredRequests.Visible = true;
                this.Container_pdpToDate_RegisteredRequests.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_RegisteredRequests.Visible = true;
                this.Container_gdpToDate_RegisteredRequests.Visible = true;
                break;
        }
    }

    private CurrentUserState GetCurrentUserState_RegisteredRequests()
    {
        bool IsOperator = this.RegisteredRequestsBusiness.IsCurrentUserOperator;
        CurrentUserState CUS = CurrentUserState.NormalUser;
        if (IsOperator)
            CUS = CurrentUserState.Operator;
        else
            CUS = CurrentUserState.NormalUser;

        CurrentUserState_RegisteredRequests.Value = CUS.ToString();
        return CUS;
    }

    private void Customize_TlbRegisteredRequests_RegisteredRequests(CurrentUserState CUS)
    {
        ToolBarItem tlbItemInsert_TlbRegisteredRequests = new ToolBarItem();
        tlbItemInsert_TlbRegisteredRequests.ID = "tlbItemInsert_TlbRegisteredRequests";
        tlbItemInsert_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemInsert_TlbRegisteredRequests").ToString();
        tlbItemInsert_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemInsert_TlbRegisteredRequests.ClientSideCommand = "tlbItemInsert_TlbRegisteredRequests_onClick();";
        tlbItemInsert_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemInsert_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemInsert_TlbRegisteredRequests.ImageUrl = "add.png";

        ToolBarItem tlbItemDelete_TlbRegisteredRequests = new ToolBarItem();
        tlbItemDelete_TlbRegisteredRequests.ID = "tlbItemDelete_TlbRegisteredRequests";
        tlbItemDelete_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemDelete_TlbRegisteredRequests").ToString();
        tlbItemDelete_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemDelete_TlbRegisteredRequests.ClientSideCommand = "tlbItemDelete_TlbRegisteredRequests_onClick();";
        tlbItemDelete_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemDelete_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemDelete_TlbRegisteredRequests.ImageUrl = "remove.png";

        ToolBarItem tlbItemFilter_TlbRegisteredRequests = new ToolBarItem();
        tlbItemFilter_TlbRegisteredRequests.ID = "tlbItemFilter_TlbRegisteredRequests";
        tlbItemFilter_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemFilter_TlbRegisteredRequests").ToString();
        tlbItemFilter_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemFilter_TlbRegisteredRequests.ClientSideCommand = "tlbItemFilter_TlbRegisteredRequests_onClick();";
        tlbItemFilter_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemFilter_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemFilter_TlbRegisteredRequests.ImageUrl = "filter.png";

        ToolBarItem tlbItemRequestByOperator_TlbRegisteredRequests = new ToolBarItem();
        tlbItemRequestByOperator_TlbRegisteredRequests.ID = "tlbItemRequestByOperator_TlbRegisteredRequests";
        tlbItemRequestByOperator_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemRequestByOperator_TlbRegisteredRequests").ToString();
        tlbItemRequestByOperator_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemRequestByOperator_TlbRegisteredRequests.ClientSideCommand = "tlbItemRequestByOperator_TlbRegisteredRequests_onClick();";
        tlbItemRequestByOperator_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemRequestByOperator_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemRequestByOperator_TlbRegisteredRequests.ImageUrl = "operator.png";

        ToolBarItem tlbItemPermitByOperator_TlbRegisteredRequests = new ToolBarItem();
        tlbItemPermitByOperator_TlbRegisteredRequests.ID = "tlbItemPermitByOperator_TlbRegisteredRequests";
        tlbItemPermitByOperator_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemPermitByOperator_TlbRegisteredRequests").ToString();
        tlbItemPermitByOperator_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemPermitByOperator_TlbRegisteredRequests.ClientSideCommand = "tlbItemPermitByOperator_TlbRegisteredRequests_onClick();";
        tlbItemPermitByOperator_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemPermitByOperator_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemPermitByOperator_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemPermitByOperator_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemPermitByOperator_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemPermitByOperator_TlbRegisteredRequests.ImageUrl = "operatorPermit.png";


        ToolBarItem tlbItemHelp_TlbRegisteredRequests = new ToolBarItem();
        tlbItemHelp_TlbRegisteredRequests.ID = "tlbItemHelp_TlbRegisteredRequests";
        tlbItemHelp_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemHelp_TlbRegisteredRequests").ToString();
        tlbItemHelp_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemHelp_TlbRegisteredRequests.ClientSideCommand = "tlbItemHelp_TlbRegisteredRequests_onClick();";
        tlbItemHelp_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemHelp_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemHelp_TlbRegisteredRequests.ImageUrl = "help.gif";

        ToolBarItem tlbItemFormReconstruction_TlbRegisteredRequests = new ToolBarItem();
        tlbItemFormReconstruction_TlbRegisteredRequests.ID = "tlbItemFormReconstruction_TlbRegisteredRequests";
        tlbItemFormReconstruction_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemFormReconstruction_TlbRegisteredRequests").ToString();
        tlbItemFormReconstruction_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemFormReconstruction_TlbRegisteredRequests.ClientSideCommand = "tlbItemFormReconstruction_TlbRegisteredRequests_onClick();";
        tlbItemFormReconstruction_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemFormReconstruction_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemFormReconstruction_TlbRegisteredRequests.ImageUrl = "refresh.png";

        ToolBarItem tlbItemExit_TlbRegisteredRequests = new ToolBarItem();
        tlbItemExit_TlbRegisteredRequests.ID = "tlbItemExit_TlbRegisteredRequests";
        tlbItemExit_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemExit_TlbRegisteredRequests").ToString();
        tlbItemExit_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemExit_TlbRegisteredRequests.ClientSideCommand = "tlbItemExit_TlbRegisteredRequests_onClick();";
        tlbItemExit_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemExit_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemExit_TlbRegisteredRequests.ImageUrl = "exit.png";

        ToolBarItem tlbItemTerminate_TlbRegisteredRequests = new ToolBarItem();
        tlbItemTerminate_TlbRegisteredRequests.ID = "tlbItemTerminate_TlbRegisteredRequests";
        tlbItemTerminate_TlbRegisteredRequests.Text = GetLocalResourceObject("tlbItemTerminate_TlbRegisteredRequests").ToString();
        tlbItemTerminate_TlbRegisteredRequests.ItemType = ToolBarItemType.Command;
        tlbItemTerminate_TlbRegisteredRequests.ClientSideCommand = "tlbItemTerminate_TlbRegisteredRequests_onClick();";
        tlbItemTerminate_TlbRegisteredRequests.DropDownImageHeight = Unit.Pixel(16);
        tlbItemTerminate_TlbRegisteredRequests.DropDownImageWidth = Unit.Pixel(16);
        tlbItemTerminate_TlbRegisteredRequests.ImageHeight = Unit.Pixel(16);
        tlbItemTerminate_TlbRegisteredRequests.ImageWidth = Unit.Pixel(16);
        tlbItemTerminate_TlbRegisteredRequests.TextImageSpacing = 5;
        tlbItemTerminate_TlbRegisteredRequests.ImageUrl = "down.png";

        TlbRegisteredRequests.Items.Add(tlbItemInsert_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemDelete_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemFilter_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemTerminate_TlbRegisteredRequests);
        if (CUS == CurrentUserState.Operator)
        { 
            TlbRegisteredRequests.Items.Add(tlbItemRequestByOperator_TlbRegisteredRequests);
            TlbRegisteredRequests.Items.Add(tlbItemPermitByOperator_TlbRegisteredRequests);
        }
        TlbRegisteredRequests.Items.Add(tlbItemHelp_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemFormReconstruction_TlbRegisteredRequests);
        TlbRegisteredRequests.Items.Add(tlbItemExit_TlbRegisteredRequests);
    }

    private void SetPersonnelPageSize_cmbPersonnel_RegisteredRequests()
    {
        this.hfPersonnelPageSize_RegisteredRequests.Value = this.cmbPersonnel_RegisteredRequests.DropDownPageSize.ToString();
    }

    private void SetPersonnelPageCount_cmbPersonnel_RegisteredRequests(LoadState Ls, int pageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int PersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(string.Empty, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(SearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), PersonCategory.Operator_UnderManagment);
                    break;
                default:
                    break;
            }
            this.hfPersonnelCount_RegisteredRequests.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_RegisteredRequests.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void SetRegisteredRequestsPageSize_RegisteredRequests()
    {
        this.hfRegisteredRequestsPageSize_RegisteredRequests.Value = this.GridRegisteredRequests_RegisteredRequests.PageSize.ToString();
    }

    private void Fill_cmbYear_RegisteredRequests()
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
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_RegisteredRequests, this.hfCurrentYear_RegisteredRequests,applicantSendedYear);
    }

    private void Fill_cmbMonth_RegisteredRequests()
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
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_RegisteredRequests, this.hfCurrentMonth_RegisteredRequests,applicantSendedMonth);
    }

    private void SetRequestStatesStr_RegisteredRequests()
    {
        string strRequestStates = string.Empty;
        foreach (RequestState requestStateItem in Enum.GetValues(typeof(RequestState)))
        {
            strRequestStates += "#" + GetLocalResourceObject(requestStateItem.ToString()).ToString() + ":" + ((int)requestStateItem).ToString();
        }
        this.hfRequestStates_RegisteredRequests.Value = strRequestStates;
    }

    private void SetRequestTypesStr_RegisteredRequests()
    {
        string strRequestTypes = string.Empty;
        foreach (RequestType requestTypeItem in Enum.GetValues(typeof(RequestType)))
        {
            strRequestTypes += "#" + GetLocalResourceObject(requestTypeItem.ToString()).ToString() + ":" + ((int)requestTypeItem).ToString();
        }
        this.hfRequestTypes_RegisteredRequests.Value = strRequestTypes;
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

    protected void CallBack_GridRegisteredRequests_RegisteredRequests_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridRegisteredRequests_RegisteredRequests((CurrentUserState)Enum.Parse(typeof(CurrentUserState), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[4]), int.Parse(this.StringBuilder.CreateString(e.Parameters[5]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[6]), CultureInfo.InvariantCulture));
        this.SetRegisteredRequestsPageCount_RegisteredRequests(e);
        this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
        this.GridRegisteredRequests_RegisteredRequests.RenderControl(e.Output);
        this.hfRegisteredRequestsCount_RegisteredRequests.RenderControl(e.Output);
        this.hfRegisteredRequestsPageCount_RegisteredRequests.RenderControl(e.Output);
    }

    private void SetRegisteredRequestsPageCount_RegisteredRequests(CallBackEventArgs e)
    {
        string LoadState = this.StringBuilder.CreateString(e.Parameters[1]);
        Applicant applicant = Applicant.None;
        int year = int.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
        int month = int.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
        KartablSummaryItems itemSummary = KartablSummaryItems.UnKnown;
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
        {
            applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));
            if (applicant == Applicant.PrivateNews)
            {
                itemSummary = (KartablSummaryItems)Enum.Parse(typeof(KartablSummaryItems), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["KeyApplicant"]));
                switch (itemSummary)
                {

                    case KartablSummaryItems.ConfirmedRequestCount:
                        LoadState = RequestState.Confirmed.ToString();                       
                        break;
                    case KartablSummaryItems.NotConfirmedRequestCount:
                        LoadState = RequestState.Unconfirmed.ToString();
                        break;
                    case KartablSummaryItems.InFlowRequestCount:
                        LoadState = RequestState.UnderReview.ToString();
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            year = new PersianDateTime(DateTime.Now).Year;
                           // month = new PersianDateTime(DateTime.Now).Month;
                        }
                        else
                        {
                            year = DateTime.Now.Year;
                            //month = DateTime.Now.Month;
                        }
                        break;
                    default:
                        break;
                }

            }


        }
        switch (LoadState)
        {
            case "CustomFilter":
                this.SetRegisteredRequestsPageCount_RegisteredRequests(this.StringBuilder.CreateString(e.Parameters[4]));
                break;
            default:
                //this.SetRegisteredRequestsPageCount_RegisteredRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), int.Parse(this.StringBuilder.CreateString(e.Parameters[2])), int.Parse(this.StringBuilder.CreateString(e.Parameters[3])));
                this.SetRegisteredRequestsPageCount_RegisteredRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), year , month , itemSummary);
                break;
        }
    }

    private void SetRegisteredRequestsPageCount_RegisteredRequests(RequestState RS, int Year, int Month, KartablSummaryItems itemSummary)
    {        
       // int KartableCount = this.RegisteredRequestsBusiness.GetUserRequestCount(RS, Year, Month);
        int KartableCount = this.RegisteredRequestsBusiness.GetUserRequestCount(RS, Year, Month, itemSummary);
        this.hfRegisteredRequestsCount_RegisteredRequests.Value = KartableCount.ToString();
        this.hfRegisteredRequestsPageCount_RegisteredRequests.Value = Utility.GetPageCount(KartableCount, this.GridRegisteredRequests_RegisteredRequests.PageSize).ToString();
    }

    private void SetRegisteredRequestsPageCount_RegisteredRequests(string StrRequestFliterProxyList)
    {
        UserRequestFilterProxy CustomFilterProxy = GetRegisteredRequestsCustomFilterProxy_RegisteredRequests(StrRequestFliterProxyList);
        int RegisteredRequestsCount = this.RegisteredRequestsBusiness.GetFilterUserRequestsCount(CustomFilterProxy);
        this.hfRegisteredRequestsCount_RegisteredRequests.Value = RegisteredRequestsCount.ToString();
        this.hfRegisteredRequestsPageCount_RegisteredRequests.Value = Utility.GetPageCount(RegisteredRequestsCount, this.GridRegisteredRequests_RegisteredRequests.PageSize).ToString();
    }

    private void Fill_GridRegisteredRequests_RegisteredRequests(CurrentUserState CUS, string LoadState, int year, int month, string filterString, int pageSize, int pageIndex)
    {
        string[] retMessage = new string[4];
        IList<KartablProxy> RegisteredRequestsList = null;
        try
        {            
            Applicant applicant = Applicant.None;
            KartablSummaryItems itemSummary = KartablSummaryItems.UnKnown;
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Applicant") && this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]) != "")
            {
                applicant = (Applicant)Enum.Parse(typeof(Applicant), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Applicant"]));              
                if (applicant == Applicant.PrivateNews)
                {
                     itemSummary =(KartablSummaryItems)Enum.Parse(typeof(KartablSummaryItems), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["KeyApplicant"]));                                                              
                     switch (itemSummary)
                     {                         
                         case KartablSummaryItems.ConfirmedRequestCount:
                             LoadState = RequestState.Confirmed.ToString();
                             break;
                         case KartablSummaryItems.NotConfirmedRequestCount:
                             LoadState = RequestState.Unconfirmed.ToString();
                             break;
                         case KartablSummaryItems.InFlowRequestCount:
                             LoadState = RequestState.UnderReview.ToString();
                             break;                         
                         default:
                             break;
                     }

                }


            }
            switch (LoadState)
            {
                case "CustomFilter":
                    UserRequestFilterProxy CustomFilterProxy = this.GetRegisteredRequestsCustomFilterProxy_RegisteredRequests(filterString);
                    RegisteredRequestsList = this.RegisteredRequestsBusiness.GetFilterUserRequests(CustomFilterProxy, pageIndex, pageSize);
                    break;
                default:
                    RegisteredRequestsList = this.RegisteredRequestsBusiness.GetAllUserRequests((RequestState)Enum.Parse(typeof(RequestState), LoadState), year, month, pageIndex, pageSize , itemSummary);
                    break;
            }
            
            this.operationYearMonthProvider.SetOperationYearMonth(year, month);
            this.GridRegisteredRequests_RegisteredRequests.DataSource = RegisteredRequestsList;
            this.GridRegisteredRequests_RegisteredRequests.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private UserRequestFilterProxy GetRegisteredRequestsCustomFilterProxy_RegisteredRequests(string strCustomFilter)
    {
        UserRequestFilterProxy customFilterProxy = new UserRequestFilterProxy();
        if (strCustomFilter != string.Empty)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Dictionary<string, object> ParamsDic = (Dictionary<string, object>)jsSerializer.DeserializeObject(strCustomFilter);
            CurrentUserState CUS = (CurrentUserState)Enum.Parse(typeof(CurrentUserState), ParamsDic["currentUserState"].ToString());
            int personnelID = int.Parse(ParamsDic["personnelID"].ToString(), CultureInfo.InvariantCulture);
            int requestTypeID = int.Parse(ParamsDic["requestTypeID"].ToString(), CultureInfo.InvariantCulture);
            int requestExporter = int.Parse(ParamsDic["requestExporter"].ToString(), CultureInfo.InvariantCulture);
            string fromDate = ParamsDic["fromDate"].ToString();
            string toDate = ParamsDic["toDate"].ToString();
            switch (CUS)
            {
                case CurrentUserState.NormalUser:
                    break;
                case CurrentUserState.Operator:
                    customFilterProxy.UnderManagmentPersonId = personnelID;
                    break;
            }
            if (requestTypeID != -1)
                customFilterProxy.RequestType = (RequestType)Enum.ToObject(typeof(RequestType), requestTypeID);
            if (requestExporter != -1)
                customFilterProxy.RequestSubmiter = (RequestSubmiter)Enum.ToObject(typeof(RequestSubmiter), requestExporter);
            if (fromDate != string.Empty)
                customFilterProxy.FromDate = fromDate;
            if (toDate != string.Empty)
                customFilterProxy.ToDate = toDate;
        }
        return customFilterProxy;
    }

    protected void CallBack_cmbRequestType_RegisteredRequests_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbRequestType_RegisteredRequests.Dispose();
        this.Fill_cmbRequestType_RegisteredRequests();
        this.ErrorHiddenField_RequestsTypes.RenderControl(e.Output);
        this.cmbRequestType_RegisteredRequests.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_RegisteredRequests()
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            foreach (RequestType requestTypeItem in Enum.GetValues(typeof(RequestType)))
            {
                if (requestTypeItem != RequestType.Monthly && requestTypeItem != RequestType.Imperative)
                {
                    ComboBoxItem cmbItemRequestType = new ComboBoxItem(GetLocalResourceObject(requestTypeItem.ToString()).ToString());
                    cmbItemRequestType.Value = requestTypeItem.ToString();
                    cmbItemRequestType.Id = ((int)requestTypeItem).ToString();
                    this.cmbRequestType_RegisteredRequests.Items.Add(cmbItemRequestType);
                }
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestsTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestsTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestsTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbExporter_RegisteredRequests_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbExporter_RegisteredRequests.Dispose();
        this.Fill_cmbExporter_RegisteredRequests();
        this.ErrorHiddenField_Exporters.RenderControl(e.Output);
        this.cmbExporter_RegisteredRequests.RenderControl(e.Output);
    }

    private void Fill_cmbExporter_RegisteredRequests()
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();
        try
        {
            bool isCurrentUserOperator = (new BKartabl()).IsCurrentUserOperator;
            foreach (RequestSubmiter requestExporterItem in Enum.GetValues(typeof(RequestSubmiter)))
            {
                if (requestExporterItem == RequestSubmiter.OPERATOR && !isCurrentUserOperator)
                    continue;
                ComboBoxItem cmbItemExporter = new ComboBoxItem(GetLocalResourceObject(requestExporterItem.ToString()).ToString());
                cmbItemExporter.Value = requestExporterItem.ToString();
                cmbItemExporter.Id = ((int)requestExporterItem).ToString();
                this.cmbExporter_RegisteredRequests.Items.Add(cmbItemExporter);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Exporters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Exporters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Exporters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateRegisteredRequest_RegisteredRequestsPage", "UpdateRegisteredRequest_RegisteredRequestsPage_onCallBack", null, null)]
    public string[] UpdateRegisteredRequest_RegisteredRequestsPage(string state, string SelectedRegisteredRequestID, string SelectedRegisteredRequestAttachmentFile, string ActionDescription, string month, string year)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            decimal selectedRegisteredRequestID = decimal.Parse(this.StringBuilder.CreateString(SelectedRegisteredRequestID), CultureInfo.InvariantCulture);
            SelectedRegisteredRequestAttachmentFile = this.StringBuilder.CreateString(SelectedRegisteredRequestAttachmentFile);
            ActionDescription = this.StringBuilder.CreateString(ActionDescription);
            month = this.StringBuilder.CreateString(month);
            year = this.StringBuilder.CreateString(year);

            switch (uam)
            {
                case UIActionType.TERMINATE:
                    if (selectedRegisteredRequestID == 0)
                        return this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRegisteredRequestsSelectedforTerminate").ToString()), retMessage);
                    this.RegisteredRequestsBusiness.TerminateRequest(selectedRegisteredRequestID, ActionDescription, Convert.ToInt32(month), Convert.ToInt32(year));
                    break;
                case UIActionType.DELETE:
                    if (selectedRegisteredRequestID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRegisteredRequestsSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    this.RegisteredRequestsBusiness.DeleteRequest(selectedRegisteredRequestID);
                    if(SelectedRegisteredRequestAttachmentFile != null && SelectedRegisteredRequestAttachmentFile != string.Empty)
                       this.MasterRequestBusiness.DeleteRequestAttachment(AppDomain.CurrentDomain.BaseDirectory + AppFolders.RequestsAttachments.ToString() + "\\" + SelectedRegisteredRequestAttachmentFile);
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.TERMINATE:
                    SuccessMessageBody = GetLocalResourceObject("TerminateComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = selectedRegisteredRequestID.ToString();
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

    protected void CallBack_cmbPersonnel_RegisteredRequests_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbPersonnel_RegisteredRequests.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_RegisteredRequests((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_RegisteredRequests((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.hfPersonnelCount_RegisteredRequests.RenderControl(e.Output);
        this.hfPersonnelPageCount_RegisteredRequests.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_RegisteredRequests.RenderControl(e.Output);
        this.cmbPersonnel_RegisteredRequests.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_RegisteredRequests(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, string.Empty, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                    break;
            }
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                PersonnelDetails personnelDetails = new PersonnelDetails();
                personnelDetails.ID = personItem.ID.ToString();
                personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                personCmbItem.Value = this.JsSeializer.Serialize(personnelDetails);
                this.cmbPersonnel_RegisteredRequests.Items.Add(personCmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }



}