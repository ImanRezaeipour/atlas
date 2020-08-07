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

public partial class Grade : GTSBasePage
{
    public BGrade GradeBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BGrade>();
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
        Grade_onPageLoad,
        tbGrade_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridGrade_Grade.IsCallback)
        {
            Page GradePage = this;
            Ajax.Utility.GenerateMethodScripts(GradePage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckGradesLoadAccess_Grade();
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
    private void CheckGradesLoadAccess_Grade()
    {
        string[] retMessage = new string[4];
        try
        {
            this.GradeBusiness.CheckGradesLoadAccess();
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    protected void CallBack_GridGrade_Grade_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridGrade_Grade();
        this.GridGrade_Grade.RenderControl(e.Output);
        this.ErrorHiddenField_Grade.RenderControl(e.Output);
    }

    private void Fill_GridGrade_Grade()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<GTS.Clock.Model.BaseInformation.Grade> gradeList = this.GradeBusiness.GetAll();
            this.GridGrade_Grade.DataSource = gradeList;
            this.GridGrade_Grade.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Grade.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Grade.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Grade.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateGrade_GradePage", "UpdateGrade_GradePage_onCallBack", null, null)]
    public string[] UpdateGrade_GradePage(string state, string SelectedGradeID, string GradeName, string GradeDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal GradeID = 0;
            decimal selectedGradeID = decimal.Parse(this.StringBuilder.CreateString(SelectedGradeID), CultureInfo.InvariantCulture);
            GradeName = this.StringBuilder.CreateString(GradeName);
            GradeDescription = this.StringBuilder.CreateString(GradeDescription);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            GTS.Clock.Model.BaseInformation.Grade grade = new GTS.Clock.Model.BaseInformation.Grade();
            grade.ID = selectedGradeID;
            if (uam != UIActionType.DELETE)
            {
                grade.Name = GradeName;
                grade.Description = GradeDescription;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    GradeID = this.GradeBusiness.InsertGrade(grade, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedGradeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoGradeSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    GradeID = this.GradeBusiness.UpdateGrade(grade, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedGradeID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoGradeSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    GradeID = this.GradeBusiness.DeleteGrade(grade, uam);
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
            retMessage[3] = GradeID.ToString();
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