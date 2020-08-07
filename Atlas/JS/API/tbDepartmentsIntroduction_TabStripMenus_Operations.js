//DNN Note:------------------------------
var SelectedDepartmentType_DepartmentIntroduction = null;
//----------------------------------
var CurrentPageState_Departments = 'View';
var ConfirmState_Departments = null;
var ObjDepartment_Departments = null;
var CurrentPageTreeViewsObj = new Object();
var CurrentPageCombosCallBcakStateObj = new Object();
var box_DepartmentSearch_Departments_IsShown = false;
var DepartmentSelectedType_Departments = null;

function GetBoxesHeaders_Departments() {
    document.getElementById('header_Departments_DepartmentIntroduction').innerHTML = document.getElementById('hfheader_Departments_DepartmentIntroduction').value;
    document.getElementById('header_DepartmentDetails_DepartmentIntroduction').innerHTML = document.getElementById('hfheader_DepartmentDetails_DepartmentIntroduction').value;
}

function CacheTreeViewsSize_Departments() {
    CurrentPageTreeViewsObj.trvDepartmentsIntroduction_DepartmentIntroduction = document.getElementById('trvDepartmentsIntroduction_DepartmentIntroduction').clientWidth + 'px';
}

function trvDepartmentsIntroduction_DepartmentIntroduction_onNodeSelect(sender, e) {
    if (CurrentPageState_Departments != 'Add')
        NavigateDepartment_DepartmentIntroduction(e.get_node());
    DepartmentSelectedType_Departments = 'Normal';
}

function NavigateDepartment_DepartmentIntroduction(selectedDepartmentNode) {
    if (selectedDepartmentNode != undefined) {
        //DNN Note--------------------------
        cmbDepartmentType_DepartmentIntroduction.unSelect();
        //----------------------------------
        var departmentObj = selectedDepartmentNode.get_value();
        departmentObj = eval('(' + departmentObj + ')');
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').value = departmentObj.CustomCode != null && departmentObj.CustomCode != '' ? departmentObj.CustomCode : "";
        document.getElementById('txtDepartmentName_DepartmentIntroduction').value = departmentObj.Name;
        //DNN Note--------------------------
        SelectedDepartmentType_DepartmentIntroduction = new Object();
        SelectedDepartmentType_DepartmentIntroduction.ID = departmentObj.DepartmentTypeID != null && departmentObj.DepartmentTypeID != '' && departmentObj.DepartmentTypeID != '0' ? departmentObj.DepartmentTypeID : "";
        SelectedDepartmentType_DepartmentIntroduction.Name = document.getElementById('cmbDepartmentType_DepartmentIntroduction_Input').value = departmentObj.DepartmentTypeTitle != null && departmentObj.DepartmentTypeTitle != '' && departmentObj.DepartmentTypeTitle != '0' ? departmentObj.DepartmentTypeTitle : "";
        //----------------------------------
    }
}

function chbDepartmentCodeView_DepartmentIntroduction_onclick() {
    CallBack_trvDepartmentsIntroduction_DepartmentIntroduction.callback('DepartmentCodeView', "" + document.getElementById('chbDepartmentCodeView_DepartmentIntroduction').checked + "");
}

function tlbItemNew_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('Add');
    ClearList_Departments();
    FocusOnFirstElement_Departments();
}

function tlbItemHelp_TlbDepartmentsIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbDepartmentsIntroduction');
}

function tlbItemEdit_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('Edit');
    FocusOnFirstElement_Departments();
}

function tlbItemDelete_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('Delete');
}

function tlbItemSave_TlbDepartmentsIntroduction_onClick() {
    Department_onSave();
}

function Department_onSave() {
    if (CurrentPageState_Departments != 'Delete')
        UpdateDepartment_Departments();
    else
        ShowDialogConfirm('Delete');
}

function UpdateDepartment_Departments() {
    ObjDepartment_Departments = new Object();
    ObjDepartment_Departments.CustomCode = null;
    ObjDepartment_Departments.Name = null;
    ObjDepartment_Departments.DepartmentTypeID = '0';
    ObjDepartment_Departments.DepartmentTypeTitle = null;
    ObjDepartment_Departments.SelectedID = '0';
    switch (DepartmentSelectedType_Departments) {
        case 'Normal':
            var SelectedDepartmentNode_Departments = trvDepartmentsIntroduction_DepartmentIntroduction.get_selectedNode();
            if (SelectedDepartmentNode_Departments != undefined)
                ObjDepartment_Departments.SelectedID = SelectedDepartmentNode_Departments.get_id();
            break;
        case 'Search':
            var selectedItem_cmbDepartmentSearchResult_Departments = cmbDepartmentSearchResult_Departments.getSelectedItem();
            if (selectedItem_cmbDepartmentSearchResult_Departments != undefined && selectedItem_cmbDepartmentSearchResult_Departments != null) {
                ObjDepartment_Departments.SelectedID = selectedItem_cmbDepartmentSearchResult_Departments.get_id();
            }
            break;
    }
    if (CurrentPageState_Departments != 'Delete') {
        ObjDepartment_Departments.CustomCode = document.getElementById('txtDepartmentCode_DepartmentIntroduction').value;
        ObjDepartment_Departments.Name = document.getElementById('txtDepartmentName_DepartmentIntroduction').value;
        if (cmbDepartmentType_DepartmentIntroduction.getSelectedItem() != undefined) {
            ObjDepartment_Departments.DepartmentTypeID = cmbDepartmentType_DepartmentIntroduction.getSelectedItem().get_value();
            ObjDepartment_Departments.DepartmentTypeTitle = cmbDepartmentType_DepartmentIntroduction.getSelectedItem().get_text();
        }
        else {
            if (SelectedDepartmentType_DepartmentIntroduction != null) {
                ObjDepartment_Departments.DepartmentTypeID = SelectedDepartmentType_DepartmentIntroduction.ID;
                ObjDepartment_Departments.DepartmentTypeTitle = SelectedDepartmentType_DepartmentIntroduction.Name;
            }
        }
    }
    UpdateDepartment_DepartmentsPage(
        CharToKeyCode_Departments(CurrentPageState_Departments),
        CharToKeyCode_Departments(ObjDepartment_Departments.SelectedID),
        CharToKeyCode_Departments(ObjDepartment_Departments.CustomCode),
        CharToKeyCode_Departments(ObjDepartment_Departments.Name),
        CharToKeyCode_Departments(ObjDepartment_Departments.DepartmentTypeID));
    DialogWaiting.Show();
}

function Department_OnAfterUpdate(Response) {
    var DepartmentNodeText = ObjDepartment_Departments.Name;
    var DepartmentNodeValue = '{"Name":"' + ObjDepartment_Departments.Name + '","CustomCode":"' + ObjDepartment_Departments.CustomCode + '","DepartmentTypeID":"' + ObjDepartment_Departments.DepartmentTypeID + '","DepartmentTypeTitle":"' + ObjDepartment_Departments.DepartmentTypeTitle + '"}';
    if (document.getElementById('chbDepartmentCodeView_DepartmentIntroduction').checked)
        DepartmentNodeText = ObjDepartment_Departments.CustomCode + ' - ' + DepartmentNodeText;

    trvDepartmentsIntroduction_DepartmentIntroduction.beginUpdate();
    switch (CurrentPageState_Departments) {
        case 'Add':
            var newDepartmentNode = new ComponentArt.Web.UI.TreeViewNode();
            newDepartmentNode.set_text(DepartmentNodeText);
            newDepartmentNode.set_value(DepartmentNodeValue);
            newDepartmentNode.set_id(Response[3]);
            newDepartmentNode.set_imageUrl('Images/TreeView/folder.gif');
            trvDepartmentsIntroduction_DepartmentIntroduction.findNodeById(ObjDepartment_Departments.SelectedID).get_nodes().add(newDepartmentNode);
            trvDepartmentsIntroduction_DepartmentIntroduction.selectNodeById(ObjDepartment_Departments.SelectedID);
            break;
        case 'Edit':
            var selectedDepartmentNode = trvDepartmentsIntroduction_DepartmentIntroduction.findNodeById(Response[3]);
            selectedDepartmentNode.set_text(DepartmentNodeText);
            selectedDepartmentNode.set_value(DepartmentNodeValue);
            trvDepartmentsIntroduction_DepartmentIntroduction.selectNodeById(Response[3]);
            var selectedItem_cmbDepartmentSearchResult_Departments = cmbDepartmentSearchResult_Departments.findItemByProperty('Id', Response[3]);
            if (selectedItem_cmbDepartmentSearchResult_Departments != null && selectedItem_cmbDepartmentSearchResult_Departments != undefined) {
                cmbDepartmentSearchResult_Departments.beginUpdate();
                selectedItem_cmbDepartmentSearchResult_Departments.set_text(DepartmentNodeText);
                selectedItem_cmbDepartmentSearchResult_Departments.set_value(DepartmentNodeValue);
                cmbDepartmentSearchResult_Departments.endUpdate();
            }
            break;
        case 'Delete':
            trvDepartmentsIntroduction_DepartmentIntroduction.findNodeById(ObjDepartment_Departments.SelectedID).remove();
            var selectedItem_cmbDepartmentSearchResult_Departments = cmbDepartmentSearchResult_Departments.findItemByProperty('Id', Response[3]);
            if (selectedItem_cmbDepartmentSearchResult_Departments != null && selectedItem_cmbDepartmentSearchResult_Departments != undefined) {
                var index = selectedItem_cmbDepartmentSearchResult_Departments.Index;
                cmbDepartmentSearchResult_Departments.beginUpdate();
                cmbDepartmentSearchResult_Departments.removeItemAt(index);
                cmbDepartmentSearchResult_Departments.endUpdate();
            }
            break;

    }
    trvDepartmentsIntroduction_DepartmentIntroduction.endUpdate();
    if (CurrentPageState_Departments == 'Add')
        trvDepartmentsIntroduction_DepartmentIntroduction.get_selectedNode().expand();
    Resize_trvDepartmentsIntroduction_DepartmentIntroduction();
    ChangeDirection_trvDepartmentsIntroduction_DepartmentIntroduction();
}

function tlbItemCancel_TlbDepartmentsIntroduction_onClick() {
    ChangePageState_Departments('View');
    ClearList_Departments();
}

function tlbItemExit_TlbDepartmentsIntroduction_onClick() {
    ShowDialogConfirm('Exit');
}

function ChangePageState_Departments(state) {
    CurrentPageState_Departments = state;
    SetActionMode_Departments(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_enabled(false);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_imageUrl('add_silver.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_enabled(false);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_imageUrl('edit_silver.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_enabled(false);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_imageUrl('remove_silver.png');
        }
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_enabled(true);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_imageUrl('save.png');
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_enabled(true);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_imageUrl('cancel.png');
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').disabled = '';
        document.getElementById('txtDepartmentName_DepartmentIntroduction').disabled = '';
        cmbDepartmentType_DepartmentIntroduction.enable();
        if (state == 'Edit') {
            switch (DepartmentSelectedType_Departments) {
                case 'Normal':
                    NavigateDepartment_DepartmentIntroduction(trvDepartmentsIntroduction_DepartmentIntroduction.get_selectedNode());
                    break;
                case 'Search':
                    NavigateDepartment_DepartmentIntroduction(cmbDepartmentSearchResult_Departments.getSelectedItem());
                    break;
            }
        }
        if (state == 'Delete')
            Department_onSave();
    }
    if (state == 'View') {
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_enabled(true);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemNew_TlbDepartmentsIntroduction').set_imageUrl('add.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_enabled(true);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemEdit_TlbDepartmentsIntroduction').set_imageUrl('edit.png');
        }
        if (TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction') != null) {
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_enabled(true);
            TlbDepartmentsIntroduction.get_items().getItemById('tlbItemDelete_TlbDepartmentsIntroduction').set_imageUrl('remove.png');
        }
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_enabled(false);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemSave_TlbDepartmentsIntroduction').set_imageUrl('save_silver.png');
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_enabled(false);
        TlbDepartmentsIntroduction.get_items().getItemById('tlbItemCancel_TlbDepartmentsIntroduction').set_imageUrl('cancel_silver.png');
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').disabled = 'disabled';
        document.getElementById('txtDepartmentName_DepartmentIntroduction').disabled = 'disabled';
        cmbDepartmentType_DepartmentIntroduction.disable();
    }
}

function SetActionMode_Departments(state) {
    document.getElementById('ActionMode_DepartmentForm').innerHTML = document.getElementById("hf" + state + "_Departments").value;
}

function ClearList_Departments() {
    if (CurrentPageState_Departments != 'Edit') {
        document.getElementById('txtDepartmentCode_DepartmentIntroduction').value = '';
        document.getElementById('txtDepartmentName_DepartmentIntroduction').value = '';
        document.getElementById('cmbDepartmentType_DepartmentIntroduction_Input').value = document.getElementById('hfcmbAlarm_Department').value;
        cmbDepartmentType_DepartmentIntroduction.unSelect();
        SelectedDepartmentType_DepartmentIntroduction = null;
    }
}

function FocusOnFirstElement_Departments() {
    document.getElementById('txtDepartmentCode_DepartmentIntroduction').focus();
}

function CharToKeyCode_Departments(str) {
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

function Fill_trvDepartmentsIntroduction_DepartmentIntroduction() {
    document.getElementById('loadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').value);
    CallBack_trvDepartmentsIntroduction_DepartmentIntroduction.callback('DepartmentCodeView', "" + document.getElementById('chbDepartmentCodeView_DepartmentIntroduction').checked + "");
}

function CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Departments').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvDepartmentsIntroduction_DepartmentIntroduction();
    }
    else {
        Resize_trvDepartmentsIntroduction_DepartmentIntroduction();
        ChangeDirection_trvDepartmentsIntroduction_DepartmentIntroduction();
    }
}

function Refresh_trvDepartmentsIntroduction_DepartmentIntroduction() {
    Fill_trvDepartmentsIntroduction_DepartmentIntroduction();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Departments = confirmState;
    if (CurrentPageState_Departments == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Departments').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Departments').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Departments) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateDepartment_Departments();
            break;
        case 'Exit':
            ClearList_Departments();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Departments('View');
}

function trvDepartmentsIntroduction_DepartmentIntroduction_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').innerHTML = "";
}

function UpdateDepartment_DepartmentsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Departments').value;
            Response[1] = document.getElementById('hfConnectionError_Departments').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            // ClearSearchDetails_DepartmentIntroduction();
            Clear_cmbDepartmentSearchResult_Departments();
            Department_OnAfterUpdate(Response);
            ClearList_Departments();
            ChangePageState_Departments('View');
        }
        else {
            if (CurrentPageState_Departments == 'Delete')
                ChangePageState_Departments('View');
        }
    }
}
function Clear_cmbDepartmentSearchResult_Departments() {
    cmbDepartmentSearchResult_Departments.beginUpdate();
    cmbDepartmentSearchResult_Departments.removeAll();
    cmbDepartmentSearchResult_Departments.endUpdate();
}


function CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction').innerHTML = '';
    ShowConnectionError_DepartmentIntroduction();
}

function ShowConnectionError_DepartmentIntroduction() {
    var error = document.getElementById('hfErrorType_Departments').value;
    var errorBody = document.getElementById('hfConnectionError_Departments').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbDepartmentsIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvDepartmentsIntroduction_iFrame').src = parent.ModulePath + 'Departments.aspx';
}

function trvDepartmentsIntroduction_DepartmentIntroduction_onNodeExpand(sender, e) {
    Resize_trvDepartmentsIntroduction_DepartmentIntroduction();
    ChangeDirection_trvDepartmentsIntroduction_DepartmentIntroduction();
}

function Resize_trvDepartmentsIntroduction_DepartmentIntroduction() {
    document.getElementById('trvDepartmentsIntroduction_DepartmentIntroduction').style.width = CurrentPageTreeViewsObj.trvDepartmentsIntroduction_DepartmentIntroduction;
}

function ChangeDirection_trvDepartmentsIntroduction_DepartmentIntroduction() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvDepartmentsIntroduction_DepartmentIntroduction').style.direction = 'ltr';
}


function cmbDepartmentSearchResult_Departments_onChange(sender, e) {
    var selectedItem_cmbDepartmentSearchResult_Departments = cmbDepartmentSearchResult_Departments.getSelectedItem();
    if (selectedItem_cmbDepartmentSearchResult_Departments != undefined && selectedItem_cmbDepartmentSearchResult_Departments != null) {
        DepartmentSelectedType_Departments = 'Search';
        NavigateDepartment_DepartmentIntroduction(selectedItem_cmbDepartmentSearchResult_Departments);
    }
}


function CallBack_cmbDepartmentSearchResult_Departments_onBeforeCallback(sender, e) {
    cmbDepartmentSearchResult_Departments.dispose();
}

function CallBack_cmbDepartmentSearchResult_Departments_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DepartmentSearchResult_Departments').value;
    if (error == "") {
        cmbDepartmentSearchResult_Departments.expand();
        ChangeControlDirection_Departments('cmbDepartmentSearchResult_Departments_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function ChangeControlDirection_Departments(ctrl) {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById(ctrl).style.direction = direction;
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbDepartmentSearchResult_Departments_onCallbackError(sender, e) {
    ShowConnectionError_DepartmentIntroduction();
}

function tlbItemDepartmentSearch_TlbDepartmentSearch_Departments_onClick() {
    Fill_cmbDepartmentSearchResult_Departments();
}

function Fill_cmbDepartmentSearchResult_Departments() {
    ComboBox_onBeforeLoadData('cmbDepartmentSearchResult_Departments');
    var SearchTerm = document.getElementById('txtSearchTerm_Departments').value;
    CallBack_cmbDepartmentSearchResult_Departments.callback(CharToKeyCode_Departments(SearchTerm));
}
function txtSerchTerm_Departments_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemDepartmentSearch_TlbDepartmentSearch_Departments_onClick();
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

//DNN Note:--------------------------------------

function cmbDepartmentType_DepartmentIntroduction_onExpand(sender, e) {
    if (cmbDepartmentType_DepartmentIntroduction.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartmentType_DepartmentIntroduction == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartmentType_DepartmentIntroduction = true;
        Fill_cmbDepartmentType_DepartmentIntroduction();
    }
}

function Fill_cmbDepartmentType_DepartmentIntroduction() {
    ComboBox_onBeforeLoadData('cmbDepartmentType_DepartmentIntroduction');
    CallBack_cmbDepartmentType_DepartmentIntroduction.callback();
}

function cmbDepartmentType_DepartmentIntroduction_onCollapse(sender, e) {
    if (cmbDepartmentType_DepartmentIntroduction.getSelectedItem() == undefined && SelectedDepartmentType_DepartmentIntroduction != null) {
        if (SelectedDepartmentType_DepartmentIntroduction.ID == null || SelectedDepartmentType_DepartmentIntroduction.ID == undefined)
            document.getElementById('cmbDepartmentType_DepartmentIntroduction_Input').value = document.getElementById('hfcmbAlarm_Department').value;
        else
            if (SelectedDepartmentType_DepartmentIntroduction.ID != null && SelectedDepartmentType_DepartmentIntroduction.ID != undefined)
                document.getElementById('cmbDepartmentType_DepartmentIntroduction_Input').value = SelectedDepartmentType_DepartmentIntroduction.Name;
    }
}

function CallBack_cmbDepartmentType_DepartmentIntroduction_onBeforeCallback(sender, e) {
    cmbDepartmentType_DepartmentIntroduction.dispose();
}

function CallBack_cmbDepartmentType_DepartmentIntroduction_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DepartmentType').value;
    if (error == "") {
        document.getElementById('cmbDepartmentType_DepartmentIntroduction_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDepartmentType_DepartmentIntroduction_DropImage').mousedown();
        cmbDepartmentType_DepartmentIntroduction.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDepartmentType_DepartmentIntroduction_DropDown').style.display = 'none';
    }
}

