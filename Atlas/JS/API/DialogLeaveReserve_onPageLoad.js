


function DialogLeaveReserve_onShow(sender, e) {
    DialogLeaveReserve.set_contentUrl(parent.ModulePath + "LeaveReserve.aspx");
    document.getElementById(ClientPerfixId + 'DialogLeaveReserve_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogLeaveReserve_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogLeaveReserve_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogLeaveReserve_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogLeaveReserve_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogLeaveReserve_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogLeaveReserve').align = 'left';
    }
    if (CurrentLangID == 'en-US') 
        document.getElementById('CloseButton_DialogLeaveReserve').align = 'right';
}

function DialogLeaveReserve_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogLeaveReserve_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogLeaveReserve_IFrame').style.visibility = 'hidden';
    DialogLeaveReserve.set_contentUrl("about:blank");
}
