

function initMasterReportViewer() {
    var reportViewerAttributes = document.getElementById('hfMasterReportViewerFrame_MasterReportViewer').value;
    if (reportViewerAttributes != undefined && reportViewerAttributes != null) {
        reportViewerAttributes = eval('(' + reportViewerAttributes + ')');
        document.getElementById('MasterReportViewerFrame_MasterReportViewer').src = "ReportViewer.aspx?ReportGUID=" + reportViewerAttributes.ReportGUID + "&ReportTitle=" + reportViewerAttributes.ReportTitle + "&IsDesigned=" + reportViewerAttributes.IsDesigned + "&IsContainsForm=" + reportViewerAttributes.IsContainsForm + "&Width=" + reportViewerAttributes.Width + "";
    }
    document.title = document.getElementById('hfReportViewerTitle_MasterReportViewer').value;
}





