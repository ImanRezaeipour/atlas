﻿
var ObjDataAccessLevel_MultiLevelsDataAccessLevels = null;
var CurrentPageState_MultiLevelsDataAccessLevels = 'View';
var ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels = null;
var ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels = null;
var OperationState_MultiLevelsDataAccessLevels = 'Before';
var CurrentPageTreeViewsObj = new Object();
var LoadState_DataAccessLevelsSource_MultiLevelsDataAccessLevels = 'Normal';
var LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels = 'Normal';
var SearchValueSource = '';
var SearchValueTarget = '';


function GetObjDataAccessLevel_MultiLevelsDataAccessLevels() {
    ObjDataAccessLevel_MultiLevelsDataAccessLevels = parent.GetObjDataAccessLevel_MasterDataAccessLevels();
}

function GetBoxesHeaders_MultiLevelsDataAccessLevels() {
    if (ObjDataAccessLevel_MultiLevelsDataAccessLevels != null) {
        document.getElementById('header_DataAccessLevelsSource_MultiLevelsDataAccessLevels').innerHTML = ObjDataAccessLevel_MultiLevelsDataAccessLevels.Source;
        document.getElementById('header_DataAccessLevelsTarget_MultiLevelsDataAccessLevels').innerHTML = ObjDataAccessLevel_MultiLevelsDataAccessLevels.Target;
    }
}

function CacheTreeViewsSize_MultiLevelsDataAccessLevels() {
    CurrentPageTreeViewsObj.trvDataAccessLevelsSource_MultiLevelsDataAccessLevels = document.getElementById('trvDataAccessLevelsSource_MultiLevelsDataAccessLevels').clientWidth + 'px';
    CurrentPageTreeViewsObj.trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels = document.getElementById('trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels').clientWidth + 'px';
}

function Refresh_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels() {
    LoadState_DataAccessLevelsSource_MultiLevelsDataAccessLevels = 'Normal';
    Fill_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
}
function tlbItemSearch_TlbDataAccessLevelsSourceQuickSearch_MultiLevelsDataAccessLevels_onClick() {
    if (document.getElementById('txtDataAccessLevelsSourceQuickSearch_MultiLevelsDataAccessLevels').value != '')
        LoadState_DataAccessLevelsSource_MultiLevelsDataAccessLevels = 'Search';
    else
        LoadState_DataAccessLevelsSource_MultiLevelsDataAccessLevels = 'Normal';
    Fill_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
}
function Fill_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels() {
    document.getElementById('loadingPanel_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels').value);
    var DataAccessLevelKey = ObjDataAccessLevel_MultiLevelsDataAccessLevels != null ? ObjDataAccessLevel_MultiLevelsDataAccessLevels.Key : '';
    switch (LoadState_DataAccessLevelsSource_MultiLevelsDataAccessLevels) {
        case 'Normal':
            //SearchValueSource = '';
            var SearchItem = '';
            break;
        case 'Search':
            // SearchValueSource = document.getElementById('txtDataAccessLevelsSourceQuickSearch_MultiLevelsDataAccessLevels').value;
            var SearchItem = document.getElementById('txtDataAccessLevelsSourceQuickSearch_MultiLevelsDataAccessLevels').value;
            break;
    }
    CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels.callback(CharToKeyCode_MultiLevelsDataAccessLevels(DataAccessLevelKey), CharToKeyCode_MultiLevelsDataAccessLevels(LoadState_DataAccessLevelsSource_MultiLevelsDataAccessLevels), CharToKeyCode_MultiLevelsDataAccessLevels(SearchItem));
}

function trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels').innerHTML = '';
}

function trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels != null)
        ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels = null;
    var DataAccessLevelKey = ObjDataAccessLevel_MultiLevelsDataAccessLevels != null ? ObjDataAccessLevel_MultiLevelsDataAccessLevels.Key : '';
    if (DataAccessLevelKey != '' && DataAccessLevelKey == 'OrganizationUnit' && LoadState_DataAccessLevelsSource_MultiLevelsDataAccessLevels == 'Normal') {
        ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels = new Object();
        ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels.Node = e.get_node();
        if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
            ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels.HasChild = true;
            trvDataAccessLevelsSource_MultiLevelsDataAccessLevels.beginUpdate();
            ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels.Node.get_nodes().remove(0);
            trvDataAccessLevelsSource_MultiLevelsDataAccessLevels.endUpdate();
        }
        else {
            if (e.get_node().get_nodes().get_length() == 0)
                ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels.HasChild = false;
            else
                ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels.HasChild = true;
        }
    }
}

function trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallbackComplete(sender, e) {
    var DataAccessLevelKey = ObjDataAccessLevel_MultiLevelsDataAccessLevels != null ? ObjDataAccessLevel_MultiLevelsDataAccessLevels.Key : '';
    if (DataAccessLevelKey != '' && DataAccessLevelKey == 'OrganizationUnit' && ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels != null) {
        if (ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels.Node.get_nodes().get_length() == 0 && ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels.HasChild) {
            ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels = null;
            GetLoadonDemandError_MultiLevelsDataAccessLevelsPage();
        }
        else
            ObjexpandingDataAccessLevelSourceNode_MultiLevelsDataAccessLevels = null;
    }
}

function GetLoadonDemandError_MultiLevelsDataAccessLevelsPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DataAccessLevelsSource').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
    }
    else {
        Resize_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
        ChangeDirection_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
    }
}

function CallBack_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onCallbackError(sender, e) {
    ShowConnectionError_MultiLevelsDataAccessLevels();
}

function tlbItemAdd_TlbInterAction_MultiLevelsDataAccessLevels_onClick() {
    CurrentPageState_MultiLevelsDataAccessLevels = 'Add';
    UpdateDataAccessLevels_MultiLevelsDataAccessLevels();
    // Refresh_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
}

function tlbItemDelete_TlbInterAction_MultiLevelsDataAccessLevels_onClick() {
    CurrentPageState_MultiLevelsDataAccessLevels = 'Delete';
    UpdateDataAccessLevels_MultiLevelsDataAccessLevels();
}

function UpdateDataAccessLevels_MultiLevelsDataAccessLevels() {
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    var UserID = ObjDialogMasterDataAccessLevels.UserID;
    var SearchKey = ObjDialogMasterDataAccessLevels.SearchKey;
    var SearchTerm = ObjDialogMasterDataAccessLevels.SearchTerm;
    var DataAccessLevelKey = ObjDataAccessLevel_MultiLevelsDataAccessLevels != null ? ObjDataAccessLevel_MultiLevelsDataAccessLevels.Key : '';
    var DataAccessLevelSourceID = '-1';
    var DataAccessLevelTargetID = '-1';
    switch (CurrentPageState_MultiLevelsDataAccessLevels) {
        case 'Add':
            if (document.getElementById('chbSelectAll_MultiLevelsDataAccessLevels').checked)
                DataAccessLevelSourceID = '0';
            else {
                var SelectedNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels = trvDataAccessLevelsSource_MultiLevelsDataAccessLevels.get_selectedNode();
                if (SelectedNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels != undefined)
                    DataAccessLevelSourceID = SelectedNode_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels.get_id();
            }
            break;
        case 'Delete':
            var SelectedNode_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels = trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels.get_selectedNode();
            if (SelectedNode_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels != undefined)
                DataAccessLevelTargetID = SelectedNode_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels.get_id();
            break;
    }
    UpdateDataAccessLevels_MultiLevelsDataAccessLevelsPage(CharToKeyCode_MultiLevelsDataAccessLevels(CurrentPageState_MultiLevelsDataAccessLevels), CharToKeyCode_MultiLevelsDataAccessLevels(DataAccessLevelKey), CharToKeyCode_MultiLevelsDataAccessLevels(DataAccessLevelOperationType), CharToKeyCode_MultiLevelsDataAccessLevels(UserID), CharToKeyCode_MultiLevelsDataAccessLevels(SearchKey), CharToKeyCode_MultiLevelsDataAccessLevels(SearchTerm), CharToKeyCode_MultiLevelsDataAccessLevels(DataAccessLevelSourceID), CharToKeyCode_MultiLevelsDataAccessLevels(DataAccessLevelTargetID));
    DialogWaiting.Show();
}

function UpdateDataAccessLevels_MultiLevelsDataAccessLevelsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_MultiLevelsDataAccessLevels').value;
            Response[1] = document.getElementById('hfConnectionError_MultiLevelsDataAccessLevels').value;
        }
        if (RetMessage[2] == 'success') {
            CurrentPageState_MultiLevelsDataAccessLevels = 'View';
            OperationState_MultiLevelsDataAccessLevels = 'After';
            document.getElementById('chbSelectAll_MultiLevelsDataAccessLevels').checked = false;
            document.getElementById('txtDataAccessLevelsTargetQuickSearch_MultiLevelsDataAccessLevels').value = '';
            LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels = 'Normal';
            Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function Refresh_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels() {
    LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels = 'Normal';
    Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
}
function tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiLevelsDataAccessLevels_onClick() {
    if (document.getElementById('txtDataAccessLevelsTargetQuickSearch_MultiLevelsDataAccessLevels').value != '')
        LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels = 'Search';
    else
        LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels = 'Normal';
    Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
}

function Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels() {
    document.getElementById('loadingPanel_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels').value);
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var UserID = ObjDialogMasterDataAccessLevels.UserID;
    var UserSearchKey = ObjDialogMasterDataAccessLevels.SearchKey;
    var UserSearchTerm = ObjDialogMasterDataAccessLevels.SearchTerm;
    var DataAccessLevelKey = ObjDataAccessLevel_MultiLevelsDataAccessLevels != null ? ObjDataAccessLevel_MultiLevelsDataAccessLevels.Key : '';
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    switch (LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels) {
        case 'Normal':
            //SearchValueTarget = '';
            var SearchItem = '';
            break;
        case 'Search':
            // SearchValueTarget = document.getElementById('txtDataAccessLevelsTargetQuickSearch_MultiLevelsDataAccessLevels').value;
            var SearchItem = document.getElementById('txtDataAccessLevelsTargetQuickSearch_MultiLevelsDataAccessLevels').value;
            break;
    }
    CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels.callback(CharToKeyCode_MultiLevelsDataAccessLevels(UserID), CharToKeyCode_MultiLevelsDataAccessLevels(DataAccessLevelKey), CharToKeyCode_MultiLevelsDataAccessLevels(DataAccessLevelOperationType), CharToKeyCode_MultiLevelsDataAccessLevels(OperationState_MultiLevelsDataAccessLevels), CharToKeyCode_MultiLevelsDataAccessLevels(UserSearchKey), CharToKeyCode_MultiLevelsDataAccessLevels(UserSearchTerm), CharToKeyCode_MultiLevelsDataAccessLevels(LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels), CharToKeyCode_MultiLevelsDataAccessLevels(SearchItem));
}

function trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels').innerHTML = '';
}

function trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels != null)
        ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels = null;
    var DataAccessLevelKey = ObjDataAccessLevel_MultiLevelsDataAccessLevels != null ? ObjDataAccessLevel_MultiLevelsDataAccessLevels.Key : '';
    if (DataAccessLevelKey != '' && DataAccessLevelKey == 'OrganizationUnit' && LoadState_DataAccessLevelsTarget_MultiLevelsDataAccessLevels == 'Normal') {
        ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels = new Object();
        ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels.Node = e.get_node();
        if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
            ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels.HasChild = true;
            trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels.beginUpdate();
            ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels.Node.get_nodes().remove(0);
            trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels.endUpdate();
        }
        else {
            if (e.get_node().get_nodes().get_length() == 0)
                ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels.HasChild = false;
            else
                ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels.HasChild = true;
        }
    }
}

function trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallbackComplete(sender, e) {
    var DataAccessLevelKey = ObjDataAccessLevel_MultiLevelsDataAccessLevels != null ? ObjDataAccessLevel_MultiLevelsDataAccessLevels.Key : '';
    if (DataAccessLevelKey != '' && DataAccessLevelKey == 'OrganizationUnit' && ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels != null) {
        if (ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels.Node.get_nodes().get_length() == 0 && ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels.HasChild) {
            ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels = null;
            GetLoadonDemandError_MultiLevelsDataAccessLevelsPage();
        }
        else
            ObjexpandingDataAccessLevelTargetNode_MultiLevelsDataAccessLevels = null;
    }
}

function CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DataAccessLevelsTarget').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
    }
    else {
        Resize_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
        ChangeDirection_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
    }
}

function CallBack_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onCallbackError(sender, e) {
    ShowConnectionError_MultiLevelsDataAccessLevels();
}

function CharToKeyCode_MultiLevelsDataAccessLevels(str) {
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

function ShowConnectionError_MultiLevelsDataAccessLevels() {
    var error = document.getElementById('hfErrorType_MultiLevelsDataAccessLevels').value;
    var errorBody = document.getElementById('hfConnectionError_MultiLevelsDataAccessLevels').value;
    showDialog(error, errorBody, 'error');
}

function trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onNodeSelect(sender, e) {
    var ObjDialogMasterDataAccessLevels = parent.parent.DialogMasterDataAccessLevels.get_value();
    var DataAccessLevelOperationType = ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType;
    var isDeleteEnabled = e.get_node().get_value();
    var enabled = false;
    var ImageUrl = null;
    if (isDeleteEnabled == 'true' && DataAccessLevelOperationType == 'Single') {
        switch (parent.parent.CurrentLangID) {
            case 'fa-IR':
                ImageUrl = 'arrow-right.png';
                break;
            case 'en-US':
                ImageUrl = 'arrow-left.png';
                break;
        }
        enabled = true;
    }
    else {
        switch (parent.parent.CurrentLangID) {
            case 'fa-IR':
                ImageUrl = 'arrow-right_silver.png';
                break;
            case 'en-US':
                ImageUrl = 'arrow-left_silver.png';
                break;
        }
        enabled = false;
    }
    TlbInterAction_MultiLevelsDataAccessLevels.beginUpdate();
    TlbInterAction_MultiLevelsDataAccessLevels.get_items().getItemById('tlbItemDelete_TlbInterAction_MultiLevelsDataAccessLevels').set_enabled(enabled);
    TlbInterAction_MultiLevelsDataAccessLevels.get_items().getItemById('tlbItemDelete_TlbInterAction_MultiLevelsDataAccessLevels').set_imageUrl(ImageUrl);
    TlbInterAction_MultiLevelsDataAccessLevels.endUpdate();
}

function trvDataAccessLevelsSource_MultiLevelsDataAccessLevels_onNodeExpand(sender, e) {
    Resize_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
    ChangeDirection_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels();
}

function Resize_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels() {
    document.getElementById('trvDataAccessLevelsSource_MultiLevelsDataAccessLevels').style.width = CurrentPageTreeViewsObj.trvDataAccessLevelsSource_MultiLevelsDataAccessLevels;
}

function ChangeDirection_trvDataAccessLevelsSource_MultiLevelsDataAccessLevels() {
    if (parent.parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvDataAccessLevelsSource_MultiLevelsDataAccessLevels').style.direction = 'ltr';
    }
}

function trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels_onNodeExpand(sender, e) {
    Resize_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
    ChangeDirection_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels();
}

function Resize_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels() {
    document.getElementById('trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels').style.width = CurrentPageTreeViewsObj.trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels;
}

function ChangeDirection_trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels() {
    if (parent.parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvDataAccessLevelsTarget_MultiLevelsDataAccessLevels').style.direction = 'ltr';
    }
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}
function txtDataAccessLevelsSourceQuickSearch_MultiLevelsDataAccessLevels_onKeyPress(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbDataAccessLevelsSourceQuickSearch_MultiLevelsDataAccessLevels_onClick();
    }
}
function txtDataAccessLevelsTargetQuickSearch_MultiLevelsDataAccessLevels_onKeyPress(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbDataAccessLevelsTargetQuickSearch_MultiLevelsDataAccessLevels_onClick();
    }
}







