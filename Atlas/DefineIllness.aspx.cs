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

public partial class DefineIllness : GTSBasePage
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
    public BIllness IllnessBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BIllness>();
        }
    }
    enum Scripts
    {
        DialogDefineIllness_onPageLoad,
        DefineIllness_onPageLoad,
        DialogDefineIllness_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations

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
    private enum RequestTarget
    {
        Hourly,
        Daily,
        OverTime,
        Imperative
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

    [Ajax.AjaxMethod("UpdateIllness_DefineIllnessPage", "UpdateIllness_DefineIllnessPage_onCallBack", null, null)]
    public string[] UpdateIllness_DefineIllnessPage(string state, string requestCaller, string requestLoadState, string IllnessID, string NameIllness, string Description, string Caller, string Target, string UserCaller)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal illnessID = 0;
            decimal selectedIllnessID = decimal.Parse(this.StringBuilder.CreateString(IllnessID), CultureInfo.InvariantCulture);
            NameIllness = this.StringBuilder.CreateString(NameIllness);

            Description = this.StringBuilder.CreateString(Description);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            RequestTarget target = (RequestTarget)Enum.Parse(typeof(RequestTarget), this.StringBuilder.CreateString(Target));
            PageCaller caller = (PageCaller)Enum.Parse(typeof(PageCaller), this.StringBuilder.CreateString(Caller));
            UserCallerEnum userCaller = (UserCallerEnum)Enum.Parse(typeof(UserCallerEnum), this.StringBuilder.CreateString(UserCaller));
            RequestCaller RC = (RequestCaller)Enum.Parse(typeof(RequestCaller), this.StringBuilder.CreateString(requestCaller));
            RequestLoadState RLS = (RequestLoadState)Enum.Parse(typeof(RequestLoadState), this.StringBuilder.CreateString(requestLoadState));
            Illness illness = new Illness();
            illness.ID = selectedIllnessID;
            if (uam != UIActionType.DELETE)
            {
                illness.Name = NameIllness;
                illness.Description = Description;
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
                                            illnessID = this.IllnessBusiness.InsertIllness_onRequestHourly_onRequestRegister(illness, uam);
                                            break;
                                        case UserCallerEnum.Operator:
                                            illnessID = this.IllnessBusiness.InsertIllness_onRequestHourly_onRequestRegisterByOperator(illness, uam);
                                            break;
                                        case UserCallerEnum.OperatorPermit:
                                            illnessID = this.IllnessBusiness.InsertIllness_onRequestHourly_onRequestRegisterByOperatorPermit(illness, uam);
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
                                                    this.IllnessBusiness.InsertIllness_onHourlyRequest_onPersonnel_onGridSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    this.IllnessBusiness.InsertIllness_onHourlyRequest_onManager_onGridSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    this.IllnessBusiness.InsertIllness_onHourlyRequest_onOperator_onGridSchema(illness, uam);
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
                                                    this.IllnessBusiness.InsertIllness_onHourlyRequest_onPersonnel_onGanttChartSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    this.IllnessBusiness.InsertIllness_onHourlyRequest_onManager_onGanttChartSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    this.IllnessBusiness.InsertIllness_onHourlyRequest_onOperator_onGanttChartSchema(illness, uam);
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
                                            illnessID = this.IllnessBusiness.InsertIllness_onRequestDaily_onRequestRegister(illness, uam);
                                            break;
                                        case UserCallerEnum.Operator:
                                            illnessID = this.IllnessBusiness.InsertIllness_onRequestDaily_onRequestRegisterByOperator(illness, uam);
                                            break;
                                        case UserCallerEnum.OperatorPermit:
                                            illnessID = this.IllnessBusiness.InsertIllness_onRequestDaily_onRequestRegisterByOperatorPermit(illness, uam);
                                            break;
                                    }
                                    break;
                                case PageCaller.DailyRequestOnAbsence:
                                    switch (RC)
                                    {
                                        case RequestCaller.Grid:
                                            switch (RLS)
                                            {
                                                case RequestLoadState.Personnel:
                                                    this.IllnessBusiness.InsertIllness_onDailyRequest_onPersonnel_onGridSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    this.IllnessBusiness.InsertIllness_onDailyRequest_onManager_onGridSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    this.IllnessBusiness.InsertIllness_onDailyRequest_onOperator_onGridSchema(illness, uam);
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
                                                    this.IllnessBusiness.InsertIllness_onDailyRequest_onPersonnel_onGanttChartSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Manager:
                                                    this.IllnessBusiness.InsertIllness_onDailyRequest_onManager_onGanttChartSchema(illness, uam);
                                                    break;
                                                case RequestLoadState.Operator:
                                                    this.IllnessBusiness.InsertIllness_onDailyRequest_onOperator_onGanttChartSchema(illness, uam);
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
                                case PageCaller.HourlyRequestOnAbsence:
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
                    //if (selectedPhysicianID == 0)
                    //{
                    //    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPhysicianSelectedforEdit").ToString()), retMessage);
                    //    return retMessage;
                    //}
                    //PhysicianID = this.PhysiciansBusiness.Updatephysician(physician, uam);
                    break;
                case UIActionType.DELETE:
                    //if (selectedPhysicianID == 0)
                    //{
                    //    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPhysicianSelectedforDelete").ToString()), retMessage);
                    //    return retMessage;
                    //}
                    //PhysicianID = this.PhysiciansBusiness.Deletephysician(physician, uam);
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
            retMessage[3] = illnessID.ToString();
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