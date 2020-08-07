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
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.BaseInformation;
using System.IO;
using GTS.Clock.Model.Concepts;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;
using System.Web.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using System.Reflection;
using Subgurim.Controles;
using System.Web.Configuration;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.BaseInformation;
using NHibernate;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using System.Web.UI.HtmlControls;

public partial class RequestRegister : GTSBasePage
{
    const string ZeroTime = "00:00";

    public IRegisteredRequests RequestRegisterBusiness
    {
        get
        {
            return (IRegisteredRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
        }
    }

    public BImperativeRequest ImperativeRequestBusiness
    {
        get
        {
            return new BImperativeRequest();
        }
    }
    public BDutyPlace MissionLocationBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDutyPlace>();
        }
    }
    public BRequest MasterRequestBusiness
    {
        get
        {
            return new BRequest();
        }
    }

    enum RequestCaller
    {
        NormalUser,
        Operator,
        OperatorPermit
    }

    enum RequestPersonnelCountState
    {
        Single,
        Collective
    }

    internal class RequestTargetFeatures
    {
        public bool IsSickLeave { get; set; }
        public bool IsMission { get; set; }
        public bool IsTraffic { get; set; }
        public bool IsLeaveDutyEstelajy { get; set; }
    }

    private enum RequestTarget
    {
        Hourly,
        Daily,
        OverTime,
        Imperative
    }

    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get
        {
            return new OperationYearMonthProvider();
        }
    }

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
        }
    }

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
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
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string OrganizationPostID { get; set; }
        public string OrganizationPostName { get; set; }
    }

    internal class ImperativeProxy
    {
        public string PersonID { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string PersonImage { get; set; }
        public decimal ImperativeValue { get; set; }
        public bool IsLockedImperative { get; set; }
        public string ImperativeDescription { get; set; }
        public string CalcInfo { get; set; }
    }

    internal class CalculationsProgress
    {
        public int AllPersonnelCount { get; set; }
        public int CalculatedPersonnelCount { get; set; }
        public int ErrorPersonnelCount { get; set; }
        public int Progress { get; set; }
        public bool InProgress { get; set; }
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

    enum Scripts
    {
        RequestRegister_onPageLoad,
        DialogRequestRegister_Operations,
        Alert_Box,
        DialogWaiting_Operations
    }

    internal class ObjRequestAttachment
    {
        public string RequestAttachmentPath { get; set; }
        public string RequestAttachmentRealName { get; set; }
        public string RequestAttachmentSavedPath { get; set; }
        public string RequestAttachmentSavedName { get; set; }
        public bool IsErrorOccured { get; set; }
        public string Message { get; set; }
    }
    internal class SubstitutePerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
    }
    private string StrRequestAttachment = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        this.SetPersonnelPageSize_cmbPersonnel_Substitute_RequestRegister();
        if (!CallBack_cmbDoctors_tbDaily_RequestRegister.IsCallback && !CallBack_cmbDoctors_tbHourly_RequestRegister.IsCallback && !CallBack_cmbIllnesses_tbDaily_RequestRegister.IsCallback && !CallBack_cmbIllnesses_tbHourly_RequestRegister.IsCallback && !CallBack_cmbMissionLocation_tbDaily_RequestRegister.IsCallback && !CallBack_cmbMissionLocation_tbHourly_RequestRegister.IsCallback && !CallBack_cmbRequestType_tbHourly_RequestRegister.IsCallback && !CallBack_cmbRequestType_tbDaily_RequestRegister.IsCallback && !CallBack_cmbRequestType_tbOverTime_RequestRegister.IsCallback && !CallBack_cmbPersonnel_RequestRegister.IsCallback && !CallBack_GridPersonnel_CollectiveTraffic.IsCallback && !CallBack_GridPersonnel_tbImperative_RequestRegister.IsCallback && !CallBack_GridPersonnel_tbImperative_RequestRegister.IsCallback && !Callback_AttachmentUploader_tbHourly_RequestRegister.IsCallback && !AttachmentUploader_tbDaily_RequestRegister.IsRequesting && !AttachmentUploader_tbHourly_RequestRegister.IsRequesting && !CallBack_bulletedListDayTraffics_tbHourly_RequestRegister.IsCallback)
        {
            Page RequestRegisterPage = this;
            Ajax.Utility.GenerateMethodScripts(RequestRegisterPage);

            this.CheckRequestRegisterLoadAccess_ReqiestRegister();
            this.CheckNextDayTimesLoadAccess_RequestRegister();
            this.CustomizeRequestRegister_RequestRegister();
            this.ViewCurrentLangCalendars_RequestRegister();
            this.SetCurrentDate_RequestRegiser();
            this.Fill_cmbYear_tbImperative_RequestRegister();
            this.Fill_cmbMonth_tbImperative_RequestRegister();
            if (Request.QueryString["RequestCaller"] != null && ((RequestCaller)Enum.Parse(typeof(RequestCaller), StringBuilder.CreateString(Request.QueryString["RequestCaller"].ToString()))) != RequestCaller.NormalUser)
                this.SetHeaderPersonnelCount_RequestRegister();
            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }

        if (this.AttachmentUploader_tbHourly_RequestRegister.IsPosting)
            this.ManagePostedData_AttachmentUploader_RequestRegister(this.AttachmentUploader_tbHourly_RequestRegister);
        if (this.AttachmentUploader_tbDaily_RequestRegister.IsPosting)
            this.ManagePostedData_AttachmentUploader_RequestRegister(this.AttachmentUploader_tbDaily_RequestRegister);

        if (!Page.IsPostBack)
        {
            this.AttachmentUploader_tbHourly_RequestRegister.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_tbHourly_RequestRegister_OnAfterFileUpload('" + StrRequestAttachment + "');");
            this.AttachmentUploader_tbHourly_RequestRegister.addCustomJS(FileUploaderAJAX.customJSevent.preUpload, "parent.AttachmentUploader_tbHourly_RequestRegister_OnPreFileUpload();");
            this.AttachmentUploader_tbDaily_RequestRegister.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_tbDaily_RequestRegister_OnAfterFileUpload('" + StrRequestAttachment + "');");
            this.AttachmentUploader_tbDaily_RequestRegister.addCustomJS(FileUploaderAJAX.customJSevent.preUpload, "parent.AttachmentUploader_tbDaily_RequestRegister_OnPreFileUpload();");
        }
    }

    private void SetHeaderPersonnelCount_RequestRegister()
    {
        int PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCount(string.Empty, PersonCategory.Operator_UnderManagment);
        this.hfheaderPersonnelCount_RequestRegister.Value = PersonnelCount.ToString();
    }

    private void ManagePostedData_AttachmentUploader_RequestRegister(FileUploaderAJAX AttachmentUploader)
    {
        try
        {
            string separator = "_";
            string RequestAttachmentsPathKey = AppFolders.RequestsAttachments.ToString();
            string path = AppDomain.CurrentDomain.BaseDirectory + RequestAttachmentsPathKey;
            HttpPostedFileAJAX HPFA = AttachmentUploader.PostedFile;
            string operatorSeparator = string.Empty;
            if (this.RequestRegisterBusiness.IsCurrentUserOperator)
                operatorSeparator = "Operator";
            string RequestAttachmentSavedFileName = Guid.NewGuid().ToString() + separator + operatorSeparator + separator + BUser.CurrentUser.Person.BarCode + separator + this.StringBuilder.CreateString(HPFA.FileName, StringGeneratorExceptionType.ClientAttachments);
            ObjRequestAttachment RequestAttachment = new ObjRequestAttachment()
            {
                RequestAttachmentPath = path,
                RequestAttachmentRealName = HPFA.FileName,
                RequestAttachmentSavedPath = path + "/" + RequestAttachmentSavedFileName,
                RequestAttachmentSavedName = RequestAttachmentSavedFileName
            };
            this.StrRequestAttachment = this.JsSerializer.Serialize(RequestAttachment);
            AttachmentUploader.PostedFile.responseMessage_Uploaded_Saved = " ";
            AttachmentUploader.PostedFile.responseMessage_Uploaded_NotSaved = " ";
            AttachmentUploader.SaveAs(AppFolders.RequestsAttachments.ToString(), RequestAttachmentSavedFileName);
        }
        catch (Exception ex)
        {
            ObjRequestAttachment RequestAttachment = new ObjRequestAttachment()
            {
                IsErrorOccured = true,
                Message = GetLocalResourceObject("UploadingError").ToString()
            };
            this.StrRequestAttachment = this.JsSerializer.Serialize(RequestAttachment);
        }
    }

    [Ajax.AjaxMethod("DeleteRequestAttachment_RequestRegisterPage", "DeleteRequestAttachment_RequestRegisterPage_onCallBack", null, null)]
    public string[] DeleteRequestAttachment_RequestRegisterPage(string requestTarget, string RequestAttachmentSavedName)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            RequestTarget RT = (RequestTarget)Enum.Parse(typeof(RequestTarget), this.StringBuilder.CreateString(requestTarget));
            RequestAttachmentSavedName = this.StringBuilder.CreateString(RequestAttachmentSavedName);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + AppFolders.RequestsAttachments.ToString() + "\\" + RequestAttachmentSavedName;
            this.MasterRequestBusiness.DeleteRequestAttachment(filePath);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("DeleteComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = RT.ToString();
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

    private void CheckRequestRegisterLoadAccess_ReqiestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
            {
                RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
                switch (RC)
                {
                    case RequestCaller.NormalUser:
                        this.RequestRegisterBusiness.CheckRequestRgisterLoadAccess_onNormalUser();
                        break;
                    case RequestCaller.Operator:
                        this.RequestRegisterBusiness.CheckRequestRgisterLoadAccess_onOperator();
                        break;
                    case RequestCaller.OperatorPermit:
                        this.RequestRegisterBusiness.CheckRequestRgisterLoadAccess_onOperatorPermit();
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

    private void CheckNextDayTimesLoadAccess_RequestRegister()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            switch (RC)
            {
                case RequestCaller.NormalUser:
                    try
                    {
                        this.RequestRegisterBusiness.CheckNextDayHourlyRequestLoadAccess_RequestRegister_onNormalUser();
                    }
                    catch (Exception)
                    {
                        this.tblToHourInNextDay_tbHourly_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckAllNextDayHourlyRequestLoadAccess_RequestRegister_onNormalUser();
                    }
                    catch (Exception)
                    {
                        this.tblFromAndToHourInNextDay_tbHourly_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckNextDayOvertimeRequestLoadAccess_RequestRegister_onNormalUser();
                    }
                    catch (Exception)
                    {
                        this.tblToHourInNextDay_tbOverTime_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckAllNextDayOvertimeRequestLoadAccess_RequestRegister_onNormalUser();
                    }
                    catch (Exception)
                    {
                        this.tblFromAndToHourInNextDay_tbOverTime_RequestRegister.Visible = false;
                    }
                    break;
                case RequestCaller.Operator:
                    try
                    {
                        this.RequestRegisterBusiness.CheckNextDayHourlyRequestLoadAccess_RequestRegister_onOperator();
                    }
                    catch (Exception)
                    {
                        this.tblToHourInNextDay_tbHourly_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckAllNextDayHourlyRequestLoadAccess_RequestRegister_onOperator();
                    }
                    catch (Exception)
                    {
                        this.tblFromAndToHourInNextDay_tbHourly_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckNextDayOvertimeRequestLoadAccess_RequestRegister_onOperator();
                    }
                    catch (Exception)
                    {
                        this.tblToHourInNextDay_tbOverTime_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckAllNextDayOvertimeRequestLoadAccess_RequestRegister_onOperator();
                    }
                    catch (Exception)
                    {
                        this.tblFromAndToHourInNextDay_tbOverTime_RequestRegister.Visible = false;
                    }
                    break;
                case RequestCaller.OperatorPermit:
                    try
                    {
                        this.RequestRegisterBusiness.CheckNextDayHourlyRequestLoadAccess_RequestRegister_onOperatorPermit();
                    }
                    catch (Exception)
                    {
                        this.tblToHourInNextDay_tbHourly_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckAllNextDayHourlyRequestLoadAccess_RequestRegister_onOperatorPermit();
                    }
                    catch (Exception)
                    {
                        this.tblFromAndToHourInNextDay_tbHourly_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckNextDayOvertimeRequestLoadAccess_RequestRegister_onOperatorPermit();
                    }
                    catch (Exception)
                    {
                        this.tblToHourInNextDay_tbOverTime_RequestRegister.Visible = false;
                    }
                    try
                    {
                        this.RequestRegisterBusiness.CheckAllNextDayOvertimeRequestLoadAccess_RequestRegister_onOperatorPermit();
                    }
                    catch (Exception)
                    {
                        this.tblFromAndToHourInNextDay_tbOverTime_RequestRegister.Visible = false;
                    }
                    break;
            }
        }
    }

    private void CustomizeRequestRegister_RequestRegister()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            switch (RC)
            {
                case RequestCaller.NormalUser:
                    this.Container_PersonnelSelect_RequestRegister.Visible = false;
                    this.TabStripRequestRegister.Tabs[3].Visible = false;
                    break;
                case RequestCaller.Operator:
                case RequestCaller.OperatorPermit:
                    this.SetPersonnelPageSize_cmbPersonnel_RequestRegister();
                    this.SetPersonnelPageSize_GridPersonnel_CollectiveTraffic();
                    if (RC == RequestCaller.Operator)
                        this.SetPersonnelPageSize_GridPersonnel_tbImperative_RequestRegister();
                    if (RC == RequestCaller.OperatorPermit)
                        this.TabStripRequestRegister.Tabs[3].Visible = false;
                    break;
            }
        }
    }

    private void ViewCurrentLangCalendars_RequestRegister()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_tbDaily_RequestRegister.Visible = true;
                this.Container_pdpFromDate_tbOverTime_RequestRegister.Visible = true;
                this.Container_pdpRequestDate_tbHourly_RequestRegister.Visible = true;
                this.Container_pdpToDate_tbDaily_RequestRegister.Visible = true;
                this.Container_pdpToDate_tbOverTime_RequestRegister.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_tbDaily_RequestRegister.Visible = true;
                this.Container_gdpFromDate_tbOverTime_RequestRegister.Visible = true;
                this.Container_gdpRequestDate_tbHourly_RequestRegister.Visible = true;
                this.Container_gdpToDate_tbDaily_RequestRegister.Visible = false;
                this.Container_gdpToDate_tbOverTime_RequestRegister.Visible = false;
                break;
        }
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripRequestRegister);
    }

    private void SetCurrentDate_RequestRegiser()
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
        this.hfCurrentDate_RequestRegister.Value = strCurrentDate;
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

    private void SetPersonnelPageSize_cmbPersonnel_RequestRegister()
    {
        this.hfPersonnelPageSize_RequestRegister.Value = this.cmbPersonnel_RequestRegister.DropDownPageSize.ToString();
    }

    private void SetPersonnelPageCount_cmbPersonnel_RequestRegister(LoadState Ls, int pageSize, string SearchTerm)
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
            this.hfPersonnelCount_RequestRegister.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_RequestRegister.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void SetPersonnelPageSize_GridPersonnel_CollectiveTraffic()
    {
        this.hfPersonnelPageSize_Personnel_CollectiveTraffic.Value = this.GridPersonnel_CollectiveTraffic.PageSize.ToString();
    }

    private void SetPersonnelPageCount_GridPersonnel_CollectiveTraffic(LoadState Ls, int PageSize, string SearchTerm, string IntegratedSearchTerm)
    {
        string[] retMessage = new string[4];
        int PersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickIntegratedSearchCount(string.Empty, IntegratedSearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickIntegratedSearchCount(SearchTerm, IntegratedSearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), PersonCategory.Operator_UnderManagment);
                    break;
                default:
                    break;
            }
            this.hfPersonnelCount_Personnel_CollectiveTraffic.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_Personnel_CollectiveTraffic.Value = Utility.GetPageCount(PersonnelCount, this.GridPersonnel_CollectiveTraffic.PageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void SetPersonnelPageSize_GridPersonnel_tbImperative_RequestRegister()
    {
        this.hfImperativePageSize_tbImperative_RequestRegister.Value = this.GridPersonnel_tbImperative_RequestRegister.PageSize.ToString();
    }

    private void SetImperativePageCount_GridPersonnel_tbImperative_RequestRegister(LoadState Ls, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, PersonCategory searchInCategory, int PageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int ImperativePersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case LoadState.Normal:
                    ImperativePersonnelCount = this.ImperativeRequestBusiness.GetQuickSearchPersonCountByImperativeRequest(string.Empty, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    ImperativePersonnelCount = this.ImperativeRequestBusiness.GetQuickSearchPersonCountByImperativeRequest(SearchTerm, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    ImperativePersonnelCount = this.ImperativeRequestBusiness.GetAdvancedSearchPersonCountByImperativeRequest(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment);
                    break;
            }
            this.hfImperativeCount_tbImperative_RequestRegister.Value = ImperativePersonnelCount.ToString();
            this.hfImperativePageCount_tbImperative_RequestRegister.Value = Utility.GetPageCount(ImperativePersonnelCount, this.GridPersonnel_tbImperative_RequestRegister.PageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbIllnesses_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbIllnesses_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbIllnesses_tbHourly_RequestRegister();
        this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbIllnesses_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbIllnesses_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Illnesses_RequestRegister(cmbIllnesses_tbHourly_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_Illnesses_RequestRegister(ComponentArt.Web.UI.ComboBox cmbIllnesses)
    {
        this.InitializeCulture();

        ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
        cmbItemNotDetermined.Value = "0";
        cmbIllnesses.Items.Add(cmbItemNotDetermined);

        IList<Illness> IllnessesList = this.RequestRegisterBusiness.GetAllIllness();
        foreach (Illness IllnessItem in IllnessesList)
        {
            ComboBoxItem cmbItemIllness = new ComboBoxItem(IllnessItem.Name);
            cmbItemIllness.Value = IllnessItem.ID.ToString();
            cmbIllnesses.Items.Add(cmbItemIllness);
        }
        cmbIllnesses.SelectedItem = cmbItemNotDetermined;
    }

    protected void CallBack_cmbDoctors_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbDoctors_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbDoctors_tbHourly_RequestRegister();
        this.ErrorHiddenField_Doctors_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbDoctors_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbDoctors_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Doctors_RequestRegister(cmbDoctors_tbHourly_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_Doctors_RequestRegister(ComponentArt.Web.UI.ComboBox cmbDoctors)
    {
        this.InitializeCulture();

        ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
        cmbItemNotDetermined.Value = "0";
        cmbDoctors.Items.Add(cmbItemNotDetermined);

        IList<Doctor> DoctorsList = this.RequestRegisterBusiness.GetAllDoctors();
        foreach (Doctor DoctorItem in DoctorsList)
        {
            ComboBoxItem cmbItemDoctor = new ComboBoxItem(DoctorItem.Nezampezaeshki + " - " + DoctorItem.Name);
            cmbItemDoctor.Value = DoctorItem.ID.ToString();
            cmbDoctors.Items.Add(cmbItemDoctor);
        }
        cmbDoctors.SelectedItem = cmbItemNotDetermined;
    }

    protected void CallBack_cmbMissionLocation_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbMissionLocation_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbMissionLocation_tbHourly_RequestRegister();
        this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbMissionLocation_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbMissionLocation_tbHourly_RequestRegister()
    {
        this.Fill_trvMissionLocation_tbHourly_RequestRegister();
    }

    private void Fill_trvMissionLocation_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_MissionLocations_RequestRegister(trvMissionLocation_tbHourly_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_MissionLocations_RequestRegister(ComponentArt.Web.UI.TreeView trvMissionLocations)
    {
        this.InitializeCulture();

        TreeViewNode trvNodeNotDetermined = new TreeViewNode();
        trvNodeNotDetermined.Text = GetLocalResourceObject("NotDetermined").ToString();
        trvNodeNotDetermined.ID = "-1";
        trvNodeNotDetermined.Value = "NotDetermined";
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder_blue.gif"))
            trvNodeNotDetermined.ImageUrl = "Images/TreeView/folder_blue.gif";
        trvMissionLocations.Nodes.Add(trvNodeNotDetermined);

        IList<DutyPlace> rootDutyPlacesList = this.RequestRegisterBusiness.GetAllDutyPlaceRoot();
        foreach (DutyPlace rootDutyPlaceItem in rootDutyPlacesList)
        {
            TreeViewNode trvNodeRootDutyPlace = new TreeViewNode();
            if (rootDutyPlaceItem.ParentID == 0 && this.GetLocalResourceObject("MissLocNode_trvMissionLocations_RequestRegister") != null)
                trvNodeRootDutyPlace.Text = this.GetLocalResourceObject("MissLocNode_trvMissionLocations_RequestRegister").ToString();
            else
                trvNodeRootDutyPlace.Text = rootDutyPlaceItem.Name;
            trvNodeRootDutyPlace.Value = trvNodeRootDutyPlace.ID = rootDutyPlaceItem.ID.ToString();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                trvNodeRootDutyPlace.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
            trvMissionLocations.Nodes.Add(trvNodeRootDutyPlace);
            this.GetChildMissionLocations_MissionLocations_RequestRegister(trvNodeRootDutyPlace, rootDutyPlaceItem);
        }

        trvMissionLocations.SelectedNode = trvNodeNotDetermined;
    }

    private void GetChildMissionLocations_MissionLocations_RequestRegister(TreeViewNode parentDutyPlaceNode, DutyPlace parentDutyPlace)
    {
        foreach (DutyPlace childDutyPlace in this.RequestRegisterBusiness.GetAllDutyPlaceChild(parentDutyPlace.ID))
        {
            TreeViewNode trvNodeChildDutyPlace = new TreeViewNode();
            trvNodeChildDutyPlace.Text = childDutyPlace.Name;
            trvNodeChildDutyPlace.ID = childDutyPlace.ID.ToString();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\folder.gif"))
                trvNodeChildDutyPlace.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
            parentDutyPlaceNode.Nodes.Add(trvNodeChildDutyPlace);
            if (this.RequestRegisterBusiness.GetAllDutyPlaceChild(childDutyPlace.ID).Count > 0)
                this.GetChildMissionLocations_MissionLocations_RequestRegister(trvNodeChildDutyPlace, childDutyPlace);
        }
    }


    protected void CallBack_cmbRequestType_tbHourly_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbRequestType_tbHourly_RequestRegister.Dispose();
        this.Fill_cmbRequestType_tbHourly_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbHourly_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllHourlyRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                RequestTargetFeatures RtFeatures = new RequestTargetFeatures();
                RtFeatures.IsSickLeave = preCardItem.IsEstelajy;
                RtFeatures.IsMission = preCardItem.IsDuty;
                RtFeatures.IsTraffic = preCardItem.IsTraffic;
                RtFeatures.IsLeaveDutyEstelajy = preCardItem.IsLeaveDutyEstelajy;
                cmbItemRequestType.Value = this.JsSerializer.Serialize(RtFeatures);
                this.cmbRequestType_tbHourly_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbHourly_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbIllnesses_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbIllnesses_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbIllnesses_tbDaily_RequestRegister();
        this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbIllnesses_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbIllnesses_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Illnesses_RequestRegister(cmbIllnesses_tbDaily_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Illnesses_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbDoctors_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbDoctors_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbDoctors_tbDaily_RequestRegister();
        this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbDoctors_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbDoctors_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_Doctors_RequestRegister(cmbDoctors_tbDaily_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Doctors_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbMissionLocation_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbMissionLocation_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbMissionLocation_tbDaily_RequestRegister();
        this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbMissionLocation_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbMissionLocation_tbDaily_RequestRegister()
    {
        this.Fill_trvMissionLocation_tbDaily_RequestRegister();
    }

    private void Fill_trvMissionLocation_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_MissionLocations_RequestRegister(trvMissionLocation_tbDaily_RequestRegister);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MissionLocations_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbRequestType_tbDaily_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbRequestType_tbDaily_RequestRegister.Dispose();
        this.Fill_cmbRequestType_tbDaily_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbDaily_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllDailyRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                RequestTargetFeatures RtFeatures = new RequestTargetFeatures();
                RtFeatures.IsSickLeave = preCardItem.IsEstelajy;
                RtFeatures.IsMission = preCardItem.IsDuty;
                RtFeatures.IsLeaveDutyEstelajy = preCardItem.IsLeaveDutyEstelajy;
                cmbItemRequestType.Value = this.JsSerializer.Serialize(RtFeatures);
                this.cmbRequestType_tbDaily_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbDaily_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbRequestType_tbOverTime_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbRequestType_tbOverTime_RequestRegister.Dispose();
        this.Fill_cmbRequestType_tbOverTime_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbOverTime_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbOverTime_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllOverTimeRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                this.cmbRequestType_tbOverTime_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbOverTime_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateRequest_RequestRegisterPage", "UpdateRequest_RequestRegisterPage_onCallBack", null, null)]
    public string[] UpdateRequest_RequestRegisterPage(string requestCaller, string requestPersonnelCountState, string SinglePersonnelID, string CollectiveConditionsLoadState, string CollectiveConditions, string StrPersonnelList, string requestTarget, string Year, string Month, string PageSize, string RequestID, string PreCardID, string RequestDate, string FromDate, string ToDate, string FromTime, string ToTime, string IsToTimeInNextDay, string IsFromAndToTimeInNextDay, string Duration, string Description, string IsSeakLeave, string IllnessID, string DoctorID, string IsMission, string MissionLocationID, string RequestAttachmentFile, string selectedsubstitute, string IsWarning)
    {
        string[] retMessage = new string[6];
        string WarningMessage = string.Empty;
        this.InitializeCulture();
        IList<RequestKartablValidationProxy> requestValidationProxyList = new List<RequestKartablValidationProxy>();
        try
        {

            int registReqeustFailedCount = 0;
            AttackDefender.CSRFDefender(this.Page);
            int RequestCount = 0;
            int RequestPageCount = 0;
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(requestCaller));
            RequestPersonnelCountState RPCS = (RequestPersonnelCountState)Enum.Parse(typeof(RequestPersonnelCountState), this.StringBuilder.CreateString(requestPersonnelCountState));
            decimal singlePersonnelID = decimal.Parse(this.StringBuilder.CreateString(SinglePersonnelID), CultureInfo.InvariantCulture);
            LoadState CCLS = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(CollectiveConditionsLoadState));
            CollectiveConditions = this.StringBuilder.CreateString(CollectiveConditions);
            IList<decimal> PersonnelIDsList = this.CreatePersonnelList(this.StringBuilder.CreateString(StrPersonnelList));
            RequestTarget RT = (RequestTarget)Enum.Parse(typeof(RequestTarget), this.StringBuilder.CreateString(requestTarget));
            int year = int.Parse(this.StringBuilder.CreateString(Year), CultureInfo.InvariantCulture);
            int month = int.Parse(this.StringBuilder.CreateString(Month), CultureInfo.InvariantCulture);
            int pageSize = int.Parse(this.StringBuilder.CreateString(PageSize), CultureInfo.InvariantCulture);
            decimal requestID = decimal.Parse(this.StringBuilder.CreateString(RequestID), CultureInfo.InvariantCulture);
            decimal preCardID = decimal.Parse(this.StringBuilder.CreateString(PreCardID), CultureInfo.InvariantCulture);
            RequestDate = this.StringBuilder.CreateString(RequestDate);
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            FromTime = this.StringBuilder.CreateString(FromTime);
            ToTime = this.StringBuilder.CreateString(ToTime);
            bool isToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsToTimeInNextDay));
            bool isFromAndToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsFromAndToTimeInNextDay));
            Duration = this.StringBuilder.CreateString(Duration);
            Description = this.StringBuilder.CreateString(Description);
            bool isSickLeave = bool.Parse(this.StringBuilder.CreateString(IsSeakLeave));
            decimal illnessID = decimal.Parse(this.StringBuilder.CreateString(IllnessID), CultureInfo.InvariantCulture);
            decimal doctorID = decimal.Parse(this.StringBuilder.CreateString(DoctorID), CultureInfo.InvariantCulture);
            bool isMission = bool.Parse(this.StringBuilder.CreateString(IsMission));
            decimal missionLocationID = decimal.Parse(this.StringBuilder.CreateString(MissionLocationID), CultureInfo.InvariantCulture);
            RequestAttachmentFile = this.StringBuilder.CreateString(RequestAttachmentFile);
            selectedsubstitute = this.StringBuilder.CreateString(selectedsubstitute);
            SubstitutePerson Substitute = new SubstitutePerson();
            Substitute = this.JsSerializer.Deserialize<SubstitutePerson>(selectedsubstitute);
            bool isWarning = bool.Parse(this.StringBuilder.CreateString(IsWarning));
            GTS.Clock.Model.RequestFlow.Request request = new GTS.Clock.Model.RequestFlow.Request();
            Precard preCard = new Precard();
            decimal applicatorID = BUser.CurrentUser.Person.ID;
            switch (RT)
            {
                case RequestTarget.Hourly:
                    preCard.IsHourly = true;
                    request.TheFromDate = request.TheToDate = RequestDate;
                    request.TheFromTime = FromTime;
                    request.TheToTime = ToTime;
                    request.ContinueOnTomorrow = isToTimeInNextDay;
                    if (!isToTimeInNextDay)
                        request.AllOnTomorrow = isFromAndToTimeInNextDay;
                    request.AttachmentFile = RequestAttachmentFile;
                    request.IsExecuteWarningUIValidation = isWarning;
                    if (isSickLeave)
                    {
                        request.IllnessID = illnessID;
                        request.DoctorID = doctorID;
                    }
                    if (isMission)
                        request.DutyPositionID = missionLocationID;
                    break;
                case RequestTarget.Daily:
                    preCard.IsDaily = true;
                    request.TheFromDate = FromDate;
                    request.TheToDate = ToDate;
                    request.AttachmentFile = RequestAttachmentFile;
                    request.IsExecuteWarningUIValidation = isWarning;
                    if (isSickLeave)
                    {
                        request.IllnessID = illnessID;
                        request.DoctorID = doctorID;
                    }
                    if (isMission)
                        request.DutyPositionID = missionLocationID;
                    break;
                case RequestTarget.OverTime:
                    request.TheFromDate = FromDate;
                    request.TheToDate = ToDate;
                    request.TheFromTime = FromTime;
                    request.TheToTime = ToTime;
                    request.TheTimeDuration = !isToTimeInNextDay ? Duration : string.Empty;
                    request.ContinueOnTomorrow = isToTimeInNextDay;
                    if (!isToTimeInNextDay)
                        request.AllOnTomorrow = isFromAndToTimeInNextDay;
                    request.IsExecuteWarningUIValidation = isWarning;
                    break;
                case RequestTarget.Imperative:
                    preCard.IsMonthly = true;
                    FromDate = year.ToString() + "/" + month.ToString() + "/1";
                    switch (BLanguage.CurrentSystemLanguage)
                    {
                        case LanguagesName.Parsi:
                            ToDate = year.ToString() + "/" + month.ToString() + "/" + new PersianCalendar().GetDaysInMonth(year, month).ToString();
                            break;
                        case LanguagesName.English:
                            ToDate = year.ToString() + "/" + month.ToString() + "/" + new GregorianCalendar().GetDaysInMonth(year, month).ToString();
                            break;
                    }
                    request.TheFromDate = FromDate;
                    request.TheToDate = ToDate;
                    break;
            }
            preCard.ID = preCardID;
            request.Description = Description;
            request.IsExecuteWarningUIValidation = isWarning;
            if (Substitute != null)
            {
                request.SubstitutePerson = new Person() { ID = decimal.Parse(Substitute.Id, CultureInfo.InvariantCulture) };
                request.Description += " " + string.Format("{0} {1} , {2} {3}", GetLocalResourceObject("substituteName_Description_RequestRegister").ToString(), Substitute.Name, GetLocalResourceObject("substituteBarCode_Description_RequestRegister").ToString(), Substitute.BarCode);
            }
            request.Precard = preCard;

            switch (RC)
            {
                case RequestCaller.NormalUser:
                    switch (RT)
                    {
                        case RequestTarget.Hourly:
                            RequestCount = this.RequestRegisterBusiness.InsertSingleHourlyRequestByNormalUser(request, year, month);
                            break;
                        case RequestTarget.Daily:
                            RequestCount = this.RequestRegisterBusiness.InsertSingleDailyRequestByNormalUser(request, year, month);
                            break;
                        case RequestTarget.OverTime:
                            RequestCount = this.RequestRegisterBusiness.InsertSingleOverTimeRequestByNormalUser(request, year, month);
                            break;
                    }
                    break;
                case RequestCaller.Operator:
                    if (RT != RequestTarget.Imperative)
                    {
                        switch (RPCS)
                        {
                            case RequestPersonnelCountState.Single:
                                if (singlePersonnelID != 0)
                                {
                                    switch (RT)
                                    {
                                        case RequestTarget.Hourly:
                                            RequestCount = this.RequestRegisterBusiness.InsertSingleHourlyRequestByOperator(request, year, month, singlePersonnelID);
                                            break;
                                        case RequestTarget.Daily:
                                            RequestCount = this.RequestRegisterBusiness.InsertSingleDailyRequestByOperator(request, year, month, singlePersonnelID);
                                            break;
                                        case RequestTarget.OverTime:
                                            RequestCount = this.RequestRegisterBusiness.InsertSingleOverTimeRequestByOperator(request, year, month, singlePersonnelID);
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (RT)
                                    {
                                        case RequestTarget.Hourly:
                                            this.RequestRegisterBusiness.InsertCollectiveHourlyRequestByOperator();
                                            break;
                                        case RequestTarget.Daily:
                                            this.RequestRegisterBusiness.InsertCollectiveDailyRequestByOperator();
                                            break;
                                        case RequestTarget.OverTime:
                                            this.RequestRegisterBusiness.InsertCollectiveOverTimeRequestByOperator();
                                            break;
                                    }
                                    switch (CCLS)
                                    {
                                        case LoadState.Normal:
                                            RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, string.Empty, year, month, out registReqeustFailedCount);
                                            break;
                                        case LoadState.Search:
                                            RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, CollectiveConditions, year, month, out registReqeustFailedCount);
                                            break;
                                        case LoadState.AdvancedSearch:
                                            RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, this.APSProv.CreateAdvancedPersonnelSearchProxy(CollectiveConditions), year, month, out registReqeustFailedCount);
                                            break;
                                    }
                                }
                                break;
                            case RequestPersonnelCountState.Collective:
                                switch (RT)
                                {
                                    case RequestTarget.Hourly:
                                        this.RequestRegisterBusiness.InsertCollectiveHourlyRequestByOperator();
                                        break;
                                    case RequestTarget.Daily:
                                        this.RequestRegisterBusiness.InsertCollectiveDailyRequestByOperator();
                                        break;
                                    case RequestTarget.OverTime:
                                        this.RequestRegisterBusiness.InsertCollectiveOverTimeRequestByOperator();
                                        break;
                                }
                                switch (CCLS)
                                {
                                    case LoadState.Normal:
                                        if (PersonnelIDsList.Count == 0)
                                        {
                                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                                            return retMessage;
                                        }
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, PersonnelIDsList, year, month, out registReqeustFailedCount);
                                        break;
                                    case LoadState.Search:
                                        if (PersonnelIDsList.Count == 0)
                                        {
                                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                                            return retMessage;
                                        }
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, CollectiveConditions, PersonnelIDsList, year, month, out registReqeustFailedCount);
                                        break;
                                    case LoadState.AdvancedSearch:
                                        if (PersonnelIDsList.Count == 0)
                                        {
                                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                                            return retMessage;
                                        }
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequest(request, this.APSProv.CreateAdvancedPersonnelSearchProxy(CollectiveConditions), PersonnelIDsList, year, month, out registReqeustFailedCount);
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (PersonnelIDsList.Count == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                            return retMessage;
                        }
                        ImperativeRequest imperativeRequest = new ImperativeRequest()
                        {
                            Precard = new Precard() { ID = preCardID },
                            Year = year,
                            Month = month
                        };
                        this.RequestRegisterBusiness.InsertImperativeRequestByOperator(request, imperativeRequest, PersonnelIDsList);
                    }
                    break;
                case RequestCaller.OperatorPermit:
                    if (RT != RequestTarget.Imperative)
                    {
                        switch (RPCS)
                        {
                            case RequestPersonnelCountState.Single:
                                if (singlePersonnelID != 0)
                                {
                                    switch (RT)
                                    {
                                        case RequestTarget.Hourly:
                                            RequestCount = this.RequestRegisterBusiness.InsertSingleHourlyRequestByOperatorPermit(request, year, month, singlePersonnelID, out requestValidationProxyList, applicatorID);
                                            break;
                                        case RequestTarget.Daily:
                                            RequestCount = this.RequestRegisterBusiness.InsertSingleDailyRequestByOperatorPermit(request, year, month, singlePersonnelID, out requestValidationProxyList, applicatorID);
                                            break;
                                        case RequestTarget.OverTime:
                                            RequestCount = this.RequestRegisterBusiness.InsertSingleOverTimeRequestByOperatorPermit(request, year, month, singlePersonnelID, out requestValidationProxyList, applicatorID);
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (RT)
                                    {
                                        case RequestTarget.Hourly:
                                            this.RequestRegisterBusiness.InsertCollectiveHourlyRequestByOperatorPermit();
                                            break;
                                        case RequestTarget.Daily:
                                            this.RequestRegisterBusiness.InsertCollectiveDailyRequestByOperatorPermit();
                                            break;
                                        case RequestTarget.OverTime:
                                            this.RequestRegisterBusiness.InsertCollectiveOverTimeRequestByOperatorPermit();
                                            break;
                                    }
                                    switch (CCLS)
                                    {
                                        case LoadState.Normal:
                                            RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequestByOperatorPermit(request, year, month, out requestValidationProxyList, out registReqeustFailedCount, applicatorID);
                                            break;
                                        case LoadState.Search:
                                            RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequestByOperatorPermit(request, CollectiveConditions, year, month, out requestValidationProxyList, out registReqeustFailedCount, applicatorID);
                                            break;
                                        case LoadState.AdvancedSearch:
                                            RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequestByOperatorPermit(request, this.APSProv.CreateAdvancedPersonnelSearchProxy(CollectiveConditions), year, month, out requestValidationProxyList, out registReqeustFailedCount, applicatorID);
                                            break;
                                    }

                                }
                                break;
                            case RequestPersonnelCountState.Collective:
                                switch (RT)
                                {
                                    case RequestTarget.Hourly:
                                        this.RequestRegisterBusiness.InsertCollectiveHourlyRequestByOperatorPermit();
                                        break;
                                    case RequestTarget.Daily:
                                        this.RequestRegisterBusiness.InsertCollectiveDailyRequestByOperatorPermit();
                                        break;
                                    case RequestTarget.OverTime:
                                        this.RequestRegisterBusiness.InsertCollectiveOverTimeRequestByOperatorPermit();
                                        break;
                                }
                                switch (CCLS)
                                {
                                    case LoadState.Normal:
                                        if (PersonnelIDsList.Count == 0)
                                        {
                                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                                            return retMessage;
                                        }
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequestByOperatorPermit(request, PersonnelIDsList, year, month, out requestValidationProxyList, out registReqeustFailedCount, applicatorID);
                                        break;
                                    case LoadState.Search:
                                        if (PersonnelIDsList.Count == 0)
                                        {
                                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                                            return retMessage;
                                        }
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequestByOperatorPermit(request, CollectiveConditions, PersonnelIDsList, year, month, out requestValidationProxyList, out registReqeustFailedCount, applicatorID);
                                        break;
                                    case LoadState.AdvancedSearch:
                                        if (PersonnelIDsList.Count == 0)
                                        {
                                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                                            return retMessage;
                                        }
                                        RequestCount = this.RequestRegisterBusiness.InsertCollectiveRequestByOperatorPermit(request, this.APSProv.CreateAdvancedPersonnelSearchProxy(CollectiveConditions), PersonnelIDsList, year, month, out requestValidationProxyList, out registReqeustFailedCount, applicatorID);
                                        break;
                                }
                                break;
                        }
                    }
                    break;

            }
            RequestPageCount = Utility.GetPageCount(RequestCount, pageSize);
            registReqeustFailedCount += requestValidationProxyList.Count;
            if (registReqeustFailedCount > 0)
                WarningMessage += string.Format(GetLocalResourceObject("WarningMessageRequestRegistCollctiveByOperator").ToString(), registReqeustFailedCount);
            if (WarningMessage != string.Empty)
            {
                retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                retMessage[1] = WarningMessage;
                retMessage[2] = "warning";
                retMessage[3] = RequestPageCount.ToString();
                retMessage[4] = RT.ToString();
            }
            else
            {
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("AddComplete").ToString();
                retMessage[2] = "success";
                retMessage[3] = RequestPageCount.ToString();
                retMessage[4] = RT.ToString();
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

    private IList<decimal> CreatePersonnelList(string StrUnCollectivePersonnelList)
    {
        IList<decimal> UnCollectivePersonnelList = new List<decimal>();
        string[] StrUnCollectivePersonnelListParts = StrUnCollectivePersonnelList.Split(new char[] { '#' });
        for (int i = 0; i < StrUnCollectivePersonnelListParts.Length; i++)
        {
            if (StrUnCollectivePersonnelListParts[i] != string.Empty)
                UnCollectivePersonnelList.Add(decimal.Parse(StrUnCollectivePersonnelListParts[i], CultureInfo.InvariantCulture));
        }
        return UnCollectivePersonnelList;
    }

    protected void CallBack_cmbPersonnel_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbPersonnel_RequestRegister.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_RequestRegister((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_RequestRegister((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.cmbPersonnel_RequestRegister.RenderControl(e.Output);
        this.hfPersonnelCount_RequestRegister.RenderControl(e.Output);
        this.hfPersonnelPageCount_RequestRegister.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbPersonnel_RequestRegister(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                this.cmbPersonnel_RequestRegister.Items.Add(personCmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridPersonnel_CollectiveTraffic_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.SetPersonnelPageCount_GridPersonnel_CollectiveTraffic((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]), this.StringBuilder.CreateString(e.Parameters[4]));
        this.Fill_GridPersonnel_CollectiveTraffic((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]), this.StringBuilder.CreateString(e.Parameters[4]));
        this.ErrorHiddenField_Personnel_CollectiveTraffic.RenderControl(e.Output);
        this.hfPersonnelCount_Personnel_CollectiveTraffic.RenderControl(e.Output);
        this.hfPersonnelPageCount_Personnel_CollectiveTraffic.RenderControl(e.Output);
        this.GridPersonnel_CollectiveTraffic.RenderControl(e.Output);
    }

    private void Fill_GridPersonnel_CollectiveTraffic(LoadState Ls, int pageSize, int pageIndex, string SearchTerm, string IntegratedSearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.QuickIntegratedSearchByPage(pageIndex, pageSize, string.Empty, IntegratedSearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickIntegratedSearchByPage(pageIndex, pageSize, SearchTerm, IntegratedSearchTerm, PersonCategory.Operator_UnderManagment);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                    break;
            }
            this.GridPersonnel_CollectiveTraffic.DataSource = PersonnelList;
            this.GridPersonnel_CollectiveTraffic.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_CollectiveTraffic.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_cmbRequestType_tbImperative_RequestRegister_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_cmbRequestType_tbImperative_RequestRegister();
        this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.RenderControl(e.Output);
        this.cmbRequestType_tbImperative_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbRequestType_tbImperative_RequestRegister()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Precard> PreCardsList = this.RequestRegisterBusiness.GetAllImperativeRequestTypes();
            foreach (Precard preCardItem in PreCardsList)
            {
                ComboBoxItem cmbItemRequestType = new ComboBoxItem(preCardItem.Name);
                cmbItemRequestType.Id = preCardItem.ID.ToString();
                this.cmbRequestType_tbImperative_RequestRegister.Items.Add(cmbItemRequestType);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RequestTypes_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridPersonnel_tbImperative_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        LoadState Ls = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0]));
        ImperativeRequestLoadState IRLS = (ImperativeRequestLoadState)Enum.Parse(typeof(ImperativeRequestLoadState), this.StringBuilder.CreateString(e.Parameters[1]));
        decimal precardID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture);
        int year = int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture);
        int month = int.Parse(this.StringBuilder.CreateString(e.Parameters[4]), CultureInfo.InvariantCulture);
        bool isLockedImperative = bool.Parse(this.StringBuilder.CreateString(e.Parameters[5]));
        int pageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[6]), CultureInfo.InvariantCulture);
        int pageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[7]), CultureInfo.InvariantCulture);
        string SearchTermConditions = this.StringBuilder.CreateString(e.Parameters[8]);

        ImperativeRequest imperativeRequest = new ImperativeRequest()
        {
            Precard = new Precard() { ID = precardID },
            Year = year,
            Month = month,
            IsLocked = isLockedImperative
        };

        this.SetImperativePageCount_GridPersonnel_tbImperative_RequestRegister(Ls, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, pageSize, SearchTermConditions);
        this.Fill_GridPersonnel_tbImperative_RequestRegister(Ls, IRLS, imperativeRequest, pageSize, pageIndex, SearchTermConditions);
        this.ErrorHiddenField_tbImperative_RequestRegister.RenderControl(e.Output);
        this.hfImperativeCount_tbImperative_RequestRegister.RenderControl(e.Output);
        this.hfImperativePageCount_tbImperative_RequestRegister.RenderControl(e.Output);
        this.GridPersonnel_tbImperative_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_GridPersonnel_tbImperative_RequestRegister(LoadState Ls, ImperativeRequestLoadState IRLS, ImperativeRequest imperativeRequest, int PageSize, int PageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<ImperativeUndermanagementInfoProxy> ImperativeUndermanagementInfoProxyList = null;
            switch (Ls)
            {
                case LoadState.Normal:
                    ImperativeUndermanagementInfoProxyList = this.ImperativeRequestBusiness.GetQuickSearchPersonByImperativeRequest(string.Empty, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, PageIndex, PageSize);
                    break;
                case LoadState.Search:
                    ImperativeUndermanagementInfoProxyList = this.ImperativeRequestBusiness.GetQuickSearchPersonByImperativeRequest(SearchTerm, IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, PageIndex, PageSize);
                    break;
                case LoadState.AdvancedSearch:
                    ImperativeUndermanagementInfoProxyList = this.ImperativeRequestBusiness.GetAdvancedSearchPersonByImperativeRequest(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), IRLS, imperativeRequest, PersonCategory.Operator_UnderManagment, PageIndex, PageSize);
                    break;
            }
            this.GridPersonnel_tbImperative_RequestRegister.DataSource = this.CreateImperativeProxy_RequestRegister(ImperativeUndermanagementInfoProxyList);
            this.GridPersonnel_tbImperative_RequestRegister.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_tbImperative_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private IList<ImperativeProxy> CreateImperativeProxy_RequestRegister(IList<ImperativeUndermanagementInfoProxy> ImperativeUndermanagementInfoProxyList)
    {
        IList<ImperativeProxy> ImperativeProxyList = new List<ImperativeProxy>();
        foreach (ImperativeUndermanagementInfoProxy imperativeUndermanagementInfoProxyitem in ImperativeUndermanagementInfoProxyList)
        {
            ImperativeProxy imperativeProxy = new ImperativeProxy();
            imperativeProxy.PersonID = imperativeUndermanagementInfoProxyitem.PersonID.ToString();
            imperativeProxy.PersonCode = imperativeUndermanagementInfoProxyitem.PersonCode;
            imperativeProxy.PersonName = imperativeUndermanagementInfoProxyitem.PersonName;
            imperativeProxy.PersonImage = imperativeUndermanagementInfoProxyitem.PersonImage;
            imperativeProxy.ImperativeValue = imperativeUndermanagementInfoProxyitem.ImperativeValue;
            imperativeProxy.IsLockedImperative = imperativeUndermanagementInfoProxyitem.IsLockedImperative;
            imperativeProxy.ImperativeDescription = imperativeUndermanagementInfoProxyitem.ImperativeDescription;
            string CalcInfo = string.Empty;
            foreach (PropertyInfo propertyInfo in typeof(CalcInfoProxy).GetProperties())
            {
                string propertyInfoValue = propertyInfo.GetValue(imperativeUndermanagementInfoProxyitem.CalcInfo, null).ToString().Trim();
                CalcInfo += GetLocalResourceObject(propertyInfo.Name).ToString() + ":" + (propertyInfoValue != string.Empty ? propertyInfoValue : "0") + ",";
            }
            imperativeProxy.CalcInfo = CalcInfo;
            ImperativeProxyList.Add(imperativeProxy);
        }
        return ImperativeProxyList;
    }

    private void Fill_cmbYear_tbImperative_RequestRegister()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_tbImperative_RequestRegister, this.hfCurrentYear_RequestRegister, 0);
    }

    private void Fill_cmbMonth_tbImperative_RequestRegister()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_tbImperative_RequestRegister, this.hfCurrentMonth_RequestRegister, 0);
    }

    protected void Callback_AttachmentUploader_tbHourly_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.AttachmentUploader_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    protected void Callback_AttachmentUploader_tbDaily_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.AttachmentUploader_tbDaily_RequestRegister.RenderControl(e.Output);
    }

    [Ajax.AjaxMethod("ApplyImperativeRequest_RequestRegisterPage", "ApplyImperativeRequest_RequestRegisterPage_onCallBack", null, null)]
    public string[] ApplyImperativeRequest_RequestRegisterPage(string StrCollectivePersonnelList, string PrecardID, string Year, string Month, string ImperativeValue, string Description)
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            IList<decimal> PersonnelIDsList = this.CreatePersonnelList(this.StringBuilder.CreateString(StrCollectivePersonnelList));
            decimal precardID = decimal.Parse(this.StringBuilder.CreateString(PrecardID), CultureInfo.InvariantCulture);
            int year = int.Parse(this.StringBuilder.CreateString(Year), CultureInfo.InvariantCulture);
            int month = int.Parse(this.StringBuilder.CreateString(Month), CultureInfo.InvariantCulture);
            int imperativeValue = int.Parse(this.StringBuilder.CreateString(ImperativeValue), CultureInfo.InvariantCulture);
            Description = this.StringBuilder.CreateString(Description);

            ImperativeRequest imperativeRequest = new ImperativeRequest()
            {
                Precard = new Precard() { ID = precardID },
                Year = year,
                Month = month,
                Value = imperativeValue,
                Description = Description
            };

            this.ImperativeRequestBusiness.UpdateImperativeCollectiveRequest(imperativeRequest, PersonnelIDsList);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("ApplyComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = imperativeValue.ToString();

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

    protected void CallBack_cmbMissionLocationSearchResult_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbMissionLocationSearchResult_RequestRegister.Dispose();
        this.Fill_cmbMissionSearchResult_RequestRegister(this.StringBuilder.CreateString(e.Parameter));
        this.ErrorHiddenField_MissionLocationSearchResult_RequestRegister.RenderControl(e.Output);
        this.cmbMissionLocationSearchResult_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_cmbMissionSearchResult_RequestRegister(string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {

            IList<DutyPlace> dutyPlaceList = this.MissionLocationBusiness.SearchMissionLocations(DutyPlaceSearchFields.DutyPlaceName, SearchTerm);

            foreach (DutyPlace dutyItem in dutyPlaceList)
            {

                ComboBoxItem missionLocationCmbItem = new ComboBoxItem(dutyItem.Name);
                missionLocationCmbItem.Id = dutyItem.ID.ToString();
                missionLocationCmbItem.Text = dutyItem.Name;
                missionLocationCmbItem.Value = dutyItem.CustomCode;

                this.cmbMissionLocationSearchResult_RequestRegister.Items.Add(missionLocationCmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MissionLocationSearchResult_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MissionLocationSearchResult_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MissionLocationSearchResult_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void SetPersonnelPageSize_cmbPersonnel_Substitute_RequestRegister()
    {
        this.hfPersonnelPageSize_Substitute_RequestRegister.Value = this.cmbPersonnel_Substitute_RequestRegister.DropDownPageSize.ToString();
    }
    protected void CallBack_cmbPersonnel_Substitute_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbPersonnel_Substitute_RequestRegister.Dispose();
        this.Fill_cmbPersonnel_Substitute_RequestRegister((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[1])), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[4]), int.Parse(this.StringBuilder.CreateString(e.Parameters[5])));
        this.cmbPersonnel_Substitute_RequestRegister.RenderControl(e.Output);
        this.hfPersonnelCount_Substitute_RequestRegister.RenderControl(e.Output);
        this.hfPersonnelPageCount_Substitute_RequestRegister.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_Substitute_RequestRegister.RenderControl(e.Output);
    }
    private void Fill_cmbPersonnel_Substitute_RequestRegister(RequestCaller requestCaller, LoadState Ls, int pageSize, int pageIndex, string SearchTerm, int PersonId)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            int PersonnelCount = 0;
            PersonCategory personCategory = 0;
            switch (requestCaller)
            {
                case RequestCaller.NormalUser:
                    personCategory = PersonCategory.Public;
                    PersonId = 0;
                    break;
                case RequestCaller.Operator:
                    personCategory = PersonCategory.Operator_UnderManagment;
                    break;
                case RequestCaller.OperatorPermit:
                    personCategory = PersonCategory.Operator_UnderManagment;
                    break;
                default:
                    break;

            }
            switch (Ls)
            {
                case LoadState.Normal:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPageBySubstitute(pageIndex, pageSize, string.Empty, personCategory, PersonId);
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCountBySubstitute(string.Empty, personCategory, PersonId);
                    break;
                case LoadState.Search:
                    PersonnelList = this.PersonnelBusiness.QuickSearchByPageBySubstitute(pageIndex, pageSize, SearchTerm, personCategory, PersonId);
                    PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCountBySubstitute(SearchTerm, personCategory, PersonId);
                    break;
                case LoadState.AdvancedSearch:
                    PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize, personCategory);
                    PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), personCategory);
                    break;
            }
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                PersonnelDetails personnelDetails = new PersonnelDetails();
                personnelDetails.ID = personItem.ID.ToString();
                personnelDetails.Name = personItem.Name;
                personnelDetails.Barcode = personItem.BarCode;
                personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                personCmbItem.Value = this.JsSerializer.Serialize(personnelDetails);
                this.cmbPersonnel_Substitute_RequestRegister.Items.Add(personCmbItem);
            }

            this.hfPersonnelCount_Substitute_RequestRegister.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_Substitute_RequestRegister.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_Substitute_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_Substitute_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_Substitute_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


    protected void CallBack_bulletedListDayTraffics_tbHourly_RequestRegister_onCallBack(object sender, CallBackEventArgs e)
    {
        this.Fill_bulletedListDayTraffics_tbHourly_RequestRegister((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[2]));
        this.ErrorHiddenField_DayTraffics_RequestRegister.RenderControl(e.Output);
        this.bulletedListDayTraffics_tbHourly_RequestRegister.RenderControl(e.Output);
    }

    private void Fill_bulletedListDayTraffics_tbHourly_RequestRegister(RequestCaller RC, decimal personnelID, string date)
    {
        string[] retMessage = new string[4];
        try
        {
            if (RC == RequestCaller.NormalUser)
                personnelID = BUser.CurrentUser.Person.ID;
            if (personnelID != 0 && date != string.Empty)
            {
                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Parsi:
                        date = Utility.ToString(Utility.ToMildiDate(date));
                        break;
                    case LanguagesName.English:
                        break;
                }

                BTraffic bTraffic = new BTraffic();
                IList<BasicTrafficProxy> basicTrafficProxyList = bTraffic.GetDayTraffics(personnelID, date);
                if (basicTrafficProxyList.Count > 0)
                {
                    for (int i = 0; i < basicTrafficProxyList.Count; i++)
                    {
                        basicTrafficProxyList[i].Description = GetLocalResourceObject("Traffic").ToString() + " " + (i + 1).ToString() + " : " + basicTrafficProxyList[i].TheTime;
                    }
                }
                else
                    basicTrafficProxyList.Add(new BasicTrafficProxy() { ID = 1, Description = GetLocalResourceObject("Without").ToString() + " " + GetLocalResourceObject("Traffic").ToString() });
                this.bulletedListDayTraffics_tbHourly_RequestRegister.DataSource = basicTrafficProxyList;
                this.bulletedListDayTraffics_tbHourly_RequestRegister.DataBind();
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DayTraffics_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DayTraffics_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DayTraffics_RequestRegister.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

}