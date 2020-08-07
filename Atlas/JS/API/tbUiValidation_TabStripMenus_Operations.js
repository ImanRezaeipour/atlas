var CurrentPageState_UiValidation = 'View';
var ConfirmState_UiValidation = null;
var ObjUiValidation_UiValidation = null;
var LoadStateGridUiValidationRules_UiValidationIntroduction = 'Normal';
var CurrentPageCombosCallBcakStateObj = new Object();
var SelectedItemGridUiValidationRules_UiValidationIntroduction = new Object();

function CallBack_GridUiValidation_UiValidation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UiValidation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridUiValidation_UiValidation();
    }
    ResetGridUiValidationRules_UiValidation();
}
function ShowConnectionError_UiValidation() {
    var error = document.getElementById('hfErrorType_UiValidation').value;
    var errorBody = document.getElementById('hfConnectionError_UiValidation').value;
    showDialog(error, errorBody, 'error');
}
function CallBack_GridUiValidation_UiValidation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridUiValidation_UiValidation').innerHTML = '';
    ShowConnectionError_UiValidation();
}
function GetBoxesHeaders_UiValidation() {

    document.getElementById('header_UiValidation_UiValidation').innerHTML = document.getElementById('hfheader_UiValidation_UiValidation').value;
    document.getElementById('header_tblUiValidation_UiValidationIntroduction').innerHTML = document.getElementById('hfheader_tblUiValidationDetails_UiValidationIntroduction').value;
    document.getElementById('header_UiValidationRules_UiValidationIntroduction').innerHTML = document.getElementById('hfheader_UiValidationRules_UiValidationIntroduction').value;

}
function SetActionMode_UiValidation(state) {
    document.getElementById('ActionMode_UiValidation').innerHTML = document.getElementById("hf" + state + "_UiValidation").value;
}
function Refresh_GridUiValidation_UiValidation() {
    Fill_GridUiValidation_UiValidation();
}
function GridUiValidation_UiValidation_onItemSelect(sender, e) {
    if (CurrentPageState_UiValidation != 'Add')
        NavigateUiValidation_UiValidation(e.get_item(), '');
}
function GridUiValidation_UiValidation_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUiValidation_UiValidation').innerHTML = '';
}
//function ShowDialogUiValidationRules() {
//    var ObjPageState_UiValidation = new Object();
//    selectedItems_GridUiValidation_UiValidation = GridUiValidation_UiValidation.getSelectedItems();
//            if (selectedItems_GridUiValidation_UiValidation.length > 0) {
//                ObjPageState_UiValidation.PageState = CurrentPageState_UiValidation;
//                ObjPageState_UiValidation.GroupID = selectedItems_GridUiValidation_UiValidation[0].getMember('ID').get_text();
//                ObjPageState_UiValidation.Name = selectedItems_GridUiValidation_UiValidation[0].getMember('Name').get_text();
//                parent.DialogUiValidationRules.set_value(ObjPageState_UiValidation);
//                parent.DialogUiValidationRules.Show();
//            }


//}
function tlbItemNew_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('Add');
    ClearList_UiValidation();
    FocusOnFirstElement_UiValidation();
    ResetGridUiValidationRules_UiValidation();

}

function tlbItemEdit_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('Edit');
    FocusOnFirstElement_UiValidation();
}

function tlbItemDelete_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('Delete');
}
function tlbItemCancel_TlbUiValidation_onClick() {
    ChangePageState_UiValidation('View');
    Fill_GridUiValidationRules_UiValidationIntroduction();
    //ClearList_UiValidation();
    //ResetGridUiValidationRules_UiValidation();
}
function tlbItemSetLawParameter_TlbUiValidation_onClick() {
    if (GridUiValidationRules_UiValidationIntroduction.getSelectedItems().length > 0) {
        var selectedItem_GridUiValidationRules_UiValidationIntroduction = GridUiValidationRules_UiValidationIntroduction.getSelectedItems()[0];
        GetRuleType_UiValidation(selectedItem_GridUiValidationRules_UiValidationIntroduction);
    }

    // parent.DialogRulePrecards.Show();
    // DialogParameterValue.Show();
    //var SelectedItems_GridUiValidation_UiValidation = GridUiValidation_UiValidation.getSelectedItems();
    //if (SelectedItems_GridUiValidation_UiValidation.length > 0) {
    //    ShowDialogUiValidationRules();
    //}
}
function tlbItemExit_TlbUiValidation_onClick() {
    ShowDialogConfirm('Exit');
}
function ClearList_UiValidation() {
    if (CurrentPageState_UiValidation != 'Edit') {

        document.getElementById('txtUiValidationCode_UiValidationIntroduction').value = '';
        document.getElementById('txtUiValidationName_UiValidationIntroduction').value = '';
        GridUiValidation_UiValidation.unSelectAll();
    }
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_UiValidation = confirmState;
    if (CurrentPageState_UiValidation == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_UiValidation').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_UiValidation').value;
    DialogConfirm.Show();
}
function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_UiValidation) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateUiValidation_UiValidation();
            // ResetGridUiValidationRules_UiValidation();
            break;
        case 'Exit':
            ClearList_UiValidation();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function FocusOnFirstElement_UiValidation() {
    document.getElementById('txtUiValidationCode_UiValidationIntroduction').focus();
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_UiValidation('View');
}
function tlbItemFormReconstruction_TlbUiValidationIntroduction_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvUiValidation_iFrame').src = parent.ModulePath + 'UiValidation.aspx';
}
//function tlbItemCancel_TlbUiValidation_onClick() {
//    ChangePageState_UiValidation('View');
//    ClearList_UiValidation();
//}
function tlbItemSave_TlbUiValidation_onClick() {
    UiValidation_onSave();
}
function ChangePageState_UiValidation(state) {
    CurrentPageState_UiValidation = state;
    SetActionMode_UiValidation(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_enabled(false);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_imageUrl('add_silver.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_enabled(false);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_imageUrl('edit_silver.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_enabled(false);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_imageUrl('remove_silver.png');
        }
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_enabled(true);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_imageUrl('save.png');
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_enabled(true);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_imageUrl('cancel.png');
        document.getElementById('txtUiValidationCode_UiValidationIntroduction').disabled = '';
        document.getElementById('txtUiValidationName_UiValidationIntroduction').disabled = '';
        if (state == 'Edit')
            NavigateUiValidation_UiValidation(GridUiValidation_UiValidation.getSelectedItems()[0], 'Edit');
        if (state == 'Delete')
            UiValidation_onSave();
    }
    if (state == 'View') {
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_enabled(true);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemNew_TlbUiValidationIntroduction').set_imageUrl('add.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_enabled(true);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemEdit_TlbUiValidationIntroduction').set_imageUrl('edit.png');
        }
        if (TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction') != null) {
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_enabled(true);
            TlbUiValidationIntroduction.get_items().getItemById('tlbItemDelete_TlbUiValidationIntroduction').set_imageUrl('remove.png');
        }
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_enabled(false);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemSave_TlbUiValidationIntroduction').set_imageUrl('save_silver.png');
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_enabled(false);
        TlbUiValidationIntroduction.get_items().getItemById('tlbItemCancel_TlbUiValidationIntroduction').set_imageUrl('cancel_silver.png');
        document.getElementById('txtUiValidationCode_UiValidationIntroduction').disabled = 'disabled';
        document.getElementById('txtUiValidationName_UiValidationIntroduction').disabled = 'disabled';
    }
}
function Fill_GridUiValidation_UiValidation() {
    document.getElementById('loadingPanel_GridUiValidation_UiValidation').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridUiValidation_UiValidation').value);
    CallBack_GridUiValidation_UiValidation.callback();
}
//function CallBack_GridUiValidation_UiValidation_onCallbackComplete(sender, e) {
//    var error = document.getElementById('ErrorHiddenField_UiValidation').value;
//    if (error != "") {
//        var errorParts = eval('(' + error + ')');
//        showDialog(errorParts[0], errorParts[1], errorParts[2]);
//        if (errorParts[3] == 'Reload')
//            Fill_GridUiValidation_UiValidation();
//    }
//}
function UpdateUiValidation_UiValidation() {
    ObjUiValidation_UiValidation = new Object();
    ObjUiValidation_UiValidation.Code = null;
    ObjUiValidation_UiValidation.Name = null;
    ObjUiValidation_UiValidation.Description = null;
    ObjUiValidation_UiValidation.ID = '0';
    var SelectedItems_GridUiValidation_UiValidation = GridUiValidation_UiValidation.getSelectedItems();
    if (SelectedItems_GridUiValidation_UiValidation.length > 0)
        ObjUiValidation_UiValidation.ID = SelectedItems_GridUiValidation_UiValidation[0].getMember("ID").get_text();

    if (CurrentPageState_UiValidation != 'Delete') {
        ObjUiValidation_UiValidation.Code = document.getElementById('txtUiValidationCode_UiValidationIntroduction').value;
        ObjUiValidation_UiValidation.Name = document.getElementById('txtUiValidationName_UiValidationIntroduction').value;

    }
    var gridItem;
    var itemIndex = 0;
    var strRulesListValue = '';
    strRulesListValue += '[';
    while (gridItem = GridUiValidationRules_UiValidationIntroduction.get_table().getRow(itemIndex)) {
        strRulesListValue += '{"uvID" : "' + gridItem.getMember('ID').get_text() + '" , "IsA" : "' + gridItem.getMember('Active').get_text() + '" , "RID" : "' + gridItem.getMember('RuleID').get_text() + '" , "GID" : "' + gridItem.getMember('GroupID').get_text() + '" , "IsW" : "' + gridItem.getMember('Warning').get_text() + '" , "ExistRuleTag" : "' + gridItem.getMember('ExistRuleTag').get_text() + '"},';
        itemIndex++;
    }
    strRulesListValue = strRulesListValue.substring(0, strRulesListValue.length - 1);
    strRulesListValue += ']';
    UpdateUiValidation_UiValidationPage(CharToKeyCode_UiValidation(CurrentPageState_UiValidation), CharToKeyCode_UiValidation(ObjUiValidation_UiValidation.ID), CharToKeyCode_UiValidation(ObjUiValidation_UiValidation.Code), CharToKeyCode_UiValidation(ObjUiValidation_UiValidation.Name), CharToKeyCode_UiValidation(strRulesListValue));
    DialogWaiting.Show();
}

function UpdateUiValidation_UiValidationPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UiValidation').value;
            Response[1] = document.getElementById('hfConnectionError_UiValidation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_UiValidation();
            UiValidation_OnAfterUpdate(Response);
            ChangePageState_UiValidation('View');            
        }
        else {
            if (CurrentPageState_UiValidation == 'Delete')
                ChangePageState_UiValidation('View');
        }
    }
}

function UiValidation_OnAfterUpdate(Response) {
    if (ObjUiValidation_UiValidation != null) {
        var UiValidationCode = ObjUiValidation_UiValidation.Code;
        var UiValidationName = ObjUiValidation_UiValidation.Name;


        var UiValidationItem = null;
        GridUiValidation_UiValidation.beginUpdate();
        switch (CurrentPageState_UiValidation) {
            case 'Add':
                UiValidationItem = GridUiValidation_UiValidation.get_table().addEmptyRow(GridUiValidation_UiValidation.get_recordCount());
                UiValidationItem.setValue(0, Response[3], false);
                GridUiValidation_UiValidation.selectByKey(Response[3], 0, false);
                //ResetcmbRuleGroup_UiValidationIntroduction_UiValidation();
                cmbRuleGroup_UiValidationIntroduction.enable();
                Fill_GridUiValidationRules_UiValidationIntroduction();
                break;
            case 'Edit':
                GridUiValidation_UiValidation.selectByKey(Response[3], 0, false);
                UiValidationItem = GridUiValidation_UiValidation.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                // GridUiValidation_UiValidation.selectByKey(ObjUiValidation_UiValidation.ID, 0, false);
                // GridUiValidation_UiValidation.deleteSelected();
                Fill_GridUiValidation_UiValidation();
                break;
        }
        if (CurrentPageState_UiValidation != 'Delete') {
            UiValidationItem.setValue(1, UiValidationCode, false);
            UiValidationItem.setValue(2, UiValidationName, false);

        }
        GridUiValidation_UiValidation.endUpdate();
    }
}
function NavigateUiValidation_UiValidation(selectedUiValidationItem, state) {
    if (selectedUiValidationItem != undefined) {
        document.getElementById('txtUiValidationCode_UiValidationIntroduction').value = selectedUiValidationItem.getMember('CustomCode').get_text();
        document.getElementById('txtUiValidationName_UiValidationIntroduction').value = selectedUiValidationItem.getMember('Name').get_text();
        TlbRefresh_GridUiValidationRules.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridUiValidationRules_UiValidationIntroduction').set_enabled(true);
        cmbRuleGroup_UiValidationIntroduction.enable();
        if (state != 'Edit') {
            ResetcmbRuleGroup_UiValidationIntroduction_UiValidation();
            Fill_GridUiValidationRules_UiValidationIntroduction();
        }
    }
}
function UiValidation_onSave() {
    if (CurrentPageState_UiValidation != 'Delete')
        UpdateUiValidation_UiValidation();
    else
        ShowDialogConfirm('Delete');
}
function CharToKeyCode_UiValidation(str) {
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

function tlbItemHelp_TlbUiValidationIntroduction_onClick() {
    LoadHelpPage('tlbItemHelp_TlbUiValidationIntroduction');
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}
function tlbItemRefresh_GridUiValidationRules_UiValidationIntroduction(sender, e) {
    ResetcmbRuleGroup_UiValidationIntroduction_UiValidation();
    Fill_GridUiValidationRules_UiValidationIntroduction();
}

function GridUiValidationRules_UiValidationIntroduction_onItemDoubleClick(sender, e) {
    GetRuleType_UiValidation(e.get_item());
}

function GetRuleType_UiValidation(SelectedGridUiValidationRuleGroup) {
    var CustomCode = SelectedGridUiValidationRuleGroup.getMember('CustomCode').get_text();
    var RuleType = SelectedGridUiValidationRuleGroup.getMember('RuleType').get_text();
    if (RuleType == '1' || RuleType == '2' || RuleType == '3' || CustomCode == '22') {
        SelectedItemGridUiValidationRules_UiValidationIntroduction.GroupID = SelectedGridUiValidationRuleGroup.getMember('GroupID').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.RuleID = SelectedGridUiValidationRuleGroup.getMember('RuleID').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.RuleGroupID = SelectedGridUiValidationRuleGroup.getMember('RuleGroupID').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.RuleType = SelectedGridUiValidationRuleGroup.getMember('RuleType').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.RuleGroupType = SelectedGridUiValidationRuleGroup.getMember('RuleGroupType').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.RuleName = SelectedGridUiValidationRuleGroup.getMember('RuleName').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.ExistRuleTag = SelectedGridUiValidationRuleGroup.getMember('ExistRuleTag').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.CustomCode = SelectedGridUiValidationRuleGroup.getMember('CustomCode').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.Active = SelectedGridUiValidationRuleGroup.getMember('Active').get_text();
        SelectedItemGridUiValidationRules_UiValidationIntroduction.Warning = SelectedGridUiValidationRuleGroup.getMember('Warning').get_text();
        parent.DialogRulePrecards.set_value(SelectedItemGridUiValidationRules_UiValidationIntroduction);
        ShowDialogRulePrecards();
    }
}

function ShowDialogRulePrecards() {
    parent.DialogRulePrecards.Show();
}

function GridUiValidationRules_UiValidationIntroduction_onLoad(sender, e) {

}
function CallBack_GridUiValidationRules_UiValidationIntroduction_onCallbackComplete(sender, e) {
    document.getElementById('loadingPanel_GridUiValidationRules_UiValidationIntroduction').innerHTML = '';
}


function CallBack_GridUiValidationRules_UiValidationIntroduction_onCallbackError(sender, e) {

}
function Fill_GridUiValidationRules_UiValidationIntroduction() {
    var ruleGroupID = '0';
    var UiValidationGroupId = '0';
    var selectedItemGridUiValidation_UiValidation = GridUiValidation_UiValidation.getSelectedItems();
    switch (LoadStateGridUiValidationRules_UiValidationIntroduction) {
        case 'Normal':
            break;
        case 'GroupRule':
            if (cmbRuleGroup_UiValidationIntroduction.getSelectedItem() != null && cmbRuleGroup_UiValidationIntroduction.getSelectedItem() != undefined) {
                ruleGroupID = cmbRuleGroup_UiValidationIntroduction.getSelectedItem().get_value();
            }
            break;
        default:
            break;
    }
    if (selectedItemGridUiValidation_UiValidation.length > 0) {
        UiValidationGroupId = selectedItemGridUiValidation_UiValidation[0].getMember('ID').get_text();
    }
    document.getElementById('loadingPanel_GridUiValidationRules_UiValidationIntroduction').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridUiValidationRules_UiValidationIntroduction').value);
    CallBack_GridUiValidationRules_UiValidationIntroduction.callback(CharToKeyCode_UiValidation(UiValidationGroupId), CharToKeyCode_UiValidation(ruleGroupID));
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function cmbRuleGroup_UiValidationIntroduction_onExpand(sender, e) {
    if (cmbRuleGroup_UiValidationIntroduction.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroup_UiValidationIntroduction == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroup_UiValidationIntroduction = true;
        Fill_cmbRuleGroup_UiValidationIntroduction();
    }
}
function Fill_cmbRuleGroup_UiValidationIntroduction() {
    ComboBox_onBeforeLoadData('cmbRuleGroup_UiValidationIntroduction');
    CallBack_cmbRuleGroup_UiValidationIntroduction.callback();
}
function ComboBox_onBeforeLoadData(cmbID) {
    if (document.getElementById(cmbID) != null && document.getElementById(cmbID) != undefined) {
        document.getElementById(cmbID).onmouseover = " ";
        document.getElementById(cmbID).onmouseout = " ";
    }
    if (document.getElementById(cmbID + '_Input') != null && document.getElementById(cmbID + '_Input') != undefined) {
        document.getElementById(cmbID + '_Input').onfocus = " ";
        document.getElementById(cmbID + '_Input').onblur = " ";
        document.getElementById(cmbID + '_Input').onkeydown = " ";
        document.getElementById(cmbID + '_Input').onmouseup = " ";
        document.getElementById(cmbID + '_Input').onmousedown = " ";
    }
    if (document.getElementById(cmbID + '_DropImage') != null && document.getElementById(cmbID + '_DropImage') != undefined)
        document.getElementById(cmbID + '_DropImage').src = 'Images/ComboBox/ComboBoxLoading.gif';
    eval(cmbID).disable();
}
function cmbRuleGroup_UiValidationIntroduction_onCollapse(sender, e) {

}

function cmbRuleGroup_UiValidationIntroduction_onChange(sender, e) {
    if (cmbRuleGroup_UiValidationIntroduction.getSelectedItem() != null && cmbRuleGroup_UiValidationIntroduction.getSelectedItem() != undefined) {
        LoadStateGridUiValidationRules_UiValidationIntroduction = 'GroupRule';
        Fill_GridUiValidationRules_UiValidationIntroduction();
    }
}

function CallBack_cmbRuleGroup_UiValidationIntroduction_onBeforeCallback(sender, e) {
    cmbRuleGroup_UiValidationIntroduction.dispose();
}

function CallBack_cmbRuleGroup_UiValidationIntroduction_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleGroup').value;
    if (error == "") {
        document.getElementById('cmbRuleGroup_UiValidationIntroduction_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRuleGroup_UiValidationIntroduction_DropImage').mousedown();
        cmbRuleGroup_UiValidationIntroduction.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleGroup_UiValidationIntroduction_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbRuleGroup_UiValidationIntroduction_onCallbackError(sender, e) {
    ShowConnectionError_UiValidation();
}

function tlbItemExit_TlbParameterValue_onClick() {

}

function tlbItemExit_TlbParameterValue_onClick() {
    DialogParameterValue.Close();
}

function tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms_onClick() {

}

function tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms_onClick() {

}

function tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms_onClick() {

}

function CharToKeyCode_UiValidation(str) {
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
function ResetGridUiValidationRules_UiValidation() {
    GridUiValidationRules_UiValidationIntroduction.beginUpdate();
    GridUiValidationRules_UiValidationIntroduction.get_table().clearData();
    GridUiValidationRules_UiValidationIntroduction.endUpdate();
    TlbRefresh_GridUiValidationRules.get_items().getItemById('tlbItemRefresh_TlbRefresh_GridUiValidationRules_UiValidationIntroduction').set_enabled(false);
    cmbRuleGroup_UiValidationIntroduction.disable();
    document.getElementById('cmbRuleGroup_UiValidationIntroduction_Input').value = '';
    cmbRuleGroup_UiValidationIntroduction.unSelect();
}

function ResetcmbRuleGroup_UiValidationIntroduction_UiValidation() {
    document.getElementById('cmbRuleGroup_UiValidationIntroduction_Input').value = '';
    cmbRuleGroup_UiValidationIntroduction.unSelect();
}

function GridUiValidationRules_UiValidationIntroduction_onItemCheckChange(sender, e) {
    var index = e.get_item().get_index();
    var chbWarningID = 'checkbox_GridUiValidationRules_UiValidationIntroduction_Warning_' + index.toString();
    if (e.get_item().getMember('Warning').get_value() != document.getElementById(chbWarningID).checked) {
        if (!e.get_item().getMember('IsWarningAllowed').get_value()) {
            GridUiValidationRules_UiValidationIntroduction.beginUpdate();
            e.get_item().setValue(11, !document.getElementById(chbWarningID).checked, false);
            GridUiValidationRules_UiValidationIntroduction.endUpdate();
        }
    }
}