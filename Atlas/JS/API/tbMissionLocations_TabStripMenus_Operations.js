
var CurrentPageState_MissionLocations = 'View';
var ConfirmState_MissionLocations = null;
var ObjMissionLocation_MissionLocations = null;
var CurrentPageTreeViewsObj = new Object();
var MissionSelectedType_MissionLocations = null;

function GetBoxesHeaders_MissionLocations() {
    document.getElementById('header_MissionLocations_MissionLocationsIntroduction').innerHTML = document.getElementById('hfheader_MissionLocations_MissionLocationsIntroduction').value;
    document.getElementById('header_MissionLocationsDetails_MissionLocationIntroduction').innerHTML = document.getElementById('hfheader_MissionLocationsDetails_MissionLocationIntroduction').value;
}

function CacheTreeViewsSize_MissionLocations() {
    CurrentPageTreeViewsObj.trvMissionLocationsIntroduction_MissionLocationsIntroduction = document.getElementById('trvMissionLocationsIntroduction_MissionLocationsIntroduction').clientWidth + 'px';
}

function tlbItemNew_TlbMissionOverallLocation_onClick() {
    ChangePageState_MissionLocations('Add');
    ClearList_MissionLocations();
    FocusOnFirstElement_MissionLocations();
}

function tlbItemEdit_TlbMissionOverallLocation_onClick() {
    ChangePageState_MissionLocations('Edit');
    FocusOnFirstElement_MissionLocations();
}

function tlbItemDelete_TlbMissionOverallLocation() {
    ChangePageState_MissionLocations('Delete');
}

function tlbItemSave_TlbMissionOverallLocation_onClick() {
    MissionLocation_onSave();
}

function tlbItemCancel_TlbMissionOverallLocation_onClick() {
    DialogConfirm.Close();
    ChangePageState_MissionLocations('View');
}

function ChangePageState_MissionLocations(state) {
    CurrentPageState_MissionLocations = state;
    SetActionMode_MissionLocations(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_enabled(false);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_imageUrl('add_silver.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_enabled(false);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_imageUrl('edit_silver.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_enabled(false);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_imageUrl('remove_silver.png');
        }
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_enabled(true);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_imageUrl('save.png');
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_enabled(true);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_imageUrl('cancel.png');
        document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').disabled = '';
        document.getElementById('txtMissionLocationName_MissionLocationIntroduction').disabled = '';
        if (state == 'Edit')
        {
            switch (MissionSelectedType_MissionLocations) {
                case 'Normal':
                    {

                        NavigateMissionLocation_MissionLocations(trvMissionLocationsIntroduction_MissionLocationsIntroduction.get_selectedNode());
                        break;
                    }
                case 'Search':
                    {
                        NavigateMissionLocation_MissionLocations(cmbMissionSearchResult_MissionLocationsIntroduction.getSelectedItem());

                        break;
                    }
                default:
                    break;
            }
        }
            
        if (state == 'Delete')
            MissionLocation_onSave();
    }
    if (state == 'View') {
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_enabled(true);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemNew_TlbMissionOverallLocation').set_imageUrl('add.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_enabled(true);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemEdit_TlbMissionOverallLocation').set_imageUrl('edit.png');
        }
        if (TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation') != null) {
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_enabled(true);
            TlbMissionOverallLocation.get_items().getItemById('tlbItemDelete_TlbMissionOverallLocation').set_imageUrl('remove.png');
        }
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_enabled(false);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemSave_TlbMissionOverallLocation').set_imageUrl('save_silver.png');
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_enabled(false);
        TlbMissionOverallLocation.get_items().getItemById('tlbItemCancel_TlbMissionOverallLocation').set_imageUrl('cancel_silver.png');
        document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').disabled = 'disabled';
        document.getElementById('txtMissionLocationName_MissionLocationIntroduction').disabled = 'disabled';
    }
}

function SetActionMode_MissionLocations(state) {
    document.getElementById('ActionMode_MissionOverallLocationForm').innerHTML = document.getElementById("hf" + state + "_MissionLocations").value;
}

function ClearList_MissionLocations() {
    if (CurrentPageState_MissionLocations != 'Edit') {
        document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').value = '';
        document.getElementById('txtMissionLocationName_MissionLocationIntroduction').value = '';
    }
}

function FocusOnFirstElement_MissionLocations() {
    document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').focus();
}

function tlbItemExit_TlbMissionOverallLocation_onClick() {
    ShowDialogConfirm('Exit');
}

function MissionLocation_onSave() {
    if (CurrentPageState_MissionLocations != 'Delete')
        UpdateMissionLocation_MissionLocations();
    else
        ShowDialogConfirm('Delete');
}

function UpdateMissionLocation_MissionLocations() {
    ObjMissionLocation_MissionLocations = new Object();
    ObjMissionLocation_MissionLocations.CustomCode = null;
    ObjMissionLocation_MissionLocations.Name = null;
    ObjMissionLocation_MissionLocations.SelectedID = '0';
    ObjMissionLocation_MissionLocations.ParentID = '0';
    switch (MissionSelectedType_MissionLocations) {
        case 'Normal':
            {
                var SelectedMissionLocationNode_MissionLocations = trvMissionLocationsIntroduction_MissionLocationsIntroduction.get_selectedNode();
                if (SelectedMissionLocationNode_MissionLocations != undefined) {
                    ObjMissionLocation_MissionLocations.SelectedID = SelectedMissionLocationNode_MissionLocations.get_id();
                    if (SelectedMissionLocationNode_MissionLocations.get_parentNode() != undefined)
                        ObjMissionLocation_MissionLocations.ParentID = SelectedMissionLocationNode_MissionLocations.get_parentNode().get_id();
                }
                break;
            }
        case 'Search':
            {
                var selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction = cmbMissionSearchResult_MissionLocationsIntroduction.getSelectedItem();
                if (selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction != undefined && selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction != null) {
                    var missionObj = selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction.get_value();
                        missionObj = eval('(' + missionObj + ')');
                        ObjMissionLocation_MissionLocations.SelectedID = selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction.get_id();
                    ObjMissionLocation_MissionLocations.ParentID = missionObj.ParentID;
                    
                }
                break;
            }
        default:
            break;
    }


    
   
    if (CurrentPageState_MissionLocations != 'Delete') {
        ObjMissionLocation_MissionLocations.CustomCode = document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').value;
        ObjMissionLocation_MissionLocations.Name = document.getElementById('txtMissionLocationName_MissionLocationIntroduction').value;
    }
    UpdateMissionLocation_MissionLocationsPage(CharToKeyCode_MissionLocations(CurrentPageState_MissionLocations), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.ParentID), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.SelectedID), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.CustomCode), CharToKeyCode_MissionLocations(ObjMissionLocation_MissionLocations.Name));
    DialogWaiting.Show();
}

function UpdateMissionLocation_MissionLocationsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MissionLocations').value;
            Response[1] = document.getElementById('hfConnectionError_MissionLocations').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Clear_cmbMissionSearchResult_MissionLocationsIntroduction();
            MissionLocation_OnAfterUpdate(Response);
            ClearList_MissionLocations();
            ChangePageState_MissionLocations('View');
        }
        else {
            if (CurrentPageState_MissionLocations == 'Delete')
                ChangePageState_MissionLocations('View');
        }
    }
}
function Clear_cmbMissionSearchResult_MissionLocationsIntroduction() {
    cmbMissionSearchResult_MissionLocationsIntroduction.beginUpdate();
    cmbMissionSearchResult_MissionLocationsIntroduction.removeAll();
    cmbMissionSearchResult_MissionLocationsIntroduction.endUpdate();
}

function MissionLocation_OnAfterUpdate(Response) {
    var MissionLocationNodeText = ObjMissionLocation_MissionLocations.Name;
    var MissionLocationNodeValue = ObjMissionLocation_MissionLocations.CustomCode;
    var MissionLocationCmbValue = '{"Name":"' + ObjMissionLocation_MissionLocations.Name + '","CustomCode":"' + ObjMissionLocation_MissionLocations.CustomCode + '","ParentID":"' + ObjMissionLocation_MissionLocations .ParentID + '"}';
    trvMissionLocationsIntroduction_MissionLocationsIntroduction.beginUpdate();
    switch (CurrentPageState_MissionLocations) {
        case 'Add':
            var newMissionLocationNode = new ComponentArt.Web.UI.TreeViewNode();
            newMissionLocationNode.set_text(MissionLocationNodeText);
            newMissionLocationNode.set_value(MissionLocationNodeValue);
            newMissionLocationNode.set_id(Response[3]);
            newMissionLocationNode.set_imageUrl('Images/TreeView/folder.gif');
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.findNodeById(ObjMissionLocation_MissionLocations.SelectedID).get_nodes().add(newMissionLocationNode);
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.selectNodeById(ObjMissionLocation_MissionLocations.SelectedID);
            break;
        case 'Edit':
            var selectedMissionLocationNode = trvMissionLocationsIntroduction_MissionLocationsIntroduction.findNodeById(Response[3]);
            selectedMissionLocationNode.set_text(MissionLocationNodeText);
            selectedMissionLocationNode.set_value(MissionLocationNodeValue);
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.selectNodeById(Response[3]);
            var selectedMissionLocationcmbItem = cmbMissionSearchResult_MissionLocationsIntroduction.findItemByProperty('Id', Response[3]);
            if (selectedMissionLocationcmbItem != null && selectedMissionLocationcmbItem != undefined) {
                cmbMissionSearchResult_MissionLocationsIntroduction.beginUpdate();
                selectedMissionLocationcmbItem.set_text(MissionLocationNodeText);
                selectedMissionLocationcmbItem.set_value(MissionLocationCmbValue);
                cmbMissionSearchResult_MissionLocationsIntroduction.endUpdate();
            }
            break;
        case 'Delete':
            trvMissionLocationsIntroduction_MissionLocationsIntroduction.findNodeById(ObjMissionLocation_MissionLocations.SelectedID).remove();
            var selectedMissionLocationcmbItem = cmbMissionSearchResult_MissionLocationsIntroduction.findItemByProperty('Id', Response[3]);
            if (selectedMissionLocationcmbItem != null && selectedMissionLocationcmbItem != undefined) {
                var index = selectedMissionLocationcmbItem.Index;
                cmbMissionSearchResult_MissionLocationsIntroduction.beginUpdate();
                cmbMissionSearchResult_MissionLocationsIntroduction.removeItemAt(index);
                cmbMissionSearchResult_MissionLocationsIntroduction.endUpdate();
            }
            break;
    }
    trvMissionLocationsIntroduction_MissionLocationsIntroduction.endUpdate();
    if (CurrentPageState_MissionLocations == 'Add')
        trvMissionLocationsIntroduction_MissionLocationsIntroduction.get_selectedNode().expand();
    Resize_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
    ChangeDirection_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MissionLocations = confirmState;
    if (CurrentPageState_MissionLocations == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MissionLocations').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MissionLocations').value;
    DialogConfirm.Show();
}


function Refresh_trvMissionLocationsIntroduction_MissionLocationsIntroduction() {
    Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
}

function Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction() {
    document.getElementById('loadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').value);
    CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction.callback();
}

function trvMissionLocationsIntroduction_MissionLocationsIntroduction_onNodeSelect(sender, e) {
    if (CurrentPageState_MissionLocations != 'Add') 
        NavigateMissionLocation_MissionLocations(e.get_node());
    MissionSelectedType_MissionLocations = 'Normal';
}

function trvMissionLocationsIntroduction_MissionLocationsIntroduction_onNodeExpand(sender, e) {
    Resize_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
    ChangeDirection_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
}

function NavigateMissionLocation_MissionLocations(selectedMissionLocationNode) {
    if (selectedMissionLocationNode != undefined) {
        switch (MissionSelectedType_MissionLocations) {
            case 'Search':
                var departmentObj = selectedMissionLocationNode.get_value();
                departmentObj = eval('(' + departmentObj + ')');
                document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').value = departmentObj.CustomCode;
                document.getElementById('txtMissionLocationName_MissionLocationIntroduction').value = departmentObj.Name;
                
                break;

            case 'Normal':
                document.getElementById('txtMissionLocationCode_MissionLocationIntroduction').value = selectedMissionLocationNode.get_value();
                document.getElementById('txtMissionLocationName_MissionLocationIntroduction').value = selectedMissionLocationNode.get_text();
                break;
            default:
                break;
        }
        
    }
}

function trvMissionLocationsIntroduction_MissionLocationsIntroduction_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').innerHTML = "";
}

function CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionLocations').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
    }
    else {
        Resize_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
        ChangeDirection_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
    }
}

function CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvMissionLocationsIntroduction_MissionLocationsIntroduction').innerHTML = '';
    ShowConnectionError_MissionLocationsIntroduction();
}

function CharToKeyCode_MissionLocations(str) {
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

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MissionLocations) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateMissionLocation_MissionLocations();
            break;
        case 'Exit':
            ClearList_MissionLocations();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_MissionLocations('View');
}

function tlbItemFormReconstruction_TlbMissionOverallLocation_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvMissionLocationsIntroduction_iFrame').src = parent.ModulePath + 'MissionLocations.aspx';
}

function tlbItemHelp_TlbMissionOverallLocation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMissionOverallLocation');
}

function Resize_trvMissionLocationsIntroduction_MissionLocationsIntroduction() {
    document.getElementById('trvMissionLocationsIntroduction_MissionLocationsIntroduction').style.width = CurrentPageTreeViewsObj.trvMissionLocationsIntroduction_MissionLocationsIntroduction;
}

function ChangeDirection_trvMissionLocationsIntroduction_MissionLocationsIntroduction() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvMissionLocationsIntroduction_MissionLocationsIntroduction').style.direction = 'ltr';
}

function cmbMissionSearchResult_MissionLocationsIntroduction_onChange(sender, e) {

    var selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction = cmbMissionSearchResult_MissionLocationsIntroduction.getSelectedItem();
    if (selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction != undefined && selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction != null) {
        MissionSelectedType_MissionLocations = 'Search';
        NavigateMissionLocation_MissionLocations(selectedItem_cmbMissionSearchResult_MissionLocationsIntroduction);
    }

}



function CallBack_cmbMissionSearchResult_MissionLocationsIntroduction_onBeforeCallback(sender, e) {
    cmbMissionSearchResult_MissionLocationsIntroduction.dispose();
}

function CallBack_cmbMissionSearchResult_MissionLocationsIntroduction_onCallbackComplete(sender, e) {
       var error = document.getElementById('ErrorHiddenField_MissionSearchResult_MissionLocationsIntroduction').value;
    if (error == "") {
       cmbMissionSearchResult_MissionLocationsIntroduction.expand();
       ChangeControlDirection_MissionLocationsIntroduction('cmbMissionSearchResult_MissionLocationsIntroduction_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}
function ChangeControlDirection_MissionLocationsIntroduction(ctrl) {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    if (ctrl == 'All') {
        document.getElementById('cmbMissionSearchResult_MissionLocationsIntroduction_DropDownContent').dir =
        direction;
    }
    else
        document.getElementById(ctrl).style.direction = direction;
}
function CallBack_cmbMissionSearchResult_MissionLocationsIntroduction_onCallbackError(sender, e) {
    ShowConnectionError_MissionLocationsIntroduction();
}

function tlbItemMissionSearch_TlbMissionSearch_MissionLocationsIntroduction_onClick() {
    Fill_cmbMissionSearchResult_MissionLocationsIntroduction();
}

function Fill_cmbMissionSearchResult_MissionLocationsIntroduction() {

    var SearchTerm = document.getElementById('txtSearchTerm_MissionLocationsIntroduction').value;
    ComboBox_onBeforeLoadData('cmbMissionSearchResult_MissionLocationsIntroduction');
    CallBack_cmbMissionSearchResult_MissionLocationsIntroduction.callback(CharToKeyCode_MissionLocations(SearchTerm));
}
function ShowConnectionError_MissionLocationsIntroduction() {
    var error = document.getElementById('hfErrorType_MissionLocationsIntroduction').value;
    var errorBody = document.getElementById('hfConnectionError_MissionLocationsIntroduction').value;
    showDialog(error, errorBody, 'error');
}
 
function txtSerchTerm_MissionLocationsIntroduction_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemMissionSearch_TlbMissionSearch_MissionLocationsIntroduction_onClick();
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