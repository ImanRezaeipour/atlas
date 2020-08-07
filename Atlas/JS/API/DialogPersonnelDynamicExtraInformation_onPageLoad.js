
function DialogPersonnelDynamicExtraInformation_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var DialogPersonnelDynamicExtraInformationValue = DialogPersonnelDynamicExtraInformation.get_value();
    DialogPersonnelDynamicExtraInformation.set_contentUrl("PersonnelDynamicExtraInformation.aspx?PersonnelState=" + CharToKeyCode_PersonnelDynamicExtraInformation(DialogPersonnelDynamicExtraInformationValue.PageState) + "");
    document.getElementById('DialogPersonnelDynamicExtraInformation_IFrame').style.display = '';
    document.getElementById('DialogPersonnelDynamicExtraInformation_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelDynamicExtraInformation_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelDynamicExtraInformation_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelDynamicExtraInformation_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelDynamicExtraInformation_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelDynamicExtraInformation').align = 'left';
        document.getElementById('tbl_DialogPersonnelDynamicExtraInformationheader').dir = 'rtl';
        document.getElementById('tbl_DialogPersonnelDynamicExtraInformationfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPersonnelDynamicExtraInformation').align = 'right';
}

function DialogPersonnelDynamicExtraInformation_onClose(sender, e) {
    document.getElementById('DialogPersonnelDynamicExtraInformation_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelDynamicExtraInformation_IFrame').style.visibility = 'hidden';
    DialogPersonnelDynamicExtraInformation.set_contentUrl("about:blank");
}

function CharToKeyCode_PersonnelDynamicExtraInformation(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}
