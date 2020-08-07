var CurrentPageState_ReportParametersConditions = 'Add';
var CurrentPageCombosCallBcakStateObj = new Object();
var Conditions_ReportParametersConditions = '';
var TrafficConditions_ReportParametersConditions = '';
var Orders_ReportParametersConditions = '';
var ObjReportParameters_ReportParametersConditions = null;
var SelectedGroupColumnField_ReportParametersConditions = null;
var LastColumnTypeSaveToCondition='';
function tlbItemRegister_TlbReportParametersConditions_onClick() {
    UpdateCondition_ReportParametersConditions();
}

function tlbItemClear_TlbReportParametersConditions_onClick() {
    document.getElementById('txtConditions_ReportParametersConditions').value = '';
    Conditions_ReportParametersConditions = '';
    TrafficConditions_ReportParametersConditions = '';
}
function tlbItemClear_TlbOrderReportParametersConditions_onClick() {
    document.getElementById('txtOrders_ReportParametersConditions').value = '';
    Orders_ReportParametersConditions = '';
}
function tlbItemHelp_TlbReportParametersConditions_onClick() {
    LoadHelpPage('tlbItemHelp_TlbReportParametersConditions');
}

function tlbItemExit_TlbReportParametersConditions_onClick() {
    CloseDialogReportParametersConditions();
}

function CloseDialogReportParametersConditions() {

    parent.DialogReportParametersConditions.Close();
}
function Fill_txtFieldCondition_ReportParametersConditions() {
    var ObjReport = parent.DialogReportParameters.get_value();
    CallBack_txtConditions_ReportParametersConditions.callback(CharToKeyCode_ReportParametersConditions(ObjReport.ReportID));
}
function Fill_txtFieldOrder_ReportParametersConditions() {
    var ObjReport = parent.DialogReportParameters.get_value();
    CallBack_txtOrders_ReportParametersConditions.callback(CharToKeyCode_ReportParametersConditions(ObjReport.ReportID));
}
function cmbField_ReportParametersConditions_onExpand(sender, e) {
    if (cmbField_ReportParametersConditions.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbField_ReportParametersConditions == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbField_ReportParametersConditions = true;
        Fill_cmbField_ReportParametersConditions();
    }
}
function Fill_cmbField_ReportParametersConditions() {
    ComboBox_onBeforeLoadData('cmbField_ReportParametersConditions');
    var ObjReport = parent.DialogReportParameters.get_value();
    CallBack_cmbField_ReportParametersConditions.callback(CharToKeyCode_ReportParametersConditions(ObjReport.ReportID));
}

function CallBack_cmbField_ReportParametersConditions_onBeforeCallback(sender, e) {
    cmbField_ReportParametersConditions.dispose();
}

function CallBack_cmbField_ReportParametersConditions_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ReportParametersConditions').value;
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
function CallBack_txtConditions_ReportParametersConditions_onBeforeCallback(sender, e) {

}
function CallBack_txtConditions_ReportParametersConditions_onCallbackError(sender, e) {
    ShowConnectionError_ReportParametersConditions();
}
function CallBack_txtConditions_ReportParametersConditions_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_txtConditions_ReportParametersConditions').value;
    if (error == "") {
        Conditions_ReportParametersConditions = document.getElementById('hfConditionValue_ReportParametersConditions').value;
        TrafficConditions_ReportParametersConditions = document.getElementById('hfTrafficConditionValue_ReportParametersConditions').value;
    }
    else {

        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);

    }
}

function CallBack_txtOrders_ReportParametersConditions_onBeforeCallback(sender, e) {

}
function CallBack_txtOrders_ReportParametersConditions_onCallbackError(sender, e) {
    ShowConnectionError_ReportParametersConditions();
}
function CallBack_txtOrders_ReportParametersConditions_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_txtOrders_ReportParametersConditions').value;
    if (error == "") {
        Orders_ReportParametersConditions = document.getElementById('hfOrderValue_ReportParametersConditions').value;
    }
    else {

        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);

    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbField_ReportParametersConditions_onCallbackError(sender, e) {
    ShowConnectionError_ReportParametersConditions();
}

function ShowConnectionError_ReportParametersConditions() {
    var error = document.getElementById('hfErrorType_ReportParametersConditions').value;
    var errorBody = document.getElementById('hfConnectionError_ReportParametersConditions').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemInsertFieldToConditions_TlbInsertFieldToConditions_ReportParametersConditions_onClick() {
    var SelectedItem_cmbField_ReportParametersConditions = cmbField_ReportParametersConditions.getSelectedItem();
    if (SelectedItem_cmbField_ReportParametersConditions != undefined) {
        document.getElementById('txtConditions_ReportParametersConditions').value += ' ' + SelectedItem_cmbField_ReportParametersConditions.get_text();
        if (SelectedItem_cmbField_ReportParametersConditions.get_value() == 'BasicTraffic_Time_Last' || SelectedItem_cmbField_ReportParametersConditions.get_value() == 'BasicTraffic_Time_First' || SelectedItem_cmbField_ReportParametersConditions.get_value() == 'BasicTraffic_Time_All') {
            TrafficConditions_ReportParametersConditions += ' ' + SelectedItem_cmbField_ReportParametersConditions.get_value();
            LastColumnTypeSaveToCondition = 'Traffic';
        }
        else {
            Conditions_ReportParametersConditions += ' ' + SelectedItem_cmbField_ReportParametersConditions.get_value();
            LastColumnTypeSaveToCondition = 'Concept';
        }
    }
}
function tlbItemInsertFieldToOrders_TlbInsertFieldToOrders_ReportParametersConditions_onClick() {
    var SelectedItem_cmbField_ReportParametersConditions = cmbField_ReportParametersConditions.getSelectedItem();
    if (SelectedItem_cmbField_ReportParametersConditions != undefined) {
        document.getElementById('txtOrders_ReportParametersConditions').value += ' ' + SelectedItem_cmbField_ReportParametersConditions.get_text();
        Orders_ReportParametersConditions += ' ' + SelectedItem_cmbField_ReportParametersConditions.get_value();
    }
}
function tlbItemInsertValueToConditions_TlbInsertValueToConditions_ReportParametersConditions_onClick() {
    var SelectedItem_cmbField_ReportParametersConditions = cmbField_ReportParametersConditions.getSelectedItem();
    if (SelectedItem_cmbField_ReportParametersConditions != undefined) {
        document.getElementById('txtConditions_ReportParametersConditions').value += ' ' + document.getElementById('txtFieldValue_ReportParametersConditions').value;
        //Conditions_ReportParametersConditions += ' ' + "'" + document.getElementById('txtFieldValue_ReportParametersConditions').value + "'";

        if (LastColumnTypeSaveToCondition == 'Traffic')
            TrafficConditions_ReportParametersConditions += ' ' + "'" + document.getElementById('txtFieldValue_ReportParametersConditions').value + "'";

        else
            Conditions_ReportParametersConditions += ' ' + "'" + document.getElementById('txtFieldValue_ReportParametersConditions').value + "'";
    }
}
function tlbItemInsertFieldToGrouping_TlbInsertFieldToOrders_ReportParametersConditions_onClick() {
    var SelectedItem_cmbField_ReportParametersConditions = cmbField_ReportParametersConditions.getSelectedItem();
    if (SelectedItem_cmbField_ReportParametersConditions != undefined) {
        document.getElementById('txtOrders_ReportParametersConditions').value += ' ' + SelectedItem_cmbField_ReportParametersConditions.get_text();
        Orders_ReportParametersConditions += ' ' + SelectedItem_cmbField_ReportParametersConditions.get_value();
    }
}
function InsertOperatorInConditions_ReportParametersConditions(tlb, id) {
    var tlbParent = eval(tlb);
    var tlbItem = tlbParent.get_items().getItemById(id);
    var valueItem = $('<div/>').html(tlbItem.get_value()).text();
    document.getElementById('txtConditions_ReportParametersConditions').value += ' ' + $('<div/>').html(tlbItem.get_text()).text();
    var SelectedItem_cmbField_ReportParametersConditions = cmbField_ReportParametersConditions.getSelectedItem();
    if (LastColumnTypeSaveToCondition=='Traffic')
        TrafficConditions_ReportParametersConditions += ' ' + $('<div/>').html(tlbItem.get_value()).text();

        else
    Conditions_ReportParametersConditions += ' ' + $('<div/>').html(tlbItem.get_value()).text();
}
function InsertOperatorInOrders_ReportParametersConditions(tlb, id) {
    var tlbParent = eval(tlb);
    var tlbItem = tlbParent.get_items().getItemById(id);
    var valueItem = $('<div/>').html(tlbItem.get_value()).text();
    document.getElementById('txtOrders_ReportParametersConditions').value += ' ' + $('<div/>').html(tlbItem.get_text()).text();
    Orders_ReportParametersConditions += ' ' + $('<div/>').html(tlbItem.get_value()).text();
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
function UpdateCondition_ReportParametersConditions() {

    var ObjReport = parent.DialogReportParameters.get_value();
    ObjCondition_ReportParametersConditions = new Object();
    ObjCondition_ReportParametersConditions.ID = '0';
    ObjCondition_ReportParametersConditions.ReportID = ObjReport.ReportID;
    ObjCondition_ReportParametersConditions.ConditionText = document.getElementById('txtConditions_ReportParametersConditions').value.toString();
    ObjCondition_ReportParametersConditions.ConditionValue = Conditions_ReportParametersConditions.toString();
    ObjCondition_ReportParametersConditions.OrderText = document.getElementById('txtOrders_ReportParametersConditions').value.toString();
    ObjCondition_ReportParametersConditions.OrderValue = Orders_ReportParametersConditions.toString();
    ObjCondition_ReportParametersConditions.TrafficConditionValue = TrafficConditions_ReportParametersConditions.toString();
    if (document.getElementById('hfConditionID_ReportParametersConditions').value != '')
        ObjCondition_ReportParametersConditions.ID = document.getElementById('hfConditionID_ReportParametersConditions').value;


    UpdateConditions_ReportParametersConditionsPage(CharToKeyCode_ReportParametersConditions(ObjCondition_ReportParametersConditions.ID), CharToKeyCode_ReportParametersConditions(ObjCondition_ReportParametersConditions.ReportID), CharToKeyCode_ReportParametersConditions(ObjCondition_ReportParametersConditions.ConditionText), CharToKeyCode_ReportParametersConditions(ObjCondition_ReportParametersConditions.ConditionValue), CharToKeyCode_ReportParametersConditions(ObjCondition_ReportParametersConditions.OrderText), CharToKeyCode_ReportParametersConditions(ObjCondition_ReportParametersConditions.OrderValue),CharToKeyCode_ReportParametersConditions(ObjCondition_ReportParametersConditions.TrafficConditionValue));
    DialogWaiting.Show();
}

function UpdateConditions_ReportParametersConditionsPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ReportParametersConditions').value;
            Response[1] = document.getElementById('hfConnectionError_ReportParametersConditions').value;
        }
        showDialog(RetMesage[0], Response[1], RetMesage[2]);

    }
}
function CharToKeyCode_ReportParametersConditions(str) {
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

function GridGroupColumns_ReportParametersConditions_onLoad() {
    document.getElementById('loadingPanel_GridGroupColumn_ReportParametersConditions').innerHTML = '';
}

function CallBack_GridGroupColumns_ReportParametersConditions_onCallbackComplete() {
    var error = document.getElementById('ErrorHiddenField_ReportParametersConditions').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridGroupColumn_ReportParametersConditions();
    }
}

function CallBack_GridGroupColumns_ReportParametersConditions_onCallbackError() {
    document.getElementById('loadingPanel_GridGroupColumn_ReportParametersConditions').innerHTML = '';
    ShowConnectionError_ReportParametersConditions();
}
function Fill_GridGroupColumn_ReportParametersConditions() {
    document.getElementById('loadingPanel_GridGroupColumn_ReportParametersConditions').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridGroupColumn_ReportParametersConditions').value);
    var ObjReport = parent.DialogReportParameters.get_value();

    CallBack_GridGroupColumns_ReportParametersConditions.callback(CharToKeyCode_ReportParametersConditions(ObjReport.ReportID));
}
function Refresh_GridGroupColumn_ReportParametersConditions() {
    Fill_GridGroupColumn_ReportParametersConditions();
}
function GetBoxesHeaders_ReportParametersConditions() {
    document.getElementById('header_GroupColumnDetails_ReportParametersConditions').innerHTML = document.getElementById('hfheader_GroupColumnDetails_ReportParametersConditions').value;
    document.getElementById('header_GroupColumn_ReportParametersConditions').innerHTML = document.getElementById('hfheader_GroupColumn_ReportParametersConditions').value;
    document.getElementById('cmbGroupColumnField_ReportParametersConditions_Input').value = document.getElementById('hfcmbAlarm_ReportParametersConditions').value;

}
function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function cmbGroupColumnField_ReportParametersConditions_onExpand() {
    if (cmbGroupColumnField_ReportParametersConditions.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGroupColumnField_ReportParametersConditions == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGroupColumnField_ReportParametersConditions = true;
        Fill_cmbGroupColumnField_ReportParametersConditions();
    }

}
function Fill_cmbGroupColumnField_ReportParametersConditions() {
    ComboBox_onBeforeLoadData('cmbGroupColumnField_ReportParametersConditions');
    var ObjReport = parent.DialogReportParameters.get_value();
    CallBack_cmbGroupColumnField_ReportParametersConditions.callback(CharToKeyCode_ReportParametersConditions(ObjReport.ReportID));
}
function cmbGroupColumnField_ReportParametersConditions_onCollapse() {
    if (cmbGroupColumnField_ReportParametersConditions.getSelectedItem() == undefined) {
        if (SelectedGroupColumnField_ReportParametersConditions != null) {
            if (SelectedGroupColumnField_ReportParametersConditions.ID == null || SelectedGroupColumnField_ReportParametersConditions.ID == undefined)
                document.getElementById('cmbGroupColumnField_ReportParametersConditions_Input').value = document.getElementById('hfcmbAlarm_ReportParametersConditions').value;
            else {
                if (SelectedGroupColumnField_ReportParametersConditions.ID != null && SelectedGroupColumnField_ReportParametersConditions.ID != undefined)
                    document.getElementById('cmbGroupColumnField_ReportParametersConditions_Input').value = SelectedGroupColumnField_ReportParametersConditions.Name;
            }
        }
    }

}

function CallBack_cmbGroupColumnField_ReportParametersConditions_onBeforeCallback() {

    cmbGroupColumnField_ReportParametersConditions.dispose();
}
function CallBack_cmbGroupColumnField_ReportParametersConditions_onCallbackComplete() {
    var error = document.getElementById('ErrorHiddenField_cmbGroupColumn_ReportParametersConditions').value;
    if (error == "") {
        document.getElementById('cmbGroupColumnField_ReportParametersConditions_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbGroupColumnField_ReportParametersConditions_DropImage').mousedown();
        cmbGroupColumnField_ReportParametersConditions.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbGroupColumnField_ReportParametersConditions_DropDown').style.display = 'none';
    }

}
function CallBack_cmbGroupColumnField_ReportParametersConditions_onCallbackError() {
    ShowConnectionError_ReportParametersConditions();
}



function tlbItemDelete_TlbGroupColumn_ReportParametersConditions_onClick() {
    CurrentPageState_ReportParametersConditions = 'Delete';
    CollapseControls_ReportParametersConditions();
    UpdateGroupColumn_ReportParametersConditions();
}

//function ShowDialogConfirm(confirmState) {
//    ConfirmState_ReportParametersConditions = confirmState;
//    if (CurrentPageState_ReportParametersConditions == 'Delete')
//    DialogConfirm.Show();
//    CollapseControls_ReportParametersConditions();
//}
function CollapseControls_ReportParametersConditions() {
    cmbGroupColumnField_ReportParametersConditions.collapse();

}
//function tlbItemOk_TlbOkConfirm_onClick() {
//    switch (ConfirmState_ReportParametersConditions) {
//        case 'Delete':
//            DialogConfirm.Close();
//            UpdateGroupColumn_ReportParametersConditions();
//            break;

//        default:
//    }
//}

function tlbItemSave_TlbGroupColumn_ReportParametersConditions_onClick() {
    CurrentPageState_ReportParametersConditions = 'Add';
    CollapseControls_ReportParametersConditions();
    UpdateGroupColumn_ReportParametersConditions();
    //Column_onSave();
}

//function Column_onSave() {
//    if (CurrentPageState_ReportParametersConditions != 'Delete')
//        UpdateGroupColumn_ReportParametersConditions();
//    else
//        ShowDialogConfirm('Delete');
//}

function UpdateGroupColumn_ReportParametersConditions() {
    ObjGroupColumn_ReportParametersConditions = new Object();
    ObjGroupColumn_ReportParametersConditions.ID = '0';
    ObjGroupColumn_ReportParametersConditions.ReportID = '0';
    ObjGroupColumn_ReportParametersConditions.ColumnID = '0';
    ObjGroupColumn_ReportParametersConditions.Order = '0';
    ObjGroupColumn_ReportParametersConditions.GroupingNewPage = document.getElementById('chbGroupingByNewPage_ReportParametersConditions').checked.toString();
    var SelectedItems_GridGroupColumns_ReportParametersConditions = GridGroupColumns_ReportParametersConditions.getSelectedItems();
    if (SelectedItems_GridGroupColumns_ReportParametersConditions.length > 0) {
        ObjGroupColumn_ReportParametersConditions.ID = SelectedItems_GridGroupColumns_ReportParametersConditions[0].getMember("ID").get_text();
    }




    if (CurrentPageState_ReportParametersConditions != 'Delete') {
        var ObjReport = parent.DialogReportParameters.get_value();
        ObjGroupColumn_ReportParametersConditions.ReportID = ObjReport.ReportID;
        if (cmbGroupColumnField_ReportParametersConditions.getSelectedItem() != undefined) {
            ObjGroupColumn_ReportParametersConditions.ColumnID = cmbGroupColumnField_ReportParametersConditions.getSelectedItem().get_id();
        }



    }
    UpdateGroupColumn_ReportParametersConditionsPage(CharToKeyCode_ReportParametersConditions(CurrentPageState_ReportParametersConditions), CharToKeyCode_ReportParametersConditions(ObjGroupColumn_ReportParametersConditions.ID), CharToKeyCode_ReportParametersConditions(ObjGroupColumn_ReportParametersConditions.ReportID), CharToKeyCode_ReportParametersConditions(ObjGroupColumn_ReportParametersConditions.ColumnID), CharToKeyCode_ReportParametersConditions(ObjGroupColumn_ReportParametersConditions.GroupingNewPage));
    DialogWaiting.Show();
}

function UpdateGroupColumn_ReportParametersConditionsPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ReportParametersConditions').value;
            Response[1] = document.getElementById('hfConnectionError_ReportParametersConditions').value;
        }

        if (RetMesage[2] == 'success') {
            Fill_GridGroupColumn_ReportParametersConditions();
            ClearList_ReportParametersConditions();
        }
        else {
            showDialog(RetMesage[0], Response[1], RetMesage[2]);
        }
    }
}
function ClearList_ReportParametersConditions() {

    document.getElementById('cmbGroupColumnField_ReportParametersConditions_Input').value = document.getElementById('hfcmbAlarm_ReportParametersConditions').value;
    cmbGroupColumnField_ReportParametersConditions.unSelect();
    GridGroupColumns_ReportParametersConditions.unSelectAll();
}

function tlbItemUp_TlbInterAction_ReportParametersConditions_onClick() {
    ChangeItemPriority_GridGroupColumns_ReportParametersConditions('Up');
}

function tlbItemDown_TlbInterAction_ReportParametersConditions_onClick() {
    ChangeItemPriority_GridGroupColumns_ReportParametersConditions('Down');
}

function ChangeItemPriority_GridGroupColumns_ReportParametersConditions(state) {
    if (GridGroupColumns_ReportParametersConditions.getSelectedItems().length > 0) {
        var tempItem = null;
        var targetItem = null;
        var selectedItem = tempItem = GridGroupColumns_ReportParametersConditions.getSelectedItems()[0];
        switch (state) {
            case 'Up':
                if (selectedItem.get_index() > 0) {
                    targetItem = GridGroupColumns_ReportParametersConditions.get_table().getRow(selectedItem.get_index() - 1);
                }
                break;
            case 'Down':
                if (selectedItem.get_index() < 9)
                    targetItem = GridGroupColumns_ReportParametersConditions.get_table().getRow(selectedItem.get_index() + 1);
                break;
        }
        if (targetItem != null) {
            GridGroupColumns_ReportParametersConditions.beginUpdate();
            selectedItem.setValue(0, targetItem.getMember('ID').get_text(), false);
            selectedItem.setValue(1, targetItem.getMember('Column.Name').get_text(), false);
            selectedItem.setValue(2, targetItem.getMember('IsGroupingNewPage').get_text() == 'true' ? true : false, false);
            selectedItem.setValue(3, targetItem.getMember('Column.ID').get_text(), false);
            selectedItem.setValue(4, targetItem.getMember('Report.ID').get_text(), false);
            selectedItem.setValue(5, targetItem.getMember('Person.ID').get_text(), false);
            selectedItem.setValue(6, targetItem.getMember('Order').get_text(), false);


            targetItem.setValue(0, tempItem.getMember('ID').get_text(), false);
            targetItem.setValue(1, tempItem.getMember('Column.Name').get_text(), false);
            targetItem.setValue(2, tempItem.getMember('IsGroupingNewPage').get_text() == 'true' ? true : false, false);
            targetItem.setValue(3, tempItem.getMember('Column.ID').get_text(), false);
            targetItem.setValue(4, tempItem.getMember('Report.ID').get_text(), false);
            targetItem.setValue(5, tempItem.getMember('Person.ID').get_text(), false);
            targetItem.setValue(6, tempItem.getMember('Order').get_text(), false);

            GridGroupColumns_ReportParametersConditions.endUpdate();
            UpdateGridOrder_ReportParametersConditionsPage(CharToKeyCode_ReportParametersConditions(state), CharToKeyCode_ReportParametersConditions((tempItem.get_index() + 1).toString()), CharToKeyCode_ReportParametersConditions(tempItem.getMember('Report.ID').get_text()));
            TlbInterAction_ReportParametersConditions.set_enabled(false);
        }


    }
}
function UpdateGridOrder_ReportParametersConditionsPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ReportParametersConditions').value;
            Response[1] = document.getElementById('hfConnectionError_ReportParametersConditions').value;
        }

        if (RetMesage[2] == 'success') {

        }
        else {

            showDialog(RetMesage[0], Response[1], RetMesage[2]);
            Fill_GridGroupColumn_ReportParametersConditions();
        }
        TlbInterAction_ReportParametersConditions.set_enabled(true);
    }
}