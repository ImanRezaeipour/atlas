
function DialogPersonnelMultiDateFeatures_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogPersonnelMultiDateFeatures = DialogPersonnelMultiDateFeatures.get_value();
    var Caller = ObjDialogPersonnelMultiDateFeatures.Caller;
    var PageState = ObjDialogPersonnelMultiDateFeatures.PageState;
    DialogPersonnelMultiDateFeatures.set_contentUrl('PersonnelMultiDateFeatures.aspx?Caller=' + CharToKeyCode_PersonnelMultiDateFeatures(Caller) + '&PersonnelState=' + CharToKeyCode_PersonnelMultiDateFeatures(PageState));
    document.getElementById('DialogPersonnelMultiDateFeatures_IFrame').style.display = '';
    document.getElementById('DialogPersonnelMultiDateFeatures_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelMultiDateFeatures_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelMultiDateFeatures_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelMultiDateFeatures_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelMultiDateFeatures_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelMultiDateFeatures').align = 'left';
        document.getElementById('tbl_DialogPersonnelMultiDateFeaturesheader').dir = 'rtl';
        document.getElementById('tbl_DialogPersonnelMultiDateFeaturesfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogPersonnelMultiDateFeatures').align = 'right';
}

function DialogPersonnelMultiDateFeatures_onClose(sender, e) {
    document.getElementById('DialogPersonnelMultiDateFeatures_IFrame').style.display = 'none';
    document.getElementById('DialogPersonnelMultiDateFeatures_IFrame').style.visibility = 'hidden';
    DialogPersonnelMultiDateFeatures.set_contentUrl("about:blank");
}

function CharToKeyCode_PersonnelMultiDateFeatures(str) {
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

