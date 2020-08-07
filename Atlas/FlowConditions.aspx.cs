using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure;
using System.Web.Script.Serialization;
using GTS.Clock.Business;

public partial class FlowConditions : GTSBasePage
{
    public BManagerFlowCondition ManagerFlowConditionBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BManagerFlowCondition>();
        }
    }
    enum Scripts
    {
        FlowConditions_onPageLoad,
        DialogFlowConditions_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
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

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridFlowConditions_FlowConditions.IsCallback)
        {
            Page FlowConditionsPage = this;
            Ajax.Utility.GenerateMethodScripts(FlowConditionsPage);

            this.CheckFlowConditionsLoadAccess_FlowConditions();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeDefiantScripts(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void Fill_cmbOperator_FlowConditions()
    {
        DropDownList cmbOperator_FlowConditions = (DropDownList)this.GridFlowConditions_FlowConditions.FindControl("cmbConditionOperator_FlowConditions");
        foreach (string conditionOperatorItem in Enum.GetNames(typeof(ConditionOperators)))
        {
            ListItem cmbItemConditionOperator = new ListItem(this.GetLocalResourceObject(conditionOperatorItem).ToString());
            cmbItemConditionOperator.Value = conditionOperatorItem;
            cmbOperator_FlowConditions.Items.Add(cmbItemConditionOperator);
        }
    }

    private Dictionary<ConditionOperators, string> CreateConditionOperatorsDictionary_FlowConditions()
    {
        Dictionary<ConditionOperators, string> conditionOperatorsDic = new Dictionary<ConditionOperators, string>();
        foreach (string conditionOperatorItem in Enum.GetNames(typeof(ConditionOperators)))
        {
            conditionOperatorsDic.Add((ConditionOperators)Enum.Parse(typeof(ConditionOperators), conditionOperatorItem), this.GetLocalResourceObject(conditionOperatorItem).ToString());
        }
        return conditionOperatorsDic;
    }

    void GridFlowConditions_FlowConditions_NeedRebind(object sender, EventArgs e)
    {
        this.GridFlowConditions_FlowConditions.DataBind();
    }

    private void CheckFlowConditionsLoadAccess_FlowConditions()
    {
        string[] retMessage = new string[4];
        try
        {
            this.ManagerFlowConditionBusiness.CheckFlowConditionsLoadAccess();
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

    protected void CallBack_GridFlowConditions_FlowConditions_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridFlowConditions_FlowConditions(decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture), decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture));
        this.ErrorHiddenField_FlowConditions_FlowConditions.RenderControl(e.Output);
        this.GridFlowConditions_FlowConditions.RenderControl(e.Output);
    }

    private void Fill_GridFlowConditions_FlowConditions(decimal flowID, decimal managerFlowID)
    {
        string[] retMessage = new string[4];
        try
        {
            Dictionary<ConditionOperators, string> conditionOperatorsDic = this.CreateConditionOperatorsDictionary_FlowConditions();
            IList<PrecardGroups> precardAccessGroupList = this.ManagerFlowConditionBusiness.GetAllPrecardGroups();
            foreach (PrecardGroups precardGroupItem in precardAccessGroupList)
            {
                GridItem gridItemPrecardGroup = new GridItem(this.GridFlowConditions_FlowConditions, 0, precardGroupItem);               
                IList<ManagerFlowConditionProxy> managerFlowConditionProxyList = this.ManagerFlowConditionBusiness.GetAllManagerFlowConditionsByPrecardGroup(flowID, managerFlowID, precardGroupItem.ID, conditionOperatorsDic);
                foreach (ManagerFlowConditionProxy managerFlowConditionProxyItem in managerFlowConditionProxyList)
                {
                    GridItem gridItemManagerFlowConditionProxy = new GridItem(this.GridFlowConditions_FlowConditions, 1, managerFlowConditionProxyItem);
                    gridItemPrecardGroup.Items.Add(gridItemManagerFlowConditionProxy);
                }
                this.GridFlowConditions_FlowConditions.Items.Add(gridItemPrecardGroup);
                this.GridFlowConditions_FlowConditions.ExpandAll();
            }
            this.Fill_cmbOperator_FlowConditions();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_FlowConditions_FlowConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_FlowConditions_FlowConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_FlowConditions_FlowConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateFlowConditions_FlowConditionsPage", "UpdateFlowConditions_FlowConditionsPage_onCallBack", null, null)]
    public string[] UpdateFlowConditions_FlowConditionsPage(string FlowID, string ManagerFlowID, string strFlowConditionsList)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal flowID = decimal.Parse(this.StringBuilder.CreateString(FlowID), CultureInfo.InvariantCulture);
            decimal managerFlowID = decimal.Parse(this.StringBuilder.CreateString(ManagerFlowID), CultureInfo.InvariantCulture);
            IList<ManagerFlowConditionProxy> managerFlowConditionProxyList = this.JsSerializer.Deserialize<List<ManagerFlowConditionProxy>>(this.StringBuilder.CreateString(strFlowConditionsList));

            this.ManagerFlowConditionBusiness.UpdateFlowConditions(flowID, managerFlowID, managerFlowConditionProxyList);

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