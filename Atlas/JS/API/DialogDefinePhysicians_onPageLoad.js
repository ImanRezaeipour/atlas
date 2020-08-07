function DialogDefinePhysicians_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;

    DialogDefinePhysicians.set_contentUrl(parent.ModulePath + "DefinePhysicians.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById(ClientPerfixId + 'DialogDefinePhysicians_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogDefinePhysicians_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogDefinePhysicians_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogDefinePhysicians_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogDefinePhysicians_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogDefinePhysicians_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogDefinePhysicians').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogDefinePhysicians').align = 'right';
}
function DialogDefinePhysicians_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogDefinePhysicians_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogDefinePhysicians_IFrame').style.visibility = 'hidden';
    DialogDefinePhysicians.set_contentUrl("about:blank");
}
function ChangeStyle_DialogDefinePhysicians() {
    document.getElementById(ClientPerfixId + 'DialogDefinePhysicians_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogDefinePhysicians_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogDefinePhysiciansheader').style.width = document.getElementById('tbl_DialogDefinePhysiciansfooter').style.width = (screen.width - 7).toString() + 'px';
}