function DialogReportParametersConditions_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogReportParametersConditions.set_contentUrl(parent.ModulePath + "ReportParametersConditions.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById(ClientPerfixId + 'DialogReportParametersConditions_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogReportParametersConditions_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogReportParametersConditions_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogReportParametersConditions_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogReportParametersConditions_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogReportParametersConditions_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogReportParametersConditions').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogReportParametersConditions').align = 'right';
}
function DialogReportParametersConditions_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogReportParametersConditions_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogReportParametersConditions_IFrame').style.visibility = 'hidden';
    DialogReportParametersConditions.set_contentUrl("about:blank");
}
function ChangeStyle_DialogReportParametersConditions() {
    document.getElementById(ClientPerfixId + 'DialogReportParametersConditions_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogReportParametersConditions_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogReportParametersConditionsheader').style.width = document.getElementById('tbl_DialogReportParametersConditionsfooter').style.width = (screen.width - 7).toString() + 'px';
}