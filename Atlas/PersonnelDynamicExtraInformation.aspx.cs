using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Model;
using System.Threading;
using System.Globalization;
using GTS.Clock.Model.Rules;
using GTS.Clock.Business.Rules;

public partial class PersonnelDynamicExtraInformation : GTSBasePage
{
    public BPersonParamFields DynamicParametersBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BPersonParamFields>();
        }
    }

    public BPersonParams DynamicParameterPairsBusiness
    {

        get
        {
            return BusinessHelper.GetBusinessInstance<BPersonParams>();
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

    enum Scripts
    {
        PersonnelDynamicExtraInformation_onPageLoad,
        DialogPersonnelDynamicExtraInformation_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation.IsCallback && !this.CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation.IsCallback)
        {
            Page PersonnelDynamicExtraInformationPage = this;
            Ajax.Utility.GenerateMethodScripts(PersonnelDynamicExtraInformationPage);
            this.ViewCurrentLangCalendars_PersonnelDynamicExtraInformation();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckPersonnelDynamicExtraInformationLoadAccess_PersonnelDynamicExtraInformation();
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

    private void ViewCurrentLangCalendars_PersonnelDynamicExtraInformation()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "en-US":
                this.Container_gdpFromDate_PersonnelDynamicExtraInformation.Visible = true;
                this.Container_gdpToDate_PersonnelDynamicExtraInformation.Visible = true;
                break;
            case "fa-IR":
                this.Container_pdpFromDate_PersonnelDynamicExtraInformation.Visible = true;
                this.Container_pdpToDate_PersonnelDynamicExtraInformation.Visible = true;
                break;
        }
    }

    private void CheckPersonnelDynamicExtraInformationLoadAccess_PersonnelDynamicExtraInformation()
    {
        string[] retMessage = new string[4];
        try
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PersonnelState"))
            {
                UIActionType UAT = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PersonnelState"]).ToUpper());
                switch (UAT)
                {
                    case UIActionType.ADD:
                        this.DynamicParametersBusiness.CheckPersonParamFieldsLoadAccess_onPersonnelInsert();
                        break;
                    case UIActionType.EDIT:
                        this.DynamicParametersBusiness.CheckPersonParamFieldsLoadAccess_onPersonnelUpdate();
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

    protected void CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridDynamicParameters_PersonnelDynamicExtraInformation();
        this.ErrorHiddenField_DynamicParameters_PersonnelDynamicExtraInformation.RenderControl(e.Output);
        this.GridDynamicParameters_PersonnelDynamicExtraInformation.RenderControl(e.Output);
    }

    private void Fill_GridDynamicParameters_PersonnelDynamicExtraInformation()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<PersonParamField> DynamicParametersList = this.DynamicParametersBusiness.GetAll();
            this.GridDynamicParameters_PersonnelDynamicExtraInformation.DataSource = DynamicParametersList;
            this.GridDynamicParameters_PersonnelDynamicExtraInformation.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DynamicParameters_PersonnelDynamicExtraInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DynamicParameters_PersonnelDynamicExtraInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DynamicParameters_PersonnelDynamicExtraInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridDynamicParameterPairs_PersonnelDynamicExtraInformation(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture));
        this.ErrorHiddenField_DynamicParameterPairs_PersonnelDynamicExtraInformation.RenderControl(e.Output);
        this.GridDynamicParameterPairs_PersonnelDynamicExtraInformation.RenderControl(e.Output);
    }

    private void Fill_GridDynamicParameterPairs_PersonnelDynamicExtraInformation(decimal DynamicParameterID, decimal personnelID)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<PersonParamValue> DynamicParameterPairsList = this.DynamicParameterPairsBusiness.GetAll(personnelID, DynamicParameterID);
            this.GridDynamicParameterPairs_PersonnelDynamicExtraInformation.DataSource = DynamicParameterPairsList;
            this.GridDynamicParameterPairs_PersonnelDynamicExtraInformation.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DynamicParameterPairs_PersonnelDynamicExtraInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DynamicParameterPairs_PersonnelDynamicExtraInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DynamicParameterPairs_PersonnelDynamicExtraInformation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateDynamicParameter_PersonnelDynamicExtraInformationPage", "UpdateDynamicParameter_PersonnelDynamicExtraInformationPage_onCallBack", null, null)]
    public string[] UpdateDynamicParameter_PersonnelDynamicExtraInformationPage(string state, string PersonnelState, string SelectedDynamicParameterID, string DynamicParameterCustomCode, string DynamicParameterTitle)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal DynamicParameterID = 0;
            decimal selectedDynamicParameterID = decimal.Parse(this.StringBuilder.CreateString(SelectedDynamicParameterID), CultureInfo.InvariantCulture);
            DynamicParameterCustomCode = this.StringBuilder.CreateString(DynamicParameterCustomCode);
            DynamicParameterTitle = this.StringBuilder.CreateString(DynamicParameterTitle);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            UIActionType pls = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(PersonnelState).ToUpper());

            PersonParamField DynamicParameter = new PersonParamField();
            DynamicParameter.ID = selectedDynamicParameterID;
            if (uam != UIActionType.DELETE)
            {
                DynamicParameter.Active = true;
                DynamicParameter.Key = DynamicParameterCustomCode;
                DynamicParameter.Title = DynamicParameterTitle;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    switch (pls)
                    {
                        case UIActionType.ADD:
                            DynamicParameterID = this.DynamicParametersBusiness.InsertPersonParamField_onPersonnelInsert(DynamicParameter, uam);
                            break;
                        case UIActionType.EDIT:
                            DynamicParameterID = this.DynamicParametersBusiness.InsertPersonParamField_onPersonnelUpdate(DynamicParameter, uam);
                            break;
                    }
                    break;
                case UIActionType.EDIT:
                    if (selectedDynamicParameterID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoDynamicParameterSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    switch (pls)
                    {
                        case UIActionType.ADD:
                            DynamicParameterID = this.DynamicParametersBusiness.UpdatePersonParamField_onPersonnelInsert(DynamicParameter, uam);
                            break;
                        case UIActionType.EDIT:
                            DynamicParameterID = this.DynamicParametersBusiness.UpdatePersonParamField_onPersonnelUpdate(DynamicParameter, uam);
                            break;
                    }
                    break;
                case UIActionType.DELETE:
                    if (selectedDynamicParameterID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoDynamicParameterSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    switch (pls)
                    {
                        case UIActionType.ADD:
                            this.DynamicParametersBusiness.DeletePersonParamField_onPersonnelInsert(DynamicParameter, uam);
                            break;
                        case UIActionType.EDIT:
                            this.DynamicParametersBusiness.DeletePersonParamField_onPersonnelUpdate(DynamicParameter, uam);
                            break;
                    }
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
            retMessage[3] = DynamicParameterID.ToString();
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

    [Ajax.AjaxMethod("UpdateDynamicParameterPair_PersonnelDynamicExtraInformationPage", "UpdateDynamicParameterPair_PersonnelDynamicExtraInformationPage_onCallBack", null, null)]
    public string[] UpdateDynamicParameterPair_PersonnelDynamicExtraInformationPage(string state, string PersonnelState, string PersonnelID, string SelectedDynamicParameterID, string SelectedDynamicParameterPairID, string DynamicParameterValue, string FromDate, string ToDate)    
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal DynamicParameterPairID = 0;
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
            decimal selectedDynamicParameterID = decimal.Parse(this.StringBuilder.CreateString(SelectedDynamicParameterID), CultureInfo.InvariantCulture);
            decimal selectedDynamicParameterPairID = decimal.Parse(this.StringBuilder.CreateString(SelectedDynamicParameterPairID), CultureInfo.InvariantCulture);
            DynamicParameterValue = this.StringBuilder.CreateString(DynamicParameterValue);
            FromDate = this.StringBuilder.CreateString(FromDate);
            ToDate = this.StringBuilder.CreateString(ToDate);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            UIActionType pls = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(PersonnelState).ToUpper());
            
            PersonParamValue DynamicParameterPair = new PersonParamValue();
            Person person = new Person();
            person.ID = personnelID;
            DynamicParameterPair.Person = person;
            DynamicParameterPair.ID = selectedDynamicParameterPairID;
            if (uam != UIActionType.DELETE)
            {
                
                PersonParamField DynamicParameter = new PersonParamField();
                DynamicParameter.ID = selectedDynamicParameterID;
                DynamicParameterPair.ParamField = DynamicParameter;
                DynamicParameterPair.Value = DynamicParameterValue;
                DynamicParameterPair.TheFromDate = FromDate;
                DynamicParameterPair.TheToDate = ToDate;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    switch (pls)
                    {
                        case UIActionType.ADD:
                            DynamicParameterPairID = this.DynamicParameterPairsBusiness.InsertPersonParamValue_onPersonnelInsert(DynamicParameterPair, uam);
                            break;
                        case UIActionType.EDIT:
                            DynamicParameterPairID = this.DynamicParameterPairsBusiness.InsertPersonParamValue_onPersonnelUpdate(DynamicParameterPair, uam);
                            break;
                    }
                    break;
                case UIActionType.EDIT:
                    if (selectedDynamicParameterID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoDynamicParameterPairSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    switch (pls)
                    {
                        case UIActionType.ADD:
                            DynamicParameterPairID = this.DynamicParameterPairsBusiness.UpdatePersonParamValue_onPersonnelInsert(DynamicParameterPair, uam);
                            break;
                        case UIActionType.EDIT:
                            DynamicParameterPairID = this.DynamicParameterPairsBusiness.UpdatePersonParamValue_onPersonnelUpdate(DynamicParameterPair, uam);
                            break;
                    }
                    break;
                case UIActionType.DELETE:
                    if (selectedDynamicParameterID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoDynamicParameterPairSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    switch (pls)
                    {
                        case UIActionType.ADD:
                            this.DynamicParameterPairsBusiness.DeletePersonParamValue_onPersonnelInsert(DynamicParameterPair, uam);
                            break;
                        case UIActionType.EDIT:
                            this.DynamicParameterPairsBusiness.DeletePersonParamValue_onPersonnelUpdate(DynamicParameterPair, uam);
                            break;
                    }
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
            retMessage[3] = DynamicParameterPairID.ToString();
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