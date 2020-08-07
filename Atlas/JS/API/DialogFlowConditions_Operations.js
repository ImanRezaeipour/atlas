
var ConfirmState_FlowConditions = null;
var CurrentPageState_FlowConditions = 'View';
var ObjFlowConditionsList_FlowConditions = [];
var ObjEditingFlowCondition_FlowConditions = null;
var CurrentRowID = null;

function GetBoxesHeaders_FlowConditions() {
    parent.document.getElementById('Title_DialogFlowConditions').innerHTML = document.getElementById('hfTitle_DialogFlowConditions').value;
    document.getElementById('header_FlowConditions_FlowConditions').innerHTML = document.getElementById('hfheader_FlowConditions_FlowConditions').value;
}

function tlbItemSave_TlbFlowConditions_onClick() {
    UpdateFlowCondition_FlowConditions();
}

function UpdateFlowCondition_FlowConditions() {
    var ObjDialogFlowConditions = parent.DialogFlowConditions.get_value();
    var flowID = ObjDialogFlowConditions.FlowID;
    var managerFlowID = ObjDialogFlowConditions.ManagerFlowID;
    var strFlowConditionsList = JSON.stringify(ObjFlowConditionsList_FlowConditions);
    UpdateFlowConditions_FlowConditionsPage(CharToKeyCode_FlowConditions(flowID), CharToKeyCode_FlowConditions(managerFlowID), CharToKeyCode_FlowConditions(strFlowConditionsList));
    DialogWaiting.Show();
}

function UpdateFlowConditions_FlowConditionsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_FlowConditions').value;
            Response[1] = document.getElementById('hfConnectionError_FlowConditions').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            CurrentRowID = null;
            document.getElementById('chbAllPrecardsEndFlow_FlowConditions').checked = false;
            CurrentPageState_FlowConditions = 'View';
            ObjFlowConditionsList_FlowConditions = [];
            Refresh_GridFlowConditions_FlowConditions();
        }
    }
}

function tlbItemHelp_TlbFlowConditions_onClick() {
    LoadHelpPage('tlbItemHelp_TlbFlowConditions');
}

function tlbItemFormReconstruction_TlbFlowConditions_onClick() {
    CloseDialogFlowConditions();
    parent.document.getElementById(parent.ClientPerfixId + 'DialogUnderManagementPersonnel_IFrame').contentWindow.document.getElementById('DialogManagersWorkFlow_IFrame').contentWindow.ShowDialogFlowConditions();
}

function CloseDialogFlowConditions() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogFlowConditions_IFrame').src =parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogFlowConditions').Close();
}

function tlbItemExit_TlbFlowConditions_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_FlowConditions = confirmState;
    if (ConfirmState_FlowConditions == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_FlowConditions').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_FlowConditions').value;
    DialogConfirm.Show();
}

function Fill_GridFlowConditions_FlowConditions() {
    document.getElementById('loadingPanel_GridFlowConditions_FlowConditions').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridFlowConditions_FlowConditions').value);
    var ObjDialogFlowConditions = parent.DialogFlowConditions.get_value();
    var flowID = ObjDialogFlowConditions.FlowID;
    var managerFlowID = ObjDialogFlowConditions.ManagerFlowID;
    CallBack_GridFlowConditions_FlowConditions.callback(CharToKeyCode_FlowConditions(flowID), CharToKeyCode_FlowConditions(managerFlowID));
}

function Refresh_GridFlowConditions_FlowConditions() {
    Fill_GridFlowConditions_FlowConditions();
}

function EditGridFlowConditions_FlowConditions(rowID) {
    CurrentPageState_FlowConditions = 'Edit';
    CurrentRowID = rowID;
    var editItem_GridFlowConditions_FlowConditions = GridFlowConditions_FlowConditions.getItemFromClientId(rowID);
    GridFlowConditions_FlowConditions.beginUpdate();
    editItem_GridFlowConditions_FlowConditions.setValue(4, true, false);
    GridFlowConditions_FlowConditions.endUpdate();
    GridFlowConditions_FlowConditions.edit(editItem_GridFlowConditions_FlowConditions);
    SetTitle_txtConditionValue_FlowConditions(editItem_GridFlowConditions_FlowConditions);
}

function SetTitle_txtConditionValue_FlowConditions(dataItem) {
    if (dataItem.getMember('ValueType').get_text() != null && dataItem.getMember('ValueType').get_text() != '' && dataItem.getMember('ValueType').get_text() != ' ')
        document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_6_txtConditionValue_FlowConditions').title = document.getElementById('hf' + dataItem.getMember('ValueType').get_text() + '_FlowConditions').value;
}

function SetCellTitle_GridFlowConditions_FlowConditions(state) {
    return document.getElementById('hf' + state + '_FlowConditions').value;
}

function cmbConditionOperator_FlowConditions_onChange() {
    var cmbConditionOperator_FlowConditions = document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_5_cmbConditionOperator_FlowConditions');
    if (cmbConditionOperator_FlowConditions != undefined && cmbConditionOperator_FlowConditions != null && cmbConditionOperator_FlowConditions.selectedIndex >= 0) {
        var txtConditionValue_FlowConditions = document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_6_txtConditionValue_FlowConditions');
        var operatorFlowCondition = cmbConditionOperator_FlowConditions.options[cmbConditionOperator_FlowConditions.selectedIndex];
        if (operatorFlowCondition.value == 'Between') {
            if (txtConditionValue_FlowConditions.title.indexOf(',') < 0)
                txtConditionValue_FlowConditions.title = txtConditionValue_FlowConditions.title + ',' + txtConditionValue_FlowConditions.title;
        }
        else {
            if (txtConditionValue_FlowConditions.title.indexOf(',') >= 0)
                txtConditionValue_FlowConditions.title = txtConditionValue_FlowConditions.title.split(',')[0];
        }
    }
}

function GetConditionOperator_GridFlowConditions_FlowConditions(dataItem) {
    var cmbConditionOperator_FlowConditions = document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_5_cmbConditionOperator_FlowConditions');
    if (cmbConditionOperator_FlowConditions != undefined && cmbConditionOperator_FlowConditions != null && cmbConditionOperator_FlowConditions.selectedIndex >= 0) {
        ObjEditingFlowCondition_FlowConditions = new Object();
        ObjEditingFlowCondition_FlowConditions.FlowConditionItem = GridFlowConditions_FlowConditions.getItemFromKey(1, dataItem.getMember('PrecardID').get_text());
        ObjEditingFlowCondition_FlowConditions.OperatorFlowCondition = cmbConditionOperator_FlowConditions.options[cmbConditionOperator_FlowConditions.selectedIndex];
        return ObjEditingFlowCondition_FlowConditions.OperatorFlowCondition.text;
    }
}

function SetConditionOperator_GridFlowConditions_FlowConditions(dataItem) {
    var cmbConditionOperator_FlowConditions = document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_5_cmbConditionOperator_FlowConditions');
    for (var i = 0; i < cmbConditionOperator_FlowConditions.options.length; i++) {
        var cmbItemConditionOperator = cmbConditionOperator_FlowConditions.options[i];
        if (cmbItemConditionOperator.value == dataItem.getMember('OperatorKey').get_text()) {
            $('#GridFlowConditions_FlowConditions_EditTemplate_1_5_cmbConditionOperator_FlowConditions').val(dataItem.getMember('OperatorKey').get_text());
            break;
        }
    }
}

function GetConditionValue_GridFlowConditions_FlowConditions(dataItem) {
    var conditionValue = document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_6_txtConditionValue_FlowConditions').value;
    if (ObjEditingFlowCondition_FlowConditions != null) {
        if (ObjEditingFlowCondition_FlowConditions.OperatorFlowCondition.value != 'Between')
            conditionValue = !isNaN(parseFloat(conditionValue)) ? parseFloat(conditionValue) : 0;
        else {
            if (conditionValue.indexOf(',') < 0 || conditionValue.replace(/[^,]/g, '').length > 1)
                conditionValue = '0,0';
            else {
                var conditionValueParts = conditionValue.split(',');
                conditionValue = '';
                var tempConditionValues = [];
                for (var i = 0; i < conditionValueParts.length; i++) {
                    conditionValueParts[i] = conditionValueParts[i].replace(/ /g, '');
                    tempConditionValues.push(!isNaN(parseFloat(conditionValueParts[i])) ? parseFloat(conditionValueParts[i]) : 0);
                }
                if (tempConditionValues.length == 2) {
                    if (tempConditionValues[1] > tempConditionValues[0]) {
                        conditionValue += tempConditionValues[0] + ',' + tempConditionValues[1];
                    }
                    else
                        conditionValue = '0,0';
                }
            }
        }
        return conditionValue;
    }
}

function SetConditionValue_GridFlowConditions_FlowConditions(dataItem) {
    document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_6_txtConditionValue_FlowConditions').value = dataItem.getMember('Value').get_text();
}

function GetDescription_GridFlowConditions_FlowConditions() {
    return document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_7_txtDescription_FlowConditions').value;
}

function SetDescription_GridFlowConditions_FlowConditions(dataItem) {
    document.getElementById('GridFlowConditions_FlowConditions_EditTemplate_1_7_txtDescription_FlowConditions').value = dataItem.getMember('Description').get_text();
}

function DeleteGridFlowConditions_FlowConditions(rowID) {
    if (parseFloat(GridFlowConditions_FlowConditions.getItemFromClientId(rowID).getMember('PrecardID').get_text()) > 0) {
        var flowConditionItem_GridFlowConditions_FlowConditions = GridFlowConditions_FlowConditions.getItemFromClientId(rowID);
        if (parseFloat(flowConditionItem_GridFlowConditions_FlowConditions.getMember('ID').get_text()) > 0) {
            GridFlowConditions_FlowConditions.select(flowConditionItem_GridFlowConditions_FlowConditions);
            CurrentPageState_FlowConditions = 'Delete';
            UpdateFlowConditionsList_FlowConditions('Delete');
            CurrentRowID = null;
        }
    }
}

function UpdateGridFlowConditions_FlowConditions() {
    GridFlowConditions_FlowConditions.editComplete();
    if (ObjEditingFlowCondition_FlowConditions != null) {
        GridFlowConditions_FlowConditions.beginUpdate();
        ObjEditingFlowCondition_FlowConditions.FlowConditionItem.setValue(10, ObjEditingFlowCondition_FlowConditions.OperatorFlowCondition.value);
        GridFlowConditions_FlowConditions.endUpdate();
    }
    UpdateFlowConditionsList_FlowConditions('Edit');
    CurrentRowID = null;
}

function CancelEditGridFlowConditions_FlowConditions(rowID) {
    GridFlowConditions_FlowConditions.EditCancel();
    CurrentRowID = null;
}

function UpdateFlowConditionsList_FlowConditions(state) {
    if (GridFlowConditions_FlowConditions.getSelectedItems() != null && GridFlowConditions_FlowConditions.getSelectedItems() != undefined && GridFlowConditions_FlowConditions.getSelectedItems().length > 0) {
        JSON.search.trace = true;
        var selectedFlowCondition_GridFlowConditions_FlowConditions = GridFlowConditions_FlowConditions.getSelectedItems()[0];
        var flowConditionSearchResult = JSON.search(ObjFlowConditionsList_FlowConditions, '//*[PrecardID = "' + selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('PrecardID').get_text() + '"]');
        if (flowConditionSearchResult != undefined && flowConditionSearchResult != null && flowConditionSearchResult.length > 0) {
            flowConditionSearchResult[0].State = state;
            flowConditionSearchResult[0].ID = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('ID').get_text();
            flowConditionSearchResult[0].PrecardAccessGroupDetailID = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('PrecardAccessGroupDetailID').get_text();
            flowConditionSearchResult[0].PrecardID = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('PrecardID').get_text();
            flowConditionSearchResult[0].Access = true;
            flowConditionSearchResult[0].EndFlow = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('EndFlow').get_value();
            flowConditionSearchResult[0].OperatorKey = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('OperatorKey').get_text();
            flowConditionSearchResult[0].Value = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('Value').get_text();
            flowConditionSearchResult[0].Description = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('Description').get_text();
        }
        else {
            var ObjFlowCondition_FlowConditions = new Object();
            ObjFlowCondition_FlowConditions.State = state;
            ObjFlowCondition_FlowConditions.ID = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('ID').get_text();
            ObjFlowCondition_FlowConditions.PrecardAccessGroupDetailID = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('PrecardAccessGroupDetailID').get_text();
            ObjFlowCondition_FlowConditions.PrecardID = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('PrecardID').get_text();
            ObjFlowCondition_FlowConditions.Access = true;
            ObjFlowCondition_FlowConditions.EndFlow = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('EndFlow').get_value();
            ObjFlowCondition_FlowConditions.OperatorKey = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('OperatorKey').get_text();
            ObjFlowCondition_FlowConditions.Value = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('Value').get_text();
            ObjFlowCondition_FlowConditions.Description = selectedFlowCondition_GridFlowConditions_FlowConditions.getMember('Description').get_text();
            ObjFlowConditionsList_FlowConditions.push(ObjFlowCondition_FlowConditions);
        }
    }
    ObjEditingFlowCondition_FlowConditions = null;
    GridFlowConditions_FlowConditions_onAfterUpdate(state);
}

function GridFlowConditions_FlowConditions_onAfterUpdate(state) {
    var selectedItem_GridFlowConditions_FlowConditions = GridFlowConditions_FlowConditions.getSelectedItems()[0];
    GridFlowConditions_FlowConditions.beginUpdate();
    selectedItem_GridFlowConditions_FlowConditions.setValue(1, state, false);
    GridFlowConditions_FlowConditions.endUpdate();
}

function SetState_GridKartable_Kartable(state) {
    var stateImageSrc = null;
    switch (state) {
        case 'Edit':
            stateImageSrc = 'Images/ToolBar/edit.png';
            break;
        case 'Delete':
            stateImageSrc = 'Images/ToolBar/remove.png';
            break;
        default:
            stateImageSrc = 'Images/ToolBar/BallClockAqua.png';
    }
    return stateImageSrc;
}

function CharToKeyCode_FlowConditions(str) {
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

function CallBack_GridFlowConditions_FlowConditions_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_FlowConditions_FlowConditions').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridFlowConditions_FlowConditions();
    }
}

function CallBack_GridFlowConditions_FlowConditions_onCallbackError(sender, e) {
    ShowConnectionError_FlowConditions();
}

function ShowConnectionError_FlowConditions() {
    var error = document.getElementById('hfErrorType_FlowConditions').value;
    var errorBody = document.getElementById('hfConnectionError_FlowConditions').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_FlowConditions) {
        case 'Delete':
            UpdateFlowConditionsList_FlowConditions('Delete');
            break;
        case 'Exit':
            CloseDialogFlowConditions();
            break;
    }
    DialogConfirm.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function GridFlowConditions_FlowConditions_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridFlowConditions_FlowConditions').innerHTML = '';
}

function GridFlowConditions_FlowConditions_onItemBeforeCheckChange(sender, e) {
    if (CurrentRowID != null) {
        if (e.get_item().getMember('PrecardID').get_value() != GridFlowConditions_FlowConditions.getItemFromClientId(CurrentRowID).getMember('PrecardID').get_value())
            e.set_cancel(true);
    }
    else
        e.set_cancel(true);
}

function chbAllPrecardsEndFlow_FlowConditions_onclick() {
    var checked = document.getElementById('chbAllPrecardsEndFlow_FlowConditions').checked;
    var parentTable_GridFlowConditions_FlowConditions = GridFlowConditions_FlowConditions.get_table();
    if (parentTable_GridFlowConditions_FlowConditions != undefined && parentTable_GridFlowConditions_FlowConditions != null) {
        for (var i = 0; i < parentTable_GridFlowConditions_FlowConditions.getRowCount() ; i++) {
            var parentItem_GridFlowConditions_FlowConditions = parentTable_GridFlowConditions_FlowConditions.getRow(i);
            if (parentItem_GridFlowConditions_FlowConditions != undefined && parentItem_GridFlowConditions_FlowConditions != null) {
                var childTable_GridFlowConditions_FlowConditions = parentItem_GridFlowConditions_FlowConditions.get_childTable();
                if (childTable_GridFlowConditions_FlowConditions != undefined && childTable_GridFlowConditions_FlowConditions != null) {
                    for (var j = 0; j < childTable_GridFlowConditions_FlowConditions.getRowCount() ; j++) {
                        var childItem_GridFlowConditions_FlowConditions = childTable_GridFlowConditions_FlowConditions.getRow(j);
                        if (childItem_GridFlowConditions_FlowConditions != undefined && childItem_GridFlowConditions_FlowConditions != null) {
                            var state = 'Edit';
                            GridFlowConditions_FlowConditions.beginUpdate();
                            childItem_GridFlowConditions_FlowConditions.setValue(1, state, false);
                            childItem_GridFlowConditions_FlowConditions.setValue(4, checked, false);
                            GridFlowConditions_FlowConditions.endUpdate();
                            JSON.search.trace = true;
                            var flowConditionSearchResult = JSON.search(ObjFlowConditionsList_FlowConditions, '//*[PrecardID = "' + childItem_GridFlowConditions_FlowConditions.getMember('PrecardID').get_text() + '"]');
                            if (flowConditionSearchResult != undefined && flowConditionSearchResult != null && flowConditionSearchResult.length > 0) {
                                flowConditionSearchResult[0].State = state;
                                flowConditionSearchResult[0].EndFlow = checked;
                            }
                            else {
                                var operatorKey = childItem_GridFlowConditions_FlowConditions.getMember('OperatorKey').get_text();
                                var conditionValue = childItem_GridFlowConditions_FlowConditions.getMember('Value').get_text();
                                var ObjFlowCondition_FlowConditions = new Object();
                                ObjFlowCondition_FlowConditions.State = state;
                                ObjFlowCondition_FlowConditions.ID = childItem_GridFlowConditions_FlowConditions.getMember('ID').get_text();
                                ObjFlowCondition_FlowConditions.PrecardAccessGroupDetailID = childItem_GridFlowConditions_FlowConditions.getMember('PrecardAccessGroupDetailID').get_text();
                                ObjFlowCondition_FlowConditions.PrecardID = childItem_GridFlowConditions_FlowConditions.getMember('PrecardID').get_text();
                                ObjFlowCondition_FlowConditions.Access = true;
                                ObjFlowCondition_FlowConditions.EndFlow = checked;
                                ObjFlowCondition_FlowConditions.OperatorKey = operatorKey != undefined && operatorKey != null && operatorKey != '' && operatorKey != ' ' ? operatorKey : 'Equal';
                                ObjFlowCondition_FlowConditions.Value = conditionValue != undefined && conditionValue != null && conditionValue != '' && conditionValue != ' ' ? conditionValue : '0';
                                ObjFlowCondition_FlowConditions.Description = childItem_GridFlowConditions_FlowConditions.getMember('Description').get_text();
                                ObjFlowConditionsList_FlowConditions.push(ObjFlowCondition_FlowConditions);
                            }
                        }
                    }
                }
            }
        }
    }
}




