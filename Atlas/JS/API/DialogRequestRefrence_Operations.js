var zeroTime = '00';
NullTime_RequestRefrence = '';

function GetBoxesHeaders_RequestRefrence() {
    parent.document.getElementById('Title_DialogRequestRefrence').innerHTML = document.getElementById('hfTitle_DialogRequestRefrence').value;
    document.getElementById('header_RequestRefrence_RequestRefrence').innerHTML = document.getElementById('hfheader_RequestRefrence_RequestRefrence').value;
}

function GridRequestRefrence_RequestRefrence_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRequestRefrence_RequestRefrence').innerHTML = '';
}

function CallBack_GridRequestRefrence_RequestRefrence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Refrence_RequestRefrence').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRequestRefrence_RequestRefrence();
    }
}

function CallBack_GridRequestRefrence_RequestRefrence_onCallbackError(sender, e) {
    ShowConnectionError_RequestRefrence();
}

function Fill_GridRequestRefrence_RequestRefrence() {
    document.getElementById('loadingPanel_GridRequestRefrence_RequestRefrence').innerHTML = document.getElementById('hfloadingPanel_GridRequestRefrence_RequestRefrence').value;
    var ObjRequestRefrence = parent.DialogRequestRefrence.get_value();
    var RequestID = ObjRequestRefrence.RequestID;
    var RefrenceType = ObjRequestRefrence.RefrenceType;
    CallBack_GridRequestRefrence_RequestRefrence.callback(CharToKeyCode_RequestRefrence(RequestID), CharToKeyCode_RequestRefrence(RefrenceType));
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

function Refresh_GridRequestRefrence_RequestRefrence() {
    GetRequestRefrence_RequestRefrence();
}

function ShowConnectionError_RequestRefrence() {
    var error = document.getElementById('hfErrorType_RequestRefrence').value;
    var errorBody = document.getElementById('hfConnectionError_RequestRefrence').value;
    showDialog(error, errorBody, 'error');
}

function GetRequestRefrence_RequestRefrence() {
    var ObjRequestRefrence = parent.DialogRequestRefrence.get_value();
    var RequestID = ObjRequestRefrence.RequestID;
    if (RequestID != 0)
        Fill_GridRequestRefrence_RequestRefrence();
}
 
function DialogRequestRefrence_onClose() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogRequestRefrence_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogRequestRefrence').Close();
}

function tlbItemExit_TlbRequest_RequestRefrence_onClick() {
    ShowDialogConfirm();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    DialogKartable_onClose();
    DialogConfirm.Close();
}

function DialogKartable_onClose() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogRequestRefrence_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogRequestRefrence').Close();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RequestRefrence').value;
    DialogConfirm.Show();
}

function DialogConfirm_OnShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tblConfirm_DialogConfirm').style.direction = 'rtl';
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}