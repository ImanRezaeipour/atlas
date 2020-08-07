function DialogDefineIllness_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogDefineIllness.set_contentUrl(parent.ModulePath + "DefineIllness.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById(ClientPerfixId + 'DialogDefineIllness_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogDefineIllness_IFrame').style.visibility = 'visible';
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogDefineIllness_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogDefineIllness_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogDefineIllness_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogDefineIllness_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogDefineIllness').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogDefineIllness').align = 'right';
}
function DialogDefineIllness_onClose(sender, e) {

    document.getElementById(ClientPerfixId + 'DialogDefineIllness_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogDefineIllness_IFrame').style.visibility = 'hidden';
    DialogDefineIllness.set_contentUrl("about:blank");
}
function ChangeStyle_DialogDefineIllness() {
    document.getElementById(ClientPerfixId + 'DialogDefineIllness_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogDefineIllness_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogDefineIllnessheader').style.width = document.getElementById('tbl_DialogDefineIllnessfooter').style.width = (screen.width - 7).toString() + 'px';
}