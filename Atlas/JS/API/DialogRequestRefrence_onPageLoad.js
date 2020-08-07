
function DialogRequestRefrence_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    var ObjDialogRequestRefrence = DialogRequestRefrence.get_value();
    var RequestCaller = ObjDialogRequestRefrence.RequestCaller;
    var RequestID = ObjDialogRequestRefrence.RequestID;
    var RefrenceType = ObjDialogRequestRefrence.RefrenceType;

    DialogRequestRefrence.set_contentUrl(parent.ModulePath + "RequestRefrence.aspx?RequestCaller=" + CharToKeyCode_RequestRefrence(RequestCaller) + "&RequestID=" + CharToKeyCode_RequestRefrence(RequestID) + "&RefrenceType=" + CharToKeyCode_RequestRefrence(RefrenceType));
    document.getElementById(ClientPerfixId + 'DialogRequestRefrence_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogRequestRefrence_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRequestRefrence_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRequestRefrence_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRequestRefrence_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRequestRefrence_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('tbl_DialogRequestRefrenceheader').dir = 'rtl';
        document.getElementById('tbl_DialogRequestRefrencefooter').dir = 'rtl';
        document.getElementById('CloseButton_DialogRequestRefrence').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogRequestRefrence').align = 'right';
}

function DialogRequestRefrence_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogRequestRefrence_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogRequestRefrence_IFrame').style.visibility = 'hidden';
    DialogRequestRefrence.set_contentUrl("about:blank");
}

function CharToKeyCode_RequestRefrence(str) {
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

