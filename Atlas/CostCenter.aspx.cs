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

public partial class CostCenter : GTSBasePage
{
    public BCostCenter CostCenterBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BCostCenter>();
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
        CostCenter_onPageLoad,
        tbCostCenter_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }
     
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridCostCenter_CostCenter.IsCallback)
        {
            Page CostCenterPage = this;
            Ajax.Utility.GenerateMethodScripts(CostCenterPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckCostCentersLoadAccess_CostCenter();
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
    private void CheckCostCentersLoadAccess_CostCenter()
    {
        string[] retMessage = new string[4];
        try
        {
            this.CostCenterBusiness.CheckCostCentersLoadAccess();
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }
    protected void CallBack_GridCostCenter_CostCenter_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridCostCenter_CostCenter();
        this.GridCostCenter_CostCenter.RenderControl(e.Output);
        this.ErrorHiddenField_CostCenter.RenderControl(e.Output);
    }
    private void Fill_GridCostCenter_CostCenter()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<GTS.Clock.Model.BaseInformation.CostCenter> CostCenterList = this.CostCenterBusiness.GetAllWithOutCheckAccess();
            this.GridCostCenter_CostCenter.DataSource = CostCenterList;
            this.GridCostCenter_CostCenter.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_CostCenter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_CostCenter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_CostCenter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateCostCenter_CostCenterPage", "UpdateCostCenter_CostCenterPage_onCallBack", null, null)]
    public string[] UpdateCostCenter_CostCenterPage(string state, string SelectedCostCenterID, string CostCenterName, string CostCenterCode, string CostCenterDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal CostCenterID = 0;
            decimal selectedCostCenterID = decimal.Parse(this.StringBuilder.CreateString(SelectedCostCenterID), CultureInfo.InvariantCulture);
            CostCenterName = this.StringBuilder.CreateString(CostCenterName);
            CostCenterCode = this.StringBuilder.CreateString(CostCenterCode);
            CostCenterDescription = this.StringBuilder.CreateString(CostCenterDescription);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            GTS.Clock.Model.BaseInformation.CostCenter CostCenter = new GTS.Clock.Model.BaseInformation.CostCenter();
            CostCenter.ID = selectedCostCenterID;
            if (uam != UIActionType.DELETE)
            {
                CostCenter.Name = CostCenterName;
                CostCenter.Code = CostCenterCode;
                CostCenter.Description = CostCenterDescription;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    CostCenterID = this.CostCenterBusiness.InsertCostCenter(CostCenter, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedCostCenterID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoCostCenterSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    CostCenterID = this.CostCenterBusiness.UpdateCostCenter(CostCenter, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedCostCenterID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoCostCenterSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    CostCenterID = this.CostCenterBusiness.DeleteCostCenter(CostCenter, uam);
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
            retMessage[3] = CostCenterID.ToString();
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