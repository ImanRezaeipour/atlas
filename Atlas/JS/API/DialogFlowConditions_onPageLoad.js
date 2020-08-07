


function DialogFlowConditions_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogFlowConditions =eval(ClientPerfixId +  'DialogFlowConditions').get_value();
    DialogFlowConditions.set_contentUrl(parent.ModulePath + "FlowConditions.aspx");
    document.getElementById(ClientPerfixId + 'DialogFlowConditions_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogFlowConditions_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogFlowConditions_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogFlowConditions_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogFlowConditions_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogFlowConditions_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogFlowConditions').align = 'left';
        document.getElementById('tbl_DialogFlowConditionsheader').dir = 'rtl';
        document.getElementById('tbl_DialogFlowConditionsfooter').dir = 'rtl';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogFlowConditions').align = 'right';
}

function DialogFlowConditions_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogFlowConditions_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogFlowConditions_IFrame').style.visibility = 'hidden';
    DialogFlowConditions.set_contentUrl("about:blank");
}

