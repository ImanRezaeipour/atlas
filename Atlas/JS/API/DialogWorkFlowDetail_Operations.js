var CurrentPageCombosCallBcakStateObj = new Object();
var ObjexpandingDepPersonnelNode_WorkFlowDetail = null;
var SearchType_cmbSearchField_WorkFlowDetail = '';
var CurrentPageTreeViewsObj = new Object();
var box_SearchByPersonnel_WorkFlowDetail_IsShown = false;
var AdvancedSearchTerm_cmbPersonnel_WorkFlowDetail = '';
var LoadState_cmbPersonnel_WorkFlowDetail = 'Normal';
var IsMatchWholWord = '';
var flowID;

function GetBoxesHeaders_WorkFlowDetail() {
    parent.document.getElementById('Title_DialogWorkFlowDetail').innerHTML = '&nbsp;&nbsp;&nbsp;' + document.getElementById('hfTitle_DialogWorkFlowDetail').value;
    document.getElementById('clmnBarCode_cmbPersonnel_WorkFlowDetail').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_WorkFlowDetail').value;
    document.getElementById('clmnName_cmbPersonnel_WorkFlowDetail').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_WorkFlowDetail').value;
    document.getElementById('clmnCardNum_cmbPersonnel_WorkFlowDetail').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_WorkFlowDetail').value;
    document.getElementById('header_Managers_WorkFlowDetail').innerHTML = document.getElementById('hfheader_Managers_WorkFlowDetail').value;
    document.getElementById('header_WorkFlows_WorkFlowDetail').innerHTML = document.getElementById('hfheader_WorkFlows_WorkFlowDetail').value;
    document.getElementById('header_Operator_WorkFlowDetail').innerHTML = document.getElementById('hfheader_Operator_WorkFlowDetail').value;
    document.getElementById('header_Substitute_WorkFlowDetail').innerHTML = document.getElementById('hfheader_Substitute_WorkFlowDetail').value;
    document.getElementById('header_UnderManagementPersonnel_WorkFlowDetail').innerHTML = document.getElementById('hfheader_UnderManagementPersonnel_WorkFlowDetail').value;
    document.getElementById('cmbSearchField_WorkFlowDetail_Input').value = document.getElementById('hfcmbAlarm_WorkFlowDetail').value;
    document.getElementById('header_SearchByPersonnelBox_WorkFlowDetail').innerHTML = document.getElementById('hfheader_SearchByPersonnelBox_WorkFlowDetail').value;
}
function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
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

function ChangeEnabled_DropDownDive_WorkFlowDetail(state) {
    if (state == 'enabled') {
        document.getElementById('imgbox_SearchByPersonnel_WorkFlowDetail').onclick = function () {
            ShowHide_SearchByPersonnelBox_WorkFlowDetail();
        };
        document.getElementById('imgbox_SearchByPersonnel_WorkFlowDetail').src = 'Images/Ghadir/arrowDown.jpg';
    }
}

function CharToKeyCode_WorkFlowDetail(str) {
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

function tlbItemHelp_TlbWorkFlowDetail_onClick() {
    LoadHelpPage('tlbItemHelp_TlbWorkFlowDetail');
}
function tlbItemFormReconstruction_TlbWorkFlowDetail_onClick() {
    CloseDialogWorkFlowDetail();
    parent.DialogWorkFlowDetail.Show();
}

function CloseDialogWorkFlowDetail() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogWorkFlowDetail_IFrame').src =parent.ModulePath +  'WhitePage.aspx';
    parent.DialogWorkFlowDetail.Close();
}
function tlbItemExit_TlbWorkFlowDetail_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage__WorkFlowDetail').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    parent.DialogWorkFlowDetail.Close();
    DialogConfirm.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}
function tlbItemAccessLevelView_TlbWorkFlowDetail_onClick() {
    if (GridWorkFlows_WorkFlowDetail.getSelectedItems() != undefined && GridWorkFlows_WorkFlowDetail.getSelectedItems() != null && GridWorkFlows_WorkFlowDetail.getSelectedItems().length > 0) {
        ShowDialogAccessLevel();
    }
}

function ShowDialogAccessLevel() {
    DialogAccessLevel.Show();
}

function cmbSearchField_WorkFlowDetail_OnChange(sender, e) {
    if (cmbSearchField_WorkFlowDetail.getSelectedItem() != undefined && cmbSearchField_WorkFlowDetail.getSelectedItem() != null) {
        SearchType_cmbSearchField_WorkFlowDetail = cmbSearchField_WorkFlowDetail.getSelectedItem().get_value();
        ClearList_WorkFlowDetail();
        ResetAllGrids_WorkFlowDetail();
        HideShow_SearchByPersonnelBox_WorkFlowDetail();
        if (SearchType_cmbSearchField_WorkFlowDetail == 'Personnel') {
            document.getElementById('divPersonnelSearch_WorkFlowDetail').style.visibility = 'visible';
            ChangeEnabled_DropDownDive_WorkFlowDetail('enabled');
            document.getElementById('txtSearchTerm_WorkFlowDetail').style.visibility = 'hidden';
            document.getElementById('lblSearchTerm_WorkFlowDetail').style.visibility = 'hidden';
            document.getElementById('tlbView_WorkFlowDetail').style.visibility = 'hidden';
            document.getElementById('chbMatchWholWord_WorkFlowDetail').style.visibility = 'hidden';
            document.getElementById('tdlblMatchWholWord_WorkFlowDetail').style.visibility = 'hidden';

        }
        else {
            document.getElementById('divPersonnelSearch_WorkFlowDetail').style.visibility = 'hidden';
            document.getElementById('txtSearchTerm_WorkFlowDetail').style.visibility = 'visible';
            document.getElementById('lblSearchTerm_WorkFlowDetail').style.visibility = 'visible';
            document.getElementById('tlbView_WorkFlowDetail').style.visibility = 'visible';
            document.getElementById('chbMatchWholWord_WorkFlowDetail').style.visibility = 'visible';
            document.getElementById('tdlblMatchWholWord_WorkFlowDetail').style.visibility = 'visible';
        }
    }
}
function HideShow_SearchByPersonnelBox_WorkFlowDetail() {
    if (box_SearchByPersonnel_WorkFlowDetail_IsShown) {
        slidedown_showHide('box_SearchByPersonnel_WorkFlowDetail');
        box_SearchByPersonnel_WorkFlowDetail_IsShown = false;
    }
}

function ClearList_WorkFlowDetail() {
    document.getElementById('txtSearchTerm_WorkFlowDetail').value = '';
    document.getElementById('txtPersonnelSearch_WorkFlowDetail').value = '';
    document.getElementById('cmbPersonnel_WorkFlowDetail_Input').value = '';
    cmbPersonnel_WorkFlowDetail.unSelect();
}

function View_WorkFlowDetail(sender, e) {
    if (cmbSearchField_WorkFlowDetail.getSelectedItem() != undefined && cmbSearchField_WorkFlowDetail.getSelectedItem() != null) {
        SearchType_cmbSearchField_WorkFlowDetail = cmbSearchField_WorkFlowDetail.getSelectedItem().get_value();
    }
    if (SearchType_cmbSearchField_WorkFlowDetail != '') {
        switch (SearchType_cmbSearchField_WorkFlowDetail) {
            case 'WorkFlow':
                Fill_GridWorkFlows_WorkFlowDetail(null);
                break;
            case 'Manager':
                Fill_GridManagers_WorkFlowDetail(null);
                break;
            case 'Operator':
                Fill_GridOperator_WorkFlowDetail(null);
                break;
            case 'Substitute':
                Fill_GridSubstitute_WorkFlowDetail(null);
                break;
        }
        ResetAllGrids_WorkFlowDetail();
    }
}

function ViewWorkFlow_WorkFlowDetail() {
    HideShow_SearchByPersonnelBox_WorkFlowDetail();
    if (cmbPersonnel_WorkFlowDetail.getSelectedItem() != undefined && cmbPersonnel_WorkFlowDetail.getSelectedItem() != null) {
        Fill_GridWorkFlows_WorkFlowDetail('Personnel');
    }
}
function txtPersonnelSearch_WorkFlowDetail_onKeyPess(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbSearchPersonnel_WorkFlowDetail_onClick();
    }
}

function ResetAllGrids_WorkFlowDetail() {
    GridManagers_WorkFlowDetail.get_table().clearData();
    GridWorkFlows_WorkFlowDetail.get_table().clearData();
    GridOperator_WorkFlowDetail.get_table().clearData();
    GridSubstitute_WorkFlowDetail.get_table().clearData();
    Clear_trvUnderManagementPersonnel_WorkFlowDetail();
}

function SpecialResets_WorkFlowDetail(Caller) {
    var State = SearchType_cmbSearchField_WorkFlowDetail;
    switch (State) {
        case 'WorkFlow':
        case 'Personnel':
            if (Caller == 'WorkFlow')
                GridSubstitute_WorkFlowDetail.get_table().clearData();
            break;
        case 'Manager':
            if (Caller == 'Manager') {
                GridOperator_WorkFlowDetail.get_table().clearData();
                Clear_trvUnderManagementPersonnel_WorkFlowDetail();
            }
            break;
        case 'Operator':
            if (Caller == 'Operator') {
                GridManagers_WorkFlowDetail.get_table().clearData();
                GridSubstitute_WorkFlowDetail.get_table().clearData();
                Clear_trvUnderManagementPersonnel_WorkFlowDetail();
            }
            break;
        case 'Substitute':
            if (Caller == 'Substitute') {
                GridWorkFlows_WorkFlowDetail.get_table().clearData();
                GridOperator_WorkFlowDetail.get_table().clearData();
                Clear_trvUnderManagementPersonnel_WorkFlowDetail();
            }
            break;
    }
}

function Clear_trvUnderManagementPersonnel_WorkFlowDetail() {
    trvUnderManagementPersonnel_WorkFlowDetail.beginUpdate();
    trvUnderManagementPersonnel_WorkFlowDetail.get_nodes().clear();
    trvUnderManagementPersonnel_WorkFlowDetail.endUpdate();
}

function cmbSearchField_WorkFlowDetail_onExpand(sender, e) {
    if (cmbSearchField_WorkFlowDetail.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_WorkFlowDetail == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_WorkFlowDetail = true;
        Fill_cmbSearchField_WorkFlowDetail();
    }
}

function Fill_cmbSearchField_WorkFlowDetail() {
    CallBack_cmbSearchField_WorkFlowDetail.callback();
}

function cmbSearchField_WorkFlowDetail_onCollapse(sender, e) {
    if (cmbSearchField_WorkFlowDetail.getSelectedItem() == undefined) {
        document.getElementById('cmbSearchField_WorkFlowDetail_Input').value = document.getElementById('hfcmbAlarm_WorkFlowDetail').value;
    }
    if (cmbSearchField_WorkFlowDetail.getSelectedItem() != undefined && cmbSearchField_WorkFlowDetail.getSelectedItem() != null) {
        document.getElementById('cmbSearchField_WorkFlowDetail_Input').value = cmbSearchField_WorkFlowDetail.getSelectedItem().get_text();
    }
}

function CallBack_cmbSearchField_WorkFlowDetail_onBeforeCallback(sender, e) {
    cmbSearchField_WorkFlowDetail.dispose();
}

function CallBack_cmbSearchField_WorkFlowDetail_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlowDetailSearchField').value;
    if (error == "") {
        document.getElementById('cmbSearchField_WorkFlowDetail_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSearchField_WorkFlowDetail_DropImage').mousedown();
        cmbSearchField_WorkFlowDetail.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbSearchField_WorkFlowDetail_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbSearchField_WorkFlowDetail_onCallbackError(sender, e) {
    ShowConnectionError_WorkFlowDetail();
}

function ShowConnectionError_WorkFlowDetail() {
    var error = document.getElementById('hfErrorType_WorkFlowDetail').value;
    var errorBody = document.getElementById('hfConnectionError_WorkFlowDetail').value;
    showDialog(error, errorBody, 'error');
}

function Fill_GridManagers_WorkFlowDetail(Caller, Id) {
    document.getElementById('loadingPanel_GridManagers_WorkFlowDetail').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridManagers_WorkFlowDetail').value);
    if (document.getElementById('chbMatchWholWord_WorkFlowDetail').checked)
        IsMatchWholWord = 'true';
    else
        IsMatchWholWord = 'false';
    if (Caller != undefined && Caller != null && Caller != 'Refresh') {
        var SearchTerm = '';
        var ID = Id;
        var State = Caller;
    }
    else if (Caller == 'Refresh') {
        var SearchTerm = '';
        var ID = '0';
        var State = 'Normal';
    }
    else {
        var SearchTerm = document.getElementById('txtSearchTerm_WorkFlowDetail').value;
        var ID = '0';
        var State = 'Normal';
    }
    CallBack_GridManagers_WorkFlowDetail.callback(CharToKeyCode_WorkFlowDetail(SearchTerm), CharToKeyCode_WorkFlowDetail(State), CharToKeyCode_WorkFlowDetail(ID), CharToKeyCode_WorkFlowDetail(IsMatchWholWord));
}

function GridManagers_WorkFlowDetail_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridManagers_WorkFlowDetail').innerHTML = '';
}

function GridManagers_WorkFlowDetail_onItemSelect(sender, e) {
    var Caller = 'Manager';
    SpecialResets_WorkFlowDetail(Caller);
    FillGrids_WorkFlowDetail(Caller);
}

function CallBack_GridManagers_WorkFlowDetail_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Managers_WorkFlowDetail').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload') {

        }
    }
}

function CallBack_GridManagers_WorkFlowDetail_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridManagers_WorkFlowDetail').innerHTML = '';
}

function Refresh_GridManagers_WorkFlowDetail() {
    if (SearchType_cmbSearchField_WorkFlowDetail == 'Manager') {
        ResetAllGrids_WorkFlowDetail();
        Fill_GridManagers_WorkFlowDetail('Refresh');
    }
}

function Fill_GridWorkFlows_WorkFlowDetail(Caller, Id) {
    document.getElementById('loadingPanel_GridWorkFlows_WorkFlowDetail').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridWorkFlows_WorkFlowDetail').value);
    if (document.getElementById('chbMatchWholWord_WorkFlowDetail').checked)
        IsMatchWholWord = 'true';
    else
        IsMatchWholWord = 'false';
    if (Caller != undefined && Caller != null && Caller != 'Refresh') {
        switch (Caller) {
            case 'Personnel':
                var SearchTerm = '';
                var ID = cmbPersonnel_WorkFlowDetail.getSelectedItem().get_value();
                var State = Caller;
                break;
            default:
                var SearchTerm = '';
                var ID = Id;
                var State = Caller;
                break;
        }
    }
    else if (Caller == 'Refresh') {
        var SearchTerm = '';
        var ID = '0';
        var State = 'Normal';
    }
    else {
        var SearchTerm = document.getElementById('txtSearchTerm_WorkFlowDetail').value;
        var ID = '0';
        var State = '0';
    }
    CallBack_GridWorkFlows_WorkFlowDetail.callback(CharToKeyCode_WorkFlowDetail(SearchTerm), CharToKeyCode_WorkFlowDetail(State), CharToKeyCode_WorkFlowDetail(ID), CharToKeyCode_WorkFlowDetail(IsMatchWholWord));
}

function Refresh_GridWorkFlows_WorkFlowDetail() {
    if (SearchType_cmbSearchField_WorkFlowDetail == 'WorkFlow') {
        ResetAllGrids_WorkFlowDetail();
        Fill_GridWorkFlows_WorkFlowDetail('Refresh');
    }
}

function GridWorkFlows_WorkFlowDetail_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridWorkFlows_WorkFlowDetail').innerHTML = '';
}

function GridWorkFlows_WorkFlowDetail_onItemSelect(sender, e) {
    var Caller = 'WorkFlow';
    TlbGridWorkFlows_WorkFlowDetail.get_items().getItemById('tlbItemUnderManagmentPersonnelsRetrieve_TlbGridWorkFlows').set_enabled(true);
    TlbGridWorkFlows_WorkFlowDetail.get_items().getItemById('tlbItemUnderManagmentPersonnelsRetrieve_TlbGridWorkFlows').set_imageUrl('retrieveUndermanagements.png');
    SpecialResets_WorkFlowDetail(Caller);
    FillGrids_WorkFlowDetail(Caller);
}

function CallBack_GridWorkFlows_WorkFlowDetail_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkFlows_WorkFlowDetail').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload') {

        }
    }
}

function CallBack_GridWorkFlows_WorkFlowDetail_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridWorkFlows_WorkFlowDetail').innerHTML = '';
}

function Refresh_GridOperator_WorkFlowDetail() {
    if (SearchType_cmbSearchField_WorkFlowDetail == 'Operator') {
        ResetAllGrids_WorkFlowDetail();
        Fill_GridOperator_WorkFlowDetail('Refresh');
    }
}

function Fill_GridOperator_WorkFlowDetail(Caller, Id) {
    document.getElementById('loadingPanel_GridOperator_WorkFlowDetail').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridOperator_WorkFlowDetail').value);
    if (document.getElementById('chbMatchWholWord_WorkFlowDetail').checked)
        IsMatchWholWord = 'true';
    else
        IsMatchWholWord = 'false';
    if (Caller != undefined && Caller != null && Caller != 'Refresh') {
        var SearchTerm = '';
        var ID = Id;
        var State = Caller;
    }
    else if (Caller == 'Refresh') {
        var SearchTerm = '';
        var ID = '0';
        var State = 'Normal';
    }
    else {
        var SearchTerm = document.getElementById('txtSearchTerm_WorkFlowDetail').value;
        var ID = '0';
        var State = 'Normal';
    }
    CallBack_GridOperator_WorkFlowDetail.callback(CharToKeyCode_WorkFlowDetail(SearchTerm), CharToKeyCode_WorkFlowDetail(State), CharToKeyCode_WorkFlowDetail(ID), CharToKeyCode_WorkFlowDetail(IsMatchWholWord));
}

function GridOperator_WorkFlowDetail_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridOperator_WorkFlowDetail').innerHTML = '';
}

function GridOperator_WorkFlowDetail_onItemSelect(sender, e) {
    var Caller = 'Operator';
    SpecialResets_WorkFlowDetail(Caller);
    FillGrids_WorkFlowDetail(Caller);
}

function CallBack_GridOperator_WorkFlowDetail_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Operator_WorkFlowDetail').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload') {

        }
    }
}

function CallBack_GridOperator_WorkFlowDetail_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridOperator_WorkFlowDetail').innerHTML = '';
}

function Refresh_GridSubstitute_WorkFlowDetail() {
    if (SearchType_cmbSearchField_WorkFlowDetail == 'Substitute') {
        ResetAllGrids_WorkFlowDetail();
        Fill_GridSubstitute_WorkFlowDetail('Refresh');
    }
}

function Fill_GridSubstitute_WorkFlowDetail(Caller, Id) {
    document.getElementById('loadingPanel_GridSubstitute_WorkFlowDetail').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridSubstitute_WorkFlowDetail').value);
    if (document.getElementById('chbMatchWholWord_WorkFlowDetail').checked)
        IsMatchWholWord = 'true';
    else
        IsMatchWholWord = 'false';
    if (Caller != undefined && Caller != null && Caller != 'Refresh') {
        var SearchTerm = '';
        var ID = Id;
        var State = Caller;
    }
    else if (Caller == 'Refresh') {
        var SearchTerm = '';
        var ID = '0';
        var State = 'Normal';
    }
    else {
        var SearchTerm = document.getElementById('txtSearchTerm_WorkFlowDetail').value;
        var ID = '0';
        var State = '0';
    }
    CallBack_GridSubstitute_WorkFlowDetail.callback(CharToKeyCode_WorkFlowDetail(SearchTerm), CharToKeyCode_WorkFlowDetail(State), CharToKeyCode_WorkFlowDetail(ID), CharToKeyCode_WorkFlowDetail(IsMatchWholWord));
}
function GridSubstitute_WorkFlowDetail_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridSubstitute_WorkFlowDetail').innerHTML = '';
}

function GridSubstitute_WorkFlowDetail_onItemSelect(sender, e) {
    var Caller = 'Substitute';
    SpecialResets_WorkFlowDetail(Caller);
    FillGrids_WorkFlowDetail(Caller);
}

function CallBack_GridSubstitute_WorkFlowDetail_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Substitute_WorkFlowDetail').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload') {

        }
    }
}

function CallBack_GridSubstitute_WorkFlowDetail_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridSubstitute_WorkFlowDetail').innerHTML = '';
}

function Refresh_trvUnderManagementPersonnel_WorkFlowDetail() {
    if (cmbSearchField_WorkFlowDetail.getSelectedItem() != undefined && cmbSearchField_WorkFlowDetail.getSelectedItem() != null) {
        var SelectedItems_GridWorkFlows_WorkFlowDetail = GridWorkFlows_WorkFlowDetail.getSelectedItems();
        SearchType_cmbSearchField_WorkFlowDetail = cmbSearchField_WorkFlowDetail.getSelectedItem().get_value();
        if (SelectedItems_GridWorkFlows_WorkFlowDetail != null && SelectedItems_GridWorkFlows_WorkFlowDetail != undefined && SelectedItems_GridWorkFlows_WorkFlowDetail.length > 0) {
            var FlowId_WorkFlowDetail = SelectedItems_GridWorkFlows_WorkFlowDetail[0].getMember('ID').get_text();
            var Caller = 'WorkFlow';
            Fill_trvUnderManagementPersonnel_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
        }
    }
}

function Fill_trvUnderManagementPersonnel_WorkFlowDetail(Caller, Id) {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_WorkFlowDetail').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvUnderManagementPersonnel_WorkFlowDetail').value);
    var SearchTerm = '0';
    var ID = Id;
    var State = Caller;
    CallBack_trvUnderManagementPersonnel_WorkFlowDetail.callback(CharToKeyCode_WorkFlowDetail(ID));
}

function trvUnderManagementPersonnel_WorkFlowDetail_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_WorkFlowDetail').innerHTML = '';
}

function trvUnderManagementPersonnel_WorkFlowDetail_onCallbackComplete(sender, e) {
    if (ObjexpandingDepPersonnelNode_WorkFlowDetail != null) {
        if (ObjexpandingDepPersonnelNode_WorkFlowDetail.Node.get_nodes().get_length() == 0 && ObjexpandingDepPersonnelNode_WorkFlowDetail.HasChild) {
            ObjexpandingDepPersonnelNode_WorkFlowDetail = null;
            GetLoadonDemandError_DepartmetsPersonnel_WorkFlowDetailPage();
        }
        else
            ObjexpandingDepPersonnelNode_WorkFlowDetail = null;
    }
}

function trvUnderManagementPersonnel_WorkFlowDetail_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingDepPersonnelNode_WorkFlowDetail != null)
        ObjexpandingDepPersonnelNode_WorkFlowDetail = null;
    ObjexpandingDepPersonnelNode_WorkFlowDetail = new Object();
    ObjexpandingDepPersonnelNode_WorkFlowDetail.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingDepPersonnelNode_WorkFlowDetail.HasChild = true;
        trvUnderManagementPersonnel_WorkFlowDetail.beginUpdate();
        ObjexpandingDepPersonnelNode_WorkFlowDetail.Node.get_nodes().remove(0);
        trvUnderManagementPersonnel_WorkFlowDetail.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingDepPersonnelNode_WorkFlowDetail.HasChild = false;
        else
            ObjexpandingDepPersonnelNode_WorkFlowDetail.HasChild = true;
    }
}

function trvUnderManagementPersonnel_WorkFlowDetail_onNodeExpand(sender, e) {
    Resize_trvUnderManagementPersonnel_WorkFlowDetail();
    ChangeDirection_trvUnderManagementPersonnel_WorkFlowDetail();
}
function Resize_trvUnderManagementPersonnel_WorkFlowDetail() {
    document.getElementById('trvUnderManagementPersonnel_WorkFlowDetail').style.width = CurrentPageTreeViewsObj.trvUnderManagementPersonnel_trvUnderManagementPersonnel_WorkFlowDetail;
}

function ChangeDirection_trvUnderManagementPersonnel_WorkFlowDetail() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvUnderManagementPersonnel_WorkFlowDetail').style.direction = 'ltr';
}

function CacheTreeViewsSize_WorkFlowDetail() {
    CurrentPageTreeViewsObj.trvUnderManagementPersonnel_WorkFlowDetail = document.getElementById('trvUnderManagementPersonnel_WorkFlowDetail').clientWidth + 'px';
}

function CallBack_trvUnderManagementPersonnel_WorkFlowDetail_onCallbackComplete(sender, e) {
    if (ObjexpandingDepPersonnelNode_WorkFlowDetail != null) {
        if (ObjexpandingDepPersonnelNode_WorkFlowDetail.Node.get_nodes().get_length() == 0 && ObjexpandingDepPersonnelNode_WorkFlowDetail.HasChild) {
            ObjexpandingDepPersonnelNode_WorkFlowDetail = null;
            GetLoadonDemandError_DepartmetsPersonnel_WorkFlowDetailPage();
        }
        else
            ObjexpandingDepPersonnelNode_WorkFlowDetail = null;
    }
}

function CallBack_trvUnderManagementPersonnel_WorkFlowDetail_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvUnderManagementPersonnel_WorkFlowDetail').innerHTML = '';
}

function FillGrids_WorkFlowDetail(Caller) {
    var State = SearchType_cmbSearchField_WorkFlowDetail;
    switch (State) {
        case 'WorkFlow':
            switch (Caller) {
                case 'WorkFlow':
                    var SelectedItems_GridWorkFlows_WorkFlowDetail = GridWorkFlows_WorkFlowDetail.getSelectedItems();
                    var FlowId_WorkFlowDetail = SelectedItems_GridWorkFlows_WorkFlowDetail[0].getMember('ID').get_text();
                    Fill_GridManagers_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    Fill_GridOperator_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    Fill_trvUnderManagementPersonnel_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    break;
                case 'Manager':
                    var SelectedItems_GridManagers_WorkFlowDetail = GridManagers_WorkFlowDetail.getSelectedItems();
                    var ManagerId_WorkFlowDetail = SelectedItems_GridManagers_WorkFlowDetail[0].getMember('ThePerson.ID').get_text();
                    Fill_GridSubstitute_WorkFlowDetail(Caller, ManagerId_WorkFlowDetail);
                    break;
            }
            break;
        case 'Manager':
            switch (Caller) {
                case 'Manager':
                    var SelectedItems_GridManagers_WorkFlowDetail = GridManagers_WorkFlowDetail.getSelectedItems();
                    var ManagerPersonId_WorkFlowDetail = SelectedItems_GridManagers_WorkFlowDetail[0].getMember('ThePerson.ID').get_text();
                    var ManagerId_WorkFlowDetail = SelectedItems_GridManagers_WorkFlowDetail[0].getMember('ID').get_text();
                    Fill_GridSubstitute_WorkFlowDetail(Caller, ManagerPersonId_WorkFlowDetail);
                    Fill_GridWorkFlows_WorkFlowDetail(Caller, ManagerId_WorkFlowDetail);
                    break;
                case 'WorkFlow':
                    var SelectedItems_GridWorkFlows_WorkFlowDetail = GridWorkFlows_WorkFlowDetail.getSelectedItems();
                    var FlowId_WorkFlowDetail = SelectedItems_GridWorkFlows_WorkFlowDetail[0].getMember('ID').get_text();
                    Fill_GridOperator_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    Fill_trvUnderManagementPersonnel_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    break;
            }
            break;
        case 'Operator':
            switch (Caller) {
                case 'WorkFlow':
                    var SelectedItems_GridWorkFlows_WorkFlowDetail = GridWorkFlows_WorkFlowDetail.getSelectedItems();
                    var FlowId_WorkFlowDetail = SelectedItems_GridWorkFlows_WorkFlowDetail[0].getMember('ID').get_text();
                    Fill_GridManagers_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    Fill_trvUnderManagementPersonnel_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    break;
                case 'Operator':
                    var SelectedItems_GridOperator_WorkFlowDetail = GridOperator_WorkFlowDetail.getSelectedItems();
                    var PersonId_WorkFlowDetail = SelectedItems_GridOperator_WorkFlowDetail[0].getMember('Person.ID').get_text();
                    Fill_GridWorkFlows_WorkFlowDetail(Caller, PersonId_WorkFlowDetail);
                    break;
                case 'Manager':
                    var SelectedItems_GridManagers_WorkFlowDetail = GridManagers_WorkFlowDetail.getSelectedItems();
                    var ManagerPersonId_WorkFlowDetail = SelectedItems_GridManagers_WorkFlowDetail[0].getMember('ThePerson.ID').get_text();
                    Fill_GridSubstitute_WorkFlowDetail(Caller, ManagerPersonId_WorkFlowDetail);
                    break;
            }
            break;
        case 'Substitute':
            switch (Caller) {
                case 'Substitute':
                    var SelectedItem_GridSubstitute_WorkFlowDetail = GridSubstitute_WorkFlowDetail.getSelectedItems();
                    var SubstituteId_WorkFlowDetail = SelectedItem_GridSubstitute_WorkFlowDetail[0].getMember('Manager.ID').get_text();
                    Fill_GridManagers_WorkFlowDetail(Caller, SubstituteId_WorkFlowDetail);
                    break;
                case 'Manager':
                    var SelectedItems_GridManagers_WorkFlowDetail = GridManagers_WorkFlowDetail.getSelectedItems();
                    var ManagerId_WorkFlowDetail = SelectedItems_GridManagers_WorkFlowDetail[0].getMember('ID').get_text();
                    Fill_GridWorkFlows_WorkFlowDetail(Caller, ManagerId_WorkFlowDetail);
                    break;
                case 'WorkFlow':
                    var SelectedItems_GridWorkFlows_WorkFlowDetail = GridWorkFlows_WorkFlowDetail.getSelectedItems();
                    var FlowId_WorkFlowDetail = SelectedItems_GridWorkFlows_WorkFlowDetail[0].getMember('ID').get_text();
                    Fill_trvUnderManagementPersonnel_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    Fill_GridOperator_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    break;
            }
            break;
        case 'Personnel':
            switch (Caller) {
                case 'WorkFlow':
                    var SelectedItems_GridWorkFlows_WorkFlowDetail = GridWorkFlows_WorkFlowDetail.getSelectedItems();
                    var FlowId_WorkFlowDetail = SelectedItems_GridWorkFlows_WorkFlowDetail[0].getMember('ID').get_text();
                    Fill_GridManagers_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    Fill_GridOperator_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    Fill_trvUnderManagementPersonnel_WorkFlowDetail(Caller, FlowId_WorkFlowDetail);
                    break;
                case 'Manager':
                    var SelectedItems_GridManagers_WorkFlowDetail = GridManagers_WorkFlowDetail.getSelectedItems();
                    var ManagerPersonId_WorkFlowDetail = SelectedItems_GridManagers_WorkFlowDetail[0].getMember('ThePerson.ID').get_text();
                    Fill_GridSubstitute_WorkFlowDetail(Caller, ManagerPersonId_WorkFlowDetail);
                    break;
            }
            break;
    }
}

function Refresh_trvAccessLevel_AccessGroups() {

}

function trvAccessLevel_AccessGroups_onNodeCheckChange(sender, e) {

}

function trvAccessLevel_AccessGroups_onLoad(sender, e) {

}

function trvAccessLevel_AccessGroups_onNodeExpand(sender, e) {

}

function CallBack_trvAccessLevel_AccessGroups_onCallbackComplete(sender, e) {

}

function CallBack_trvAccessLevel_AccessGroups_onCallbackError(sender, e) {

}

function DialogAccessLevel_OnShow(sender, e) {

    document.getElementById('Title_DialogAccessLevel').innerHTML = document.getElementById('hfTitle_DialogAccessLevel').value;
    document.getElementById('header_AccessLevelBox_AccessGroups').innerHTML = document.getElementById('hfheader_AccessLevelBox_AccessGroups').value;
    var CurrentLangID = parent.parent.CurrentLangID;
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogAccessLevel_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogAccessLevel_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogAccessLevel_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogAccessLevel_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogAccessLevel').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('CloseButton_DialogAccessLevel').align = 'right';
    Fill_trvAccessLevel_AccessGroups();
}

function Fill_trvAccessLevel_AccessGroups() {
    var AccessGroupID = '0';
    var SelectedItem_GridWorkFlows_WorkFlowDetail = GridWorkFlows_WorkFlowDetail.getSelectedItems();
    var AccessGroupID = SelectedItem_GridWorkFlows_WorkFlowDetail[0].getMember('AccessGroup.ID').get_text();
    document.getElementById('loadingPanel_trvAccessLevel_AccessGroups').innerHTML = document.getElementById('hfloadingPanel_trvAccessLevel_AccessGroups').value;
    CallBack_trvAccessLevel_AccessGroups.callback(CharToKeyCode_WorkFlowDetail(AccessGroupID));

}

function ShowHide_SearchByPersonnelBox_WorkFlowDetail() {
    imgbox_SearchByPersonnel_WorkFlowDetail_onClick();
}

function imgbox_SearchByPersonnel_WorkFlowDetail_onClick() {
    CollapseControls_WorkFlowDetail();
    setSlideDownSpeed(200);
    slidedown_showHide('box_SearchByPersonnel_WorkFlowDetail');
    if (box_SearchByPersonnel_WorkFlowDetail_IsShown) {
        box_SearchByPersonnel_WorkFlowDetail_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel_WorkFlowDetail').src = 'Images/Ghadir/arrowDown.jpg';
        cmbPersonnel_WorkFlowDetail.collapse();
    }
    else {
        box_SearchByPersonnel_WorkFlowDetail_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel_WorkFlowDetail').src = 'Images/Ghadir/arrowUp.jpg';
    }
}
function CollapseControls_WorkFlowDetail() {
    cmbPersonnel_WorkFlowDetail.collapse();
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick() {
    Refresh_cmbPersonnel_WorkFlowDetail();
}

function Refresh_cmbPersonnel_WorkFlowDetail() {
    LoadState_cmbPersonnel_WorkFlowDetail = 'Normal';
    SetPageIndex_cmbPersonnel_WorkFlowDetail(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick() {
    SetPageIndex_cmbPersonnel_WorkFlowDetail(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick() {
    if (CurrentPageIndex_cmbPersonnel_WorkFlowDetail != 0) {
        CurrentPageIndex_cmbPersonnel_WorkFlowDetail = CurrentPageIndex_cmbPersonnel_WorkFlowDetail - 1;
        SetPageIndex_cmbPersonnel_WorkFlowDetail(CurrentPageIndex_cmbPersonnel_WorkFlowDetail);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick() {
    if (CurrentPageIndex_cmbPersonnel_WorkFlowDetail < parseInt(document.getElementById('hfPersonnelPageCount_WorkFlowDetail').value) - 1) {
        CurrentPageIndex_cmbPersonnel_WorkFlowDetail = CurrentPageIndex_cmbPersonnel_WorkFlowDetail + 1;
        SetPageIndex_cmbPersonnel_WorkFlowDetail(CurrentPageIndex_cmbPersonnel_WorkFlowDetail);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick() {
    SetPageIndex_cmbPersonnel_WorkFlowDetail(parseInt(document.getElementById('hfPersonnelPageCount_WorkFlowDetail').value) - 1);
}

function CallBack_cmbPersonnel_WorkFlowDetail_onBeforeCallback(sender, e) {
    cmbPersonnel_WorkFlowDetail.dispose();
}

function CallBack_cmbPersonnel_WorkFlowDetail_onCallBackComplete(sender, e) {
    document.getElementById('clmnBarCode_cmbPersonnel_WorkFlowDetail').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_WorkFlowDetail').value;
    document.getElementById('clmnName_cmbPersonnel_WorkFlowDetail').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_WorkFlowDetail').value;
    document.getElementById('clmnCardNum_cmbPersonnel_WorkFlowDetail').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_WorkFlowDetail').value;
    var error = document.getElementById('ErrorHiddenField_Personnel_WorkFlowDetail').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_WorkFlowDetail_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_WorkFlowDetail_DropImage').mousedown();
        else
            cmbPersonnel_WorkFlowDetail.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_WorkFlowDetail_DropDown').style.display = 'none';
    }
}

function CallBack_cmbPersonnel_WorkFlowDetail_onCallbackError(sender, e) {
    ShowConnectionError_WorkFlowDetail();
}

function ShowConnectionError_WorkFlowDetail() {
    var error = document.getElementById('hfErrorType_WorkFlowDetail').value;
    var errorBody = document.getElementById('hfConnectionError_WorkFlowDetail').value;
    showDialog(error, errorBody, 'error');
}

function cmbPersonnel_WorkFlowDetail_onExpand(sender, e) {
    if (cmbPersonnel_WorkFlowDetail.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_WorkFlowDetail == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_WorkFlowDetail = true;
        SetPageIndex_cmbPersonnel_WorkFlowDetail(0);
    }
}

function cmbPersonnel_WorkFlowDetail_OnChange(sender, e) {
    GridManagers_WorkFlowDetail.get_table().clearData();
    GridWorkFlows_WorkFlowDetail.get_table().clearData();
    GridOperator_WorkFlowDetail.get_table().clearData();
    GridSubstitute_WorkFlowDetail.get_table().clearData();
}

function SetPageIndex_cmbPersonnel_WorkFlowDetail(pageIndex) {
    CurrentPageIndex_cmbPersonnel_WorkFlowDetail = pageIndex;
    Fill_cmbPersonnel_WorkFlowDetail(pageIndex);
}

function Fill_cmbPersonnel_WorkFlowDetail(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_WorkFlowDetail').value);
    switch (LoadState_cmbPersonnel_WorkFlowDetail) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtPersonnelSearch_WorkFlowDetail').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbPersonnel_WorkFlowDetail;
            break;
    }
    ComboBox_onBeforeLoadData('cmbPersonnel_WorkFlowDetail');
    CallBack_cmbPersonnel_WorkFlowDetail.callback(CharToKeyCode_WorkFlowDetail(LoadState_cmbPersonnel_WorkFlowDetail), CharToKeyCode_WorkFlowDetail(pageSize.toString()), CharToKeyCode_WorkFlowDetail(pageIndex.toString()), CharToKeyCode_WorkFlowDetail(SearchTerm));
}
function WorkFlowDetail_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_WorkFlowDetail = SearchTerm;
    SetPageIndex_cmbPersonnel_WorkFlowDetail(0);
}

function tlbItemSearch_TlbSearchPersonnel_WorkFlowDetail_onClick() {
    LoadState_cmbPersonnel_WorkFlowDetail = 'Search';
    SetPageIndex_cmbPersonnel_WorkFlowDetail(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_WorkFlowDetail_onClick() {
    LoadState_cmbPersonnel_WorkFlowDetail = 'AdvancedSearch';
    ShowDialogPersonnelSearch('DialogWorkFlowDetail');
}
function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_WorkFlowDetail();
}

function txtSearchTerm_WorkFlowDetail_onKeyPress(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        View_WorkFlowDetail();
    }
}
function tlbItemUnderManagmentPersonnelsRetrieve_TlbWorkFlowDetail_onClick() {

    if (GridWorkFlows_WorkFlowDetail.getSelectedItems() != undefined && GridWorkFlows_WorkFlowDetail.getSelectedItems() != null && GridWorkFlows_WorkFlowDetail.getSelectedItems().length > 0) {
        DialogWaiting.Show();
        flowID = GridWorkFlows_WorkFlowDetail.getSelectedItems()[0].getMember('ID').get_text();       
        UpdateUnderManagmentPersons_WorkFlowDetailPage(CharToKeyCode_WorkFlowDetail(flowID));
    }
}
function UpdateUnderManagmentPersons_WorkFlowDetailPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_WorkFlowDetail').value;
            Response[1] = document.getElementById('hfConnectionError_WorkFlowDetail').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {           
            TlbGridWorkFlows_WorkFlowDetail.get_items().getItemById('tlbItemUnderManagmentPersonnelsRetrieve_TlbGridWorkFlows').set_enabled(false);
            TlbGridWorkFlows_WorkFlowDetail.get_items().getItemById('tlbItemUnderManagmentPersonnelsRetrieve_TlbGridWorkFlows').set_imageUrl('retrieveUndermanagements_Silver.png');
            Fill_trvUnderManagementPersonnel_WorkFlowDetail('WorkFlow', flowID);
        }
    }
}
