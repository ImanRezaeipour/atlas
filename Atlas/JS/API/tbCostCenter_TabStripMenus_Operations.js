
var CurrentPageState_CostCenter = 'View';
var ConfirmState_CostCenter = null;
var ObjCostCenter_CostCenter = null;

function GetBoxesHeaders_CostCenter() {
    document.getElementById('header_CostCenters_CostCenter').innerHTML = document.getElementById('hfheader_CostCenters_CostCenter').value;
    document.getElementById('header_tblCostCenter_CostCenter').innerHTML = document.getElementById('hfheader_tblCostCenter_CostCenter').value;
}

function GridCostCenter_CostCenter_onItemSelect(sender, e) {
    if (CurrentPageState_CostCenter != 'Add')
        NavigateCostCenter_CostCenter(e.get_item());
}

function NavigateCostCenter_CostCenter(selectedCostCenterItem) {
    if (selectedCostCenterItem != undefined) {
        document.getElementById('txtCostCenterName_CostCenter').value = selectedCostCenterItem.getMember('Name').get_text();
        document.getElementById('txtCostCenterCode_CostCenter').value = selectedCostCenterItem.getMember('Code').get_text();
        document.getElementById('txtDescription_CostCenter').value = selectedCostCenterItem.getMember('Description').get_text();
    }
}


function tlbItemNew_TlbCostCenter_onClick() {
    ChangePageState_CostCenter('Add');
    ClearList_CostCenter();
    FocusOnFirstElement_CostCenter();
}

function tlbItemEdit_TlbCostCenter_onClick() {
    ChangePageState_CostCenter('Edit');
    FocusOnFirstElement_CostCenter();
}

function tlbItemDelete_TlbCostCenter_onClick() {
    ChangePageState_CostCenter('Delete');
}

function tlbItemSave_TlbCostCenter_onClick() {
    CostCenter_onSave();
}

function tlbItemCancel_TlbCostCenter_onClick() {
    ChangePageState_CostCenter('View');
    ClearList_CostCenter();
}

function tlbItemExit_TlbCostCenter_onClick() {
    ShowDialogConfirm('Exit');
}

function CostCenter_onSave() {
    if (CurrentPageState_CostCenter != 'Delete')
        UpdateCostCenter_CostCenter();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_CostCenter(str) {
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


function ClearList_CostCenter() {
    if (CurrentPageState_CostCenter != 'Edit') {
        document.getElementById('txtCostCenterName_CostCenter').value = '';
        document.getElementById('txtCostCenterCode_CostCenter').value = '';
        document.getElementById('txtDescription_CostCenter').value = '';
        GridCostCenter_CostCenter.unSelectAll();
    }
}

function FocusOnFirstElement_CostCenter() {
    document.getElementById('txtCostCenterName_CostCenter').focus();
}

function ChangePageState_CostCenter(state) {
    CurrentPageState_CostCenter = state;
    SetActionMode_CostCenter(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbCostCenter.get_items().getItemById('tlbItemNew_TlbCostCenter') != null) {
            TlbCostCenter.get_items().getItemById('tlbItemNew_TlbCostCenter').set_enabled(false);
            TlbCostCenter.get_items().getItemById('tlbItemNew_TlbCostCenter').set_imageUrl('add_silver.png');
        }
        if (TlbCostCenter.get_items().getItemById('tlbItemEdit_TlbCostCenter') != null) {
            TlbCostCenter.get_items().getItemById('tlbItemEdit_TlbCostCenter').set_enabled(false);
            TlbCostCenter.get_items().getItemById('tlbItemEdit_TlbCostCenter').set_imageUrl('edit_silver.png');
        }
        if (TlbCostCenter.get_items().getItemById('tlbItemDelete_TlbCostCenter') != null) {
            TlbCostCenter.get_items().getItemById('tlbItemDelete_TlbCostCenter').set_enabled(false);
            TlbCostCenter.get_items().getItemById('tlbItemDelete_TlbCostCenter').set_imageUrl('remove_silver.png');
        }
        TlbCostCenter.get_items().getItemById('tlbItemSave_TlbCostCenter').set_enabled(true);
        TlbCostCenter.get_items().getItemById('tlbItemSave_TlbCostCenter').set_imageUrl('save.png');
        TlbCostCenter.get_items().getItemById('tlbItemCancel_TlbCostCenter').set_enabled(true);
        TlbCostCenter.get_items().getItemById('tlbItemCancel_TlbCostCenter').set_imageUrl('cancel.png');
        document.getElementById('txtCostCenterName_CostCenter').disabled = '';
        document.getElementById('txtCostCenterCode_CostCenter').disabled = '';
        document.getElementById('txtDescription_CostCenter').disabled = '';
        if (state == 'Edit')
            NavigateCostCenter_CostCenter(GridCostCenter_CostCenter.getSelectedItems()[0]);
        if (state == 'Delete')
            CostCenter_onSave();
    }
    if (state == 'View') {
        if (TlbCostCenter.get_items().getItemById('tlbItemNew_TlbCostCenter') != null) {
            TlbCostCenter.get_items().getItemById('tlbItemNew_TlbCostCenter').set_enabled(true);
            TlbCostCenter.get_items().getItemById('tlbItemNew_TlbCostCenter').set_imageUrl('add.png');
        }
        if (TlbCostCenter.get_items().getItemById('tlbItemEdit_TlbCostCenter') != null) {
            TlbCostCenter.get_items().getItemById('tlbItemEdit_TlbCostCenter').set_enabled(true);
            TlbCostCenter.get_items().getItemById('tlbItemEdit_TlbCostCenter').set_imageUrl('edit.png');
        }
        if (TlbCostCenter.get_items().getItemById('tlbItemDelete_TlbCostCenter') != null) {
            TlbCostCenter.get_items().getItemById('tlbItemDelete_TlbCostCenter').set_enabled(true);
            TlbCostCenter.get_items().getItemById('tlbItemDelete_TlbCostCenter').set_imageUrl('remove.png');
        }
        TlbCostCenter.get_items().getItemById('tlbItemSave_TlbCostCenter').set_enabled(false);
        TlbCostCenter.get_items().getItemById('tlbItemSave_TlbCostCenter').set_imageUrl('save_silver.png');
        TlbCostCenter.get_items().getItemById('tlbItemCancel_TlbCostCenter').set_enabled(false);
        TlbCostCenter.get_items().getItemById('tlbItemCancel_TlbCostCenter').set_imageUrl('cancel_silver.png');
        document.getElementById('txtCostCenterName_CostCenter').disabled = 'disabled';
        document.getElementById('txtCostCenterCode_CostCenter').disabled = 'disabled';
        document.getElementById('txtDescription_CostCenter').disabled = 'disabled';
    }
}

function SetActionMode_CostCenter(state) {
    document.getElementById('ActionMode_CostCenter').innerHTML = document.getElementById("hf" + state + "_CostCenter").value;
}

function Fill_GridCostCenter_CostCenter() {
    document.getElementById('loadingPanel_GridCostCenter_CostCenter').innerHTML = document.getElementById('hfloadingPanel_GridCostCenter_CostCenter').value;
    CallBack_GridCostCenter_CostCenter.callback();
}

function Refresh_GridCostCenter_CostCenter() {
    Fill_GridCostCenter_CostCenter();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_CostCenter = confirmState;
    if (CurrentPageState_CostCenter == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_CostCenter').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_CostCenter').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_CostCenter) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateCostCenter_CostCenter();
            break;
        case 'Exit':
            ClearList_CostCenter();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_CostCenter('View');
}

function UpdateCostCenter_CostCenter() {
    ObjCostCenter_CostCenter = new Object();
    ObjCostCenter_CostCenter.Name = null;
    ObjCostCenter_CostCenter.Code = null;
    ObjCostCenter_CostCenter.Description = null;
    ObjCostCenter_CostCenter.ID = '0';
    var SelectedItems_GridCostCenter_CostCenter = GridCostCenter_CostCenter.getSelectedItems();
    if (SelectedItems_GridCostCenter_CostCenter.length > 0)
        ObjCostCenter_CostCenter.ID = SelectedItems_GridCostCenter_CostCenter[0].getMember("ID").get_text();

    if (CurrentPageState_CostCenter != 'Delete') {
        ObjCostCenter_CostCenter.Name = document.getElementById('txtCostCenterName_CostCenter').value;
        ObjCostCenter_CostCenter.Code = document.getElementById('txtCostCenterCode_CostCenter').value;
        ObjCostCenter_CostCenter.Description = document.getElementById('txtDescription_CostCenter').value;
    }
    UpdateCostCenter_CostCenterPage(
        CharToKeyCode_CostCenter(CurrentPageState_CostCenter),
        CharToKeyCode_CostCenter(ObjCostCenter_CostCenter.ID),
        CharToKeyCode_CostCenter(ObjCostCenter_CostCenter.Name),
        CharToKeyCode_CostCenter(ObjCostCenter_CostCenter.Code),
        CharToKeyCode_CostCenter(ObjCostCenter_CostCenter.Description)
        );
    DialogWaiting.Show();
}

function UpdateCostCenter_CostCenterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_CostCenter').value;
            Response[1] = document.getElementById('hfConnectionError_CostCenter').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_CostCenter();
            CostCenter_OnAfterUpdate(Response);
            ChangePageState_CostCenter('View');
        }
        else {
            if (CurrentPageState_CostCenter == 'Delete')
                ChangePageState_CostCenter('View');
        }
    }
}

function CostCenter_OnAfterUpdate(Response) {
    if (ObjCostCenter_CostCenter != null) {
        var CostCenterName = ObjCostCenter_CostCenter.Name;
        var CostCenterCode = ObjCostCenter_CostCenter.Code;
        var CostCenterDescription = ObjCostCenter_CostCenter.Description;

        var CostCenterItem = null;
        GridCostCenter_CostCenter.beginUpdate();
        switch (CurrentPageState_CostCenter) {
            case 'Add':
                CostCenterItem = GridCostCenter_CostCenter.get_table().addEmptyRow(GridCostCenter_CostCenter.get_recordCount());
                CostCenterItem.setValue(0, Response[3], false);
                GridCostCenter_CostCenter.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridCostCenter_CostCenter.selectByKey(Response[3], 0, false);
                CostCenterItem = GridCostCenter_CostCenter.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridCostCenter_CostCenter.selectByKey(ObjCostCenter_CostCenter.ID, 0, false);
                GridCostCenter_CostCenter.deleteSelected();
                break;
        }
        if (CurrentPageState_CostCenter != 'Delete') {
            CostCenterItem.setValue(1, CostCenterName, false);
            CostCenterItem.setValue(2, CostCenterCode, false);
            CostCenterItem.setValue(3, CostCenterDescription, false);
        }
        GridCostCenter_CostCenter.endUpdate();
    }
}

function CallBack_GridCostCenter_CostCenter_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CostCenter').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridCostCenter_CostCenter();
    }
}

function GridCostCenter_CostCenter_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridCostCenter_CostCenter').innerHTML = '';
}

function CallBack_GridCostCenter_CostCenter_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridCostCenter_CostCenter').innerHTML = '';
    ShowConnectionError_CostCenter();
}

function ShowConnectionError_CostCenter() {
    var error = document.getElementById('hfErrorType_CostCenter').value;
    var errorBody = document.getElementById('hfConnectionError_CostCenter').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbCostCenter_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvCostCenterIntroduction_iFrame').src = parent.ModulePath + 'CostCenter.aspx';
}

function tlbItemHelp_TlbCostCenter_onClick() {
    LoadHelpPage('tlbItemHelp_TlbCostCenter');
}












