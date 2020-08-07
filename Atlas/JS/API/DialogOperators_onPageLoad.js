

function DialogOperators_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogOperators.set_contentUrl(parent.ModulePath + "Operators.aspx");
    document.getElementById(ClientPerfixId + 'DialogOperators_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogOperators_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogOperators_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogOperators_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogOperators_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogOperators_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogOperators').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogOperators').align = 'right';
}

function DialogOperators_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogOperators_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogOperators_IFrame').style.visibility = 'hidden';
    DialogOperators.set_contentUrl("about:blank");
}
