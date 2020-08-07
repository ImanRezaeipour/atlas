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
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure;

public partial class UiValidationRules : GTSBasePage
{
    enum RuleParametersTimeState
    {
        Get,
        Set
    }
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
        Alert_Box,
        DropDownDive,
        UiValidationRules_onPageLoad,
        DialogUiValidationRules_Operations,
        DialogWaiting_Operations,
        HelpForm_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
		if (!this.CallBack_GridUiValidationRules_UiValidationRules.CausedCallback && !CallBack_cmbRuleGroup_UiValidationRules.IsCallback)
        {
            Page UiValidation = this;
            Ajax.Utility.GenerateMethodScripts(UiValidation);

            this.CheckUIValidationRulesLoadAccess_UiValidationRules();
            this.ViewCurrentLangCalendars_UiValidationRules();
            this.SetCurrentDate_RuleParameters();
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void ViewCurrentLangCalendars_UiValidationRules()
    {
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                this.Container_pdpDate_RuleParameters.Visible = true;
                break;
            case "en-US":
                this.Container_gdpDate_RuleParameters.Visible = true;
                break;
        }
    }

    private void CheckUIValidationRulesLoadAccess_UiValidationRules()
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

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
        SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripRuleParametersTerms);
    }

    private void SetCurrentDate_RuleParameters()
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
        this.hfCurrentDate_RuleParameters.Value = strCurrentDate;
    }
    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }
    private void SetCurrentCultureResObjs(string LangID)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }
    protected void CallBack_GridUiValidationRules_UiValidationRules_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        decimal selectedGroupID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
		int selectedRuleGroupID = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
		this.Fill_GridUiValidationRules_UiValidationRules(selectedGroupID, selectedRuleGroupID);
        this.GridUiValidationRules_UiValidationRules.RenderControl(e.Output);
        this.ErrorHiddenField_UiValidationRules.RenderControl(e.Output);
    }
    private void Fill_GridUiValidationRules_UiValidationRules(decimal GroupID,int ruleGroupId)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
			IList<GTS.Clock.Business.Proxy.UIValidationGroupingProxy> uiValidationRuleList;
			if(ruleGroupId==0)
                uiValidationRuleList = this.UiValidationBusiness.GetRuleList(GroupID).OrderBy(o=>o.Type).ToList();
			else
				uiValidationRuleList = this.UiValidationBusiness.GetRuleList(GroupID).Where(u => u.Type == (UIValidationRuleType)ruleGroupId).ToList();
            this.GridUiValidationRules_UiValidationRules.DataSource = uiValidationRuleList;
            this.GridUiValidationRules_UiValidationRules.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_UiValidationRules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_UiValidationRules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_UiValidationRules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    protected void CallBack_GridParameterUiValidationRules_UiValidationRules_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        decimal selectedGroupID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
        decimal selectedRuleID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
        this.Fill_GridParameterUiValidationRules_UiValidationRules(selectedGroupID, selectedRuleID);
        this.GridParameterUiValidationRules_UiValidationRules.RenderControl(e.Output);
        this.ErrorHiddenField_ParameterUiValidationRules.RenderControl(e.Output);
    }
    private void Fill_GridParameterUiValidationRules_UiValidationRules(decimal GroupID, decimal RuleID)
    {
        string[] retMessage = new string[4];
        try
        {            
            this.InitializeCulture();
            IList<GTS.Clock.Model.UIValidation.UIValidationRuleParameter> uiValidationRuleParameterList = this.UiValidationBusiness.GetRuleParameter(GroupID, RuleID);
            foreach (UIValidationRuleParameter uiValidationRuleParameterItem in uiValidationRuleParameterList)
            {
                if (uiValidationRuleParameterItem.Type == RuleParamType.Time && uiValidationRuleParameterItem.ContinueOnTomorrow)

                    uiValidationRuleParameterItem.Value = "+" + uiValidationRuleParameterItem.Value;
            }
            this.GridParameterUiValidationRules_UiValidationRules.DataSource = uiValidationRuleParameterList;
            this.GridParameterUiValidationRules_UiValidationRules.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ParameterUiValidationRules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ParameterUiValidationRules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ParameterUiValidationRules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    [Ajax.AjaxMethod("UpdateUiValidationRules_UiValidationRulesPage", "UpdateUiValidationRules_UiValidationRulesPage_onCallBack", null, null)]
    public string[] UpdateUiValidationRules_UiValidationRulesPage(string strListRow, string state)
    {
        this.InitializeCulture();
        string[] retMessage = new string[4];
        IList<UIValidationGroupingProxy> UIValidationGroupingProxyList = this.CreateUIValidationGroupingProxyList(this.StringBuilder.CreateString(strListRow));
        UIActionType uam = UIActionType.EDIT;

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            switch (this.StringBuilder.CreateString(state))
            {
              
                case "Edit":
                    uam = UIActionType.EDIT;
                    if (strListRow == "")
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("nouivalidationselectedforedit").ToString()), retMessage);
                        return retMessage;
                    }
                    break;
                
                default:
                    break;
            }
            this.UiValidationBusiness.UpdateRuleList(UIValidationGroupingProxyList);


            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                
                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
               
                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            // retMessage[3] = uiValidationID.ToString();
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

    private IList<UIValidationGroupingProxy> CreateUIValidationGroupingProxyList(string StrObjRuleFeatures)
    {
        IList<UIValidationGroupingProxy> UIValidationGroupingProxyList = new List<UIValidationGroupingProxy>();
        object[] ParamsBatchList = (object[])this.JsSerializer.DeserializeObject(StrObjRuleFeatures);
        foreach (object paramsBatch in ParamsBatchList)
        {
            Dictionary<string, object> paramsBatchDic = (Dictionary<string, object>)paramsBatch;
            UIValidationGroupingProxy UIVGP = new UIValidationGroupingProxy();
            UIVGP.ID = decimal.Parse(paramsBatchDic["uvID"].ToString());
            UIVGP.Active = bool.Parse(paramsBatchDic["IsA"].ToString());
            UIVGP.OpratorRestriction = bool.Parse(paramsBatchDic["IsO"].ToString());
            UIVGP.GroupID = decimal.Parse(paramsBatchDic["GID"].ToString(), CultureInfo.InvariantCulture);
            UIVGP.RuleID = decimal.Parse(paramsBatchDic["RID"].ToString(), CultureInfo.InvariantCulture);
            UIValidationGroupingProxyList.Add(UIVGP);
        }
        return UIValidationGroupingProxyList;
    }
    [Ajax.AjaxMethod("UpdateParameterUiValidationRules_UiValidationRulesPage", "UpdateParameterUiValidationRules_UiValidationRulesPage_onCallBack", null, null)]
    public string[] UpdateParameterUiValidationRules_UiValidationRulesPage(string strListRow, string state)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        
        IList<UIValidationRuleParameter> UIValidationRuleParameterList = this.CreateUIValidationRuleParameterList(this.StringBuilder.CreateString(strListRow));

        UIActionType uam = UIActionType.EDIT;

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            switch (this.StringBuilder.CreateString(state))
            {

                case "Edit":
                    uam = UIActionType.EDIT;
                    if (strListRow == "")
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("nouivalidationselectedforedit").ToString()), retMessage);
                        return retMessage;
                    }
                    break;

                default:
                    break;
            }
            this.UiValidationBusiness.UpdateRuleParameter(UIValidationRuleParameterList);


            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {

                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;

                default:
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            // retMessage[3] = uiValidationID.ToString();
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

    private IList<UIValidationRuleParameter> CreateUIValidationRuleParameterList(string StrObjRuleParameter)
    {
        IList<UIValidationRuleParameter> UIValidationRuleParameterList = new List<UIValidationRuleParameter>();
        object[] ParamsBatchList = (object[])this.JsSerializer.DeserializeObject(StrObjRuleParameter);
        foreach (object paramsBatch in ParamsBatchList)
        {
            string ParamValue = string.Empty;
            Dictionary<string, object> paramsBatchDic = (Dictionary<string, object>)paramsBatch;
            UIValidationRuleParameter UIVGP = new UIValidationRuleParameter();
            UIVGP.ID = decimal.Parse(paramsBatchDic["PID"].ToString(), CultureInfo.InvariantCulture);
            UIVGP.Name = paramsBatchDic["Name"].ToString();
            RuleParamType ParameterType = (RuleParamType)Enum.ToObject(typeof(RuleParamType), int.Parse(paramsBatchDic["Type"].ToString()));
            if (ParameterType == RuleParamType.Time && paramsBatchDic["Val"].ToString().Contains("+"))
            {
                ParamValue = paramsBatchDic["Val"].ToString().Replace("+", string.Empty);
                UIVGP.ContinueOnTomorrow = true;
            }
            else
                ParamValue = paramsBatchDic["Val"].ToString();
            UIVGP.TheValue = ParamValue;
            UIVGP.KeyName = paramsBatchDic["KN"].ToString();
			UIVGP.Type = (RuleParamType)Convert.ToInt32(paramsBatchDic["Type"]);
          UIValidationRuleParameterList.Add(UIVGP);
        }
        return UIValidationRuleParameterList;
    }
	protected void CallBack_cmbRuleGroup_UiValidationRules_onCallback(object sender, CallBackEventArgs e)
	{
		this.cmbRuleGroup_UiValidationRules.Dispose();
		this.Fill_cmbRuleGroup_UiValidationRules(0);
		this.ErrorHiddenField_RuleGroup.RenderControl(e.Output);
		this.cmbRuleGroup_UiValidationRules.RenderControl(e.Output);
	}
	private void Fill_cmbRuleGroup_UiValidationRules(decimal groupID)
	{
		this.cmbRuleGroup_UiValidationRules.Enabled = true;

		string[] retMessage = new string[4];
		try
		{
			this.InitializeCulture();

			ComponentArt.Web.UI.ComboBoxItem cbItemAll = new ComboBoxItem();
			cbItemAll.Text = GetLocalResourceObject("AllRuleGroup").ToString();
			cbItemAll.Value = "0";
			cmbRuleGroup_UiValidationRules.Items.Add(cbItemAll);
			foreach (UIValidationRuleType  ruleType in Enum.GetValues(typeof(UIValidationRuleType)))
			{
				ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
				cbItem.Text = GetLocalResourceObject(ruleType.ToString()).ToString();
				cbItem.Value = ((int)ruleType).ToString();

				cmbRuleGroup_UiValidationRules.Items.Add(cbItem);
			}
			
			this.cmbRuleGroup_UiValidationRules.Enabled = true;
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