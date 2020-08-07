function GetBoxesHeaders_DefineIllness() {
    SetTitle_DialogDefineIllness__DefineIllness();

}

function SetTitle_DialogDefineIllness__DefineIllness() {

    var DialogDefineIllnessTitle = null;
    switch (parent.SysLangID) {
        case 'fa-IR':
            DialogDefineIllnessTitle = document.getElementById('hfTitle_DialogDefineIllness').value;
            break;
        case 'en-US':
            DialogDefineIllnessTitle = document.getElementById('hfTitle_DialogDefineIllness').value;
            break;
    }
    parent.parent.document.getElementById('Title_DialogDefineIllness').innerHTML = DialogDefineIllnessTitle;
}
function tlbItemSave_TlbDefineIllness_onClick() {
    CurrentPageState_DefineIllness = 'ADD';
    UpdateIllness_DefineIllness();
}

function CloseDialogDefineIllness() {
    var ObjDialogDefineIllness = parent.DialogDefineIllness.get_value();
    var Caller = ObjDialogDefineIllness.Caller;
    switch (Caller) {
        case 'RequestRegister':
            var State = ObjDialogDefineIllness.State;
            parent.document.getElementById(parent.ClientPerfixId + 'DialogRegisteredRequests_IFrame').contentWindow.document.getElementById('DialogRequestRegister_IFrame').contentWindow.Refresh_cmbIllness_RequestRegister(State);

            break;
        case 'DailyRequestOnAbsence':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.document.getElementById('DialogDailyRequestOnAbsence_IFrame').contentWindow.Refresh_cmbIllness_DailyRequestOnAbsence(State);
            break;
        case 'HourlyRequestOnAbsence':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.document.getElementById('DialogHourlyRequestOnAbsence_IFrame').contentWindow.Refresh_cmbIllness_HourlyRequestOnAbsence(State);
            break;
    }
    parent.DialogDefineIllness.Close();
}

function UpdateIllness_DefineIllness() {
    ObjIllness_DefineIllness = new Object();
    ObjIllness_DefineIllness.ID = '0';
    ObjIllness_DefineIllness.Name = null;
    ObjIllness_DefineIllness.Description = null;
    ObjIllness_DefineIllness.Name = document.getElementById('txtIllnessName_DefineIllness').value;   
    ObjIllness_DefineIllness.Description = document.getElementById('txtIllnessDescription_DefineIllness').value;
    var ObjDialogDefineIllness = parent.DialogDefineIllness.get_value();
    ObjIllness_DefineIllness.Caller = ObjDialogDefineIllness.Caller;
    ObjIllness_DefineIllness.Target = ObjDialogDefineIllness.State;
    ObjIllness_DefineIllness.UserCaller = ObjDialogDefineIllness.UserCaller;
    ObjIllness_DefineIllness.RequestCaller = parent.DialogDefineIllness.get_value().RequestCaller;
    ObjIllness_DefineIllness.RequestLoadState = parent.DialogDefineIllness.get_value().LoadState;

    UpdateIllness_DefineIllnessPage(CharToKeyCode_DefineIllness(CurrentPageState_DefineIllness), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.RequestCaller), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.RequestLoadState), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.ID), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.Name), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.Description), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.Caller), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.Target), CharToKeyCode_DefineIllness(ObjIllness_DefineIllness.UserCaller));
    DialogWaiting.Show();
}
function UpdateIllness_DefineIllnessPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DefineIllness').value;
            Response[1] = document.getElementById('hfConnectionError_DefineIllness').value;
        }
        showDialog(RetMesage[0], Response[1], RetMesage[2]);
        if (RetMesage[2] == 'success') {
            CurrentPageState_DefinePhysicians = 'View';
            CloseDialogDefineIllness();
        }

    }
}

function tlbItemCancel_TlbDefineIllness_onClick() {
    parent.DialogDefineIllness.Close();
}

function CharToKeyCode_DefineIllness(str) {
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