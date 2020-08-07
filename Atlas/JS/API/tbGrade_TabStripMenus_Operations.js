
var CurrentPageState_Grade = 'View';
var ConfirmState_Grade = null;
var ObjGrade_Grade = null;

function GetBoxesHeaders_Grade() {
    document.getElementById('header_Grades_Grade').innerHTML = document.getElementById('hfheader_Grades_Grade').value;
    document.getElementById('header_tblGrade_Grade').innerHTML = document.getElementById('hfheader_tblGrade_Grade').value;
}

function GridGrade_Grade_onItemSelect(sender, e) {
    if (CurrentPageState_Grade != 'Add')
        NavigateGrade_Grade(e.get_item());
}

function NavigateGrade_Grade(selectedGradeItem) {
    if (selectedGradeItem != undefined) {
        document.getElementById('txtGradeName_Grade').value = selectedGradeItem.getMember('Name').get_text();
        document.getElementById('txtDescription_Grade').value = selectedGradeItem.getMember('Description').get_text();
    }
}


function tlbItemNew_TlbGrade_onClick() {
    ChangePageState_Grade('Add');
    ClearList_Grade();
    FocusOnFirstElement_Grade();
}

function tlbItemEdit_TlbGrade_onClick() {
    ChangePageState_Grade('Edit');
    FocusOnFirstElement_Grade();
}

function tlbItemDelete_TlbGrade_onClick() {
    ChangePageState_Grade('Delete');
}

function tlbItemSave_TlbGrade_onClick() {
    Grade_onSave();
}

function tlbItemCancel_TlbGrade_onClick() {
    ChangePageState_Grade('View');
    ClearList_Grade();
}

function tlbItemExit_TlbGrade_onClick() {
    ShowDialogConfirm('Exit');
}

function Grade_onSave() {
    if (CurrentPageState_Grade != 'Delete')
        UpdateGrade_Grade();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_Grade(str) {
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


function ClearList_Grade() {
    if (CurrentPageState_Grade != 'Edit') {
        document.getElementById('txtGradeName_Grade').value = '';
        document.getElementById('txtDescription_Grade').value = '';
        GridGrade_Grade.unSelectAll();
    }
}

function FocusOnFirstElement_Grade() {
    document.getElementById('txtGradeName_Grade').focus();
}

function ChangePageState_Grade(state) {
    CurrentPageState_Grade = state;
    SetActionMode_Grade(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbGrade.get_items().getItemById('tlbItemNew_TlbGrade') != null) {
            TlbGrade.get_items().getItemById('tlbItemNew_TlbGrade').set_enabled(false);
            TlbGrade.get_items().getItemById('tlbItemNew_TlbGrade').set_imageUrl('add_silver.png');
        }
        if (TlbGrade.get_items().getItemById('tlbItemEdit_TlbGrade') != null) {
            TlbGrade.get_items().getItemById('tlbItemEdit_TlbGrade').set_enabled(false);
            TlbGrade.get_items().getItemById('tlbItemEdit_TlbGrade').set_imageUrl('edit_silver.png');
        }
        if (TlbGrade.get_items().getItemById('tlbItemDelete_TlbGrade') != null) {
            TlbGrade.get_items().getItemById('tlbItemDelete_TlbGrade').set_enabled(false);
            TlbGrade.get_items().getItemById('tlbItemDelete_TlbGrade').set_imageUrl('remove_silver.png');
        }
        TlbGrade.get_items().getItemById('tlbItemSave_TlbGrade').set_enabled(true);
        TlbGrade.get_items().getItemById('tlbItemSave_TlbGrade').set_imageUrl('save.png');
        TlbGrade.get_items().getItemById('tlbItemCancel_TlbGrade').set_enabled(true);
        TlbGrade.get_items().getItemById('tlbItemCancel_TlbGrade').set_imageUrl('cancel.png');
        document.getElementById('txtGradeName_Grade').disabled = '';
        document.getElementById('txtDescription_Grade').disabled = '';
        if (state == 'Edit')
            NavigateGrade_Grade(GridGrade_Grade.getSelectedItems()[0]);
        if (state == 'Delete')
            Grade_onSave();
    }
    if (state == 'View') {
        if (TlbGrade.get_items().getItemById('tlbItemNew_TlbGrade') != null) {
            TlbGrade.get_items().getItemById('tlbItemNew_TlbGrade').set_enabled(true);
            TlbGrade.get_items().getItemById('tlbItemNew_TlbGrade').set_imageUrl('add.png');
        }
        if (TlbGrade.get_items().getItemById('tlbItemEdit_TlbGrade') != null) {
            TlbGrade.get_items().getItemById('tlbItemEdit_TlbGrade').set_enabled(true);
            TlbGrade.get_items().getItemById('tlbItemEdit_TlbGrade').set_imageUrl('edit.png');
        }
        if (TlbGrade.get_items().getItemById('tlbItemDelete_TlbGrade') != null) {
            TlbGrade.get_items().getItemById('tlbItemDelete_TlbGrade').set_enabled(true);
            TlbGrade.get_items().getItemById('tlbItemDelete_TlbGrade').set_imageUrl('remove.png');
        }
        TlbGrade.get_items().getItemById('tlbItemSave_TlbGrade').set_enabled(false);
        TlbGrade.get_items().getItemById('tlbItemSave_TlbGrade').set_imageUrl('save_silver.png');
        TlbGrade.get_items().getItemById('tlbItemCancel_TlbGrade').set_enabled(false);
        TlbGrade.get_items().getItemById('tlbItemCancel_TlbGrade').set_imageUrl('cancel_silver.png');
        document.getElementById('txtGradeName_Grade').disabled = 'disabled';
        document.getElementById('txtDescription_Grade').disabled = 'disabled';
    }
}

function SetActionMode_Grade(state) {
    document.getElementById('ActionMode_Grade').innerHTML = document.getElementById("hf" + state + "_Grade").value;
}

function Fill_GridGrade_Grade() {
    document.getElementById('loadingPanel_GridGrade_Grade').innerHTML = document.getElementById('hfloadingPanel_GridGrade_Grade').value;
    CallBack_GridGrade_Grade.callback();
}

function Refresh_GridGrade_Grade() {
    Fill_GridGrade_Grade();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Grade = confirmState;
    if (CurrentPageState_Grade == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Grade').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Grade').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Grade) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateGrade_Grade();
            break;
        case 'Exit':
            ClearList_Grade();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Grade('View');
}

function UpdateGrade_Grade() {
    ObjGrade_Grade = new Object();
    ObjGrade_Grade.Name = null;
    ObjGrade_Grade.Description = null;
    ObjGrade_Grade.ID = '0';
    var SelectedItems_GridGrade_Grade = GridGrade_Grade.getSelectedItems();
    if (SelectedItems_GridGrade_Grade.length > 0)
        ObjGrade_Grade.ID = SelectedItems_GridGrade_Grade[0].getMember("ID").get_text();

    if (CurrentPageState_Grade != 'Delete') {
        ObjGrade_Grade.Name = document.getElementById('txtGradeName_Grade').value;
        ObjGrade_Grade.Description = document.getElementById('txtDescription_Grade').value;
    }
    UpdateGrade_GradePage(CharToKeyCode_Grade(CurrentPageState_Grade), CharToKeyCode_Grade(ObjGrade_Grade.ID), CharToKeyCode_Grade(ObjGrade_Grade.Name), CharToKeyCode_Grade(ObjGrade_Grade.Description));
    DialogWaiting.Show();
}

function UpdateGrade_GradePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Grade').value;
            Response[1] = document.getElementById('hfConnectionError_Grade').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Grade();
            Grade_OnAfterUpdate(Response);
            ChangePageState_Grade('View');
        }
        else {
            if (CurrentPageState_Grade == 'Delete')
                ChangePageState_Grade('View');
        }
    }
}

function Grade_OnAfterUpdate(Response) {
    if (ObjGrade_Grade != null) {
        var GradeName = ObjGrade_Grade.Name;
        var GradeDescription = ObjGrade_Grade.Description;

        var GradeItem = null;
        GridGrade_Grade.beginUpdate();
        switch (CurrentPageState_Grade) {
            case 'Add':
                GradeItem = GridGrade_Grade.get_table().addEmptyRow(GridGrade_Grade.get_recordCount());
                GradeItem.setValue(0, Response[3], false);
                GridGrade_Grade.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridGrade_Grade.selectByKey(Response[3], 0, false);
                GradeItem = GridGrade_Grade.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridGrade_Grade.selectByKey(ObjGrade_Grade.ID, 0, false);
                GridGrade_Grade.deleteSelected();
                break;
        }
        if (CurrentPageState_Grade != 'Delete') {
            GradeItem.setValue(1, GradeName, false);
            GradeItem.setValue(2, GradeDescription, false);
        }
        GridGrade_Grade.endUpdate();
    }
}

function CallBack_GridGrade_Grade_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Grade').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridGrade_Grade();
    }
}

function GridGrade_Grade_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridGrade_Grade').innerHTML = '';
}

function CallBack_GridGrade_Grade_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridGrade_Grade').innerHTML = '';
    ShowConnectionError_Grade();
}

function ShowConnectionError_Grade() {
    var error = document.getElementById('hfErrorType_Grade').value;
    var errorBody = document.getElementById('hfConnectionError_Grade').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbGrade_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvGradeIntroduction_iFrame').src = parent.ModulePath + 'Grade.aspx';
}

function tlbItemHelp_TlbGrade_onClick() {
    LoadHelpPage('tlbItemHelp_TlbGrade');
}












