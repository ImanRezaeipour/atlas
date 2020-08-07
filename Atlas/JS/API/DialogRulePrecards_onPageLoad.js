function DialogRulePrecards_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogRulePrecards = DialogRulePrecards.get_value();
    DialogRulePrecards.set_contentUrl(parent.ModulePath + "RulePrecards.aspx");
    document.getElementById(ClientPerfixId + 'DialogRulePrecards_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogRulePrecards_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRulePrecards_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRulePrecards_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRulePrecards_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRulePrecards_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRulePrecards').align = 'left';
        document.getElementById('tbl_DialogRulePrecardsheader').dir = 'rtl';
        document.getElementById('tbl_DialogRulePrecardsfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRulePrecards').align = 'right';
}

function DialogRulePrecards_onClose(sender , e) {
    document.getElementById(ClientPerfixId + 'DialogRulePrecards_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogRulePrecards_IFrame').style.visibility = 'hidden';
    DialogRulePrecards.set_contentUrl("about:blank");
}