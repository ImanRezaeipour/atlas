var LoadState_Precard = 'Normal';
var ObjPrecardList_RulePrecards = [];
var ObjParameterList_RulePrecards = [];
var Operator = false;
var ConfirmState_RulePrecards = null;
var box_RTagDiv_RulePrecards_IsShown = false;


function GetBoxesHeaders_RulePrecards() {   
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var ruleType = ObjDialogRulePrecards.RuleType;
    var ruleName = ObjDialogRulePrecards.RuleName;
    var ExistTag = ObjDialogRulePrecards.ExistRuleTag;
    document.getElementById('header_RuleText_RulePrecards').innerHTML = document.getElementById('hfheader_RuleText_RulePrecards').value + ' ' + ruleName;
    switch (ruleType) {
        case '1':
            parent.document.getElementById('Title_DialogRulePrecards').innerHTML = document.getElementById('hfTitle_RulePrecardsParameters_DialogRulePrecards').value;
            document.getElementById('header_RulePrecards_RulePrecards').innerHTML = document.getElementById('hfheader_RulePrecardsParameters_RulePrecards').value;
            document.getElementById('tblOperator_RulePrecards').style.visibility = 'hidden';
            break;
        case '2':
            parent.document.getElementById('Title_DialogRulePrecards').innerHTML = document.getElementById('hfTitle_RulePrecards_DialogRulePrecards').value;
            document.getElementById('header_RulePrecards_RulePrecards').innerHTML = document.getElementById('hfheader_RulePrecards_RulePrecards').value;
            document.getElementById('tblOperator_RulePrecards').style.visibility = 'hidden';
            break;
        case '3':
            parent.document.getElementById('Title_DialogRulePrecards').innerHTML = document.getElementById('hfTitle_RuleParameters_DialogRulePrecards').value;
            document.getElementById('header_RulePrecards_RulePrecards').innerHTML = document.getElementById('hfheader_RuleParameter_RulePrecards').value;
            document.getElementById('txtSerchTerm_Precard').disabled = 'disabled';
            TlbPrecardQuickSearch.get_items().getItemById('tlbItemSearch_TlbPrecardQuickSearch').set_enabled(false);
            document.getElementById('tblPecardSearch_RulePrecards').style.visibility = 'hidden';
            break;
        case '4':
            document.getElementById('header_RulePrecards_RulePrecards').innerHTML = document.getElementById('hfheader_RuleParameter_RulePrecards').value;
            document.getElementById('txtSerchTerm_Precard').disabled = 'disabled';
            TlbPrecardQuickSearch.get_items().getItemById('tlbItemSearch_TlbPrecardQuickSearch').set_enabled(false);
            document.getElementById('tblPecardSearch_RulePrecards').style.visibility = 'hidden';           
            document.getElementById('tbRefresh_RulePrecards').style.visibility = 'hidden'; 
            document.getElementById('tbGridRulePrecard_RulePrecards').style.visibility = 'hidden';
            document.getElementById('header_RulePrecards_RulePrecards').style.visibility = 'hidden';
            break;
    }
    document.getElementById('header_RuleDetails_RulePrecards').innerHTML = document.getElementById('hfheader_RuleDetails_RulePrecards').value;
    if (ExistTag == 'true') {
        document.getElementById('divRuleDetails_RulePrecards').style.visibility = 'visible';
        ChangeEnabled_DropDownDive_RulePrecards('enabled');
       
        
    }
}
function tlbItemSave_TlbRulePrecards_onClick() {
    UpdateRulePrecards_RulePrecards();
}
function UpdateRulePrecards_RulePrecards() {
    var ObjRuleDetails_RulePrecards = new Object();
    ObjRuleDetails_RulePrecards.StrObjRuleDetails_RulePrecards = null;
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var ExistRuleTag = ObjDialogRulePrecards.ExistRuleTag;
    var CustomCode = ObjDialogRulePrecards.CustomCode;
    var groupID = ObjDialogRulePrecards.GroupID;
    var ruleID = ObjDialogRulePrecards.RuleID;
    var ruleGroupID = ObjDialogRulePrecards.RuleGroupID;
    var ruleType = ObjDialogRulePrecards.RuleType;
    var Active = ObjDialogRulePrecards.Active;
    var Warning = ObjDialogRulePrecards.Warning;
    var strRulePrecardList = JSON.stringify(ObjPrecardList_RulePrecards);
    if (ExistRuleTag == 'true') {
        switch (CustomCode) {
            case '33':
                SubstituteConfirm = document.getElementById('chbSubstituteConfirmation_RulePrecards').checked;
                ObjRuleDetails_RulePrecards.StrObjRuleDetails_RulePrecards = '{"IsForceConfirmByRequestSubstitute" : "' + SubstituteConfirm + '" }';
                break;
        }
    }

    UpdateRulePrecards_RulePrecardsPage(CharToKeyCode_RulePrecards(groupID), CharToKeyCode_RulePrecards(ruleID), CharToKeyCode_RulePrecards(ruleGroupID), CharToKeyCode_RulePrecards(strRulePrecardList), CharToKeyCode_RulePrecards(ruleType), CharToKeyCode_RulePrecards(ObjRuleDetails_RulePrecards.StrObjRuleDetails_RulePrecards), CharToKeyCode_RulePrecards(CustomCode), CharToKeyCode_RulePrecards(Active), CharToKeyCode_RulePrecards(Warning));
    DialogWaiting.Show();

}
function UpdateRulePrecards_RulePrecardsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RulePrecards').value;
            Response[1] = document.getElementById('hfConnectionError_RulePrecards').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Refresh_GridRulePrecards_RulePrecards();
        }
    }
}
function GridRulePrecards_RulePrecards_onItemCheckChange(sender, e) {
    GridLevel = '0';
    var Value = null;
    var Operator = null;
    var SelectedGridItem_GridRulePrecards_RulePrecards = e.get_item();
    var index = e.get_item().get_index();
    var chbActiveID = 'checkbox_GridRulePrecards_RulePrecards_Active_' + index.toString();
    var chbOprerator = 'checkbox_GridRulePrecards_RulePrecards_Operator_' + index.toString();
    if (e.get_item().getMember('Active').get_value() != document.getElementById(chbActiveID).checked)
        Value = !e.get_item().getMember('Active').get_value();
    else
        Value = e.get_item().getMember('Active').get_value();
    if (e.get_item().getMember('Operator').get_value() != document.getElementById(chbOprerator).checked)
        Operator = !e.get_item().getMember('Operator').get_value();
    else
        Operator = e.get_item().getMember('Operator').get_value();
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var RuleType = ObjDialogRulePrecards.RuleType;
    UpdateRulePrecardsParamList_RulePrecards(GridLevel, SelectedGridItem_GridRulePrecards_RulePrecards, Value, RuleType, Operator);
    ClearParameterValue_RulePrecards(GridLevel, SelectedGridItem_GridRulePrecards_RulePrecards);
}
function chbActiveOperator_RulePrecards_OnChange() {
    JSON.search.trace = true;
    var RuleType = parent.DialogRulePrecards.get_value().RuleType;
    if (RuleType == '3') {
        var rulePrecardResult = JSON.search(ObjPrecardList_RulePrecards, '//*[PrecardID = 0]');
        if (rulePrecardResult != null && rulePrecardResult != undefined && rulePrecardResult.length > 0) {
            rulePrecardResult[0].Operator = document.getElementById('chbActiveOperator_RulePrecards').checked;
        }

    }
    if (RuleType == '4') {
        var rulePrecardResult = JSON.search(ObjPrecardList_RulePrecards, '//*[PrecardID = 0]');
        if (rulePrecardResult != null && rulePrecardResult != undefined && rulePrecardResult.length > 0) 
            rulePrecardResult[0].Operator = document.getElementById('chbActiveOperator_RulePrecards').checked;
        else
        {
            var ObjRulePrecard_RulePrecards = new Object();
            ObjRulePrecard_RulePrecards.ID = 0;
            ObjRulePrecard_RulePrecards.PrecardID = 0;
            ObjRulePrecard_RulePrecards.Active = true;
            ObjRulePrecard_RulePrecards.Operator = document.getElementById('chbActiveOperator_RulePrecards').checked;
            ObjRulePrecard_RulePrecards.ExistPrecard = 0;
            ObjRulePrecard_RulePrecards.ObjRuleParams = [];
            ObjPrecardList_RulePrecards.push(ObjRulePrecard_RulePrecards);
        }
    }
}
function UpdateRulePrecardsParamList_RulePrecards(Level, SelectedGridItem_GridRulePrecards_RulePrecards, Value, RuleType, Operator) {
    JSON.search.trace = true;
    if (RuleType == '3') {
        var rulePrecardResult = JSON.search(ObjPrecardList_RulePrecards, '//*[PrecardID = 0]');
        if (rulePrecardResult != null && rulePrecardResult != undefined && rulePrecardResult.length > 0) {
            var ruleParameterResult = JSON.search(ObjPrecardList_RulePrecards, '//*[ParamID = "' + SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text() + '"]');
            if (ruleParameterResult != null && ruleParameterResult != undefined && ruleParameterResult.length > 0) {
                ruleParameterResult[0].ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
                ruleParameterResult[0].ParamID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text();
                ruleParameterResult[0].KeyName = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('KeyName').get_text();
                ruleParameterResult[0].ExistParam = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistParam').get_text();
                ruleParameterResult[0].ParameterValue = Value;
                ruleParameterResult[0].ParamType = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text();
                ruleParameterResult[0].ContinueOnTomorrow = document.getElementById('chbNextDay_RulePrecards').checked;

            }
            else {
                var ObjRuleParam_RulePrecards = new Object();
                ObjRuleParam_RulePrecards.ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
                ObjRuleParam_RulePrecards.ParamID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text();
                ObjRuleParam_RulePrecards.KeyName = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('KeyName').get_text();
                ObjRuleParam_RulePrecards.ExistParam = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistParam').get_text();
                ObjRuleParam_RulePrecards.ParamType = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text();
                ObjRuleParam_RulePrecards.ParameterValue = Value;
                rulePrecardResult[0].ObjRuleParams.push(ObjRuleParam_RulePrecards);
            }
        }
        else {
            var ObjRulePrecard_RulePrecards = new Object();
            var ObjRuleParam_RulePrecards = new Object();
            ObjRulePrecard_RulePrecards.ID = 0;
            ObjRulePrecard_RulePrecards.PrecardID = 0;
            ObjRulePrecard_RulePrecards.Active = true;
            ObjRulePrecard_RulePrecards.Operator = document.getElementById('chbActiveOperator_RulePrecards').checked;
            ObjRulePrecard_RulePrecards.ObjRuleParams = [];
            ObjRuleParam_RulePrecards.ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
            ObjRuleParam_RulePrecards.ParamID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text();
            ObjRuleParam_RulePrecards.KeyName = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('KeyName').get_text();
            ObjRuleParam_RulePrecards.ExistParam = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistParam').get_text();
            ObjRuleParam_RulePrecards.ParameterValue = Value;
            ObjRuleParam_RulePrecards.ParamType = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text();
            ObjRuleParam_RulePrecards.ContinueOnTomorrow = document.getElementById('chbNextDay_RulePrecards').checked;
            ObjRulePrecard_RulePrecards.ObjRuleParams.push(ObjRuleParam_RulePrecards);
            ObjPrecardList_RulePrecards.push(ObjRulePrecard_RulePrecards);
        }
    }
    else {
        var rulePrecardResult = JSON.search(ObjPrecardList_RulePrecards, '//*[PrecardID = "' + SelectedGridItem_GridRulePrecards_RulePrecards.getMember('PrecardID').get_text() + '" ]');
        if (rulePrecardResult != null && rulePrecardResult != undefined && rulePrecardResult.length > 0) {

            switch (Level) {
                case '0':
                    rulePrecardResult[0].ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
                    rulePrecardResult[0].PrecardID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('PrecardID').get_text();
                    rulePrecardResult[0].Active = Value;
                    rulePrecardResult[0].Operator = Operator;
                    rulePrecardResult[0].ExistPrecard = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistPrecard').get_text();
                    break;
                case '1':
                    var ruleParamResult = JSON.search(rulePrecardResult, '//*[ParamID = "' + SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text() + '"]');
                    if (ruleParamResult != null && ruleParamResult != undefined && ruleParamResult.length > 0) {
                        ruleParamResult[0].ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
                        ruleParamResult[0].ParamID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text();
                        ruleParamResult[0].KeyName = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('KeyName').get_text();
                        ruleParamResult[0].ExistParam = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistParam').get_text();
                        ruleParamResult[0].ContinueOnTomorrow = document.getElementById('chbNextDay_RulePrecards').checked;
                        ruleParamResult[0].ParameterValue = Value;
                        ruleParamResult[0].ParamType = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text();
                    }
                    else {
                        var ObjRuleParam_RulePrecards = new Object();
                        ObjRuleParam_RulePrecards.ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
                        ObjRuleParam_RulePrecards.ParamID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text();
                        ObjRuleParam_RulePrecards.KeyName = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('KeyName').get_text();
                        ObjRuleParam_RulePrecards.ExistParam = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistParam').get_text();
                        ObjRuleParam_RulePrecards.ParamType = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text();
                        ObjRuleParam_RulePrecards.ParameterValue = Value;
                        ObjRuleParam_RulePrecards.ContinueOnTomorrow = document.getElementById('chbNextDay_RulePrecards').checked;
                        rulePrecardResult[0].ObjRuleParams.push(ObjRuleParam_RulePrecards);
                    }
                    break;
            }
        }
        else {
            var ObjRulePrecard_RulePrecards = new Object();
            switch (Level) {
                case '0':
                    ObjRulePrecard_RulePrecards.ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
                    ObjRulePrecard_RulePrecards.PrecardID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('PrecardID').get_text();
                    ObjRulePrecard_RulePrecards.Active = Value;
                    ObjRulePrecard_RulePrecards.Operator = Operator;
                    ObjRulePrecard_RulePrecards.ExistPrecard = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistPrecard').get_text();
                    ObjRulePrecard_RulePrecards.ObjRuleParams = [];
                    ObjPrecardList_RulePrecards.push(ObjRulePrecard_RulePrecards);
                    break;
                case '1':
                    var parentItem = GridRulePrecards_RulePrecards.getItemFromKey(0, SelectedGridItem_GridRulePrecards_RulePrecards.ParentItem.Key);
                    ObjRulePrecard_RulePrecards.ID = parentItem.getMember('ID').get_text();
                    ObjRulePrecard_RulePrecards.PrecardID = parentItem.getMember('PrecardID').get_text();
                    ObjRulePrecard_RulePrecards.Active = parentItem.getMember('Active').get_value();
                    ObjRulePrecard_RulePrecards.Operator = parentItem.getMember('Operator').get_value();
                    ObjRulePrecard_RulePrecards.ExistPrecard = parentItem.getMember('ExistPrecard').get_text();
                    var ObjRuleParam_RulePrecards = new Object();
                    ObjRuleParam_RulePrecards.ID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ID').get_text();
                    ObjRuleParam_RulePrecards.ParamID = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text();
                    ObjRuleParam_RulePrecards.KeyName = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('KeyName').get_text();
                    ObjRuleParam_RulePrecards.ExistParam = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ExistParam').get_text();
                    ObjRuleParam_RulePrecards.ParamType = SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text();
                    ObjRuleParam_RulePrecards.ParameterValue = Value;
                    ObjRuleParam_RulePrecards.ContinueOnTomorrow = document.getElementById('chbNextDay_RulePrecards').checked;
                    ObjRulePrecard_RulePrecards.ObjRuleParams = [];
                    ObjRulePrecard_RulePrecards.ObjRuleParams.push(ObjRuleParam_RulePrecards);
                    ObjPrecardList_RulePrecards.push(ObjRulePrecard_RulePrecards);
                    break;
            }
        }
    }
}
function tlbItemHelp_TlbRulePrecards_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRulePrecards');
}
function tlbItemFormReconstruction_TlbRulePrecards_onClick() {
    CloseDialogRulePrecards();
    parent.document.getElementById('pgvUiValidation_iFrame').contentWindow.ShowDialogRulePrecards();
}
function CloseDialogRulePrecards() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogRulePrecards_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogRulePrecards').Close();
}
function tlbItemExit_TlbRulePrecards_onClick() {
    ShowDialogConfirm();
}
function ShowDialogConfirm() {
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var ruleType = ObjDialogRulePrecards.RuleType;
    switch (ruleType) {
        case '1':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RulePrecardsParameters_RulePrecards').value;
            break;
        case '2':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RulePrecards_RulePrecards').value;
            break;
        case '3':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RuleParameters_RulePrecards').value;
            break;
        case '4':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RulePrecards').value;
            break;
    }
    DialogConfirm.Show();
}
function Refresh_GridRulePrecards_RulePrecards() {
    Fill_GridRulePrecards_RulePrecards('Normal');
}
function GridRulePrecards_RulePrecards_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRulePrecards_RulePrecards').innerHTML = '';
}
function CallBack_GridRulePrecards_RulePrecards_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RulePrecards_RulePrecards').value;
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var CustomCode = ObjDialogRulePrecards.CustomCode;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRulePrecards_RulePrecards('Normal');        
    }
    TlbRulePrecards.get_items().getItemById('tlbItemSave_TlbRulePrecards').set_enabled(true);
    TlbRulePrecards.get_items().getItemById('tlbItemSave_TlbRulePrecards').set_imageUrl('save.png');
    ObjPrecardList_RulePrecards = eval('(' + document.getElementById('RulePrecardParamObjHiddenField_RulePrecards').value + ')');
    var RuleType = parent.DialogRulePrecards.get_value().RuleType;
    if ((RuleType == '3' || RuleType == '4') && ObjPrecardList_RulePrecards != null && ObjPrecardList_RulePrecards != undefined && ObjPrecardList_RulePrecards.length > 0)
        document.getElementById('chbActiveOperator_RulePrecards').checked = ObjPrecardList_RulePrecards[0].Operator;
    if (document.getElementById('RuleDetailObjHiddenField_RulePrecards').value != '') {
        var objRuleDetail_RulePrecards = eval('(' + document.getElementById('RuleDetailObjHiddenField_RulePrecards').value + ')');
        if (objRuleDetail_RulePrecards != null && objRuleDetail_RulePrecards != undefined)
            SetValueToRuleDetail(CustomCode, objRuleDetail_RulePrecards);
    }
}

function CallBack_GridRulePrecards_RulePrecards_onCallbackError(sender, e) {
    ShowConnectionError_RulePrecards();
}
function ShowConnectionError_RulePrecards() {
    var error = document.getElementById('hfErrorType_RulePrecards').value;
    var errorBody = document.getElementById('hfConnectionError_RulePrecards').value;
    showDialog(error, errorBody, 'error');
}
function tlbItemOk_TlbOkConfirm_onClick() {
    parent.DialogRulePrecards.Close();
    DialogConfirm.Close();
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}
function tlbItemSetParameter_TlbRulePrecards_onClick() {
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var RuleType = ObjDialogRulePrecards.RuleType;
    if (GridRulePrecards_RulePrecards.getSelectedItems()[0] != null && GridRulePrecards_RulePrecards.getSelectedItems()[0] != undefined) {
        var SelectedGridItem_GridRulePrecards_RulePrecards = GridRulePrecards_RulePrecards.getSelectedItems()[0];
        switch (RuleType) {
            case '1':
                if (SelectedGridItem_GridRulePrecards_RulePrecards.Level > 0) {
                    NavligateRuleParameter_RulePrecards(SelectedGridItem_GridRulePrecards_RulePrecards);
                }
                break;
            case '3':
                NavligateRuleParameter_RulePrecards(SelectedGridItem_GridRulePrecards_RulePrecards);
                break;
            default:
                break;
        }
    }
}
function Fill_GridRulePrecards_RulePrecards(LoadState) {
    TlbRulePrecards.get_items().getItemById('tlbItemSave_TlbRulePrecards').set_enabled(false);
    TlbRulePrecards.get_items().getItemById('tlbItemSave_TlbRulePrecards').set_imageUrl('save_silver.png');
    document.getElementById('loadingPanel_GridRulePrecards_RulePrecards').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridRulePrecards_RulePrecards').value);
    var ObjDialogRulePrecard = parent.DialogRulePrecards.get_value();
    var SearchValue;
    switch (LoadState) {
        case 'Normal':
            SearchValue = '';
            CallBack_GridRulePrecards_RulePrecards.callback(CharToKeyCode_RulePrecards(ObjDialogRulePrecard.GroupID), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.RuleID), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.RuleGroupType), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.RuleType), CharToKeyCode_RulePrecards(SearchValue), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.CustomCode), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.ExistRuleTag));
            break;
        case 'Search':
            SearchValue = document.getElementById('txtSerchTerm_Precard').value;
            CallBack_GridRulePrecards_RulePrecards.callback(CharToKeyCode_RulePrecards(ObjDialogRulePrecard.GroupID), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.RuleID), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.RuleGroupType), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.RuleType), CharToKeyCode_RulePrecards(SearchValue), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.CustomCode), CharToKeyCode_RulePrecards(ObjDialogRulePrecard.ExistRuleTag));
            break;
    }
}
function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}
function CharToKeyCode_RulePrecards(str) {
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
function tlbItemExit_TlbParameterValue_onClick() {
    CloseDatepickerIframe();
    DialogParameterValue.Close();
    SetScrollTopPosition_RulePrecards();
}
function CloseDatepickerIframe() {
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpDate_RulePrecards');
}
function Container_pdpDate_RulePrecards_onKeyPress(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRulePrecardsTerms_onClick();
    }    
}
function tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRulePrecardsTerms_onClick() {
    GridLevel = '1';
    var NumericValue_RulePrecards = document.getElementById('txtNumeric_RulePrecards').value;
    if (!isNaN(parseInt(NumericValue_RulePrecards)) && parseInt(NumericValue_RulePrecards) >= 0 && !isNaN(Math.round(NumericValue_RulePrecards)) && !(NumericValue_RulePrecards > Math.round(NumericValue_RulePrecards)) && !(NumericValue_RulePrecards < Math.round(NumericValue_RulePrecards))) {

        var SelectedGridItem_GridRulePrecards_RulePrecards = GridRulePrecards_RulePrecards.getSelectedItems()[0];
        GridRulePrecards_RulePrecards.beginUpdate();
        SelectedGridItem_GridRulePrecards_RulePrecards.setValue(7, NumericValue_RulePrecards);
        GridRulePrecards_RulePrecards.endUpdate();
        var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
        var RuleType = ObjDialogRulePrecards.RuleType;
        UpdateRulePrecardsParamList_RulePrecards(GridLevel, SelectedGridItem_GridRulePrecards_RulePrecards, NumericValue_RulePrecards, RuleType, null);
        DialogParameterValue.Close();
        SetScrollTopPosition_RulePrecards();
    }
}
function tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRulePrecardsTerms_onClick() {
    GridLevel = '1';
    var TimeValue_RuleParametes = GetDuration_TimePicker_RulePrecards('TimeSelector_Hour_RulePrecards');
    var ContinueOnTomorrow_RuleParameters = document.getElementById('chbNextDay_RulePrecards').checked;
    if (document.getElementById('chbNextDay_RulePrecards').checked)
        TimeValue_RuleParametes = '+' + TimeValue_RuleParametes;
    else
        TimeValue_RuleParametes = TimeValue_RuleParametes.replace('+', '');
    var SelectedGridItem_GridRulePrecards_RulePrecards = GridRulePrecards_RulePrecards.getSelectedItems()[0];
    GridRulePrecards_RulePrecards.beginUpdate();
    SelectedGridItem_GridRulePrecards_RulePrecards.setValue(7, TimeValue_RuleParametes);
    SelectedGridItem_GridRulePrecards_RulePrecards.setValue(8, ContinueOnTomorrow_RuleParameters);
    GridRulePrecards_RulePrecards.endUpdate();
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var RuleType = ObjDialogRulePrecards.RuleType;
    UpdateRulePrecardsParamList_RulePrecards(GridLevel, SelectedGridItem_GridRulePrecards_RulePrecards, TimeValue_RuleParametes, RuleType, null);
    DialogParameterValue.Close();
    SetScrollTopPosition_RulePrecards();
}
function tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRulePrecardsTerms_onClick() {
    var DateValue_RuleParametes = null;
    CloseDatepickerIframe();
    GridLevel = '1';
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            DateValue_RuleParametes = document.getElementById('pdpDate_RulePrecards').value;
            break;
        case 'en-US':
            DateValue_RuleParametes = document.getElementById('gdpDate_RulePrecards_picker').value;
            break;
    }
    if (!isNaN(parseInt(DateValue_RuleParametes))) {
        var SelectedGridItem_GridRulePrecards_RulePrecards = GridRulePrecards_RulePrecards.getSelectedItems()[0];
        GridRulePrecards_RulePrecards.beginUpdate();
        SelectedGridItem_GridRulePrecards_RulePrecards.setValue(7, DateValue_RuleParametes);
        GridRulePrecards_RulePrecards.endUpdate();
        var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
        var RuleType = ObjDialogRulePrecards.RuleType;
        // Value = document.getElementById('pdpDate_RulePrecards').get_value()[0];
        UpdateRulePrecardsParamList_RulePrecards(GridLevel, SelectedGridItem_GridRulePrecards_RulePrecards, DateValue_RuleParametes, RuleType, null);
        DialogParameterValue.Close();
        SetScrollTopPosition_RulePrecards();
    }
}
function GetDuration_TimePicker_RulePrecards(TimePicker) {
    var hour = document.getElementById(TimePicker + '_txtHour').value;
    var minute = document.getElementById(TimePicker + '_txtMinute').value;
    if (hour == '' || parseFloat(hour) < 0)
        document.getElementById(TimePicker + '_txtHour').value = hour = '00';
    if (minute == '' || parseFloat(minute) < 0)
        document.getElementById(TimePicker + '_txtMinute').value = minute = '00';
    if (document.getElementById(TimePicker + '_txtHour').value.length < 2)
        document.getElementById(TimePicker + '_txtHour').value = '0' + document.getElementById(TimePicker + '_txtHour').value;
    if (document.getElementById(TimePicker + '_txtMinute').value.length < 2)
        document.getElementById(TimePicker + '_txtMinute').value = '0' + document.getElementById(TimePicker + '_txtMinute').value;
    return document.getElementById(TimePicker + '_txtHour').value + ':' + document.getElementById(TimePicker + '_txtMinute').value;
}
function TimeSelector_Hour_RulePrecards_onChange(partID) {
    var id = 'TimeSelector_Hour_RulePrecards_' + partID;
    var val = document.getElementById(id).value;
    val = !isNaN(parseFloat(val)) ? parseFloat(val) > 0 ? "" + parseFloat(val) + "" : '00' : '00';
    switch (partID) {
        case 'txtHour':
            break;
        case 'txtMinute':
            val = parseFloat(val) < 60 ? val : '59';
            break;
    }
    document.getElementById(id).value = val.length == 2 ? val : '0' + val;
}
function ViewCurrentLangCalendars_RulePrecards() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_RulePrecards").parentNode.removeChild(document.getElementById("pdpDate_RulePrecards"));
            document.getElementById("pdpDate_RulePrecardsimgbt").parentNode.removeChild(document.getElementById("pdpDate_RulePrecardsimgbt"));
            document.getElementById("pdpFromDate_RulePrecards").parentNode.removeChild(document.getElementById("pdpFromDate_RulePrecards"));
            document.getElementById("pdpFromDate_RulePrecardsimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_RulePrecardsimgbt"));
            document.getElementById("pdpToDate_RulePrecards").parentNode.removeChild(document.getElementById("pdpToDate_RulePrecards"));
            document.getElementById("pdpToDate_RulePrecardsimgbt").parentNode.removeChild(document.getElementById("pdpToDate_RulePrecardsimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RulePrecards").removeChild(document.getElementById("Container_gCalDate_RulePrecards"));
            document.getElementById("Container_FromDateCalendars_RulePrecards").removeChild(document.getElementById("Container_gCalFromDate_RulePrecards"));
            document.getElementById("Container_ToDateCalendars_RulePrecards").removeChild(document.getElementById("Container_gCalToDate_RulePrecards"));
            break;
    }
}
function ChangeCalendarsEnabled_RulePrecards(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_RulePrecards('pdpDate_RulePrecards', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_RulePrecards('gdpDate_RulePrecards', state);
            break;
    }
}
function ChangeCalendarEnabled_RulePrecards(cal, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById(cal).onclick = " ";
                    document.getElementById(cal + 'imgbt').onclick = " ";
                    break;
                case 'en-US':
                    document.getElementById('btn_' + cal).onclick = " ";
                    break;
            }
            break;
        case 'enable':
            disabled = '';
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById(cal).onclick = function () {
                        displayDatePicker(cal);
                    };
                    document.getElementById(cal + 'imgbt').onclick = function () {
                        displayDatePicker(cal);
                    };
                    break;
                case 'en-US':
                    document.getElementById('btn_' + cal).onclick = function () {
                        CalendarsViewManage_RulePrecards(cal);
                    };
                    break;
            }
            break;
    }
}
function gCalDate_RulePrecards_OnChange(sender, e) {
    var Date = gCalDate_RulePrecards.getSelectedDate();
    gdpDate_RulePrecards.setSelectedDate(Date);
}
function gCalDate_RulePrecards_onLoad(sender, e) {
    window.gCalDate_RulePrecards.PopUpObject.z = 25000000;
}
function gdpDate_RulePrecards_OnDateChange(sender, e) {
    var Date = gdpDate_RulePrecards.getSelectedDate();
    gCalDate_RulePrecards.setSelectedDate(Date);
}
function btn_gdpDate_RulePrecards_OnClick(event) {
    if (gCalDate_RulePrecards.get_popUpShowing()) {
        gCalDate_RulePrecards.hide();
    }
    else {
        gCalDate_RulePrecards.setSelectedDate(gdpDate_RulePrecards.getSelectedDate());
        gCalDate_RulePrecards.show();
    }
}
function btn_gdpDate_RulePrecards_OnMouseUp(event) {
    if (gCalDate_RulePrecards.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function GridRulePrecards_RulePrecards_onItemDoubleClick(sender, e) {
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var RuleType = ObjDialogRulePrecards.RuleType;
    if (e.get_item() != null && e.get_item() != undefined) {
        var SelectedGridItem_GridRulePrecards_RulePrecards = e.get_item();
        switch (RuleType) {
            case '1':
                if (SelectedGridItem_GridRulePrecards_RulePrecards.get_table().get_level() > 0) {
                    NavligateRuleParameter_RulePrecards(SelectedGridItem_GridRulePrecards_RulePrecards);
                }
                break;
            case '3':
                NavligateRuleParameter_RulePrecards(SelectedGridItem_GridRulePrecards_RulePrecards);
                break;
            default:
                break;
        }
    }
}
function SetFocus_RulePrecards(selectedRuleParameter) {
    switch (selectedRuleParameter.getMember('ParamType').get_text()) {
        case '0':
            document.getElementById("txtNumeric_RulePrecards").focus();
            break;
        case '1':
            document.getElementById("TimeSelector_Hour_RulePrecards_txtHour").focus();
            break;
        default:
            break;
    }
}
function NavligateRuleParameter_RulePrecards(selectedRuleParameter) {
    var ruleParameterValue = selectedRuleParameter.getMember('ParameterValue').get_text();
    switch (selectedRuleParameter.getMember('ParamType').get_text()) {
        case '0':
            TabStripRulePrecardsTerms.selectTabById('tbNumeric_TabStripRulePrecardsTerms');
            document.getElementById('txtNumeric_RulePrecards').value = ruleParameterValue;
            document.getElementById("txtNumeric_RulePrecards").focus();

            break;
        case '1':
            TabStripRulePrecardsTerms.selectTabById('tbTime_TabStripRulePrecardsTerms');
            SetDuration_TimePicker_RulePrecards('TimeSelector_Hour_RulePrecards', ruleParameterValue);
            document.getElementById('chbNextDay_RulePrecards').checked = selectedRuleParameter.getMember('ContinueOnTomorrow').get_value();
            break;
        case '2':
            TabStripRulePrecardsTerms.selectTabById('tbDate_TabStripRulePrecardsTerms');
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    document.getElementById('pdpDate_RulePrecards').value = ruleParameterValue;
                    document.getElementById('pdpDate_RulePrecards').focus();
                    break;
                case 'en-US':
                    ruleparametervalue = new date(ruleparametervalue);
                    gCalDate_RulePrecards.setSelectedDate(ruleParameterValue);
                    gdpDate_RulePrecards.setSelectedDate(ruleParameterValue);
                    document.getElementById('gdpDate_RulePrecards_picker').focus();
                    break;
            }
            break;
    }
    GetScrollTopPosition_RulePrecards();
    DialogParameterValue.Show();
    document.getElementById('Title_DialogParameterValue').innerHTML = '&nbsp;&nbsp;&nbsp;' + document.getElementById('hfTitle_DialogParameterValue').value;
    SetFocus_RulePrecards(selectedRuleParameter);
}

function SetDuration_TimePicker_RulePrecards(TimePicker, strTime) {
    if (strTime != '') {
        var arTime_RuleParameter = strTime.split(':');
        for (var i = 0; i < 2; i++) {
            if (arTime_RuleParameter[i].length < 2)
                arTime_RuleParameter[i] = '0' + arTime_RuleParameter[i];
        }
        document.getElementById(TimePicker + '_txtHour').value = arTime_RuleParameter[0];
        document.getElementById(TimePicker + '_txtMinute').value = arTime_RuleParameter[1];
    }
    else {
        document.getElementById(TimePicker + '_txtHour').value = '';
        document.getElementById(TimePicker + '_txtMinute').value = '';
    }
}
function tlbItemSearch_TlbPrecardQuickSearch_onClick() {
    Fill_GridRulePrecards_RulePrecards('Search');
}
function txtSerchTerm_Precard_onKeyPess(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbPrecardQuickSearch_onClick();
    }
}
function txtNumeric_RulePrecards_onkeypress(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRulePrecardsTerms_onClick();
    }
}
function TimeSelector_Hour_RulePrecards_txtHour_onKeyPress(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRulePrecardsTerms_onClick();
    }
}
function TimeSelector_Hour_RulePrecards_txtMinute_onKeyPress(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRulePrecardsTerms_onClick();
    }
}
function tlbItemClean_TlbClean_pgvDate_MultiPageRulePrecardsTerms_onClick() {
    var GridLevel = '1';
    ClearParameterValue_RulePrecards(GridLevel);
}

function tlbItemClean_TlbClean_pgvNumeric_MultiPageRulePrecardsTerms_onClick() {
    var GridLevel = '1';
    ClearParameterValue_RulePrecards(GridLevel);
}
function tlbItemClean_TlbClean_pgvTime_MultiPageRulePrecardsTerms_onClick() {
    var GridLevel = '1';
    ClearParameterValue_RulePrecards(GridLevel);
}
function GetErrorMessage_RulePrecards() {
    showDialog(document.getElementById('hfWarningType_RulePrecards').value, document.getElementById('hfParameterValueNotEmpty_RulePrecards').value, "error");
}
function ClearParameterValue_RulePrecards(gridLevel, selectedGridItem_GridRulePrecards_RulePrecards) {
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var RuleType = ObjDialogRulePrecards.RuleType;
    if (GridRulePrecards_RulePrecards.getSelectedItems()[0] != null && GridRulePrecards_RulePrecards.getSelectedItems()[0] != undefined) {
        var SelectedGridItem_GridRulePrecards_RulePrecards = GridRulePrecards_RulePrecards.getSelectedItems()[0];
    }
    switch (RuleType) {
        case '3':
            var rulePrecardResult = JSON.search(ObjPrecardList_RulePrecards, '//*[PrecardID = 0]');
            if (rulePrecardResult != null && rulePrecardResult != undefined && rulePrecardResult.length > 0) {
                for (var j = rulePrecardResult[0].ObjRuleParams.length - 1; j >= 0; j--) {
                    var ruleParamResult = JSON.search(rulePrecardResult[0].ObjRuleParams[j], '//*[ParamID = "' + SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text() + '"]');
                    if (ruleParamResult != null && ruleParamResult != undefined && ruleParamResult.length > 0) {
                        if (ruleParamResult[0].ExistParam == '0') {
                            switch (SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text()) {
                                case '0':
                                    document.getElementById('txtNumeric_RulePrecards').value = '';
                                    break;
                                case '1':
                                    SetDuration_TimePicker_RulePrecards('TimeSelector_Hour_RulePrecards', '');
                                    break;
                                case '2':
                                    switch (parent.parent.SysLangID) {
                                        case 'fa-IR':
                                            document.getElementById('pdpDate_RulePrecards').value = '';
                                            break;
                                        case 'en-US':
                                            document.getElementById('gdpDate_RulePrecards_picker').value = '';
                                            break;
                                    }
                                    break;
                            }
                            GridRulePrecards_RulePrecards.beginUpdate();
                            GridRulePrecards_RulePrecards.getSelectedItems()[0].setValue(7, '');
                            GridRulePrecards_RulePrecards.endUpdate();
                            rulePrecardResult[0].ObjRuleParams.splice(j, 1);
                            if (rulePrecardResult[0].ObjRuleParams.length <= 0) {
                                ObjPrecardList_RulePrecards.splice(0);
                            }
                        }
                        else {
                            DialogParameterValue.Close();
                            GetErrorMessage_RulePrecards();
                            return;
                        }
                    }
                }
            }
            break;
        default:
            switch (gridLevel) {
                case '0':
                    for (var i = ObjPrecardList_RulePrecards.length - 1; i >= 0; i--) {
                        var rulePrecardResult = JSON.search(ObjPrecardList_RulePrecards[i], '//*[PrecardID = "' + selectedGridItem_GridRulePrecards_RulePrecards.getMember('PrecardID').get_text() + '" ]');
                        if (rulePrecardResult != null && rulePrecardResult != undefined && rulePrecardResult.length > 0) {
                            if (rulePrecardResult[0].ExistPrecard == '0' && rulePrecardResult[0].Active == '0' && rulePrecardResult[0].ObjRuleParams.length <= 0) {
                                ObjPrecardList_RulePrecards.splice(i, 1);
                            }
                        }
                    }
                    break;
                case '1':
                    for (var i = ObjPrecardList_RulePrecards.length - 1; i >= 0; i--) {
                        var rulePrecardResult = JSON.search(ObjPrecardList_RulePrecards[i], '//*[PrecardID = "' + SelectedGridItem_GridRulePrecards_RulePrecards.getMember('PrecardID').get_text() + '" ]');
                        if (rulePrecardResult != null && rulePrecardResult != undefined && rulePrecardResult.length > 0) {
                            if (rulePrecardResult[0].ExistPrecard == '0') {
                                for (var j = rulePrecardResult[0].ObjRuleParams.length - 1; j >= 0; j--) {
                                    var ruleParamResult = JSON.search(rulePrecardResult[0].ObjRuleParams[j], '//*[ParamID = "' + SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamID').get_text() + '"]');
                                    if (ruleParamResult != null && ruleParamResult != undefined && ruleParamResult.length > 0) {
                                        if (ruleParamResult[0].ExistParam == '0') {
                                            var SelectedGridItem_GridRulePrecards_RulePrecards = GridRulePrecards_RulePrecards.getSelectedItems()[0];
                                            switch (SelectedGridItem_GridRulePrecards_RulePrecards.getMember('ParamType').get_text()) {
                                                case '0':
                                                    document.getElementById('txtNumeric_RulePrecards').value = '';
                                                    break;
                                                case '1':
                                                    SetDuration_TimePicker_RulePrecards('TimeSelector_Hour_RulePrecards', '');
                                                    break;
                                                case '2':
                                                    switch (parent.parent.SysLangID) {
                                                        case 'fa-IR':
                                                            document.getElementById('pdpDate_RulePrecards').value = '';
                                                            break;
                                                        case 'en-US':
                                                            document.getElementById('gdpDate_RulePrecards_picker').value = '';
                                                            break;
                                                    }
                                                    break;
                                            }
                                            GridRulePrecards_RulePrecards.beginUpdate();
                                            GridRulePrecards_RulePrecards.getSelectedItems()[0].setValue(7, '');
                                            GridRulePrecards_RulePrecards.endUpdate();
                                            rulePrecardResult[0].ObjRuleParams.splice(j, 1);
                                            if (rulePrecardResult[0].ObjRuleParams.length <= 0 && ObjPrecardList_RulePrecards[i].Active != 1 && ObjPrecardList_RulePrecards[i].Operator != 1) {
                                                ObjPrecardList_RulePrecards.splice(i, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                DialogParameterValue.Close();
                                GetErrorMessage_RulePrecards();
                                return;
                            }
                        }
                    }
                    break;
            }
            break;
    }
}
function GetScrollTopPosition_RulePrecards() {
    var doc = parent.document.getElementById(parent.ClientPerfixId + 'DialogRulePrecards_IFrame').contentWindow.document.documentElement;
    var body = parent.document.getElementById(parent.ClientPerfixId + 'DialogRulePrecards_IFrame').contentWindow.document.body;
    scrollTopPosition_RulePrecards = ((doc && doc.scrollTop) || (body && body.scrollTop || 0)) - (doc.clientTop || 0);
}

function SetScrollTopPosition_RulePrecards() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogRulePrecards_IFrame').contentWindow.scroll(20000, scrollTopPosition_RulePrecards);
}
function imgbox_RuleDetails_RulePrecards_onClick() {
    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
    var CustomCode = ObjDialogRulePrecards.CustomCode;
    setSlideDownSpeed(200);
    slidedown_showHide('box_R' + CustomCode + 'TagDiv_RulePrecards');
    if (box_RTagDiv_RulePrecards_IsShown) {
        box_RTagDiv_RulePrecards_IsShown = false;
        document.getElementById('imgbox_RuleDetails_RulePrecards').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_RTagDiv_RulePrecards_IsShown = true;
        document.getElementById('imgbox_RuleDetails_RulePrecards').src = 'Images/Ghadir/arrowUp.jpg';
    }
}
function ChangeEnabled_DropDownDive_RulePrecards(state) {
    if (state == 'enabled') {
        document.getElementById('imgbox_RuleDetails_RulePrecards').onclick = function () {
            imgbox_RuleDetails_RulePrecards_onClick();
        };
        document.getElementById('imgbox_RuleDetails_RulePrecards').src = 'Images/Ghadir/arrowDown.jpg';
    }
}
function SetValueToRuleDetail(CustomCode, objRuleDetail_RulePrecards) {
    switch (CustomCode) {
        case '33':
            if (objRuleDetail_RulePrecards.IsForceConfirmByRequestSubstitute == 'true')
                document.getElementById('chbSubstituteConfirmation_RulePrecards').checked = true;
            else
                document.getElementById('chbSubstituteConfirmation_RulePrecards').checked = false;
            break;
    }
}
//function ParameterEditRulePrecards_OnClick() {
//    var ObjDialogRulePrecards = parent.DialogRulePrecards.get_value();
//    var RuleType = ObjDialogRulePrecards.RuleType;
//    var SelectedGridItem_GridRulePrecards_RulePrecards = GridRulePrecards_RulePrecards.getSelectedItems()[0];
//    if (SelectedGridItem_GridRulePrecards_RulePrecards != null && SelectedGridItem_GridRulePrecards_RulePrecards != undefined) {
//        switch (RuleType) {
//            case '1':
//                if (SelectedGridItem_GridRulePrecards_RulePrecards.get_table().get_level() > 0) {
//                    NavligateRuleParameter_RulePrecards(SelectedGridItem_GridRulePrecards_RulePrecards);
//                }
//                break;
//            case '3':
//                NavligateRuleParameter_RulePrecards(SelectedGridItem_GridRulePrecards_RulePrecards);
//                break;
//            default:
//                break;
//        }
//    }
//}