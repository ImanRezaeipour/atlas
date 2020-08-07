﻿var ObjDialogUiValidationRules;
var ObjDateRangeParametersValue_RuleParameters = '';
var selectedDataRangeID_RuleParameters = '-1';
var ObjRuleParameter_RuleParameters = null;
var CurrentPageState_RuleParameters = 'View';
var CurrentPageState_Rule = 'View';
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadStateGridUiValidationRules_UiValidationRules = 'Normal';

function CallBack_GridUiValidationRules_UiValidationRules_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UiValidationRules').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridUiValidationRules_UiValidationRules();
    }
}
function ShowConnectionError_UiValidationRules() {
    var error = document.getElementById('hfErrorType_UiValidationRules').value;
    var errorBody = document.getElementById('hfConnectionError_UiValidationRules').value;
    showDialog(error, errorBody, 'error');
}
function CallBack_GridUiValidationRules_UiValidationRules_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridUiValidationRules_UiValidationRules').innerHTML = '';
    ShowConnectionError_UiValidation();
}
function GetBoxesHeaders_UiValidationRules() {
    parent.document.getElementById('Title_DialogUiValidationRules').innerHTML = document.getElementById('hfTitle_DialogUiValidationRules').value;
    document.getElementById('header_UiValidationRules_UiValidationRules').innerHTML = document.getElementById('hfheader_UiValidationRules_UiValidationRules').value;
    document.getElementById('header_ParameterUiValidationRules_UiValidationRules').innerHTML = document.getElementById('hfheader_ParameterUiValidationRules_UiValidationRules').value;
    document.getElementById('cmbRuleGroup_UiValidationRules_TextBox').innerHTML = document.getElementById('hfcmbAlarm_UiValidationRules').value;
}
function SetActionMode_UiValidationRules(state) {
    document.getElementById('ActionMode_UiValidationRules').innerHTML = document.getElementById("hf" + state + "_UiValidationRules").value;
}
function Refresh_GridUiValidationRules_UiValidationRules() {
    Fill_GridUiValidationRules_UiValidationRules();
}
function GridUiValidationRules_UiValidationRules_onItemSelect(sender, e) {
    NavigateUiValidation_UiValidation(e.get_item());
    CurrentPageState_RuleParameters = 'View';
    ChangePageState_RuleParameters();
    ClearList_RuleParameters();

}
function GridUiValidationRules_UiValidationRules_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUiValidationRules_UiValidationRules').innerHTML = '';
}

function NavigateUiValidation_UiValidation(selectedGridUiValidationRules) {
    var selectedID = selectedGridUiValidationRules.getMember('ID').get_text();
    var selectedRuleID = selectedGridUiValidationRules.getMember('RuleID').get_text();
    Fill_GridParameterUiValidationRules_UiValidationRules(selectedID, selectedRuleID);
}


function CallBack_GridParameterUiValidationRules_UiValidationRules_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridParameterUiValidationRules_UiValidationRules').innerHTML = '';
    ShowConnectionError_UiValidationRules();
}
//function Refresh_GridParameterUiValidationRules_UiValidationRules() {
//    Fill_GridParameterUiValidationRules_UiValidationRules();
//}
function GridParameterUiValidationRules_UiValidationRules_onItemSelect(sender, e) {
    NavligateRuleParameter_RuleParameters(e.get_item());
}
function GridParameterUiValidationRules_UiValidationRules_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridParameterUiValidationRules_UiValidationRules').innerHTML = '';
}
function tlbItemExit_TlbUiValidationRules_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_UiValidationRules').value;
    DialogConfirm.Show();

}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}
function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDilaogUiValidationRules();
}
function CloseDilaogUiValidationRules() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogUiValidationRules_IFrame').src =parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogUiValidationRules').Close();
}
function tlbItemFormReconstruction_TlbUiValidationRules_onClick() {
    CloseDilaogUiValidationRules();
    var ObjDialogUiValidationRules = parent.DialogUiValidationRules.get_value();
    parent.document.getElementById('pgvUiValidation_iFrame').contentWindow.ShowDialogUiValidationRules(ObjDialogUiValidationRules.State);
}


function ViewCurrentLangCalendars_RuleParameters() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpDate_RuleParameters"));
            document.getElementById("pdpDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpDate_RuleParametersimgbt"));
            document.getElementById("pdpFromDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpFromDate_RuleParameters"));
            document.getElementById("pdpFromDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_RuleParametersimgbt"));
            document.getElementById("pdpToDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpToDate_RuleParameters"));
            document.getElementById("pdpToDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpToDate_RuleParametersimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalDate_RuleParameters"));
            document.getElementById("Container_FromDateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalFromDate_RuleParameters"));
            document.getElementById("Container_ToDateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalToDate_RuleParameters"));
            break;
    }
}
function Fill_ObjectUiValidationRules_UiValidationRules() {
    ObjDialogUiValidationRules = parent.DialogUiValidationRules.get_value();
    document.getElementById("txtNameUiValidationRules_UiValidationRules").value = ObjDialogUiValidationRules.Name;
}
function Fill_GridUiValidationRules_UiValidationRules() {
    var ruleGroupID = '0';
    switch (LoadStateGridUiValidationRules_UiValidationRules) {
        case 'Normal':

            break;

        case 'GroupRule':
            if (cmbRuleGroup_UiValidationRules.getSelectedItem() != null && cmbRuleGroup_UiValidationRules.getSelectedItem() != undefined) {
                ruleGroupID = cmbRuleGroup_UiValidationRules.getSelectedItem().get_value();
            }
            break;
        default:
            break;
    }
    
    document.getElementById('loadingPanel_GridUiValidationRules_UiValidationRules').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridUiValidationRules_UiValidationRules').value);
    CallBack_GridUiValidationRules_UiValidationRules.callback(CharToKeyCode_UiValidationRules(ObjDialogUiValidationRules.GroupID), CharToKeyCode_UiValidationRules(ruleGroupID));
}
function CharToKeyCode_UiValidationRules(str) {
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
function NavigateUiValidationRules_UiValidationRules(selectedUiValidationRulesItem) {
    if (selectedUiValidationItem != undefined) {
        var selectedRuleID = selectedUiValidationRulesItem.getMember('ID').get_text();
        Fill_GridParameterUiValidationRules_UiValidationRules(selectedRuleID);

    }
}
function Fill_GridParameterUiValidationRules_UiValidationRules(ID, RuleID) {
    var selectedID = ID;
    var selectedRuleID = RuleID;
    document.getElementById('loadingPanel_GridParameterUiValidationRules_UiValidationRules').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridParameterUiValidationRules_UiValidationRules').value);
    CallBack_GridParameterUiValidationRules_UiValidationRules.callback(CharToKeyCode_UiValidationRules(ID), CharToKeyCode_UiValidationRules(selectedRuleID));
}
function CallBack_GridParameterUiValidationRules_UiValidationRules_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ParameterUiValidationRules').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridParameterUiValidationRules_UiValidationRules();
    }
}
function NavligateRuleParameter_RuleParameters(selectedRuleParameter) {
    if (selectedRuleParameter != undefined) {
        CurrentPageState_RuleParameters = 'Edit';
        ChangePageState_RuleParameters();
        var ruleParameterValue = selectedRuleParameter.getMember('TheValue').get_text();
        switch (selectedRuleParameter.getMember('Type').get_text()) {
            case '0':
                TabStripRuleParametersTerms.selectTabById('tbNumeric_TabStripRuleParametersTerms');
                document.getElementById('txtNumeric_RuleParameters').value = ruleParameterValue;
                break;
            case '1':
                TabStripRuleParametersTerms.selectTabById('tbTime_TabStripRuleParametersTerms');
                if (ruleParameterValue != '') {
                    SetDuration_TimePicker_RuleParameters('TimeSelector_Hour_RuleParameters', ruleParameterValue);
                    document.getElementById('chbNextDay_RuleParameters').checked = selectedRuleParameter.getMember('ContinueOnTomorrow').get_value();
                }
                break;
            case '2':
                TabStripRuleParametersTerms.selectTabById('tbDate_TabStripRuleParametersTerms');
                if (ruleParameterValue != '') {
                    switch (parent.parent.SysLangID) {
                        case 'fa-IR':
                            document.getElementById('pdpDate_RuleParameters').value = ruleParameterValue;
                            break;
                        case 'en-US':
                            ruleparametervalue = new date(ruleparametervalue);
                            gCalDate_RuleParameters.setSelectedDate(ruleParameterValue);
                            gdpDate_RuleParameters.setSelectedDate(ruleParameterValue);
                            break;
                    }
                }
                break;
        }
    }
}
function SetDuration_TimePicker_RuleParameters(TimePicker, strTime) {
    var arTime_RuleParameter = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_RuleParameter[i].length < 2)
            arTime_RuleParameter[i] = '0' + arTime_RuleParameter[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_RuleParameter[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_RuleParameter[1];
}
function gdpDate_RuleParameters_OnDateChange(sender, e) {
    var Date = gdpDate_RuleParameters.getSelectedDate();
    gCalDate_RuleParameters.setSelectedDate(Date);
}
function gCalDate_RuleParameters_OnChange(sender, e) {
    var Date = gCalDate_RuleParameters.getSelectedDate();
    gdpDate_RuleParameters.setSelectedDate(Date);
}
function gCalDate_RuleParameters_onLoad(sender, e) {
    window.gCalDate_RuleParameters.PopUpObject.z = 25000000;
}
function btn_gdpDate_RuleParameters_OnClick(event) {
    if (gCalDate_RuleParameters.get_popUpShowing()) {
        gCalDate_RuleParameters.hide();
    }
    else {
        gCalDate_RuleParameters.setSelectedDate(gdpDate_RuleParameters.getSelectedDate());
        gCalDate_RuleParameters.show();
    }
}

function ChangePageState_RuleParameters() {
    state = CurrentPageState_RuleParameters;
    if (state == 'Edit') {
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemSave_TlbParameterUiValidationRules').set_enabled(true);
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemSave_TlbParameterUiValidationRules').set_imageUrl('save.png');
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemCancel_TlbParameterUiValidationRules').set_enabled(true);
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemCancel_TlbParameterUiValidationRules').set_imageUrl('cancel.png');
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_enabled(true);
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_imageUrl('save.png');
        document.getElementById('txtNumeric_RuleParameters').disabled = '';
        ChangeCalendarsEnabled_RuleParameters('enable');
        ChangeTimePickerEnabled_RuleParameters('TimeSelector_Hour_RuleParameters', 'enable');

    }
    if (state == 'View') {
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemSave_TlbParameterUiValidationRules').set_enabled(false);
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemSave_TlbParameterUiValidationRules').set_imageUrl('save_silver.png');
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemCancel_TlbParameterUiValidationRules').set_enabled(false);
        TlbParameterUiValidationRules.get_items().getItemById('tlbItemCancel_TlbParameterUiValidationRules').set_imageUrl('cancel_silver.png');
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_enabled(false);
        TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms').set_imageUrl('save_silver.png');
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_enabled(false);
        TlbConfirm_pgvTime_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms').set_imageUrl('save_silver.png');
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_enabled(false);
        TlbConfirm_pgvDate_MultiPageRuleParametersTerms.get_items().getItemById('tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms').set_imageUrl('save_silver.png');
        document.getElementById('txtNumeric_RuleParameters').disabled = 'disabled';
        ChangeCalendarsEnabled_RuleParameters('disable');
        ChangeTimePickerEnabled_RuleParameters('TimeSelector_Hour_RuleParameters', 'disable');
    }
}
function ChangeTimePickerEnabled_RuleParameters(TimeSelector, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            break;
        case 'enable':
            disabled = '';
            break;
    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
}
function ChangeCalendarsEnabled_RuleParameters(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_RuleParameters('pdpDate_RuleParameters', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_RuleParameters('gdpDate_RuleParameters', state);
            break;
    }
}

function ChangeCalendarEnabled_RuleParameters(cal, state) {
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
                        CalendarsViewManage_RuleParameters(cal);
                    };
                    break;
            }
            break;
    }
}
function ClearList_RuleParameters() {
    ResetCalendars_RuleParameters();
    ResetTimepicker_RuleParameters('TimeSelector_Hour_RuleParameters');
    document.getElementById('txtNumeric_RuleParameters').value = '';

}
function ResetCalendars_RuleParameters() {
    var currentDate_RuleParameters = document.getElementById('hfCurrentDate_RuleParameters').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_RuleParameters = new Date(currentDate_RuleParameters);
            gdpDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            gCalDate_RuleParameters.setSelectedDate(currentDate_RuleParameters);
            break;
        case 'fa-IR':
            document.getElementById('pdpDate_RuleParameters').value = currentDate_RuleParameters;
            break;
    }
}
function ResetTimepicker_RuleParameters(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
}

function btn_gdpDate_RuleParameters_OnClick(event) {
    if (gCalDate_RuleParameters.get_popUpShowing()) {
        gCalDate_RuleParameters.hide();
    }
    else {
        gCalDate_RuleParameters.setSelectedDate(gdpDate_RuleParameters.getSelectedDate());
        gCalDate_RuleParameters.show();
    }
}
function btn_gdpDate_RuleParameters_OnMouseUp(event) {
    if (gCalDate_RuleParameters.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}


function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond" && !document.getElementById("" + TimeSelector + "_txtHour").disabled)
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function ViewCurrentLangCalendars_RuleParameters() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_RuleParameters").parentNode.removeChild(document.getElementById("pdpDate_RuleParameters"));
            document.getElementById("pdpDate_RuleParametersimgbt").parentNode.removeChild(document.getElementById("pdpDate_RuleParametersimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RuleParameters").removeChild(document.getElementById("Container_gCalDate_RuleParameters"));
            break;
    }
}
function GetDuration_TimePicker_RuleParameters(TimePicker) {
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
function tlbItemCancel_TlbUiValidationRules_onClick() {
    CurrentPageState_RuleParameters = 'View';
    ChangePageState_RuleParameters();
    ClearList_RuleParameters();
    GridParameterUiValidationRules_UiValidationRules.unSelectAll();
}


function UpdateUiValidationRules_UiValidationRules() {
    CurrentPageState_Rule = 'Edit';

    var gridItem;
    var itemIndex = 0;
    var strListRowValue = '';
    strListRowValue += '[';
    while (gridItem = GridUiValidationRules_UiValidationRules.get_table().getRow(itemIndex)) {
        strListRowValue += '{"uvID":"' + gridItem.getMember("ID").get_text() + '","IsA":"' + gridItem.getMember("Active").get_text() + '","IsO":"' + gridItem.getMember("OpratorRestriction").get_text() + '","GID":"' + ObjDialogUiValidationRules.GroupID + '","RID":"' + gridItem.getMember("RuleID").get_text() + '"},';
        itemIndex++;
    }
    strListRowValue = strListRowValue.substring(0, strListRowValue.length - 1);
    strListRowValue += ']';

    UpdateUiValidationRules_UiValidationRulesPage(CharToKeyCode_UiValidationRules(strListRowValue), CharToKeyCode_UiValidationRules(CurrentPageState_Rule));
    DialogWaiting.Show();
}
function UpdateUiValidationRules_UiValidationRulesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UiValidation').value;
            Response[1] = document.getElementById('hfConnectionError_UiValidation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            CurrentPageState_RuleParameters = 'View';
            CurrentPageState_Rule = 'View';
            ChangePageState_RuleParameters();
        }
    }
}

function tlbItemSave_TlbUiValidationRules_onClick() {
    UpdateUiValidationRules_UiValidationRules();

}
function tlbItemSave_TlbParameterUiValidationRules_onClick() {
    UpdateParameterUiValidationRules_UiValidationRules();
}

function UpdateParameterUiValidationRules_UiValidationRules() {

    var gridItem;
    var itemIndex = 0;
    var strListRowValue = '';
    strListRowValue += '[';
    while (gridItem = GridParameterUiValidationRules_UiValidationRules.get_table().getRow(itemIndex)) {
        if (GridParameterUiValidationRules_UiValidationRules.getSelectedItems().length > 0 && gridItem.getMember("TheValue").get_text() == '' && gridItem.getMember("Type").get_text() == '2') {
            var DateValue_RuleParametes = null;
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    DateValue_RuleParametes = document.getElementById('pdpDate_RuleParameters').value;
                    break;
                case 'en-US':
                    DateValue_RuleParametes = document.getElementById('gdpDate_RuleParameters_picker').value;
                    break;
            }
            UpadateGridParameterUiValidationRules_UiValidationRules(DateValue_RuleParametes);
            gridItem = GridParameterUiValidationRules_UiValidationRules.get_table().getRow(itemIndex)
        }
        strListRowValue += '{"PID":"' + gridItem.getMember("ID").get_text() + '","Name":"' + gridItem.getMember("Name").get_text() + '","Val":"' + gridItem.getMember("TheValue").get_text() + '","Type":"' + gridItem.getMember("Type").get_text() + '","KN":"' + gridItem.getMember("KeyName").get_text() + '"},';
        itemIndex++;
    }
    strListRowValue = strListRowValue.substring(0, strListRowValue.length - 1);
    strListRowValue += ']';

    UpdateParameterUiValidationRules_UiValidationRulesPage(CharToKeyCode_UiValidationRules(strListRowValue), CharToKeyCode_UiValidationRules(CurrentPageState_RuleParameters));
    DialogWaiting.Show();
}
function UpdateParameterUiValidationRules_UiValidationRulesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UiValidation').value;
            Response[1] = document.getElementById('hfConnectionError_UiValidation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_RuleParameters();
            CurrentPageState_RuleParameters = 'View';
            CurrentPageState_Rule = 'View';
            ChangePageState_RuleParameters();
            GridParameterUiValidationRules_UiValidationRules.unSelectAll();

        }

    }
}

function tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms_onClick() {
    if (GridParameterUiValidationRules_UiValidationRules.getSelectedItems().length > 0) {
        var NumericValue_RuleParameters = document.getElementById('txtNumeric_RuleParameters').value;
        if (!isNaN(parseInt(NumericValue_RuleParameters)))
            UpadateGridParameterUiValidationRules_UiValidationRules(NumericValue_RuleParameters);
    }
}

function tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms_onClick() {
    if (GridParameterUiValidationRules_UiValidationRules.getSelectedItems().length > 0) {
        var TimeValue_RuleParametes = GetDuration_TimePicker_RuleParameters('TimeSelector_Hour_RuleParameters');
        if (document.getElementById('chbNextDay_RuleParameters').checked)
            TimeValue_RuleParametes = '+' + TimeValue_RuleParametes;
        UpadateGridParameterUiValidationRules_UiValidationRules(TimeValue_RuleParametes);
    }
}
function tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms_onClick() {
    if (GridParameterUiValidationRules_UiValidationRules.getSelectedItems().length > 0) {
        var DateValue_RuleParametes = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                DateValue_RuleParametes = document.getElementById('pdpDate_RuleParameters').value;
                break;
            case 'en-US':
                DateValue_RuleParametes = document.getElementById('gdpDate_RuleParameters_picker').value;
                break;
        }
        UpadateGridParameterUiValidationRules_UiValidationRules(DateValue_RuleParametes);
    }
}

function UpadateGridParameterUiValidationRules_UiValidationRules(ruleParameterValue) {
    GridParameterUiValidationRules_UiValidationRules.beginUpdate();
    GridParameterUiValidationRules_UiValidationRules.getSelectedItems()[0].setValue(4, ruleParameterValue, false);
    GridParameterUiValidationRules_UiValidationRules.endUpdate();
}

function TimeSelector_Hour_RuleParameters_onChange(partID) {
    var id = 'TimeSelector_Hour_RuleParameters_' + partID;
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

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function CallBack_cmbRuleGroup_UiValidationRules_onBeforeCallback() {
    cmbRuleGroup_UiValidationRules.dispose();
}

function CallBack_cmbRuleGroup_UiValidationRules_onCallbackComplete() {
    var error = document.getElementById('ErrorHiddenField_RuleGroup').value;
    if (error == "") {
        document.getElementById('cmbRuleGroup_UiValidationRules_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRuleGroup_UiValidationRules_DropImage').mousedown();
        cmbRuleGroup_UiValidationRules.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleGroup_UiValidationRules_DropDown').style.display = 'none';
    }
}

function CallBack_cmbRuleGroup_UiValidationRules_onCallbackError() {
    ShowConnectionError_UiValidationRules();
}


function cmbRuleGroup_UiValidationRules_onExpand() {
    if (cmbRuleGroup_UiValidationRules.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroup_UiValidationRules == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroup_UiValidationRules = true;
        Fill_cmbRuleGroup_UiValidationRules();
    }
}
function Fill_cmbRuleGroup_UiValidationRules() {
    ComboBox_onBeforeLoadData('cmbRuleGroup_UiValidationRules');
    CallBack_cmbRuleGroup_UiValidationRules.callback();
}

function cmbRuleGroup_UiValidationRules_onCollapse() {
    if (cmbRuleGroup_UiValidationRules.getSelectedItem()!=null && cmbRuleGroup_UiValidationRules.getSelectedItem() == undefined) {
        document.getElementById('cmbRuleGroup_OrganizationWorkFlow_Input').value = document.getElementById('hfcmbAlarm_UiValidationRules').value;
    }
}

function cmbRuleGroup_UiValidationRules_onChange() {
    LoadStateGridUiValidationRules_UiValidationRules = 'GroupRule';
    Fill_GridUiValidationRules_UiValidationRules();
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
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}
function tlbItemHelp_TlbUiValidationRules_onClick() {
    LoadHelpPage('tlbItemHelp_TlbUiValidationRules');
}