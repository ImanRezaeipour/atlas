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
using ComponentArt.Web.UI;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.RequestFlow;


public partial class FlowGroup : GTSBasePage
{
    public BFlowGroup FlowGroupBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BFlowGroup>();
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
        FlowGroup_onPageLoad,
        tbFlowGroup_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridFlowGroup_FlowGroup.IsCallback)
        {
            Page FlowGroupPage = this;
            Ajax.Utility.GenerateMethodScripts(FlowGroupPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckFlowGroupsLoadAccess_FlowGroup();
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
    private void CheckFlowGroupsLoadAccess_FlowGroup()
    {
        string[] retMessage = new string[4];
        try
        {
            this.FlowGroupBusiness.CheckFlowGroupLoadAccess();
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    protected void CallBack_GridFlowGroup_FlowGroup_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridFlowGroup_FlowGroup();
        this.GridFlowGroup_FlowGroup.RenderControl(e.Output);
        this.ErrorHiddenField_FlowGroup.RenderControl(e.Output);
    }

    private void Fill_GridFlowGroup_FlowGroup()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<GTS.Clock.Model.RequestFlow.FlowGroup> FlowGroupList = this.FlowGroupBusiness.GetAll();
            this.GridFlowGroup_FlowGroup.DataSource = FlowGroupList;
            this.GridFlowGroup_FlowGroup.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_FlowGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_FlowGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_FlowGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateFlowGroup_FlowGroupPage", "UpdateFlowGroup_FlowGroupPage_onCallBack", null, null)]
    public string[] UpdateFlowGroup_FlowGroupPage(string state, string SelectedFlowGroupID, string FlowGroupName, string FlowGroupDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal FlowGroupID = 0;
            decimal selectedFlowGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedFlowGroupID), CultureInfo.InvariantCulture);
            FlowGroupName = this.StringBuilder.CreateString(FlowGroupName);
            FlowGroupDescription = this.StringBuilder.CreateString(FlowGroupDescription);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            GTS.Clock.Model.RequestFlow.FlowGroup FlowGroup = new GTS.Clock.Model.RequestFlow.FlowGroup();
            FlowGroup.ID = selectedFlowGroupID;
            if (uam != UIActionType.DELETE)
            {
                FlowGroup.Name = FlowGroupName;
                FlowGroup.Description = FlowGroupDescription;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    FlowGroupID = this.FlowGroupBusiness.InsertFlowGroup(FlowGroup, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedFlowGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoFlowGroupSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    FlowGroupID = this.FlowGroupBusiness.UpdateFlowGroup(FlowGroup, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedFlowGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoFlowGroupSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    FlowGroupID = this.FlowGroupBusiness.DeleteFlowGroup(FlowGroup, uam);
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
            retMessage[3] = FlowGroupID.ToString();
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