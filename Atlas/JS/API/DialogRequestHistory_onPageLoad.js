
function DialogRequestHistory_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogRequestHistory = eval(ClientPerfixId + 'DialogRequestHistory').get_value();
    var RequestCaller = ObjDialogRequestHistory.RequestCaller;
    var RequestType = ObjDialogRequestHistory.RequestType;
    DialogRequestHistory.set_contentUrl(parent.ModulePath + "RequestHistory.aspx?RequestCaller=" + CharToKeyCode_RequestHistory(RequestCaller) + "&RequestType=" + CharToKeyCode_RequestHistory(RequestType));
    document.getElementById(ClientPerfixId + 'DialogRequestHistory_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogRequestHistory_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRequestHistory_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRequestHistory_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRequestHistory_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRequestHistory_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('tbl_DialogRequestHistoryheader').dir = 'rtl';
        document.getElementById('tbl_DialogRequestHistoryfooter').dir = 'rtl';
        document.getElementById('CloseButton_DialogRequestHistory').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRequestHistory').align = 'right';
    //ChangeStyle_RequestHistory();
}

function DialogRequestHistory_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogRequestHistory_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogRequestHistory_IFrame').style.visibility = 'hidden';
    DialogRequestHistory.set_contentUrl("about:blank");
}
function ChangeStyle_RequestHistory() {
    var ObjDialogRequestHistory = DialogRequestHistory.get_value();
    var Caller = ObjDialogRequestHistory.RequestCaller;
    var height = '330px';
    if (Caller == "Survey" || Caller == "Sentry" || Caller == "RegisteredRequest")
        height = '170px';

    document.getElementById(ClientPerfixId + 'DialogRequestHistory_IFrame').style.height = height;
}
function CharToKeyCode_RequestHistory(str) {
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
