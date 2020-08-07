

function DialogUserInterfaceAccessLevels_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUserInterfaceAccessLevels.set_contentUrl(parent.ModulePath + "UserInterfaceAccessLevels.aspx");
    document.getElementById(ClientPerfixId + 'DialogUserInterfaceAccessLevels_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogUserInterfaceAccessLevels_IFrame').style.visibility = 'visible';
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUserInterfaceAccessLevels_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUserInterfaceAccessLevels_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUserInterfaceAccessLevels_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUserInterfaceAccessLevels_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUserInterfaceAccessLevels').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogUserInterfaceAccessLevels').align = 'right';
    }
}

function DialogUserInterfaceAccessLevels_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogUserInterfaceAccessLevels_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogUserInterfaceAccessLevels_IFrame').style.visibility = 'hidden';
    DialogUserInterfaceAccessLevels.set_contentUrl("about:blank");
}
