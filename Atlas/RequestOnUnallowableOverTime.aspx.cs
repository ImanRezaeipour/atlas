using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Globalization;
using System.Threading;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class RequestOnUnallowableOverTime : GTSBasePage
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

        public IOverTimeBRequest RequestBusiness
        {
            get
            {
                return (IOverTimeBRequest)(BusinessHelper.GetBusinessInstance<BRequest>());
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

        enum Scripts
        {
            RequestOnUnallowableOverTime_onPageLoad,
            DialogRequestOnUnallowableOverTime_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime.IsCallback && !CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime.IsCallback && !CallBack_cmbOverTimeType_RequestOnUnallowableOverTime.IsCallback)
            {
                Page RequestOnTrafficPage = this;
                Ajax.Utility.GenerateMethodScripts(RequestOnTrafficPage);

                this.CheckRequestOnUnallowableOverTimeLoadState_RequestOnUnallowableOverTime();
                this.CheckNextDayTimesLoadAccess_RequestOnUnallowableOverTime();
                this.SetRequestsStatesStr_RequestOnUnallowableOverTime();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckRequestOnUnallowableOverTimeLoadState_RequestOnUnallowableOverTime()
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
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Manager:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onManagerLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
                                    break;
                            }
                            break;
                        case RequestLoadState.Operator:
                            switch (requestCaller)
                            {
                                case RequestCaller.Grid:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onOperatorLoadStateInGridSchema();
                                    break;
                                case RequestCaller.GanttChart:
                                    this.RequestBusiness.CheckOverTimeRequestLoadAccess_onOperatorLoadStateInGanttChartSchema();
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

        private void CheckNextDayTimesLoadAccess_RequestOnUnallowableOverTime()
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
                                    this.RequestBusiness.CheckNextDayOvertimeRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayOvertimeRequestLoadAccess_onPersonnelLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayOvertimeRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayOvertimeRequestLoadAccess_onPersonnelLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
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
                                    this.RequestBusiness.CheckNextDayOvertimeRequestLoadAccess_onManagerLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayOvertimeRequestLoadAccess_onManagerLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayOvertimeRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayOvertimeRequestLoadAccess_onManagerLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
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
                                    this.RequestBusiness.CheckNextDayOvertimeRequestLoadAccess_onOperatorLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayOvertimeRequestLoadAccess_onOperatorLoadStateInGridSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                try
                                {
                                    this.RequestBusiness.CheckNextDayOvertimeRequestLoadAccess_onOperatorLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                try
                                {
                                    this.RequestBusiness.CheckAllNextDayOvertimeRequestLoadAccess_onOperatorLoadStateInGanttChartSchema();
                                }
                                catch (Exception)
                                {
                                    this.tblFromAndToHourInNextDay_RequestOnUnallowableOverTime.Visible = false;
                                }
                                break;
                        }
                        break;
                }
            }
        }


        private void SetRequestsStatesStr_RequestOnUnallowableOverTime()
        {
            string strRequestsStates = string.Empty;
            foreach (RequestState requestsStateItem in Enum.GetValues(typeof(RequestState)))
            {
                strRequestsStates += "#" + GetLocalResourceObject(requestsStateItem.ToString()).ToString() + ":" + ((int)requestsStateItem).ToString();
            }
            this.hfRequestStates_RequestOnUnallowableOverTime.Value = strRequestsStates;
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

        protected void CallBack_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture));
            this.ErrorHiddenField_UnallowableOverTimePairs.RenderControl(e.Output);
            this.GridUnallowableOverTimePairs_RequestOnUnallowableOverTime.RenderControl(e.Output);
        }

        private void Fill_GridUnallowableOverTimePairs_RequestOnUnallowableOverTime(RequestCaller RC, string DateKey, string RequestDate, decimal PersonnelID)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<MonthlyDetailReportProxy> UnallowableOverTimePairsList = this.RequestBusiness.GetAllUnallowedOverworks(RequestDate, PersonnelID);
                this.GridUnallowableOverTimePairs_RequestOnUnallowableOverTime.DataSource = UnallowableOverTimePairsList;
                this.GridUnallowableOverTimePairs_RequestOnUnallowableOverTime.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_UnallowableOverTimePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_UnallowableOverTimePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_UnallowableOverTimePairs.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }

        protected void CallBack_GridRegisteredRequests_RequestOnUnallowableOverTime_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_GridRegisteredRequests_RequestOnUnallowableOverTime((RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]), this.StringBuilder.CreateString(e.Parameters[2]), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture));
            this.ErrorHiddenField_RegisteredRequests.RenderControl(e.Output);
            this.GridRegisteredRequests_RequestOnUnallowableOverTime.RenderControl(e.Output);
        }

        private void Fill_GridRegisteredRequests_RequestOnUnallowableOverTime(RequestCaller RC, string DateKey, string RequestDate, decimal PersonnelID)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Request> RequestsList = this.RequestBusiness.GetAllOverTimeRequests(RequestDate, PersonnelID);
                this.GridRegisteredRequests_RequestOnUnallowableOverTime.DataSource = RequestsList;
                this.GridRegisteredRequests_RequestOnUnallowableOverTime.DataBind();
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

        protected void CallBack_cmbOverTimeType_RequestOnUnallowableOverTime_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbOverTimeType_RequestOnUnallowableOverTime.Dispose();
            this.Fill_cmbOverTimeType_RequestOnUnallowableOverTime();
            this.ErrorHiddenField_OverTimeTypes.RenderControl(e.Output);
            this.cmbOverTimeType_RequestOnUnallowableOverTime.RenderControl(e.Output);
        }

        private void Fill_cmbOverTimeType_RequestOnUnallowableOverTime()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Precard> OverTimeTypesList = this.RequestBusiness.GetAllOverWorks();
                this.cmbOverTimeType_RequestOnUnallowableOverTime.DataSource = OverTimeTypesList;
                this.cmbOverTimeType_RequestOnUnallowableOverTime.DataBind();
                this.cmbOverTimeType_RequestOnUnallowableOverTime.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OverTimeTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OverTimeTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OverTimeTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


        [Ajax.AjaxMethod("UpdateRequest_RequestOnUnallowableOverTimePage", "UpdateRequest_RequestOnUnallowableOverTimePage_onCallBack", null, null)]
        public string[] UpdateRequest_RequestOnUnallowableOverTimePage(string requestCaller, string requestLoadState, string state, string SelectedRequestID, string PreCardID, string RequestDate, string RequestFromTime, string RequestToTime, string IsRequestToTimeInNextDay, string IsRequestFromAndToTimeInNextDay, string RequestDescription, string PersonnelID, string IsWarning)
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
                bool isWarning = bool.Parse(this.StringBuilder.CreateString(IsWarning));

                switch (uam)
                {
                    case UIActionType.ADD:
                        decimal preCardID = decimal.Parse(this.StringBuilder.CreateString(PreCardID), CultureInfo.InvariantCulture);
                        RequestFromTime = this.StringBuilder.CreateString(RequestFromTime);
                        RequestToTime = this.StringBuilder.CreateString(RequestToTime);
                        bool isRequestToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsRequestToTimeInNextDay));
                        bool isRequestFromAndToTimeInNextDay = bool.Parse(this.StringBuilder.CreateString(IsRequestFromAndToTimeInNextDay));
                        RequestDescription = this.StringBuilder.CreateString(RequestDescription);

                        request.TheFromDate = request.TheToDate = this.StringBuilder.CreateString(RequestDate);
                        Precard precard = new Precard();
                        precard.ID = preCardID;
                        request.Precard = precard;
                        if (RequestFromTime != string.Empty)
                            request.TheFromTime = RequestFromTime;
                        if (RequestToTime != string.Empty)
                        {
                            request.TheToTime = RequestToTime;
                            request.ContinueOnTomorrow = isRequestToTimeInNextDay;
                            if (!isRequestToTimeInNextDay)
                                request.AllOnTomorrow = isRequestFromAndToTimeInNextDay;
                        }
                        request.Description = RequestDescription;
                        request.IsExecuteWarningUIValidation = isWarning;
                        switch (RC)
	                    {
                            case RequestCaller.Grid: 
                                switch (RLS)
	                            {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        request.Person = new Model.Person() { ID = personnelID };
                                        request = this.RequestBusiness.InsertOverTimeRequest_onOperatorLoadStateInGridSchema(request);
                                        break;
	                            }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        request = this.RequestBusiness.InsertOverTimeRequest_onManagerLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        request.Person = new Model.Person() { ID = personnelID };
                                        request = this.RequestBusiness.InsertOverTimeRequest_onOperatorLoadStateInGanttChartSchema(request);
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
                                        this.RequestBusiness.DeleteOverTimeRequest_onPersonnelLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteOverTimeRequest_onManagerLoadStateInGridSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        this.RequestBusiness.DeleteOverTimeRequest_onOperatorLoadStateInGridSchema(request);
                                        break;
                                }
                                break;
                            case RequestCaller.GanttChart:
                                switch (RLS)
                                {
                                    case RequestLoadState.Personnel:
                                        this.RequestBusiness.DeleteOverTimeRequest_onPersonnelLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Manager:
                                        this.RequestBusiness.DeleteOverTimeRequest_onManagerLoadStateInGanttChartSchema(request);
                                        break;
                                    case RequestLoadState.Operator:
                                        this.RequestBusiness.DeleteOverTimeRequest_onOperatorLoadStateInGanttChartSchema(request);
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


    }

}