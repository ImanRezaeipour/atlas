
var DialogKartableMode = '';

function DialogKartable_onShow(sender, e) {
    CurrentLangID = parent.CurrentLangID;
    var ObjDialogKartable = eval(ClientPerfixId + 'DialogKartable').get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var Applicant = ObjDialogKartable.Applicant;
    var KeyApplicant = ObjDialogKartable.KeyApplicant;
    var LastRequestDate = ObjDialogKartable.LastRequestDate;
    var contentUrl_DialogKartable = parent.ModulePath + "Kartable.aspx?RequestCaller=" + CharToKeyCode(RequestCaller) + "&Applicant=" + CharToKeyCode(Applicant) + "&KeyApplicant=" + CharToKeyCode(KeyApplicant) + "&LastRequestDate=" + CharToKeyCode(LastRequestDate);
    DialogKartable.set_contentUrl(contentUrl_DialogKartable);
    document.getElementById(ClientPerfixId + 'DialogKartable_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogKartable_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogKartable_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogKartable_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogKartable_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogKartable_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogKartable').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogKartable').align = 'right';

    ChangeStyle_DialogKartable();
}

function DialogKartable_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogKartable_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogKartable_IFrame').style.visibility = 'hidden';
    DialogKartable.set_contentUrl("about:blank");
}

function CharToKeyCode(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

function ChangeStyle_DialogKartable() {
    document.getElementById(ClientPerfixId + 'DialogKartable_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById(ClientPerfixId + 'DialogKartable_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('tbl_DialogKartableheader').style.width = document.getElementById('tbl_DialogKartablefooter').style.width = (screen.width - 7).toString() + 'px';
}


