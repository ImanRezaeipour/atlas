function DialogOnlineTraffics_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogOnlineTraffics.set_contentUrl(parent.ModulePath + "OnlineTraffics.aspx");
    document.getElementById(ClientPerfixId + 'DialogOnlineTraffics_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogOnlineTraffics_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogOnlineTraffics_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogOnlineTraffics_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogOnlineTraffics_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogOnlineTraffics_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogOnlineTraffics').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogOnlineTraffics').align = 'right';
    }

    ChangeStyle_DialogOnlineTraffics();
}


function DialogOnlineTraffics_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogOnlineTraffics_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogOnlineTraffics_IFrame').style.visibility = 'hidden';
    DialogOnlineTraffics.set_contentUrl("about:blank");
}

function ChangeStyle_DialogOnlineTraffics() {
    document.getElementById(ClientPerfixId + 'DialogOnlineTraffics_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogOnlineTraffics_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogOnlineTrafficsheader').style.width = document.getElementById('tbl_DialogOnlineTrafficsfooter').style.width = (screen.width - 7).toString() + 'px';
}



