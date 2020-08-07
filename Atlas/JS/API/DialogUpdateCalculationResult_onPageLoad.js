
function DialogUpdateCalculationResult_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogUpdateCalculationResult.set_contentUrl(parent.ModulePath + "UpdateCalculationResult.aspx?reload=" + (new Date()).getTime() + "");
    document.getElementById(ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogUpdateCalculationResult_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogUpdateCalculationResult_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogUpdateCalculationResult_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogUpdateCalculationResult_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogUpdateCalculationResult').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogUpdateCalculationResult').align = 'right';

    ChangeStyle_DialogUpdateCalculationResult();
}

function DialogUpdateCalculationResult_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').style.visibility = 'hidden';
    DialogUpdateCalculationResult.set_contentUrl("about:blank");
}

function ChangeStyle_DialogUpdateCalculationResult() {
    document.getElementById(ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogUpdateCalculationResultheader').style.width = document.getElementById('tbl_DialogUpdateCalculationResultfooter').style.width = (screen.width - 7).toString() + 'px';
}
