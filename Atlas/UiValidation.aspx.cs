using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business;
using GTS.Clock.Business.Shifts;
using System.IO;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UIValidation;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;

public partial class UiValidation : GTSBasePage
{
    public BUIValidationGroup UiValidationBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BUIValidationGroup>();
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
    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    enum Scripts
    {
        UiValidation_onPageLoad,
        tbUiValidation_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridUiValidation_UiValidation.CausedCallback)
        {
            Page UiValidation = this;
            Ajax.Utility.GenerateMethodScripts(UiValidation);

            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckUIValidationLoadAccess_UiValidation();
        }
    }

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripRuleParametersTerms);
    }


    private void CheckUIValidationLoadAccess_UiValidation()
    {
        string[] retMessage = new string[4];
        try
        {
            this.UiValidationBusiness.CheckUIValidationLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
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

    protected void CallBack_GridUiValidation_UiValidation_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.Fill_GridUiValidation_UiValidation();
        this.GridUiValidation_UiValidation.RenderControl(e.Output);
        this.ErrorHiddenField_UiValidation.RenderControl(e.Output);
    }
    private void Fill_GridUiValidation_UiValidation()
    {
        string[] retMessage = new string[4];
        try
        {

            this.InitializeCulture();
            IList<UIValidationGroup> uiValidationList = this.UiValidationBusiness.GetAll();
            this.GridUiValidation_UiValidation.DataSource = uiValidationList;
            this.GridUiValidation_UiValidation.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_UiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_UiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_UiValidation.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    [Ajax.AjaxMethod("UpdateUiValidation_UiValidationPage", "UpdateUiValidation_UiValidationPage_onCallBack", null, null)]
    public string[] UpdateUiValidation_UiValidationPage(string state, string SelectedUiValidationID, string UiValidationCode, string UiValidationName, string strRulesList)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal uiValidationID = 0;
            decimal selectedUiValidationID = decimal.Parse(this.StringBuilder.CreateString(SelectedUiValidationID), CultureInfo.InvariantCulture);
            UiValidationCode = this.StringBuilder.CreateString(UiValidationCode);
            UiValidationName = this.StringBuilder.CreateString(UiValidationName);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            strRulesList = this.StringBuilder.CreateString(strRulesList);
            UIValidationGroup uiValidation = new UIValidationGroup();
            uiValidation.ID = selectedUiValidationID;
            if (uam != UIActionType.DELETE)
            {
                uiValidation.CustomCode = UiValidationCode;
                uiValidation.Name = UiValidationName;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    uiValidationID = this.UiValidationBusiness.InsertUIValidationGroup(uiValidation, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedUiValidationID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoUiValidationSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }

                    IList<UiValidationRuleGroupProxy> UiValidtionRuleGroupProxyList = this.CreateUiValidationGroupingList(strRulesList);
                    // this.UiValidationBusiness.UpdateRulesList(UiValidtionRuleGroupProxyList);

                    uiValidationID = this.UiValidationBusiness.UpdateUIValidationGroup(uiValidation, UiValidtionRuleGroupProxyList, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedUiValidationID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoUiValidationSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    uiValidationID = this.UiValidationBusiness.DeleteUIValidationGroup(uiValidation, uam);
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
            retMessage[3] = uiValidationID.ToString();
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
    private IList<UiValidationRuleGroupProxy> CreateUiValidationGroupingList(string StrListRule)
    {
        IList<UiValidationRuleGroupProxy> UIValidationRuleGroupProxyList = new List<UiValidationRuleGroupProxy>();
        object[] ParamsBatchList = (object[])JsSerializer.DeserializeObject(StrListRule);
        foreach (object paramBatch in ParamsBatchList)
        {
            Dictionary<string, object> paramBatchDic = (Dictionary<string, object>)paramBatch;
            UiValidationRuleGroupProxy UIVRG = new UiValidationRuleGroupProxy();
            UIVRG.ID = decimal.Parse(paramBatchDic["uvID"].ToString(), CultureInfo.InvariantCulture);
            UIVRG.Active = bool.Parse(paramBatchDic["IsA"].ToString());
            UIVRG.RuleID = decimal.Parse(paramBatchDic["RID"].ToString(), CultureInfo.InvariantCulture);
            UIVRG.GroupID = decimal.Parse(paramBatchDic["GID"].ToString(), CultureInfo.InvariantCulture);
            UIVRG.Warning = bool.Parse(paramBatchDic["IsW"].ToString());
            UIVRG.ExistRuleTag = bool.Parse(paramBatchDic["ExistRuleTag"].ToString());
        UIValidationRuleGroupProxyList.Add(UIVRG);
        }
        return UIValidationRuleGroupProxyList;

    }
    protected void CallBack_GridUiValidationRules_UiValidationIntroduction_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        decimal selectedGroupID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
        int selectedRuleGroupID = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
        this.Fill_GridUiValidationRules_UiValidationIntroduction(selectedGroupID, selectedRuleGroupID);
        this.GridUiValidationRules_UiValidationIntroduction.RenderControl(e.Output);
        this.ErrorHiddenField_GridUiValidationRules_UiValidationIntroduction.RenderControl(e.Output);
    }
    private void Fill_GridUiValidationRules_UiValidationIntroduction(decimal groupId, int ruleGroupId)
    {
        string[] retMessage = new string[5];
        try
        {
            IList<UiValidationRuleGroupProxy> UiValidationRuleGroupProxyList;
            if (groupId != 0)
            {
                if (ruleGroupId == 0)
                    UiValidationRuleGroupProxyList = UiValidationBusiness.GetGroupRules(groupId);
                else
                    UiValidationRuleGroupProxyList = UiValidationBusiness.GetGroupRules(groupId).Where(x => x.RuleGroupType == ruleGroupId).ToList();
                this.GridUiValidationRules_UiValidationIntroduction.DataSource = UiValidationRuleGroupProxyList;
                this.GridUiValidationRules_UiValidationIntroduction.DataBind();
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GridUiValidationRules_UiValidationIntroduction.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GridUiValidationRules_UiValidationIntroduction.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GridUiValidationRules_UiValidationIntroduction.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    protected void CallBack_cmbRuleGroup_UiValidationIntroduction_onCallback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        this.cmbRuleGroup_UiValidationIntroduction.Dispose();
        this.Fill_cmbRuleGroup_UiValidationIntroduction();
        this.ErrorHiddenField_RuleGroup.RenderControl(e.Output);
        this.cmbRuleGroup_UiValidationIntroduction.RenderControl(e.Output);

    }
    private void Fill_cmbRuleGroup_UiValidationIntroduction()
    {
        this.cmbRuleGroup_UiValidationIntroduction.Enabled = true;
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            ComponentArt.Web.UI.ComboBoxItem cbItemAll = new ComboBoxItem();
            cbItemAll.Text = GetLocalResourceObject("AllRuleGroup").ToString();
            cbItemAll.Value = "0";
            cmbRuleGroup_UiValidationIntroduction.Items.Add(cbItemAll);
            foreach (UIValidationRuleGroupType ruleType in Enum.GetValues(typeof(UIValidationRuleGroupType)))
            {
                ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Text = GetLocalResourceObject(ruleType.ToString()).ToString();
                cbItem.Value = ((int)ruleType).ToString();
                cmbRuleGroup_UiValidationIntroduction.Items.Add(cbItem);
            }
            cmbRuleGroup_UiValidationIntroduction.Enabled = true;

        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RuleGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RuleGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RuleGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

}