
var CurrentPageState_PersonnelMultiDateFeatures = 'View';
var SelectedMultiDateFeature_PersonnelMultiDateFeatures = null;
var ConfirmState_PersonnelMultiDateFeatures = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures = null;


function GetBoxesHeaders_PersonnelMultiDateFeatures() {
    parent.document.getElementById('Title_DialogPersonnelMultiDateFeatures').innerHTML = document.getElementById('hfTitle_DialogPersonnelMultiDateFeatures').value;
    document.getElementById('header_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures').innerHTML = document.getElementById('hfheader_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures').value;
}

function ViewCurrentLangCalendars_PersonnelMultiDateFeatures() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_PersonnelMultiDateFeatures").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelMultiDateFeatures"));
            document.getElementById("pdpFromDate_PersonnelMultiDateFeaturesimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelMultiDateFeaturesimgbt"));
            document.getElementById("pdpToDate_PersonnelMultiDateFeatures").parentNode.removeChild(document.getElementById("pdpToDate_PersonnelMultiDateFeatures"));
            document.getElementById("pdpToDate_PersonnelMultiDateFeaturesimgbt").parentNode.removeChild(document.getElementById("pdpToDate_PersonnelMultiDateFeaturesimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_PersonnelMultiDateFeatures").removeChild(document.getElementById("Container_gCalFromDate_PersonnelMultiDateFeatures"));
            document.getElementById("Container_ToDateCalendars_PersonnelMultiDateFeatures").removeChild(document.getElementById("Container_gCalToDate_PersonnelMultiDateFeatures"));
            break;
    }
}

function ResetCalendars_PersonnelMultiDateFeatures() {
    var currentDate_PersonnelMultiDateFeatures = document.getElementById('hfCurrentDate_PersonnelMultiDateFeatures').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_PersonnelMultiDateFeatures = new Date(currentDate_PersonnelMultiDateFeatures);
            gdpFromDate_PersonnelMultiDateFeatures.setSelectedDate(currentDate_PersonnelMultiDateFeatures);
            gCalFromDate_PersonnelMultiDateFeatures.setSelectedDate(currentDate_PersonnelMultiDateFeatures);
            gdpToDate_PersonnelMultiDateFeatures.setSelectedDate(currentDate_PersonnelMultiDateFeatures);
            gCalToDate_PersonnelMultiDateFeatures.setSelectedDate(currentDate_PersonnelMultiDateFeatures);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_PersonnelMultiDateFeatures').value = currentDate_PersonnelMultiDateFeatures;
            document.getElementById('pdpToDate_PersonnelMultiDateFeatures').value = currentDate_PersonnelMultiDateFeatures;
            break;
    }
}

function SetActionMode_PersonnelMultiDateFeatures(state) {
    document.getElementById('ActionMode_PersonnelMultiDateFeatures').innerHTML = document.getElementById('hf' + state + '_PersonnelMultiDateFeatures').value;
}

function ChangeCalendarsEnabled_PersonnelMultiDateFeatures(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_PersonnelMultiDateFeatures('pdpFromDate_PersonnelMultiDateFeatures', state);
            ChangeCalendarEnabled_PersonnelMultiDateFeatures('pdpToDate_PersonnelMultiDateFeatures', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_PersonnelMultiDateFeatures('gdpFromDate_PersonnelMultiDateFeatures', state);
            ChangeCalendarEnabled_PersonnelMultiDateFeatures('gdpToDate_PersonnelMultiDateFeatures', state);
            break;
    }
}

function ChangeCalendarEnabled_PersonnelMultiDateFeatures(cal, state) {
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
                        CalendarsViewManage_PersonnelMultiDateFeatures(cal);
                    };
                    break;
            }
            break;
    }
}

function CalendarsViewManage_PersonnelMultiDateFeatures(gdpID) {
    var CalID_PersonnelMultiDateFeatures = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_PersonnelMultiDateFeatures = eval(CalID_PersonnelMultiDateFeatures);
    if (!Cal_PersonnelMultiDateFeatures.get_popUpShowing())
        Cal_PersonnelMultiDateFeatures.show();
    else
        Cal_PersonnelMultiDateFeatures.hide();
}


function Fill_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures() {
    document.getElementById('header_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures').innerHTML = document.getElementById('hfheader_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures').value;
    document.getElementById('loadingPanel_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures').value);
    var Caller = GetCaller_PersonnelMultiDateFeatures();
    var PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.callback(CharToKeyCode_PersonnelMultiDateFeatures(Caller), CharToKeyCode_PersonnelMultiDateFeatures(PersonnelID));
}

function CharToKeyCode_PersonnelMultiDateFeatures(str) {
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

function NavigatePersonnelMultiDateFeature_PersonnelMultiDateFeatures(selectedPersonnelMultiDateFeature) {
    if (selectedPersonnelMultiDateFeature != undefined) {
        var Caller = GetCaller_PersonnelMultiDateFeatures();
        var PersonnelMultiDateFeatureIDDataField = null;
        var PersonnelMultiDateFeatureNameDataField = null;
        switch (Caller) {
            case 'RuleGroups':
                PersonnelMultiDateFeatureIDDataField = 'RuleCategory.ID';
                PersonnelMultiDateFeatureNameDataField = 'RuleCategory.Name';
                break;
            case 'Contracts':
                PersonnelMultiDateFeatureIDDataField = 'Contract.ID';
                PersonnelMultiDateFeatureNameDataField = 'Contract.Title';
                break;
        }
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpFromDate_PersonnelMultiDateFeatures').value = selectedPersonnelMultiDateFeature.getMember('UIFromDate').get_text();
                document.getElementById('pdpToDate_PersonnelMultiDateFeatures').value = selectedPersonnelMultiDateFeature.getMember('UIToDate').get_text();
                break;
            case 'en-US':
                var gFromDate = new Date(selectedPersonnelMultiDateFeature.getMember('UIFromDate').get_text());
                gdpFromDate_PersonnelMultiDateFeatures.setSelectedDate(gFromDate);
                gdpFromDate_PersonnelMultiDateFeatures.setSelectedDate(gFromDate);
                var gToDate = new Date(selectedPersonnelMultiDateFeature.getMember('UIToDate').get_text());
                gdpToDate_PersonnelMultiDateFeatures.setSelectedDate(gToDate);
                gdpToDate_PersonnelMultiDateFeatures.setSelectedDate(gToDate);
                break;
        }
        SelectedMultiDateFeature_PersonnelMultiDateFeatures = new Object();
        SelectedMultiDateFeature_PersonnelMultiDateFeatures.ID = selectedPersonnelMultiDateFeature.getMember(PersonnelMultiDateFeatureIDDataField).get_text();
        document.getElementById('cmbMultiDateFeatures_PersonnelMultiDateFeatures_Input').value = SelectedMultiDateFeature_PersonnelMultiDateFeatures.Name = selectedPersonnelMultiDateFeature.getMember(PersonnelMultiDateFeatureNameDataField).get_text();
    }
}

function ChangePageState_PersonnelMultiDateFeatures(state) {
    SetActionMode_PersonnelMultiDateFeatures(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        CurrentPageState_PersonnelMultiDateFeatures = state;
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelMultiDateFeatures').set_enabled(false);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelMultiDateFeatures').set_imageUrl('add_silver.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelMultiDateFeatures').set_enabled(false);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelMultiDateFeatures').set_imageUrl('edit_silver.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelMultiDateFeatures').set_enabled(false);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelMultiDateFeatures').set_imageUrl('remove_silver.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelMultiDateFeatures').set_enabled(true);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelMultiDateFeatures').set_imageUrl('save.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelMultiDateFeatures').set_enabled(true);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelMultiDateFeatures').set_imageUrl('cancel.png');
        cmbMultiDateFeatures_PersonnelMultiDateFeatures.enable();
        ChangeCalendarsEnabled_PersonnelMultiDateFeatures('enable');
        if (state == 'Edit')
            NavigatePersonnelMultiDateFeature_PersonnelMultiDateFeatures(GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.getSelectedItems()[0]);
        if (state == 'Delete')
            PersonnelMultiDateFeature_onSave();
    }
    if (state == 'View') {
        CurrentPageState_PersonnelMultiDateFeatures = state;
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelMultiDateFeatures').set_enabled(true);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemNew_TlbPersonnelMultiDateFeatures').set_imageUrl('add.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelMultiDateFeatures').set_enabled(true);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemEdit_TlbPersonnelMultiDateFeatures').set_imageUrl('edit.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelMultiDateFeatures').set_enabled(true);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemDelete_TlbPersonnelMultiDateFeatures').set_imageUrl('remove.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelMultiDateFeatures').set_enabled(false);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemSave_TlbPersonnelMultiDateFeatures').set_imageUrl('save_silver.png');
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelMultiDateFeatures').set_enabled(false);
        TlbPersonnelMultiDateFeatures.get_items().getItemById('tlbItemCancel_TlbPersonnelMultiDateFeatures').set_imageUrl('cancel_silver.png');
        ChangeCalendarsEnabled_PersonnelMultiDateFeatures('disable');
        cmbMultiDateFeatures_PersonnelMultiDateFeatures.disable();
    }
}

function PersonnelMultiDateFeature_onSave() {
    if (CurrentPageState_PersonnelMultiDateFeatures != 'Delete')
        UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeatures();
    else
        ShowDialogConfirm('Delete');
}

function UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeatures() {
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures = new Object();
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.Caller = GetCaller_PersonnelMultiDateFeatures();
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ID = '0';
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureID = '0';
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureName = null;
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.PersonnelID = '0';
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.FromDate = null;
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ToDate = null;

    var SelectedItems_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures = GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.getSelectedItems();
    if (SelectedItems_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.length > 0)
        ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ID = SelectedItems_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures[0].getMember("ID").get_text();

    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.PersonnelState = parent.parent.DialogPersonnelMainInformation.get_value().PageState;
    if (CurrentPageState_PersonnelMultiDateFeatures != 'Delete') {
        if (cmbMultiDateFeatures_PersonnelMultiDateFeatures.getSelectedItem() != undefined) {
            ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureID = cmbMultiDateFeatures_PersonnelMultiDateFeatures.getSelectedItem().get_value();
            ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureName = cmbMultiDateFeatures_PersonnelMultiDateFeatures.getSelectedItem().get_text();
        }
        else {
            if (SelectedMultiDateFeature_PersonnelMultiDateFeatures != null) {
                ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureID = SelectedMultiDateFeature_PersonnelMultiDateFeatures.ID;
                ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureName = SelectedMultiDateFeature_PersonnelMultiDateFeatures.Name;
            }
        }

        var FromDate_PersonnelMultiDateFeature = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                FromDate_PersonnelMultiDateFeature = document.getElementById('pdpFromDate_PersonnelMultiDateFeatures').value;
                break;
            case 'en-US':
                FromDate_PersonnelMultiDateFeature = document.getElementById('gdpFromDate_PersonnelMultiDateFeatures_picker').value;
                break;
        }
        ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.FromDate = FromDate_PersonnelMultiDateFeature;

        var ToDate_PersonnelMultiDateFeature = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                ToDate_PersonnelMultiDateFeature = document.getElementById('pdpToDate_PersonnelMultiDateFeatures').value;
                break;
            case 'en-US':
                ToDate_PersonnelMultiDateFeature = document.getElementById('gdpToDate_PersonnelMultiDateFeatures_picker').value;
                break;
        }
        ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ToDate = ToDate_PersonnelMultiDateFeature;

    }
    UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeaturesPage(CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.Caller), CharToKeyCode_PersonnelMultiDateFeatures(CurrentPageState_PersonnelMultiDateFeatures), CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ID), CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.PersonnelID), CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureID), CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureName), CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.FromDate), CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ToDate),CharToKeyCode_PersonnelMultiDateFeatures(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.PersonnelState));
    DialogWaiting.Show();
}

function UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeaturesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_PersonnelMultiDateFeatures();
            PersonnelMultiDateFeatures_OnAfterUpdate(Response);
            ChangePageState_PersonnelMultiDateFeatures('View');
            parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ChangePersonnel_onPersonnelMultiDateFeaturesOperationCompleted(GetCaller_PersonnelMultiDateFeatures(), Response[4], Response[5]);
        }
        else {
            if (CurrentPageState_PersonnelMultiDateFeatures == 'Delete')
                ChangePageState_PersonnelMultiDateFeatures('View');
        }
    }
}

function PersonnelMultiDateFeatures_OnAfterUpdate(Response) {
    if (ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures != null) {
        var PersonnelID = ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.PersonnelID;
        var MultiDateFeatureID = ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureID;
        var MultiDateFeatureName = ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.MultiDateFeatureName;
        var FromDate = ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.FromDate;
        var ToDate = ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ToDate;

        var PersonnelMultiDateFeatureItem = null;
        GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.beginUpdate();
        switch (CurrentPageState_PersonnelMultiDateFeatures) {
            case 'Add':
                PersonnelMultiDateFeatureItem = GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.get_table().addEmptyRow(GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.get_recordCount());
                PersonnelMultiDateFeatureItem.setValue(0, Response[3], false);
                GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.selectByKey(Response[3], 0, false);
                PersonnelMultiDateFeatureItem = GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.selectByKey(ObjPersonnelMultiDateFeature_PersonnelMultiDateFeatures.ID, 0, false);
                GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.deleteSelected();
                break;
        }
        if (CurrentPageState_PersonnelMultiDateFeatures != 'Delete') {
            PersonnelMultiDateFeatureItem.setValue(1, MultiDateFeatureID, false);
            PersonnelMultiDateFeatureItem.setValue(2, MultiDateFeatureName, false);
            PersonnelMultiDateFeatureItem.setValue(3, FromDate, false);
            PersonnelMultiDateFeatureItem.setValue(4, ToDate, false);
        }
        GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.endUpdate();
    }
}


function ShowDialogConfirm(confirmState) {
    ConfirmState_PersonnelMultiDateFeatures = confirmState;
    if (CurrentPageState_PersonnelMultiDateFeatures == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_PersonnelMultiDateFeatures').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelMultiDateFeatures').value;
    DialogConfirm.Show();
}

function ClearList_PersonnelMultiDateFeatures(){
    if (CurrentPageState_PersonnelMultiDateFeatures != 'Edit') {
        document.getElementById('cmbMultiDateFeatures_PersonnelMultiDateFeatures_Input').value = document.getElementById('hfcmbAlarm_PersonnelMultiDateFeatures').value;
        cmbMultiDateFeatures_PersonnelMultiDateFeatures.unSelect();
        SelectedMultiDateFeature_PersonnelMultiDateFeatures = null;
        ResetCalendars_PersonnelMultiDateFeatures();
        GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures.unSelectAll();
    }
}


function tlbItemNew_TlbPersonnelMultiDateFeatures_onClick() {
    ChangePageState_PersonnelMultiDateFeatures('Add');
    ClearList_PersonnelMultiDateFeatures();
}

function tlbItemEdit_TlbPersonnelMultiDateFeatures_onClick() {
    ChangePageState_PersonnelMultiDateFeatures('Edit');
}

function tlbItemDelete_TlbPersonnelMultiDateFeatures_onClick() {
    ChangePageState_PersonnelMultiDateFeatures('Delete');
}

function tlbItemSave_TlbPersonnelMultiDateFeatures_onClick() {
    PersonnelMultiDateFeature_onSave();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PersonnelMultiDateFeatures) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePersonnelMultiDateFeature_PersonnelMultiDateFeatures();
            break;
        case 'Exit':
            ClearList_PersonnelMultiDateFeatures();
            CloseDialogPersonnelMultiDateFeatures();
            break;
    }
}

function CloseDialogPersonnelMultiDateFeatures() {
    parent.document.getElementById('DialogPersonnelMultiDateFeatures_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelMultiDateFeatures.Close();
}


function tlbItemCancel_TlbPersonnelMultiDateFeatures_onClick() {
    DialogConfirm.Close();
    ChangePageState_PersonnelMultiDateFeatures('View');   
}

function tlbItemExit_TlbPersonnelMultiDateFeatures_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbMultiDateFeatures_PersonnelMultiDateFeatures_onExpand(sender, e) {
    if (cmbMultiDateFeatures_PersonnelMultiDateFeatures.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMultiDateFeatures_PersonnelMultiDateFeatures == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMultiDateFeatures_PersonnelMultiDateFeatures = true;
        Fill_cmbMultiDateFeatures_PersonnelMultiDateFeatures();
    }
    if (cmbMultiDateFeatures_PersonnelMultiDateFeatures.getSelectedItem() == undefined && SelectedMultiDateFeature_PersonnelMultiDateFeatures != null)
        document.getElementById('cmbMultiDateFeatures_PersonnelMultiDateFeatures_Input').value = SelectedMultiDateFeature_PersonnelMultiDateFeatures.Name;
}

function Fill_cmbMultiDateFeatures_PersonnelMultiDateFeatures() {
    var Caller = GetCaller_PersonnelMultiDateFeatures();
    ComboBox_onBeforeLoadData('cmbMultiDateFeatures_PersonnelMultiDateFeatures');
    CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures.callback(CharToKeyCode_PersonnelMultiDateFeatures(Caller));
}

function CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onBeforeCallback(sender, e) {
    cmbMultiDateFeatures_PersonnelMultiDateFeatures.dispose();
}

function CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMultiDateFeatures_PersonnelMultiDateFeatures_DropImage').mousedown();
        cmbMultiDateFeatures_PersonnelMultiDateFeatures.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function btn_gdpFromDate_PersonnelMultiDateFeatures_OnMouseUp(event) {
    if (gCalFromDate_PersonnelMultiDateFeatures.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpFromDate_PersonnelMultiDateFeatures_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_PersonnelMultiDateFeatures.getSelectedDate();
    gCalFromDate_PersonnelMultiDateFeatures.setSelectedDate(FromDate);
}

function btn_gdpFromDate_PersonnelMultiDateFeatures_OnClick(event) {
    if (gCalFromDate_PersonnelMultiDateFeatures.get_popUpShowing()) {
        gCalFromDate_PersonnelMultiDateFeatures.hide();
    }
    else {
        gCalFromDate_PersonnelMultiDateFeatures.setSelectedDate(gdpFromDate_PersonnelMultiDateFeatures.getSelectedDate());
        gCalFromDate_PersonnelMultiDateFeatures.show();
    }
}

function gCalFromDate_PersonnelMultiDateFeatures_OnChange(sender, e) {
    var FromDate = gCalFromDate_PersonnelMultiDateFeatures.getSelectedDate();
    gdpFromDate_PersonnelMultiDateFeatures.setSelectedDate(FromDate);
}

function gCalFromDate_PersonnelMultiDateFeatures_onLoad(sender, e) {
    window.gCalFromDate_PersonnelMultiDateFeatures.PopUpObject.z = 25000000;
    
}

function btn_gdpToDate_PersonnelMultiDateFeatures_OnMouseUp(event) {
    if (gCalToDate_PersonnelMultiDateFeatures.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpToDate_PersonnelMultiDateFeatures_OnDateChange(sender, e) {
    var ToDate = gdpToDate_PersonnelMultiDateFeatures.getSelectedDate();
    gCalToDate_PersonnelMultiDateFeatures.setSelectedDate(ToDate);
}

function btn_gdpToDate_PersonnelMultiDateFeatures_OnClick(event) {
    if (gCalToDate_PersonnelMultiDateFeatures.get_popUpShowing()) {
        gCalToDate_PersonnelMultiDateFeatures.hide();
    }
    else {
        gCalToDate_PersonnelMultiDateFeatures.setSelectedDate(gdpToDate_PersonnelMultiDateFeatures.getSelectedDate());
        gCalToDate_PersonnelMultiDateFeatures.show();
    }
}

function gCalToDate_PersonnelMultiDateFeatures_OnChange(sender, e) {
    var ToDate = gCalToDate_PersonnelMultiDateFeatures.getSelectedDate();
    gdpToDate_PersonnelMultiDateFeatures.setSelectedDate(ToDate);
}

function gCalToDate_PersonnelMultiDateFeatures_onLoad(sender, e) {
    window.gCalToDate_PersonnelMultiDateFeatures.PopUpObject.z = 25000000;
}

function Refresh_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures() {
    Fill_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures();
}

function GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures').innerHTML = '';
}

function GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onItemSelect(sender, e) {
    if (CurrentPageState_PersonnelMultiDateFeatures != 'Add')
        NavigatePersonnelMultiDateFeature_PersonnelMultiDateFeatures(e.get_item());
}

function CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures').value;
    if (error != "") {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        if (erroParts[3] == 'Reload')
            Refresh_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures();
    }
}

function CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackError() {
    ShowConnectionError_hfErrorType_PersonnelMultiDateFeatures();
}

function CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures').innerHTML = '';
    ShowConnectionError_hfErrorType_PersonnelMultiDateFeatures();
}

function ShowConnectionError_hfErrorType_PersonnelMultiDateFeatures() {
    var error = document.getElementById('hfErrorType_PersonnelMultiDateFeatures').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelMultiDateFeatures').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemHelp_TlbPersonnelMultiDateFeatures_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRulesGroup');
}

function tlbItemFormReconstruction_TlbPersonnelMultiDateFeatures_onClick() {
    var Caller = GetCaller_PersonnelMultiDateFeatures();
    CloseDialogPersonnelMultiDateFeatures();
    parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ShowDialogPersonnelMultiDateFeatures(Caller);    
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
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_PersonnelMultiDateFeatures('View');
}

function GetCaller_PersonnelMultiDateFeatures() {
    var ObjDialogPersonnelMultiDateFeatures = parent.DialogPersonnelMultiDateFeatures.get_value();
    var Caller = ObjDialogPersonnelMultiDateFeatures.Caller;
    return Caller;
}

function tlbItemClear_TlbClear_ToDateCalendars_PersonnelMultiDateFeatures_onClick() {
    var Caller = GetCaller_PersonnelMultiDateFeatures();
    if (Caller == 'Contracts') {
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpToDate_PersonnelMultiDateFeatures').value = "";
                break;
            case 'en-US':
                document.getElementById('gdpToDate_PersonnelMultiDateFeatures_picker').value = "";
                break;
        }
    }
}
