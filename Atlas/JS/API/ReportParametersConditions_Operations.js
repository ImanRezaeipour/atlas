
var CurrentPageCombosCallBcakStateObj = new Object();
var Conditions_ReportParametersConditions = '';

function tlbItemRegister_TlbReportParametersConditions_onClick() {
}

function tlbItemClear_TlbReportParametersConditions_onClick() {
    document.getElementById('txtConditions_ReportParametersConditions').value = '';
    Conditions_ReportParametersConditions = '';
}

function tlbItemHelp_TlbReportParametersConditions_onClick() {
    LoadHelpPage('tlbItemHelp_TlbReportParametersConditions');
}

function tlbItemExit_TlbReportParametersConditions_onClick() {
    CloseDialogReportParametersConditions();
}

function CloseDialogReportParametersConditions() {
    parent.DialogInternalReportParameters.Close();
}

function cmbField_ReportParametersConditions_onExpand(sender, e) {
    if (cmbField_ReportParametersConditions.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbField_ReportParametersConditions == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbField_ReportParametersConditions = true;
        CallBack_cmbField_ReportParametersConditions.callback();
    }
}

function CallBack_cmbField_ReportParametersConditions_onBeforeCallback(sender, e) {
    cmbField_ReportParametersConditions.dispose();
}

function CallBack_cmbField_ReportParametersConditions_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Field_ReportParametersConditions').value;
    if (error == "") {
        document.getElementById('cmbField_ReportParametersConditions_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbField_ReportParametersConditions_DropImage').mousedown();
        cmbField_ReportParametersConditions.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbField_ReportParametersConditions_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbField_ReportParametersConditions_onCallbackError(sender, e) {
    ShowConnectionError_ReportParameters();
}

function ShowConnectionError_ReportParameters() {
    var error = document.getElementById('hfErrorType_ReportParametersConditions').value;
    var errorBody = document.getElementById('hfConnectionError_ReportParametersConditions').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemInsertToConditions_TlbInsertToConditions_ReportParametersConditions_onClick() {
    var SelectedItem_cmbField_ReportParametersConditions = cmbField_ReportParametersConditions.getSelectedItem();
    if (SelectedItem_cmbField_ReportParametersConditions != undefined) {
        document.getElementById('txtConditions_ReportParametersConditions').value += SelectedItem_cmbField_ReportParametersConditions.get_text();
        Conditions_ReportParametersConditions += SelectedItem_cmbField_ReportParametersConditions.get_value();
    }
}

function InsertOperatorInConditions_ReportParametersConditions(id) {
    var tlbItem = TlbReportParametersConditions.get_items().getItemById(id);
    document.getElementById('txtConditions_ReportParametersConditions').value += tlbItem.get_text();
    Conditions_ReportParametersConditions += tlbItem.get_value();
}

function SetReportParameterObj_ReportParametersConditions() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjReportParametersConditions_ReportParametersConditions = new Object();
        ObjReportParametersConditions_ReportParametersConditions.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjReportParametersConditions_ReportParametersConditions.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjReportParametersConditions_ReportParametersConditions.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}


