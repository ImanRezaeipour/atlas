function DialogDesignedReportsSelectColumn_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogDesignedReportsSelectColumn.set_contentUrl(parent.ModulePath + "DesignedReportsSelectColumn.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById(ClientPerfixId + 'DialogDesignedReportsSelectColumn_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogDesignedReportsSelectColumn_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogDesignedReportsSelectColumn_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogDesignedReportsSelectColumn_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogDesignedReportsSelectColumn_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogDesignedReportsSelectColumn_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogDesignedReportsSelectColumn').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogDesignedReportsSelectColumn').align = 'right';
}
function DialogDesignedReportsSelectColumn_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogDesignedReportsSelectColumn_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogDesignedReportsSelectColumn_IFrame').style.visibility = 'hidden';
    DialogDesignedReportsSelectColumn.set_contentUrl("about:blank");
}
function ChangeStyle_DialogDesignedReportsSelectColumn() {
    document.getElementById(ClientPerfixId + 'DialogDesignedReportsSelectColumn_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogDesignedReportsSelectColumn_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogDesignedReportsSelectColumnheader').style.width = document.getElementById('tbl_DialogDesignedReportsSelectColumnfooter').style.width = (screen.width - 7).toString() + 'px';
}