var CurrentPageState_Contractors = 'View';
var LoadState_Contractors = 'Normal';
var CurrentPageIndex_GridContractors_Contractors = 0;

function GetBoxesHeaders_Contractors() {
    document.getElementById('header_ContractorDetails_Contractors').innerHTML = document.getElementById('hfheader_ContractorDetails_Contractors').value;
    document.getElementById('header_Contractors_Contractors').innerHTML = document.getElementById('hfheader_Contractors_Contractors').value;
    document.getElementById('endfooter_gridcontractors_contractors').innerHTML = document.getElementById('hffooter_GridContractors_Contractors').value;
}
function SetActionMode_Contractors(state) {
    document.getElementById('ActionMode_Contractors').innerHTML = document.getElementById("hf" + state + "_Contractors").value;
}
function tlbItemNew_TlbContractors_onClick() {
    ChangePageState_Contractors('Add');
    ClearList_Contractors();
    FocusOnFirstElement_Contractors();
}
function ChangePageState_Contractors(state) {
    CurrentPageState_Contractors = state;
    SetActionMode_Contractors(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbContractors.get_items().getItemById('tlbItemNew_TlbContractors') != null) {
            TlbContractors.get_items().getItemById('tlbItemNew_TlbContractors').set_enabled(false);
            TlbContractors.get_items().getItemById('tlbItemNew_TlbContractors').set_imageUrl('add_silver.png');
        }
        if (TlbContractors.get_items().getItemById('tlbItemEdit_TlbContractors') != null) {
            TlbContractors.get_items().getItemById('tlbItemEdit_TlbContractors').set_enabled(false);
            TlbContractors.get_items().getItemById('tlbItemEdit_TlbContractors').set_imageUrl('edit_silver.png');
        }
        if (TlbContractors.get_items().getItemById('tlbItemDelete_TlbContractors') != null) {
            TlbContractors.get_items().getItemById('tlbItemDelete_TlbContractors').set_enabled(false);
            TlbContractors.get_items().getItemById('tlbItemDelete_TlbContractors').set_imageUrl('remove_silver.png');
        }
        TlbContractors.get_items().getItemById('tlbItemSave_TlbContractors').set_enabled(true);
        TlbContractors.get_items().getItemById('tlbItemSave_TlbContractors').set_imageUrl('save.png');
        TlbContractors.get_items().getItemById('tlbItemCancel_TlbContractors').set_enabled(true);
        TlbContractors.get_items().getItemById('tlbItemCancel_TlbContractors').set_imageUrl('cancel.png');
        document.getElementById('txtContractorName_Contractors').disabled = '';
        document.getElementById('txtContractorCode_Contractors').disabled = '';
        document.getElementById('txtEconomicCode_Contractors').disabled = '';
        document.getElementById('txtOrganization_Contractors').disabled = '';
        document.getElementById('txtTelNumber_Contractors').disabled = '';
        document.getElementById('txtFax_Contractors').disabled = '';
        document.getElementById('txtAddress_Contractors').disabled = '';
        document.getElementById('txtDescription_Contractors').disabled = '';
        document.getElementById('txtEmail_Contractors').disabled = '';
        document.getElementById('chbDefaultContractor_Contractors').disabled = '';
        if (state == 'Edit')
            NavigateContractors_Contractors(GridContractors_Contractors.getSelectedItems()[0]);
        if (state == 'Delete')
            Contractor_onSave();
    }
    if (state == 'View') {
        if (TlbContractors.get_items().getItemById('tlbItemNew_TlbContractors') != null) {
            TlbContractors.get_items().getItemById('tlbItemNew_TlbContractors').set_enabled(true);
            TlbContractors.get_items().getItemById('tlbItemNew_TlbContractors').set_imageUrl('add.png');
        }
        if (TlbContractors.get_items().getItemById('tlbItemEdit_TlbContractors') != null) {
            TlbContractors.get_items().getItemById('tlbItemEdit_TlbContractors').set_enabled(true);
            TlbContractors.get_items().getItemById('tlbItemEdit_TlbContractors').set_imageUrl('edit.png');
        }
        if (TlbContractors.get_items().getItemById('tlbItemDelete_TlbContractors') != null) {
            TlbContractors.get_items().getItemById('tlbItemDelete_TlbContractors').set_enabled(true);
            TlbContractors.get_items().getItemById('tlbItemDelete_TlbContractors').set_imageUrl('remove.png');
        }
        TlbContractors.get_items().getItemById('tlbItemSave_TlbContractors').set_enabled(false);
        TlbContractors.get_items().getItemById('tlbItemSave_TlbContractors').set_imageUrl('save_silver.png');
        TlbContractors.get_items().getItemById('tlbItemCancel_TlbContractors').set_enabled(false);
        TlbContractors.get_items().getItemById('tlbItemCancel_TlbContractors').set_imageUrl('cancel_silver.png');
        document.getElementById('txtContractorName_Contractors').disabled = 'disabled';
        document.getElementById('txtContractorCode_Contractors').disabled = 'disabled';
        document.getElementById('txtEconomicCode_Contractors').disabled = 'disabled';
        document.getElementById('txtOrganization_Contractors').disabled = 'disabled';
        document.getElementById('txtTelNumber_Contractors').disabled = 'disabled';
        document.getElementById('txtFax_Contractors').disabled = 'disabled';
        document.getElementById('txtAddress_Contractors').disabled = 'disabled';
        document.getElementById('txtDescription_Contractors').disabled = 'disabled';
        document.getElementById('txtEmail_Contractors').disabled = 'disabled';
        document.getElementById('chbDefaultContractor_Contractors').disabled = 'disabled';
    }
}
function GridContractors_Contractors_OnItemSelect(sender, e) {
    //if (CurrentPageState_Contractors != 'Add')
    NavigateContractors_Contractors(e.get_item());
}
function NavigateContractors_Contractors(selectedContractorItem) {
    if (selectedContractorItem != undefined) {
        document.getElementById('txtContractorName_Contractors').value = selectedContractorItem.getMember('Name').get_text();
        document.getElementById('txtContractorCode_Contractors').value = selectedContractorItem.getMember('Code').get_text();
        document.getElementById('txtEconomicCode_Contractors').value = selectedContractorItem.getMember('EconomicCode').get_text();
        document.getElementById('txtOrganization_Contractors').value = selectedContractorItem.getMember('Organization').get_text();
        document.getElementById('txtTelNumber_Contractors').value = selectedContractorItem.getMember('Tel').get_text();
        document.getElementById('txtFax_Contractors').value = selectedContractorItem.getMember('Fax').get_text();
        document.getElementById('txtAddress_Contractors').value = selectedContractorItem.getMember('Address').get_text();
        document.getElementById('txtDescription_Contractors').value = selectedContractorItem.getMember('Description').get_text();
        document.getElementById('txtEmail_Contractors').value = selectedContractorItem.getMember('Email').get_text();
        document.getElementById('chbDefaultContractor_Contractors').checked = selectedContractorItem.getMember('IsDefault').get_value();
    }
}
function Contractor_onSave() {
    if (CurrentPageState_Contractors != 'Delete')
        UpdateContractor_Contractors();
    else
        ShowDialogConfirm('Delete');
}
function UpdateContractor_Contractors() {
    ObjContractor_Contractors = new Object();
    ObjContractor_Contractors.ID = '0';
    ObjContractor_Contractors.Name = null;
    ObjContractor_Contractors.Code = null;
    ObjContractor_Contractors.Organization = null;
    ObjContractor_Contractors.EconomicCode = null;
    ObjContractor_Contractors.Tel = null;
    ObjContractor_Contractors.Fax = null;
    ObjContractor_Contractors.Email = null;
    ObjContractor_Contractors.Address = null;
    ObjContractor_Contractors.Description = null;
    ObjContractor_Contractors.IsDefault = false;

    var SelectedItems_GridContractors_Contractors = GridContractors_Contractors.getSelectedItems();  
    if (SelectedItems_GridContractors_Contractors.length > 0) {
        ObjContractor_Contractors.ID = SelectedItems_GridContractors_Contractors[0].getMember("ID").get_text();
        ObjContractor_Contractors.IsDefault = SelectedItems_GridContractors_Contractors[0].getMember('IsDefault').get_value();
    }       
    if (CurrentPageState_Contractors != 'Delete') {
        ObjContractor_Contractors.Name = document.getElementById('txtContractorName_Contractors').value;
        ObjContractor_Contractors.Code = document.getElementById('txtContractorCode_Contractors').value;
        ObjContractor_Contractors.EconomicCode = document.getElementById('txtEconomicCode_Contractors').value;
        ObjContractor_Contractors.Organization = document.getElementById('txtOrganization_Contractors').value;
        ObjContractor_Contractors.Tel = document.getElementById('txtTelNumber_Contractors').value;
        ObjContractor_Contractors.Fax = document.getElementById('txtFax_Contractors').value;
        ObjContractor_Contractors.Email = document.getElementById('txtEmail_Contractors').value;
        ObjContractor_Contractors.Address = document.getElementById('txtAddress_Contractors').value;
        ObjContractor_Contractors.Description = document.getElementById('txtDescription_Contractors').value;
        ObjContractor_Contractors.IsDefault = document.getElementById('chbDefaultContractor_Contractors').checked;
    }
    UpdateContractor_ContractorsPage(CharToKeyCode_Contractors(CurrentPageState_Contractors), CharToKeyCode_Contractors(ObjContractor_Contractors.ID), CharToKeyCode_Contractors(ObjContractor_Contractors.Name), CharToKeyCode_Contractors(ObjContractor_Contractors.Code), CharToKeyCode_Contractors(ObjContractor_Contractors.EconomicCode), CharToKeyCode_Contractors(ObjContractor_Contractors.Organization), CharToKeyCode_Contractors(ObjContractor_Contractors.Tel), CharToKeyCode_Contractors(ObjContractor_Contractors.Fax), CharToKeyCode_Contractors(ObjContractor_Contractors.Email), CharToKeyCode_Contractors(ObjContractor_Contractors.Address), CharToKeyCode_Contractors(ObjContractor_Contractors.Description), CharToKeyCode_Contractors(ObjContractor_Contractors.IsDefault.toString()));
    DialogWaiting.Show();
}
function UpdateContractor_ContractorsPage_onCallBack(Response) {
    var RetMesage = Response;
    if (RetMesage != null && RetMesage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Contractors').value;
            Response[1] = document.getElementById('hfConnectionError_Contractors').value;
        }
        showDialog(RetMesage[0], Response[1], RetMesage[2]);
        if (RetMesage[2] == 'success') {
            //  ClearList_Contractors();
            ControlStation_OnAfterUpdate(Response);
            //if (CurrentPageState_Contractors == 'Add') {
            //    LoadState_Contractors = 'Normal';
            //    Fill_GridContractors_Contractors(0);
            //}
            ChangePageState_Contractors('View');
        }
        else {
            if (CurrentPageState_Contractors == 'Delete')
                ChangePageState_Contractors('View');
        }
    }
}
function ControlStation_OnAfterUpdate(Response) {
    if (ObjContractor_Contractors != null) {
        var Name = ObjContractor_Contractors.Name;
        var Code = ObjContractor_Contractors.Code;
        var EconomicCode = ObjContractor_Contractors.EconomicCode;
        var Organization = ObjContractor_Contractors.Organization;
        var Tel = ObjContractor_Contractors.Tel;
        var Fax = ObjContractor_Contractors.Fax;
        var Email = ObjContractor_Contractors.Email;
        var Address = ObjContractor_Contractors.Address;
        var Description = ObjContractor_Contractors.Description;
        var IsDefault = ObjContractor_Contractors.IsDefault;

        var ContractorItem = null;
        GridContractors_Contractors.beginUpdate();
        switch (CurrentPageState_Contractors) {
            case 'Add':
                ContractorItem = GridContractors_Contractors.get_table().addEmptyRow(GridContractors_Contractors.get_recordCount());
                ContractorItem.setValue(0, Response[3], false);
                GridContractors_Contractors.selectByKey(Response[3], 0, false);
                UpdateFeatures_GridContractors_Contractors();
                break;
            case 'Edit':
                // GridContractors_Contractors.selectByKey(Response[3], 0, false);
                ContractorItem = GridContractors_Contractors.getItemFromKey(0, Response[3]);
                UpdateFeatures_GridContractors_Contractors();
                break;
            case 'Delete':
                GridContractors_Contractors.selectByKey(ObjContractor_Contractors.ID, 0, false);
                GridContractors_Contractors.deleteSelected();
                UpdateFeatures_GridContractors_Contractors();
                break;
        }
        if (CurrentPageState_Contractors != 'Delete') {
            ContractorItem.setValue(1, Name, false);
            ContractorItem.setValue(2, Organization, false);
            ContractorItem.setValue(3, Code, false);
            ContractorItem.setValue(4, EconomicCode, false);
            ContractorItem.setValue(5, Description, false);
            ContractorItem.setValue(6, Tel, false);
            ContractorItem.setValue(7, Fax, false);
            ContractorItem.setValue(8, Email, false);
            ContractorItem.setValue(9, Address, false);
            ContractorItem.setValue(10, IsDefault, false);
        }
        GridContractors_Contractors.endUpdate();
    }
}
function UpdateFeatures_GridContractors_Contractors() {
    var ContractorsCount = parseInt(document.getElementById('hfContractorsCount_Contractors').value);
    var ContractorsPageCount = parseInt(document.getElementById('hfContractorsPageCount_Contractors').value);
    var ContractorsPageSize = parseInt(document.getElementById('hfContractorsPageSize_Contractors').value);
    var Lag = 0;
    switch (CurrentPageState_Contractors) {
        case 'Add':
            Lag = Lag + 1;
            break;
        case 'Delete':
            Lag = Lag - 1;
            break;
    }
    if ((ContractorsCount > 0 && CurrentPageState_Contractors == 'Delete') || CurrentPageState_Contractors == 'Add') {
        ContractorsCount = ContractorsCount + Lag;
        var divRem = mod(ContractorsCount, ContractorsPageSize);
        switch (CurrentPageState_Contractors) {
            case 'Add':
                if (GridContractors_Contractors.get_table().getRowCount() > ContractorsPageSize) {
                    ContractorsPageCount = ContractorsPageCount + Lag;
                    CurrentPageIndex_GridContractors_Contractors = CurrentPageIndex_GridContractors_Contractors + Lag;
                }
                break;
            case 'Delete':
                if (divRem == 0) {
                    ContractorsPageCount = ContractorsPageCount + Lag;
                    if (CurrentPageIndex_GridContractors_Contractors == ContractorsPageCount) {
                        CurrentPageIndex_GridContractors_Contractors = CurrentPageIndex_GridContractors_Contractors + Lag >= 0 ? CurrentPageIndex_GridContractors_Contractors + Lag : 0;
                    }
                }
                break;
        }
        SetPageIndex_GridContractors_Contractors(CurrentPageIndex_GridContractors_Contractors);
        document.getElementById('hfContractorsCount_Contractors').value = ContractorsCount.toString();
        document.getElementById('hfContractorsPageCount_Contractors').value = ContractorsPageCount.toString();
        Changefooter_GridContractors_Contractors();
    }
    if ((ContractorsCount > 0 && CurrentPageState_Contractors == 'Edit')) {
        SetPageIndex_GridContractors_Contractors(CurrentPageIndex_GridContractors_Contractors);
    }
}
function mod(a, b) {
    return a - (b * Math.floor(a / b));
}
function ShowDialogConfirm(confirmState) {
    ConfirmState_Contractors = confirmState;
    if (CurrentPageState_Contractors == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Contractors').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Contractors').value;
    DialogConfirm.Show();
}
function ClearList_Contractors() {
    document.getElementById('txtContractorName_Contractors').value = '';
    document.getElementById('txtContractorCode_Contractors').value = '';
    document.getElementById('txtEconomicCode_Contractors').value = '';
    document.getElementById('txtOrganization_Contractors').value = '';
    document.getElementById('txtTelNumber_Contractors').value = '';
    document.getElementById('txtFax_Contractors').value = '';
    document.getElementById('txtEmail_Contractors').value = '';
    document.getElementById('txtAddress_Contractors').value = '';
    document.getElementById('txtDescription_Contractors').value = '';
    document.getElementById('chbDefaultContractor_Contractors').checked = '';
}
function FocusOnFirstElement_Contractors() {
    document.getElementById('txtContractorName_Contractors').focus();
}
function tlbItemEdit_TlbContractors_onClick() {
    ChangePageState_Contractors('Edit');
    FocusOnFirstElement_Contractors();
}
function tlbItemDelete_TlbContractors_onClick() {
    ChangePageState_Contractors('Delete');
}
function tlbItemCancel_TlbContractors_onClick() {
    ChangePageState_Contractors('View');
    ClearList_Contractors();
}
function tlbItemSave_TlbContractors_onClick() {
    Contractor_onSave();
   // ClearList_Contractors();
}
function tlbItemHelp_TlbContractors_onClick() {
    LoadHelpPage('tlbItemHelp_TlbContractors');
}

function tlbItemFormReconstruction_TlbContractors_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvContractorsIntroduction_iFrame').src = 'Contractors.aspx';
}
function tlbItemExit_TlbContractors_onClick() {
    ShowDialogConfirm('Exit');
}

function GridContractors_Contractors_onLoad() {
    document.getElementById('loadingPanel_GridContractors_Contractors').innerHTML = '';
}
function GridContractors_Contractors_onItemCheckChange() {

}
function CallBack_GridContractors_Contractors_onCallbackComplete() {
    var error = document.getElementById('ErrorHiddenField_Contractors').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridContractors_Contractors(0);
    }
    else Changefooter_GridContractors_Contractors();
}
function Changefooter_GridContractors_Contractors() {
    var retfooterVal = '';
    var footerVal = document.getElementById('endfooter_gridcontractors_contractors').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfContractorsPageCount_Contractors').value) > 0 ? CurrentPageIndex_GridContractors_Contractors + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfContractorsPageCount_Contractors').value;
        if ((i == 1 || i == 3) && GridContractors_Contractors.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('endfooter_gridcontractors_contractors').innerHTML = retfooterVal;
   document.getElementById('beginfooter_GridContractors_Contractors').innerHTML = document.getElementById('hfContractorsCount_GridContractors_Contractors').value + document.getElementById('hfContractorsCount_Contractors').value;
}
function CallBack_GridContractors_Contractors_onCallbackError() {
    document.getElementById('loadingPanel_GridContractors_Contractors').innerHTML = '';
    ShowConnectionError_Contractors();
}
function ShowConnectionError_Contractors() {
    var error = document.getElementById('hfErrorType_Contractors').value;
    var errorBody = document.getElementById('hfConnectionError_Contractors').value;
    showDialog(error, errorBody, 'error');
}
function tlbItemSearch_TlbContractorsSearch_onClick() {
    LoadState_Contractors = 'Search';
    Fill_GridContractors_Contractors(0);
}
function tlbItemRefresh_TlbPaging_GridContractors_Contractors_onClick() {
    LoadState_Contractors = 'Normal';
    Fill_GridContractors_Contractors(0);
}
function tlbItemFirst_TlbPaging_GridContractors_Contractors_onClick() {
    SetPageIndex_GridContractors_Contractors(0);
}
function tlbItemBefore_TlbPaging_GridContractors_Contractors_onClick() {
    if (CurrentPageIndex_GridContractors_Contractors != 0) {
        CurrentPageIndex_GridContractors_Contractors = CurrentPageIndex_GridContractors_Contractors - 1;
        SetPageIndex_GridContractors_Contractors(CurrentPageIndex_GridContractors_Contractors);
    }
}
function tlbItemNext_TlbPaging_GridContractors_Contractors_onClick() {
    if (CurrentPageIndex_GridContractors_Contractors < parseInt(document.getElementById('hfContractorsPageCount_Contractors').value) - 1) {
        CurrentPageIndex_GridContractors_Contractors = CurrentPageIndex_GridContractors_Contractors + 1;
        SetPageIndex_GridContractors_Contractors(CurrentPageIndex_GridContractors_Contractors);
    }
}
function tlbItemLast_TlbPaging_GridContractors_Contractors_onClick() {
    SetPageIndex_GridContractors_Contractors(parseInt(document.getElementById('hfContractorsPageCount_Contractors').value) - 1);
}
function SetPageIndex_GridContractors_Contractors(pageIndex) {
    CurrentPageIndex_GridContractors_Contractors = pageIndex;
    ClearList_Contractors();
    Fill_GridContractors_Contractors(pageIndex);
}
function Fill_GridContractors_Contractors(pageIndex) {
    document.getElementById('loadingPanel_GridContractors_Contractors').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridContractors_Contractors').value);
    var pageSize = parseInt(document.getElementById('hfContractorsPageSize_Contractors').value);
    switch (LoadState_Contractors) {
        case 'Normal':
            var SearchValue = '';
            break;
        case 'Search':
            var SearchValue = document.getElementById('txtSearchTerm_Contractors').value;
            break;
    }
    CallBack_GridContractors_Contractors.callback(CharToKeyCode_Contractors(pageIndex.toString()), CharToKeyCode_Contractors(pageSize.toString()), CharToKeyCode_Contractors(SearchValue));
}
function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}
function txtSearchTerm_Contractors_onKeyPess(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbContractorsSearch_onClick();
    }
}
function CharToKeyCode_Contractors(str) {
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
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Contractors('View');
}
function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Contractors) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateContractor_Contractors();
            break;
        case 'Exit':
            ClearList_Contractors();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

