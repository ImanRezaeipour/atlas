
var CurrentPageState_RulesGroup = 'View';
var ConfirmState_RulesGroup = null;
var ObjRuleGroup_RulesGroup = null;
var CurrentPageMode_RulesGroup = null;
var CurrentPageTreeViewsObj = new Object();
var ObjRule_RulesGroup = null;
var LoadState_RulesGroup = 'Normal';

function GetBoxesHeaders_RulesGroup() {
    document.getElementById('header_RulesGroup_RulesGroup').innerHTML = document.getElementById('hfheader_RulesGroup_RulesGroup').value;
    document.getElementById('header_RulesGroup_RulesGroup').innerHTML = document.getElementById('hfheader_RulesGroup_RulesGroup').value;
}

function CacheTreeViewsSize_RulesGroup() {
    CurrentPageTreeViewsObj.trvRulesGroup_RulesGroup = document.getElementById('trvRulesGroup_RulesGroup').clientWidth + 'px';
}

function ShowDialogRulesGroupUpdate(state) {
    var CurrentStateObj_RulesGroup = null;
    switch (state) {
        case 'Add':
            CurrentStateObj_RulesGroup = { "State": "" + CurrentPageState_RulesGroup + "", "RuleGroupID": "" + trvRulesGroup_RulesGroup.get_selectedNode().get_id() + "" };
            break;
        case 'Edit':
            var RuleGroupDescription_RulesGroup = '';
            if (trvRulesGroup_RulesGroup.get_selectedNode().get_value() != undefined && trvRulesGroup_RulesGroup.get_selectedNode().get_value()!= '')
                RuleGroupDescription_RulesGroup = trvRulesGroup_RulesGroup.get_selectedNode().get_value();
            CurrentStateObj_RulesGroup = { "State": "" + CurrentPageState_RulesGroup + "", "RuleGroupID": "" + trvRulesGroup_RulesGroup.get_selectedNode().get_id() + "", "RuleGroupName": "" + trvRulesGroup_RulesGroup.get_selectedNode().get_text() + "", "RuleGroupDescription": "" + RuleGroupDescription_RulesGroup +"" };
    }
    parent.DialogRulesGroupUpdate.set_value(CurrentStateObj_RulesGroup);
    parent.DialogRulesGroupUpdate.Show();
}

function ShowDialogMasterRulesView() {
    if (trvRulesGroup_RulesGroup.get_selectedNode() != undefined) {
        var ObjDialogMasterRulesView = new Object();
        ObjDialogMasterRulesView.RuleGroupID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
        parent.DialogMasterRulesView.set_value(ObjDialogMasterRulesView);
        parent.DialogMasterRulesView.Show();
    }
}

function trvRulesGroup_RulesGroup_onNodeMouseDoubleClick() {
    ShowDialogMasterRulesView();
}

function RuleGroup_onOperationComplete(objRuleGroup) {
    trvRulesGroup_RulesGroup.beginUpdate();
    switch (objRuleGroup.State) {
        case 'Add':
            var newRuleGroupNode = new ComponentArt.Web.UI.TreeViewNode();
            newRuleGroupNode.set_text(objRuleGroup.Name);
            newRuleGroupNode.set_value(objRuleGroup.Description);
            newRuleGroupNode.set_id(objRuleGroup.ID);
            newRuleGroupNode.set_imageUrl('Images/TreeView/folder.gif');
            trvRulesGroup_RulesGroup.get_nodes().getNode(0).get_nodes().add(newRuleGroupNode);
            trvRulesGroup_RulesGroup.selectNodeById(objRuleGroup.ID);
            break;
        case 'Edit':
            var selectedRuleGroupNode = trvRulesGroup_RulesGroup.findNodeById(objRuleGroup.ID);
            selectedRuleGroupNode.set_text(objRuleGroup.Name);
            selectedRuleGroupNode.set_value(objRuleGroup.Description);
            trvRulesGroup_RulesGroup.selectNodeById(objRuleGroup.ID);
            break;
    }
    trvRulesGroup_RulesGroup.endUpdate();
}

function Refresh_trvRulesGroup_RulesGroup() {
    LoadState_RulesGroup = "Normal";
    document.getElementById('txtQuickSearch_RulesGroup').value = "";
    Fill_trvRulesGroup_RulesGroup();
}

function SetActionMode_RulesGroup(state) {
    document.getElementById('ActionMode_RulesGroup').innerHTML = document.getElementById("hf" + state + "_RulesGroup").value;
}

function Fill_trvRulesGroup_RulesGroup() {
    document.getElementById('loadingPanel_trvRulesGroup_RulesGroup').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvRulesGroup_RulesGroup').value);
    CallBack_trvRulesGroup_RulesGroup.callback();
}

function tlbItemNew_TlbRulesGroup_onClick() {
    CurrentPageState_RulesGroup = 'Add';
    ShowDialogRulesGroupUpdate(CurrentPageState_RulesGroup);
}

function tlbItemEdit_TlbRulesGroup_onClick() {
    console.log('tlbItemEdit_TlbRulesGroup_onClick');

    var selectedNode_trvRulesGroup_RulesGroup = trvRulesGroup_RulesGroup.get_selectedNode();
    if (selectedNode_trvRulesGroup_RulesGroup != undefined && selectedNode_trvRulesGroup_RulesGroup.get_nodes().get_length() == 0) {
        CurrentPageState_RulesGroup = 'Edit';

        CurrentPageMode_RulesGroup = 'RuleGroup';
        ChangePageState_RulesGroup('Edit');

        //if (CurrentPageMode_RulesGroup == 'RuleGroup') {
        //    ChangePageState_RulesGroup('Edit');
        //}
        //else {
        //    ShowDialogRulesGroupUpdate(CurrentPageState_RulesGroup);
        //}
        
    }
}

function tlbItemDelete_TlbRulesGroup_onClick() {
    var selectedNode_trvRulesGroup_RulesGroup = trvRulesGroup_RulesGroup.get_selectedNode();
    if (selectedNode_trvRulesGroup_RulesGroup != undefined && selectedNode_trvRulesGroup_RulesGroup.get_nodes().get_length() == 0) {
        CurrentPageState_RulesGroup = 'Delete';
        ShowDialogConfirm('Delete');
    }
}

function tlbItemRulesView_TlbRulesGroup_onClick() {
    ShowDialogMasterRulesView();
}

function tlbItemExit_TlbRulesGroup_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_RulesGroup = confirmState;
    if (CurrentPageState_RulesGroup == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_RulesGroup').value;
    else        
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RulesGroup').value;
    DialogConfirm.Show();
}

function UpdateRuleGroup_RulesGroup() {
    var selectedRuleGroupID = '0';
    if (trvRulesGroup_RulesGroup.get_selectedNode() != undefined)
        selectedRuleGroupID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
    ObjRuleGroup_RulesGroup = new Object();
    ObjRuleGroup_RulesGroup.SelectedID = selectedRuleGroupID;
    ObjRuleGroup_RulesGroup.Description = trvRulesGroup_RulesGroup.get_selectedNode().get_value();
    UpdateRuleGroup_RulesGroupPage(CharToKeyCode_RulesGroup(CurrentPageState_RulesGroup), CharToKeyCode_RulesGroup(selectedRuleGroupID));
    DialogWaiting.Show();
}

function UpdateRuleGroup_RulesGroupPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            RuleGroup_OnAfterUpdate(Response[3]);
            CurrentPageState_RulesGroup = 'View';
        }
        else {
            if (CurrentPageState_RulesGroup == 'Delete')
                CurrentPageState_RulesGroup = 'View';
        }
    }
}

function RuleGroup_OnAfterUpdate(StrObjRuleGroup) {
    var ObjRuleGroup = eval('(' + StrObjRuleGroup + ')');
    trvRulesGroup_RulesGroup.beginUpdate();
    switch (CurrentPageState_RulesGroup) {
        case 'Delete':
            trvRulesGroup_RulesGroup.findNodeById(ObjRuleGroup.ID).remove();
            break;
        case 'Copy':
            var newRuleGroupNode = new ComponentArt.Web.UI.TreeViewNode();
            newRuleGroupNode.set_text(ObjRuleGroup.Name);
            newRuleGroupNode.set_value(ObjRuleGroup_RulesGroup.Description);
            newRuleGroupNode.set_id(ObjRuleGroup.ID);
            newRuleGroupNode.set_imageUrl('Images/TreeView/folder.gif');
            trvRulesGroup_RulesGroup.get_nodes().getNode(0).get_nodes().add(newRuleGroupNode);
            trvRulesGroup_RulesGroup.selectNodeById(ObjRuleGroup.ID);
            break;

        case 'RuleGroup':
            var newRuleGroupNode = new ComponentArt.Web.UI.TreeViewNode();
            newRuleGroupNode.set_text(ObjRuleGroup.Name);
            newRuleGroupNode.set_value(ObjRuleGroup_RulesGroup.Description);
            newRuleGroupNode.set_id(ObjRuleGroup.ID);
            newRuleGroupNode.set_imageUrl('Images/TreeView/group.png');
            trvRulesGroup_RulesGroup.get_nodes().getNode(0).get_nodes().add(newRuleGroupNode);
            trvRulesGroup_RulesGroup.selectNodeById(ObjRuleGroup.ID);
            break;
    }

    trvRulesGroup_RulesGroup.endUpdate();
    Resize_trvRulesGroup_RulesGroup();
    ChangeDirection_trvRulesGroup_RulesGroup();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_RulesGroup) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRuleGroup_RulesGroup();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_RulesGroup = 'View';
}

function trvRulesGroup_RulesGroup_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvRulesGroup_RulesGroup').innerHTML = "";
}

function trvRulesGroup_RulesGroup_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RulesGroup').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvRulesGroup_RulesGroup();
    }
    else {
        Resize_trvRulesGroup_RulesGroup();
        ChangeDirection_trvRulesGroup_RulesGroup();
    }
}

function SetWrapper_Alert_Box_RulesGroup(title, message, type) {
    SetWrapper_Alert_Box(document.RulesGroupForm.id);
    showDialog(title, message, type);
}

function CharToKeyCode_RulesGroup(str) {
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

function trvRulesGroup_RulesGroup_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvRulesGroup_RulesGroup').innerHTML = '';
    ShowConnectionError_RulesGroup();
}

function ShowConnectionError_RulesGroup() {
    var error = document.getElementById('hfErrorType_RulesGroup').value;
    var errorBody = document.getElementById('hfConnectionError_RulesGroup').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemLeaveBudget_TlbRulesGroup_onClick() {
    ShowDialogLeaveBudget();
}

function ShowDialogLeaveBudget() {
    if (trvRulesGroup_RulesGroup.get_selectedNode() != undefined) {
        var ObjDialogLeaveBudget = new Object();
        ObjDialogLeaveBudget.RuleGroupID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
        parent.DialogLeaveBudget.set_value(ObjDialogLeaveBudget);
        parent.DialogLeaveBudget.Show();
    }
}

function tlbItemFormReconstruction_TlbRulesGroup_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvRulesGroupIntroduction_iFrame').src = parent.ModulePath + 'RulesGroup.aspx';
}

function tlbItemRuleGroupCopy_TlbRulesGroup_onClick() {    
    CurrentPageState_RulesGroup = 'Copy';
    UpdateRuleGroup_RulesGroup();
}

function tlbItemHelp_TlbRulesGroup_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRulesGroup');
}

function trvRulesGroup_RulesGroup_onNodeExpand(sender, e) {
    Resize_trvRulesGroup_RulesGroup();
    ChangeDirection_trvRulesGroup_RulesGroup();
}

function Resize_trvRulesGroup_RulesGroup() {
    document.getElementById('trvRulesGroup_RulesGroup').style.width = CurrentPageTreeViewsObj.trvRulesGroup_RulesGroup;
}

function ChangeDirection_trvRulesGroup_RulesGroup() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvRulesGroup_RulesGroup').style.direction = 'ltr';
}
function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}





function tlbItemNewGroup_TlbRulesGroup_onClick() {
    
        CurrentPageMode_RulesGroup = 'RuleGroup';
        ChangePageState_RulesGroup('AddGroup');
        ClearList_RulesGroup();
    
}



function ChangePageState_RulesGroup(state) {
    console.log('ChangePageState_RulesGroup');

    CurrentPageState_RulesGroup = state;

    SetActionMode_RulesGroup(state);

    if (state == 'AddGroup' || state == 'Edit' || state == 'Delete') {
        if (TlbRulesGroup.get_items().getItemById('tlbItemNewGroup_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemNewGroup_TlbRulesGroup').set_enabled(false);
            TlbRulesGroup.get_items().getItemById('tlbItemNewGroup_TlbRulesGroup').set_imageUrl('group_silver.png');
        }
        if (TlbRulesGroup.get_items().getItemById('tlbItemNew_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemNew_TlbRulesGroup').set_enabled(false);
            TlbRulesGroup.get_items().getItemById('tlbItemNew_TlbRulesGroup').set_imageUrl('add_silver.png');
        }
        if (TlbRulesGroup.get_items().getItemById('tlbItemEdit_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemEdit_TlbRulesGroup').set_enabled(false);
            TlbRulesGroup.get_items().getItemById('tlbItemEdit_TlbRulesGroup').set_imageUrl('edit_silver.png');
        }
        if (TlbRulesGroup.get_items().getItemById('tlbItemDelete_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemDelete_TlbRulesGroup').set_enabled(false);
            TlbRulesGroup.get_items().getItemById('tlbItemDelete_TlbRulesGroup').set_imageUrl('remove_silver.png');
        }

        TlbRulesGroup.get_items().getItemById('tlbItemSave_TlbRulesGroup').set_enabled(true);
        TlbRulesGroup.get_items().getItemById('tlbItemSave_TlbRulesGroup').set_imageUrl('save.png');
        TlbRulesGroup.get_items().getItemById('tlbItemCancel_TlbRulesGroup').set_enabled(true);
        TlbRulesGroup.get_items().getItemById('tlbItemCancel_TlbRulesGroup').set_imageUrl('cancel.png');

        switch (CurrentPageMode_RulesGroup) {
            case 'RuleGroup':
                document.getElementById('txtRuleGroupName_RulesGroup').disabled = '';
                break;
        }

        if (state == 'Edit')
            NavigateRulesGroup_RulesGroup(trvRulesGroup_RulesGroup.get_selectedNode());
        if (state == 'Delete')
            RulesGroup_onSave();
    }


    if (state == 'View') {
        if (TlbRulesGroup.get_items().getItemById('tlbItemNewGroup_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemNewGroup_TlbRulesGroup').set_enabled(true);
            TlbRulesGroup.get_items().getItemById('tlbItemNewGroup_TlbRulesGroup').set_imageUrl('group.png');
        }
        if (TlbRulesGroup.get_items().getItemById('tlbItemNew_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemNew_TlbRulesGroup').set_enabled(true);
            TlbRulesGroup.get_items().getItemById('tlbItemNew_TlbRulesGroup').set_imageUrl('add.png');
        }
        if (TlbRulesGroup.get_items().getItemById('tlbItemEdit_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemEdit_TlbRulesGroup').set_enabled(true);
            TlbRulesGroup.get_items().getItemById('tlbItemEdit_TlbRulesGroup').set_imageUrl('edit.png');
        }
        if (TlbRulesGroup.get_items().getItemById('tlbItemDelete_TlbRulesGroup') != null) {
            TlbRulesGroup.get_items().getItemById('tlbItemDelete_TlbRulesGroup').set_enabled(true);
            TlbRulesGroup.get_items().getItemById('tlbItemDelete_TlbRulesGroup').set_imageUrl('remove.png');
        }

        TlbRulesGroup.get_items().getItemById('tlbItemSave_TlbRulesGroup').set_enabled(false);
        TlbRulesGroup.get_items().getItemById('tlbItemSave_TlbRulesGroup').set_imageUrl('save_silver.png');
        TlbRulesGroup.get_items().getItemById('tlbItemCancel_TlbRulesGroup').set_enabled(false);
        TlbRulesGroup.get_items().getItemById('tlbItemCancel_TlbRulesGroup').set_imageUrl('cancel_silver.png');

        document.getElementById('txtRuleGroupName_RulesGroup').disabled = 'disabled';

    }
}


function SetActionMode_RuleGroups(state) {
    document.getElementById('ActionMode_RulesGroup').innerHTML = document.getElementById('hf' + state + '_RulesGroup').value + (CurrentPageState_RulesGroup != 'View' ? ' ' + document.getElementById('hf' + CurrentPageMode_RulesGroup + '_RulesGroup').value : '');
}


function ClearList_RulesGroup() {
    document.getElementById('txtRuleGroupName_RulesGroup').value = '';
}



function tlbItemCancel_TlbRulesGroup_onClick() {
    DialogConfirm.Close();
    ChangePageState_RulesGroup('View');
}

function tlbItemSave_TlbRulesGroup_onClick() {
    RulesGroup_onSave();
}

function RulesGroup_onSave() {
    if (CurrentPageState_RulesGroup != 'Delete')
        UpdateRuleGroup_RulesGroup();
    else
        ShowDialogConfirm('Delete');
}

function UpdateRuleGroup_RulesGroup() {

    ObjRule_RulesGroup = new Object();
    ObjRule_RulesGroup.TargetType = null;
    ObjRule_RulesGroup.RuleParentId = '0';
    ObjRule_RulesGroup.SelectedID = '0';
    ObjRule_RulesGroup.Name = null;


    ObjRule_RulesGroup.TargetType = CurrentPageState_RulesGroup;


    if (CurrentPageState_RulesGroup != 'Delete') {
        console.log(CurrentPageState_RulesGroup);
        switch (CurrentPageState_RulesGroup) {
            case 'AddGroup':

                ObjRule_RulesGroup.Name = document.getElementById('txtRuleGroupName_RulesGroup').value;
               // ObjRule_RulesGroup.SelectedID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
               // ObjRule_RulesGroup.RuleParentId = 

                break;

            case 'Edit':
                ObjRule_RulesGroup.Name = document.getElementById('txtRuleGroupName_RulesGroup').value;
                ObjRule_RulesGroup.SelectedID = trvRulesGroup_RulesGroup.get_selectedNode().get_id();
               // ObjRule_RulesGroup.RuleParentId =

                break;

        }
    }
    UpdateRuleGroup_RulesGroupPage(CharToKeyCode_RulesGroup(CurrentPageState_RulesGroup), CharToKeyCode_RulesGroup(ObjRule_RulesGroup.SelectedID), CharToKeyCode_RulesGroup(ObjRule_RulesGroup.Name),  CharToKeyCode_RulesGroup(ObjRule_RulesGroup.RuleParentId));
    DialogWaiting.Show();
}


function tlbItemSearch_TlbQuickSearch_RulesGroup_onClick() {
    if (document.getElementById('txtQuickSearch_RulesGroup').value != '')
        LoadState_RulesGroup = 'Search';
    else
        LoadState_RulesGroup = 'Normal';
    Fill_trvRulesGroup_RulesGroup();
}


function Fill_trvRulesGroup_RulesGroup() {
    switch (LoadState_RulesGroup) {
        case 'Normal':
            var SearchItem = '';
            break;
        case 'Search':
            var SearchItem = document.getElementById('txtQuickSearch_RulesGroup').value;
            break;
    }

    document.getElementById('loadingPanel_trvRulesGroup_RulesGroup').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvRulesGroup_RulesGroup').value);
    CallBack_trvRulesGroup_RulesGroup.callback(CharToKeyCode_RulesGroup(LoadState_RulesGroup), CharToKeyCode_RulesGroup(SearchItem));
}


function NavigateRulesGroup_RulesGroup(selectedRulesGroupNode) {
    console.log('NavigateRulesGroup_RulesGroup');


    if (selectedRulesGroupNode != undefined) {

        ObjTarget_RulesGroup = selectedRulesGroupNode.get_value();
        ObjTarget_RulesGroup = eval('(' + ObjTarget_RulesGroup + ')');
       // CurrentPageMode_RulesGroup = ObjTarget_RulesGroup.TargetType;
        console.log(CurrentPageMode_RulesGroup);
        switch (CurrentPageMode_RulesGroup) {
            case 'RuleGroup':
                document.getElementById('txtRuleGroupName_RulesGroup').value = selectedRulesGroupNode.get_text();

                break;

        }
    }
}

function tlbItemDelete_TlbReports_onClick() {
    ChangePageState_RulesGroup('Delete');
}
