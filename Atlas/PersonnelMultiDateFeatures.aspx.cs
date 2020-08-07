using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.Assignments;
using System.Collections;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Model.Contracts;
using GTS.Clock.Infrastructure.Exceptions;

public partial class PersonnelMultiDateFeatures : GTSBasePage
{
    public BAssignRule PersonnelRuleBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            return new BAssignRule(Slr);
        }
    }

    public BAssignRule bAssignRule
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BAssignRule>();
        }
    }
    public BPersonContractAssignment PersonnelContractAssignmentBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            return new BPersonContractAssignment(Slr);
        }
    }
    public BPersonContractAssignment bPersonContractAssignment
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BPersonContractAssignment>();
        }
    }

    public BPerson PersonBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            LocalLanguageResource Llr = LocalLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentLanguage())
            {
                case "fa-IR":
                    Llr = LocalLanguageResource.Parsi;
                    break;
                case "en-US":
                    Llr = LocalLanguageResource.English;
                    break;
            }
            return new BPerson(Slr, Llr);
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

    enum Caller
    {
        RuleGroups,
        Contracts
    }

    enum Scripts
    {
        PersonnelMultiDateFeatures_onPageLoad,
        DialogPersonnelMultiDateFeatures_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.IsCallback && !CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures.IsCallback)
        {
            Page PersonnelMultiDateFeaturesPage = this;
            Ajax.Utility.GenerateMethodScripts(PersonnelMultiDateFeaturesPage);

            this.CustomizePersonnelMultiDateFeature_PersonnelMultiDateFeatures();
            this.ViewCurrentLangCalendars_PersonnelMultiDateFeatures();
            this.SetCurrentDate_PersonnelMultiDateFeatures();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckLoadAccess_PersonnelMultiDateFeatures();
        }
    }
    private void CheckLoadAccess_PersonnelMultiDateFeatures()
    {
        string[] retMessage = new string[4];
        try
        {            
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
            {
                Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));

                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("PersonnelState"))
                {
                    UIActionType PersonnelState = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["PersonnelState"]).ToUpper());
                    switch (PersonnelState)
                    {
                        case UIActionType.ADD:
                            switch (caller)
                            {
                                case Caller.Contracts:
                                    this.bPersonContractAssignment.CheckContractsLoadAccess_OnPersonnelInsert();
                                    break;
                                case Caller.RuleGroups:
                                    this.bAssignRule.CheckAssignRuleLoadAccess_OnPersonnelInsert();
                                    break;
                            }
                            break;
                        case UIActionType.EDIT:
                            switch (caller)
                            {
                                case Caller.Contracts:
                                    this.bPersonContractAssignment.CheckContractsLoadAccess_OnPersonnelUpdate();
                                    break;
                                case Caller.RuleGroups:
                                    this.bAssignRule.CheckAssignRuleLoadAccess_OnPersonnelUpdate();
                                    break;
                            }
                            break;
                    }
                }
            }
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void CustomizePersonnelMultiDateFeature_PersonnelMultiDateFeatures()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
        {
            Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));
            this.CustomizeGeneralComponents_PersonnelMultiDateFeatures(caller);
            this.CustomizeCmbMultiDateFeatures_PersonnelMultiDateFeatures(caller);
            this.CustomizeGridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures(caller);
        }
    }

    private void CustomizeGeneralComponents_PersonnelMultiDateFeatures(Caller caller)
    {
        string PersonnelMultiDateFeaturesName = string.Empty;
        string GridHeader = string.Empty;
        string DialogTitle = string.Empty;
        string DeleteMessage = string.Empty;
        string CloseMessage = string.Empty;
        switch (caller)
        {
            case Caller.RuleGroups:
                PersonnelMultiDateFeaturesName = this.GetLocalResourceObject("lblRuleGroupName_PersonnelMultiDateFeatures").ToString();
                GridHeader = this.GetLocalResourceObject("hfRuleGroupsheader_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures").ToString();
                DialogTitle = this.GetLocalResourceObject("hfRuleGroupsTitle_DialogPersonnelMultiDateFeatures").ToString();
                DeleteMessage = this.GetLocalResourceObject("hfRuleGroupDeleteMessage_PersonnelMultiDateFeatures").ToString();
                CloseMessage = this.GetLocalResourceObject("hfRuleGroupCloseMessage_PersonnelMultiDateFeatures").ToString();
                this.Container_TlbClear_ToDateCalendars_PersonnelMultiDateFeatures.Visible = false;
                break;
            case Caller.Contracts:
                PersonnelMultiDateFeaturesName = this.GetLocalResourceObject("lblContractTitle_PersonnelMultiDateFeatures").ToString();
                GridHeader = this.GetLocalResourceObject("hfContractsheader_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures").ToString();
                DialogTitle = this.GetLocalResourceObject("hfContractsTitle_DialogPersonnelMultiDateFeatures").ToString();
                DeleteMessage = this.GetLocalResourceObject("hfContractDeleteMessage_PersonnelMultiDateFeatures").ToString();
                CloseMessage = this.GetLocalResourceObject("hfContractCloseMessage_PersonnelMultiDateFeatures").ToString();
                this.Container_TlbClear_ToDateCalendars_PersonnelMultiDateFeatures.Visible = true;
                break;
        }
        this.lblPersonnelMultiDateFeatureName_PersonnelMultiDateFeatures.Text = PersonnelMultiDateFeaturesName;
        this.hfheader_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures.Value = GridHeader;
        this.hfTitle_DialogPersonnelMultiDateFeatures.Value = DialogTitle;
        this.hfDeleteMessage_PersonnelMultiDateFeatures.Value = DeleteMessage;
        this.hfCloseMessage_PersonnelMultiDateFeatures.Value = CloseMessage;
    }

    private void CustomizeGridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures(Caller caller)
    {
        string clmnPersonnelMultiDateFeaturesIDDataField = string.Empty;
        string clmnPersonnelMultiDateFeaturesNameDataField = string.Empty;
        string clmnPersonnelMultiDateFeaturesNameHeadingsText = string.Empty;
        switch (caller)
        {
            case Caller.RuleGroups:
                clmnPersonnelMultiDateFeaturesIDDataField = "RuleCategory.ID";
                clmnPersonnelMultiDateFeaturesNameDataField = "RuleCategory.Name";
                clmnPersonnelMultiDateFeaturesNameHeadingsText = this.GetLocalResourceObject("clmnRuleGroupName_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures").ToString();
                break;
            case Caller.Contracts:
                clmnPersonnelMultiDateFeaturesIDDataField = "Contract.ID";
                clmnPersonnelMultiDateFeaturesNameDataField = "Contract.Title";
                clmnPersonnelMultiDateFeaturesNameHeadingsText = this.GetLocalResourceObject("clmnContractTitle_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures").ToString();
                break;
        }
        this.GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.Levels[0].Columns[1].DataField = clmnPersonnelMultiDateFeaturesIDDataField;
        this.GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.Levels[0].Columns[2].DataField = clmnPersonnelMultiDateFeaturesNameDataField;
        this.GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.Levels[0].Columns[2].HeadingText = clmnPersonnelMultiDateFeaturesNameHeadingsText;
    }

    private void CustomizeCmbMultiDateFeatures_PersonnelMultiDateFeatures(Caller caller)
    {
        switch (caller)
        {
            case Caller.RuleGroups:
                this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.DataTextField = "Name";
                break;
            case Caller.Contracts:
                this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.DataTextField = "Title";
                break;
        }
    }


    private void ViewCurrentLangCalendars_PersonnelMultiDateFeatures()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpFromDate_PersonnelMultiDateFeatures.Visible = true;
                this.Container_pdpToDate_PersonnelMultiDateFeatures.Visible = true;
                break;
            case "en-US":
                this.Container_gdpFromDate_PersonnelMultiDateFeatures.Visible = true;
                this.Container_gdpToDate_PersonnelMultiDateFeatures.Visible = true;
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


    private void SetCurrentDate_PersonnelMultiDateFeatures()
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
        this.hfCurrentDate_PersonnelMultiDateFeatures.Value = strCurrentDate;
    }

    protected void CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(e.Parameters[0]));
        this.CustomizeGridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures(caller);
        this.Fill_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures(caller, decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture));
        this.GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.RenderControl(e.Output);
        this.ErrorHiddenField_PersonnelMultiDateFeatures.RenderControl(e.Output);
    }

    private void Fill_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures(Caller caller, decimal PersonID)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            switch (caller)
            {
                case Caller.RuleGroups:
                    IList<PersonRuleCatAssignment> PersonRuleCatAssignmentList = this.PersonnelRuleBusiness.GetAll(PersonID);
                    this.GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.DataSource = PersonRuleCatAssignmentList;
                    break;
                case Caller.Contracts:
                    IList<PersonContractAssignment> PersonContractAssignmentList = this.PersonnelContractAssignmentBusiness.GetAll(PersonID);
                    this.GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.DataSource = PersonContractAssignmentList;
                    break;
            }
            this.GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PersonnelMultiDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PersonnelMultiDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PersonnelMultiDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        Caller caller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(e.Parameter));
        this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.Dispose();
        this.CustomizeCmbMultiDateFeatures_PersonnelMultiDateFeatures(caller);
        this.Fill_cmbMultiDateFeatures_PersonnelMultiDateFeatures(caller);
        this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.Enabled = true;
        this.ErrorHiddenField_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures.RenderControl(e.Output);
        this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.RenderControl(e.Output);
    }

    private void Fill_cmbMultiDateFeatures_PersonnelMultiDateFeatures(Caller caller)
    {
        string[] retMessage = new string[4];

        this.InitializeCulture();
        try
        {
            switch (caller)
            {
                case Caller.RuleGroups:
                    IList<RuleCategory> RulesGroupsList = this.PersonnelRuleBusiness.GetAllRuleCategories();
                    this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.DataSource = RulesGroupsList;
                    break;
                case Caller.Contracts:
                    IList<Contract> ContractsList = this.PersonnelContractAssignmentBusiness.GetAllContracts();
                    this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.DataSource = ContractsList;
                    break;
            }
            this.cmbMultiDateFeatures_PersonnelMultiDateFeatures.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeaturesPage", "UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeaturesPage_onCallBack", null, null)]
    public string[] UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeaturesPage(string caller, string state, string SelectedPersonnelMultiDateFeatureID, string PersonnelID, string MultiDateFeatureID, string MultiDateFeatureName, string FromDate, string ToDate, string PersonnelState)
    {
        this.InitializeCulture();

        string[] retMessage = new string[6];

        Caller PersonnelMultiDateFeatureCaller = (Caller)Enum.Parse(typeof(Caller), this.StringBuilder.CreateString(caller));
        decimal PersonnelMultiDateFeatureID = 0;
        decimal selectedPersonnelMultiDateFeatureID = decimal.Parse(this.StringBuilder.CreateString(SelectedPersonnelMultiDateFeatureID), CultureInfo.InvariantCulture);
        decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
        decimal multiDateFeatureID = decimal.Parse(this.StringBuilder.CreateString(MultiDateFeatureID), CultureInfo.InvariantCulture);
        UIActionType personnelState = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(PersonnelState).ToUpper());       
        MultiDateFeatureName = this.StringBuilder.CreateString(MultiDateFeatureName);
        FromDate = this.StringBuilder.CreateString(FromDate);
        ToDate = this.StringBuilder.CreateString(ToDate);
        string CurrentActiveMultiDateFeatureName = string.Empty;

        string NoItemSelectedforEditMessage = string.Empty;
        string NoItemSelectedforDeleteMessage = string.Empty;
        switch (PersonnelMultiDateFeatureCaller)
        {
            case Caller.RuleGroups:
                NoItemSelectedforEditMessage = this.GetLocalResourceObject("NoRuleGroupPersonnelMultiDateFeatureselectedforEdit").ToString();
                NoItemSelectedforDeleteMessage = this.GetLocalResourceObject("NoRuleGroupPersonnelMultiDateFeatureselectedforDelete").ToString();
                break;
            case Caller.Contracts:
                NoItemSelectedforEditMessage = this.GetLocalResourceObject("NoContractPersonnelMultiDateFeatureselectedforEdit").ToString();
                NoItemSelectedforDeleteMessage = this.GetLocalResourceObject("NoContractPersonnelMultiDateFeatureselectedforDelete").ToString();
                break;
        }

        UIActionType uam = UIActionType.ADD;
        try
        {
            AttackDefender.CSRFDefender(this.Page);
            switch (this.StringBuilder.CreateString(state))
            {
                case "Add":
                    uam = UIActionType.ADD;
                    break;
                case "Edit":
                    uam = UIActionType.EDIT;
                    if (selectedPersonnelMultiDateFeatureID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(NoItemSelectedforEditMessage), retMessage);
                        return retMessage;
                    }
                    break;
                case "Delete":
                    uam = UIActionType.DELETE;
                    if (selectedPersonnelMultiDateFeatureID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(NoItemSelectedforDeleteMessage), retMessage);
                        return retMessage;
                    }
                    break;
                default:
                    break;
            }

            switch (PersonnelMultiDateFeatureCaller)
            {
                case Caller.RuleGroups:
                    PersonRuleCatAssignment personnelRuleGroup = new PersonRuleCatAssignment();
                    personnelRuleGroup.ID = selectedPersonnelMultiDateFeatureID;
                    personnelRuleGroup.Person = new Person() { ID = personnelID };
                    if (uam != UIActionType.DELETE)
                    {
                        Person person = new Person();
                        person.ID = personnelID;
                        personnelRuleGroup.Person = person;                       
                        RuleCategory ruleGroup = new RuleCategory();
                        ruleGroup.ID = multiDateFeatureID;
                        ruleGroup.Name = MultiDateFeatureName;
                        personnelRuleGroup.RuleCategory = ruleGroup;
                        personnelRuleGroup.UIFromDate = FromDate;
                        personnelRuleGroup.UIToDate = ToDate;
                    }
                    switch (personnelState)
                    {
                        case UIActionType.ADD:
                            switch (uam)
                            {
                                case UIActionType.ADD:
                                    PersonnelMultiDateFeatureID = this.bAssignRule.InsertAssignRule_OnPersonnelInsert(personnelRuleGroup, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveRuleGroup(personnelID);
                                    break;
                                case UIActionType.EDIT:
                                    PersonnelMultiDateFeatureID = this.bAssignRule.UpdateAssignRule_OnPersonnelInsert(personnelRuleGroup, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveRuleGroup(personnelID);
                                    break;
                                case UIActionType.DELETE:
                                    PersonnelMultiDateFeatureID = this.bAssignRule.DeleteAssignRule_OnPersonnelInsert(personnelRuleGroup, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveRuleGroup(personnelID);
                                    break;
                            }
                            break;
                        case UIActionType.EDIT:
                            switch (uam)
                            {
                                case UIActionType.ADD:
                                    PersonnelMultiDateFeatureID = this.bAssignRule.InsertAssignRule_OnPersonnelUpdate(personnelRuleGroup, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveRuleGroup(personnelID);
                                    break;
                                case UIActionType.EDIT:
                                    PersonnelMultiDateFeatureID = this.bAssignRule.UpdateAssignRule_OnPersonnelUpdate(personnelRuleGroup, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveRuleGroup(personnelID);
                                    break;
                                case UIActionType.DELETE:
                                    PersonnelMultiDateFeatureID = this.bAssignRule.DeleteAssignRule_OnPersonnelUpdate(personnelRuleGroup, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveRuleGroup(personnelID);
                                    break;
                            }
                            break;
                    }                    
                    break;
                case Caller.Contracts:
                    PersonContractAssignment personContractAssignment = new PersonContractAssignment();
                    personContractAssignment.ID = selectedPersonnelMultiDateFeatureID;
                    personContractAssignment.Person = new Person() { ID = personnelID };
                    if (uam != UIActionType.DELETE)
                    {
                        Contract contract = new Contract();
                        contract.ID = multiDateFeatureID;
                        contract.Title = MultiDateFeatureName;
                        personContractAssignment.Contract = contract;
                        personContractAssignment.UIFromDate = FromDate;
                        personContractAssignment.UIToDate = ToDate;
                    }
                    switch (personnelState)
                    {
                        case UIActionType.ADD:
                            switch (uam)
                            {
                                case UIActionType.ADD:
                                    PersonnelMultiDateFeatureID = this.bPersonContractAssignment.InsertContract_OnPersonnelInsert(personContractAssignment, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveContract(personnelID);
                                    break;
                                case UIActionType.EDIT:
                                    PersonnelMultiDateFeatureID = this.bPersonContractAssignment.UpdateContract_OnPersonnelInsert(personContractAssignment, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveContract(personnelID);
                                    break;
                                case UIActionType.DELETE:
                                    PersonnelMultiDateFeatureID = this.bPersonContractAssignment.DeleteContract_OnPersonnelInsert(personContractAssignment, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveContract(personnelID);
                                    break;
                            }
                            break;
                        case UIActionType.EDIT:
                            switch (uam)
                            {
                                case UIActionType.ADD:
                                    PersonnelMultiDateFeatureID = this.bPersonContractAssignment.InsertContract_OnPersonnelUpdate(personContractAssignment, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveContract(personnelID);
                                    break;
                                case UIActionType.EDIT:
                                    PersonnelMultiDateFeatureID = this.bPersonContractAssignment.UpdateContract_OnPersonnelUpdate(personContractAssignment, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveContract(personnelID);
                                    break;
                                case UIActionType.DELETE:
                                    PersonnelMultiDateFeatureID = this.bPersonContractAssignment.DeleteContract_OnPersonnelUpdate(personContractAssignment, uam);
                                    CurrentActiveMultiDateFeatureName = this.PersonBusiness.GetCurrentActiveContract(personnelID);
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
            retMessage[3] = PersonnelMultiDateFeatureID.ToString();
            retMessage[4] = CurrentActiveMultiDateFeatureName;
            retMessage[5] = FromDate;
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