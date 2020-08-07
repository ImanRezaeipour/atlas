
var CurrentPageState_Contract = 'View';
var ConfirmState_Contract = null;
var ObjContract_Contract = null;
var CurrentPageIndex_GridContracts_Contract = 0;
var LoadState_Contract = 'Normal';
var CurrentPageCombosCallBcakStateObj = new Object();
var SelectedContractor_Contract = null;
function GetBoxesHeaders_Contract() {
    document.getElementById('header_Contracts_Contract').innerHTML = document.getElementById('hfheader_Contracts_Contract').value;
    document.getElementById('header_tblContract_Contract').innerHTML = document.getElementById('hfheader_tblContracts_Contract').value;
    document.getElementById('footer_GridContracts_Contract').innerHTML = document.getElementById('hffooter_GridContracts_Contract').value;
}
function SetHorizontalScrollingDirection_GridContracts_Contract_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridContracts_Contract').style.direction = "ltr";
}
function GridContracts_Contract_onItemSelect(sender, e) {
    if (CurrentPageState_Contract != 'Add')
        NavigateContract_Contract(e.get_item());
}
function UpdateFeatures_GridContracts_Contract() {
    var ContractCount = parseInt(document.getElementById('hfContractsCount_Contract').value);
    var ContractPageCount = parseInt(document.getElementById('hfContractsPageCount_Contract').value);
    var ContractPageSize = parseInt(document.getElementById('hfContractsPageSize_Contract').value);
    var Lag = 0;
    switch (CurrentPageState_Contract) {
        case 'Add':
            Lag = Lag + 1;
            break;
        case 'Delete':
            Lag = Lag - 1;
            break;
    }
    if ((ContractCount > 0 && CurrentPageState_Contract == 'Delete') || CurrentPageState_Contract == 'Add') {
        ContractCount = ContractCount + Lag;
        var divRem = mod(ContractCount, ContractPageSize);
        switch (CurrentPageState_Contract) {
            case 'Add':
                if (GridContracts_Contract.get_table().getRowCount() > ContractPageSize) {
                    ContractPageCount = ContractPageCount + Lag;
                    CurrentPageIndex_GridContracts_Contract = CurrentPageIndex_GridContracts_Contract + Lag;
                }
                break;
            case 'Delete':
                if (divRem == 0) {
                    ContractPageCount = ContractPageCount + Lag;
                    if (CurrentPageIndex_GridContracts_Contract == ContractPageCount) {
                        CurrentPageIndex_GridContracts_Contract = CurrentPageIndex_GridContracts_Contract + Lag >= 0 ? CurrentPageIndex_GridContracts_Contract + Lag : 0;
                    }
                }
                break;
        }
        SetPageIndex_GridContracts_Contract(CurrentPageIndex_GridContracts_Contract);
        document.getElementById('hfContractsCount_Contract').value = ContractCount.toString();
        document.getElementById('hfContractsPageCount_Contract').value = ContractPageCount.toString();
        Changefooter_GridContracts_Contract();
    }
}
function GridContracts_Contract_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridContracts_Contract').innerHTML = '';
}
function NavigateContract_Contract(selectedContractItem) {
    if (selectedContractItem != undefined) {
        document.getElementById('txtTitle_Contract').value = selectedContractItem.getMember('Title').get_text();
        document.getElementById('txtCode_Contract').value = selectedContractItem.getMember('Code').get_text();
        document.getElementById('cmbContractor_Contract_Input').value = selectedContractItem.getMember('Contractor.Name').get_text();
        document.getElementById('txtDescription_Contract').value = selectedContractItem.getMember('Description').get_text();
        SelectedContractor_Contract = new Object();
        SelectedContractor_Contract.ID = selectedContractItem.getMember('Contractor.ID').get_text();
        SelectedContractor_Contract.Name = selectedContractItem.getMember('Contractor.Name').get_text();
        if (selectedContractItem.getMember('IsDefault').get_text() == 'false')
            document.getElementById('chbContractIsDefault_Contract').checked = false;
        else
            document.getElementById('chbContractIsDefault_Contract').checked = true;
    }
}

function tlbItemNew_TlbContract_onClick() {
    ChangePageState_Contract('Add');
    ClearList_Contract();
    FocusOnFirstElement_Contract();
}

function tlbItemEdit_TlbContract_onClick() {
    ChangePageState_Contract('Edit');
    FocusOnFirstElement_Contract();
}

function tlbItemDelete_TlbContract_onClick() {
    ChangePageState_Contract('Delete');
}

function tlbItemSave_TlbContract_onClick() {
    Contract_onSave();
}

function tlbItemCancel_TlbContract_onClick() {
    ChangePageState_Contract('View');
    ClearList_Contract();
}

function tlbItemExit_TlbContract_onClick() {
    ShowDialogConfirm('Exit');
}

function Contract_onSave() {
    if (CurrentPageState_Contract != 'Delete')
        UpdateContract_Contract();
    else
        ShowDialogConfirm('Delete');
}

function CharToKeyCode_Contract(str) {
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


function ClearList_Contract() {
    if (CurrentPageState_Contract != 'Edit') {
        document.getElementById('txtTitle_Contract').value = '';
        document.getElementById('txtCode_Contract').value = '';
        document.getElementById('cmbContractor_Contract_Input').value =  document.getElementById('hfcmbAlarm_Contract').value;
        cmbContractor_Contract.unSelect();
        document.getElementById('txtDescription_Contract').value = '';
        GridContracts_Contract.unSelectAll();
        SelectedContractor_Contract = null;
        document.getElementById('chbContractIsDefault_Contract').checked = '';
    }
}

function FocusOnFirstElement_Contract() {
    document.getElementById('txtTitle_Contract').focus();
}

function ChangePageState_Contract(state) {
    CurrentPageState_Contract = state;
    SetActionMode_Contract(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbContract.get_items().getItemById('tlbItemNew_TlbContract') != null) {
            TlbContract.get_items().getItemById('tlbItemNew_TlbContract').set_enabled(false);
            TlbContract.get_items().getItemById('tlbItemNew_TlbContract').set_imageUrl('add_silver.png');
        }
        if (TlbContract.get_items().getItemById('tlbItemEdit_TlbContract') != null) {
            TlbContract.get_items().getItemById('tlbItemEdit_TlbContract').set_enabled(false);
            TlbContract.get_items().getItemById('tlbItemEdit_TlbContract').set_imageUrl('edit_silver.png');
        }
        if (TlbContract.get_items().getItemById('tlbItemDelete_TlbContract') != null) {
            TlbContract.get_items().getItemById('tlbItemDelete_TlbContract').set_enabled(false);
            TlbContract.get_items().getItemById('tlbItemDelete_TlbContract').set_imageUrl('remove_silver.png');
        }
        TlbContract.get_items().getItemById('tlbItemSave_TlbContract').set_enabled(true);
        TlbContract.get_items().getItemById('tlbItemSave_TlbContract').set_imageUrl('save.png');
        TlbContract.get_items().getItemById('tlbItemCancel_TlbContract').set_enabled(true);
        TlbContract.get_items().getItemById('tlbItemCancel_TlbContract').set_imageUrl('cancel.png');
        document.getElementById('txtTitle_Contract').disabled = '';
        document.getElementById('txtCode_Contract').disabled = '';
        document.getElementById('txtDescription_Contract').disabled = '';
        document.getElementById('chbContractIsDefault_Contract').disabled = '';
        cmbContractor_Contract.enable();
        if (state == 'Edit')
            NavigateContract_Contract(GridContracts_Contract.getSelectedItems()[0]);
        if (state == 'Delete')
            Contract_onSave();
    }
    if (state == 'View') {
        if (TlbContract.get_items().getItemById('tlbItemNew_TlbContract') != null) {
            TlbContract.get_items().getItemById('tlbItemNew_TlbContract').set_enabled(true);
            TlbContract.get_items().getItemById('tlbItemNew_TlbContract').set_imageUrl('add.png');
        }
        if (TlbContract.get_items().getItemById('tlbItemEdit_TlbContract') != null) {
            TlbContract.get_items().getItemById('tlbItemEdit_TlbContract').set_enabled(true);
            TlbContract.get_items().getItemById('tlbItemEdit_TlbContract').set_imageUrl('edit.png');
        }
        if (TlbContract.get_items().getItemById('tlbItemDelete_TlbContract') != null) {
            TlbContract.get_items().getItemById('tlbItemDelete_TlbContract').set_enabled(true);
            TlbContract.get_items().getItemById('tlbItemDelete_TlbContract').set_imageUrl('remove.png');
        }
        TlbContract.get_items().getItemById('tlbItemSave_TlbContract').set_enabled(false);
        TlbContract.get_items().getItemById('tlbItemSave_TlbContract').set_imageUrl('save_silver.png');
        TlbContract.get_items().getItemById('tlbItemCancel_TlbContract').set_enabled(false);
        TlbContract.get_items().getItemById('tlbItemCancel_TlbContract').set_imageUrl('cancel_silver.png');
        document.getElementById('txtTitle_Contract').disabled = 'disabled';
        document.getElementById('txtCode_Contract').disabled = 'disabled';
        document.getElementById('txtDescription_Contract').disabled = 'disabled';
        document.getElementById('chbContractIsDefault_Contract').disabled = 'disabled';
        cmbContractor_Contract.disable();
    }
}



function Fill_GridContracts_Contract(pageIndex) {
    document.getElementById('loadingPanel_GridContracts_Contract').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridContracts_Contract').value);
    var pageSize = parseInt(document.getElementById('hfContractsPageSize_Contract').value);
    var searchTerm = '';
    switch (LoadState_Contract) {
        case 'Normal':
            break;
        case 'Search':
            searchTerm = document.getElementById('txtSearchTerm_Contract').value;
            break;
        default:
            break;
    }
    
    //if (box_UserSearch_Users_IsShown) {
    //    if (cmbSearchField_Contract.getSelectedItem() != undefined)
    //        searchKey = cmbSearchField_Contract.getSelectedItem().get_value();
        
    //}
    CallBack_GridContracts_Contract.callback(CharToKeyCode_Contract(LoadState_Contract), CharToKeyCode_Contract(pageSize.toString()), CharToKeyCode_Contract(pageIndex.toString()), CharToKeyCode_Contract(searchTerm));
}


function ChangeLoadState_GridContracts_Contract(state) {
    LoadState_Contract = state;
    SetPageIndex_GridContracts_Contract(0);
}


function ShowDialogConfirm(confirmState) {
    ConfirmState_Contract = confirmState;
    if (CurrentPageState_Contract == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Contract').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Contract').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Contract) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateContract_Contract();
            break;
        case 'Exit':
            ClearList_Contract();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Contract('View');
}

function UpdateContract_Contract() {
    ObjContract_Contract = new Object();
    ObjContract_Contract.Title = null;
    ObjContract_Contract.Code = null;
    ObjContract_Contract.ContractorID = '0';
    ObjContract_Contract.Description = null;
    ObjContract_Contract.ID = '0';
    ObjContract_Contract.IsDefault = null;
    var SelectedItems_GridContracts_Contract = GridContracts_Contract.getSelectedItems();
    if (SelectedItems_GridContracts_Contract.length > 0)
        ObjContract_Contract.ID = SelectedItems_GridContracts_Contract[0].getMember("ID").get_text();

    if (CurrentPageState_Contract != 'Delete') {
        ObjContract_Contract.Title = document.getElementById('txtTitle_Contract').value;
        ObjContract_Contract.Code = document.getElementById('txtCode_Contract').value;
        ObjContract_Contract.Description = document.getElementById('txtDescription_Contract').value;
        if (cmbContractor_Contract.getSelectedItem() != undefined) {
            ObjContract_Contract.ContractorID = cmbContractor_Contract.getSelectedItem().get_value();
            ObjContract_Contract.ContractorName = cmbContractor_Contract.getSelectedItem().get_text();
        }
        else {
            if (SelectedContractor_Contract != null) {
                ObjContract_Contract.ContractorID = SelectedContractor_Contract.ID;
                ObjContract_Contract.ContractorName = SelectedContractor_Contract.Name;
            }
        }
    }
    ObjContract_Contract.IsDefault = (document.getElementById('chbContractIsDefault_Contract').checked).toString();
    UpdateContract_ContractPage(CharToKeyCode_Contract(CurrentPageState_Contract), CharToKeyCode_Contract(ObjContract_Contract.ID), CharToKeyCode_Contract(ObjContract_Contract.Title), CharToKeyCode_Contract(ObjContract_Contract.Code), CharToKeyCode_Contract(ObjContract_Contract.ContractorID), CharToKeyCode_Contract(ObjContract_Contract.Description),CharToKeyCode_Contract(ObjContract_Contract.IsDefault));
    DialogWaiting.Show();
}

function UpdateContract_ContractPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Contract').value;
            Response[1] = document.getElementById('hfConnectionError_Contract').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Contract();
            Contract_OnAfterUpdate(Response);
            ChangePageState_Contract('View');
        }
        else {
            if (CurrentPageState_Contract == 'Delete')
                ChangePageState_Contract('View');
        }
    }
}

function Contract_OnAfterUpdate(Response) {
    if (ObjContract_Contract != null) {
        var Title = ObjContract_Contract.Title;
        var Code = ObjContract_Contract.Code;
        var ContractorID = ObjContract_Contract.ContractorID;
        var ContractorName = ObjContract_Contract.ContractorName;
        var Description = ObjContract_Contract.Description;
        var IsDefault = ObjContract_Contract.IsDefault;
        var ContractItem = null;
        var ChangedContractItem = null;
        var ContractIdChangedIsDefault = Response[4];
        GridContracts_Contract.beginUpdate();
        if (ContractIdChangedIsDefault != 0)
        {
            ChangedContractItem = GridContracts_Contract.getItemFromKey(0, Response[4]);
            ChangedContractItem.setValue(6, false, false);
        }
        switch (CurrentPageState_Contract) {
            case 'Add':
                ContractItem = GridContracts_Contract.get_table().addEmptyRow(GridContracts_Contract.get_recordCount());
                ContractItem.setValue(0, Response[3], false);
                GridContracts_Contract.selectByKey(Response[3], 0, false);
                UpdateFeatures_GridContract_Contract();
                break;
            case 'Edit':
                GridContracts_Contract.selectByKey(Response[3], 0, false);
                ContractItem = GridContracts_Contract.getItemFromKey(0, Response[3]);
                UpdateFeatures_GridContract_Contract();
                break;
            case 'Delete':
                GridContracts_Contract.selectByKey(ObjContract_Contract.ID, 0, false);
                GridContracts_Contract.deleteSelected();
                UpdateFeatures_GridContract_Contract();
                break;
        }
        if (CurrentPageState_Contract != 'Delete') {
            ContractItem.setValue(1, Title, false);
            ContractItem.setValue(2, Code, false);
            ContractItem.setValue(3, ContractorName, false);
            ContractItem.setValue(4, Description, false);
            ContractItem.setValue(5, ContractorID, false);
            ContractItem.setValue(6, IsDefault, false);
        }
        GridContracts_Contract.endUpdate();
    }
}
function UpdateFeatures_GridContract_Contract() {
    var ContractsCount = parseInt(document.getElementById('hfContractsCount_Contract').value);
    var ContractsPageCount = parseInt(document.getElementById('hfContractsPageCount_Contract').value);
    var ContractsPageSize = parseInt(document.getElementById('hfContractsPageSize_Contract').value);
    var Lag = 0;
    switch (CurrentPageState_Contract) {
        case 'Add':
            Lag = Lag + 1;
            break;
        case 'Delete':
            Lag = Lag - 1;
            break;
    }
    if ((ContractsCount > 0 && CurrentPageState_Contract == 'Delete') || CurrentPageState_Contract == 'Add') {
        ContractsCount = ContractsCount + Lag;
        var divRem = mod(ContractsCount, ContractsPageSize);
        switch (CurrentPageState_Contract) {
            case 'Add':
                if (GridContracts_Contract.get_table().getRowCount() > ContractsPageSize) {
                    ContractsPageCount = ContractsPageCount + Lag;
                    CurrentPageIndex_GridContracts_Contract = CurrentPageIndex_GridContracts_Contract + Lag;
                }
                break;
            case 'Delete':
                if (divRem == 0) {
                    ContractsPageCount = ContractsPageCount + Lag;
                    if (CurrentPageIndex_GridContracts_Contract == ContractsPageCount) {
                        CurrentPageIndex_GridContracts_Contract = CurrentPageIndex_GridContracts_Contract + Lag >= 0 ? CurrentPageIndex_GridContracts_Contract + Lag : 0;
                    }
                }
                break;
        }
        SetPageIndex_GridContracts_Contract(CurrentPageIndex_GridContracts_Contract);
        document.getElementById('hfContractsCount_Contract').value = ContractsCount.toString();
        document.getElementById('hfContractsPageCount_Contract').value = ContractsPageCount.toString();
        Changefooter_GridContracts_Contract();
    }
    if ((ContractsCount > 0 && CurrentPageState_Contract == 'Edit')) {
        SetPageIndex_GridContracts_Contract(CurrentPageIndex_GridContracts_Contract);
    }
}
function mod(a, b) {
    return a - (b * Math.floor(a / b));
}
function CallBack_GridContracts_Contract_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Contract').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridContracts_Contract(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else
        Changefooter_GridContracts_Contract();
}
function Changefooter_GridContracts_Contract() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridContracts_Contract').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfContractsPageCount_Contract').value) > 0 ? CurrentPageIndex_GridContracts_Contract + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfContractsPageCount_Contract').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridContracts_Contract').innerHTML = retfooterVal;
}

function GridContracts_Contract_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridContracts_Contract').innerHTML = '';
}

function CallBack_GridContracts_Contract_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridContracts_Contract').innerHTML = '';
    ShowConnectionError_Contract();
}

function ShowConnectionError_Contract() {
    var error = document.getElementById('hfErrorType_Contract').value;
    var errorBody = document.getElementById('hfConnectionError_Contract').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemFormReconstruction_TlbContract_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvContractIntroduction_iFrame').src = 'Contract.aspx';
}

function tlbItemHelp_TlbContract_onClick() {
    LoadHelpPage('tlbItemHelp_TlbContract');
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function CallBcak_cmbContractor_Contract_onCallbackError(sender, e) {
    ShowConnectionError_Contract();
}

function cmbContractor_Contract_onExpand(sender, e) {
    if (cmbContractor_Contract.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbContractor == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbContractor = true;
        Fill_cmbContractor_Contract();
    }
}
function Fill_cmbContractor_Contract() {
    ComboBox_onBeforeLoadData('cmbContractor_Contract');
    CallBcak_cmbContractor_Contract.callback();
}

function cmbContractor_Contract_onCollapse(sender, e) {
    if (cmbContractor_Contract.getSelectedItem() == undefined && SelectedContractor_Contract != null) {
        if (SelectedContractor_Contract.ID == null || SelectedContractor_Contract.ID == undefined)
            document.getElementById('cmbContractor_Contract_Input').value = document.getElementById('hfcmbAlarm_Contract').value;
        else {
            if (SelectedContractor_Contract.ID != null && SelectedContractor_Contract.ID != undefined)
                document.getElementById('cmbContractor_Contract_Input').value = SelectedContractor_Contract.Name;
        }
    }
}

function CallBcak_cmbContractor_Contract_onBeforeCallback(sender, e) {
    cmbContractor_Contract.dispose();
}

function CallBcak_cmbContractor_Contract_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Contractor').value;
    if (error == "") {
        document.getElementById('cmbContractor_Contract_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbContractor_Contract_DropImage').mousedown();
        cmbContractor_Contract.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbContractor_Contract_DropDown').style.display = 'none';
    }
}

function tlbItemRefresh_TlbPaging_GridContracts_Contract_onClick() {
    ChangeLoadState_GridContracts_Contract('Normal');
}
function tlbItemFirst_TlbPaging_GridContracts_Contract_onClick() {
    SetPageIndex_GridContracts_Contract(0);
}

function tlbItemBefore_TlbPaging_GridContracts_Contract_onClick() {
    if (CurrentPageIndex_GridContracts_Contract != 0) {
        CurrentPageIndex_GridContracts_Contract = CurrentPageIndex_GridContracts_Contract - 1;
        SetPageIndex_GridContracts_Contract(CurrentPageIndex_GridContracts_Contract);
    }
}

function tlbItemNext_TlbPaging_GridContracts_Contract_onClick() {
    if (CurrentPageIndex_GridContracts_Contract < parseInt(document.getElementById('hfContractsPageCount_Contract').value) - 1) {
        CurrentPageIndex_GridContracts_Contract = CurrentPageIndex_GridContracts_Contract + 1;
        SetPageIndex_GridContracts_Contract(CurrentPageIndex_GridContracts_Contract);
    }
}

function tlbItemLast_TlbPaging_GridContracts_Contract_onClick() {
    SetPageIndex_GridContracts_Contract(parseInt(document.getElementById('hfContractsPageCount_Contract').value) - 1);
}
function SetPageIndex_GridContracts_Contract(pageIndex) {
    CurrentPageIndex_GridContracts_Contract = pageIndex;
    Fill_GridContracts_Contract(pageIndex);
}

function SetActionMode_Contract(state) {
    document.getElementById('ActionMode_Contract').innerHTML = document.getElementById("hf" + state + "_Contract").value;
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
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}
function tlbItemContractSearch_TlbContractSearch_Contract_onClick()
{
    LoadState_Contract = 'Search';
    SetPageIndex_GridContracts_Contract(0);

}

function txtSearchTerm_Contract_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemContractSearch_TlbContractSearch_Contract_onClick();
    }
}




