function DialogUiValidationRules_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUiValidationRules.set_contentUrl(parent.ModulePath + "UiValidationRules.aspx");
    document.getElementById(ClientPerfixId + 'DialogUiValidationRules_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogUiValidationRules_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUiValidationRules_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUiValidationRules_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUiValidationRules_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUiValidationRules_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUiValidationRules').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUiValidationRules').align = 'right';
    
}

function DialogUiValidationRules_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogUiValidationRules_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogUiValidationRules_IFrame').style.visibility = 'hidden';
    DialogUiValidationRules.set_contentUrl("about:blank");
}
