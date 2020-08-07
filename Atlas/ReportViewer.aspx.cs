using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report.Web;
using Stimulsoft.Report;
using GTS.Clock.Infrastructure.Report;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Temp;
using System.IO;
using System.Web.Script.Serialization;
using System.Data;

public partial class ReportViewer : GTSBasePage
{
    internal class ReportAttributesObj
    {
        public bool IsContainsForm { get; set; }
    }

    public BTemp TempBusiness
    {
        get
        {
            return new BTemp();
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
        ReportViewer_onPageLoad,
        ReportViewer_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack || HttpContext.Current.Request.QueryString.AllKeys.Contains("OME"))
        {
            this.InitReportViewer_ReportViewer();
            this.GetReport_ReportViewer();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
        this.StiReportViewer.ReportExport += StiReportViewer_ReportExport;
    }

    private void InitializeReportAttributesObj()
    {
        ReportAttributesObj rao = new ReportAttributesObj();
        rao.IsContainsForm = HttpContext.Current.Request.QueryString.AllKeys.Contains("IsContainsForm") && bool.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["IsContainsForm"]));
        this.hfReportAttributes_ReportViewer.Value = this.JsSerializer.Serialize(rao);
    }

    protected override void InitializeCulture()
    {
        this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
        base.InitializeCulture();
    }

    /// <summary>
    /// تنظیم زبان انتخابی کاربر 
    /// </summary>
    /// <param name="LangID"></param>
    private void SetCurrentCultureResObjs(string LangID)
    {
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    void StiReportViewer_ReportExport(object sender, StiExportDataEventArgs e)
    {
        try
        {
            if (e.Settings.GetExportFormat() == StiExportFormat.Excel || e.Settings.GetExportFormat() == StiExportFormat.Excel2007)
            {
                this.StiReportViewer.ExportResponse = false;
                Stimulsoft.Report.Export.StiExcelExportSettings stiExcelExportSettings = new Stimulsoft.Report.Export.StiExcelExportSettings();
                stiExcelExportSettings.ExportDataOnly = true;
                stiExcelExportSettings.UseOnePageHeaderAndFooter = true;
                stiExcelExportSettings.ExportPageBreaks = false;
                stiExcelExportSettings.ExportObjectFormatting = false;
                MemoryStream xlsMemoryStream = new MemoryStream();
                e.Report.ExportDocument(StiExportFormat.Excel, xlsMemoryStream, stiExcelExportSettings);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=Report.xls");
                Response.ContentType = "application/ms-excel";
                Response.BinaryWrite(xlsMemoryStream.ToArray());
                Response.End();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.EnableViewState = true;
    }

    private void InitReportViewer_ReportViewer()
    {
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportTitle"))
            hfReportViewerTitle_ReportViewer.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportTitle"]);
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("OME"))
            this.StiReportViewer.RenderMode = StiRenderMode.Standard;
        else
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("IsContainsForm") && bool.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["IsContainsForm"])))
                this.StiReportViewer.RenderMode = StiRenderMode.AjaxWithCache;
        }
        InitializeReportAttributesObj();
    }


    private void GetReport_ReportViewer()
    {
        string[] retMessage = new string[3];
        try
        {
            bool IsContainsFormExistance = HttpContext.Current.Request.QueryString.AllKeys.Contains("IsContainsForm");
            bool IsContainsForm = bool.Parse(this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["IsContainsForm"]));
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportGUID"))
            {
                string stiReportGUID = HttpContext.Current.Request.QueryString["ReportGUID"];
                if (Session["SysReports"] != null)
                {
                    Dictionary<string, StiReport> SysReportsDic = (Dictionary<string, StiReport>)Session["SysReports"];
                    if (SysReportsDic.Keys.Contains(stiReportGUID))
                    {
                        this.StiReportViewer.Report = SysReportsDic[stiReportGUID];
                        if (Request.Browser.Browser == "InternetExplorer" && Request.Browser.Version.StartsWith("11."))
                            StiReportViewer.PrintDestination = StiPrintDestination.WithPreview;
                        if (!HttpContext.Current.Request.QueryString.AllKeys.Contains("OME") && (this.StiReportViewer.RenderMode == StiRenderMode.AjaxWithCache && !IsContainsFormExistance || (IsContainsFormExistance && !IsContainsForm)))
                            SysReportsDic.Remove(stiReportGUID);
                        Session["SysReports"] = SysReportsDic;
                        if (!IsContainsFormExistance || (IsContainsFormExistance && !IsContainsForm))
                            this.TempBusiness.DeleteTempList(ReportHelper.Instance().OperationGUID);
                    }
                }
            }

        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (Exception ex)
        {
            if (ex is OutOfMemoryException && !HttpContext.Current.Request.QueryString.AllKeys.Contains("OME"))
            {
                this.StiReportViewer.Report = null;
                string url = "ReportViewer.aspx?";
                foreach (string key in HttpContext.Current.Request.QueryString.AllKeys)
                {
                    url += key + "=" + HttpContext.Current.Request.QueryString[key] + "&";
                }
                url += "OME=true";
                Response.Redirect(url);
            }
            else
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage) + "&ReportGUID=" + HttpContext.Current.Request.QueryString["ReportGUID"]);
            }
        }
    }

    private void ShowDesignedReport_ReportViewer()
    {
        string[] retMessage = new string[3];
        try
        {

        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
        }
    }
    protected void StiReportViewer_GetReportData(object sender, StiReportDataEventArgs e)
    {

        string[] retMessage = new string[4];
        try
        {
            if (IsPostBack)
            {
                if (Session[e.Report.ReportName] != null)
                    e.Report.RegData((DataTable)Session[e.Report.ReportName]);
            }
        }
        catch (Exception ex)
        {
            Response.Clear();
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            Response.Write(this.exceptionHandler.CreateErrorMessage(retMessage));
            Response.Flush();
        }
    }
}