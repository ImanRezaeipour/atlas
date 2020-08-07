using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.BaseInformation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefinePhysicians : GTSBasePage
{
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
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }
    }
    public BDoctor PhysiciansBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDoctor>();
        }
    }
    enum Scripts
    {
        DialogDefinePhysicians_onPageLoad,
        DefinePhysicians_onPageLoad,
        DialogDefinePhysicians_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations

    }
    private enum RequestTarget
    {
        Hourly,
        Daily,
        OverTime,
        Imperative
    }
    enum UserCallerEnum
    {
        NormalUser,
        Operator,
        OperatorPermit
    }
    private enum PageCaller
    {
        RequestRegister,
        DailyRequestOnAbsence,
        HourlyRequestOnAbsence
    }

    enum RequestCaller
    {
        Grid,
        GanttChart,
        RequestRegister,
    }

    enum RequestLoadState
    {
        Personnel,
        Manager,
        Operator,
        Integral
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();

        Page DefinePhysiciansPage = this;
        Ajax.Utility.GenerateMethodScripts(DefinePhysiciansPage);
        this.InitializeSkin();
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

    [Ajax.AjaxMethod("UpdatePhysician_DefinePhysiciansPage", "UpdatePhysician_DefinePhysiciansPage_onCallBack", null, null)]
    public string[] UpdatePhysician_DefinePhysiciansPage(string state, string requestCaller, string requestLoadState, string PhysiciansID, string FirstName, string LastName, string Proficiency, string MedicalAssociation, string Description, string Caller, string Target, string UserCaller)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal PhysicianID = 0;
            decimal selectedPhysicianID = decimal.Parse(this.StringBuilder.CreateString(PhysiciansID), CultureInfo.InvariantCulture);
            FirstName = this.StringBuilder.CreateString(FirstName);
            LastName = this.StringBuilder.CreateString(LastName);
            Proficiency = this.StringBuilder.CreateString(Proficiency);
            MedicalAssociation = this.StringBuilder.CreateString(MedicalAssociation);
            Description = this.StringBuilder.CreateString(Description);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(requestCaller));
            RequestLoadState RLS = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(requestLoadState));
            RequestTarget target = (RequestTarget)Enum.Parse(typeof(RequestTarget), this.StringBuilder.CreateString(Target));
            PageCaller caller = (PageCaller)Enum.Parse(typeof(PageCaller), this.StringBuilder.CreateString(Caller));
            UserCallerEnum userCaller = (UserCallerEnum)Enum.Parse(typeof(UserCallerEnum), this.StringBuilder.CreateString(UserCaller));
            Doctor physician = new Doctor();
            physician.ID = selectedPhysicianID;
            if (uam != UIActionType.DELETE)
            {
                physician.FirstName = FirstName;
                physician.LastName = LastName;
                physician.Takhasos = Proficiency;
                physician.Nezampezaeshki = MedicalAssociation;
                physician.Description = Description;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    switch (target)
                    {
                        case RequestTarget.Hourly:
                            switch (caller)
                            {
                                case PageCaller.RequestRegister:
                                    switch (userCaller)
                                    {
                                        case UserCallerEnum.NormalUser:
                                            PhysicianID = this.PhysiciansBusiness.InsertPhysician_onRequestHourly_onRequestRegister(physician, uam);
                                            break;
                                        case UserCallerEnum.Operator:
                                            PhysicianID = this.PhysiciansBusiness.InsertPhysician_onRequestHourly_onRequestRegisterByOperator(physician, uam);
                                            break;
                                        case UserCallerEnum.OperatorPermit:
                                            PhysicianID = this.PhysiciansBusiness.InsertPhysician_onRequestHourly_onRequestRegisterByOperatorPermit(physician, uam);
                                            break;
                                    }
                                    break;
                                case PageCaller.DailyRequestOnAbsence:
                                    break;
                                case PageCaller.HourlyRequestOnAbsence:
                                    switch (RC)
	                                {
                                        case RequestCaller.Grid:
                                            switch (RLS)
	                                        {
                                                case RequestLoadState.Personnel:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onHourlyRequest_onPersonnel_onGridSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onHourlyRequest_onManager_onGridSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onHourlyRequest_onOperator_onGridSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Integral:
                                                    break;
                                                default:
                                                    break;
	                                        }
                                            break;
                                        case RequestCaller.GanttChart:
                                            switch (RLS)
                                            {
                                                case RequestLoadState.Personnel:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onHourlyRequest_onPersonnel_onGanttChartSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onHourlyRequest_onManager_onGanttChartSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onHourlyRequest_onOperator_onGanttChartSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Integral:
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        case RequestCaller.RequestRegister:
                                            break;
                                        default:
                                            break;
	                                }
                                    break;
                            }
                            break;
                        case RequestTarget.Daily:
                            switch (caller)
                            {
                                case PageCaller.RequestRegister:
                                    switch (userCaller)
                                    {
                                        case UserCallerEnum.NormalUser:
                                            PhysicianID = this.PhysiciansBusiness.InsertPhysician_onRequestDaily_onRequestRegister(physician, uam);
                                            break;
                                        case UserCallerEnum.Operator:
                                            PhysicianID = this.PhysiciansBusiness.InsertPhysician_onRequestDaily_onRequestRegisterByOperator(physician, uam);
                                            break;
                                        case UserCallerEnum.OperatorPermit:
                                            PhysicianID = this.PhysiciansBusiness.InsertPhysician_onRequestDaily_onRequestRegisterByOperatorPermit(physician, uam);
                                            break;
                                    }
                                    break;
                                case PageCaller.HourlyRequestOnAbsence:
                                    break;
                                case PageCaller.DailyRequestOnAbsence:
                                    switch (RC)
                                    {
                                        case RequestCaller.Grid:
                                            switch (RLS)
                                            {
                                                case RequestLoadState.Personnel:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onDailyRequest_onPersonnel_onGridSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onDailyRequest_onManager_onGridSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onDailyRequest_onOperator_onGridSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Integral:
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        case RequestCaller.GanttChart:
                                            switch (RLS)
                                            {
                                                case RequestLoadState.Personnel:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onDailyRequest_onPersonnel_onGanttChartSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onDailyRequest_onManager_onGanttChartSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    PhysicianID = this.PhysiciansBusiness.InsertPhysician_onDailyRequest_onOperator_onGanttChartSchema(physician, uam);
                                                    break;
                                                case RequestLoadState.Integral:
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        case RequestCaller.RequestRegister:
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case RequestTarget.OverTime:
                            break;
                        case RequestTarget.Imperative:
                            break;
                    }
                    break;
                case UIActionType.EDIT:
                    break;
                case UIActionType.DELETE:
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.ADD:
                    SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                    break;
                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = PhysicianID.ToString();
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