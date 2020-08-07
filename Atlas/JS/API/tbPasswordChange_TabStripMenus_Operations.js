
var ConfirmState_PasswordChange = null;
var OpenWithLoginPage_PasswordChange = '';
function OpenPasswordChangeWithLoginPage_PasswordChange(OpenWithLogin) {
    OpenWithLoginPage_PasswordChange = OpenWithLogin;
}
function GetCurrentUser_PasswordChange() {
    document.getElementById('txtUserName_PasswordChange').value = document.getElementById('hfCurrentUser_PasswordChange').value;
}

function tlbItemEndorsement_TlbPasswordChange_onClick() {
    ShowDialogConfirm('Change');
}

function tlbItemExit_TlbPasswordChange_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PasswordChange) {
        case 'Change':
            DialogConfirm.Close();
            UpdatePassword_PasswordChange();
            break;
        case 'Exit':
            if (OpenWithLoginPage_PasswordChange == '')
                parent.CloseCurrentTabOnTabStripMenus();
            else               
                window.location = "MainPage.aspx";
            break;
    }
}

function UpdatePassword_PasswordChange() {
    var CurrentPassword = EncryptData_PasswordChange(document.getElementById('txtPassword_PasswordChange').value).toString();
    var NewPassword = EncryptData_PasswordChange(document.getElementById('txtNewPassword_PasswordChange').value).toString();
    var NewPasswordRepeat = EncryptData_PasswordChange(document.getElementById('txtNewPasswordRepeat_PasswordChange').value).toString();

    UpdatePassword_PasswordChangePage(CharToKeyCode_PasswordChange(CurrentPassword), CharToKeyCode_PasswordChange(NewPassword), CharToKeyCode_PasswordChange(NewPasswordRepeat));
    DialogWaiting.Show();
}

function UpdatePassword_PasswordChange_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PasswordChange').value;
            Response[1] = document.getElementById('hfConnectionError_PasswordChange').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            if (OpenWithLoginPage_PasswordChange != '')
                window.location = "MainPage.aspx?reloadByPasswordChange=PasswordChangePage";
            ClearList_PasswordChange();
        }

    }
}

function ClearList_PasswordChange() {
    document.getElementById('txtPassword_PasswordChange').value = '';
    document.getElementById('txtNewPassword_PasswordChange').value = '';
    document.getElementById('txtNewPasswordRepeat_PasswordChange').value = '';
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function CharToKeyCode_PasswordChange(str) {
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

function ShowDialogConfirm(confirmState) {
    ConfirmState_PasswordChange = confirmState;
    switch (confirmState) {
        case 'Change':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfChangeMessage_PasswordChange').value;
            break;
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PasswordChange').value;
            break;
    }
    DialogConfirm.Show();
}

function tlbItemFormReconstruction_TlbPasswordChange_onClick() {
    if (OpenWithLoginPage_PasswordChange == '') {
        parent.DialogLoading.Show();
        parent.document.getElementById('pgvPasswordChange_iFrame').src =parent.ModulePath +  'PasswordChange.aspx';
    }
    else
        ClearList_PasswordChange();
}

function tlbItemHelp_TlbPasswordChange_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPasswordChange');
}

function EncryptData_PasswordChange(text) {
    var iv = CryptoJS.enc.Utf8.parse('1234567891234567');
    var key = CryptoJS.enc.Utf8.parse("www.ghadirco.net");
    var encryptedText = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(text), key, {
        keySize: 128 / 8,
        iv: iv,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });
    return encryptedText;
}

