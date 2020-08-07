using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business;


public partial class ShiftPairTypes : GTSBasePage
{
    public BShiftPairType ShiftPairTypesBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BShiftPairType>();
        }
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

    enum Scripts
    {
        ShiftPairTypes_onPageLoad,
        tbShiftPairTypesIntroduction_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridShiftPairTypes_ShiftPairTypes.IsCallback)
        {
            Page ShiftPairTypesPage = this;
            Ajax.Utility.GenerateMethodScripts(ShiftPairTypesPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckShiftPairTypesLoadAccess_ShiftPairTypes();
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


    private void CheckShiftPairTypesLoadAccess_ShiftPairTypes()
    {
        string[] retMessage = new string[4];
        try
        {
            this.ShiftPairTypesBusiness.CheckShiftPairTypesLoadAccess();
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    protected void CallBack_GridShiftPairTypes_ShiftPairTypes_onCallback(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridShiftPairTypes_ShiftPairTypes();
        this.ErrorHiddenField_ShiftPairTypes.RenderControl(e.Output);
        this.GridShiftPairTypes_ShiftPairTypes.RenderControl(e.Output);
    }

    private void Fill_GridShiftPairTypes_ShiftPairTypes()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<ShiftPairType> ShiftPairTypesList = this.ShiftPairTypesBusiness.GetAll();
            this.GridShiftPairTypes_ShiftPairTypes.DataSource = ShiftPairTypesList;
            this.GridShiftPairTypes_ShiftPairTypes.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ShiftPairTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ShiftPairTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ShiftPairTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateShiftPairType_ShiftPairTypesPage", "UpdateShiftPairType_ShiftPairTypesPage_onCallBack", null, null)]
    public string[] UpdateShiftPairType_ShiftPairTypesPage(string state, string SelectedShiftPairTypeID, string Active, string ShiftPairTypeCode, string ShiftPairTypeTitle, string Descriptions)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal ShiftPairTypeID = 0;
            decimal selectedShiftPairTypeID = decimal.Parse(this.StringBuilder.CreateString(SelectedShiftPairTypeID), CultureInfo.InvariantCulture);
            bool active = bool.Parse(this.StringBuilder.CreateString(Active));
            ShiftPairTypeCode = this.StringBuilder.CreateString(ShiftPairTypeCode);
            ShiftPairTypeTitle = this.StringBuilder.CreateString(ShiftPairTypeTitle);
            Descriptions = this.StringBuilder.CreateString(Descriptions);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            ShiftPairType shiftPairType = new ShiftPairType();
            shiftPairType.ID = selectedShiftPairTypeID;
            if (uam != UIActionType.DELETE)
            {
                shiftPairType.Active = active;
                shiftPairType.CustomCode = ShiftPairTypeCode;
                shiftPairType.Title = ShiftPairTypeTitle;
                shiftPairType.Description = Descriptions;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    ShiftPairTypeID = this.ShiftPairTypesBusiness.InsertShiftPairType(shiftPairType, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedShiftPairTypeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoShiftPairTypeSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    ShiftPairTypeID = this.ShiftPairTypesBusiness.UpdateShiftPairType(shiftPairType, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedShiftPairTypeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoShiftPairTypeSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    ShiftPairTypeID = this.ShiftPairTypesBusiness.DeleteShiftPairType(shiftPairType, uam);
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
            retMessage[3] = ShiftPairTypeID.ToString();
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