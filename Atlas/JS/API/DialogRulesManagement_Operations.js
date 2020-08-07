
var CurrentPageIndex_GridRules_Rules = 0;
var RuleSearchBoxIsShownRule_Rules = false;
var LoadStateRule_Rules = 'Normal';
var CombosCallBackCurrentStateRule_Rules = new Object();
var CurrentPageStateRule_Rules = 'View';
var SelectedRules_Rule = new Object();
var ConfirmStateRule_Rules = null;
var CurrentPageStateRuleParameter_Rules = 'View';
var SelectedRuleParameter_Rules = new Object();
var ConfirmStateRuleParameter_Rules = null;
var CombosCallBackCurrentStateRuleParameter_Rules = new Object();
var EnumRuleParameterTypeRuleParameter_Rules = null;
var SelectedItemGrigRulesManagement = new Object();
function ChangePageStateRule_Rules(State) {
    CurrentPageStateRule_Rules = State;
    SetActionModeRule_Rules(State);

    if (State == 'Add' || State == 'Edit' || State == 'Delete') {
        if (TlbRules.get_items().getItemById('tlbItemNew_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_enabled(false);
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_imageUrl('add_silver.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemEdit_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_enabled(false);
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_imageUrl('edit_silver.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemDelete_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_enabled(false);
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_imageUrl('remove_silver.png');
        }
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_enabled(true);
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_imageUrl('save.png');
        TlbRules.get_items().getItemById('tlbItemCancel_TlbRules').set_enabled(true);
        TlbRules.get_items().getItemById('tlbItemCancel_TlbRules').set_imageUrl('cancel.png');
       
        //if (SelectedRules_Rule.hasOwnProperty("ID")) {
        //    if (SelectedRules_Rule.ID != 0 || SelectedRules_Rule.Name != null) {
        //        TlbRules.get_items().getItemById('tlbItemDefine_TlbRules').set_enabled(true);
        //        TlbRules.get_items().getItemById('tlbItemDefine_TlbRules').set_imageUrl('view_detailed.png');
        //    }
        //}
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_imageUrl('refresh_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_imageUrl('last_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_imageUrl('Next_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_imageUrl('Before_silver.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_enabled(false);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_imageUrl('first_silver.png');
        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_enabled(false);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_imageUrl('search_silver.png');
        } 
        document.getElementById('txtSearchTerm_Rules').disabled = true;
        document.getElementById('txtRuleName_Rules').disabled = false;
        document.getElementById('txtRulePriority').disabled = false;

        cmbRuleType_Rules.enable();
        cmbRuleScope_Rules.enable();

        if (State == 'Edit')
            NavigateRule_Rules(GridRules_Rules.getSelectedItems()[0]);
        if (State == 'Delete')
            onSaveRule_Rules();
    }

    if (State == 'View') {
        if (TlbRules.get_items().getItemById('tlbItemNew_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_enabled(true);
            TlbRules.get_items().getItemById('tlbItemNew_TlbRules').set_imageUrl('add.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemEdit_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_enabled(true);
            TlbRules.get_items().getItemById('tlbItemEdit_TlbRules').set_imageUrl('edit.png');
        }
        if (TlbRules.get_items().getItemById('tlbItemDelete_TlbRules') != null) {
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_enabled(true);
            TlbRules.get_items().getItemById('tlbItemDelete_TlbRules').set_imageUrl('remove.png');
        }
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_enabled(false);
        TlbRules.get_items().getItemById('tlbItemSave_TlbRules').set_imageUrl('save_silver.png');
        TlbRules.get_items().getItemById('tlbItemCancel_TlbRules').set_enabled(false);
        TlbRules.get_items().getItemById('tlbItemCancel_TlbRules').set_imageUrl('cancel_silver.png');
        TlbRules.get_items().getItemById('tlbItemDefine_TlbRules').set_enabled(false);
        TlbRules.get_items().getItemById('tlbItemDefine_TlbRules').set_imageUrl('view_detailed_silver.png');

        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemRefresh_TlbPaging_GridRules_Rules').set_imageUrl('refresh.png');
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemLast_TlbPaging_GridRules_Rules').set_imageUrl("last.png");
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemNext_TlbPaging_GridRules_Rules').set_imageUrl("Next.png");
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemBefore_TlbPaging_GridRules_Rules').set_imageUrl("Before.png");
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_enabled(true);
        TlbPaging_GridRules_Rules.get_items().getItemById('tlbItemFirst_TlbPaging_GridRules_Rules').set_imageUrl("first.png");
        if (tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch') != null) {
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_enabled(true);
            tlbItemQuickSearch.get_items().getItemById('tlbItemSearch_TlbRuleQuickSearch').set_imageUrl('search.png');
        }

        document.getElementById('txtSearchTerm_Rules').disabled = false;
        document.getElementById('txtRuleName_Rules').disabled = true;
        //document.getElementById('txtRuleCode_Rules').disabled = true;
        document.getElementById('txtRulePriority').disabled = true;
        cmbRuleType_Rules.disable();
        cmbRuleScope_Rules.disable();
    }

}

function GetBoxesHeaders_Rules() {
    parent.document.getElementById('Title_DialogRulesManagement').innerHTML = document.getElementById('hfTitle_DialogRulesManagement').value;
    document.getElementById('header_RulesBox_Rules').innerHTML = document.getElementById('hfHeaderRule_Rules').value;
    //document.getElementById('header_RuleParameters_Rule').innerHTML = document.getElementById('hfHeaderRuleParameter_Rules').value;
    document.getElementById('header_RuleDetails_Rules').innerHTML = document.getElementById('hfheader_RuleDetails_Rules').value;
    //document.getElementById('header_RuleParametersDetails_Rules').innerHTML = document.getElementById('hfheader_RuleParametersDetails_Rules').value;
}

function SetPageIndex_GridRules_Rules(pageIndex) {
    CurrentPageIndex_GridRules_Rules = pageIndex;
    Fill_GridRules_Rules(pageIndex);
}

function Fill_GridRules_Rules(pageIndex) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridRules_Rules').value);
    var pageSize = parseInt(document.getElementById('hfRulesPageSize_Rules').value);
    var searchKey = 'NotSpecified';
    var searchTerm = '';
    if (RuleSearchBoxIsShownRule_Rules) {
        searchTerm = document.getElementById('txtSearchTerm_Rules').value;
    }
    CallBack_GridRules_Rules.callback(CharToKeyCode_Rules(LoadStateRule_Rules), CharToKeyCode_Rules(pageSize.toString()), CharToKeyCode_Rules(pageIndex.toString()), CharToKeyCode_Rules(searchKey), CharToKeyCode_Rules(searchTerm));
}

function tlbItemSearch_TlbRuleQuickSearch_onClick(sender, args) {
    RuleSearchBoxIsShownRule_Rules = true;
    LoadStateRule_Rules = 'Search';
    SetPageIndex_GridRules_Rules(0);
}

function GridRules_Rules_onLoad(sender, args) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
}

function GridRules_Rules_onItemSelect(sender, args) {
    if (CurrentPageStateRule_Rules != 'Add')
        NavigateRule_Rules(args.get_item());

}

function CallBack_GridRules_Rules_OnCallbackComplete(sender, args) {
    var error = document.getElementById('ErrorHiddenField_Rules').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRules_Rules();
    }
}

function CallBack_GridRules_Rules_onCallbackError(sender, args) {
    ShowConnectionError_Rules();
}

function tlbItemRefresh_TlbPaging_GridRules_Rules_onClick(sender, args) {
    ChangeLoadState_GridRules_Rules('Normal');
}

function ChangeLoadState_GridRules_Rules(state) {
    LoadStateRule_Rules = state;
    SetPageIndex_GridRules_Rules(0);
}

function tlbItemFirst_TlbPaging_GridRules_Rules_onClick(sender, args) {
    SetPageIndex_GridRules_Rules(0);
}

function tlbItemBefore_TlbPaging_GridRules_Rules_onClick(sender, args) {
    if (CurrentPageIndex_GridRules_Rules != 0) {
        CurrentPageIndex_GridRules_Rules = CurrentPageIndex_GridRules_Rules - 1;
        SetPageIndex_GridRules_Rules(CurrentPageIndex_GridRules_Rules);
    }
}

function tlbItemNext_TlbPaging_GridRules_Rules_onClick(sender, args) {
    if (CurrentPageIndex_GridRules_Rules < parseInt(document.getElementById('hfRulesPageCount_Rules').value) - 1) {
        CurrentPageIndex_GridRules_Rules = CurrentPageIndex_GridRules_Rules + 1;
        SetPageIndex_GridRules_Rules(CurrentPageIndex_GridRules_Rules);
    }
}

function tlbItemLast_TlbPaging_GridRules_Rules_onClick(sender, args) {
    SetPageIndex_GridRules_Rules(parseInt(document.getElementById('hfRulesPageCount_Rules').value) - 1);
}

function cmbRuleType_Rules_onExpand(sender, args) {
    if (cmbRuleType_Rules.get_itemCount() == 0 && CombosCallBackCurrentStateRule_Rules.IsExpandOccured_cmbRuleType_Rules == undefined) {
        CombosCallBackCurrentStateRule_Rules.cmbRuleType_Rules_Text = document.getElementById('cmbRuleType_Rules_Input').value;
        CombosCallBackCurrentStateRule_Rules.IsExpandOccured_cmbRuleType_Rules = true;
        Fill_cmbRuleType_Rules();
    } else {
        document.getElementById('cmbRuleType_Rules_Input').value = CombosCallBackCurrentStateRule_Rules.cmbRuleType_Rules_Text;
    }
}
function cmbRuleScope_Rules_onExpand(sender, args) {
    //if (cmbRuleScope_Rules.get_itemCount() == 0 && CombosCallBackCurrentStateRule_Rules.IsExpandOccured_cmbRuleScope_Rules == undefined) {
    //    CombosCallBackCurrentStateRule_Rules.IsExpandOccured_cmbRuleScope_Rules = true;
    //    FillRuleScope_RulesManagement();

    //}
}
function Fill_cmbRuleType_Rules() {
    ComboBox_onBeforeLoadData('cmbRuleType_Rules');
    CallBack_cmbRuleType_Rules.callback();
}

function Fill_cmbRuleScope_Rules() {
    //ComboBox_onBeforeLoadData('cmbRuleScope_Rules');
    //CallBack_cmbRuleScope_Rules.callback();
}

function cmbRuleType_Rules_onBeforeCallback(sender, args) {
    cmbRuleType_Rules.dispose();
}

function cmbRuleScope_Rules_onBeforeCallback(sender, args)
{
    //cmbRuleScope_Rules.dispose();
}
function cmbRuleType_Rules_onCallbackComplete(sender, args) {
    var error = document.getElementById('ErrorHiddenField_TypeFields').value;
    if (error == "") {
        document.getElementById('cmbRuleType_Rules_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompletedRule_Rules())
            $('#cmbRuleType_Rules_DropImage').mousedown();
        cmbRuleType_Rules.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleType_Rules_DropDown').style.display = 'none';
    }
}
function FillRuleScope_RulesManagement()
{
    var RuleScopeValueJson = $("#hfRuleTypeEnum").val();
    var RuleScopValue = JSON.parse(RuleScopeValueJson);

}

function cmbRuleScope_Rules_onCallbackComplete(sender, args)
{
    //var error = document.getElementById('ErrorHiddenField_ScopeFields').value;
    //if (error == "") {
    //    document.getElementById('cmbRuleScope_Rules_DropDown').style.display = 'none';
    //    if (CheckNavigator_onCmbCallBackCompletedRule_Rules())
    //        $('#cmbRuleScope_Rules_DropImage').mousedown();
    //    cmbRuleScope_Rules.expand();
    //}
    //else {
    //    var errorParts = eval('(' + error + ')');
    //    showDialog(errorParts[0], errorParts[1], errorParts[2]);
    //    document.getElementById('cmbRuleScope_Rules_DropDown').style.display = 'none';
    //}
}
function cmbRuleType_Rules_onCallbackError(sender, args) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
    ShowConnectionError_Rules();
}

function cmbRuleScope_Rules_onCallbackError (sender, args)
{
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
    ShowConnectionError_Rules();
}
function ClearControlsRule_Rules() {

    SelectedRules_Rule.Script = '';
    SelectedRules_Rule.CSharpCode = '';

    SelectedRules_Rule.CustomCategoryCode = '';
    //SelectedRules_Rule.JsonObject = '';
    SelectedRules_Rule.ParameterObject = '';
    SelectedRules_Rule.VariableObject = '';
    SelectedRules_Rule.RuleObject = '';
    SelectedRules_Rule.RuleStateObject = '';
    SelectedRules_Rule.Order = '';
    SelectedRules_Rule.RuleTemplateID = '';
    SelectedRules_Rule.OperationalAreaId = '';
    //document.getElementById('txtRuleCode_Rules').value = '';
    document.getElementById('txtRuleName_Rules').value = '';
    document.getElementById('txtRulePriority').value = '';


    document.getElementById('cmbRuleType_Rules_Input').value = '';
    if (cmbRuleType_Rules.getSelectedItem() != undefined)
        cmbRuleType_Rules.unSelect();

    document.getElementById('cmbRuleScope_Rules_Input').value = '';
    if (cmbRuleScope_Rules.getSelectedItem() != undefined)
        cmbRuleScope_Rules.unSelect();
}

function NavigateRule_Rules(selectedRule) {
    if (selectedRule == undefined) {
        return;
    }
 
    RefreshRule_Rules();
   
    GridRuleParametersCallCallBack_Rules(selectedRule.getMember('ID').get_text());
    FillSelectedRule_Rules(
        selectedRule.getMember('ID').get_text(),
        selectedRule.getMember('IdentifierCode').get_text(),
        selectedRule.getMember('Name').get_text(),
        selectedRule.getMember('TypeId').get_text(),
        selectedRule.getMember('Type').get_text(),
        selectedRule.getMember('UserDefined').get_text(),
        selectedRule.getMember('Script').get_text(),
        selectedRule.getMember('CSharpCode').get_text(),
        selectedRule.getMember('JsonObject').get_text(),
        selectedRule.getMember('CustomCategoryCode').get_text(),
        selectedRule.getMember('Order').get_text(),
        selectedRule.getMember('RuleStateObject').get_text(),
        selectedRule.getMember('RuleParametersObject').get_text(),
        selectedRule.getMember('RuleVariablesObject').get_text(),
        selectedRule.getMember('DesignedRuleID').get_text(),
        selectedRule.getMember('operationalArea').get_text(),
        selectedRule.getMember('operationalAreaName').get_text()

    );
    if (SelectedRules_Rule.ID != 0 || SelectedRules_Rule.Name != null) {
        TlbRules.get_items().getItemById('tlbItemDefine_TlbRules').set_enabled(true);
        TlbRules.get_items().getItemById('tlbItemDefine_TlbRules').set_imageUrl('view_detailed.png');
    }
    FillSelectedFieldsRule_Rules();
}

function onSaveRule_Rules() {
    if (CurrentPageStateRule_Rules != 'Delete')
        UpdateRule_Rules();
    else
        ShowDialogConfirm('Delete', 'Rule');
}

function OnCancelRule_Rules() {
    ChangePageStateRule_Rules('View');
    ClearControlsRule_Rules();
}

function RefreshRule_Rules() {
    FillSelectedRule_Rules(0 , 0, null, 0, null, null, null, null, null, 0, 0, null, null, null, 0, 0, null);
}

function FillSelectedRule_Rules(id, identifierCode, name, typeId, type, userDefined, script, cSharpCode, jsonObject, customCategoryCode, Order, RuleStateObject, ParameterObject, VariableObject, DesignedRuleID, operationalareaId, operationalareaName) {
    SelectedRules_Rule = new Object();
    SelectedRules_Rule.ID = id;
    SelectedRules_Rule.IdentifierCode = identifierCode;
    SelectedRules_Rule.Name = name;
    SelectedRules_Rule.Order = Order;
    SelectedRules_Rule.TypeId = typeId;
    SelectedRules_Rule.Type = type;
    SelectedRules_Rule.RuleStateObject = RuleStateObject;
    SelectedRules_Rule.UserDefined = userDefined;
    SelectedRules_Rule.ParameterObject = ParameterObject;
    SelectedRules_Rule.VariableObject = VariableObject;
    SelectedRules_Rule.DesignedRuleID = DesignedRuleID;
    SelectedRules_Rule.OperationalAreaId = operationalareaId;
    SelectedRules_Rule.OperationalAreaName = operationalareaName;
    //SelectedRules_Rule.OperationalArea = operationalArea;
    if (jsonObject != undefined && jsonObject != "") {
        SelectedRules_Rule.Script = script;
        SelectedRules_Rule.CSharpCode = cSharpCode;

        SelectedRules_Rule.CustomCategoryCode = customCategoryCode;
        SelectedRules_Rule.JsonObject = JSON.parse(jsonObject);
    }
}

function FillSelectedFieldsRule_Rules() {
    //document.getElementById('txtRuleCode_Rules').value = SelectedRules_Rule.IdentifierCode;
    document.getElementById('txtRuleName_Rules').value = SelectedRules_Rule.Name;
    document.getElementById('cmbRuleType_Rules_Input').value = SelectedRules_Rule.Type;
    document.getElementById('txtRulePriority').value = SelectedRules_Rule.Order;
    document.getElementById('cmbRuleScope_Rules_Input').value = SelectedRules_Rule.OperationalAreaName;
}

function SetActionModeRule_Rules(state) {
    document.getElementById('ActionMode_Rules').innerHTML = document.getElementById("hfState" + state + "_Rules").value;
}

function CharToKeyCode_Rules(str) {
    if (str == null) return '';

    str = str.toString();

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

function ClearListRule_Rules() {
    if (CurrentPageStateRule_Rules != 'Edit') {
        RefreshRule_Rules();
    }
}

function CheckNavigator_onCmbCallBackCompletedRule_Rules() {
    if (navigator.userAgent.indexOf('Safari') != 1 || navigator.userAgent.indexOf('Chrome') != 1)
        return true;
    return false;
}

function CloseDialogRulesManagement() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogRulesManagement_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogRulesManagement').Close();
}

function ShowDialogConfirm(confirmState) {

    //if (objectType == 'Rule') {
        ConfirmStateRule_Rules = confirmState;
        if (CurrentPageStateRule_Rules == 'Delete')
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessageRule_Rules').value;
        else
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Rules').value;
    //}
    //if (objectType == 'RuleParameter') {
    //    ConfirmStateRuleParameter_Rules = confirmState;
    //    if (ConfirmStateRuleParameter_Rules == 'Delete')
    //        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessageRuleParam_Rules').value;
    //    else
    //        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Rules').value;
    //}

    //DialogConfirm.set_value(objectType);
    DialogConfirm.Show();
}

function ShowConnectionError_Rules() {
    var error = document.getElementById('hfErrorType_Rules').value;
    var errorBody = document.getElementById('hfConnectionError_Rules').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemNew_TlbRules_onClick() {
    ClearControlsRule_Rules();
    ChangePageStateRule_Rules('Add');
}

function tlbItemEdit_TlbRules_onClick(sender, args) {
    ChangePageStateRule_Rules('Edit');

}

function tlbItemDelete_TlbRules_onClick(sender, args) {
    ChangePageStateRule_Rules('Delete');
}

function tlbItemSave_TlbRules_onClick(sender, args) {
    onSaveRule_Rules();
}

function tlbItemCancel_TlbRules_onClick(sender, args) {
    OnCancelRule_Rules();
}

function tlbItemDefine_TlbRules_onClick(sender, args) {
    ShowDialogConceptRuleEditor();
}

function ShowDialogConceptRuleEditor() {
    var RuleJsonObjectEditor = new Object();
    RuleJsonObjectEditor.ID = SelectedRules_Rule.ID;
    RuleJsonObjectEditor.DetailsJsonObject = SelectedRules_Rule.JsonObject;
    RuleJsonObjectEditor.ScriptEn = SelectedRules_Rule.CSharpCode;
    RuleJsonObjectEditor.ScriptFa = SelectedRules_Rule.Script;
    RuleJsonObjectEditor.IdentifierCode = SelectedRules_Rule.IdentifierCode;
    RuleJsonObjectEditor.RuleName = SelectedRules_Rule.Name;
    RuleJsonObjectEditor.RuleStateJsonObject = SelectedRules_Rule.RuleStateObject;
    RuleJsonObjectEditor.ParameterObject = SelectedRules_Rule.ParameterObject;
    RuleJsonObjectEditor.VariablrObject = SelectedRules_Rule.VariableObject;
    RuleJsonObjectEditor.PageState = CurrentPageStateRule_Rules;
    RuleJsonObjectEditor.DesignedRuleID = SelectedRules_Rule.DesignedRuleID;
    RuleJsonObjectEditor.OperationalAreaId = SelectedRules_Rule.OperationalAreaId;
    RuleJsonObjectEditor.CallerDialog = "RuleManagement";
    //parent.DialogConceptRuleEditor.set_value(RuleJsonObjectEditor);
    //parent.DialogConceptRuleEditor.Show();
    parent.DialogRuleGenerator.set_value(RuleJsonObjectEditor);
    parent.DialogRuleGenerator.show();
}

function tlbItemFormReconstruction_TlbRule_onClick(sender, args) {
    CloseDialogRulesManagement();
    parent.DialogRulesManagement.Show();
}
function tlbItemExit_TlbRules_onClick(sender, e) {
    ShowDialogConfirm('Exit');
}
function tlbRuleExit_TlbRules_onClick(sender, e) {
    ShowDialogConfirm('Exit');
}

function Apply_Object_CSharp_Script_FromRuleRuleEditor(recivedObject) {

    SelectedRules_Rule.Script = recivedObject.ScriptFa;
    SelectedRules_Rule.CSharpCode = recivedObject.ScriptEn;
    //SelectedRules_Rule.JsonObject = recivedObject.DetailsJsonObject;
    SelectedRules_Rule.ParameterObject = recivedObject.ParameterObjects;
    SelectedRules_Rule.VariableObject = recivedObject.VariableObjects;
    SelectedRules_Rule.RuleStateObject = recivedObject.RuleStateObject;
    SelectedRules_Rule.Order = recivedObject.Order;
    SelectedRules_Rule.RuleObject = recivedObject.RuleObject;
    SelectedRules_Rule.OperationalAreaId = recivedObject.OperationalAreaId;
    return SelectedRules_Rule;
}
function Apply_Object_RuleStateObjectArrey_FromRuleGenerator()
{
    CloseDialogRulesManagement();
    parent.DialogRulesManagement.Show();
}
function UpdateRule_Rules() {
     
    ObjRule_RuleTemplate = new Object();
    ObjRule_DesignedRules = new Object();

    ObjRule_RuleTemplate.ID = 0;
    ObjRule_RuleTemplate.IdentifierCode = "0";
    ObjRule_RuleTemplate.Name = "";
    ObjRule_RuleTemplate.CustomCategoryCode = null;
    ObjRule_RuleTemplate.TypeId = 0;
    ObjRule_RuleTemplate.Type = null;
    ObjRule_RuleTemplate.UserDefined = true;
    //ObjRule_RuleTemplate.Script = "";
    //ObjRule_RuleTemplate.CSharpCode = "";
    //ObjRule_RuleTemplate.JsonObject = "";
    ObjRule_RuleTemplate.CustomCategoryCode = 0;
    ObjRule_RuleTemplate.Order = 0;
    ObjRule_RuleTemplate.OperationalAreaId = 1;
    ObjRule_RuleTemplate.OperationalAreaName = null;


    var SelectedItems_GridRules_Rules = GridRules_Rules.getSelectedItems();
    if (SelectedItems_GridRules_Rules.length > 0)
        ObjRule_RuleTemplate.ID = SelectedItems_GridRules_Rules[0].getMember("ID").get_text();
    else ObjRule_RuleTemplate.ID = 0;

    if (CurrentPageStateRule_Rules != 'Delete') {

        //if (document.getElementById('txtRuleCode_Rules').value != undefined && document.getElementById('txtRuleCode_Rules').value != null && document.getElementById('txtRuleCode_Rules').value != '' && document.getElementById('txtRuleCode_Rules').value != ' ')
        //    ObjRule_RuleTemplate.IdentifierCode = document.getElementById('txtRuleCode_Rules').value;
        ObjRule_RuleTemplate.Name = document.getElementById('txtRuleName_Rules').value;
        //ObjRule_RuleTemplate.Order = document.getElementById('txtRulePriority').value;
        var RuleOrder = document.getElementById('txtRulePriority').value;
      
        if (RuleOrder == "") {
            ObjRule_RuleTemplate.Order = "20000";
        }
        else
        {
            ObjRule_RuleTemplate.Order = RuleOrder;

        }
        RuleOrder = parseInt(RuleOrder);
        if (RuleOrder < 5000)
        {
            var errorBody = "مقدار الویت قوانین طراحی شده باید بیشتر از 5000 باشد";
            
            var error = "خطا";
            showDialog(error, errorBody, 'error');
            return;
        }

        if (SelectedRules_Rule.CustomCategoryCode != undefined && SelectedRules_Rule.CustomCategoryCode != "") {
            ObjRule_RuleTemplate.CustomCategoryCode = SelectedRules_Rule.CustomCategoryCode;
        } else {
            ObjRule_RuleTemplate.CustomCategoryCode = SelectedRules_Rule.IdentifierCode;
        }


        if (cmbRuleType_Rules.getSelectedItem() != undefined) {
            ObjRule_RuleTemplate.TypeId = parseInt(cmbRuleType_Rules.getSelectedItem().Value);
        } else if (SelectedRules_Rule.TypeId != undefined) {
            ObjRule_RuleTemplate.TypeId = SelectedRules_Rule.TypeId;
        }

        if (cmbRuleScope_Rules.getSelectedItem() != undefined) {
            ObjRule_RuleTemplate.OperationalAreaId = parseInt(cmbRuleScope_Rules.getSelectedItem().Value);
        }
        else if (SelectedRules_Rule.OperationalAreaId != "")
        {
            ObjRule_RuleTemplate.OperationalAreaId = SelectedRules_Rule.OperationalAreaId;
        }
        if (SelectedRules_Rule.UserDefined != undefined)
            ObjRule_RuleTemplate.UserDefined = SelectedRules_Rule.UserDefined;
        else SelectedRules_Rule.UserDefined = true;

        if (SelectedRules_Rule.Script != undefined)
            ObjRule_RuleTemplate.Script = SelectedRules_Rule.Script;
        else ObjRule_RuleTemplate.Script = '';

        if (SelectedRules_Rule.CSharpCode != undefined)
            ObjRule_RuleTemplate.CSharpCode = SelectedRules_Rule.CSharpCode;
        else ObjRule_RuleTemplate.CSharpCode = '';

        ObjRule_RuleTemplate.JsonObject = JSON.stringify(SelectedRules_Rule.JsonObject);

    }

  
    UpdateRule_RulePage_Call(ObjRule_RuleTemplate);
}

function UpdateRule_RulePage_Call(objRuleRules) {

    UpdateRule_RulesPage(
        CharToKeyCode_Rules(objRuleRules.ID),
        CharToKeyCode_Rules(objRuleRules.IdentifierCode),
        CharToKeyCode_Rules(objRuleRules.Name),
        CharToKeyCode_Rules(objRuleRules.CustomCategoryCode),
        CharToKeyCode_Rules(objRuleRules.TypeId),
        CharToKeyCode_Rules(objRuleRules.UserDefined),
        CharToKeyCode_Rules(objRuleRules.Order),
        CharToKeyCode_Rules(objRuleRules.OperationalAreaId),
        CharToKeyCode_Rules(CurrentPageStateRule_Rules)

    );
}
function UpdateRule_RulesPage_onCallBack(Response) {

    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Rules').value;
            Response[1] = document.getElementById('hfConnectionError_Rules').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearListRule_Rules();
            RefreshRule_Rules();
            ClearControlsRule_Rules();
            ChangePageStateRule_Rules('View');
            SetPageIndex_GridRules_Rules(0);
        }
        else {
            if (CurrentPageStateRule_Rules == 'Delete')
                ChangePageStateRule_Rules('View');
        }
    }
}

function CallBackSaveRules_Rules_Call(objRuleRules) {
    
    CallBackSaveRules_Rules.callback(
        CharToKeyCode_Rules(objRuleRules.ID),
        CharToKeyCode_Rules(objRuleRules.IdentifierCode),
        CharToKeyCode_Rules(objRuleRules.Name),
        CharToKeyCode_Rules(objRuleRules.CustomCategoryCode),
        CharToKeyCode_Rules(objRuleRules.TypeId),
        CharToKeyCode_Rules(objRuleRules.UserDefined),
        CharToKeyCode_Rules(objRuleRules.Script),
        CharToKeyCode_Rules(objRuleRules.CSharpCode),
        CharToKeyCode_Rules(objRuleRules.Order),
        CharToKeyCode_Rules(objRuleRules.OperationalAreaId),
        CharToKeyCode_Rules(CurrentPageStateRule_Rules)
    );
}
function CallBackSaveRules_Rules_onCallbackComplete(sender, e) {

    var strCallBackComplete = document.getElementById('hfCallBackDataSaveRules_Rules').value;

    if (strCallBackComplete != "") {
        var RetMessage = JSON.parse(strCallBackComplete);

        if (RetMessage != null && RetMessage.length > 0) {
            if (RetMessage[1] == "ConnectionError") {
                RetMessage[0] = document.getElementById('hfErrorType_Rules').value;
                RetMessage[1] = document.getElementById('hfConnectionError_Rules').value;
            }
            showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
            if (RetMessage[2] == 'success') {
                Fill_GridRules_Rules(CurrentPageIndex_GridRules_Rules);
                ClearListRule_Rules();
                RefreshRule_Rules();
                ClearControlsRule_Rules();
                ChangePageStateRule_Rules('View');
            }
            else {
                if (CurrentPageStateRule_Rules == 'Delete')
                    ChangePageStateRule_Rules('View');
            }
        }
    }
}
function CallBackSaveRules_Rules_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
    ShowConnectionError_Rules();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageStateRule_Rules('View');
}

function tlbItemOk_TlbOkConfirm_onClick() {

    //var objType = DialogConfirm.get_value();

    //if (objType == 'Rule') {
        switch (ConfirmStateRule_Rules) {
            case 'Delete':
                DialogConfirm.Close();
                UpdateRule_Rules();
                break;
            case 'Exit':
                RefreshRule_Rules();
                CloseDialogRulesManagement();
                break;
            default:
        }
    //}

    //if (objType == 'RuleParameter') {
    //    switch (ConfirmStateRuleParameter_Rules) {
    //        case 'Delete':

    //            DialogConfirm.Close();
    //            UpdateRuleParameter_Rules();

    //            break;
    //        case 'Exit':

    //            RefreshRuleParameter_Rules();
    //            CloseDialogRulesManagement();

    //            break;
    //        default:
    //    }
    //}



}

function SetEnumTypes() {
    EnumRuleParameterTypeRuleParameter_Rules = JSON.parse($('#hfJsonRuleParameterTypeEnum').val());
}

function GetRuleTempParamTypeTitle_Rules(enumId) {
    return EnumRuleParameterTypeRuleParameter_Rules[enumId];
}

function GridRuleParameters_Rules_onLoad(sender, args) {
    document.getElementById('loadingPanel_GridRuleParameters_Rule').innerHTML = '';
}

function GridRuleParameters_Rules_onItemSelect(sender, args) {
    if (CurrentPageStateRuleParameter_Rules != 'Add')
        NavigateRuleParameter_Rules(args.get_item());
}

function CallBack_GridRuleParameters_Rules_OnCallbackComplete(sender, args) {
    var error = document.getElementById('ErrorHiddenFieldRuleParameter_Rules').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Refresh_GridRuleParameters_Rules();
    }
}

function CallBack_GridRuleParameters_Rules_onCallbackError(sender, args) {
    ShowConnectionError_Rules();
}

function GridRuleParametersCallCallBack_Rules(SelectedRuleId) {
    //CallBack_GridRuleParameters_Rules.callback(CharToKeyCode_Rules(SelectedRuleId));
}

function cmbRuleParameterType_Rules_onBeforeCallback(sender, args) {
    //cmbRuleParameterType_Rules.dispose();
}

function cmbRuleParameterType_Rules_onCallbackComplete(sender, args) {
    var error = document.getElementById('ErrorHiddenField_RuleParameterTypeFields').value;
    if (error == "") {
        document.getElementById('cmbRuleParameterType_Rules_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompletedRule_Rules())
            $('#cmbRuleParameterType_Rules_DropImage').mousedown();
        cmbRuleParameterType_Rules.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleParameterType_Rules_DropDown').style.display = 'none';
    }
}

function cmbRuleParameterType_Rules_onExpand(sender, args) {
    if (cmbRuleParameterType_Rules.get_itemCount() == 0 && CombosCallBackCurrentStateRuleParameter_Rules.IsExpandOccured_cmbRuleParameterType_Rules == undefined) {
        CombosCallBackCurrentStateRuleParameter_Rules.cmbRuleParameterType_Rules_Text = document.getElementById('cmbRuleParameterType_Rules_Input').value;
        CombosCallBackCurrentStateRuleParameter_Rules.IsExpandOccured_cmbRuleParameterType_Rules = true;
        Fill_cmbRuleParameterType_Rules();
    } else {
        document.getElementById('cmbRuleParameterType_Rules_Input').value = CombosCallBackCurrentStateRuleParameter_Rules.cmbRuleParameterType_Rules_Text;
    }
}

function Fill_cmbRuleParameterType_Rules() {
    ComboBox_onBeforeLoadData('cmbRuleParameterType_Rules');
    CallBack_cmbRuleParameterType_Rules.callback();
}

function cmbRuleParameterType_Rules_onCallbackError(sender, args) {
    document.getElementById('loadingPanel_GridRules_Rules').innerHTML = '';
    ShowConnectionError_Rules();
}

function tlbItemNew_TlbRuleParameters_onClick(sender, args) {
    ClearControlsRuleTempParam_Rules();
    ChangePageStateRuleParameter_Rules('Add');
}

function tlbItemEdit_TlbRuleParameters_onClick(sender, args) {
    ChangePageStateRuleParameter_Rules('Edit');
}

function tlbItemDelete_TlbRuleParameters_onClick(sender, args) {
    ChangePageStateRuleParameter_Rules('Delete');
}

function tlbItemSave_TlbRuleParameters_onClick(sender, args) {
    RuleParameters_onSave();
}

function tlbItemCancel_TlbRuleParameters_onClick(sender, args) {
    RuleParameters_onCancel();
}

function Refresh_GridRuleParameters_Rules() {
    if (GridRules_Rules.getSelectedItems()[0] != undefined) {
        RefreshRuleParameter_Rules();
        ClearControlsRuleTempParam_Rules();
        GridRuleParametersCallCallBack_Rules(GridRules_Rules.getSelectedItems()[0].getMember('ID').get_text());
    }
}

function ChangePageStateRuleParameter_Rules(State) {
    CurrentPageStateRuleParameter_Rules = State;
    SetActionModeRuleParameter_Rules(State);

    if (State == 'Add' || State == 'Edit' || State == 'Delete') {
        if (TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_imageUrl('add_silver.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_imageUrl('edit_silver.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_enabled(false);
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_imageUrl('remove_silver.png');
        }

        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_enabled(true);
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_imageUrl('save.png');

        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_enabled(true);
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_imageUrl('cancel.png');

        cmbRuleScope_Rules.enable();
        cmbRuleType_Rules.enable();

        if (State == 'Edit')
            NavigateRuleParameter_Rules(GridRuleParameters_Rules.getSelectedItems()[0]);
        if (State == 'Delete')
            RuleParameters_onSave();
    }

    if (State == 'View') {
        if (TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemNew_TlbRuleParameters').set_imageUrl('add.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemEdit_TlbRuleParameters').set_imageUrl('edit.png');
        }
        if (TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters') != null) {
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_enabled(true);
            TlbRuleParameters.get_items().getItemById('tlbItemDelete_TlbRuleParameters').set_imageUrl('remove.png');
        }

        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_enabled(false);
        TlbRuleParameters.get_items().getItemById('tlbItemSave_TlbRuleParameters').set_imageUrl('save_silver.png');

        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_enabled(false);
        TlbRuleParameters.get_items().getItemById('tlbItemCancel_TlbRuleParameters').set_imageUrl('cancel_silver.png');

        cmbRuleScope_Rules.disable();
        cmbRuleType_Rules.disable();

    }
}

function NavigateRuleParameter_Rules(RuleParameter) {

    ClearControlsRuleTempParam_Rules();

    if (RuleParameter == undefined) {
        return;
    }

    SelectedRuleParameter_Rules = new Object();

    FillSelectedRuleTempParam_Rules(
        RuleParameter.getMember('ID').get_text(),
        RuleParameter.getMember('Name').get_text(),
        RuleParameter.getMember('Title').get_text(),
        RuleParameter.getMember('Type').get_text(),
        RuleParameter.getMember('RuleTemplateId').get_text()
    );

    FillFieldsSelectedRuleTempParam_Rules();

}

function ClearListRuleParameter_Rules() {
    if (CurrentPageStateRuleParameter_Rules != 'Edit') {
        ClearControlsRuleTempParam_Rules();
    }
}
function ClearControlsRuleTempParam_Rules() {

    document.getElementById('txtRuleParameterTitle_Rules').value = '';
    document.getElementById('txtRuleParameterName_Rules').value = '';

    document.getElementById('cmbRuleParameterType_Rules_Input').value = '';
    if (cmbRuleParameterType_Rules.getSelectedItem() != undefined)
        cmbRuleParameterType_Rules.unSelect();

}

function RuleParameters_onSave() {
    if (CurrentPageStateRuleParameter_Rules != 'Delete')
        UpdateRuleParameter_Rules();
    else
        ShowDialogConfirm('Delete', 'RuleParameter');
}

function RuleParameters_onCancel() {
    ChangePageStateRuleParameter_Rules('View');
    ClearControlsRuleTempParam_Rules();
}

function RefreshRuleParameter_Rules() {
    FillSelectedRuleTempParam_Rules(0, null, null, null, 0);
}

function FillSelectedRuleTempParam_Rules(id, name, title, type, ruleId) {
    SelectedRuleParameter_Rules.RuleTempParamId = id;
    SelectedRuleParameter_Rules.RuleParameterName = name;
    SelectedRuleParameter_Rules.RuleParameterTitle = title;
    SelectedRuleParameter_Rules.RuleParameterType = type;
    SelectedRuleParameter_Rules.RuleTempParamRuleId = ruleId;
}
function FillFieldsSelectedRuleTempParam_Rules() {
    //document.getElementById('txtRuleParameterTitle_Rules').value = SelectedRuleParameter_Rules.RuleParameterTitle;
    //document.getElementById('txtRuleParameterName_Rules').value = SelectedRuleParameter_Rules.RuleParameterName;
    //document.getElementById('cmbRuleParameterType_Rules_Input').value = GetRuleTempParamTypeTitle_Rules(SelectedRuleParameter_Rules.RuleParameterType);
}

function SetActionModeRuleParameter_Rules(state) {
    //document.getElementById('ActionMode_RuleParameter_Rules').innerHTML = document.getElementById("hfState" + state + "_Rules").value;
}

function UpdateRuleParameter_Rules() {

    ObjRuleParameter_Rules = new Object();

    ObjRuleParameter_Rules.ID = 0;
    ObjRuleParameter_Rules.RuleId = 0;
    ObjRuleParameter_Rules.Title = "";
    ObjRuleParameter_Rules.Name = "";
    ObjRuleParameter_Rules.TypeId = 0;
    ObjRuleParameter_Rules.Type = null;

    var SelectedItems_GridRuleParameters_Rules = GridRuleParameters_Rules.getSelectedItems();
    if (SelectedItems_GridRuleParameters_Rules.length > 0)
        ObjRuleParameter_Rules.ID = SelectedItems_GridRuleParameters_Rules[0].getMember("ID").get_text();
    else ObjRuleParameter_Rules.ID = 0;

    var SelectedItems_GridRules_Rules = GridRules_Rules.getSelectedItems();
    if (SelectedItems_GridRules_Rules.length > 0)
        ObjRuleParameter_Rules.RuleId = SelectedItems_GridRules_Rules[0].getMember("ID").get_text();
    else ObjRuleParameter_Rules.RuleId = 0;

    if (CurrentPageStateRuleParameter_Rules != 'Delete') {

        ObjRuleParameter_Rules.Title = document.getElementById('txtRuleParameterTitle_Rules').value;
        ObjRuleParameter_Rules.Name = document.getElementById('txtRuleParameterName_Rules').value;

        if (cmbRuleParameterType_Rules.getSelectedItem() != undefined) {
            ObjRuleParameter_Rules.TypeId = parseInt(cmbRuleParameterType_Rules.getSelectedItem().Value);
        } else if (SelectedRuleParameter_Rules.TypeId != undefined) {
            ObjRuleParameter_Rules.TypeId = SelectedRuleParameter_Rules.TypeId;
        }
    }

    UpdateRuleParameter_RulesPage(
        CharToKeyCode_Rules(ObjRuleParameter_Rules.ID),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.RuleId),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.Title),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.Name),
        CharToKeyCode_Rules(ObjRuleParameter_Rules.TypeId),
        CharToKeyCode_Rules(CurrentPageStateRuleParameter_Rules)
    );
}

function UpdateRuleParameter_RulesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Rules').value;
            Response[1] = document.getElementById('hfConnectionError_Rules').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {

            var SelectedItems_GridRule_Rules = GridRules_Rules.getSelectedItems();
            if (SelectedItems_GridRule_Rules.length > 0) {

                var se1 = SelectedItems_GridRule_Rules[0];
                var se2 = se1.getMember("ID");
                var se3 = se2.get_text();

                GridRuleParametersCallCallBack_Rules(se3);
            }

            ClearListRuleParameter_Rules();
            RefreshRuleParameter_Rules();
            ClearControlsRuleTempParam_Rules();
            ChangePageStateRuleParameter_Rules('View');
        }
        else {
            if (CurrentPageStateRuleParameter_Rules == 'Delete')
                ChangePageStateRuleParameter_Rules('View');
        }
    }
}

//function txtRuleCode_Rules_onChange() {
//    var val = document.getElementById('txtRuleCode_Rules').value;
//    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '0' : '0';
//    document.getElementById('txtRuleCode_Rules').value = val;
//}

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

function tlbItemCompileConceptAndRule_TlbRules_onClick() {
    TlbRules.get_items().getItemById('tlbItemCompileConceptAndRule_TlbRules').set_enabled(false);
    CompileAndMakeDll_Rules();
}

function CompileAndMakeDll_Rules_onCallBack(Response) {
    var RetMessage = Response;
    try {
        if (RetMessage != null && RetMessage.length > 0) {
            

            //if (Response[1] == "ConnectionError") {
            //    Response[0] = document.getElementById('hfErrorType_Rules').value;
            //    Response[1] = document.getElementById('hfConnectionError_Rules').value;
            //}
            if (Response[2] == "success")
            {
                $("#DLLIframe_RulesManagement").attr({ src: "RuleGenerator/GTS.Clock.Business.UserCalculator.DLL" });

            }
            showDialog(RetMessage[0], Response[1], RetMessage[2]);

        }
    } catch (e) {

    }
    finally {
        TlbRules.get_items().getItemById('tlbItemCompileConceptAndRule_TlbRules').set_enabled(true);
    }

}

function CallBack_GridUsers_Users_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Users').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridUsers_Users(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else
        Changefooter_GridUsers_Users();
}
function CallBack_GridUsers_Users_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridUsers_Users').innerHTML = '';
    ShowConnectionError_Users();
}


function CallBack_GridUsers_Users_OnCallbackError(sender, e) {
    ShowConnectionError_Users();
}
function ShowConnectionError_Users() {
    var error = document.getElementById('hfErrorType_Users').value;
    var errorBody = document.getElementById('hfConnectionError_Users').value;
    showDialog(error, errorBody, 'error');
}
function GridUsers_Users_onItemSelect(sender, e) {
    if (CurrentPageState_Users != 'Add')
        NavigateUsers_Users(e.get_item());
}
function GridUsers_Users_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUsers_Users').innerHTML = '';
}
function NavigateUsers_Users(selectedUserItem) {
    if (selectedUserItem != undefined) {
        document.getElementById('chbActiveUser_Users').checked = selectedUserItem.getMember('Active').get_value();
        SelectedPersonnel_Users = new Object();
        SelectedPersonnel_Users.ID = selectedUserItem.getMember('PersonID').get_text();
        SelectedPersonnel_Users.Name = selectedUserItem.getMember('PersonName').get_text();
        SelectedPersonnel_Users.BarCode = selectedUserItem.getMember('PersonCode').get_text();
        SelectedRole_Users = new Object();
        SelectedRole_Users.ID = selectedUserItem.getMember('RoleID').get_text();
        SelectedRole_Users.Name = selectedUserItem.getMember('RoleName').get_text();
        document.getElementById('cmbUserRole_Users_Input').value = SelectedRole_Users.Name = selectedUserItem.getMember('RoleName').get_text();
        var IsActiveDirectoryAuthenticated = selectedUserItem.getMember('ActiveDirectoryAuthenticate').get_value();
        if (IsActiveDirectoryAuthenticated != null && IsActiveDirectoryAuthenticated != undefined && IsActiveDirectoryAuthenticated != '' && IsActiveDirectoryAuthenticated != false) {
            SelectedActiveDirectoryUser_Users = new Object();
            SelectedActiveDirectoryUser_Users.DomainID = selectedUserItem.getMember('TheDoaminId').get_text();
            document.getElementById('cmbDomainName_Users_Input').value = SelectedActiveDirectoryUser_Users.DomainName = selectedUserItem.getMember('TheDoaminName').get_text();
            document.getElementById('cmbDomainUserName_Users_Input').value = SelectedActiveDirectoryUser_Users.UserName = selectedUserItem.getMember('UserName').get_text();
            document.getElementById('rdbActiveDirValidation_Users').checked = true;
            document.getElementById('txtUserName_Users').value = '';
            document.getElementById('txtPassword_Users').value = document.getElementById('txtPasswordRepeat_Users').value = '';
            if (CurrentPageState_Users == 'Edit')
                ChangeItemsEnabled_onCheckChange('ActiveDirValidation');
        }
        else {
            document.getElementById('txtUserName_Users').value = selectedUserItem.getMember('UserName').get_text();
            document.getElementById('txtPassword_Users').value = document.getElementById('txtPasswordRepeat_Users').value = selectedUserItem.getMember('Password').get_text();
            document.getElementById('rdbUserNameIntroduction_Users').checked = true;
            document.getElementById('cmbDomainName_Users_Input').value = '';
            document.getElementById('cmbDomainUserName_Users_Input').value = '';
            SelectedActiveDirectoryUser_Users = null;
            if (CurrentPageState_Users == 'Edit')
                ChangeItemsEnabled_onCheckChange('UserNameIntroduction');
        }
    }
    //CallBack_cmbRuleType_Rules.callback();
}

function tlbItemHelp_TlbRulesManagement_onClick()
{
    LoadHelpPage('tlbItemHelp_TlbRulesManagement');

}

