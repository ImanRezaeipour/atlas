
var CurrentPageState_DefinePhysicians = 'View';

function GetBoxesHeaders_DefinePhysicians() {
    SetTitle_DialogDefinePhysicians__DefinePhysicians();

}

function SetTitle_DialogDefinePhysicians__DefinePhysicians() {

    var DialogDefinePhysiciansTitle = null;
    switch (parent.SysLangID) {
        case 'fa-IR':
            DialogDefinePhysiciansTitle = document.getElementById('hfTitle_DialogDefinePhysicians').value;
            break;
        case 'en-US':
            DialogDefinePhysiciansTitle = document.getElementById('hfTitle_DialogDefinePhysicians').value;
            break;
    }
    parent.parent.document.getElementById('Title_DialogDefinePhysicians').innerHTML = DialogDefinePhysiciansTitle;
}
function tlbItemSave_TlbDefinePhysicians_onClick() {
    CurrentPageState_DefinePhysicians = 'ADD';
    UpdatePhysician_DefinePhysicians();
}

function CloseDialogDefinePhysicians(){
    var ObjDialogDefinePhysicians = parent.DialogDefinePhysicians.get_value();
    var Caller = ObjDialogDefinePhysicians.Caller;
    switch (Caller) {
        case 'RequestRegister':
            var State = ObjDialogDefinePhysicians.State;
            parent.document.getElementById(parent.ClientPerfixId + 'DialogRegisteredRequests_IFrame').contentWindow.document.getElementById('DialogRequestRegister_IFrame').contentWindow.Refresh_cmbDoctors_RequestRegister(State);
            break;
        case 'DailyRequestOnAbsence':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.document.getElementById('DialogDailyRequestOnAbsence_IFrame').contentWindow.Refresh_cmbDoctors_DailyRequestOnAbsence(State);
            break;
        case 'HourlyRequestOnAbsence':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.document.getElementById('DialogHourlyRequestOnAbsence_IFrame').contentWindow.Refresh_cmbDoctors_HourlyRequestOnAbsence(State);

            break;
    }
    parent.DialogDefinePhysicians.Close();
}

function UpdatePhysician_DefinePhysicians() {
    ObjPhysicians_DefinePhysicians = new Object();
    ObjPhysicians_DefinePhysicians.ID = '0';
    ObjPhysicians_DefinePhysicians.Name = null;
    ObjPhysicians_DefinePhysicians.Family = null;
    ObjPhysicians_DefinePhysicians.Code = null;
    ObjPhysicians_DefinePhysicians.Proficiency = null;
    ObjPhysicians_DefinePhysicians.Description = null;
    ObjPhysicians_DefinePhysicians.RequestCaller = parent.DialogDefinePhysicians.get_value().RequestCaller;
    ObjPhysicians_DefinePhysicians.RequestLoadState = parent.DialogDefinePhysicians.get_value().LoadState;
    ObjPhysicians_DefinePhysicians.Name = document.getElementById('txtPhysiciansName_DefinePhysicians').value;
    ObjPhysicians_DefinePhysicians.Family = document.getElementById('txtPhysiciansFamily_DefinePhysicians').value;
    ObjPhysicians_DefinePhysicians.Code = document.getElementById('txtPhysiciansCode_DefinePhysicians').value;
    ObjPhysicians_DefinePhysicians.Proficiency = document.getElementById('txtPhysiciansProficiency_DefinePhysicians').value;
    ObjPhysicians_DefinePhysicians.Description = document.getElementById('txtPhysiciansDescription_DefinePhysicians').value;
    var ObjDialogDefinePhysicians = parent.DialogDefinePhysicians.get_value();
    ObjPhysicians_DefinePhysicians.Caller = ObjDialogDefinePhysicians.Caller;
    ObjPhysicians_DefinePhysicians.Target = ObjDialogDefinePhysicians.State;
    ObjPhysicians_DefinePhysicians.UserCaller = ObjDialogDefinePhysicians.UserCaller;
    UpdatePhysician_DefinePhysiciansPage(CharToKeyCode_DefinePhysicians(CurrentPageState_DefinePhysicians), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.RequestCaller), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.RequestLoadState), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.ID), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.Name), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.Family), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.Proficiency), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.Code), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.Description), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.Caller), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.Target), CharToKeyCode_DefinePhysicians(ObjPhysicians_DefinePhysicians.UserCaller));
    DialogWaiting.Show();
}
function UpdatePhysician_DefinePhysiciansPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DefinePhysicians').value;
            Response[1] = document.getElementById('hfConnectionError_DefinePhysicians').value;
        }
        showDialog(RetMesage[0], Response[1], RetMesage[2]);
        if (RetMesage[2] == 'success') {
            CurrentPageState_DefinePhysicians = 'View';
            CloseDialogDefinePhysicians();
        }

    }
}

function tlbItemCancel_TlbDefinePhysicians_onClick() {
    parent.DialogDefinePhysicians.Close();
}

function CharToKeyCode_DefinePhysicians(str) {
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