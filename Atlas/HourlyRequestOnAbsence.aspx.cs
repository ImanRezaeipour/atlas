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
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.RequestFlow;
using ComponentArt.Web.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using System.IO;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;
using Subgurim.Controles;
using System.Web.Configuration;
using GTS.Clock.Business.Security;
using System.Web.Script.Serialization;
using System.Text;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;
namespace GTS.Clock.Presentaion.WebForms
{
    public partial class HourlyRequestOnAbsence : GTSBasePage
    {
        enum RequestCaller
        {
            Grid,
            GanttChart
        }

        enum RequestLoadState
        {
            Personnel,
            Manager,
            Operator
        }

        enum RequestTypes
        {
            Leave,
            Mission
        }
        public BDutyPlace MissionLocationBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDutyPlace>();
            }
        }
        public IHourlyAbsenceBRequest RequestBusiness
        {
            get
            {
                return (IHourlyAbsenceBRequest)(BusinessHelper.GetBusinessInstance<BRequest>());
            }
        }

        public BRequest MasterRequestBusiness
        {
            get
            {
                return new BRequest();
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
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

        public JavaScriptSerializer JsSerializer
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

        internal class PersonnelDetails
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Barcode { get; set; }
            public string OrganizationPostID { get; set; }
            public string OrganizationPostName { get; set; }

        }

        internal class SubstitutePerson
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string BarCode { get; set; }
        }

        enum Scripts
        {
            HourlyRequestOnAbsence_onPageLoad,
            DialogHourlyRequestOnAbsence_Operations,
            Alert_Box,
            DropDownDive,
            HelpForm_Operations,
            DialogWaiting_Operations
        }
        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
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

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridAbsencePairs_RequestOnAbsence.IsCallback && !CallBack_GridRegisteredRequests_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbLeaveType_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbMissionType_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbDoctorName_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbIllnessName_HourlyRequestOnAbsence.IsCallback && !CallBack_cmbMissionLocation_HourlyRequestOnAbsence.IsCallback && !Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence.IsCallback && !Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence.IsCallback && !AttachmentUploader_Leave_HourlyRequestOnAbsence.IsRequesting && !AttachmentUploader_Mission_HourlyRequestOnAbsence.IsRequesting)
            {
                Page HourlyRequestOnAbsencePage = this;
                Ajax.Utility.GenerateMethodScripts(HourlyRequestOnAbsencePage);
                this.CheckHourlyRequestOnAbsenceLoadState_HourlyRequestOnAbsence();
                this.CheckNextDayTimesLoadAccess_HourlyRequestOnAbsence();
                this.SetPersonnelPageSize_cmbPersonnel_HourlyRequestOnAbsence();
                this.SetRequestsStatesStr_HourlyRequestOnAbsence();
                //DNN Note:--------------------------------------------------------
                this.SetMissionType_HourlyRequestOnAbsence();
                this.SetLeaveType_HourlyRequestOnAbsence();
                //End Of DNN Note:-------------------------------------------------
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }

            if (this.AttachmentUploader_Leave_HourlyRequestOnAbsence.IsPosting)
                this.ManagePostedData_AttachmentUploader_HourlyRequestOnAbsence(this.AttachmentUploader_Leave_HourlyRequestOnAbsence);
            if (this.AttachmentUploader_Mission_HourlyRequestOnAbsence.IsPosting)
                this.ManagePostedData_AttachmentUploader_HourlyRequestOnAbsence(this.AttachmentUploader_Mission_HourlyRequestOnAbsence);
            if (!Page.IsPostBack)
            {
                this.AttachmentUploader_Leave_HourlyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_Leave_HourlyRequestOnAbsence_OnAfterFileUpload('" + StrRequestAttachment + "');");
                this.AttachmentUploader_Leave_HourlyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.preUpload, "parent.AttachmentUploader_Leave_HourlyRequestOnAbsence_OnPreFileUpload();");
                this.AttachmentUploader_Mission_HourlyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.postUpload, "parent.AttachmentUploader_Mission_HourlyRequestOnAbsence_OnAfterFileUpload('" + StrRequestAttachment + "');");
                this.AttachmentUploader_Mission_HourlyRequestOnAbsence.addCustomJS(FileUploaderAJAX.customJSevent.preUpload, "parent.AttachmentUploader_Mission_HourlyRequestOnAbsence_OnPreFileUpload();");
            }
        }

        private void CheckNextDayTimesLoadAccess_HourlyRequestOnAbsence()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RC") && HttpContext.Current.Request.QueryString.AllKeys.Contains("RLS"))
            {
                RequestLoadState requestLoadState = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RLS"]));
                RequestCaller requestCaller = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RC"]));

                switch (requestLoadState)
                {
                    case RequestLoadState.Personnel:
                        switch (requestCaller)
                        {
                            case RequestCaller.Grid:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayHourlyRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayHourlyRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayHourlyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayHourlyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                break;
                        }
                        break;
                    case RequestLoadState.Manager:
                        switch (requestCaller)
                        {
                            case RequestCaller.Grid:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayHourlyRequestLoadAccess_onManagerLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayHourlyRequestLoadAccess_onManagerLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayHourlyRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayHourlyRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                break;
                        }
                        break;
                    case RequestLoadState.Operator:
                        switch (requestCaller)
                        {
                            case RequestCaller.Grid:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayHourlyRequestLoadAccess_onOperatorLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayHourlyRequestLoadAccess_onOperatorLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayHourlyRequestLoadAccess_onOperatorLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayHourlyRequestLoadAccess_onOperatorLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence.Visible = false;
                                    this.tblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence.Visible = false;
                                }
                                break;
                        }
                        break;
                }
            }
        }

        private void SetPersonnelPageSize_cmbPersonnel_HourlyRequestOnAbsence()
        {
            this.hfPersonnelPageSize_HourlyRequestOnAbsence.Value = this.cmbPersonnel_HourlyRequestOnAbsence.DropDownPageSize.ToString();
        }

        private void ManagePostedData_AttachmentUploader_HourlyRequestOnAbsence(FileUploaderAJAX AttachmentUploader)
        {
            try
            {
                string separator = "_";
                string RequestAttachmentsPathKey = AppFolders.RequestsAttachments.ToString();
                string path = AppDomain.CurrentDomain.BaseDirectory + RequestAttachmentsPathKey;
                HttpPostedFileAJAX HPFA = AttachmentUploader.PostedFile;
                string RequestAttachmentSavedFileName = Guid.NewGuid().ToString() + separator + BUser.CurrentUser.Person.BarCode + separator + this.StringBuilder.CreateString(HPFA.FileName, StringGeneratorExceptionType.ClientAttachments);
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

        [Ajax.AjaxMethod("DeleteRequestAttachment_HourlyRequestOnAbsencePage", "DeleteRequestAttachment_HourlyRequestOnAbsencePage_onCallBack", null, null)]
        public string[] DeleteRequestAttachment_HourlyRequestOnAbsencePage(string RequestAttachmentSavedName)
        {
            this.InitializeCulture();

            string[] retMessage = new string[3];

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

        private void CheckHourlyRequestOnAbsenceLoadState_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("RC") && HttpContext.Current.Request.QueryString.AllKeys.Contains("RLS"))
                {
                    RequestLoadState requestLoadState = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RLS"]));
                    RequestCaller requestCaller = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["RC"]));

                    switch (requestLoadState)
                    {
                        case RequestLoadState.Personnel:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Manager:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onManagerLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Operator:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onOperatorLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckHourlyRequestLoadAccess_onOperatorLoadStateInGanttChartSchema();
                                    break;
                            }
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


        private void SetRequestsStatesStr_HourlyRequestOnAbsence()
        {
            string strRequestsStates = string.Empty;
            foreach (RequestState requestsStateItem in Enum.GetValues(typeof(RequestState)))
            {
                strRequestsStates += "#" + GetLocalResourceObject(requestsStateItem.ToString()).ToString() + ":" + ((int)requestsStateItem).ToString();
            }
            this.hfRequestStates_HourlyRequestOnAbsence.Value = strRequestsStates;
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

        protected void CallBack_GridAbsencePairs_RequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_GridAbsencePairs_RequestOnAbsence((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture));
            this.ErrorHiddenField_AbsencePairs.RenderControl(e.Output);
            this.GridAbsencePairs_RequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_GridAbsencePairs_RequestOnAbsence(RequestCaller RC, string DateKey, string RequestDate, decimal PersonnelID)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<MonthlyDetailReportProxy> HourlyAbsencePairsList = this.RequestBusiness.GetAllHourlyAbsence(RequestDate, PersonnelID);
                this.GridAbsencePairs_RequestOnAbsence.DataSource = HourlyAbsencePairsList;
                this.GridAbsencePairs_RequestOnAbsence.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AbsencePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AbsencePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AbsencePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_GridRegisteredRequests_HourlyRequestOnAbsence((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture));
            this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
            this.GridRegisteredRequests_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_GridRegisteredRequests_HourlyRequestOnAbsence(RequestCaller RC, string DateKey, string RequestDate, decimal PersonnelID)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Request> RequestsList = this.RequestBusiness.GetAllHourlyLeaveDutyRequests(RequestDate, PersonnelID);
                this.GridRegisteredRequests_HourlyRequestOnAbsence.DataSource = RequestsList;
                this.GridRegisteredRequests_HourlyRequestOnAbsence.DataBind();
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
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbLeaveType_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbLeaveType_HourlyRequestOnAbsence();
            this.ErrorHiddenField_LeaveTypes.RenderControl(e.Output);
            this.cmbLeaveType_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbLeaveType_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> LeaveTypesList = this.RequestBusiness.GetAllHourlyLeaves();
                foreach (Precard LeaveTypesListItem in LeaveTypesList)
                {
                    ComboBoxItem cmbItemLeaveType = new ComboBoxItem(LeaveTypesListItem.Name);
                    cmbItemLeaveType.Value = LeaveTypesListItem.IsEstelajy.ToString().ToLower();
                    cmbItemLeaveType.Id = LeaveTypesListItem.ID.ToString();
                    this.cmbLeaveType_HourlyRequestOnAbsence.Items.Add(cmbItemLeaveType);
                }
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
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RegisteredRequests.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMissionType_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbMissionType_HourlyRequestOnAbsence();
            this.ErrorHiddenField_MissionTypes.RenderControl(e.Output);
            this.cmbMissionType_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbMissionType_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> MissionTypesList = this.RequestBusiness.GetAllHourlyDutis();
                this.cmbMissionType_HourlyRequestOnAbsence.DataSource = MissionTypesList;
                this.cmbMissionType_HourlyRequestOnAbsence.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMissionLocation_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbMissionLocation_HourlyRequestOnAbsence();
            this.ErrorHiddenField_MissionLocations.RenderControl(e.Output);
            this.cmbMissionLocation_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbMissionLocation_HourlyRequestOnAbsence()
        {
            this.Fill_trvMissionLocation_HourlyRequestOnAbsence();
        }

        private void Fill_trvMissionLocation_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                TreeViewNode trvNodeNotDetermined = new TreeViewNode();
                trvNodeNotDetermined.Text = GetLocalResourceObject("NotDetermined").ToString();
                trvNodeNotDetermined.ID = "-1";
                trvNodeNotDetermined.Value = "NotDetermined";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder_blue.gif"))
                    trvNodeNotDetermined.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder_blue.gif";
                this.trvMissionLocation_HourlyRequestOnAbsence.Nodes.Add(trvNodeNotDetermined);

                IList<DutyPlace> rootDutyPlacesList = this.RequestBusiness.GetAllDutyPlaceRoot();
                foreach (DutyPlace rootDutyPlaceItem in rootDutyPlacesList)
                {
                    TreeViewNode trvNodeRootDutyPlace = new TreeViewNode();
                    if (rootDutyPlaceItem.ParentID == 0 && this.GetLocalResourceObject("MissLocNode_trvMissionLocation_HourlyRequestOnAbsence") != null)
                        trvNodeRootDutyPlace.Text = this.GetLocalResourceObject("MissLocNode_trvMissionLocation_HourlyRequestOnAbsence").ToString();
                    else
                        trvNodeRootDutyPlace.Text = rootDutyPlaceItem.Name;
                    trvNodeRootDutyPlace.Value = trvNodeRootDutyPlace.ID = rootDutyPlaceItem.ID.ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                        trvNodeRootDutyPlace.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                    this.trvMissionLocation_HourlyRequestOnAbsence.Nodes.Add(trvNodeRootDutyPlace);
                    this.GetChildMissionLocation_trvMissionLocation_HourlyRequestOnAbsence(trvNodeRootDutyPlace, rootDutyPlaceItem);
                }

                this.trvMissionLocation_HourlyRequestOnAbsence.SelectedNode = trvNodeNotDetermined;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildMissionLocation_trvMissionLocation_HourlyRequestOnAbsence(TreeViewNode parentDutyPlaceNode, DutyPlace parentDutyPlace)
        {
            foreach (DutyPlace childDutyPlace in this.RequestBusiness.GetAllDutyPlaceChild(parentDutyPlace.ID))
            {
                TreeViewNode trvNodeChildDutyPlace = new TreeViewNode();
                trvNodeChildDutyPlace.Text = childDutyPlace.Name;
                trvNodeChildDutyPlace.ID = childDutyPlace.ID.ToString();
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\folder.gif"))
                    trvNodeChildDutyPlace.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                parentDutyPlaceNode.Nodes.Add(trvNodeChildDutyPlace);
                if (this.RequestBusiness.GetAllDutyPlaceChild(childDutyPlace.ID).Count > 0)
                    this.GetChildMissionLocation_trvMissionLocation_HourlyRequestOnAbsence(trvNodeChildDutyPlace, childDutyPlace);
            }
        }

        protected void CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDoctorName_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbDoctorName_HourlyRequestOnAbsence();
            this.ErrorHiddenField_Doctors.RenderControl(e.Output);
            this.cmbDoctorName_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbDoctorName_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
                cmbItemNotDetermined.Value = "-1";
                this.cmbDoctorName_HourlyRequestOnAbsence.Items.Add(cmbItemNotDetermined);

                IList<Doctor> DoctorsList = this.RequestBusiness.GetAllDoctors();
                foreach (Doctor DoctorItem in DoctorsList)
                {
                    ComboBoxItem cmbItemDoctor = new ComboBoxItem(DoctorItem.Nezampezaeshki + " - " + DoctorItem.Name);
                    cmbItemDoctor.Value = DoctorItem.ID.ToString();
                    this.cmbDoctorName_HourlyRequestOnAbsence.Items.Add(cmbItemDoctor);
                }
                this.cmbDoctorName_HourlyRequestOnAbsence.SelectedItem = cmbItemNotDetermined;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Doctors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Doctors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Doctors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbIllnessName_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbIllnessName_HourlyRequestOnAbsence();
            this.ErrorHiddenField_Illnesses.RenderControl(e.Output);
            this.cmbIllnessName_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbIllnessName_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                ComboBoxItem cmbItemNotDetermined = new ComboBoxItem(GetLocalResourceObject("NotDetermined").ToString());
                cmbItemNotDetermined.Value = "-1";
                this.cmbIllnessName_HourlyRequestOnAbsence.Items.Add(cmbItemNotDetermined);

                IList<Illness> IllnessesList = this.RequestBusiness.GetAllIllness();
                foreach (Illness IllnessItem in IllnessesList)
                {
                    ComboBoxItem cmbItemIllness = new ComboBoxItem(IllnessItem.Name);
                    cmbItemIllness.Value = IllnessItem.ID.ToString();
                    this.cmbIllnessName_HourlyRequestOnAbsence.Items.Add(cmbItemIllness);
                }
                this.cmbIllnessName_HourlyRequestOnAbsence.SelectedItem = cmbItemNotDetermined;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Illnesses.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Illnesses.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Illnesses.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateRequest_HourlyRequestOnAbsencePage", "UpdateRequest_HourlyRequestOnAbsencePage_onCallBack", null, null)]
        public string[] UpdateRequest_HourlyRequestOnAbsencePage(string requestCaller, string requestLoadState, string state, string SelectedRequestID, string RequestType, string PreCardID, string RequestDate, string RequestFromTime, string RequestToTime, string IsRequestToTimeInNextDay, string IsRequestFromAndToTimeInNextDay, string RequestDescription, string IsSeakLeave, string PhysicianID, string IllnessID, string MissionLocationID, string RequestAttachmentFile, string PersonnelID, string selectedsubstitute, string IsWarning)
        {
            this.InitializeCulture();

            string[] retMessage = new string[7];

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal RequestID = 0;
                decimal selectedRequestID = decimal.Parse(this.StringBuilder.CreateString(SelectedRequestID), CultureInfo.InvariantCulture);
                Request request = new Request();
                request.ID = selectedRequestID;
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
                RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(requestCaller));
                RequestLoadState RLS = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(requestLoadState));
                decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
                selectedsubstitute = this.StringBuilder.CreateString(selectedsubstitute);
                SubstitutePerson Substitute = new SubstitutePerson();
                Substitute = this.JsSerializer.Deserialize<SubstitutePerson>(selectedsubstitute);
                bool isWarning = bool.Parse(this.StringBuilder.CreateString(IsWarning));
                switch (uam)
                {
                    case UIActionType.ADD:

                        RequestTypes requestType = (RequestTypes)Enum.Parse(typeof(RequestTypes), this.StringBuilder.CreateString(RequestType));
                        decimal preCardID = decimal.Parse(this.StringBuilder.CreateString(PreCardID), CultureInfo.InvariantCulture);
                        RequestFromTime = this.StringBuilder.CreateString(RequestFromTime);
                        RequestToTime = this.StringBuilder.CreateString(RequestToTime);
                        bool isRequestToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsRequestToTimeInNextDay));
                        bool isRequestFromAndToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsRequestFromAndToTimeInNextDay));
                        RequestDescription = this.StringBuilder.CreateString(RequestDescription);
                        bool isSeakLeave = bool.Parse(this.StringBuilder.CreateString(IsSeakLeave));
                        decimal physicianID = decimal.Parse(this.StringBuilder.CreateString(PhysicianID), CultureInfo.InvariantCulture);
                        decimal illnessID = decimal.Parse(this.StringBuilder.CreateString(IllnessID), CultureInfo.InvariantCulture);
                        decimal missionLocationID = decimal.Parse(this.StringBuilder.CreateString(MissionLocationID), CultureInfo.InvariantCulture);
                        RequestAttachmentFile = this.StringBuilder.CreateString(RequestAttachmentFile);

                        request.TheFromDate = request.TheToDate = this.StringBuilder.CreateString(RequestDate);
                        Precard precard = new Precard();
                        precard.ID = preCardID;
                        request.Precard = precard;
                        request.TheFromTime = RequestFromTime;
                        request.TheToTime = RequestToTime;
                        request.ContinueOnTomorrow = isRequestToTimeInNextDay;
                        if (!isRequestToTimeInNextDay)
                            request.AllOnTomorrow = isRequestFromAndToTimeInNextDay;
                        request.Description = RequestDescription;
                        request.IsExecuteWarningUIValidation = isWarning;
                        if (Substitute != null)
                        {
                            request.SubstitutePerson = new Person() { ID = decimal.Parse(Substitute.Id, CultureInfo.InvariantCulture) };
                            request.Description += " " + string.Format("{0} {1} , {2} {3}", GetLocalResourceObject("substituteName_Description_HourlyRequestOnAbsence").ToString(), Substitute.Name, GetLocalResourceObject("substituteBarCode_Description_HourlyRequestOnAbsence").ToString(), Substitute.BarCode);
                        }
                        request.AttachmentFile = RequestAttachmentFile;
                        switch (requestType)
                        {
                            case RequestTypes.Leave:
                                if (isSeakLeave)
                                {
                                    if (physicianID != -1)
                                        request.DoctorID = physicianID;
                                    if (illnessID != -1)
                                        request.IllnessID = illnessID;
                                }
                                break;
                            case RequestTypes.Mission:
                                if (missionLocationID != -1)
                                    request.DutyPositionID = missionLocationID;
                                break;
                            default:
                                break;
                        }

                        switch (RC)
                        {
                            case RequestCaller.Grid:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertHourlyRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertHourlyRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        request.Person = new Model.Person() { ID = personnelID == 0 ? -1 : personnelID };
                                        request = this.RequestBusiness.InsertHourlyRequest_onOperatorLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertHourlyRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertHourlyRequest_onManagerLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        request.Person = new Model.Person() { ID = personnelID == 0 ? -1 : personnelID };
                                        request = this.RequestBusiness.InsertHourlyRequest_onOperatorLoadStateInGanttChartSchema(request);
                                        break;
                                }
                                break;
                        }
                        RequestID = request.ID;
                        break;
                    case UIActionType.DELETE:
                        if (selectedRequestID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRequestSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        switch (RC)
                        {
                            case RequestCaller.Grid:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.MasterRequestBusiness.GetRequestByID(request.ID);
                                        this.RequestBusiness.DeleteHourlyRequest_onPersonnelLoadStateInGridSchema(request);
                                        this.MasterRequestBusiness.DeleteRequestAttachment(AppDomain.CurrentDomain.BaseDirectory + AppFolders.RequestsAttachments.ToString() + "\\" + request.AttachmentFile);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteHourlyRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        this.RequestBusiness.DeleteHourlyRequest_onOperatorLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.MasterRequestBusiness.GetRequestByID(request.ID);
                                        this.RequestBusiness.DeleteHourlyRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        this.MasterRequestBusiness.DeleteRequestAttachment(AppDomain.CurrentDomain.BaseDirectory + AppFolders.RequestsAttachments.ToString() + "\\" + request.AttachmentFile);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteHourlyRequest_onManagerLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        this.RequestBusiness.DeleteHourlyRequest_onOperatorLoadStateInGanttChartSchema(request);
                                        break;
                                }
                                break;
                        }
                        break;
                }

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case Business.UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case Business.UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = RequestID.ToString();
                retMessage[4] = ((int)request.Status).ToString();
                retMessage[5] = request.RegistrationDate;
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

        protected void Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.AttachmentUploader_Leave_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        protected void Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.AttachmentUploader_Mission_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        protected void CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMissionLocationSearchResult_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbMissionSearchResult_HourlyRequestOnAbsence(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_MissionLocationSearchResult_HourlyRequestOnAbsence.RenderControl(e.Output);
            this.cmbMissionLocationSearchResult_HourlyRequestOnAbsence.RenderControl(e.Output);
        }

        private void Fill_cmbMissionSearchResult_HourlyRequestOnAbsence(string SearchTerm)
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

                    this.cmbMissionLocationSearchResult_HourlyRequestOnAbsence.Items.Add(missionLocationCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionLocationSearchResult_HourlyRequestOnAbsence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionLocationSearchResult_HourlyRequestOnAbsence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionLocationSearchResult_HourlyRequestOnAbsence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbPersonnel_HourlyRequestOnAbsence_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbPersonnel_HourlyRequestOnAbsence.Dispose();
            this.Fill_cmbPersonnel_HourlyRequestOnAbsence((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_HourlyRequestOnAbsence.RenderControl(e.Output);
            this.hfPersonnelCount_HourlyRequestOnAbsence.RenderControl(e.Output);
            this.hfPersonnelPageCount_HourlyRequestOnAbsence.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_HourlyRequestOnAbsence.RenderControl(e.Output);
        }
        private void Fill_cmbPersonnel_HourlyRequestOnAbsence(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Person> PersonnelList = null;
                int PersonnelCount = 0;
                int PersonId = 0;
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelList = this.PersonnelBusiness.QuickSearchByPageBySubstitute(pageIndex, pageSize, string.Empty, PersonCategory.Public, PersonId);
                        PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCountBySubstitute(string.Empty, PersonCategory.Public, PersonId);
                        break;
                    case LoadState.Search:
                        PersonnelList = this.PersonnelBusiness.QuickSearchByPageBySubstitute(pageIndex, pageSize, SearchTerm, PersonCategory.Public, PersonId);
                        PersonnelCount = this.PersonnelBusiness.GetPersonInQuickSearchCountBySubstitute(SearchTerm, PersonCategory.Public, PersonId);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelList = this.PersonnelBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize, PersonCategory.Public);
                        PersonnelCount = this.PersonnelBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), PersonCategory.Public);
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
                    this.cmbPersonnel_HourlyRequestOnAbsence.Items.Add(personCmbItem);
                }

                this.hfPersonnelCount_HourlyRequestOnAbsence.Value = PersonnelCount.ToString();
                this.hfPersonnelPageCount_HourlyRequestOnAbsence.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_HourlyRequestOnAbsence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_HourlyRequestOnAbsence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_HourlyRequestOnAbsence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        //DNN Note:------------------------------------------------
        private void SetMissionType_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> MissionTypesList = this.RequestBusiness.GetAllHourlyDutis();
                string strMissionType = string.Empty;
                foreach (Precard item in MissionTypesList)
                {
                    strMissionType += "#" + item.Name + ":" + item.ID.ToString();
                }
                this.hfMissionType_HourlyRequestOnAbsence.Value = strMissionType;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        private void SetLeaveType_HourlyRequestOnAbsence()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> LeaveTypesList = this.RequestBusiness.GetAllHourlyLeaves();
                string strLeaveType = string.Empty;
                foreach (Precard item in LeaveTypesList)
                {
                    strLeaveType += "#" + item.Name + ":" + item.ID.ToString();
                }
                this.hfLeaveType_HourlyRequestOnAbsence.Value = strLeaveType;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_LeaveTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_LeaveTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_LeaveTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        //End Of DNN Note:------------------------------------------------
    }
}

