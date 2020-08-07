
var reportAttributes_ReportViewer = null;

function initReportViewer() {    
    var overFlow = null;
    document.title = document.getElementById('hfReportViewerTitle_ReportViewer').value;
    reportAttributes_ReportViewer = document.getElementById('hfReportAttributes_ReportViewer').value;
    if (reportAttributes_ReportViewer != undefined && reportAttributes_ReportViewer != null)
        reportAttributes_ReportViewer = eval('(' + reportAttributes_ReportViewer + ')');
    if (reportAttributes_ReportViewer.IsContainsForm)
        overFlow = 'auto';
    else
        overFlow = 'hidden';
    document.body.style.overflow = overFlow;
}

$(window).load(function () {
    setFullscreenSize('Normal');
    window.CallbackFunction = function CallbackFunction(args, context) {
        SetToolbarState(args);
        var properties = args.split(";");
        var webViewerId = properties[0];
        var reportLoadState = properties[4];
        var pageSrc = properties[8];
        var webViewer = document.getElementById(webViewerId);
        var reportFrame = document.getElementById("webReportFrame_" + webViewerId);
        reportFrame.src = pageSrc;
        $("#webReportFrame_" + webViewerId).load(function () {
            var state = 'Normal';
            switch (reportLoadState) {
                case 'One Page':
                    state = 'OnePage';
                    break;
                case 'Whole Report':
                    state = 'WholeReport';
                    break;
            }
            setFullscreenSize(state);
        });
    };
});

$(window).resize(function () {
    setFullscreenSize('Normal');
});

function setFullscreenSize(state) {
    //reportFrame = document.getElementById('webReportFrame_StiReportViewer');
    //webViewer = document.getElementById('StiReportViewer');
    //scrollFrame = reportFrame.contentWindow.document.getElementById('webScrollFrame_StiReportViewer');
    //if (scrollFrame != null) {
    //    reportFrame.style.width = window.innerWidth + 'px';
    //    reportFrame.style.height = window.innerHeight + 'px';
    //    webViewer.style.width = window.innerWidth + 'px';
    //    webViewer.style.height = window.innerHeight + 'px';
    //    scrollFrame.style.width = (window.innerWidth - 6) + 'px';
    //    scrollFrame.style.height = (window.innerHeight - 44) + 'px';
    //}
    //document.getElementById('Container_StiReportViewer').style.display = '';

    if (!reportAttributes_ReportViewer.IsContainsForm && document.getElementById('webReportFrame_StiReportViewer') != undefined && document.getElementById('webReportFrame_StiReportViewer') != null) {
        var webViewer = document.getElementById('StiReportViewer');
        var reportFrame = document.getElementById('webReportFrame_StiReportViewer');
        var reportTable = document.getElementById('webReportFrame_StiReportViewer').contentWindow.document.getElementById('webReportTable_StiReportViewer');
        var scrollFrame = parent.document.getElementById('MasterReportViewerFrame_MasterReportViewer');
        var width = reportTable.clientWidth + 'px';
        var height = null;
        switch (state) {
            case 'Normal':
            case 'OnePage':
                height = reportTable.clientHeight > webViewer.clientHeight ? webViewer.clientHeight + 'px' : reportTable.clientHeight + 'px';
                break;
            case 'WholeReport':
                height = reportTable.clientHeight > webViewer.clientHeight ? reportTable.clientHeight + 'px' : webViewer.clientHeight + 'px';
                break;
        }
        reportFrame.style.width = width;
        reportFrame.style.height = height;
        webViewer.style.width = width;
        webViewer.style.height = height;
        scrollFrame.style.width = width;
        scrollFrame.style.height = height;
    }
}








