
function DialogMasterRulesView_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogMasterRulesView.set_contentUrl(parent.ModulePath + "MasterRulesView.aspx");
    document.getElementById(ClientPerfixId + 'DialogMasterRulesView_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogMasterRulesView_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogMasterRulesView_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogMasterRulesView_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogMasterRulesView_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogMasterRulesView_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogMasterRulesView').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogMasterRulesView').align = 'right';
}

function DialogMasterRulesView_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogMasterRulesView_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogMasterRulesView_IFrame').style.visibility = 'hidden';
    DialogMasterRulesView.set_contentUrl("about:blank");
}
