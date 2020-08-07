
function DialogPersonnelEmployTypes_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogPersonnelEmployTypes.set_contentUrl("PersonnelEmploymentTypes.aspx");
    document.getElementById('DialogPersonnelEmployTypes_IFrame').style.display = '';
    document.getElementById('DialogPersonnelEmployTypes_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelEmployTypes_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelEmployTypes_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelEmployTypes_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelEmployTypes_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelEmployTypes').align = 'left';
        document.getElementById('tbl_DialogPersonnelEmployTypesheader').dir = 'rtl';
        document.getElementById('tbl_DialogPersonnelEmployTypesfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPersonnelEmployTypes').align = 'right';
}

function DialogPersonnelEmployTypes_onClose(sender, e) {
    document.getElementById('DialogPersonnelEmployTypes_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelEmployTypes_IFrame').style.visibility = 'hidden';
    DialogPersonnelEmployTypes.set_contentUrl("about:blank");
}
