using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RequestHistory : GTSBasePage
{
    public enum RequestCaller
    {
        Kartable,
        Survey,
        Sentry,
        SpecialKartable,
        RegisteredRequest,

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
    private string StrRequestAttachment = string.Empty;
    public BKartabl KartableBusiness
    {
        get
        {
            return (BKartabl)(BusinessHelper.GetBusinessInstance<BKartabl>());

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
    public IRegisteredRequests RequestRegisterBusiness
    {
        get
        {
            return (IRegisteredRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
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
    enum Scripts
    {
        RequestHistory_onPageLoad,
        DialogRequestHistory_Operations,
        DialogWaiting_Operations,
        Alert_Box
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridRequestHistory_RequestHistory.IsCallback && !Callback_AttachmentUploader_RequestHistory.IsCallback)
        {
            Page EndorsementFlowState = this;
            Ajax.Utility.GenerateMethodScripts(EndorsementFlowState);
            this.CheckNextDayTimesLoadAccess_RequestHistory();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.ViewCurrentLangCalendars_RequestHistory();
            this.SetCurrentDate_RequestHistory();
            this.CustomizeControls_RequestHistory();
        }
        if (this.AttachmentUploader_RequestHistory.IsPosting)
            this.ManagePostedData_AttachmentUploader_RequestHistory(this.AttachmentUploader_RequestHistory);
        if (!Page.IsPostBack)
        {
            this.AttachmentUploader_RequestHistory.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_RequestHistory_OnAfterFileUpload('" + StrRequestAttachment + "');");
            this.AttachmentUploader_RequestHistory.addCustomJS(FileUploaderAJAX.customJSevent.preUpload, "parent.AttachmentUploader_RequestHistory_OnPreFileUpload();");
        }
    }
    private void ManagePostedData_AttachmentUploader_RequestHistory(FileUploaderAJAX AttachmentUploader)
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
    protected void Callback_AttachmentUploader_RequestHistory_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.AttachmentUploader_RequestHistory.RenderControl(e.Output);
    }
    [Ajax.AjaxMethod("DeleteRequestAttachment_RequestHistoryPage", "DeleteRequestAttachment_RequestHistoryPage_onCallBack", null, null)]
    public string[] DeleteRequestAttachment_RequestHistoryPage(string RequestAttachmentSavedName)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            RequestAttachmentSavedName = this.StringBuilder.CreateString(RequestAttachmentSavedName);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + AppFolders.RequestsAttachments.ToString() + "\\" + RequestAttachmentSavedName;
            this.MasterRequestBusiness.DeleteRequestAttachment(filePath);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("DeleteComplete").ToString();
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
    private void CheckNextDayTimesLoadAccess_RequestHistory()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller") && HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestType"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            RequestType RT = (RequestType)Enum.Parse(typeof(RequestType), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestType"]));
            switch (RC)
            {
                case RequestCaller.Kartable:
                    switch (RT)
                    {
                        case RequestType.Hourly:
                            try
                            {
                                this.KartableBusiness.CheckNextDayHourlyRequestLoadAccess_RequestHistory_onKartable();
                            }
                            catch (Exception)
                            {
                                this.tblToHourInNextDay_tbHourly_RequestHistory.Visible = false;
                            }
                            try
                            {
                                this.KartableBusiness.CheckAllNextDayHourlyRequestLoadAccess_RequestHistory_onKartable();
                            }
                            catch (Exception)
                            {
                                this.tblFromAndToHourInNextDay_tbHourly_RequestHistory.Visible = false;
                            }
                            break;
                        case RequestType.OverWork:
                            try
                            {
                                this.KartableBusiness.CheckNextDayOvertimeRequestLoadAccess_RequestHistory_onKartable();
                            }
                            catch (Exception)
                            {
                                this.tblToHourInNextDay_tbOvertime_RequestHistory.Visible = false;
                            }
                            try
                            {
                                this.KartableBusiness.CheckAllNextDayOvertimeRequestLoadAccess_RequestHistory_onKartable();
                            }
                            catch (Exception)
                            {
                                this.tblFromAndToHourInNextDay_tbOvertime_RequestHistory.Visible = false;
                            }
                            break;
                    }
                    break;
                case RequestCaller.SpecialKartable:
                    switch (RT)
                    {
                        case RequestType.Hourly:
                            try
                            {
                                this.KartableBusiness.CheckNextDayHourlyRequestLoadAccess_RequestHistory_onSpecialKartable();
                            }
                            catch (Exception)
                            {
                                this.tblToHourInNextDay_tbHourly_RequestHistory.Visible = false;
                            }
                            try
                            {
                                this.KartableBusiness.CheckAllNextDayHourlyRequestLoadAccess_RequestHistory_onSpecialKartable();
                            }
                            catch (Exception)
                            {
                                this.tblFromAndToHourInNextDay_tbHourly_RequestHistory.Visible = false;
                            }
                            break;
                        case RequestType.OverWork:
                            try
                            {
                                this.KartableBusiness.CheckNextDayOvertimeRequestLoadAccess_RequestHistory_onSpecialKartable();
                            }
                            catch (Exception)
                            {
                                this.tblToHourInNextDay_tbOvertime_RequestHistory.Visible = false;
                            }
                            try
                            {
                                this.KartableBusiness.CheckAllNextDayOvertimeRequestLoadAccess_RequestHistory_onSpecialKartable();
                            }
                            catch (Exception)
                            {
                                this.tblFromAndToHourInNextDay_tbOvertime_RequestHistory.Visible = false;
                            }
                            break;
                    }
                    break;
            }
        }
    }

    private void SetCurrentDate_RequestHistory()
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
        this.hfCurrentDate_RequestHistory.Value = strCurrentDate;
    }

    private void ViewCurrentLangCalendars_RequestHistory()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_tbDaily_RequestHistory.Visible = true;
                this.Container_pdpFromDate_tbOverTime_RequestHistory.Visible = true;
                this.Container_pdpRequestDate_tbHourly_RequestHistory.Visible = true;
                this.Container_pdpToDate_tbDaily_RequestHistory.Visible = true;
                this.Container_pdpToDate_tbOverTime_RequestHistory.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_tbDaily_RequestHistory.Visible = true;
                this.Container_gdpFromDate_tbOverTime_RequestHistory.Visible = true;
                this.Container_gdpRequestDate_tbHourly_RequestHistory.Visible = true;
                this.Container_gdpToDate_tbDaily_RequestHistory.Visible = false;
                this.Container_gdpToDate_tbOverTime_RequestHistory.Visible = false;
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

    protected void CallBack_GridRequestHistory_RequestHistory_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridRequestHistory_RequestHistory(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture));
        this.ErrorHiddenField_History_RequestHistory.RenderControl(e.Output);
        this.GridRequestHistory_RequestHistory.RenderControl(e.Output);
    }

    private void Fill_GridRequestHistory_RequestHistory(decimal RequestID)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<GTS.Clock.Model.RequestFlow.RequestHistory> RequestHistoryList = null;
            RequestHistoryList = this.KartableBusiness.GetRequestHistoryByRequestID(RequestID);

            this.GridRequestHistory_RequestHistory.DataSource = RequestHistoryList;
            this.GridRequestHistory_RequestHistory.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_History_RequestHistory.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_History_RequestHistory.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_History_RequestHistory.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateRequest_RequestHistoryPage", "UpdateRequest_RequestHistoryPage_onCallBack", null, null)]
    public string[] UpdateRequest_RequestHistoryPage(string RequestID, string FromDate, string ToDate, string FromTime, string ToTime, string IsToTimeInNextDay, string IsFromAndToTimeInNextDay, string RequestDate, string Duration, string RequestTargetType, string Caller,string AttachmentFile,string OldAttachmentFile)
    {
        string[] retMessage = new string[5];
        this.InitializeCulture();

        try
        {
            AttackDefender.CSRFDefender(this.Page);


            decimal requestID = decimal.Parse(this.StringBuilder.CreateString(RequestID), CultureInfo.InvariantCulture);
            RequestDate = this.StringBuilder.CreateString(RequestDate);
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            FromTime = this.StringBuilder.CreateString(FromTime);
            ToTime = this.StringBuilder.CreateString(ToTime);
            bool isToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsToTimeInNextDay));
            bool isFromAndToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsFromAndToTimeInNextDay));
            Duration = this.StringBuilder.CreateString(Duration);
            AttachmentFile = this.StringBuilder.CreateString(AttachmentFile);
            OldAttachmentFile = this.StringBuilder.CreateString(OldAttachmentFile);
            RequestType requestType = (RequestType)Enum.Parse(typeof(RequestType), this.StringBuilder.CreateString(RequestTargetType));
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(Caller));
            GTS.Clock.Model.RequestFlow.RequestHistory requestHistoryObj = new GTS.Clock.Model.RequestFlow.RequestHistory();
            GTS.Clock.Model.RequestFlow.Request requestObj = new GTS.Clock.Model.RequestFlow.Request();
            switch (requestType)
            {
                case RequestType.None:
                    break;
                case RequestType.Hourly:
                    requestHistoryObj.TheFromDate = RequestDate;
                    requestHistoryObj.TheFromTime = FromTime;
                    requestHistoryObj.TheToDate = RequestDate;
                    requestHistoryObj.TheToTime = ToTime;
                    requestHistoryObj.ContinueOnTomorrow = isToTimeInNextDay;
                    if (!isToTimeInNextDay)
                        requestHistoryObj.AllOnTomorrow = isFromAndToTimeInNextDay; 
                    break;
                case RequestType.Daily:
                    requestHistoryObj.TheFromDate = FromDate;
                    requestHistoryObj.TheToDate = ToDate;
                    break;
                case RequestType.Monthly:
                    break;
                case RequestType.OverWork:
                    requestHistoryObj.TheFromDate = FromDate;
                    requestHistoryObj.TheToDate = ToDate;
                    requestHistoryObj.TheFromTime = FromTime;
                    requestHistoryObj.TheToTime = ToTime;
                    requestHistoryObj.TheDuration = Duration;
                    requestHistoryObj.ContinueOnTomorrow = isToTimeInNextDay;
                    if (!isToTimeInNextDay)
                        requestHistoryObj.AllOnTomorrow = isFromAndToTimeInNextDay; 
                    break;
                case RequestType.Imperative:
                    break;
                default:
                    break;
            }
            requestObj.ID = requestID;
            requestHistoryObj.Request = requestObj;
            requestHistoryObj.RequestType = requestType;
            requestHistoryObj.AttachmentFile = OldAttachmentFile;
            requestHistoryObj.NewAttachmentFile = AttachmentFile;
            switch (RC)
            {
                case RequestCaller.Kartable:
                    KartableBusiness.UpdateRequestByManager_onKartable(requestHistoryObj);
                    break;
                case RequestCaller.SpecialKartable:
                    KartableBusiness.UpdateRequestByManager_onSpecialKartable(requestHistoryObj);
                    break;
                default:
                    break;
            }
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("EditComplete").ToString();
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

    public void CustomizeControls_RequestHistory()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestCaller") && HttpContext.Current.Request.QueryString.AllKeys.Contains("RequestType"))
        {
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestCaller"]));
            RequestType requestType = (RequestType)Enum.Parse(typeof(RequestType), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RequestType"]));
            switch (RC)
            {
                case RequestCaller.Kartable:
                    switch (requestType)
                    {
                        case RequestType.None:

                            break;
                        case RequestType.Hourly:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        case RequestType.Daily:
                            tblRequestDaily_RequestHistory.Style["display"] = "";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        case RequestType.Monthly:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        case RequestType.OverWork:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "";
                            break;
                        case RequestType.Imperative:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        default:
                            break;
                    }
                    break;
                case RequestCaller.Survey:
                    tblEditRequest_RequestHistory.Style["display"] = "none";
                    tlbItemSave_TlbRequest_RequestHistory.Visible = false;

                    break;
                case RequestCaller.Sentry:
                    tblEditRequest_RequestHistory.Style["display"] = "none";
                    tlbItemSave_TlbRequest_RequestHistory.Visible = false;
                    break;
                case RequestCaller.SpecialKartable:
                    switch (requestType)
                    {
                        case RequestType.None:

                            break;
                        case RequestType.Hourly:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        case RequestType.Daily:
                            tblRequestDaily_RequestHistory.Style["display"] = "";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        case RequestType.Monthly:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        case RequestType.OverWork:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "";
                            break;
                        case RequestType.Imperative:
                            tblRequestDaily_RequestHistory.Style["display"] = "none";
                            tblRequestHourly_RequestHistory.Style["display"] = "none";
                            tblRequestOverTime_RequestHistory.Style["display"] = "none";
                            break;
                        default:
                            break;
                    }
                    break;
                case RequestCaller.RegisteredRequest:
                    tblEditRequest_RequestHistory.Style["display"] = "none";
                    tlbItemSave_TlbRequest_RequestHistory.Visible = false;
                    break;
                default:
                    tblEditRequest_RequestHistory.Style["display"] = "none";
                    tlbItemSave_TlbRequest_RequestHistory.Visible = false;
                    break;
            }
        }
    }
}