
var CurrentPageState_FlowGroup = 'View';
var ConfirmState_FlowGroup = null;
var ObjFlowGroup_FlowGroup = null;

function GetBoxesHeaders_FlowGroup() {
    document.getElementById('header_FlowGroups_FlowGroup').innerHTML = document.getElementById('hfheader_FlowGroups_FlowGroup').value;
    document.getElementById('header_tblFlowGroup_FlowGroup').innerHTML = document.getElementById('hfheader_tblFlowGroup_FlowGroup').value;
}

function GridFlowGroup_FlowGroup_onItemSelect(sender, e) {
    if (CurrentPageState_FlowGroup != 'Add')
        NavigateFlowGroup_FlowGroup(e.get_item());
}

function NavigateFlowGroup_FlowGroup(selectedFlowGroupItem) {
    if (selectedFlowGroupItem != undefined) {
        document.getElementById('txtFlowGroupName_FlowGroup').value = selectedFlowGroupItem.getMember('Name').get_text();
        document.getElementById('txtDescription_FlowGroup').value = selectedFlowGroupItem.getMember('Description').get_text();
    }
}


function tlbItemNew_TlbFlowGroup_onClick() {
    ChangePageState_FlowGroup('Add');
    ClearList_FlowGroup();
    FocusOnFirstElement_FlowGroup();
}

function tlbItemEdit_TlbFlowGroup_onClick() {
    ChangePageState_FlowGroup('Edit');
    FocusOnFirstElement_FlowGroup();
}

function tlbItemDelete_TlbFlowGroup_onClick() {
    ChangePageState_FlowGroup('Delete');
}

function tlbItemSave_TlbFlowGroup_onClick() {
    FlowGroup_onSave();
}

function tlbItemCancel_TlbFlowGroup_onClick() {
    ChangePageState_FlowGroup('View');
    ClearList_FlowGroup();
}

function tlbItemExit_TlbFlowGroup_onClick() {
    ShowDialogConfirm('Exit');
}

function FlowGroup_onSave() {
    if (CurrentPageState_FlowGroup != 'Delete')
        UpdateFlowGroup_FlowGroup();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_FlowGroup(str) {
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


function ClearList_FlowGroup() {
    if (CurrentPageState_FlowGroup != 'Edit') {
        document.getElementById('txtFlowGroupName_FlowGroup').value = '';
        document.getElementById('txtDescription_FlowGroup').value = '';
        GridFlowGroup_FlowGroup.unSelectAll();
    }
}

function FocusOnFirstElement_FlowGroup() {
    document.getElementById('txtFlowGroupName_FlowGroup').focus();
}

function ChangePageState_FlowGroup(state) {
    CurrentPageState_FlowGroup = state;
    SetActionMode_FlowGroup(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbFlowGroup.get_items().getItemById('tlbItemNew_TlbFlowGroup') != null) {
            TlbFlowGroup.get_items().getItemById('tlbItemNew_TlbFlowGroup').set_enabled(false);
            TlbFlowGroup.get_items().getItemById('tlbItemNew_TlbFlowGroup').set_imageUrl('add_silver.png');
        }
        if (TlbFlowGroup.get_items().getItemById('tlbItemEdit_TlbFlowGroup') != null) {
            TlbFlowGroup.get_items().getItemById('tlbItemEdit_TlbFlowGroup').set_enabled(false);
            TlbFlowGroup.get_items().getItemById('tlbItemEdit_TlbFlowGroup').set_imageUrl('edit_silver.png');
        }
        if (TlbFlowGroup.get_items().getItemById('tlbItemDelete_TlbFlowGroup') != null) {
            TlbFlowGroup.get_items().getItemById('tlbItemDelete_TlbFlowGroup').set_enabled(false);
            TlbFlowGroup.get_items().getItemById('tlbItemDelete_TlbFlowGroup').set_imageUrl('remove_silver.png');
        }
        TlbFlowGroup.get_items().getItemById('tlbItemSave_TlbFlowGroup').set_enabled(true);
        TlbFlowGroup.get_items().getItemById('tlbItemSave_TlbFlowGroup').set_imageUrl('save.png');
        TlbFlowGroup.get_items().getItemById('tlbItemCancel_TlbFlowGroup').set_enabled(true);
        TlbFlowGroup.get_items().getItemById('tlbItemCancel_TlbFlowGroup').set_imageUrl('cancel.png');
        document.getElementById('txtFlowGroupName_FlowGroup').disabled = '';
        document.getElementById('txtDescription_FlowGroup').disabled = '';
        if (state == 'Edit')
            NavigateFlowGroup_FlowGroup(GridFlowGroup_FlowGroup.getSelectedItems()[0]);
        if (state == 'Delete')
            FlowGroup_onSave();
    }
    if (state == 'View') {
        if (TlbFlowGroup.get_items().getItemById('tlbItemNew_TlbFlowGroup') != null) {
            TlbFlowGroup.get_items().getItemById('tlbItemNew_TlbFlowGroup').set_enabled(true);
            TlbFlowGroup.get_items().getItemById('tlbItemNew_TlbFlowGroup').set_imageUrl('add.png');
        }
        if (TlbFlowGroup.get_items().getItemById('tlbItemEdit_TlbFlowGroup') != null) {
            TlbFlowGroup.get_items().getItemById('tlbItemEdit_TlbFlowGroup').set_enabled(true);
            TlbFlowGroup.get_items().getItemById('tlbItemEdit_TlbFlowGroup').set_imageUrl('edit.png');
        }
        if (TlbFlowGroup.get_items().getItemById('tlbItemDelete_TlbFlowGroup') != null) {
            TlbFlowGroup.get_items().getItemById('tlbItemDelete_TlbFlowGroup').set_enabled(true);
            TlbFlowGroup.get_items().getItemById('tlbItemDelete_TlbFlowGroup').set_imageUrl('remove.png');
        }
        TlbFlowGroup.get_items().getItemById('tlbItemSave_TlbFlowGroup').set_enabled(false);
        TlbFlowGroup.get_items().getItemById('tlbItemSave_TlbFlowGroup').set_imageUrl('save_silver.png');
        TlbFlowGroup.get_items().getItemById('tlbItemCancel_TlbFlowGroup').set_enabled(false);
        TlbFlowGroup.get_items().getItemById('tlbItemCancel_TlbFlowGroup').set_imageUrl('cancel_silver.png');
        document.getElementById('txtFlowGroupName_FlowGroup').disabled = 'disabled';
        document.getElementById('txtDescription_FlowGroup').disabled = 'disabled';
    }
}

function SetActionMode_FlowGroup(state) {
    document.getElementById('ActionMode_FlowGroup').innerHTML = document.getElementById("hf" + state + "_FlowGroup").value;
}

function Fill_GridFlowGroup_FlowGroup() {
    document.getElementById('loadingPanel_GridFlowGroup_FlowGroup').innerHTML = document.getElementById('hfloadingPanel_GridFlowGroup_FlowGroup').value;
    CallBack_GridFlowGroup_FlowGroup.callback();
}

function Refresh_GridFlowGroup_FlowGroup() {
    Fill_GridFlowGroup_FlowGroup();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_FlowGroup = confirmState;
    if (CurrentPageState_FlowGroup == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_FlowGroup').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_FlowGroup').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_FlowGroup) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateFlowGroup_FlowGroup();
            break;
        case 'Exit':
            ClearList_FlowGroup();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_FlowGroup('View');
}

function UpdateFlowGroup_FlowGroup() {
    ObjFlowGroup_FlowGroup = new Object();
    ObjFlowGroup_FlowGroup.Name = null;
    ObjFlowGroup_FlowGroup.Description = null;
    ObjFlowGroup_FlowGroup.ID = '0';
    var SelectedItems_GridFlowGroup_FlowGroup = GridFlowGroup_FlowGroup.getSelectedItems();
    if (SelectedItems_GridFlowGroup_FlowGroup.length > 0)
        ObjFlowGroup_FlowGroup.ID = SelectedItems_GridFlowGroup_FlowGroup[0].getMember("ID").get_text();

    if (CurrentPageState_FlowGroup != 'Delete') {
        ObjFlowGroup_FlowGroup.Name = document.getElementById('txtFlowGroupName_FlowGroup').value;
        ObjFlowGroup_FlowGroup.Description = document.getElementById('txtDescription_FlowGroup').value;
    }
    UpdateFlowGroup_FlowGroupPage(CharToKeyCode_FlowGroup(CurrentPageState_FlowGroup), CharToKeyCode_FlowGroup(ObjFlowGroup_FlowGroup.ID), CharToKeyCode_FlowGroup(ObjFlowGroup_FlowGroup.Name), CharToKeyCode_FlowGroup(ObjFlowGroup_FlowGroup.Description));
    DialogWaiting.Show();
}

function UpdateFlowGroup_FlowGroupPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_FlowGroup').value;
            Response[1] = document.getElementById('hfConnectionError_FlowGroup').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_FlowGroup();
            FlowGroup_OnAfterUpdate(Response);
            ChangePageState_FlowGroup('View');
        }
        else {
            if (CurrentPageState_FlowGroup == 'Delete')
                ChangePageState_FlowGroup('View');
        }
    }
}

function FlowGroup_OnAfterUpdate(Response) {
    if (ObjFlowGroup_FlowGroup != null) {
        var FlowGroupName = ObjFlowGroup_FlowGroup.Name;
        var FlowGroupDescription = ObjFlowGroup_FlowGroup.Description;

        var FlowGroupItem = null;
        GridFlowGroup_FlowGroup.beginUpdate();
        switch (CurrentPageState_FlowGroup) {
            case 'Add':
                FlowGroupItem = GridFlowGroup_FlowGroup.get_table().addEmptyRow(GridFlowGroup_FlowGroup.get_recordCount());
                FlowGroupItem.setValue(0, Response[3], false);
                GridFlowGroup_FlowGroup.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridFlowGroup_FlowGroup.selectByKey(Response[3], 0, false);
                FlowGroupItem = GridFlowGroup_FlowGroup.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridFlowGroup_FlowGroup.selectByKey(ObjFlowGroup_FlowGroup.ID, 0, false);
                GridFlowGroup_FlowGroup.deleteSelected();
                break;
        }
        if (CurrentPageState_FlowGroup != 'Delete') {
            FlowGroupItem.setValue(1, FlowGroupName, false);
            FlowGroupItem.setValue(2, FlowGroupDescription, false);
        }
        GridFlowGroup_FlowGroup.endUpdate();
    }
}

function CallBack_GridFlowGroup_FlowGroup_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_FlowGroup').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridFlowGroup_FlowGroup();
    }
}

function GridFlowGroup_FlowGroup_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridFlowGroup_FlowGroup').innerHTML = '';
}

function CallBack_GridFlowGroup_FlowGroup_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridFlowGroup_FlowGroup').innerHTML = '';
    ShowConnectionError_FlowGroup();
}

function ShowConnectionError_FlowGroup() {
    var error = document.getElementById('hfErrorType_FlowGroup').value;
    var errorBody = document.getElementById('hfConnectionError_FlowGroup').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbFlowGroup_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvFlowGroupIntroduction_iFrame').src = parent.ModulePath + 'FlowGroup.aspx';
}

function tlbItemHelp_TlbFlowGroup_onClick() {
    LoadHelpPage('tlbItemHelp_TlbFlowGroup');
}












