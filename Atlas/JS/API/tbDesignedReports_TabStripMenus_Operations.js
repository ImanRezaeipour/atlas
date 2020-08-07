
var CurrentPageState_DesignedReports = 'View';
var ObjReport_DesignedReports = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ConfirmState_DesignedReports = null;
var SelectedReportType_DesignedReports = null;
var SelectedGroupNode_DesignedReports = null;
var SelectedDateParameterType_DesignedReports=null;
function FocusOnFirstElement_DesignedReports() {
    document.getElementById('cmbReportTypes_DesignedReports').focus();
}
function tlbItemNew_TlbDesignedReports_onClick() {
    ChangePageState_DesignedReports('Add');
    ClearList_DesignedReports();
    FocusOnFirstElement_DesignedReports();
}

function tlbItemEdit_TlbDesignedReports_onClick() {
    ChangePageState_DesignedReports('Edit');
    FocusOnFirstElement_DesignedReports();
}

function tlbItemDelete_TlbDesignedReports_onClick() {
    ChangePageState_DesignedReports('Delete');
}

function tlbItemSave_TlbDesignedReports_onClick() {
    CollapseControls_DesignedReports();
    Report_onSave();
}

function tlbItemCancel_TlbDesignedReports_onClick() {
    ChangePageState_DesignedReports('View');
    ClearList_DesignedReports();
}

function tlbItemExit_TlbDesignedReports_onClick() {
    ShowDialogConfirm('Exit');
}
function tlbItemSelectColumn_TlbDesignedReports_onClick()
{
    ShowDialogDesignedReportsSelectColumn_DesignedReports();
}
function ShowDialogConfirm(confirmState) {
    ConfirmState_DesignedReports = confirmState;
    if (CurrentPageState_DesignedReports == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_DesignedReports').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_DesignedReports').value;
    DialogConfirm.Show();
    CollapseControls_DesignedReports();
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DesignedReports('View');
}
function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_DesignedReports) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateReport_DesignedReports();
            break;
        case 'Exit':
            ClearList_DesignedReports();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
            break;
    }
}
function CollapseControls_DesignedReports() {
    cmbReportTypes_DesignedReports.collapse();
    cmbGroupNodes_DesignedReports.collapse();
}
function ClearList_DesignedReports() {
    if (CurrentPageState_DesignedReports != 'Edit') {
        document.getElementById('txtReportTitle_DesignedReports').value = '';
        document.getElementById('txtReportComment_DesignedReports').value = '';
        document.getElementById('cmbReportTypes_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
        cmbReportTypes_DesignedReports.unSelect();
        document.getElementById('cmbGroupNodes_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
        document.getElementById('cmbDateParameterType_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
        cmbDateParameterType_DesignedReports.unSelect();
        cmbGroupNodes_DesignedReports.unSelect();
        cmbDateParameterType_DesignedReports.unSelect();
        GridDesignedReports_DesignedReports.unSelectAll();
        SelectedReportType_DesignedReports = null;
        SelectedGroupNode_DesignedReports = null;
        SelectedDateParameterType_DesignedReports = null;
    }
}
function NavigateReport_DesignedReports(selectedReportItem) {
    if (selectedReportItem != undefined) {
        document.getElementById('txtReportTitle_DesignedReports').value = selectedReportItem.getMember('Name').get_text();
        document.getElementById('txtReportComment_DesignedReports').value = selectedReportItem.getMember('Description').get_text();
        SelectedReportType_DesignedReports = new Object();
        SelectedReportType_DesignedReports.ID = selectedReportItem.getMember('DesignedType.ID').get_text();
        SelectedReportType_DesignedReports.CustomCode = selectedReportItem.getMember('DesignedType.CustomCode').get_text();
        document.getElementById('cmbReportTypes_DesignedReports_Input').value = SelectedReportType_DesignedReports.Name = selectedReportItem.getMember('DesignedType.Name').get_text();
        SelectedGroupNode_DesignedReports = new Object();
        SelectedGroupNode_DesignedReports.ID = selectedReportItem.getMember('ParentReport.ID').get_text();
        document.getElementById('cmbGroupNodes_DesignedReports_Input').value = SelectedGroupNode_DesignedReports.Name = selectedReportItem.getMember('ParentReport.Name').get_text();
        
        SelectedDateParameterType_DesignedReports = new Object();
        if (selectedReportItem.getMember('ReportParameterDesigned.ID').get_text() != '0') {
            //SelectedDateParameterType_DesignedReports.ID = '-1';
            //document.getElementById('cmbDateParameterType_DesignedReports_Input').value = SelectedDateParameterType_DesignedReports.Name = "None";
            SelectedDateParameterType_DesignedReports.ID = selectedReportItem.getMember('ReportParameterDesigned.ID').get_text();
            document.getElementById('cmbDateParameterType_DesignedReports_Input').value = SelectedDateParameterType_DesignedReports.Name = selectedReportItem.getMember('ReportParameterDesigned.Title').get_text();
        }
       
    }
}
function ChangePageState_DesignedReports(state) {
    CurrentPageState_DesignedReports = state;
    SetActionMode_DesignedReports(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbDesignedReports.get_items().getItemById('tlbItemNew_TlbDesignedReports') != null) {
            TlbDesignedReports.get_items().getItemById('tlbItemNew_TlbDesignedReports').set_enabled(false);
            TlbDesignedReports.get_items().getItemById('tlbItemNew_TlbDesignedReports').set_imageUrl('add_silver.png');
        }
        if (TlbDesignedReports.get_items().getItemById('tlbItemEdit_TlbDesignedReports') != null) {
            TlbDesignedReports.get_items().getItemById('tlbItemEdit_TlbDesignedReports').set_enabled(false);
            TlbDesignedReports.get_items().getItemById('tlbItemEdit_TlbDesignedReports').set_imageUrl('edit_silver.png');
        }
        if (TlbDesignedReports.get_items().getItemById('tlbItemDelete_TlbDesignedReports') != null) {
            TlbDesignedReports.get_items().getItemById('tlbItemDelete_TlbDesignedReports').set_enabled(false);
            TlbDesignedReports.get_items().getItemById('tlbItemDelete_TlbDesignedReports').set_imageUrl('remove_silver.png');
        }
        TlbDesignedReports.get_items().getItemById('tlbItemSave_TlbDesignedReports').set_enabled(true);
        TlbDesignedReports.get_items().getItemById('tlbItemSave_TlbDesignedReports').set_imageUrl('save.png');
        TlbDesignedReports.get_items().getItemById('tlbItemCancel_TlbDesignedReports').set_enabled(true);
        TlbDesignedReports.get_items().getItemById('tlbItemCancel_TlbDesignedReports').set_imageUrl('cancel.png');
        document.getElementById('txtReportTitle_DesignedReports').disabled = '';
        document.getElementById('txtReportComment_DesignedReports').disabled = '';
        
        
        cmbGroupNodes_DesignedReports.enable();
        cmbDateParameterType_DesignedReports.enable();
        if (state == 'Add')
            cmbReportTypes_DesignedReports.enable();
        if (state == 'Edit')
            NavigateReport_DesignedReports(GridDesignedReports_DesignedReports.getSelectedItems()[0]);
        if (state == 'Delete')
            Report_onSave();
    }
    
    
    if (state == 'View') {
        if (TlbDesignedReports.get_items().getItemById('tlbItemNew_TlbDesignedReports') != null) {
            TlbDesignedReports.get_items().getItemById('tlbItemNew_TlbDesignedReports').set_enabled(true);
            TlbDesignedReports.get_items().getItemById('tlbItemNew_TlbDesignedReports').set_imageUrl('add.png');
        }
        if (TlbDesignedReports.get_items().getItemById('tlbItemEdit_TlbDesignedReports') != null) {
            TlbDesignedReports.get_items().getItemById('tlbItemEdit_TlbDesignedReports').set_enabled(true);
            TlbDesignedReports.get_items().getItemById('tlbItemEdit_TlbDesignedReports').set_imageUrl('edit.png');
        }
        if (TlbDesignedReports.get_items().getItemById('tlbItemDelete_TlbDesignedReports') != null) {
            TlbDesignedReports.get_items().getItemById('tlbItemDelete_TlbDesignedReports').set_enabled(true);
            TlbDesignedReports.get_items().getItemById('tlbItemDelete_TlbDesignedReports').set_imageUrl('remove.png');
        }
        TlbDesignedReports.get_items().getItemById('tlbItemSave_TlbDesignedReports').set_enabled(false);
        TlbDesignedReports.get_items().getItemById('tlbItemSave_TlbDesignedReports').set_imageUrl('save_silver.png');
        TlbDesignedReports.get_items().getItemById('tlbItemCancel_TlbDesignedReports').set_enabled(false);
        TlbDesignedReports.get_items().getItemById('tlbItemCancel_TlbDesignedReports').set_imageUrl('cancel_silver.png');
        document.getElementById('txtReportTitle_DesignedReports').disabled = 'disabled';
        document.getElementById('txtReportComment_DesignedReports').disabled = 'disabled';
        
        cmbReportTypes_DesignedReports.disable();
        cmbGroupNodes_DesignedReports.disable();
        cmbDateParameterType_DesignedReports.disable();
    }
}
function GetBoxesHeaders_DesignedReports() {
    document.getElementById('header_DesignedReportsDetails_DesignedReports').innerHTML = document.getElementById('hfheader_DesignedReportsDetails_DesignedReports').value;
    document.getElementById('header_DesignedReports_DesignedReports').innerHTML = document.getElementById('hfheader_DesignedReports_DesignedReports').value;
}
function SetActionMode_DesignedReports(state) {
    document.getElementById('ActionMode_DesignedReports').innerHTML = document.getElementById("hf" + state + "_DesignedReports").value;
}

function Fill_GridDesignedReports_DesignedReports() {
    document.getElementById('loadingPanel_GridDesignedReports_DesignedReports').innerHTML =GetLoadingMessage(document.getElementById('hfloadingPanel_GridDesignedReports_DesignedReports').value);
    CallBack_GridDesignedReports_DesignedReports.callback();
}
function GridDesignedReports_DesignedReports_onLoad(sender, e)
{
    document.getElementById('loadingPanel_GridDesignedReports_DesignedReports').innerHTML = '';
}
function GridDesignedReports_DesignedReports_onItemSelect(sender, e) {
    if (CurrentPageState_DesignedReports != 'Add')
        NavigateReport_DesignedReports(e.get_item());
}
function CallBack_GridDesignedReports_DesignedReports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DesignedReports').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridDesignedReports_DesignedReports();
    }
}

function CallBack_GridDesignedReports_DesignedReports_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridDesignedReports_DesignedReports').innerHTML = '';
    ShowConnectionError_DesignedReports();
}

function ShowConnectionError_DesignedReports() {
    var error = document.getElementById('hfErrorType_DesignedReports').value;
    var errorBody = document.getElementById('hfConnectionError_DesignedReports').value;
    showDialog(error, errorBody, 'error');
}
function Refresh_GridDesignedReports_DesignedReports() {
    Fill_GridDesignedReports_DesignedReports();

}

function cmbReportTypes_DesignedReports_onExpand(sender, e) {
    if (cmbReportTypes_DesignedReports.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportTypes_DesignedReports == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportTypes_DesignedReports = true;
        Fill_cmbReportTypes_DesignedReports();
    }
}
function Fill_cmbReportTypes_DesignedReports() {
    ComboBox_onBeforeLoadData('cmbReportTypes_DesignedReports');
    CallBack_cmbReportTypes_DesignedReports.callback();
}
function Report_onSave() {
    if (CurrentPageState_DesignedReports != 'Delete')
        UpdateReport_DesignedReports();
    else
        ShowDialogConfirm('Delete');
}
function UpdateReport_DesignedReports() {
    ObjReport_DesignedReports = new Object();
    ObjReport_DesignedReports.ID = '0';
    ObjReport_DesignedReports.Name = null;
    ObjReport_DesignedReports.DesignedTypeID = '0';
    ObjReport_DesignedReports.DesignedTypeName = null;
    ObjReport_DesignedReports.Description = null;
    ObjReport_DesignedReports.ParentReportID = '0';
    ObjReport_DesignedReports.ParentReportName = null;
    ObjReport_DesignedReports.DesignedTypeCustomCode = null;
    ObjReport_DesignedReports.DateParameterID = '0';
    ObjReport_DesignedReports.DateParameterName = null;
    ObjReport_DesignedReports.ParentPath = null;
    var SelectedItems_GridDesignedReports_DesignedReports = GridDesignedReports_DesignedReports.getSelectedItems();
    if (SelectedItems_GridDesignedReports_DesignedReports.length > 0) {
        ObjReport_DesignedReports.ID = SelectedItems_GridDesignedReports_DesignedReports[0].getMember("ID").get_text();
        ObjReport_DesignedReports.ParentPath = SelectedItems_GridDesignedReports_DesignedReports[0].getMember("ParentPath").get_text();
    }

    if (CurrentPageState_DesignedReports != 'Delete') {
        ObjReport_DesignedReports.Name = document.getElementById('txtReportTitle_DesignedReports').value;
        ObjReport_DesignedReports.Description = document.getElementById('txtReportComment_DesignedReports').value;
        if (cmbReportTypes_DesignedReports.getSelectedItem() != undefined) {
            ObjReport_DesignedReports.DesignedTypeID = cmbReportTypes_DesignedReports.getSelectedItem().get_id();
            ObjReport_DesignedReports.DesignedTypeName = cmbReportTypes_DesignedReports.getSelectedItem().get_text();
            ObjReport_DesignedReports.DesignedTypeCustomCode = cmbReportTypes_DesignedReports.getSelectedItem().get_value();
        }
        else {

            if (SelectedReportType_DesignedReports != null) {
                ObjReport_DesignedReports.DesignedTypeID = SelectedReportType_DesignedReports.ID;
                ObjReport_DesignedReports.DesignedTypeName = SelectedReportType_DesignedReports.Name;
                ObjReport_DesignedReports.DesignedTypeCustomCode = SelectedReportType_DesignedReports.CustomCode;
            }
        }
        if (cmbGroupNodes_DesignedReports.getSelectedItem() != undefined) {
            ObjReport_DesignedReports.ParentReportID = cmbGroupNodes_DesignedReports.getSelectedItem().get_value();
            ObjReport_DesignedReports.ParentReportName = cmbGroupNodes_DesignedReports.getSelectedItem().get_text();
        }
        else {
            if (SelectedGroupNode_DesignedReports != null) {
                ObjReport_DesignedReports.ParentReportID = SelectedGroupNode_DesignedReports.ID;
                ObjReport_DesignedReports.ControlStationName = SelectedGroupNode_DesignedReports.Name;
            }
        }
        if (cmbDateParameterType_DesignedReports.getSelectedItem() != undefined) {
            ObjReport_DesignedReports.DateParameterID = cmbDateParameterType_DesignedReports.getSelectedItem().get_value();
            ObjReport_DesignedReports.DateParameterName=cmbDateParameterType_DesignedReports.getSelectedItem().get_text();
        }
        else {
            if (SelectedDateParameterType_DesignedReports != null) {
                ObjReport_DesignedReports.DateParameterID = SelectedDateParameterType_DesignedReports.ID;
                ObjReport_DesignedReports.DateParameterName = SelectedDateParameterType_DesignedReports.Name;
            }
            else {
                
            }
        }
       
    }
    UpdateReport_DesignedReportsPage(CharToKeyCode_DesignedReports(CurrentPageState_DesignedReports), CharToKeyCode_DesignedReports(ObjReport_DesignedReports.ID), CharToKeyCode_DesignedReports(ObjReport_DesignedReports.Name), CharToKeyCode_DesignedReports(ObjReport_DesignedReports.Description), CharToKeyCode_DesignedReports(ObjReport_DesignedReports.DesignedTypeID), CharToKeyCode_DesignedReports(ObjReport_DesignedReports.ParentReportID), CharToKeyCode_DesignedReports(ObjReport_DesignedReports.DateParameterID), CharToKeyCode_DesignedReports(ObjReport_DesignedReports.ParentPath));
    DialogWaiting.Show();
}
function UpdateReport_DesignedReportsPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DesignedReports').value;
            Response[1] = document.getElementById('hfConnectionError_DesignedReports').value;
        }
        showDialog(RetMesage[0], Response[1], RetMesage[2]);
        if (RetMesage[2] == 'success') {
            ClearList_DesignedReports();
            ControlStation_OnAfterUpdate(Response);
            ChangePageState_DesignedReports('View');
        }
        else {
            if (CurrentPageState_DesignedReports == 'Delete')
                ChangePageState_DesignedReports('View');
        }
    }
}
function tlbItemFormReconstruction_TlbDesignedReports_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvDesignedReports_iFrame').src =parent.ModulePath + 'DesignedReports.aspx';
}
function ControlStation_OnAfterUpdate(Response) {
    if (ObjReport_DesignedReports != null) {
        var ReportID = Response[3];
        var ReportName = ObjReport_DesignedReports.Name;
        var ReportDescription = ObjReport_DesignedReports.Description;
        var DesignedTypeID = ObjReport_DesignedReports.DesignedTypeID;
        var DesignedTypeName = ObjReport_DesignedReports.DesignedTypeName;
        var ParentReportID = ObjReport_DesignedReports.ParentReportID;
        var ParentReportName = ObjReport_DesignedReports.ParentReportName;
        var DesignedCustomCode = ObjReport_DesignedReports.DesignedTypeCustomCode;
        var DateParameterID = ObjReport_DesignedReports.DateParameterID;
        var DateParameterName = ObjReport_DesignedReports.DateParameterName;

        var ReportItem = null;
        GridDesignedReports_DesignedReports.beginUpdate();
        switch (CurrentPageState_DesignedReports) {
            case 'Add':
                ReportItem = GridDesignedReports_DesignedReports.get_table().addEmptyRow(GridDesignedReports_DesignedReports.get_recordCount());
                ReportItem.setValue(0, Response[3], false);
                GridDesignedReports_DesignedReports.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridDesignedReports_DesignedReports.selectByKey(Response[3], 0, false);
                ReportItem = GridDesignedReports_DesignedReports.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridDesignedReports_DesignedReports.selectByKey(ObjReport_DesignedReports.ID, 0, false);
                GridDesignedReports_DesignedReports.deleteSelected();
                break;
        }
        if (CurrentPageState_DesignedReports != 'Delete') {
            ReportItem.setValue(0, ReportID, false);
            ReportItem.setValue(1, DesignedTypeName, false);
            ReportItem.setValue(2, ReportName, false);
            ReportItem.setValue(3, ReportDescription, false);
            ReportItem.setValue(4, DesignedTypeID, false);
            ReportItem.setValue(5, ParentReportName, false);
            ReportItem.setValue(6, ParentReportID, false);
            ReportItem.setValue(7, DesignedCustomCode, false);
            ReportItem.setValue(8, DateParameterID, false);
            ReportItem.setValue(9, DateParameterName, false);
            
        }
        GridDesignedReports_DesignedReports.endUpdate();
    }
}
function CharToKeyCode_DesignedReports(str) {
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
function cmbReportTypes_DesignedReports_onCollapse(sender, e) {
    if (cmbReportTypes_DesignedReports.getSelectedItem() == undefined) {
        if (SelectedReportType_DesignedReports != null) {
            if (SelectedReportType_DesignedReports.ID == null || SelectedReportType_DesignedReports.ID == undefined)
                document.getElementById('cmbReportTypes_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
            else {
                if (SelectedReportType_DesignedReports.ID != null && SelectedReportType_DesignedReports.ID != undefined)
                    document.getElementById('cmbReportTypes_DesignedReports_Input').value = SelectedReportType_DesignedReports.Name;
            }
        }
    }
}
function CallBack_cmbReportTypes_DesignedReports_onBeforeCallback(sender, e) {
    cmbReportTypes_DesignedReports.dispose();
}
function CallBack_cmbReportTypes_DesignedReports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ReportTypes').value;
    if (error == "") {
        document.getElementById('cmbReportTypes_DesignedReports_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbReportTypes_DesignedReports_DropImage').mousedown();
        cmbReportTypes_DesignedReports.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbReportTypes_DesignedReports_DropDown').style.display = 'none';
    }
}
function CallBack_cmbReportTypes_DesignedReports_onCallbackError(sender, e) {
    ShowConnectionError_DesignedReports();
}
function CallBcak_cmbGroupNodes_DesignedReports_onBeforeCallback(sender, e) {
    cmbGroupNodes_DesignedReports.dispose();
}
function CallBcak_cmbGroupNodes_DesignedReports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GroupNodes').value;
    if (error == "") {
        document.getElementById('cmbGroupNodes_DesignedReports_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbGroupNodes_DesignedReports_DropImage').mousedown();
        cmbGroupNodes_DesignedReports.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbGroupNodes_DesignedReports_DropDown').style.display = 'none';
    }
}

function CallBcak_cmbDateParameterType_DesignedReports_onCallbackError(sender, e) {
    ShowConnectionError_DesignedReports();
}
function CallBcak_cmbDateParameterType_DesignedReports_onBeforeCallback(sender, e) {
    cmbDateParameterType_DesignedReports.dispose();
}
function CallBcak_cmbDateParameterType_DesignedReports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DateParameterType').value;
    if (error == "") {
        document.getElementById('cmbDateParameterType_DesignedReports_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDateParameterType_DesignedReports_DropImage').mousedown();
        cmbDateParameterType_DesignedReports.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDateParameterType_DesignedReports_DropDown').style.display = 'none';
    }
}
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}
function CallBcak_cmbGroupNodes_DesignedReports_onCallbackError(sender, e) {
    ShowConnectionError_DesignedReports();
}
function ShowConnectionError_Machines() {
    var error = document.getElementById('hfErrorType_Machines').value;
    var errorBody = document.getElementById('hfConnectionError_Machines').value;
    showDialog(error, errorBody, 'error');
}
function cmbGroupNodes_DesignedReports_onExpand(sender, e) {
    if (cmbGroupNodes_DesignedReports.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGroupNodes_DesignedReports == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGroupNodes_DesignedReports = true;
        Fill_cmbGroupNodes_DesignedReports();
    }
}
function Fill_cmbGroupNodes_DesignedReports() {
    ComboBox_onBeforeLoadData('cmbGroupNodes_DesignedReports');
    CallBcak_cmbGroupNodes_DesignedReports.callback();
}
function cmbGroupNodes_DesignedReports_onCollapse(sender, e) {
    if (cmbGroupNodes_DesignedReports.getSelectedItem() == undefined) {
        if (SelectedGroupNode_DesignedReports != null) {
            if (SelectedGroupNode_DesignedReports.ID == null || SelectedGroupNode_DesignedReports.ID == undefined)
                document.getElementById('cmbGroupNodes_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
            else {
                if (SelectedGroupNode_DesignedReports.ID != null && SelectedGroupNode_DesignedReports.ID != undefined)
                    document.getElementById('cmbGroupNodes_DesignedReports_Input').value = SelectedGroupNode_DesignedReports.Name;
            }
        }
    }
}
function cmbDateParameterType_DesignedReports_onExpand(sender, e) {
    if (cmbDateParameterType_DesignedReports.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDateParameterType_DesignedReports == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDateParameterType_DesignedReports = true;
        Fill_cmbDateParameterType_DesignedReports();
    }
}
function Fill_cmbDateParameterType_DesignedReports() {
    ComboBox_onBeforeLoadData('cmbDateParameterType_DesignedReports');
    CallBcak_cmbDateParameterType_DesignedReports.callback();
}
function cmbDateParameterType_DesignedReports_onCollapse(sender, e) {
    if (cmbDateParameterType_DesignedReports.getSelectedItem() == undefined) {
        if (SelectedDateParameterType_DesignedReports != null) {
            if (SelectedDateParameterType_DesignedReports.ID == null || SelectedDateParameterType_DesignedReports.ID == undefined)
                document.getElementById('cmbDateParameterType_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
            else {
                if (SelectedDateParameterType_DesignedReports.ID != null && SelectedDateParameterType_DesignedReports.ID != undefined)
                    document.getElementById('cmbDateParameterType_DesignedReports_Input').value = SelectedDateParameterType_DesignedReports.Name;
            }
        }
    }
}
function ShowDialogDesignedReportsSelectColumn_DesignedReports() {
    var SelectedItems_GridDesignedReports_DesignedReports = GridDesignedReports_DesignedReports.getSelectedItems();
    if (SelectedItems_GridDesignedReports_DesignedReports.length > 0)
    {
        
        
                var ObjReport = new Object();
                ObjReport.ReportID = SelectedItems_GridDesignedReports_DesignedReports[0].getMember('ID').get_text();
                ObjReport.ReportName = SelectedItems_GridDesignedReports_DesignedReports[0].getMember('Name').get_text();
                ObjReport.DesignedReportTypeID = SelectedItems_GridDesignedReports_DesignedReports[0].getMember('DesignedType.ID').get_text();
                ObjReport.DesignedReportTypeName = SelectedItems_GridDesignedReports_DesignedReports[0].getMember('DesignedType.Name').get_text();
                ObjReport.DesignedReportTypeCustomCode = SelectedItems_GridDesignedReports_DesignedReports[0].getMember('DesignedType.CustomCode').get_text();
                parent.DialogDesignedReportsSelectColumn.set_value(ObjReport);
                parent.DialogDesignedReportsSelectColumn.Show();
             
        
        CollapseControls_DesignedReports();
    }
}

function tlbItemHelp_TlbDesignedReports_onClick() {
    LoadHelpPage('tlbItemHelp_TlbDesignedReports');
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

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}
