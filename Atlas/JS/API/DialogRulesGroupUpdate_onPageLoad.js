
function DialogRulesGroupUpdate_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogRulesGroupUpdate.set_contentUrl(parent.ModulePath + "RulesGroupUpdate.aspx");
    document.getElementById(ClientPerfixId + 'DialogRulesGroupUpdate_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogRulesGroupUpdate_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRulesGroupUpdate_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRulesGroupUpdate_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRulesGroupUpdate_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRulesGroupUpdate_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRulesGroupUpdate').align = 'left';
        document.getElementById('tbl_DialogRulesGroupUpdateheader').dir = 'rtl';
        document.getElementById('tbl_DialogRulesGroupUpdatefooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRulesGroupUpdate').align = 'right';
}

function DialogRulesGroupUpdate_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogRulesGroupUpdate_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogRulesGroupUpdate_IFrame').style.visibility = 'hidden';
    DialogRulesGroupUpdate.set_contentUrl("about:blank");
}

