var CurrentPageCombosCallBcakStateObj = new Object();
var SelectedColumnName_DesignedReportsSelectColumn = null;
var SelectedPersonInfoColumnName_DesignedReportsSelectColumn = null;
var CurrentPageState_DesignedReportsSelectColumn = 'View';
var ObjColumn_DesignedReportsSelectColumn = null;
var ConfirmState_DesignedReportsSelectColumn = null;
var targetItemIdex = null;
var selectedItemIndex = null;
function GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onLoad() {
    document.getElementById('loadingPanel_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn').innerHTML = '';
}

function GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onItemSelect(sender,e)
{
    if (CurrentPageState_DesignedReportsSelectColumn != 'Add')
        NavigateColumn_DesignedReportsSelectColumn(e.get_item());
}
function CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onCallbackComplete()
{
    var error = document.getElementById('ErrorHiddenField_DesignedReportsSelectColumn').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn();
    }
}

function CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onCallbackError()
{
    document.getElementById('loadingPanel_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn').innerHTML = '';
    ShowConnectionError_DesignedReports();
}
function ShowConnectionError_DesignedReportsSelectColumn() {
    var error = document.getElementById('hfErrorType_DesignedReportsSelectColumn').value;
    var errorBody = document.getElementById('hfConnectionError_DesignedReportsSelectColumn').value;
    showDialog(error, errorBody, 'error');

}
function FocusOnFirstElement_DesignedReportsSelectColumn() {
    document.getElementById('cmbColumnName_DesignedReportsSelectColumn').focus();
}
function tlbItemNew_TlbDesignedReportsSelectColumn_onClick()
{


    ChangePageState_DesignedReportsSelectColumn('Add');
    ClearList_DesignedReportsSelectColumn();
    FocusOnFirstElement_DesignedReportsSelectColumn();

}
function tlbItemEdit_TlbDesignedReportsSelectColumn_onClick()
{
    ChangePageState_DesignedReportsSelectColumn('Edit');
  
}

function tlbItemDelete_TlbDesignedReportsSelectColumn_onClick()
{
    ChangePageState_DesignedReportsSelectColumn('Delete');
}

function tlbItemExit_TlbDesignedReportsSelectColumn_onClick()
{
    ShowDialogConfirm('Exit');
}
function ShowDialogConfirm(confirmState) {
    ConfirmState_DesignedReports = confirmState;
    if (CurrentPageState_DesignedReportsSelectColumn == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_DesignedReportsSelectColumn').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_DesignedReportsSelectColumn').value;
    DialogConfirm.Show();
    CollapseControls_DesignedReportsSelectColumn();
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DesignedReportsSelectColumn('View');
}
function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_DesignedReports) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateColumn_DesignedReportsSelectColumn();
            break;
        case 'Exit':
            ClearList_DesignedReportsSelectColumn();
            
            parent.DialogDesignedReportsSelectColumn.Close();
            break;
        default: 
    }
}
function CollapseControls_DesignedReportsSelectColumn() {
    cmbColumnName_DesignedReportsSelectColumn.collapse();
    cmbPersonInfoColumnName_DesignedReportsSelectColumn.collapse();
    
}
function Column_onSave() {
    if (CurrentPageState_DesignedReportsSelectColumn != 'Delete')
        UpdateColumn_DesignedReportsSelectColumn();
    else
        ShowDialogConfirm('Delete');
}
function Refresh_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn()
{
    Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn();
}
function tlbItemCancel_TlbDesignedReportsSelectColumn_onClick()
{
    ChangePageState_DesignedReportsSelectColumn('View');
    ClearList_DesignedReportsSelectColumn();
}
function tlbItemSave_TlbDesignedReportsSelectColumn_onClick()
{
    CollapseControls_DesignedReportsSelectColumn();
    Column_onSave();
}

function ClearList_DesignedReportsSelectColumn() {
    if (CurrentPageState_DesignedReportsSelectColumn != 'Edit') {
        document.getElementById('txtColumnTitle_DesignedReportsSelectColumn').value = '';
        document.getElementById('chbActiveTitle_DesignedReportsSelectColumn').checked = '';
        document.getElementById('cmbColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
        document.getElementById('cmbPersonInfoColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
        cmbColumnName_DesignedReportsSelectColumn.unSelect();
        cmbPersonInfoColumnName_DesignedReportsSelectColumn.unSelect();
        GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.unSelectAll();
        SelectedColumnName_DesignedReportsSelectColumn = null;
        SelectedPersonInfoColumnName_DesignedReportsSelectColumn = null;
        
    }
}
function cmbColumnName_DesignedReportsSelectColumn_onExpand() {
    if (cmbColumnName_DesignedReportsSelectColumn.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbColumnName_DesignedReportsSelectColumn == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbColumnName_DesignedReportsSelectColumn = true;
        Fill_cmbColumnName_DesignedReportsSelectColumn();
    }

}
function Fill_cmbColumnName_DesignedReportsSelectColumn() {
    ComboBox_onBeforeLoadData('cmbColumnName_DesignedReportsSelectColumn');
    var ObjReport = parent.DialogDesignedReportsSelectColumn.get_value();
    var ReportID = ObjReport.ReportID;

    CallBack_cmbColumnName_DesignedReportsSelectColumn.callback(CharToKeyCode_DesignedReportsSelectColumn(ObjReport.DesignedReportTypeCustomCode),CharToKeyCode_DesignedReportsSelectColumn(ReportID));
}
function cmbColumnName_DesignedReportsSelectColumn_onCollapse()
{
    if (cmbColumnName_DesignedReportsSelectColumn.getSelectedItem() == undefined) {
        if (SelectedColumnName_DesignedReportsSelectColumn != null) {
            if (SelectedColumnName_DesignedReportsSelectColumn.ID == null || SelectedColumnName_DesignedReportsSelectColumn.ID == undefined)
                document.getElementById('cmbColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
            else {
                if (SelectedColumnName_DesignedReportsSelectColumn.ID != null && SelectedColumnName_DesignedReportsSelectColumn.ID != undefined)
                    document.getElementById('cmbColumnName_DesignedReportsSelectColumn_Input').value = SelectedColumnName_DesignedReportsSelectColumn.Name;
            }
        }
    }

}
function cmbColumnName_DesignedReportsSelectColumn_onChange()
{
    if (cmbColumnName_DesignedReportsSelectColumn.getSelectedItem() != null && cmbColumnName_DesignedReportsSelectColumn.getSelectedItem() != undefined) {
        cmbPersonInfoColumnName_DesignedReportsSelectColumn.unSelect();
        document.getElementById('cmbPersonInfoColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
        var ConceptName = cmbColumnName_DesignedReportsSelectColumn.getSelectedItem().get_text();
        document.getElementById('txtColumnTitle_DesignedReportsSelectColumn').value = ConceptName;
        var columnObj = eval('(' + cmbColumnName_DesignedReportsSelectColumn.getSelectedItem().get_value() + ')');
        if (columnObj.KeyColumn == "AllTraffic") {
            document.getElementById('tblTrafficColumnCount_DesignedReportsSelectColumn').style.visibility = 'visible';
        }
        else {
            document.getElementById('tblTrafficColumnCount_DesignedReportsSelectColumn').style.visibility = 'hidden';
        }
    }

}
function CallBack_cmbColumnName_DesignedReportsSelectColumn_onBeforeCallback()
{

    cmbColumnName_DesignedReportsSelectColumn.dispose();
}
function CallBack_cmbColumnName_DesignedReportsSelectColumn_onCallbackComplete()
{
    var error = document.getElementById('ErrorHiddenField_ColumnName').value;
    if (error == "") {
        document.getElementById('cmbColumnName_DesignedReportsSelectColumn_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbColumnName_DesignedReportsSelectColumn_DropImage').mousedown();
        cmbColumnName_DesignedReportsSelectColumn.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbColumnName_DesignedReportsSelectColumn_DropDown').style.display = 'none';
    }

}
function CallBack_cmbColumnName_DesignedReportsSelectColumn_onCallbackError() {
    ShowConnectionError_DesignedReportsSelectColumn();
}
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}


function CharToKeyCode_DesignedReportsSelectColumn(str) {
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
function Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn() {
    document.getElementById('loadingPanel_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn').value);
    var ObjReport = parent.DialogDesignedReportsSelectColumn.get_value();

    CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.callback(ObjReport.ReportID);
}
function GetBoxesHeaders_DesignedReportsSelectColumn() {
    SetTitle_DialogDesignedReportsSelectColumn__DesignedReportsSelectColumn_();

}
function SetTitle_DialogDesignedReportsSelectColumn__DesignedReportsSelectColumn_() {
    var ObjReport = parent.DialogDesignedReportsSelectColumn.get_value();
    var ReportName = ObjReport.ReportName;
    var DialogDesignedReportsSelectColumnTitle = null;
    switch (parent.SysLangID) {
        case 'fa-IR':
            DialogDesignedReportsSelectColumnTitle = document.getElementById('hfTitle_DialogDesignedReportsSelectColumn').value + ' ' + ReportName;
            break;
        case 'en-US':
            DialogDesignedReportsSelectColumnTitle = ReportName + ' ' + document.getElementById('hfTitle_DialogDesignedReportsSelectColumn').value;
            break;
    }
    parent.document.getElementById('Title_DialogDesignedReportsSelectColumn').innerHTML = DialogDesignedReportsSelectColumnTitle;
}
function UpdateColumn_DesignedReportsSelectColumn() {
    ObjColumn_DesignedReportsSelectColumn = new Object();
    ObjColumn_DesignedReportsSelectColumn.ID = '0';
    ObjColumn_DesignedReportsSelectColumn.Title = null;
    ObjColumn_DesignedReportsSelectColumn.ReportID = '0';
    ObjColumn_DesignedReportsSelectColumn.ConceptName = null;
    ObjColumn_DesignedReportsSelectColumn.ConceptID = '0';
    ObjColumn_DesignedReportsSelectColumn.Active = null;
    ObjColumn_DesignedReportsSelectColumn.Order = '0';
    ObjColumn_DesignedReportsSelectColumn.IsConcept = 'false';
    ObjColumn_DesignedReportsSelectColumn.IsGroupColumn = 'false';
    ObjColumn_DesignedReportsSelectColumn.ColumnType = null;
    ObjColumn_DesignedReportsSelectColumn.KeyColumn = null;
    ObjColumn_DesignedReportsSelectColumn.TrafficColumnCount = '0';

    var SelectedItems_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn = GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.getSelectedItems();
    if (SelectedItems_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.length > 0)
    {
        ObjColumn_DesignedReportsSelectColumn.ID = SelectedItems_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn[0].getMember("ID").get_text();
        ObjColumn_DesignedReportsSelectColumn.Order = SelectedItems_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn[0].getMember("Order").get_text();
    }
    var ObjReport = parent.DialogDesignedReportsSelectColumn.get_value();
    ObjColumn_DesignedReportsSelectColumn.ReportID = ObjReport.ReportID;
    if (CurrentPageState_DesignedReportsSelectColumn != 'Delete') {

        ObjColumn_DesignedReportsSelectColumn.Title = document.getElementById('txtColumnTitle_DesignedReportsSelectColumn').value;
        
        if (cmbColumnName_DesignedReportsSelectColumn.getSelectedItem() != undefined) {
            ObjColumn_DesignedReportsSelectColumn.ConceptID= cmbColumnName_DesignedReportsSelectColumn.getSelectedItem().get_id();
            ObjColumn_DesignedReportsSelectColumn.ConceptName = cmbColumnName_DesignedReportsSelectColumn.getSelectedItem().get_text();
            ObjColumn_DesignedReportsSelectColumn.IsConcept = true.toString();
            var columnObj = eval('(' + cmbColumnName_DesignedReportsSelectColumn.getSelectedItem().get_value() + ')');
            ObjColumn_DesignedReportsSelectColumn.ColumnType = columnObj.ColumnType;
            ObjColumn_DesignedReportsSelectColumn.KeyColumn = columnObj.KeyColumn;
        }
        else {
            if (SelectedColumnName_DesignedReportsSelectColumn != null) {
                ObjColumn_DesignedReportsSelectColumn.ConceptID= SelectedColumnName_DesignedReportsSelectColumn.ID;
                ObjColumn_DesignedReportsSelectColumn.ConceptName = SelectedColumnName_DesignedReportsSelectColumn.Name;
                ObjColumn_DesignedReportsSelectColumn.IsConcept = true.toString();
                ObjColumn_DesignedReportsSelectColumn.ColumnType = SelectedColumnName_DesignedReportsSelectColumn.ColumnType;
            }
            else {
                if (cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem() != undefined) {
                    ObjColumn_DesignedReportsSelectColumn.ConceptID = cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem().get_id();
                    ObjColumn_DesignedReportsSelectColumn.ConceptName = cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem().get_text();
                    ObjColumn_DesignedReportsSelectColumn.IsConcept = false.toString();
                    ObjColumn_DesignedReportsSelectColumn.ColumnType = cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem().get_value()
                }
                else {
                    if (SelectedPersonInfoColumnName_DesignedReportsSelectColumn != null) {
                        ObjColumn_DesignedReportsSelectColumn.ConceptID = SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ID;
                        ObjColumn_DesignedReportsSelectColumn.ConceptName = SelectedPersonInfoColumnName_DesignedReportsSelectColumn.Name;
                        ObjColumn_DesignedReportsSelectColumn.IsConcept = false.toString();
                        ObjColumn_DesignedReportsSelectColumn.ColumnType = SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ColumnType;

                    }
                }
            }
        }
       
        if (CurrentPageState_DesignedReportsSelectColumn == 'Add')
            ObjColumn_DesignedReportsSelectColumn.Active = true.toString();
        else
            ObjColumn_DesignedReportsSelectColumn.Active = (document.getElementById('chbActiveTitle_DesignedReportsSelectColumn').checked).toString();
        if (document.getElementById('txtTrafficColumnCount_DesignedReportsSelectColumn').value!='')
        ObjColumn_DesignedReportsSelectColumn.TrafficColumnCount = document.getElementById('txtTrafficColumnCount_DesignedReportsSelectColumn').value;

    }
    UpdateColumn_DesignedReportsSelectColumnPage(CharToKeyCode_DesignedReportsSelectColumn(CurrentPageState_DesignedReportsSelectColumn), CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.ID), CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.Title), CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.Active), CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.ReportID), CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.ConceptID), CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.Order), CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.ColumnType),CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.TrafficColumnCount),CharToKeyCode_DesignedReportsSelectColumn(ObjColumn_DesignedReportsSelectColumn.KeyColumn));
    DialogWaiting.Show();
}
function GetBoxesHeaders_DesignedReportsSelectColumn() {
    document.getElementById('header_DesignedReportsSelectColumnDetails_DesignedReportsSelectColumn').innerHTML = document.getElementById('hfheader_DesignedReportsSelectColumnDetails_DesignedReportsSelectColumn').value;
    document.getElementById('header_DesignedReportsSelectColumn_DesignedReportsSelectColumn').innerHTML = document.getElementById('hfheader_DesignedReportsSelectColumn_DesignedReportsSelectColumn').value;
}
function UpdateColumn_DesignedReportsSelectColumnPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DesignedReportsSelectColumn').value;
            Response[1] = document.getElementById('hfConnectionError_DesignedReportsSelectColumn').value;
        }
        
        if (RetMesage[2] == 'success') {
            ClearList_DesignedReportsSelectColumn();
            DesignedReportsSelectColumn_OnAfterUpdate(Response);
            ChangePageState_DesignedReportsSelectColumn('View');
        }
        else {
            if (CurrentPageState_DesignedReportsSelectColumn == 'Delete')
                ChangePageState_DesignedReportsSelectColumn('View');
            showDialog(RetMesage[0], Response[1], RetMesage[2]);
        }
    }
}
function DesignedReportsSelectColumn_OnAfterUpdate(Response) {
    
    Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn();
}
function ClearItem_cmbColumnName_DesignedReportsSelectColumn() {
    if (CurrentPageState_DesignedReports != 'Edit') {
        document.getElementById('txtReportTitle_DesignedReports').value = '';
        document.getElementById('txtReportComment_DesignedReports').value = '';
        document.getElementById('cmbReportTypes_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
        cmbReportTypes_DesignedReports.unSelect();
        document.getElementById('cmbGroupNodes_DesignedReports_Input').value = document.getElementById('hfcmbAlarm_DesignedReports').value;
        cmbGroupNodes_DesignedReports.unSelect();

        GridDesignedReports_DesignedReports.unSelectAll();
        SelectedReportType_DesignedReports = null;
        SelectedGroupNode_DesignedReports = null;
    }
}
function SetActionMode_DesignedReportsSelectColumn(state) {
    document.getElementById('ActionMode_DesignedReportsSelectColumn').innerHTML = document.getElementById("hf" + state + "_DesignedReportsSelectColumn").value;
}
function ChangePageState_DesignedReportsSelectColumn(state) {
    CurrentPageState_DesignedReportsSelectColumn = state;
    SetActionMode_DesignedReportsSelectColumn(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemNew_TlbDesignedReportsSelectColumn') != null) {
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemNew_TlbDesignedReportsSelectColumn').set_enabled(false);
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemNew_TlbDesignedReportsSelectColumn').set_imageUrl('add_silver.png');
        }
        if (TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemEdit_TlbDesignedReportsSelectColumn') != null) {
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemEdit_TlbDesignedReportsSelectColumn').set_enabled(false);
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemEdit_TlbDesignedReportsSelectColumn').set_imageUrl('edit_silver.png');
        }
        if (TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemDelete_TlbDesignedReportsSelectColumn') != null) {
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemDelete_TlbDesignedReportsSelectColumn').set_enabled(false);
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemDelete_TlbDesignedReportsSelectColumn').set_imageUrl('remove_silver.png');
        }
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemSave_TlbDesignedReportsSelectColumn').set_enabled(true);
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemSave_TlbDesignedReportsSelectColumn').set_imageUrl('save.png');
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemCancel_TlbDesignedReportsSelectColumn').set_enabled(true);
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemCancel_TlbDesignedReportsSelectColumn').set_imageUrl('cancel.png');
        document.getElementById('txtColumnTitle_DesignedReportsSelectColumn').disabled = '';
        document.getElementById('chbActiveTitle_DesignedReportsSelectColumn').disabled = '';

        if (state == 'Add') {
            cmbColumnName_DesignedReportsSelectColumn.enable();
            cmbPersonInfoColumnName_DesignedReportsSelectColumn.enable();
        }
        
        if (state == 'Edit')
            NavigateColumn_DesignedReportsSelectColumn(GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.getSelectedItems()[0]);
        if (state == 'Delete')
            Column_onSave();
    }
    if (state == 'View') {
        if (TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemNew_TlbDesignedReportsSelectColumn') != null) {
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemNew_TlbDesignedReportsSelectColumn').set_enabled(true);
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemNew_TlbDesignedReportsSelectColumn').set_imageUrl('add.png');
        }
        if (TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemEdit_TlbDesignedReportsSelectColumn') != null) {
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemEdit_TlbDesignedReportsSelectColumn').set_enabled(true);
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemEdit_TlbDesignedReportsSelectColumn').set_imageUrl('edit.png');
        }
        if (TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemDelete_TlbDesignedReportsSelectColumn') != null) {
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemDelete_TlbDesignedReportsSelectColumn').set_enabled(true);
            TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemDelete_TlbDesignedReportsSelectColumn').set_imageUrl('remove.png');
        }
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemSave_TlbDesignedReportsSelectColumn').set_enabled(false);
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemSave_TlbDesignedReportsSelectColumn').set_imageUrl('save_silver.png');
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemCancel_TlbDesignedReportsSelectColumn').set_enabled(false);
        TlbDesignedReportsSelectColumn.get_items().getItemById('tlbItemCancel_TlbDesignedReportsSelectColumn').set_imageUrl('cancel_silver.png');
        document.getElementById('txtColumnTitle_DesignedReportsSelectColumn').disabled = 'disabled';
        document.getElementById('chbActiveTitle_DesignedReportsSelectColumn').disabled = 'disabled';

        cmbColumnName_DesignedReportsSelectColumn.disable();
        cmbPersonInfoColumnName_DesignedReportsSelectColumn.disable();
        
    }
}
function NavigateColumn_DesignedReportsSelectColumn(selectedColumnItem) {
    if (selectedColumnItem != undefined) {
        document.getElementById('txtColumnTitle_DesignedReportsSelectColumn').value = selectedColumnItem.getMember('Title').get_text();
        if(selectedColumnItem.getMember('Active').get_text()=='false')
            document.getElementById('chbActiveTitle_DesignedReportsSelectColumn').checked = false;
        else
            document.getElementById('chbActiveTitle_DesignedReportsSelectColumn').checked = true;
      
        if (selectedColumnItem.getMember('ColumnType').get_value() == "Concept" || selectedColumnItem.getMember('ColumnType').get_value() == "Traffic") {
            SelectedColumnName_DesignedReportsSelectColumn = new Object();
            if (selectedColumnItem.getMember('ColumnType').get_value() == "Traffic")
                SelectedColumnName_DesignedReportsSelectColumn.ID = selectedColumnItem.getMember('TrafficID').get_text();
            else if (selectedColumnItem.getMember('ColumnType').get_value() == "Concept")
                  SelectedColumnName_DesignedReportsSelectColumn.ID = selectedColumnItem.getMember('ConceptID').get_text();
            SelectedColumnName_DesignedReportsSelectColumn.Name = selectedColumnItem.getMember('Name').get_text();
            SelectedColumnName_DesignedReportsSelectColumn.ColumnType = selectedColumnItem.getMember('ColumnType').get_text();
            SelectedColumnName_DesignedReportsSelectColumn.KeyColumn = selectedColumnItem.getMember('ColumnName').get_text();
            SelectedColumnName_DesignedReportsSelectColumn.TrafficColumnCount = selectedColumnItem.getMember('TrafficColumnCount').get_text();
            document.getElementById('cmbColumnName_DesignedReportsSelectColumn_Input').value = SelectedColumnName_DesignedReportsSelectColumn.Name = selectedColumnItem.getMember('Name').get_text();
            cmbPersonInfoColumnName_DesignedReportsSelectColumn.unSelect();
            document.getElementById('cmbPersonInfoColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
            document.getElementById('txtTrafficColumnCount_DesignedReportsSelectColumn').value = SelectedColumnName_DesignedReportsSelectColumn.TrafficColumnCount;
            
        }
        else
        {
            SelectedPersonInfoColumnName_DesignedReportsSelectColumn = new Object();
            SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ID = selectedColumnItem.getMember('PersonInfoID').get_text();
            SelectedPersonInfoColumnName_DesignedReportsSelectColumn.Name = selectedColumnItem.getMember('Name').get_text();
            SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ColumnType = selectedColumnItem.getMember('ColumnType').get_text();
            SelectedPersonInfoColumnName_DesignedReportsSelectColumn.KeyColumn = selectedColumnItem.getMember('ColumnName').get_text();
            document.getElementById('cmbPersonInfoColumnName_DesignedReportsSelectColumn_Input').value = SelectedPersonInfoColumnName_DesignedReportsSelectColumn.Name = selectedColumnItem.getMember('Name').get_text();
            cmbColumnName_DesignedReportsSelectColumn.unSelect();
            document.getElementById('cmbColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
        }

        if (selectedColumnItem.getMember('ColumnName').get_text() == "AllTraffic") {
            document.getElementById('tblTrafficColumnCount_DesignedReportsSelectColumn').style.visibility = 'visible';
        }
        else {
            document.getElementById('tblTrafficColumnCount_DesignedReportsSelectColumn').style.visibility = 'hidden';
        }
       

    }
}
function tlbItemUp_TlbInterAction_DesignedReportsSelectColumn_onClick() {
    ChangeItemPriority_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn('Up');
}

function tlbItemDown_TlbInterAction_DesignedReportsSelectColumn_onClick() {
    ChangeItemPriority_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn('Down');
}

function ChangeItemPriority_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn(state) {
    if (GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.getSelectedItems().length > 0) {
       var tempItem = null;
       var targetItem = null;
       var  selectedItem = tempItem = GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.getSelectedItems()[0];
        switch (state) {
            case 'Up':
                if (selectedItem.get_index() > 0) {
                    targetItem = GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.get_table().getRow(selectedItem.get_index() - 1);
                }
                break;
            case 'Down':
                if (selectedItem.get_index() < 13)
                    targetItem = GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.get_table().getRow(selectedItem.get_index() + 1);
                break;
        }
        if (targetItem != null) {
            GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.beginUpdate();
            selectedItem.setValue(0, targetItem.getMember('ID').get_text(), false);
            selectedItem.setValue(1, targetItem.getMember('Name').get_text(), false);
            selectedItem.setValue(2, targetItem.getMember('Title').get_text(), false);
            selectedItem.setValue(3, targetItem.getMember('Active').get_text() == 'true' ? true : false, false);
            selectedItem.setValue(4, targetItem.getMember('ConceptID').get_text(), false);
            selectedItem.setValue(5, targetItem.getMember('ReportID').get_text(), false);
            selectedItem.setValue(6, targetItem.getMember('Order').get_text(), false);
            selectedItem.setValue(7, targetItem.getMember('PersonInfoID').get_text(), false);
            selectedItem.setValue(8, targetItem.getMember('IsConcept').get_text(), false);
            selectedItem.setValue(9, targetItem.getMember('ColumnType').get_text(), false);
            selectedItem.setValue(10, targetItem.getMember('ColumnName').get_text(), false);
            selectedItem.setValue(11, targetItem.getMember('TrafficColumnCount').get_text(), false);
            selectedItem.setValue(12, targetItem.getMember('TrafficID').get_text(), false);
            
            targetItem.setValue(0, tempItem.getMember('ID').get_text(), false);
            targetItem.setValue(1, tempItem.getMember('Name').get_text(), false);
            targetItem.setValue(2, tempItem.getMember('Title').get_text(), false);
            targetItem.setValue(3, tempItem.getMember('Active').get_text() == 'true' ? true : false, false);
            targetItem.setValue(4, tempItem.getMember('ConceptID').get_text(), false);
            targetItem.setValue(5, tempItem.getMember('ReportID').get_text(), false);
            targetItem.setValue(6, tempItem.getMember('Order').get_text(), false);
            targetItem.setValue(7, tempItem.getMember('PersonInfoID').get_text(), false);
            targetItem.setValue(8, tempItem.getMember('IsConcept').get_text(), false);
            targetItem.setValue(9, tempItem.getMember('ColumnType').get_text(), false);
            targetItem.setValue(10, tempItem.getMember('ColumnName').get_text(), false);
            targetItem.setValue(11, tempItem.getMember('TrafficColumnCount').get_text(), false);
            targetItem.setValue(12, tempItem.getMember('TrafficID').get_text(), false);
            GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.endUpdate();
            selectedItemIndex = targetItem.get_index();
            targetItemIdex = selectedItem.get_index();
            UpdateGridOrder_DesignedReportsSelectColumnPage(CharToKeyCode_DesignedReportsSelectColumn(state), CharToKeyCode_DesignedReportsSelectColumn(tempItem.getMember('Order').get_text()), CharToKeyCode_DesignedReportsSelectColumn(tempItem.getMember('ReportID').get_value()));
            TlbInterAction_DesignedReportsSelectColumn.set_enabled(false);
        }

        
    }
}
function UpdateGridOrder_DesignedReportsSelectColumnPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_DesignedReportsSelectColumn').value;
            Response[1] = document.getElementById('hfConnectionError_DesignedReportsSelectColumn').value;
        }
        
        if (RetMesage[2] == 'success') {
            var targetItem = GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.get_table().getRow(targetItemIdex);
            var selectedItem = GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.get_table().getRow(selectedItemIndex);
            GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.beginUpdate();
            selectedItem.setValue(6, Response[4].toString(), false);
            targetItem.setValue(6, Response[5].toString(), false);
            GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.endUpdate();
        }
        else {
            
            showDialog(RetMesage[0], Response[1], RetMesage[2]);
            Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn();
        }
        TlbInterAction_DesignedReportsSelectColumn.set_enabled(true);
    }
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





function cmbPersonInfoColumnName_DesignedReportsSelectColumn_onExpand() {
    if (cmbPersonInfoColumnName_DesignedReportsSelectColumn.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonInfoColumnName_DesignedReportsSelectColumn == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonInfoColumnName_DesignedReportsSelectColumn = true;
        Fill_cmbPersonInfoColumnName_DesignedReportsSelectColumn();
    }

}
function Fill_cmbPersonInfoColumnName_DesignedReportsSelectColumn() {
    ComboBox_onBeforeLoadData('cmbPersonInfoColumnName_DesignedReportsSelectColumn');
    var ObjReport = parent.DialogDesignedReportsSelectColumn.get_value();
    var ReportID = ObjReport.ReportID;
    CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn.callback(CharToKeyCode_DesignedReportsSelectColumn(ReportID));
}
function cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCollapse() {
    if (cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem() == undefined) {
        if (SelectedPersonInfoColumnName_DesignedReportsSelectColumn != null) {
            if (SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ID == null || SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ID == undefined)
                document.getElementById('cmbPersonInfoColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
            else {
                if (SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ID != null && SelectedPersonInfoColumnName_DesignedReportsSelectColumn.ID != undefined)
                    document.getElementById('cmbPersonInfoColumnName_DesignedReportsSelectColumn_Input').value = SelectedColumnName_DesignedReportsSelectColumn.Name;
            }
        }
    }

}
function cmbPersonInfoColumnName_DesignedReportsSelectColumn_onChange() {
    if (cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem() != null && cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem() != undefined) {
        cmbColumnName_DesignedReportsSelectColumn.unSelect();
        document.getElementById('cmbColumnName_DesignedReportsSelectColumn_Input').value = document.getElementById('hfcmbAlarm_DesignedReportsSelectColumn').value;
        var fieldName = cmbPersonInfoColumnName_DesignedReportsSelectColumn.getSelectedItem().get_text();
        document.getElementById('txtColumnTitle_DesignedReportsSelectColumn').value = fieldName;
    }

}
function CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onBeforeCallback() {

    cmbPersonInfoColumnName_DesignedReportsSelectColumn.dispose();
}
function CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCallbackComplete() {
    var error = document.getElementById('ErrorHiddenField_ColumnName').value;
    if (error == "") {
        document.getElementById('cmbPersonInfoColumnName_DesignedReportsSelectColumn_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonInfoColumnName_DesignedReportsSelectColumn_DropImage').mousedown();
        cmbPersonInfoColumnName_DesignedReportsSelectColumn.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbGroupNodes_DesignedReports_DropDown').style.display = 'none';
    }

}
function CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCallbackError() {
    ShowConnectionError_DesignedReportsSelectColumn();
}

function tlbItemFormReconstruction_TlbDesignedReportsSelectColumn_onClick() {
    ReconstructForm_DesignedReportsSelectColumn();
}

function ReconstructForm_DesignedReportsSelectColumn() {
    DesignedReportsSelectColumn_onClose();
    parent.document.getElementById('pgvDesignedReports_iFrame').contentWindow.ShowDialogDesignedReportsSelectColumn_DesignedReports();
}
function DesignedReportsSelectColumn_onClose() {
    parent.document.getElementById(parent.ClientPerfixId +  'DialogDesignedReportsSelectColumn_IFrame').src =parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogDesignedReportsSelectColumn').Close();
}

function txtTrafficColumnCount_DesignedReportsSelectColumn_onChange() {
    var val = document.getElementById('txtTrafficColumnCount_DesignedReportsSelectColumn').value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '0' : '0';
    document.getElementById('txtTrafficColumnCount_DesignedReportsSelectColumn').value = val;
}