using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterReportViewer : GTSBasePage
{
    enum Scripts
    {
        MasterReportViewer_onPageLoad,
        MasterReportViewer_Operations
    }

    internal class ReportViewerAttributes
    {
        public string ReportGUID { get; set; }
        public string ReportTitle { get; set; }
        public string IsDesigned { get; set; }
        public string IsContainsForm { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        
    }

    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
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
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        this.InitMasterReportViewer_MasterReportViewer();
        this.CacheReportViewerAttributes();
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


    private void InitMasterReportViewer_MasterReportViewer()
    {
        if(HttpContext.Current.Request.QueryString.AllKeys.Contains("Width") && HttpContext.Current.Request.QueryString.AllKeys.Contains("Height"))
        {
            string width = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Width"]);
            string height = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Height"]);
            this.MasterReportViewerFrame_MasterReportViewer.Attributes.Add("style", "overflow:auto;width:" + width + "px;height:" + height + "px;");
        }
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportTitle"))
            this.hfReportViewerTitle_MasterReportViewer.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportTitle"]);
    }

    private void CacheReportViewerAttributes()
    {
        ReportViewerAttributes reportViewerAttributes = new ReportViewerAttributes();
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportGUID"))
            reportViewerAttributes.ReportGUID = HttpContext.Current.Request.QueryString["ReportGUID"];
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportTitle"))
            reportViewerAttributes.ReportTitle = HttpContext.Current.Request.QueryString["ReportTitle"];
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("IsDesigned"))
            reportViewerAttributes.IsDesigned = HttpContext.Current.Request.QueryString["IsDesigned"];
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("IsContainsForm"))
            reportViewerAttributes.IsContainsForm = HttpContext.Current.Request.QueryString["IsContainsForm"];
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Width"))
            reportViewerAttributes.Width = HttpContext.Current.Request.QueryString["Width"];
        if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Height"))
            reportViewerAttributes.Height = HttpContext.Current.Request.QueryString["Height"];
        this.hfMasterReportViewerFrame_MasterReportViewer.Value = this.JsSerializer.Serialize(reportViewerAttributes);
    }
}