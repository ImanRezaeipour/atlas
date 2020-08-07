
var CurrentPageState_PersonnelEmploymentTypes = 'View';
var SelectedEmploymentType_PersonnelEmploymentTypes = null;
var ConfirmState_PersonnelEmploymentTypes = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjPersonnelEmploymentType_PersonnelEmploymentTypes = null;


function GetBoxesHeaders_PersonnelEmploymentTypes() {
    parent.document.getElementById('Title_DialogPersonnelEmployTypes').innerHTML = document.getElementById('hfTitle_DialogPersonnelEmploymentTypes').value;
    document.getElementById('header_PersonnelEmploymentTypes_PersonnelEmploymentTypes').innerHTML = document.getElementById('hfheader_PersonnelEmploymentTypes_PersonnelEmploymentTypes').value;
}

function ViewCurrentLangCalendars_PersonnelEmploymentTypes() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_PersonnelEmploymentTypes").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelEmploymentTypes"));
            document.getElementById("pdpFromDate_PersonnelEmploymentTypesimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_PersonnelEmploymentTypesimgbt"));
            document.getElementById("pdpToDate_PersonnelEmploymentTypes").parentNode.removeChild(document.getElementById("pdpToDate_PersonnelEmploymentTypes"));
            document.getElementById("pdpToDate_PersonnelEmploymentTypesimgbt").parentNode.removeChild(document.getElementById("pdpToDate_PersonnelEmploymentTypesimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_PersonnelEmploymentTypes").removeChild(document.getElementById("Container_gCalFromDate_PersonnelEmploymentTypes"));
            document.getElementById("Container_ToDateCalendars_PersonnelEmploymentTypes").removeChild(document.getElementById("Container_gCalToDate_PersonnelEmploymentTypes"));
            break;
    }
}

function ResetCalendars_PersonnelEmploymentTypes() {
    var currentDate_PersonnelEmploymentTypes = document.getElementById('hfCurrentDate_PersonnelEmploymentTypes').value;
    switch (parent.parent.SysLangID) {
        case 'en-US':
            currentDate_PersonnelEmploymentTypes = new Date(currentDate_PersonnelEmploymentTypes);
            gdpFromDate_PersonnelEmploymentTypes.setSelectedDate(currentDate_PersonnelEmploymentTypes);
            gCalFromDate_PersonnelEmploymentTypes.setSelectedDate(currentDate_PersonnelEmploymentTypes);
            gdpToDate_PersonnelEmploymentTypes.setSelectedDate(currentDate_PersonnelEmploymentTypes);
            gCalToDate_PersonnelEmploymentTypes.setSelectedDate(currentDate_PersonnelEmploymentTypes);
            break;
        case 'fa-IR':
            document.getElementById('pdpFromDate_PersonnelEmploymentTypes').value = currentDate_PersonnelEmploymentTypes;
            document.getElementById('pdpToDate_PersonnelEmploymentTypes').value = currentDate_PersonnelEmploymentTypes;
            break;
    }
}

function SetActionMode_PersonnelEmploymentTypes(state) {
    document.getElementById('ActionMode_PersonnelEmploymentTypes').innerHTML = document.getElementById('hf' + state + '_PersonnelEmploymentTypes').value;
}

function ChangeCalendarsEnabled_PersonnelEmploymentTypes(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_PersonnelEmploymentTypes('pdpFromDate_PersonnelEmploymentTypes', state);
            ChangeCalendarEnabled_PersonnelEmploymentTypes('pdpToDate_PersonnelEmploymentTypes', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_PersonnelEmploymentTypes('gdpFromDate_PersonnelEmploymentTypes', state);
            ChangeCalendarEnabled_PersonnelEmploymentTypes('gdpToDate_PersonnelEmploymentTypes', state);
            break;
    }
}

function ChangeCalendarEnabled_PersonnelEmploymentTypes(cal, state) {
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
                        CalendarsViewManage_PersonnelEmploymentTypes(cal);
                    };
                    break;
            }
            break;
    }
}

function CalendarsViewManage_PersonnelEmploymentTypes(gdpID) {
    var CalID_PersonnelEmploymentTypes = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_PersonnelEmploymentTypes = eval(CalID_PersonnelEmploymentTypes);
    if (!Cal_PersonnelEmploymentTypes.get_popUpShowing())
        Cal_PersonnelEmploymentTypes.show();
    else
        Cal_PersonnelEmploymentTypes.hide();
}


function Fill_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes() {
    document.getElementById('header_PersonnelEmploymentTypes_PersonnelEmploymentTypes').innerHTML = document.getElementById('hfheader_PersonnelEmploymentTypes_PersonnelEmploymentTypes').value;
    document.getElementById('loadingPanel_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes').value);
    var PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    CallBack_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.callback(CharToKeyCode_PersonnelEmploymentTypes(PersonnelID));
}

function CharToKeyCode_PersonnelEmploymentTypes(str) {
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

function NavigatePersonnelEmploymentType_PersonnelEmploymentTypes(selectedPersonnelEmploymentType) {
    if (selectedPersonnelEmploymentType != undefined) {
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpFromDate_PersonnelEmploymentTypes').value = selectedPersonnelEmploymentType.getMember('UIFromDate').get_text();
                document.getElementById('pdpToDate_PersonnelEmploymentTypes').value = selectedPersonnelEmploymentType.getMember('UIToDate').get_text();
                break;
            case 'en-US':
                var gFromDate = new Date(selectedPersonnelEmploymentType.getMember('UIFromDate').get_text());
                gdpFromDate_PersonnelEmploymentTypes.setSelectedDate(gFromDate);
                gdpFromDate_PersonnelEmploymentTypes.setSelectedDate(gFromDate);
                var gToDate = new Date(selectedPersonnelEmploymentType.getMember('UIToDate').get_text());
                gdpToDate_PersonnelEmploymentTypes.setSelectedDate(gToDate);
                gdpToDate_PersonnelEmploymentTypes.setSelectedDate(gToDate);
                break;
        }
        SelectedEmploymentType_PersonnelEmploymentTypes = new Object();
        SelectedEmploymentType_PersonnelEmploymentTypes.ID = selectedPersonnelEmploymentType.getMember('EmploymentType.ID').get_text();
        document.getElementById('cmbEmploymentTypes_PersonnelEmploymentTypes_Input').value = SelectedEmploymentType_PersonnelEmploymentTypes.Name = selectedPersonnelEmploymentType.getMember('EmploymentType.Name').get_text();
        document.getElementById('txtEmploymentTypeNumber_PersonnelEmploymentTypes').value = selectedPersonnelEmploymentType.getMember('EmploymentNumber').get_text();
    }
}

function ChangePageState_PersonnelEmploymentTypes(state) {
    SetActionMode_PersonnelEmploymentTypes(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        CurrentPageState_PersonnelEmploymentTypes = state;
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemNew_TlbPersonnelEmploymentTypes').set_enabled(false);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemNew_TlbPersonnelEmploymentTypes').set_imageUrl('add_silver.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemEdit_TlbPersonnelEmploymentTypes').set_enabled(false);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemEdit_TlbPersonnelEmploymentTypes').set_imageUrl('edit_silver.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemDelete_TlbPersonnelEmploymentTypes').set_enabled(false);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemDelete_TlbPersonnelEmploymentTypes').set_imageUrl('remove_silver.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemSave_TlbPersonnelEmploymentTypes').set_enabled(true);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemSave_TlbPersonnelEmploymentTypes').set_imageUrl('save.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemCancel_TlbPersonnelEmploymentTypes').set_enabled(true);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemCancel_TlbPersonnelEmploymentTypes').set_imageUrl('cancel.png');
        cmbEmploymentTypes_PersonnelEmploymentTypes.enable();
        document.getElementById('txtEmploymentTypeNumber_PersonnelEmploymentTypes').disabled = '';
        ChangeCalendarsEnabled_PersonnelEmploymentTypes('enable');
        if (state == 'Edit')
            NavigatePersonnelEmploymentType_PersonnelEmploymentTypes(GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.getSelectedItems()[0]);
        if (state == 'Delete')
            PersonnelEmploymentType_onSave();
    }
    if (state == 'View') {
        CurrentPageState_PersonnelEmploymentTypes = state;
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemNew_TlbPersonnelEmploymentTypes').set_enabled(true);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemNew_TlbPersonnelEmploymentTypes').set_imageUrl('add.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemEdit_TlbPersonnelEmploymentTypes').set_enabled(true);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemEdit_TlbPersonnelEmploymentTypes').set_imageUrl('edit.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemDelete_TlbPersonnelEmploymentTypes').set_enabled(true);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemDelete_TlbPersonnelEmploymentTypes').set_imageUrl('remove.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemSave_TlbPersonnelEmploymentTypes').set_enabled(false);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemSave_TlbPersonnelEmploymentTypes').set_imageUrl('save_silver.png');
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemCancel_TlbPersonnelEmploymentTypes').set_enabled(false);
        TlbPersonnelEmploymentTypes.get_items().getItemById('tlbItemCancel_TlbPersonnelEmploymentTypes').set_imageUrl('cancel_silver.png');
        ChangeCalendarsEnabled_PersonnelEmploymentTypes('disable');
        cmbEmploymentTypes_PersonnelEmploymentTypes.disable();
        document.getElementById('txtEmploymentTypeNumber_PersonnelEmploymentTypes').disabled = 'disabled';
    }
}

function PersonnelEmploymentType_onSave() {
    if (CurrentPageState_PersonnelEmploymentTypes != 'Delete')
        UpdatePersonnelEmploymentType_PersonnelEmploymentTypes();
    else
        ShowDialogConfirm('Delete');
}

function UpdatePersonnelEmploymentType_PersonnelEmploymentTypes() {
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes = new Object();
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ID = '0';
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeID = '0';
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeName = null;
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.PersonnelID = '0';
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.FromDate = null;
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ToDate = null;
    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentNumber = null;
    var SelectedItems_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes = GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.getSelectedItems();
    if (SelectedItems_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.length > 0)
        ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ID = SelectedItems_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes[0].getMember("ID").get_text();

    ObjPersonnelEmploymentType_PersonnelEmploymentTypes.PersonnelID = parent.parent.DialogPersonnelMainInformation.get_value().PersonnelID;
    if (CurrentPageState_PersonnelEmploymentTypes != 'Delete') {
        if (cmbEmploymentTypes_PersonnelEmploymentTypes.getSelectedItem() != undefined) {
            ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeID = cmbEmploymentTypes_PersonnelEmploymentTypes.getSelectedItem().get_value();
            ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeName = cmbEmploymentTypes_PersonnelEmploymentTypes.getSelectedItem().get_text();
        }
        else {
            if (SelectedEmploymentType_PersonnelEmploymentTypes != null) {
                ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeID = SelectedEmploymentType_PersonnelEmploymentTypes.ID;
                ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeName = SelectedEmploymentType_PersonnelEmploymentTypes.Name;
            }
        }

        var FromDate_PersonnelEmploymentType = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                FromDate_PersonnelEmploymentType = document.getElementById('pdpFromDate_PersonnelEmploymentTypes').value;
                break;
            case 'en-US':
                FromDate_PersonnelEmploymentType = document.getElementById('gdpFromDate_PersonnelEmploymentTypes_picker').value;
                break;
        }
        ObjPersonnelEmploymentType_PersonnelEmploymentTypes.FromDate = FromDate_PersonnelEmploymentType;

        var ToDate_PersonnelEmploymentType = null;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                ToDate_PersonnelEmploymentType = document.getElementById('pdpToDate_PersonnelEmploymentTypes').value;
                break;
            case 'en-US':
                ToDate_PersonnelEmploymentType = document.getElementById('gdpToDate_PersonnelEmploymentTypes_picker').value;
                break;
        }
        ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ToDate = ToDate_PersonnelEmploymentType;
        ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentNumber = document.getElementById('txtEmploymentTypeNumber_PersonnelEmploymentTypes').value;
    }
    UpdatePersonnelEmploymentType_PersonnelEmploymentTypesPage(CharToKeyCode_PersonnelEmploymentTypes(CurrentPageState_PersonnelEmploymentTypes), CharToKeyCode_PersonnelEmploymentTypes(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ID), CharToKeyCode_PersonnelEmploymentTypes(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.PersonnelID), CharToKeyCode_PersonnelEmploymentTypes(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeID), CharToKeyCode_PersonnelEmploymentTypes(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeName), CharToKeyCode_PersonnelEmploymentTypes(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.FromDate), CharToKeyCode_PersonnelEmploymentTypes(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ToDate),CharToKeyCode_PersonnelEmploymentTypes(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentNumber));
    DialogWaiting.Show();
}

function UpdatePersonnelEmploymentType_PersonnelEmploymentTypesPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_PersonnelEmploymentTypes();
            PersonnelEmploymentTypes_OnAfterUpdate(Response);
            ChangePageState_PersonnelEmploymentTypes('View');
            parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ChangePersonnel_onPersonnelEmployTypesOperationCompleted(Response[4]);
        }
        else {
            if (CurrentPageState_PersonnelEmploymentTypes == 'Delete')
                ChangePageState_PersonnelEmploymentTypes('View');
        }
    }
}

function PersonnelEmploymentTypes_OnAfterUpdate(Response) {
    if (ObjPersonnelEmploymentType_PersonnelEmploymentTypes != null) {
        var PersonnelID = ObjPersonnelEmploymentType_PersonnelEmploymentTypes.PersonnelID;
        var EmploymentTypeID = ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeID;
        var EmploymentTypeName = ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentTypeName;
        var FromDate = ObjPersonnelEmploymentType_PersonnelEmploymentTypes.FromDate;
        var ToDate = ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ToDate;
        var EmploymntNumber = ObjPersonnelEmploymentType_PersonnelEmploymentTypes.EmploymentNumber;
        var PersonnelEmploymentTypeItem = null;
        GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.beginUpdate();
        switch (CurrentPageState_PersonnelEmploymentTypes) {
            case 'Add':
                PersonnelEmploymentTypeItem = GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.get_table().addEmptyRow(GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.get_recordCount());
                PersonnelEmploymentTypeItem.setValue(0, Response[3], false);
                GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.selectByKey(Response[3], 0, false);
                PersonnelEmploymentTypeItem = GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.selectByKey(ObjPersonnelEmploymentType_PersonnelEmploymentTypes.ID, 0, false);
                GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.deleteSelected();
                break;
        }
        if (CurrentPageState_PersonnelEmploymentTypes != 'Delete') {
            PersonnelEmploymentTypeItem.setValue(1, EmploymentTypeID, false);
            PersonnelEmploymentTypeItem.setValue(2, EmploymentTypeName, false);
            PersonnelEmploymentTypeItem.setValue(3, FromDate, false);
            PersonnelEmploymentTypeItem.setValue(4, ToDate, false);
            PersonnelEmploymentTypeItem.setValue(5, EmploymntNumber, false);
        }
        GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.endUpdate();
    }
}


function ShowDialogConfirm(confirmState) {
    ConfirmState_PersonnelEmploymentTypes = confirmState;
    if (CurrentPageState_PersonnelEmploymentTypes == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_PersonnelEmploymentTypes').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelEmploymentTypes').value;
    DialogConfirm.Show();
}

function ClearList_PersonnelEmploymentTypes() {
    if (CurrentPageState_PersonnelEmploymentTypes != 'Edit') {
        document.getElementById('cmbEmploymentTypes_PersonnelEmploymentTypes_Input').value = document.getElementById('hfcmbAlarm_PersonnelEmploymentTypes').value;
        cmbEmploymentTypes_PersonnelEmploymentTypes.unSelect();
        SelectedEmploymentType_PersonnelEmploymentTypes = null;
        ResetCalendars_PersonnelEmploymentTypes();
        GridPersonnelEmploymentTypes_PersonnelEmploymentTypes.unSelectAll();
        document.getElementById('txtEmploymentTypeNumber_PersonnelEmploymentTypes').value = '';
    }
}


function tlbItemNew_TlbPersonnelEmploymentTypes_onClick() {
    ChangePageState_PersonnelEmploymentTypes('Add');
    ClearList_PersonnelEmploymentTypes();
}

function tlbItemEdit_TlbPersonnelEmploymentTypes_onClick() {
    ChangePageState_PersonnelEmploymentTypes('Edit');
}

function tlbItemDelete_TlbPersonnelEmploymentTypes_onClick() {
    ChangePageState_PersonnelEmploymentTypes('Delete');
}

function tlbItemSave_TlbPersonnelEmploymentTypes_onClick() {
    PersonnelEmploymentType_onSave();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_PersonnelEmploymentTypes) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePersonnelEmploymentType_PersonnelEmploymentTypes();
            break;
        case 'Exit':
            ClearList_PersonnelEmploymentTypes();
            CloseDialogPersonnelEmploymentTypes();
            break;
    }
}

function CloseDialogPersonnelEmploymentTypes() {
    parent.document.getElementById('DialogPersonnelEmployTypes_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelEmployTypes.Close();
}


function tlbItemCancel_TlbPersonnelEmploymentTypes_onClick() {
    DialogConfirm.Close();
    ChangePageState_PersonnelEmploymentTypes('View');
}

function tlbItemExit_TlbPersonnelEmploymentTypes_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbEmploymentTypes_PersonnelEmploymentTypes_onExpand(sender, e) {
    if (cmbEmploymentTypes_PersonnelEmploymentTypes.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmploymentTypes_PersonnelEmploymentTypes == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmploymentTypes_PersonnelEmploymentTypes = true;
        Fill_cmbEmploymentTypes_PersonnelEmploymentTypes();
    }
    if (cmbEmploymentTypes_PersonnelEmploymentTypes.getSelectedItem() == undefined && SelectedEmploymentType_PersonnelEmploymentTypes != null)
        document.getElementById('cmbEmploymentTypes_PersonnelEmploymentTypes_Input').value = SelectedEmploymentType_PersonnelEmploymentTypes.Name;
}

function Fill_cmbEmploymentTypes_PersonnelEmploymentTypes() {
    ComboBox_onBeforeLoadData('cmbEmploymentTypes_PersonnelEmploymentTypes');
    CallBackcmbEmploymentTypes_PersonnelEmploymentTypes.callback();
}

function CallBackcmbEmploymentTypes_PersonnelEmploymentTypes_onBeforeCallback(sender, e) {
    cmbEmploymentTypes_PersonnelEmploymentTypes.dispose();
}

function CallBackcmbEmploymentTypes_PersonnelEmploymentTypes_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_EmploymentTypes_PersonnelEmploymentTypes').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbEmploymentTypes_PersonnelEmploymentTypes_DropImage').mousedown();
        cmbEmploymentTypes_PersonnelEmploymentTypes.expand();
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

function btn_gdpFromDate_PersonnelEmploymentTypes_OnMouseUp(event) {
    if (gCalFromDate_PersonnelEmploymentTypes.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpFromDate_PersonnelEmploymentTypes_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_PersonnelEmploymentTypes.getSelectedDate();
    gCalFromDate_PersonnelEmploymentTypes.setSelectedDate(FromDate);
}

function btn_gdpFromDate_PersonnelEmploymentTypes_OnClick(event) {
    if (gCalFromDate_PersonnelEmploymentTypes.get_popUpShowing()) {
        gCalFromDate_PersonnelEmploymentTypes.hide();
    }
    else {
        gCalFromDate_PersonnelEmploymentTypes.setSelectedDate(gdpFromDate_PersonnelEmploymentTypes.getSelectedDate());
        gCalFromDate_PersonnelEmploymentTypes.show();
    }
}

function gCalFromDate_PersonnelEmploymentTypes_OnChange(sender, e) {
    var FromDate = gCalFromDate_PersonnelEmploymentTypes.getSelectedDate();
    gdpFromDate_PersonnelEmploymentTypes.setSelectedDate(FromDate);
}

function gCalFromDate_PersonnelEmploymentTypes_onLoad(sender, e) {
    window.gCalFromDate_PersonnelEmploymentTypes.PopUpObject.z = 25000000;

}

function btn_gdpToDate_PersonnelEmploymentTypes_OnMouseUp(event) {
    if (gCalToDate_PersonnelEmploymentTypes.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gdpToDate_PersonnelEmploymentTypes_OnDateChange(sender, e) {
    var ToDate = gdpToDate_PersonnelEmploymentTypes.getSelectedDate();
    gCalToDate_PersonnelEmploymentTypes.setSelectedDate(ToDate);
}

function btn_gdpToDate_PersonnelEmploymentTypes_OnClick(event) {
    if (gCalToDate_PersonnelEmploymentTypes.get_popUpShowing()) {
        gCalToDate_PersonnelEmploymentTypes.hide();
    }
    else {
        gCalToDate_PersonnelEmploymentTypes.setSelectedDate(gdpToDate_PersonnelEmploymentTypes.getSelectedDate());
        gCalToDate_PersonnelEmploymentTypes.show();
    }
}

function gCalToDate_PersonnelEmploymentTypes_OnChange(sender, e) {
    var ToDate = gCalToDate_PersonnelEmploymentTypes.getSelectedDate();
    gdpToDate_PersonnelEmploymentTypes.setSelectedDate(ToDate);
}

function gCalToDate_PersonnelEmploymentTypes_onLoad(sender, e) {
    window.gCalToDate_PersonnelEmploymentTypes.PopUpObject.z = 25000000;
}

function Refresh_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes() {
    Fill_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes();
}

function GridPersonnelEmploymentTypes_PersonnelEmploymentTypes_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes').innerHTML = '';
}

function GridPersonnelEmploymentTypes_PersonnelEmploymentTypes_onItemSelect(sender, e) {
    if (CurrentPageState_PersonnelEmploymentTypes != 'Add')
        NavigatePersonnelEmploymentType_PersonnelEmploymentTypes(e.get_item());
}

function CallBack_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PersonnelEmploymentTypes_PersonnelEmploymentTypes').value;
    if (error != "") {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        if (erroParts[3] == 'Reload')
            Refresh_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes();
    }
}

function CallBackcmbEmploymentTypes_PersonnelEmploymentTypes_onCallbackError() {
    ShowConnectionError_hfErrorType_PersonnelEmploymentTypes();
}

function CallBack_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPersonnelEmploymentTypes_PersonnelEmploymentTypes').innerHTML = '';
    ShowConnectionError_hfErrorType_PersonnelEmploymentTypes();
}

function ShowConnectionError_hfErrorType_PersonnelEmploymentTypes() {
    var error = document.getElementById('hfErrorType_hfErrorType_PersonnelEmploymentTypes').value;
    var errorBody = document.getElementById('hfConnectionError_hfErrorType_PersonnelEmploymentTypes').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemHelp_TlbEmploymentType_onClick() {
    LoadHelpPage('tlbItemHelp_TlbEmploymentType');
}

function tlbItemFormReconstruction_TlbEmploymentType_onClick() {
    CloseDialogPersonnelEmploymentTypes();
    parent.parent.document.getElementById('DialogPersonnelMainInformation_IFrame').contentWindow.ShowDialogPersonnelEmployTypes();
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
    ChangePageState_PersonnelEmploymentTypes('View');
}