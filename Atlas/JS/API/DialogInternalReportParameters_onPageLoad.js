

function DialogInternalReportParameters_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogInternalReportParameters = DialogInternalReportParameters.get_value();
    DialogInternalReportParameters.set_contentUrl(ObjDialogInternalReportParameters.Source + "?reload=" + (new Date()).getTime() + "");
    document.getElementById('DialogInternalReportParameters_IFrame').style.display = '';
    document.getElementById('DialogInternalReportParameters_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogInternalReportParameters_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogInternalReportParameters_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogInternalReportParameters_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogInternalReportParameters_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogInternalReportParameters').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogInternalReportParameters').align = 'right';
}

function DialogInternalReportParameters_onClose(sender, e) {
    document.getElementById('DialogInternalReportParameters_IFrame').style.display = 'none';
    document.getElementById('DialogInternalReportParameters_IFrame').style.visibility = 'hidden';
    DialogInternalReportParameters.set_contentUrl("about:blank");
}
