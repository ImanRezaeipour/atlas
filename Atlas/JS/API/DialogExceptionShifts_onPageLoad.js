

function DialogExceptionShifts_onShow(sender, e) {
    DialogExceptionShifts.set_contentUrl(parent.ModulePath + "ExceptionShifts.aspx");
    document.getElementById(ClientPerfixId + 'DialogExceptionShifts_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogExceptionShifts_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogExceptionShifts_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogExceptionShifts_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogExceptionShifts_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogExceptionShifts_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogExceptionShifts').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogExceptionShifts').align = 'right';
}

function DialogExceptionShifts_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogExceptionShifts_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogExceptionShifts_IFrame').style.visibility = 'hidden';
    DialogExceptionShifts.set_contentUrl("about:blank");
}


