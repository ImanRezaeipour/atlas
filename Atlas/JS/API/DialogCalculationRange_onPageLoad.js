
function DialogCalculationRange_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    DialogCalculationRange.set_contentUrl(parent.ModulePath + "CalculationRange.aspx");
    document.getElementById(ClientPerfixId + 'DialogCalculationRange_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogCalculationRange_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogCalculationRange_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogCalculationRange_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogCalculationRange_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogCalculationRange_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogCalculationRange').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogCalculationRange').align = 'right';
}

function DialogCalculationRange_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogCalculationRange_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogCalculationRange_IFrame').style.visibility = 'hidden';
    DialogCalculationRange.set_contentUrl("about:blank");
}
