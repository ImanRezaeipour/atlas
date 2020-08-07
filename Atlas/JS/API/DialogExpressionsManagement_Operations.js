
var CurrentPageState_Expressions = 'Edit';
var ConfirmState_Expressions = null;
var ObjExpression_Expressions = new Object();
var Obj_Exprsn_ExpandedNodes = null;
var SearchBox_IsShown_Expression_Expressions = null;
var LoadState_Expressions = 'Normal';
var CurrentPageIndex_GridExpressions_Expressions = 0;
var CurrentPageTreeViewsObj = new Object();


function trvExpressions_Expressions_onNodeSelect(sender, e) {
    ObjExpression_Expressions.Parent_ID = e.get_node().get_id();
}

function trvExpressions_Expressions_onNodeBeforeExpand(sender, e) {
    Obj_Exprsn_ExpandedNodes = new Object();
    Obj_Exprsn_ExpandedNodes.Node = e.get_node();
    if (
        e.get_node().get_nodes().get_length() == 1 &&
        (
            e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined ||
            e.get_node().get_nodes().get_nodeArray()[0].get_id() == ''
        )
       ) {
        Obj_Exprsn_ExpandedNodes.HasChild = true;
        trvExpressions_Expressions.beginUpdate();
        Obj_Exprsn_ExpandedNodes.Node.get_nodes().remove(0);
        trvExpressions_Expressions.endUpdate();

        Resize_trvExpressions_Expressions();
        ChangeDirection_trvExpressions_Expressions();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            Obj_Exprsn_ExpandedNodes.HasChild = false;
        else
            Obj_Exprsn_ExpandedNodes.HasChild = true;
    }
}

function trvExpressions_Expressions_onCallbackComplete(sender, e) {
    if (Obj_Exprsn_ExpandedNodes != null) {
        if (Obj_Exprsn_ExpandedNodes.Node.get_nodes().get_length() == 0 && Obj_Exprsn_ExpandedNodes.HasChild) {
            Obj_Exprsn_ExpandedNodes = null;
            GetLoadonDemandError_ExpressionPage();
        }
        else{
            Obj_Exprsn_ExpandedNodes = null;
            Resize_trvExpressions_Expressions();
            ChangeDirection_trvExpressions_Expressions();
        }
    }
}

function CallBack_trvExpressions_Expressions_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_trvExpressions_Expressions').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            CallBack_trvExpression_Expressions.callback();
    } 
}

function CallBack_trvExpressions_Expressions_onCallbackError(sender, e) {
    ShowConnectionError_Expressions();
}

function GetLoadonDemandErrorsPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}
function GetLoadonDemandError_ExpressionsPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function SetActionMode_Expressions(state) {
    document.getElementById('ActionMode_Expressions').innerHTML = document.getElementById("hf" + state + "_Expressions").value;
}

function SetPageIndex_GridExpressions_Expressions(pageIndex) {
    CurrentPageIndex_GridExpressions_Expressions = pageIndex;
    Fill_GridExpressions_Expressions(pageIndex);
}

function Fill_GridExpressions_Expressions(pageIndex) {
    document.getElementById('loadingPanel_GridExpressions_Expressions').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridExpressions_Expressions').value);
    var pageSize = parseInt(document.getElementById('hfExpressionsPageSize_Expressions').value);
    var searchKey = 'NotSpecified';
    var searchTerm = '';
    if (SearchBox_IsShown_Expression_Expressions) {
        searchTerm = document.getElementById('txtSearchTerm_Expressions').value;
    }
    CallBack_GridExpressions_Expression.callback(CharToKeyCode_Expressions(LoadState_Expressions), CharToKeyCode_Expressions(pageSize.toString()), CharToKeyCode_Expressions(pageIndex.toString()), CharToKeyCode_Expressions(searchKey), CharToKeyCode_Expressions(searchTerm));
}

function tlbItemSearch_TlbExpressionQuickSearch_onClick(sender, e) {
    SearchBox_IsShown_Expression_Expressions = true;
    LoadState_Expressions = 'Search';
    SetPageIndex_GridExpressions_Expressions(0);
}

function GridExpressions_Expressions_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridExpressions_Expressions').innerHTML = '';
}

function GridExpressions_Expressions_onItemSelect(sender, e) {
    if (CurrentPageState_Expressions != 'Add')
        NavigateExpression_Expressions(e.get_item());
}

function CallBack_GridExpressions_Expression_OnCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Expressions').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridExpressions_Expressions(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else
        Changefooter_GridExpressions_Expressions();
}

function Changefooter_GridExpressions_Expressions() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridExpressions_Expressions').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfExpressionsPageCount_Expressions').value) > 0 ? CurrentPageIndex_GridExpressions_Expressions + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfExpressionsPageCount_Expressions').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridExpressions_Expressions').innerHTML = retfooterVal;
}

function CallBack_GridExpressions_Expression_onCallbackError(sender, e) {
    ShowConnectionError_Expressions();
}

function tlbExpressionLast_TlbPaging_GridExpressions_Expressions_onClick(sender, e) {
    ChangeLoadState_GridExpressions_Expressions('Normal');
}

function ChangeLoadState_GridExpressions_Expressions(state) {
    LoadState_Expressions = state;
    SetPageIndex_GridExpressions_Expressions(0);
}

function tlbItemRefresh_TlbPaging_GridExpressions_Expressions_onClick(sender, e) {
    ChangeLoadState_GridExpressions_Expressions('Normal');
}

function ChangeLoadState_GridExpressions_Expressions(state) {
    LoadState_Expressions = state;
    SetPageIndex_GridExpressions_Expressions(0);
}

function tlbItemFirst_TlbPaging_GridExpressions_Expressions_onClick(sender, e) {
    SetPageIndex_GridExpressions_Expressions(0);
}

function tlbItemBefore_TlbPaging_GridExpressions_Expressions_onClick(sender, e) {
    if (CurrentPageIndex_GridExpressions_Expressions != 0) {
        CurrentPageIndex_GridExpressions_Expressions = CurrentPageIndex_GridExpressions_Expressions - 1;
        SetPageIndex_GridExpressions_Expressions(CurrentPageIndex_GridExpressions_Expressions);
    }
}

function tlbItemNext_TlbPaging_GridExpressions_Expressions_onClick(sender, e) {
    if (CurrentPageIndex_GridExpressions_Expressions < parseInt(document.getElementById('hfExpressionsPageCount_Expressions').value) - 1) {
        CurrentPageIndex_GridExpressions_Expressions = CurrentPageIndex_GridExpressions_Expressions + 1;
        SetPageIndex_GridExpressions_Expressions(CurrentPageIndex_GridExpressions_Expressions);
    }
}

function tlbItemLast_TlbPaging_GridExpressions_Expressions_onClick(sender, e) {
    SetPageIndex_GridExpressions_Expressions(parseInt(document.getElementById('hfExpressionsPageCount_Expressions').value) - 1);
}

function tlbExpressionsHelp_TlbExpressions_onClick(sender, e) {
    LoadHelpPage('tlbExpressionsHelp_TlbExpressions');
}

function tlbExpressionsExit_TlbExpressions_onClick(sender, e) {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmStates = confirmState;
    if (CurrentPageState == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessages').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessages').value;
    DialogConfirm.Show();
}

function tlbItemNew_TlbExpressions_onClick(sender, e) {
    ClearControls_Expressions();
    ChangePageState_Expressions('Add');
}

function tlbItemEdit_TlbExpressions_onClick(sender, e) {
    ChangePageState_Expressions('Edit');
}

function tlbItemDelete_TlbExpressions_onClick(sender, e) {
    ChangePageState_Expressions('Delete');
}

function tlbItemSave_TlbExpressions_onClick(sender, e) {
    Expressions_onSave();
}

function tlbItemCancel_TlbTlbExpressions_onClick(sender, e) {
    Expressions_Cancel();
}

function tlbItemFormReconstruction_TlbExpressions_onClick(sender, e) {
    CloseDialogExpressionsManagement();
    parent.DialogExpressionsManagement.Show();
}

function tlbItemExit_TlbExpressions_onClick(sender, e) {
    ShowDialogConfirm('Exit');
}

function CharToKeyCode_Expressions(str) {
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

function ClearList_Expressions() {
    if (CurrentPageState_Expressions != 'Edit') {
        RefreshExpression_Expressions();
    }
}

function GetBoxesHeader_Expressions() {
    parent.document.getElementById('Title_DialogExpressionsManagement').innerHTML = document.getElementById('hfTitle_DialogExpressionsManagement').value;
    document.getElementById('header_ExpressionsBox_Expressions').innerHTML = document.getElementById('hfheader_ExpressionsBox_Expressions').value;
    document.getElementById('footer_GridExpressions_Expressions').innerHTML = document.getElementById('hffooter_GridExpressions_Expressions').value;
}

function ChangePageState_Expressions(State) {

    CurrentPageState_Expressions = State;
    SetActionMode_Expressions(State);

    if (State == 'Add' || State == 'Edit' || State == 'Delete') {
        if (TlbExpressions.get_items().getItemById('tlbItemNew_TlbExpressions') != null) {
            TlbExpressions.get_items().getItemById('tlbItemNew_TlbExpressions').set_enabled(false);
            TlbExpressions.get_items().getItemById('tlbItemNew_TlbExpressions').set_imageUrl('add_silver.png');
        }
        if (TlbExpressions.get_items().getItemById('tlbItemEdit_TlbExpressions') != null) {
            TlbExpressions.get_items().getItemById('tlbItemEdit_TlbExpressions').set_enabled(false);
            TlbExpressions.get_items().getItemById('tlbItemEdit_TlbExpressions').set_imageUrl('edit_silver.png');
        }
        if (TlbExpressions.get_items().getItemById('tlbItemDelete_TlbExpressions') != null) {
            TlbExpressions.get_items().getItemById('tlbItemDelete_TlbExpressions').set_enabled(false);
            TlbExpressions.get_items().getItemById('tlbItemDelete_TlbExpressions').set_imageUrl('remove_silver.png');
        }

        TlbExpressions.get_items().getItemById('tlbItemSave_TlbExpressions').set_enabled(true);
        TlbExpressions.get_items().getItemById('tlbItemSave_TlbExpressions').set_imageUrl('save.png');
        TlbExpressions.get_items().getItemById('tlbItemCancel_TlbExpressions').set_enabled(true);
        TlbExpressions.get_items().getItemById('tlbItemCancel_TlbExpressions').set_imageUrl('cancel.png');


        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemRefresh_TlbPaging_GridExpressions_Expressions').set_enabled(false);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemRefresh_TlbPaging_GridExpressions_Expressions').set_imageUrl('refresh_silver.png');
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemLast_TlbPaging_GridExpressions_Expressions').set_enabled(false);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemLast_TlbPaging_GridExpressions_Expressions').set_imageUrl('last_silver.png');
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemNext_TlbPaging_GridExpressions_Expressions').set_enabled(false);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemNext_TlbPaging_GridExpressions_Expressions').set_imageUrl('Next_silver.png');
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemBefore_TlbPaging_GridExpressions_Expressions').set_enabled(false);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemBefore_TlbPaging_GridExpressions_Expressions').set_imageUrl('Before_silver.png');
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemFirst_TlbPaging_GridExpressions_Expressions').set_enabled(false);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemFirst_TlbPaging_GridExpressions_Expressions').set_imageUrl('first_silver.png');

        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbExpressionQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbExpressionQuickSearch').set_enabled(false);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbExpressionQuickSearch').set_imageUrl('search_silver.png');
        }
        document.getElementById('txtSearchTerm_Expressions').disabled = true;
        document.getElementById('txtScriptFaBegin_Expressions').disabled = false;
        document.getElementById('txtScriptFaEnd_Expressions').disabled = false;
        document.getElementById('txtScriptCSharpBegin_Expressions').disabled = false;
        document.getElementById('txtScriptCSharpEnd_Expressions').disabled = false;
        document.getElementById('txtOrder_Expressions').disabled = false;
        document.getElementById("ckbEditable_Expressions").disabled = false;
        document.getElementById("ckbAddable_Expressions").disabled = false;
        document.getElementById("ckbAddByParent_Expressions").disabled = false;

        if (State == 'Edit')
            NavigateExpression_Expressions(GridExpressions_Expressions.getSelectedItems()[0]);
        if (State == 'Delete')
            Expressions_onSave();
    }

    if (State == 'View') {
        if (TlbExpressions.get_items().getItemById('tlbItemNew_TlbExpressions') != null) {
            TlbExpressions.get_items().getItemById('tlbItemNew_TlbExpressions').set_enabled(true);
            TlbExpressions.get_items().getItemById('tlbItemNew_TlbExpressions').set_imageUrl('add.png');
        }
        if (TlbExpressions.get_items().getItemById('tlbItemEdit_TlbExpressions') != null) {
            TlbExpressions.get_items().getItemById('tlbItemEdit_TlbExpressions').set_enabled(true);
            TlbExpressions.get_items().getItemById('tlbItemEdit_TlbExpressions').set_imageUrl('edit.png');
        }
        if (TlbExpressions.get_items().getItemById('tlbItemDelete_TlbExpressions') != null) {
            TlbExpressions.get_items().getItemById('tlbItemDelete_TlbExpressions').set_enabled(true);
            TlbExpressions.get_items().getItemById('tlbItemDelete_TlbExpressions').set_imageUrl('remove.png');
        }
        TlbExpressions.get_items().getItemById('tlbItemSave_TlbExpressions').set_enabled(false);
        TlbExpressions.get_items().getItemById('tlbItemSave_TlbExpressions').set_imageUrl('save_silver.png');
        TlbExpressions.get_items().getItemById('tlbItemCancel_TlbExpressions').set_enabled(false);
        TlbExpressions.get_items().getItemById('tlbItemCancel_TlbExpressions').set_imageUrl('cancel_silver.png');


        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemRefresh_TlbPaging_GridExpressions_Expressions').set_enabled(true);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemRefresh_TlbPaging_GridExpressions_Expressions').set_imageUrl('refresh.png');
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemLast_TlbPaging_GridExpressions_Expressions').set_enabled(true);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemLast_TlbPaging_GridExpressions_Expressions').set_imageUrl("last.png");
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemNext_TlbPaging_GridExpressions_Expressions').set_enabled(true);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemNext_TlbPaging_GridExpressions_Expressions').set_imageUrl("Next.png");
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemBefore_TlbPaging_GridExpressions_Expressions').set_enabled(true);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemBefore_TlbPaging_GridExpressions_Expressions').set_imageUrl("Before.png");
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemFirst_TlbPaging_GridExpressions_Expressions').set_enabled(true);
        TlbPaging_GridExpressions_Expressions.get_items().getItemById('tlbItemFirst_TlbPaging_GridExpressions_Expressions').set_imageUrl("first.png");
        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbExpressionQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbExpressionQuickSearch').set_enabled(true);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbExpressionQuickSearch').set_imageUrl('search.png');
        }
        document.getElementById('txtSearchTerm_Expressions').disabled = false;
        document.getElementById('txtScriptFaBegin_Expressions').disabled = true;
        document.getElementById('txtScriptFaEnd_Expressions').disabled = true;
        document.getElementById('txtScriptCSharpBegin_Expressions').disabled = true;
        document.getElementById('txtScriptCSharpEnd_Expressions').disabled = true;
        document.getElementById('txtOrder_Expressions').disabled = true;
        document.getElementById("ckbEditable_Expressions").disabled = true;
        document.getElementById("ckbAddable_Expressions").disabled = true;
        document.getElementById("ckbAddByParent_Expressions").disabled = true;
    }
}

function ClearControls_Expressions() {
    document.getElementById('txtScriptFaBegin_Expressions').value = '';
    document.getElementById('txtScriptFaEnd_Expressions').value = '';
    document.getElementById('txtScriptCSharpBegin_Expressions').value = '';
    document.getElementById('txtScriptCSharpEnd_Expressions').value = '';
    document.getElementById('txtOrder_Expressions').value = '';
    document.getElementById("ckbEditable_Expressions").checked = false;
    document.getElementById("ckbAddable_Expressions").checked = false;
    document.getElementById("ckbAddByParent_Expressions").checked = false;
}

function RefreshExpression_Expressions() {
    ObjExpression_Expressions = new Object();

    ObjExpression_Expressions.ID = parseInt(selectedExpression.getMember('ID').get_text());
    ObjExpression_Expressions.Parent_ID = parseInt(selectedExpression.getMember('Parent_ID').get_text());
    ObjExpression_Expressions.ScriptBeginFa = selectedExpression.getMember('ScriptBeginFa').get_text();
    ObjExpression_Expressions.ScriptBeginEn = selectedExpression.getMember('ScriptBeginEn').get_text();
    ObjExpression_Expressions.ScriptEndEn = selectedExpression.getMember('ScriptEndEn').get_text();

    ObjExpression_Expressions.AddOnParentCreation = Boolean(selectedExpression.getMember('AddOnParentCreation').get_text());
    ObjExpression_Expressions.CanAddToFinal = Boolean(selectedExpression.getMember('CanAddToFinal').get_text());
    ObjExpression_Expressions.CanEditInFinal = Boolean(selectedExpression.getMember('CanEditInFinal').get_text());
    ObjExpression_Expressions.Visible = selectedExpression.getMember('Visible').get_text();

    ObjExpression_Expressions.SortOrder = parseInt(selectedExpression.getMember('SortOrder').get_text());
}

function NavigateExpression_Expressions(selectedExpression) {
    if (selectedExpression != undefined) {

        ObjExpression_Expressions.ID = parseInt(selectedExpression.getMember('ID').get_text());
        ObjExpression_Expressions.Parent_ID = parseInt(selectedExpression.getMember('Parent_ID').get_text());

        ObjExpression_Expressions.ScriptBeginFa = selectedExpression.getMember('ScriptBeginFa').get_text();
        ObjExpression_Expressions.ScriptEndFa = selectedExpression.getMember('ScriptEndFa').get_text();
        ObjExpression_Expressions.ScriptBeginEn = selectedExpression.getMember('ScriptBeginEn').get_text();
        ObjExpression_Expressions.ScriptEndEn = selectedExpression.getMember('ScriptEndEn').get_text();

        ObjExpression_Expressions.SortOrder = parseInt(selectedExpression.getMember('SortOrder').get_text());

        ObjExpression_Expressions.AddOnParentCreation = (selectedExpression.getMember('AddOnParentCreation').get_text() == "true");
        ObjExpression_Expressions.CanAddToFinal = (selectedExpression.getMember('CanAddToFinal').get_text() == "true");
        ObjExpression_Expressions.CanEditInFinal = (selectedExpression.getMember('CanEditInFinal').get_text() == "true");
        ObjExpression_Expressions.Visible = (selectedExpression.getMember('Visible').get_text() == "true");

        document.getElementById('txtScriptFaBegin_Expressions').value = ObjExpression_Expressions.ScriptBeginFa;
        document.getElementById('txtScriptFaEnd_Expressions').value = ObjExpression_Expressions.ScriptEndFa;
        document.getElementById('txtScriptCSharpBegin_Expressions').value = ObjExpression_Expressions.ScriptBeginEn;
        document.getElementById('txtScriptCSharpEnd_Expressions').value = ObjExpression_Expressions.ScriptEndEn;

        document.getElementById('txtOrder_Expressions').value = ObjExpression_Expressions.SortOrder;

        document.getElementById("ckbEditable_Expressions").checked = ObjExpression_Expressions.CanEditInFinal;
        document.getElementById("ckbAddable_Expressions").checked = ObjExpression_Expressions.CanAddToFinal;
        document.getElementById("ckbAddByParent_Expressions").checked = ObjExpression_Expressions.AddOnParentCreation;
    }
}

function Expressions_onSave() {
    if (CurrentPageState_Expressions != 'Delete')
        UpdateExpression_Expressions();
    else
        ShowDialogConfirm('Delete');
}

function Expressions_Cancel() {
    ChangePageState_Expressions('View');
    ClearControls_Expressions();
}

function CloseDialogExpressionsManagement() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogExpressionsManagement_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogExpressionsManagement').Close();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Expressions = confirmState;
    if (ConfirmState_Expressions == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Expressions').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Expressions').value;
    DialogConfirm.Show();
}

function ShowConnectionError_Expressions() {
    var error = document.getElementById('hfErrorType_Expressions').value;
    var errorBody = document.getElementById('hfConnectionError_Expressions').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Expressions('View');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Expressions) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateExpression_Expressions();
            break;
        case 'Exit':
            CloseDialogExpressionsManagement();
            break;
    }
}

function CloseDialogConceptRuleEditor() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogExpressionsManagement_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogExpressionsManagement').Close();
}
function UpdateExpression_Expressions() {

    var ObjExpressions = new Object();
    ObjExpressions.ID = 0;
    ObjExpressions.ScriptBeginFa = '';
    ObjExpressions.ScriptEndFa = '';
    ObjExpressions.ScriptBeginEn = '';
    ObjExpressions.ScriptEndEn = '';
    ObjExpressions.AddOnParentCreation = false;
    ObjExpressions.CanAddToFinal = false;
    ObjExpressions.CanEditInFinal = false;
    ObjExpressions.SortOrder = 0;
    ObjExpressions.Visible = true;
    ObjExpressions.Parent_ID = "";

    var SelectedItems_GridExpressionss = GridExpressions_Expressions.getSelectedItems();
    if (SelectedItems_GridExpressionss.length > 0)
        ObjExpressions.ID = SelectedItems_GridExpressionss[0].getMember("ID").get_text();
    else ObjExpressions.ID = 0;

    if (CurrentPageState_Expressions != 'Delete') {

        ObjExpressions.ScriptBeginFa = document.getElementById('txtScriptFaBegin_Expressions').value;
        ObjExpressions.ScriptEndFa = document.getElementById('txtScriptFaEnd_Expressions').value;
        ObjExpressions.ScriptBeginEn = document.getElementById('txtScriptCSharpBegin_Expressions').value;
        ObjExpressions.ScriptEndEn = document.getElementById('txtScriptCSharpEnd_Expressions').value;

        ObjExpressions.SortOrder = document.getElementById('txtOrder_Expressions').value;


        ObjExpressions.AddOnParentCreation = document.getElementById("ckbAddByParent_Expressions").checked;
        ObjExpressions.CanAddToFinal = document.getElementById("ckbAddable_Expressions").checked;
        ObjExpressions.CanEditInFinal = document.getElementById("ckbEditable_Expressions").checked;

        if (ObjExpression_Expressions.Visible != null && ObjExpression_Expressions.Visible != undefined)
            ObjExpressions.Visible = ObjExpression_Expressions.Visible;
        else ObjExpressions.Visible = true;

        if (
            !isNaN(ObjExpression_Expressions.Parent_ID) &&
            ObjExpression_Expressions.Parent_ID != null &&
            ObjExpression_Expressions.Parent_ID != undefined)
            ObjExpressions.Parent_ID = ObjExpression_Expressions.Parent_ID;
        else ObjExpressions.Parent_ID = "";
    }

    UpdateExpression_ExpressionsPage(
        CharToKeyCode_Expressions(ObjExpressions.ID.toString()),
        CharToKeyCode_Expressions(ObjExpressions.Parent_ID.toString()),
        CharToKeyCode_Expressions(ObjExpressions.ScriptBeginFa),
        CharToKeyCode_Expressions(ObjExpressions.ScriptEndFa),
        CharToKeyCode_Expressions(ObjExpressions.ScriptBeginEn),
        CharToKeyCode_Expressions(ObjExpressions.ScriptEndEn),
        CharToKeyCode_Expressions(ObjExpressions.AddOnParentCreation.toString()),
        CharToKeyCode_Expressions(ObjExpressions.CanAddToFinal.toString()),
        CharToKeyCode_Expressions(ObjExpressions.CanEditInFinal.toString()),
        CharToKeyCode_Expressions(ObjExpressions.Visible.toString()),
        CharToKeyCode_Expressions(ObjExpressions.SortOrder.toString()),
        CharToKeyCode_Expressions(CurrentPageState_Expressions)
    );
}

function UpdateExpression_ExpressionsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Posts').value;
            Response[1] = document.getElementById('hfConnectionError_Posts').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Fill_GridExpressions_Expressions(CurrentPageIndex_GridExpressions_Expressions);
            ClearControls_Expressions();
            ChangePageState_Expressions('View');
            CallBack_trvExpressions_Expressions.callback();
        }
        else {
            if (CurrentPageState_Expressions == 'Delete')
                ChangePageState_Expressions('View');
        }
    }
}

function CacheTreeViewsSize_Expressions() {
    CurrentPageTreeViewsObj.trvExpressions_Expressions = document.getElementById('trvExpressions_Expressions').clientWidth + 'px';
}

function Resize_trvExpressions_Expressions() {
    document.getElementById('trvExpressions_Expressions').style.width = CurrentPageTreeViewsObj.trvExpressions_Expressions;
}

function ChangeDirection_trvExpressions_Expressions() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvExpressions_Expressions').style.direction = 'ltr';
}

function trvExpressions_Expressions_onNodeExpand(sender, e) {
    Resize_trvExpressions_Expressions();
    ChangeDirection_trvExpressions_Expressions();
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

