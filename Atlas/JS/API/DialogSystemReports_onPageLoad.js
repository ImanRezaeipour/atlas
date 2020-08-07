﻿

function DialogSystemReports_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogSystemReports.set_contentUrl(parent.ModulePath + "SystemReports.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById(ClientPerfixId + 'DialogSystemReports_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogSystemReports_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogSystemReports_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogSystemReports_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogSystemReports_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogSystemReports_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogSystemReports').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogSystemReports').align = 'right';

    ChangeStyle_DialogSystemReports();
}

function DialogSystemReports_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogSystemReports_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogSystemReports_IFrame').style.visibility = 'hidden';
    DialogSystemReports.set_contentUrl("about:blank");
}

function ChangeStyle_DialogSystemReports() {
    document.getElementById(ClientPerfixId + 'DialogSystemReports_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogSystemReports_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogSystemReportsheader').style.width = document.getElementById('tbl_DialogSystemReportsfooter').style.width = (screen.width - 7).toString() + 'px';
}

function CharToKeyCode_SystemReports(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}








