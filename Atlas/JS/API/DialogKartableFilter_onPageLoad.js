

function DialogKartableFilter_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogKartableFilter.set_contentUrl(parent.ModulePath + "KartableFilter.aspx");
    document.getElementById('DialogKartableFilter_IFrame').style.display = '';
    document.getElementById('DialogKartableFilter_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogKartableFilter_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogKartableFilter_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogKartableFilter_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogKartableFilter_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogKartableFilter').align = 'left';
        document.getElementById('tbl_DialogKartableFilterheader').dir = 'rtl';
        document.getElementById('tbl_DialogKartableFilterfooter').dir = 'rtl';        
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogKartableFilter').align = 'right';
}

function DialogKartableFilter_onClose(sender, e) {
    document.getElementById('DialogKartableFilter_IFrame').style.display = 'none';
    document.getElementById('DialogKartableFilter_IFrame').style.visibility = 'hidden';
    DialogKartableFilter.set_contentUrl("about:blank");
}
