
var CurrentPageState_ShiftPairTypes = 'View';
var ConfirmState_ShiftPairTypes = null;
var ObjShiftPairType_ShiftPairTypes = null;

function GetBoxesHeaders_ShiftPairTypes() {
    document.getElementById('header_ShiftPairTypeDetails_ShiftPairTypes').innerHTML = document.getElementById('hfheader_ShiftPairTypeDetails_ShiftPairTypes').value;
    document.getElementById('header_ShiftPairTypes_ShiftPairTypes').innerHTML = document.getElementById('hfheader_ShiftPairTypes_ShiftPairTypes').value;
}


function tlbItemNew_TlbShiftPairTypes_onClick() {
    ChangePageState_ShiftPairTypes('Add');
    ClearList_ShiftPairTypes();
    FocusOnFirstElement_ShiftPairTypes();
}

function tlbItemEdit_TlbShiftPairTypes_onClick() {
    ChangePageState_ShiftPairTypes('Edit');
    FocusOnFirstElement_ShiftPairTypes();
}

function tlbItemDelete_TlbShiftPairTypes_onClick() {
    ChangePageState_ShiftPairTypes('Delete');
}

function tlbItemSave_TlbShiftPairTypes_onClick() {
    ShiftPairType_onSave();
}

function tlbItemCancel_TlbShiftPairTypes_onClick() {
    ChangePageState_ShiftPairTypes('View');
    ClearList_ShiftPairTypes();
}

function tlbItemExit_TlbShiftPairTypes_onClick() {
    ShowDialogConfirm('Exit');
}

function Refresh_GridShiftPairTypes_ShiftPairTypes() {
    Fill_GridShiftPairTypes_ShiftPairTypes();
}

function GridShiftPairTypes_ShiftPairTypes_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridShiftPairTypes_ShiftPairTypes').innerHTML = '';
}

function GridShiftPairTypes_ShiftPairTypes_onItemSelect(sender, e) {
    if (CurrentPageState_ShiftPairTypes != 'Add')
        NavigateShiftPairType_ShiftPairTypes(e.get_item());
}

function CallBack_GridShiftPairTypes_ShiftPairTypes_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ShiftPairTypes').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridShiftPairTypes_ShiftPairTypes();
    }
}

function CallBack_GridShiftPairTypes_ShiftPairTypes_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridShiftPairTypes_ShiftPairTypes').innerHTML = '';
    ShowConnectionError_ShiftPairTypes();
}

function ShowConnectionError_ShiftPairTypes() {
    var error = document.getElementById('hfErrorType_ShiftPairTypes').value;
    var errorBody = document.getElementById('hfConnectionError_ShiftPairTypes').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_ShiftPairTypes) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateShiftPairType_ShiftPairTypes();
            break;
        case 'Exit':
            ClearList_ShiftPairTypes();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_ShiftPairTypes('View');
}

function NavigateShiftPairType_ShiftPairTypes(selectedShiftPairTypeItem) {
    if (selectedShiftPairTypeItem != undefined) {
        document.getElementById('chbActive_ShiftPairTypes').checked = selectedShiftPairTypeItem.getMember('Active').get_value();
        document.getElementById('txtShiftPairTypeCode_ShiftPairTypes').value = selectedShiftPairTypeItem.getMember('CustomCode').get_text();
        document.getElementById('txtShiftPairTypeTitle_ShiftPairTypes').value = selectedShiftPairTypeItem.getMember('Title').get_text();
        document.getElementById('txtShiftPairTypeDescriptions_ShiftPairTypes').value = selectedShiftPairTypeItem.getMember('Description').get_text();
    }
}

function ShiftPairType_onSave() {
    if (CurrentPageState_ShiftPairTypes != 'Delete')
        UpdateShiftPairType_ShiftPairTypes();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_ShiftPairTypes(str) {
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


function ClearList_ShiftPairTypes() {
    if (CurrentPageState_ShiftPairTypes != 'Edit') {
        document.getElementById('chbActive_ShiftPairTypes').checked = true;
        document.getElementById('txtShiftPairTypeCode_ShiftPairTypes').value = '';
        document.getElementById('txtShiftPairTypeTitle_ShiftPairTypes').value = '';
        document.getElementById('txtShiftPairTypeDescriptions_ShiftPairTypes').value = '';
        GridShiftPairTypes_ShiftPairTypes.unSelectAll();
    }
}

function FocusOnFirstElement_ShiftPairTypes() {
    document.getElementById('txtShiftPairTypeCode_ShiftPairTypes').focus();
}

function ChangePageState_ShiftPairTypes(state) {
    CurrentPageState_ShiftPairTypes = state;
    SetActionMode_ShiftPairTypes(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbShiftPairTypes.get_items().getItemById('tlbItemNew_TlbShiftPairTypes') != null) {
            TlbShiftPairTypes.get_items().getItemById('tlbItemNew_TlbShiftPairTypes').set_enabled(false);
            TlbShiftPairTypes.get_items().getItemById('tlbItemNew_TlbShiftPairTypes').set_imageUrl('add_silver.png');
        }
        if (TlbShiftPairTypes.get_items().getItemById('tlbItemEdit_TlbShiftPairTypes') != null) {
            TlbShiftPairTypes.get_items().getItemById('tlbItemEdit_TlbShiftPairTypes').set_enabled(false);
            TlbShiftPairTypes.get_items().getItemById('tlbItemEdit_TlbShiftPairTypes').set_imageUrl('edit_silver.png');
        }
        if (TlbShiftPairTypes.get_items().getItemById('tlbItemDelete_TlbShiftPairTypes') != null) {
            TlbShiftPairTypes.get_items().getItemById('tlbItemDelete_TlbShiftPairTypes').set_enabled(false);
            TlbShiftPairTypes.get_items().getItemById('tlbItemDelete_TlbShiftPairTypes').set_imageUrl('remove_silver.png');
        }
        TlbShiftPairTypes.get_items().getItemById('tlbItemSave_TlbShiftPairTypes').set_enabled(true);
        TlbShiftPairTypes.get_items().getItemById('tlbItemSave_TlbShiftPairTypes').set_imageUrl('save.png');
        TlbShiftPairTypes.get_items().getItemById('tlbItemCancel_TlbShiftPairTypes').set_enabled(true);
        TlbShiftPairTypes.get_items().getItemById('tlbItemCancel_TlbShiftPairTypes').set_imageUrl('cancel.png');
        document.getElementById('chbActive_ShiftPairTypes').disabled = '';
        document.getElementById('txtShiftPairTypeCode_ShiftPairTypes').disabled = '';
        document.getElementById('txtShiftPairTypeTitle_ShiftPairTypes').disabled = '';
        document.getElementById('txtShiftPairTypeDescriptions_ShiftPairTypes').disabled = '';
        if (state == 'Edit')
            NavigateShiftPairType_ShiftPairTypes(GridShiftPairTypes_ShiftPairTypes.getSelectedItems()[0]);
        if (state == 'Delete')
            ShiftPairType_onSave();
    }
    if (state == 'View') {
        if (TlbShiftPairTypes.get_items().getItemById('tlbItemNew_TlbShiftPairTypes') != null) {
            TlbShiftPairTypes.get_items().getItemById('tlbItemNew_TlbShiftPairTypes').set_enabled(true);
            TlbShiftPairTypes.get_items().getItemById('tlbItemNew_TlbShiftPairTypes').set_imageUrl('add.png');
        }
        if (TlbShiftPairTypes.get_items().getItemById('tlbItemEdit_TlbShiftPairTypes') != null) {
            TlbShiftPairTypes.get_items().getItemById('tlbItemEdit_TlbShiftPairTypes').set_enabled(true);
            TlbShiftPairTypes.get_items().getItemById('tlbItemEdit_TlbShiftPairTypes').set_imageUrl('edit.png');
        }
        if (TlbShiftPairTypes.get_items().getItemById('tlbItemDelete_TlbShiftPairTypes') != null) {
            TlbShiftPairTypes.get_items().getItemById('tlbItemDelete_TlbShiftPairTypes').set_enabled(true);
            TlbShiftPairTypes.get_items().getItemById('tlbItemDelete_TlbShiftPairTypes').set_imageUrl('remove.png');
        }
        TlbShiftPairTypes.get_items().getItemById('tlbItemSave_TlbShiftPairTypes').set_enabled(false);
        TlbShiftPairTypes.get_items().getItemById('tlbItemSave_TlbShiftPairTypes').set_imageUrl('save_silver.png');
        TlbShiftPairTypes.get_items().getItemById('tlbItemCancel_TlbShiftPairTypes').set_enabled(false);
        TlbShiftPairTypes.get_items().getItemById('tlbItemCancel_TlbShiftPairTypes').set_imageUrl('cancel_silver.png');
        document.getElementById('chbActive_ShiftPairTypes').disabled = '';
        document.getElementById('txtShiftPairTypeCode_ShiftPairTypes').disabled = '';
        document.getElementById('txtShiftPairTypeTitle_ShiftPairTypes').disabled = '';
        document.getElementById('txtShiftPairTypeDescriptions_ShiftPairTypes').disabled = '';
    }
}

function SetActionMode_ShiftPairTypes(state) {
    document.getElementById('ActionMode_ShiftPairTypes').innerHTML = document.getElementById("hf" + state + "_ShiftPairTypes").value;
}

function Fill_GridShiftPairTypes_ShiftPairTypes() {
    document.getElementById('loadingPanel_GridShiftPairTypes_ShiftPairTypes').innerHTML =GetLoadingMessage(document.getElementById('hfloadingPanel_GridShiftPairTypes_ShiftPairTypes').value);
    CallBack_GridShiftPairTypes_ShiftPairTypes.callback();
}

function Refresh_GridShiftPairTypes_ShiftPairTypes() {
    Fill_GridShiftPairTypes_ShiftPairTypes();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_ShiftPairTypes = confirmState;
    if (CurrentPageState_ShiftPairTypes == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_ShiftPairTypes').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ShiftPairTypes').value;
    DialogConfirm.Show();
}


function UpdateShiftPairType_ShiftPairTypes() {
    ObjShiftPairType_ShiftPairTypes = new Object();
    ObjShiftPairType_ShiftPairTypes.Active = false;
    ObjShiftPairType_ShiftPairTypes.CustomCode = null;
    ObjShiftPairType_ShiftPairTypes.Title = null;
    ObjShiftPairType_ShiftPairTypes.Descriptions = null;
    ObjShiftPairType_ShiftPairTypes.ID = '0';
    var SelectedItems_GridShiftPairTypes_ShiftPairTypes = GridShiftPairTypes_ShiftPairTypes.getSelectedItems();
    if (SelectedItems_GridShiftPairTypes_ShiftPairTypes.length > 0)
        ObjShiftPairType_ShiftPairTypes.ID = SelectedItems_GridShiftPairTypes_ShiftPairTypes[0].getMember("ID").get_text();

    if (CurrentPageState_ShiftPairTypes != 'Delete') {
        ObjShiftPairType_ShiftPairTypes.Active = document.getElementById('chbActive_ShiftPairTypes').checked;
        ObjShiftPairType_ShiftPairTypes.CustomCode = document.getElementById('txtShiftPairTypeCode_ShiftPairTypes').value;
        ObjShiftPairType_ShiftPairTypes.Title = document.getElementById('txtShiftPairTypeTitle_ShiftPairTypes').value;
        ObjShiftPairType_ShiftPairTypes.Descriptions = document.getElementById('txtShiftPairTypeDescriptions_ShiftPairTypes').value;
    }
    UpdateShiftPairType_ShiftPairTypesPage(CharToKeyCode_ShiftPairTypes(CurrentPageState_ShiftPairTypes), CharToKeyCode_ShiftPairTypes(ObjShiftPairType_ShiftPairTypes.ID), CharToKeyCode_ShiftPairTypes(ObjShiftPairType_ShiftPairTypes.Active.toString()), CharToKeyCode_ShiftPairTypes(ObjShiftPairType_ShiftPairTypes.CustomCode), CharToKeyCode_ShiftPairTypes(ObjShiftPairType_ShiftPairTypes.Title), CharToKeyCode_ShiftPairTypes(ObjShiftPairType_ShiftPairTypes.Descriptions));
    DialogWaiting.Show();
}

function UpdateShiftPairType_ShiftPairTypesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_ShiftPairTypes').value;
            Response[1] = document.getElementById('hfConnectionError_ShiftPairTypes').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_ShiftPairTypes();
            ShiftPairType_OnAfterUpdate(Response);
            ChangePageState_ShiftPairTypes('View');
        }
        else {
            if (CurrentPageState_ShiftPairTypes == 'Delete')
                ChangePageState_ShiftPairTypes('View');
        }
    }
}

function ShiftPairType_OnAfterUpdate(Response) {
    if (ObjShiftPairType_ShiftPairTypes != null) {
        var Active = ObjShiftPairType_ShiftPairTypes.Active;
        var CustomCode = ObjShiftPairType_ShiftPairTypes.CustomCode;
        var Title = ObjShiftPairType_ShiftPairTypes.Title;
        var Descriptions = ObjShiftPairType_ShiftPairTypes.Descriptions;

        var ShiftPairTypeItem = null;
        GridShiftPairTypes_ShiftPairTypes.beginUpdate();
        switch (CurrentPageState_ShiftPairTypes) {
            case 'Add':
                ShiftPairTypeItem = GridShiftPairTypes_ShiftPairTypes.get_table().addEmptyRow(GridShiftPairTypes_ShiftPairTypes.get_recordCount());
                ShiftPairTypeItem.setValue(0, Response[3], false);
                GridShiftPairTypes_ShiftPairTypes.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridShiftPairTypes_ShiftPairTypes.selectByKey(Response[3], 0, false);
                ShiftPairTypeItem = GridShiftPairTypes_ShiftPairTypes.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridShiftPairTypes_ShiftPairTypes.selectByKey(ObjShiftPairType_ShiftPairTypes.ID, 0, false);
                GridShiftPairTypes_ShiftPairTypes.deleteSelected();
                break;
        }
        if (CurrentPageState_ShiftPairTypes != 'Delete') {
            ShiftPairTypeItem.setValue(1, Active, false);
            ShiftPairTypeItem.setValue(2, CustomCode, false);
            ShiftPairTypeItem.setValue(3, Title, false);
            ShiftPairTypeItem.setValue(4, Descriptions, false);
        }
        GridShiftPairTypes_ShiftPairTypes.endUpdate();
    }
}

function ShowConnectionError_ShiftPairTypes() {
    var error = document.getElementById('hfErrorType_ShiftPairTypes').value;
    var errorBody = document.getElementById('hfConnectionError_ShiftPairTypes').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbShiftPairTypes_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvShiftPairTypesIntroduction_iFrame').src =parent.ModulePath + 'ShiftPairTypes.aspx';
}

function tlbItemHelp_TlbShiftPairTypes_onClick() {
    LoadHelpPage('tlbItemHelp_TlbShiftPairTypes');
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function tlbItemHelp_TlbShiftPairTypes_onClick()
{
    LoadHelpPage('tlbItemHelp_TlbShiftPairTypes');
}