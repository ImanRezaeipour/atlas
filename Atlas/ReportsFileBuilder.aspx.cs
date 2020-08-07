using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Report;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportsFileBuilder : GTSBasePage
{ 
    public BReport bReport
    {
        get
        {
            return new BReport();
        }
    }

    public BReportParameter bReportParameter
    {
        get
        {
            return new BReportParameter();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.EnableViewState = true;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.FillReportsRoot();
        }
    }
    protected void btnCreateReportFiles_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblMessage.Text = string.Empty;
            this.bReport.CreateAllReportFiles();
            this.lblMessage.Text = "All Reports Files Created Successfully";
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
        }
    }
    protected void btnUpdateReportFile_Click(object sender, EventArgs e)
    {
        try
        {
            decimal reportFileID = 0;
            this.lblMessage.Text = string.Empty;
            decimal.TryParse(this.txtReportFileID.Text, out reportFileID);
            this.bReport.UpdateReportFile(reportFileID);
            this.lblMessage.Text = "Report File with ID=" + reportFileID + " Updated Successfully in DB";
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.lblMessage2.Text = "";
        try
        {
            if (FileUpload1.HasFile)
            {
                decimal reportID = SaveFile(FileUpload1.PostedFile);
                this.lblMessage2.Text = "Report with ID=" + reportID + " Updated Successfully in DB";
            }
        }
        catch (Exception ex)
        {
            this.lblMessage2.Text = ex.Message;
        }
    }

    decimal SaveFile(HttpPostedFile file)
    {
        string reportFilesPathKey = AppFolders.ReportFiles.ToString();
        string savePath = AppDomain.CurrentDomain.BaseDirectory + reportFilesPathKey;

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string fileName = FileUpload1.FileName;
        string pathToCheck = savePath + fileName;

        if (System.IO.File.Exists(pathToCheck))
        {
            File.Delete(pathToCheck);
        }

        savePath += fileName;
        FileUpload1.SaveAs(savePath);
       return  UploadReport(fileName, savePath);
    }

    void FillReportsRoot()
    {
        IList<Report> groups = this.bReport.GetReportGroups();
        DrpRoot.DataSource = groups;
        DrpRoot.DataValueField = "ID";
        DrpRoot.DataTextField = "Name";
        DrpRoot.DataBind();
    }

    decimal UploadReport(string ReportName, string ReportPath)
    {
        var nameParam = this.bReport.ReportNameResolution(ReportName);
        var rptName = nameParam[0];
        var rptDescription = nameParam[1];
        var rptParameter = nameParam[3];

        var report = this.bReport.GetReportByName(rptDescription);
        //delete existing report
        if (report != null)
        {
            this.bReportParameter.DeleteReportParameterByReportID(report.ID);
            this.bReportParameter.DeleteReportParameterByFileID(report.ReportFile.ID);
            this.bReport.DeleteReportFile(report.ReportFile.ID);
        }

        StiReport stiReport = new StiReport();
        stiReport.Load(ReportPath);

        //add report as new report

        //first: add reportFile
        ReportFile newReportFile = new ReportFile();
        newReportFile.Name = rptName;
        newReportFile.File = stiReport.SaveToString();
        newReportFile.Description = rptDescription;
        newReportFile.SubSystemId = SubSystemIdentifier.TimeAtendance;
        var fileId = this.bReport.InsertReportFile(newReportFile);

        //next: add report and add reportFile in it
        Report newReport = new Report();
        newReport.Name = rptDescription;
        newReport.ParentId = Convert.ToDecimal(this.DrpRoot.SelectedValue);
        newReport.ReportFile = new ReportFile() { ID = fileId };
        newReport.IsReport = true;
        newReport.ParentPath = GetparentPath();
        newReport.Order = this.bReport.GetMaxReportOrder();
        newReport.SubSystemId = SubSystemIdentifier.TimeAtendance;
        newReport.IsDesignedReport = false;
        var reportId = this.bReport.InsertReport(newReport, GTS.Clock.Business.UIActionType.ADD);

        //next:add report param
       
        var uiParam = this.bReportParameter.GetReportUIParameterByName(rptParameter);
        switch (rptParameter)
        {
            case "Date Range Order": AddReportParamOrderToDate(uiParam, fileId);
                break;
            case "Date FromTo Date": AddReportParamFromDateToDate(uiParam, fileId);
                break;
            case "FirstDayYear To Date": AddReportParamtoDate(uiParam, fileId);
                break;
            case "Station Clock Date": AddReportParamFromDateToDateStationIDClockID(uiParam, fileId);
                break;
            case "Date Range Order Value": AddReportParamOrderToDateValue(uiParam, fileId);
                break;
        }
        //End

        return reportId;
    }

    string GetparentPath()
    {
        string[] strArray = new string[2];

        string mainRoot = this.DrpRoot.Items[0].Value.ToString();
        string selectedRoot = this.DrpRoot.SelectedValue.ToString();

        strArray[0] = "," + mainRoot + ",";
        if (mainRoot != selectedRoot)
        {
            strArray[1] = selectedRoot + ",";
        }
        return string.Concat(strArray);
    }

    void AddReportParamFromDateToDate(ReportUIParameter reportUIParameter, decimal reportFileId)
    {

        ReportParameter parameter1 = new ReportParameter
        {
            ReportUIParameter = reportUIParameter,
            Name = "@fromDate",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter1);

        ReportParameter parameter2 = new ReportParameter
        {
            ReportUIParameter = reportUIParameter,
            Name = "@toDate",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter2);
    }
    void AddReportParamOrderToDate(ReportUIParameter reportUIParameter, decimal reportFileId)
    {
        ReportParameter parameter1 = new ReportParameter
        {
            ReportUIParameter = reportUIParameter,
            Name = "@Order",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter1);

        ReportParameter parameter2 = new ReportParameter
        {
            ReportUIParameter = reportUIParameter,
            Name = "@ToDate",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter2);
    }
    void AddReportParamOrderToDateValue(ReportUIParameter reportUIParameter, decimal reportFileId)
    {
        ReportParameter parameter1 = new ReportParameter
        {
            ReportUIParameter = reportUIParameter,
            Name = "@Order",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter1);

        ReportParameter parameter2 = new ReportParameter()
        {
            ReportUIParameter = reportUIParameter,
            Name = "@ToDate",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter2);

        ReportParameter parameter3 = new ReportParameter()
        {
            ReportUIParameter = reportUIParameter,
            Name = "@Value",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter3);
    }
    void AddReportParamtoDate(ReportUIParameter reportUIParameter, decimal reportFileId)
    {
        ReportParameter parameter1 = new ReportParameter()
        {
            ReportUIParameter = reportUIParameter,
            Name = "@toDate",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter1);
    }
    void AddReportParamFromDateToDateStationIDClockID(ReportUIParameter reportUIParameter, decimal reportFileId)
    {
        ReportParameter parameter1 = new ReportParameter()
        {
            ReportUIParameter = reportUIParameter,
            Name = "@fromDate",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter1);

        ReportParameter parameter2 = new ReportParameter()
        {
            ReportUIParameter = reportUIParameter,
            Name = "@toDate",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter2);

        ReportParameter parameter3 = new ReportParameter()
        {
            ReportUIParameter = reportUIParameter,
            Name = "@stationID",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter3);

        ReportParameter parameter4 = new ReportParameter()
        {
            ReportUIParameter = reportUIParameter,
            Name = "@ClockID",
            ReportFile = new ReportFile() { ID = reportFileId }
        };
        this.bReportParameter.InsertReportParameter(parameter4);
    }
}
