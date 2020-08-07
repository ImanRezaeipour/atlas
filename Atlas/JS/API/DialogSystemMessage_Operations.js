


function GetBoxesHeaders_SystemMessage() {
    parent.document.getElementById('Title_DialogSystemMessage').innerHTML = document.getElementById('hfTitle_DialogSystemMessage').value;
}

function tlbItemSend_TlbSystemMessage_onClick() {
    var subject = CharToKeyCode_SystemMessage(document.getElementById('txtSubject_SystemMessage').value);
    var message = CharToKeyCode_SystemMessage(document.getElementById('txtMessage_SystemMessage').value);
    if (subject != undefined && subject != null && subject != '' && message != undefined && message != null && message != '') {
        SendSystemMessage_SystemMessagePage(subject, message);
        DialogWaiting.Show();
    }
}

function SendSystemMessage_SystemMessagePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SystemMessage').value;
            Response[1] = document.getElementById('hfConnectionError_SystemMessage').value;
        }
        if (RetMessage[2] == 'success') {
            ClearList_SystemMessage();
            parent.parent.document.getElementById('pgvPrivateMessage_iFrame').contentWindow.RefreshGridPrivateMessageSend_PrivateMessage();
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function ClearList_SystemMessage() {
    document.getElementById('txtSubject_SystemMessage').value = '';
    document.getElementById('txtMessage_SystemMessage').value = '';
}

function CharToKeyCode_SystemMessage(str) {
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

