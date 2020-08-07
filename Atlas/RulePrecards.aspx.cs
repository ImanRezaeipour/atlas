using GTS.Clock.Business.AppSettings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.UI;
using GTS.Clock.Model.Concepts;
using ComponentArt.Web.UI;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.UIValidation;
using GTS.Clock.Infrastructure;
using System.Web.Script.Serialization;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.Proxy.UiValidation;

public partial class RulePrecards : GTSBasePage
{
    enum LoadState
    {
        Normal,
        Search
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
    public BPrecard PrecardBusiness
    {
        get
        {
            return new BPrecard();
        }
    }
    public BUIValidationGroup UiValidationBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BUIValidationGroup>();
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
        DialogRulePrecards_Operations,
        RulePrecards_onPageLoad,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations,
        DropDownDive,
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridRulePrecards_RulePrecards.IsCallback)
        {
            Page RulePrecardsPage = this;
            Ajax.Utility.GenerateMethodScripts(RulePrecardsPage);

            //this.CheckRulePrecardsLoadAccess_RulePrecards();
            this.InitializeSkin();
            this.ViewCurrentLangCalendars_RulePrecards();
            ScriptHelper.InitializeDefiantScripts(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.InitializeCulture();
            this.CheckUIValidationRulesLoadAccess();
        }
    }
    private void CheckUIValidationRulesLoadAccess()
    {
        string[] retMessage = new string[4];
        try
        {
            this.UiValidationBusiness.CheckUIValidationRulesLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void ViewCurrentLangCalendars_RulePrecards()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_RulePrecards.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_RulePrecards.Visible = true;
                break;
        }
    }
    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripRulePrecardsTerms);
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

    protected void CallBack_GridRulePrecards_RulePrecards_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {

        decimal GroupId = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
        decimal RuleId = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
        int RuleGroupType = int.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
        int RuleType = int.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
        string SearchValue = this.StringBuilder.CreateString(e.Parameters[4]);
        int CustomCode = int.Parse(this.StringBuilder.CreateString(e.Parameters[5]));
        bool RuleTagExist = bool.Parse(this.StringBuilder.CreateString(e.Parameters[6]));
        Fill_GridRulePrecards_RulePrecards(GroupId, RuleId, RuleGroupType, RuleType, SearchValue, CustomCode, RuleTagExist);
        this.GridRulePrecards_RulePrecards.RenderControl(e.Output);
        this.ErrorHiddenField_RulePrecards_RulePrecards.RenderControl(e.Output);
        this.RulePrecardParamObjHiddenField_RulePrecards.RenderControl(e.Output);
        this.RuleDetailObjHiddenField_RulePrecards.RenderControl(e.Output);
    }
    private void Fill_GridRulePrecards_RulePrecards(decimal groupId, decimal ruleId, int ruleGroupType, int ruleType, string searchValue, int customCode, bool ruleTagExist)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<UiValidationRulePrecardParamProxy> rulePrecardParamProxyList = new List<UiValidationRulePrecardParamProxy>();
            if (ruleType == (int)UIValidationRuleType.RuleParameter)
            {
                this.GridRulePrecards_RulePrecards.Levels.Remove(0);
                this.GridRulePrecards_RulePrecards.Width = 910;
                IList<UIValidationRuleParameterProxy> ruleParameterProxyList = this.UiValidationBusiness.GetRuleParameters(ruleId, groupId, null);
                IList<UIValidationRuleParameterProxy> ruleParameterList = ruleParameterProxyList.Where(x => x.ExistParam == (int)UIValidationExistance.Exist).ToList<UIValidationRuleParameterProxy>();
                if (ruleParameterList.Count > 0)
                {
                    UiValidationRulePrecardParamProxy RulePrecardParamProxy = this.GetRulePrcardParamValue(null, ruleParameterList);
                    rulePrecardParamProxyList.Add(RulePrecardParamProxy);
                }
                this.GridRulePrecards_RulePrecards.PageSize = ruleParameterProxyList.Count();
                this.GridRulePrecards_RulePrecards.DataSource = ruleParameterProxyList;
                this.GridRulePrecards_RulePrecards.DataBind();
            }
            else
            {
                if (ruleType == (int)UIValidationRuleType.RulePrecard || ruleType == (int)UIValidationRuleType.RulePrecardParameter)
                {
                    if (ruleType == (int)UIValidationRuleType.RulePrecard)
                        this.GridRulePrecards_RulePrecards.Levels.RemoveAt(1);
                    IList<UIValidationRulePrecardProxy> RulePrecardProxyList = this.UiValidationBusiness.GetRulePrecards(groupId, ruleId, ruleGroupType, searchValue, ruleType);

                    this.GridRulePrecards_RulePrecards.PageSize = RulePrecardProxyList.Count();
                    foreach (UIValidationRulePrecardProxy rulePrecard in RulePrecardProxyList)
                    {
                        GridItem gridItemPrecard = new GridItem(this.GridRulePrecards_RulePrecards, 0, rulePrecard);
                        IList<UIValidationRuleParameterProxy> ruleParameterProxyList = this.UiValidationBusiness.GetRuleParameters(ruleId, groupId, rulePrecard);
                        if (rulePrecard.ExistPrecard == 1)
                        {
                            UiValidationRulePrecardParamProxy RulePrecardParamProxy = this.GetRulePrcardParamValue(rulePrecard, ruleParameterProxyList);
                            rulePrecardParamProxyList.Add(RulePrecardParamProxy);
                        }
                        foreach (UIValidationRuleParameterProxy rulePrameterProxy in ruleParameterProxyList)
                        {
                            GridItem gridItemParameter = new GridItem(this.GridRulePrecards_RulePrecards, 1, rulePrameterProxy);
                            gridItemPrecard.Items.Add(gridItemParameter);
                        }
                        this.GridRulePrecards_RulePrecards.Items.Add(gridItemPrecard);
                        this.GridRulePrecards_RulePrecards.ExpandAll();
                    }
                }
            }
            if (ruleType == (int)UIValidationRuleType.Rule)
            {
                IList<UIValidationRulePrecardProxy> RulePrecardProxyList = this.UiValidationBusiness.GetRulePrecards(groupId, ruleId, ruleGroupType, string.Empty, ruleType);
                foreach (UIValidationRulePrecardProxy rulePrecard in RulePrecardProxyList)
                {
                    if (rulePrecard.ExistPrecard == 1)
                    {
                        UiValidationRulePrecardParamProxy RulePrecardParamProxy = this.GetRulePrcardParamValue(rulePrecard, null);
                        rulePrecardParamProxyList.Add(RulePrecardParamProxy);
                    }
                }
            }
            this.RulePrecardParamObjHiddenField_RulePrecards.Value = this.JsSerializer.Serialize(rulePrecardParamProxyList);
            if (ruleTagExist)
            {
                UIValidationRuleGroup ruleGroup = this.UiValidationBusiness.GetRuleGroup(ruleId, groupId);
                this.RuleDetailObjHiddenField_RulePrecards.Value = ruleGroup.Tag;
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RulePrecards_RulePrecards.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RulePrecards_RulePrecards.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RulePrecards_RulePrecards.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    public UiValidationRulePrecardParamProxy GetRulePrcardParamValue(UIValidationRulePrecardProxy rulePrecard, IList<UIValidationRuleParameterProxy> RuleParametersList)
    {
        UiValidationRulePrecardParamProxy rulePrecardParam = new UiValidationRulePrecardParamProxy();
        
        if (rulePrecard != null)
        {
            rulePrecardParam.ID = rulePrecard.ID;
            rulePrecardParam.PrecardID = rulePrecard.PrecardID;
            rulePrecardParam.Active = rulePrecard.Active;
            rulePrecardParam.Operator = rulePrecard.Operator;
            rulePrecardParam.ExistPrecard = rulePrecard.ExistPrecard;            
        }
        else
        {
            if (rulePrecard == null && RuleParametersList.Count != 0)
            {
                UIValidationRuleGroupPrecard ruleGroupPrecard = new UIValidationRuleGroupPrecard();
                ruleGroupPrecard = RuleParametersList.Select(x => x.RuleGroupPrecard).FirstOrDefault();
                rulePrecardParam.ID = 0;
                rulePrecardParam.PrecardID = 0;
                rulePrecardParam.Active = true;
                rulePrecardParam.ExistPrecard = 0;
                rulePrecardParam.Operator = ruleGroupPrecard.Operator;
            }
            //else
            //{
            //    rulePrecardParam.PrecardID = -1;
            //}
        }
        if (RuleParametersList != null && RuleParametersList.Count != 0)
        {
            rulePrecardParam.ObjRuleParams = new List<UiValidationRuleParamProxy>();
            foreach (UIValidationRuleParameterProxy ruleParameter in RuleParametersList)
            {
                if (ruleParameter.ExistParam == 1)
                {
                    UiValidationRuleParamProxy ruleParam = new UiValidationRuleParamProxy();
                    ruleParam.ID = ruleParameter.ID;
                    ruleParam.ParamID = ruleParameter.ParamID;
                    ruleParam.KeyName = ruleParameter.KeyName;
                    ruleParam.ExistParam = ruleParameter.ExistParam;
                    ruleParam.ParameterValue = ruleParameter.ParameterValue;
                    ruleParam.ContinueOnTomorrow = ruleParameter.ContinueOnTomorrow;
                    ruleParam.ParamType = ruleParameter.ParamType;                 
                    rulePrecardParam.ObjRuleParams.Add(ruleParam);
                }
            }
        }
        //}

        return rulePrecardParam;
    }
    [Ajax.AjaxMethod("UpdateRulePrecards_RulePrecardsPage", "UpdateRulePrecards_RulePrecardsPage_onCallBack", null, null)]
    public string[] UpdateRulePrecards_RulePrecardsPage(string groupId, string ruleId, string ruleGropId, string strRulePrecardList, string ruleType, string RuleDetails, string CustomCode, string ruleGroupActive, string ruleGroupWarning)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        try
        {
            decimal GroupID = decimal.Parse(this.StringBuilder.CreateString(groupId), CultureInfo.InvariantCulture);
            decimal RuleID = decimal.Parse(this.StringBuilder.CreateString(ruleId), CultureInfo.InvariantCulture);
            decimal RuleGroupID = decimal.Parse(this.StringBuilder.CreateString(ruleGropId), CultureInfo.InvariantCulture);
            int RuleType = int.Parse(this.StringBuilder.CreateString(ruleType));
            int customCode = int.Parse(this.StringBuilder.CreateString(CustomCode));
            RuleDetails = this.StringBuilder.CreateString(RuleDetails);
            bool RuleGroupActive = bool.Parse(this.StringBuilder.CreateString(ruleGroupActive));
            bool RuleGroupWarning = bool.Parse(this.StringBuilder.CreateString(ruleGroupWarning));
            IList<UiValidationRulePrecardParamProxy> rulePrecardParamProxyList = this.JsSerializer.Deserialize<List<UiValidationRulePrecardParamProxy>>(this.StringBuilder.CreateString(strRulePrecardList));
            this.UiValidationBusiness.UpdateRulePrecardsParams(GroupID, RuleID, RuleGroupID, RuleType, rulePrecardParamProxyList, RuleDetails, customCode, RuleGroupActive, RuleGroupWarning);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
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
}