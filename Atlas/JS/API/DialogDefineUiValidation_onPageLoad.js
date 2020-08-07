function DialogDefineUiValidation_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogDefineUiValidation.set_contentUrl(parent.ModulePath + "DefineUiValidation.aspx");
    document.getElementById('DialogDefineUiValidation_IFrame').style.display = '';
    document.getElementById('DialogDefineUiValidation_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogDefineUiValidation_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogDefineUiValidation_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogDefineUiValidation_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogDefineUiValidation_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogDefineUiValidation').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogDefineUiValidation').align = 'right';
    
}

function DialogDefineUiValidation_onClose(sender, e) {
    document.getElementById('DialogDefineUiValidation_IFrame').style.display = 'none';
    document.getElementById('DialogDefineUiValidation_IFrame').style.visibility = 'hidden';
    DialogDefineUiValidation.set_contentUrl("about:blank");
}
