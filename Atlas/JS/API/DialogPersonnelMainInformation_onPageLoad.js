

function DialogPersonnelMainInformation_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogPersonnelMainInformation.set_contentUrl(parent.ModulePath + "PersonnelMainInformation.aspx");
    document.getElementById(ClientPerfixId + 'DialogPersonnelMainInformation_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogPersonnelMainInformation_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogPersonnelMainInformation_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogPersonnelMainInformation_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogPersonnelMainInformation_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogPersonnelMainInformation_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogPersonnelMainInformation').align = 'left';
    }
    if (CurrentLangID == 'en-US') {
        document.getElementById('CloseButton_DialogPersonnelMainInformation').align = 'right';
    }

    ChangeStyle_DialogPersonnelMainInformation();
}


function DialogPersonnelMainInformation_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogPersonnelMainInformation_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogPersonnelMainInformation_IFrame').style.visibility = 'hidden';
    DialogPersonnelMainInformation.set_contentUrl("about:blank");
}

function ChangeStyle_DialogPersonnelMainInformation() {
    document.getElementById(ClientPerfixId + 'DialogPersonnelMainInformation_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogPersonnelMainInformation_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogPersonnelMainInformationheader').style.width = document.getElementById('tbl_DialogPersonnelMainInformationfooter').style.width = (screen.width - 7).toString() + 'px';
}



