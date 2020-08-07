using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DesignedReports : GTSBasePage
{
    public BReport ReportBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BReport>();
        }
    }
    public BReportParameter ReportParameterBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BReportParameter>();
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
        DesignedReports_onPageLoad,
        tbDesignedReports_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if ( !CallBack_GridDesignedReports_DesignedReports.IsCallback && !CallBack_cmbReportTypes_DesignedReports.IsCallback && !CallBcak_cmbGroupNodes_DesignedReports.IsCallback && !CallBcak_cmbDateParameterType_DesignedReports.IsCallback)
        {
            Page DesignedReportsPage = this;
            Ajax.Utility.GenerateMethodScripts(DesignedReportsPage);
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            
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
    protected void CallBack_GridDesignedReports_DesignedReports_onCallback(object sender, CallBackEventArgs e)
    {
        this.Fill_GridDesignedReports_DesignedReports();
        this.ErrorHiddenField_DesignedReports.RenderControl(e.Output);
        this.GridDesignedReports_DesignedReports.RenderControl(e.Output);
    }
    private void Fill_GridDesignedReports_DesignedReports()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<Report> ReportsList = this.ReportBusiness.GetAllDesignedReports();
            this.GridDesignedReports_DesignedReports.DataSource = ReportsList;
            this.GridDesignedReports_DesignedReports.DataBind();
            
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DesignedReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DesignedReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DesignedReports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }
    protected void CallBack_cmbReportTypes_DesignedReports_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbReportTypes_DesignedReports.Dispose();
        this.Fill_cmbReportTypes_DesignedReports();
        this.ErrorHiddenField_ReportTypes.RenderControl(e.Output);
        this.cmbReportTypes_DesignedReports.RenderControl(e.Output);
    }
    private void Fill_cmbReportTypes_DesignedReports()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<GTS.Clock.Model.Report.DesignedReportType> ReportTypesList = this.ReportBusiness.GetAllDesignedReportType();
            foreach (GTS.Clock.Model.Report.DesignedReportType item in ReportTypesList)
            {
                ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Text = item.Name;
                cbItem.Id = item.ID.ToString();
                cbItem.Value = item.CustomCode.ToString();
                cmbReportTypes_DesignedReports.Items.Add(cbItem);
            }
            
            this.cmbReportTypes_DesignedReports.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ReportTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ReportTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ReportTypes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    protected void CallBcak_cmbGroupNodes_DesignedReports_onCallback(object sender, CallBackEventArgs e)
    {
        this.Fill_cmbGroupNodes_DesignedReports();
        this.ErrorHiddenField_GroupNodes.RenderControl(e.Output);
        this.cmbGroupNodes_DesignedReports.RenderControl(e.Output);
    }

    private void Fill_cmbGroupNodes_DesignedReports()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<GTS.Clock.Model.Report.Report> GroupNodeList = this.ReportBusiness.GetReportGroupNodes();
            this.cmbGroupNodes_DesignedReports.DataSource = GroupNodeList;
            this.cmbGroupNodes_DesignedReports.DataBind();
            this.cmbGroupNodes_DesignedReports.Enabled = true;
            
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GroupNodes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GroupNodes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GroupNodes.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    [Ajax.AjaxMethod("UpdateReport_DesignedReportsPage", "UpdateReport_DesignedReportsPage_onCallBack", null, null)]
    public string[] UpdateReport_DesignedReportsPage(string state, string SelectedReportID, string ReportName, string ReportDescription, string DesignedTypeID, string ParentReportID,string DateParameterTypeID ,string ParentPath)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal ReportID = 0;
            decimal selectedReportID = decimal.Parse(this.StringBuilder.CreateString(SelectedReportID), CultureInfo.InvariantCulture);
            
            string  reportName = this.StringBuilder.CreateString(ReportName);
            string reportDescription = this.StringBuilder.CreateString(ReportDescription);
            decimal designedTypeID = decimal.Parse(this.StringBuilder.CreateString(DesignedTypeID), CultureInfo.InvariantCulture);
            decimal parentReportID = decimal.Parse(this.StringBuilder.CreateString(ParentReportID), CultureInfo.InvariantCulture);
            string parentPath = this.StringBuilder.CreateString(ParentPath);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            
            GTS.Clock.Model.Report.Report  report = new GTS.Clock.Model.Report.Report();
            report.ID = selectedReportID;
            ReportParameterDesigned reportParameterDesignedObj = null;
            if (uam != UIActionType.DELETE)
            {
                GTS.Clock.Model.Report.DesignedReportType designedType = new BDesignedReportsColumn().GetAllDesignedReportsTypes().SingleOrDefault(d => d.ID == designedTypeID);
                report.Name = reportName;
                report.Description = reportDescription;
                report.DesignedType = designedType;
                report.ParentId = parentReportID;
                report.IsReport = true;
                report.IsDesignedReport = true;
                report.ParentPath = parentPath;
                decimal dateParamID = decimal.Parse(this.StringBuilder.CreateString(DateParameterTypeID), CultureInfo.InvariantCulture);
                if (designedType!=null && designedType.CustomCode == DesignedReportTypeEnum.Person)
                    reportParameterDesignedObj = ReportParameterBusiness.GetAllReportParameterDesigned().SingleOrDefault(p => p.CustomCode == "None");
                else
                    reportParameterDesignedObj = ReportParameterBusiness.GetAllReportParameterDesigned().SingleOrDefault(p => p.ID == dateParamID);
                report.ReportParameterDesigned = reportParameterDesignedObj;
                
            }
            BReportParameter reportParameterBusiness = new BReportParameter();
            switch (uam)
            {

                case UIActionType.ADD:
                    ReportID = this.ReportBusiness.InsertReport(report, uam);

                    if (reportParameterDesignedObj!=null)
                    {
                        foreach (ReportParameterDesignedParam item in reportParameterDesignedObj.ReportParameterDesignedParam)
                        {
                            GTS.Clock.Model.Report.ReportParameter reportParameterObj = new GTS.Clock.Model.Report.ReportParameter();
                            reportParameterObj.ID = 0;
                            reportParameterObj.Report = new GTS.Clock.Model.Report.Report() { ID = ReportID };
                            reportParameterObj.Name = item.Parameter;
                            reportParameterObj.ReportUIParameter = reportParameterDesignedObj.ReportUIParameter;


                            decimal param1Id = reportParameterBusiness.InsertReportParameter(reportParameterObj);


                        }
                    }
                     break;
                case UIActionType.EDIT:
                    if (selectedReportID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoReportSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    
                    ReportID = this.ReportBusiness.UpdateReport(report, uam);
                    ReportParameterBusiness.DeleteReportParameterByReportID(ReportID);
                    
                    foreach (ReportParameterDesignedParam item in reportParameterDesignedObj.ReportParameterDesignedParam)
                    {
                        GTS.Clock.Model.Report.ReportParameter reportParameterObj = new GTS.Clock.Model.Report.ReportParameter();
                        reportParameterObj.ID = 0;
                        reportParameterObj.Report = new GTS.Clock.Model.Report.Report() { ID = ReportID };
                        reportParameterObj.Name = item.Parameter;
                        reportParameterObj.ReportUIParameter = reportParameterDesignedObj.ReportUIParameter;
                        
                      
                        decimal param1Id = reportParameterBusiness.InsertReportParameter(reportParameterObj);

                        
                    }
                    break;
                case UIActionType.DELETE:
                    if (selectedReportID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoReportSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    this.ReportBusiness.CheckDeleteAccess();
                    ReportID = this.ReportBusiness.DeleteReport(selectedReportID);
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
            retMessage[3] = ReportID.ToString();
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
    protected void CallBcak_cmbDateParameterType_DesignedReports_onCallback(object sender, CallBackEventArgs e)
    {
        this.Fill_cmbDateParameterType_DesignedReports();
        this.cmbDateParameterType_DesignedReports.Enabled = true;
        this.ErrorHiddenField_DateParameterType.RenderControl(e.Output);
        this.cmbDateParameterType_DesignedReports.RenderControl(e.Output);
    }

    private void Fill_cmbDateParameterType_DesignedReports()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();

            IList<ReportParameterDesigned> reportParamDesList = ReportParameterBusiness.GetAllReportParameterDesigned();
            foreach (ReportParameterDesigned item in reportParamDesList)
            {
                ComboBoxItem comboItem = new ComboBoxItem();
                comboItem.Text = item.Title;
                comboItem.Value = item.ID.ToString();
                cmbDateParameterType_DesignedReports.Items.Add(comboItem);
            }
     
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DateParameterType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DateParameterType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DateParameterType.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

}