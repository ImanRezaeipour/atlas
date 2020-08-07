﻿function DialogSendPrivateMessage_onShow(sender, e) {
    var CurrentLangID = parent.CurrentLangID;
    DialogSendPrivateMessage.set_contentUrl(parent.ModulePath + "SendPrivateMessage.aspx");
    document.getElementById(ClientPerfixId + 'DialogSendPrivateMessage_IFrame').style.display = '';
    document.getElementById(ClientPerfixId + 'DialogSendPrivateMessage_IFrame').style.visibility = 'visible';

    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogSendPrivateMessage_topLeftImage').src = parent.ModulePath + 'Images/Dialog/top_right.gif';
        document.getElementById('DialogSendPrivateMessage_topRightImage').src = parent.ModulePath + 'Images/Dialog/top_left.gif';
        document.getElementById('DialogSendPrivateMessage_downLeftImage').src = parent.ModulePath + 'Images/Dialog/down_right.gif';
        document.getElementById('DialogSendPrivateMessage_downRightImage').src = parent.ModulePath + 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogSendPrivateMessage').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogSendPrivateMessage').align = 'right';
    ChangeStyle_DialogSendPrivateMessage();
}

function DialogSendPrivateMessage_onClose(sender, e) {
    document.getElementById(ClientPerfixId + 'DialogSendPrivateMessage_IFrame').style.display = 'none';
    document.getElementById(ClientPerfixId + 'DialogSendPrivateMessage_IFrame').style.visibility = 'hidden';
    DialogSendPrivateMessage.set_contentUrl("about:blank");
}
function ChangeStyle_DialogSendPrivateMessage() {
    var ObjDialogSendPrivateMessage = DialogSendPrivateMessage.get_value();
    var State = ObjDialogSendPrivateMessage.State;
    var height = null;
    switch (State) {
        case 'Direct':
            height = '560px';
            break;
        case 'Reply':
            height = '260px';
            break;
    }
    document.getElementById(ClientPerfixId + 'DialogSendPrivateMessage_IFrame').style.height = height;
}