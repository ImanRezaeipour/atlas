
var ObjStation_Clock_Date_ReportParameter_Station_Clock_Date_ReportParameter = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var NodesSelectedList_TrvDutyPlace_ReportParameter = '';
var CountSelectedNode_trvDutyPlace_ReportParameter = 0;

function tlbItemRegister_TlbRegister_DutyPlace_ReportParameter_onClick() {
    Register_DutyPlace_ReportParameter();
}
// ارسال اطلاعات پارامتر ها به سرور 
function Register_DutyPlace_ReportParameter() {
    ObjStation_DutyPlace_ReportParameter.DutyPlace = '';
    if (NodesSelectedList_TrvDutyPlace_ReportParameter != '')
        ObjStation_DutyPlace_ReportParameter.DutyPlace = NodesSelectedList_TrvDutyPlace_ReportParameter;
    ObjStation_DutyPlace_ReportParameter.IsContainsSubDuty = document.getElementById('chbSubDutyPlace_ReportParameter').checked.toString();
    Register_DutyPlace_ReportParameterPage(CharToKeyCode_DutyPlace_ReportParameter(ObjStation_DutyPlace_ReportParameter.ReportParameterID), CharToKeyCode_DutyPlace_ReportParameter(ObjStation_DutyPlace_ReportParameter.ReportParameterActionID), CharToKeyCode_DutyPlace_ReportParameter(ObjStation_DutyPlace_ReportParameter.ReportFileID), CharToKeyCode_DutyPlace_ReportParameter(ObjStation_DutyPlace_ReportParameter.DutyPlace), CharToKeyCode_DutyPlace_ReportParameter(ObjStation_DutyPlace_ReportParameter.IsContainsSubDuty));
    DialogWaiting.Show();
}

function Register_DutyPlace_ReportParameterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DutyPlace_ReportParameter').value;
            Response[1] = document.getElementById('hfConnectionError_DutyPlace_ReportParameter').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjStation_DutyPlace_ReportParameter.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_DutyPlace_ReportParameter(str) {
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
//در یافت اطلاعات پارامتر انتخاب شده و نگهداری در آبجکت  برای ارسال مجدد به سرور 
function SetReportParameterObj_DutyPlace_ReportParameter() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjStation_DutyPlace_ReportParameter = new Object();
        ObjStation_DutyPlace_ReportParameter.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjStation_DutyPlace_ReportParameter.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjStation_DutyPlace_ReportParameter.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}
//******************************
function CallBack_cmbDutyPlace_ReportParameter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DutyPlace_ReportParameter').value;
    if (error == "") {
        document.getElementById('DutyPlace_ReportParameter_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbDutyPlace_ReportParameter = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbDutyPlace_ReportParameter = false;
        DutyPlace_ReportParameter.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDutyPlace_ReportParameter_DropDown').style.display = 'none';
    }
}
function cmbDutyPlace_ReportParameter_onExpand(sender, e) {
    if (cmbDutyPlace_ReportParameterr.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDutyPlace_ReportParameter == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDutyPlace_ReportParameter = true;
        Fill_cmbDutyPlace_ReportParameter();
    }
}
function cmbDutyPlace_ReportParameter_onChange(sender, e) {
    CurrentPageCombosCallBcakStateObj.IsChangeOccured_cmbDutyPlace_ReportParameter = true;
}
function Fill_cmbDutyPlace_ReportParameter() {
    CallBack_cmbDutyPlace_ReportParameter.callback();
}

function CallBack_cmbDutyPlace_ReportParameter_onBeforeCallback(sender, e) {
    cmbDutyPlace_ReportParameter.dispose();
}

function CallBack_cmbDutyPlace_ReportParameter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DutyPlace_ReportParameter').value;
    if (error == "") {
        document.getElementById('cmbDutyPlace_ReportParameter_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDutyPlace_ReportParameter_DropImage').mousedown();
        cmbDutyPlace_ReportParameter.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDutyPlace_ReportParameter_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDutyPlace_ReportParameter_onCallbackError(sender, e) {
    ShowConnectionError_DutyPlace_ReportParameter();
}
function trvDutyPlace_ReportParameter_onNodeExpand(sender, e) {
    ChangeDirection_trvDutyPlace_ReportParameter();
}
//*****************************








//**********************************
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}


function Refresh_cmbDutyPlace_ReportParameter() {
    Fill_cmbDutyPlace_ReportParameter();
}

function tlbItemClean_TlbRefresh_cmbDutyPlace_ReportParameter_onClick() {
    document.getElementById('cmbDutyPlace_ReportParameter_Input').value = '';
    cmbDutyPlace_ReportParameter.unSelect();
    trvDutyPlace_ReportParameter.SelectedNode = undefined;
    trvDutyPlace_ReportParameter.unCheckAll();
    NodesSelectedList_TrvDutyPlace_ReportParameter = '';
    CountSelectedNode_trvDutyPlace_ReportParameter = 0;
}

function trvDutyPlace_ReportParameter_onNodeCheckChange(sender, e) {
    var checkedNodeStatus_trvDutyPlace_ReportParameter = e.get_node().Checked;
    if (checkedNodeStatus_trvDutyPlace_ReportParameter) {
        NodesSelectedList_TrvDutyPlace_ReportParameter += '#' + e.get_node().get_id() + '#,';
        CountSelectedNode_trvDutyPlace_ReportParameter += 1;
    }
    else {
        NodesSelectedList_TrvDutyPlace_ReportParameter = NodesSelectedList_TrvDutyPlace_ReportParameter.replace('#' + e.get_node().get_id() + '#', '');
        CountSelectedNode_trvDutyPlace_ReportParameter -= 1;
    }
    if (CountSelectedNode_trvDutyPlace_ReportParameter == 0) {
        cmbDutyPlace_ReportParameter.set_text('');

    } else {
        cmbDutyPlace_ReportParameter.set_text(CountSelectedNode_trvDutyPlace_ReportParameter.toString() + ' ' + document.getElementById('hfCountSelectedNodeHiddenField_TrvDutyPlace_ReportParameter').value);
    }

}


function cmbDutyPlace_ReportParameter_onExpand(sender, e) {


    if (trvDutyPlace_ReportParameter.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDutyPlace_ReportParameter == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDutyPlace_ReportParameter = true;
        Fill_cmbDutyPlace_ReportParameter();
    }


}

function Fill_cmbDutyPlace_ReportParameter() {
    ComboBox_onBeforeLoadData('cmbDutyPlace_ReportParameter');
    CallBack_cmbDutyPlace_ReportParameter.callback();
}

function CallBack_cmbDepartment_GroupFilter_ReportParameters_onBeforeCallback(sender, e) {
    cmbDepartment_GroupFilter_ReportParameters.dispose();
}

function CallBack_cmbDutyPlace_ReportParameter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DutyPlace_ReportParameter').value;
    if (error == "") {
        document.getElementById('cmbDutyPlace_ReportParameter_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDutyPlace_ReportParameter_DropImage').mousedown();
        NodesSelectedList_TrvDutyPlace_ReportParameter = '';
        CountSelectedNode_trvDutyPlace_ReportParameter = 0;
        cmbDutyPlace_ReportParameter.expand();
        ChangeDirection_trvDutyPlace_ReportParameter();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDutyPlace_ReportParameter_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDutyPlace_ReportParameter_onCallbackError(sender, e) {
    ShowConnectionError_DutyPlace_ReportParameter();
}

function ShowConnectionError_DutyPlace_ReportParameter() {
    var error = document.getElementById('hfErrorType_DutyPlace_ReportParameter').value;
    var errorBody = document.getElementById('hfConnectionError_DutyPlace_ReportParameter').value;
    showDialog(error, errorBody, 'error');
}

function ChangeDirection_trvDutyPlace_ReportParameter() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvDutyPlace_ReportParameter').style.direction = 'ltr';
}

function ComboBox_onBeforeLoadData(cmbID) {
    document.getElementById(cmbID).onmouseover = " ";
    document.getElementById(cmbID).onmouseout = " ";
    document.getElementById(cmbID + '_Input').onfocus = " ";
    document.getElementById(cmbID + '_Input').onblur = " ";
    document.getElementById(cmbID + '_Input').onkeydown = " ";
    document.getElementById(cmbID + '_Input').onmouseup = " ";
    document.getElementById(cmbID + '_Input').onmousedown = " ";
    document.getElementById(cmbID + '_DropImage').src = 'Images/ComboBox/ComboBoxLoading.gif';
    eval(cmbID).disable();
}
