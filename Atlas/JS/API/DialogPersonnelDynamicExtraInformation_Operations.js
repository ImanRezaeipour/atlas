var CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation = 'View';
var ObjDynamicParameter_PersonnelDynamicExtraInformation = null;
var CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation = 'View';
var ObjDynamicParameterPair_PersonnelDynamicExtraInformation = new Object();
var ConfirmState_PersonnelDynamicExtraInformation = null;

function GetBoxesHeaders_PersonnelDynamicExtraInformation() {
    parent.document.getElementById('Title_DialogPersonnelDynamicExtraInformation').innerHTML = document.getElementById('hfTitle_DialogPersonnelDynamicExtraInformation').value;
    document.getElementById('header_DynamicParameters_PersonnelDynamicExtraInformation').innerHTML = document.getElementById('hfheader_DynamicParameters_PersonnelDynamicExtraInformation').value;
    document.getElementById('header_DynamicParameterDetails_PersonnelDynamicExtraInformation').innerHTML = document.getElementById('hfheader_DynamicParameterDetails_PersonnelDynamicExtraInformation').value;
    document.getElementById('header_DynamicParameterPairs_PersonnelDynamicExtraInformation').innerHTML = document.getElementById('hfheader_DynamicParameterPairs_PersonnelDynamicExtraInformation').value;
}

function gdpFromDate_PersonnelDynamicExtraInformation_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_PersonnelDynamicExtraInformation.getSelectedDate();
    gCalFromDate_PersonnelDynamicExtraInformation.setSelectedDate(FromDate);
}

function gCalFromDate_PersonnelDynamicExtraInformation_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_PersonnelDynamicExtraInformation.getSelectedDate();
    gdpFromDate_PersonnelDynamicExtraInformation.setSelectedDate(FromDate);
}

function btn_gdpFromDate_PersonnelDynamicExtraInformation_OnClick(event) {
    if (gCalFromDate_PersonnelDynamicExtraInformation.get_popUpShowing()) {
        gCalFromDate_PersonnelDynamicExtraInformation.hide();
    }
    else {
        gCalFromDate_PersonnelDynamicExtraInformation.setSelectedDate(gdpFromDate_PersonnelDynamicExtraInformation.getSelectedDate());
        gCalFromDate_PersonnelDynamicExtraInformation.show();
    }
}

function btn_gdpFromDate_PersonnelDynamicExtraInformation_OnMouseUp(event) {
    if (gCalFromDate_PersonnelDynamicExtraInformation.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_PersonnelDynamicExtraInformation_onLoad(sender, e) {
    window.gCalFromDate_PersonnelDynamicExtraInformation.PopUpObject.z = 25000000;
}

function gdpToDate_PersonnelDynamicExtraInformation_OnDateChange(sender, eventArgs) {
    var FromDate = gdpToDate_PersonnelDynamicExtraInformation.getSelectedDate();
    gCalToDate_PersonnelDynamicExtraInformation.setSelectedDate(FromDate);
}

function gCalToDate_PersonnelDynamicExtraInformation_OnChange(sender, eventArgs) {
    var FromDate = gCalToDate_PersonnelDynamicExtraInformation.getSelectedDate();
    gdpToDate_PersonnelDynamicExtraInformation.setSelectedDate(FromDate);
}

function btn_gdpToDate_PersonnelDynamicExtraInformation_OnClick(event) {
    if (gCalToDate_PersonnelDynamicExtraInformation.get_popUpShowing()) {
        gCalToDate_PersonnelDynamicExtraInformation.hide();
    }
    else {
        gCalToDate_PersonnelDynamicExtraInformation.setSelectedDate(gdpToDate_PersonnelDynamicExtraInformation.getSelectedDate());
        gCalToDate_PersonnelDynamicExtraInformation.show();
    }
}

function btn_gdpToDate_PersonnelDynamicExtraInformation_OnMouseUp(event) {
    if (gCalToDate_PersonnelDynamicExtraInformation.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_PersonnelDynamicExtraInformation_onLoad(sender, e) {
    window.gCalToDate_PersonnelDynamicExtraInformation.PopUpObject.z = 25000000;
}

function tlbItemNew_TlbDynamicParameters_onClick() {
    ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation('Add');
    ClearList_DynamicParameters_PersonnelDynamicExtraInformation();
    FocusOnFirstElement_DynamicParameters_PersonnelDynamicExtraInformation();
}

function ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation(state) {
    CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation = state;
    SetActionMode_PersonnelDynamicExtraInformation('DynamicParameter', state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters') != null) {
            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_enabled(false);
            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_imageUrl('add_silver.png');
        }
        if (TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters') != null) {
            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_enabled(false);
            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_imageUrl('edit_silver.png');
        }
        if (TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters') != null) {
            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_enabled(false);
            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_imageUrl('remove_silver.png');
        }
        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_enabled(true);
        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_imageUrl('save.png');
        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_enabled(true);
        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_imageUrl('cancel.png');
        document.getElementById('txtDynamicParameterCustomCode_PersonnelDynamicExtraInformation').disabled = '';
        document.getElementById('txtDynamicParameterTitle_PersonnelDynamicExtraInformation').disabled = '';
        if (state == 'Edit')
            NavigateDynamicParameter_PersonnelDynamicExtraInformation(GridDynamicParameters_PersonnelDynamicExtraInformation.getSelectedItems()[0]);
        if (state == 'Delete')
            DynamicParameter_onSave();
    }
    if (state == 'View') {
        if (TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters') != null) {
            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_enabled(true);
            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_imageUrl('add.png');
        }
        if (TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters') != null) {
            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_enabled(true);
            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_imageUrl('edit.png');
        }
        if (TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters') != null) {
            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_enabled(true);
            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_imageUrl('remove.png');
        }
        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_enabled(false);
        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_imageUrl('save_silver.png');
        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_enabled(false);
        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_imageUrl('cancel_silver.png');
        document.getElementById('txtDynamicParameterCustomCode_PersonnelDynamicExtraInformation').disabled = 'disabled';
        document.getElementById('txtDynamicParameterTitle_PersonnelDynamicExtraInformation').disabled = 'disabled';
    }
}

function ClearList_DynamicParameters_PersonnelDynamicExtraInformation() {
    if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation != 'Edit') {
        document.getElementById('txtDynamicParameterCustomCode_PersonnelDynamicExtraInformation').value = '';
        document.getElementById('txtDynamicParameterTitle_PersonnelDynamicExtraInformation').value = '';
    }
}

function NavigateDynamicParameter_PersonnelDynamicExtraInformation(selectedDynamicParameter) {
    if (selectedDynamicParameter != undefined) {
        document.getElementById('txtDynamicParameterCustomCode_PersonnelDynamicExtraInformation').value = selectedDynamicParameter.getMember('Key').get_text();
        document.getElementById('txtDynamicParameterTitle_PersonnelDynamicExtraInformation').value = selectedDynamicParameter.getMember('Title').get_text();
        Fill_GridDynamicParameterPairs_PersonnelDynamicExtraInformation(selectedDynamicParameter.getMember('ID').get_text());
    }
}

function FocusOnFirstElement_DynamicParameters_PersonnelDynamicExtraInformation() {
    document.getElementById('txtDynamicParameterCustomCode_PersonnelDynamicExtraInformation').focus();
}

function Fill_GridDynamicParameterPairs_PersonnelDynamicExtraInformation(DynamicParameterID) {
    document.getElementById('loadingPanel_GridDynamicParameterPairs_PersonnelDynamicExtraInformation').innerHTML = document.getElementById('hfloadingPanel_GridDynamicParameterPairs_PersonnelDynamicExtraInformation').value;
    var ObjDialogPersonnelDynamicExtraInformation = parent.DialogPersonnelDynamicExtraInformation.get_value();
    CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation.callback(CharToKeyCode_PersonnelDynamicExtraInformation(DynamicParameterID), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDialogPersonnelDynamicExtraInformation.PersonnelID));
}

function CharToKeyCode_PersonnelDynamicExtraInformation(str) {
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

function SetActionMode_PersonnelDynamicExtraInformation(contentParent, state) {
    if (contentParent == 'Form' || contentParent == 'DynamicParameter')
        document.getElementById('ActionMode_DynamicParameters').innerHTML = document.getElementById("hf" + state + "_DynamicParameter").value;
    if (contentParent == 'Form' || contentParent == 'DynamicParameterPair')
        document.getElementById('ActionMode_DynamicParametersPairs').innerHTML = document.getElementById("hf" + state + "_DynamicParameterPair").value;
}

function ChangeCalendarsEnabled_PersonnelDynamicExtraInformation(state) {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ChangeCalendarEnabled_PersonnelDynamicExtraInformation('pdpFromDate_PersonnelDynamicExtraInformation', state);
            ChangeCalendarEnabled_PersonnelDynamicExtraInformation('pdpToDate_PersonnelDynamicExtraInformation', state);
            break;
        case 'en-US':
            ChangeCalendarEnabled_PersonnelDynamicExtraInformation('gdpFromDate_PersonnelDynamicExtraInformation', state);
            ChangeCalendarEnabled_PersonnelDynamicExtraInformation('gdpToDate_PersonnelDynamicExtraInformation', state);
            break;
    }
}

function ChangeCalendarEnabled_PersonnelDynamicExtraInformation(cal, state) {
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
                        CalendarsViewManage_PersonnelDynamicExtraInformation(cal);
                    };
                    break;
            }
            break;
    }
}

function CalendarsViewManage_PersonnelDynamicExtraInformation(gdpID) {
    var CalID_PersonnelDynamicExtraInformation = 'gCal' + gdpID.substr(3, gdpID.length - 3);
    var Cal_PersonnelDynamicExtraInformation = eval(CalID_PersonnelDynamicExtraInformation);
    if (!Cal_PersonnelDynamicExtraInformation.get_popUpShowing())
        Cal_PersonnelDynamicExtraInformation.show();
    else
        Cal_PersonnelDynamicExtraInformation.hide();
}

function tlbItemEdit_TlbDynamicParameters_onClick() {
    ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation('Edit');
    FocusOnFirstElement_DynamicParameters_PersonnelDynamicExtraInformation();
}

function tlbItemDelete_TlbDynamicParameters_onClick() {
    ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation('Delete');
}

function tlbItemSave_TlbDynamicParameters_onClick() {
    CollapseControls_PersonnelDynamicExtraInformation();
    DynamicParameter_onSave();
}

function DynamicParameter_onSave() {
    if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation != 'Delete')
        UpdateDynamicParameter_PersonnelDynamicExtraInformation();
    else
        ShowDialogConfirm('DynamicParameter', 'Delete', CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation);
}

function ShowDialogConfirm(role, confirmState, currentPageState) {
    ConfirmState_PersonnelDynamicExtraInformation = confirmState;
    if (currentPageState == 'Delete') {
        var ConfirmMessage = null;
        switch (role) {
            case 'DynamicParameter':
                ConfirmMessage = document.getElementById('hfDeleteMessage_DynamicParameter').value;
                break;
            case 'DynamicParameterPair':
                ConfirmMessage = document.getElementById('hfDeleteMessage_DynamicParameterPair').value;
                break;
        }
        document.getElementById('lblConfirm').innerHTML = ConfirmMessage;
    }
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelDynamicExtraInformation').value;
    DialogConfirm.set_value(role);
    DialogConfirm.Show();
    CollapseControls_PersonnelDynamicExtraInformation();
}

function UpdateDynamicParameter_PersonnelDynamicExtraInformation() {
    ObjDynamicParameter_PersonnelDynamicExtraInformation = new Object();
    ObjDynamicParameter_PersonnelDynamicExtraInformation.ID = '0';
    ObjDynamicParameter_PersonnelDynamicExtraInformation.CustomCode = null;
    ObjDynamicParameter_PersonnelDynamicExtraInformation.Title = null;
    ObjDynamicParameter_PersonnelDynamicExtraInformation.PersonnelState = parent.DialogPersonnelDynamicExtraInformation.get_value().PageState;

    var SelectedItems_GridDynamicParameters_PersonnelDynamicExtraInformation = GridDynamicParameters_PersonnelDynamicExtraInformation.getSelectedItems();
    if (SelectedItems_GridDynamicParameters_PersonnelDynamicExtraInformation.length > 0)
        ObjDynamicParameter_PersonnelDynamicExtraInformation.ID = SelectedItems_GridDynamicParameters_PersonnelDynamicExtraInformation[0].getMember("ID").get_text();

    if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation != 'Delete') {
        ObjDynamicParameter_PersonnelDynamicExtraInformation.CustomCode = document.getElementById('txtDynamicParameterCustomCode_PersonnelDynamicExtraInformation').value;
        ObjDynamicParameter_PersonnelDynamicExtraInformation.Title = document.getElementById('txtDynamicParameterTitle_PersonnelDynamicExtraInformation').value;
    }
    UpdateDynamicParameter_PersonnelDynamicExtraInformationPage(CharToKeyCode_PersonnelDynamicExtraInformation(CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameter_PersonnelDynamicExtraInformation.PersonnelState), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameter_PersonnelDynamicExtraInformation.ID), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameter_PersonnelDynamicExtraInformation.CustomCode), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameter_PersonnelDynamicExtraInformation.Title));
    DialogWaiting.Show();
}

function UpdateDynamicParameter_PersonnelDynamicExtraInformationPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelDynamicExtraInformation').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelDynamicExtraInformation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_DynamicParameters_PersonnelDynamicExtraInformation();
            DynamicParameters_PersonnelDynamicExtraInformation_OnAfterUpdate(Response);
            ClearData_GridDynamicParameterPairs_PersonnelDynamicExtraInformation();
            ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation('View');
        }
        else {
            if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation == 'Delete')
                ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation('View');
            else
                ChangeRelativeToolBarEnabled_PersonnelDynamicExtraInformation('DynamicParameterPair', 'End');
        }
    }
}

function DynamicParameters_PersonnelDynamicExtraInformation_OnAfterUpdate(Response) {
    if (ObjDynamicParameter_PersonnelDynamicExtraInformation != null) {
        var DynamicParameterCustomCode = ObjDynamicParameter_PersonnelDynamicExtraInformation.CustomCode;
        var DynamicParameterTitle = ObjDynamicParameter_PersonnelDynamicExtraInformation.Title;

        var DynamicParameterItem = null;

        GridDynamicParameters_PersonnelDynamicExtraInformation.beginUpdate();
        switch (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation) {
            case 'Add':
                DynamicParameterItem = GridDynamicParameters_PersonnelDynamicExtraInformation.get_table().addEmptyRow(GridDynamicParameters_PersonnelDynamicExtraInformation.get_recordCount());
                DynamicParameterItem.setValue(0, Response[3], false);
                GridDynamicParameters_PersonnelDynamicExtraInformation.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridDynamicParameters_PersonnelDynamicExtraInformation.selectByKey(Response[3], 0, false);
                DynamicParameterItem = GridDynamicParameters_PersonnelDynamicExtraInformation.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridDynamicParameters_PersonnelDynamicExtraInformation.selectByKey(ObjDynamicParameter_PersonnelDynamicExtraInformation.ID, 0, false);
                GridDynamicParameters_PersonnelDynamicExtraInformation.deleteSelected();
                break;
        }
        if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation != 'Delete') {
            DynamicParameterItem.setValue(1, DynamicParameterCustomCode, false);
            DynamicParameterItem.setValue(2, DynamicParameterTitle, false);
        }
        GridDynamicParameters_PersonnelDynamicExtraInformation.endUpdate();
    }
}

function ChangeRelativeToolBarEnabled_PersonnelDynamicExtraInformation(master, operationState) {
    switch (master) {
        case 'DynamicParameter':
            switch (operationState) {
                case 'Start':
                    if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs') != null) {
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_enabled(false);
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_imageUrl('add_silver.png');
                    }
                    if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs') != null) {
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_enabled(false);
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_imageUrl('edit_silver.png');
                    }
                    if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs') != null) {
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_enabled(false);
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_imageUrl('remove_silver.png');
                    }

                    TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_enabled(false);
                    TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_imageUrl('save_silver.png');
                    TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_enabled(false);
                    TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_imageUrl('cancel_silver.png');
                    break;
                case 'End':
                    if (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation == 'Add' || CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation == 'Edit' || CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation == 'Delete') {
                        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs') != null) {
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_enabled(false);
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_imageUrl('add_silver.png');
                        }
                        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs') != null) {
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_enabled(false);
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_imageUrl('edit_silver.png');
                        }
                        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs') != null) {
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_enabled(false);
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_imageUrl('remove_silver.png');
                        }

                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_enabled(true);
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_imageUrl('save.png');
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_enabled(true);
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_imageUrl('cancel.png');
                    }
                    if (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation == 'View') {
                        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs') != null) {
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_enabled(true);
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_imageUrl('add.png');
                        }
                        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs') != null) {
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_enabled(true);
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_imageUrl('edit.png');
                        }
                        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs') != null) {
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_enabled(true);
                            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_imageUrl('remove.png');
                        }                       
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_enabled(false);
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_imageUrl('save_silver.png');
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_enabled(false);
                        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_imageUrl('cancel_silver.png');
                    }
                    break;
            }
            break;
        case 'DynamicParameterPair':
            switch (operationState) {
                case 'Start':
                    if (TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters') != null) {
                        TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_enabled(false);
                        TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_imageUrl('add_silver.png');
                    }
                    if (TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters') != null) {
                        TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_enabled(false);
                        TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_imageUrl('edit_silver.png');
                    }
                    if (TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters') != null) {
                        TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_enabled(false);
                        TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_imageUrl('remove_silver.png');
                    }
                    TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_enabled(false);
                    TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_imageUrl('save_silver.png');
                    TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_enabled(false);
                    TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_imageUrl('cancel_silver.png');

                    break;
                case 'End':
                    if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation == 'Add' || CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation == 'Edit' || CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation == 'Delete') {
                        if (TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters') != null) {
                            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_enabled(false);
                            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_imageUrl('add_silver.png');
                        }
                        if (TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters') != null) {
                            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_enabled(false);
                            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_imageUrl('edit_silver.png');
                        }
                        if (TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters') != null) {
                            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_enabled(false);
                            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_imageUrl('remove_silver.png');
                        }
                        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_enabled(true);
                        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_imageUrl('save.png');
                        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_enabled(true);
                        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_imageUrl('cancel.png');


                    }
                    if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation == 'View') {
                        if (TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters') != null) {
                            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_enabled(true);
                            TlbDynamicParameters.get_items().getItemById('tlbItemNew_TlbDynamicParameters').set_imageUrl('add.png');
                        }
                        if (TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters') != null) {
                            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_enabled(true);
                            TlbDynamicParameters.get_items().getItemById('tlbItemEdit_TlbDynamicParameters').set_imageUrl('edit.png');
                        }
                        if (TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters') != null) {
                            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_enabled(true);
                            TlbDynamicParameters.get_items().getItemById('tlbItemDelete_TlbDynamicParameters').set_imageUrl('remove.png');
                        }
                        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_enabled(false);
                        TlbDynamicParameters.get_items().getItemById('tlbItemSave_TlbDynamicParameters').set_imageUrl('save_silver.png');
                        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_enabled(false);
                        TlbDynamicParameters.get_items().getItemById('tlbItemCancel_TlbDynamicParameters').set_imageUrl('cancel_silver.png');
                    }
                    break
            }
    }
}

function ClearData_GridDynamicParameterPairs_PersonnelDynamicExtraInformation() {
    if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation == 'Add' || CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation == 'Delete') {
        GridDynamicParameterPairs_PersonnelDynamicExtraInformation.get_table().clearData();
        GridDynamicParameterPairs_PersonnelDynamicExtraInformation.render();
    }
}

function tlbItemCancel_TlbDynamicParameters_onClick() {
    ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation('View');
    ClearList_DynamicParameters_PersonnelDynamicExtraInformation();
}

function tlbItemFormReconstruction_TlbDynamicParameters_onClick() {
    CloseDialogPersonnelExtraInformation();
    parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'DialogPersonnelMainInformation_IFrame').contentWindow.ShowDialogPersonnelDynamicExtraInformation();
}

function tlbItemExit_TlbDynamicParameters_onClick() {
    ShowDialogConfirm(null, 'Exit', null);
}

function Refresh_GridDynamicParameters_PersonnelDynamicExtraInformation() {
    Fill_GridDynamicParameters_PersonnelDynamicExtraInformation();
}

function Fill_GridDynamicParameters_PersonnelDynamicExtraInformation() {
    document.getElementById('loadingPanel_GridDynamicParameters_PersonnelDynamicExtraInformation').innerHTML = document.getElementById('hfloadingPanel_GridDynamicParameters_PersonnelDynamicExtraInformation').value;
    CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation.callback();
}

function GridDynamicParameters_PersonnelDynamicExtraInformation_onItemSelect(sender, e) {
    if (CurrentPageState_DynamicParameters_PersonnelDynamicExtraInformation != 'Add')
        NavigateDynamicParameter_PersonnelDynamicExtraInformation(e.get_item());
}

function GridDynamicParameters_PersonnelDynamicExtraInformation_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridDynamicParameters_PersonnelDynamicExtraInformation').innerHTML = '';
}

function CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DynamicParameters_PersonnelDynamicExtraInformation').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridDynamicParameters_PersonnelDynamicExtraInformation();
    }
    else {
        if (GridDynamicParameters_PersonnelDynamicExtraInformation.get_table().getRowCount() > 0) {
            var DynamicParameterID = GridDynamicParameters_PersonnelDynamicExtraInformation.get_table().getRow(0).getMember('ID').get_text();
            GridDynamicParameters_PersonnelDynamicExtraInformation.selectByKey(DynamicParameterID, 0, false);
            Fill_GridDynamicParameterPairs_PersonnelDynamicExtraInformation(DynamicParameterID);
        }
    }
}

function CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridDynamicParameters_PersonnelDynamicExtraInformation').innerHTML = '';
    ShowConnectionError_PersonnelDynamicExtraInformation();
}

function ShowConnectionError_PersonnelDynamicExtraInformation() {
    var error = document.getElementById('hfErrorType_PersonnelDynamicExtraInformation').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelDynamicExtraInformation').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemNew_TlbDynamicParametersPairs_onClick() {
    ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation('Add');
    ClearList_DynamicParameterPairs_PersonnelDynamicExtraInformation();
    FocusOnFirstElement_DynamicParameterPairs_PersonnelDynamicExtraInformation();
}

function FocusOnFirstElement_DynamicParameterPairs_PersonnelDynamicExtraInformation() {
    document.getElementById('txtDynamicParameterValue_PersonnelDynamicExtraInformation').focus();
    document.getElementById('txtDynamicParameterValue_PersonnelDynamicExtraInformation').disabled = '';
}

function ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation(state) {
    CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation = state;
    SetActionMode_PersonnelDynamicExtraInformation('DynamicParameterPair', state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs') != null) {
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_enabled(false);
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_imageUrl('add_silver.png');
        }
        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs') != null) {
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_enabled(false);
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_imageUrl('edit_silver.png');
        }
        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs') != null) {
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_enabled(false);
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_imageUrl('remove_silver.png');
        }
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_enabled(true);
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_imageUrl('save.png');
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_enabled(true);
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_imageUrl('cancel.png');
        //document.getElementById('txtDynamicParameterValue_PersonnelDynamicExtraInformation').disabled = '';
        ChangeRelativeToolBarEnabled_PersonnelDynamicExtraInformation('DynamicParameterPair', 'Start');
        ChangeCalendarsEnabled_PersonnelDynamicExtraInformation('enable');
        if (state == 'Edit')
            if (GridDynamicParameterPairs_PersonnelDynamicExtraInformation.getSelectedItems()[0] != null || GridDynamicParameterPairs_PersonnelDynamicExtraInformation.getSelectedItems()[0] != undefined) {
                NavigateDynamicParameterPair_PersonnelDynamicExtraInformation(GridDynamicParameterPairs_PersonnelDynamicExtraInformation.getSelectedItems()[0]);
            }
        if (state == 'Delete')
            DynamicParameterPair_onSave();
    }
    if (state == 'View') {
        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs') != null) {
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_enabled(true);
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemNew_TlbDynamicParametersPairs').set_imageUrl('add.png');
        }
        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs') != null) {
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_enabled(true);
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemEdit_TlbDynamicParametersPairs').set_imageUrl('edit.png');
        }
        if (TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs') != null) {
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_enabled(true);
            TlbDynamicParametersPairs.get_items().getItemById('tlbItemDelete_TlbDynamicParametersPairs').set_imageUrl('remove.png');
        }
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_enabled(false);
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemSave_TlbDynamicParametersPairs').set_imageUrl('save_silver.png');
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_enabled(false);
        TlbDynamicParametersPairs.get_items().getItemById('tlbItemCancel_TlbDynamicParametersPairs').set_imageUrl('cancel_silver.png');
        document.getElementById('txtDynamicParameterValue_PersonnelDynamicExtraInformation').disabled = 'disabled';
        ChangeRelativeToolBarEnabled_PersonnelDynamicExtraInformation('DynamicParameterPair', 'End');
        ChangeCalendarsEnabled_PersonnelDynamicExtraInformation('disable');
    }
}

function DynamicParameterPair_onSave() {
    if (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation != 'Delete')
        UpdateDynamicParameterPair_PersonnelDynamicExtraInformation();
    else
        ShowDialogConfirm('DynamicParameterPair', 'Delete', CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation);
}

function UpdateDynamicParameterPair_PersonnelDynamicExtraInformation() {
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation = new Object();
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ID = '0';
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.PersonnelID = '0';
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.DynamicParameterID = '0';
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.DynamicParameterValue = null;
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.FromDate = null;
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ToDate = null;
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.PersonnelState = parent.DialogPersonnelDynamicExtraInformation.get_value().PageState;

    var ObjDialogPersonnelDynamicExtraInformation = parent.DialogPersonnelDynamicExtraInformation.get_value();
    ObjDynamicParameterPair_PersonnelDynamicExtraInformation.PersonnelID = ObjDialogPersonnelDynamicExtraInformation.PersonnelID;

    var SelectedItems_GridDynamicParameters_PersonnelDynamicExtraInformation = GridDynamicParameters_PersonnelDynamicExtraInformation.getSelectedItems();
    if (SelectedItems_GridDynamicParameters_PersonnelDynamicExtraInformation.length > 0)
        ObjDynamicParameterPair_PersonnelDynamicExtraInformation.DynamicParameterID = SelectedItems_GridDynamicParameters_PersonnelDynamicExtraInformation[0].getMember("ID").get_text();

    var SelectedItems_GridDynamicParameterPairs_PersonnelDynamicExtraInformation = GridDynamicParameterPairs_PersonnelDynamicExtraInformation.getSelectedItems();
    if (SelectedItems_GridDynamicParameterPairs_PersonnelDynamicExtraInformation.length > 0)
        ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ID = SelectedItems_GridDynamicParameterPairs_PersonnelDynamicExtraInformation[0].getMember("ID").get_text();

    if (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation != 'Delete') {
        ObjDynamicParameterPair_PersonnelDynamicExtraInformation.DynamicParameterValue = document.getElementById('txtDynamicParameterValue_PersonnelDynamicExtraInformation').value;
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                ObjDynamicParameterPair_PersonnelDynamicExtraInformation.FromDate = document.getElementById('pdpFromDate_PersonnelDynamicExtraInformation').value;
                ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ToDate = document.getElementById('pdpToDate_PersonnelDynamicExtraInformation').value;
                break;
            case 'en-US':
                ObjDynamicParameterPair_PersonnelDynamicExtraInformation.FromDate = document.getElementById('gdpFromDate_PersonnelDynamicExtraInformation_picker').value;
                ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ToDate = document.getElementById('gdpToDate_PersonnelDynamicExtraInformation_picker').value;
                break;
        }
    }
    UpdateDynamicParameterPair_PersonnelDynamicExtraInformationPage(CharToKeyCode_PersonnelDynamicExtraInformation(CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.PersonnelState), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.PersonnelID), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.DynamicParameterID), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ID), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.DynamicParameterValue), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.FromDate), CharToKeyCode_PersonnelDynamicExtraInformation(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ToDate));
    DialogWaiting.Show();
}

function UpdateDynamicParameterPair_PersonnelDynamicExtraInformationPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelDynamicExtraInformation').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelDynamicExtraInformation').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_DynamicParameterPairs_PersonnelDynamicExtraInformation();
            DynamicParameterPair_PersonnelDynamicExtraInformation_OnAfterUpdate(Response);
            ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation('View');
        }
        else {
            if (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation == 'Delete') {
                ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation('View');
            }
            else
                ChangeRelativeToolBarEnabled_PersonnelDynamicExtraInformation('DynamicParameterPair', 'End');
        }
    }
}

function DynamicParameterPair_PersonnelDynamicExtraInformation_OnAfterUpdate(Response) {
    if (ObjDynamicParameterPair_PersonnelDynamicExtraInformation != null) {
        var DynamicParameterValue = ObjDynamicParameterPair_PersonnelDynamicExtraInformation.DynamicParameterValue;
        var FromDate = ObjDynamicParameterPair_PersonnelDynamicExtraInformation.FromDate;
        var ToDate = ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ToDate;

        var DynamicParameterPairItem = null;
        GridDynamicParameterPairs_PersonnelDynamicExtraInformation.beginUpdate();
        switch (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation) {
            case 'Add':
                DynamicParameterPairItem = GridDynamicParameterPairs_PersonnelDynamicExtraInformation.get_table().addEmptyRow(GridDynamicParameterPairs_PersonnelDynamicExtraInformation.get_recordCount());
                DynamicParameterPairItem.setValue(0, Response[3], false);
                GridDynamicParameterPairs_PersonnelDynamicExtraInformation.selectByKey(Response[3], 0, false);
                break;
            case 'Edit':
                GridDynamicParameterPairs_PersonnelDynamicExtraInformation.selectByKey(Response[3], 0, false);
                DynamicParameterPairItem = GridDynamicParameterPairs_PersonnelDynamicExtraInformation.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridDynamicParameterPairs_PersonnelDynamicExtraInformation.selectByKey(ObjDynamicParameterPair_PersonnelDynamicExtraInformation.ID, 0, false);
                GridDynamicParameterPairs_PersonnelDynamicExtraInformation.deleteSelected();
                break;
        }
        if (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation != 'Delete') {
            DynamicParameterPairItem.setValue(3, DynamicParameterValue, false);
            DynamicParameterPairItem.setValue(4, FromDate, false);
            DynamicParameterPairItem.setValue(5, ToDate, false);
        }
        GridDynamicParameterPairs_PersonnelDynamicExtraInformation.endUpdate();
    }
}

function ClearList_DynamicParameterPairs_PersonnelDynamicExtraInformation() {
    document.getElementById('txtDynamicParameterValue_PersonnelDynamicExtraInformation').value = '';
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_PersonnelDynamicExtraInformation').value = "";
            document.getElementById('pdpToDate_PersonnelDynamicExtraInformation').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_PersonnelDynamicExtraInformation').value = "";
            document.getElementById('gdpToDate_PersonnelDynamicExtraInformation').value = "";
            break;
    }
}

function NavigateDynamicParameterPair_PersonnelDynamicExtraInformation(selectedDynamicParameterPairItem) {
    document.getElementById('txtDynamicParameterValue_PersonnelDynamicExtraInformation').value = selectedDynamicParameterPairItem.getMember('Value').get_text();
    if (selectedDynamicParameterPairItem != undefined) {
        switch (parent.parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpFromDate_PersonnelDynamicExtraInformation').value = selectedDynamicParameterPairItem.getMember('TheFromDate').get_text();
                document.getElementById('pdpToDate_PersonnelDynamicExtraInformation').value = selectedDynamicParameterPairItem.getMember('TheToDate').get_text();
                break;
            case 'en-US':
                var fromDate = new Date(selectedDynamicParameterPairItem.getMember('TheFromDate').get_text());
                gdpFromDate_PersonnelDynamicExtraInformation.setSelectedDate(fromDate);
                gCalFromDate_PersonnelDynamicExtraInformation.setSelectedDate(fromDate);
                var toDate = new Date(selectedDynamicParameterPairItem.getMember('TheToDate').get_text());
                gdpToDate_PersonnelDynamicExtraInformation.setSelectedDate(toDate);
                gCalToDate_PersonnelDynamicExtraInformation.setSelectedDate(toDate);
                break;
        }
    }
}

function tlbItemEdit_TlbDynamicParametersPairs_onClick() {
    ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation('Edit');
    var selectedDynamicParametersPairs_DynamicParametersPairs = GridDynamicParameterPairs_PersonnelDynamicExtraInformation.getSelectedItems()[0];
    if (selectedDynamicParametersPairs_DynamicParametersPairs != undefined) {
        FocusOnFirstElement_DynamicParameterPairs_PersonnelDynamicExtraInformation();
    }
}

function tlbItemDelete_TlbDynamicParametersPairs_onClick() {
    ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation('Delete');
}

function tlbItemSave_TlbDynamicParametersPairs_onClick() {
    CollapseControls_PersonnelDynamicExtraInformation();
    DynamicParameterPair_onSave();
}

function tlbItemCancel_TlbDynamicParametersPairs_onClick() {
    ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation('View');
    ClearList_DynamicParameterPairs_PersonnelDynamicExtraInformation();
}

function Refresh_GridDynamicParameterPairs_PersonnelDynamicExtraInformation() {
    var DynamicParameterID = null;
    if (GridDynamicParameters_PersonnelDynamicExtraInformation.get_table().getRowCount() > 0) {
        if (GridDynamicParameters_PersonnelDynamicExtraInformation.getSelectedItems() != undefined)
            DynamicParameterID = GridDynamicParameters_PersonnelDynamicExtraInformation.getSelectedItems()[0].getMember('ID').get_text();
        else
            DynamicParameterID = GridDynamicParameters_PersonnelDynamicExtraInformation.get_table().getRow(0).getMember('ID').get_text();
    }
    Fill_GridDynamicParameterPairs_PersonnelDynamicExtraInformation(DynamicParameterID);
}

function GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridDynamicParameterPairs_PersonnelDynamicExtraInformation').innerHTML = '';
}

function GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onItemSelect(sender, e) {
    if (CurrentPageState_DynamicParameterPairs_PersonnelDynamicExtraInformation != 'Add')
            NavigateDynamicParameterPair_PersonnelDynamicExtraInformation(e.get_item());
}

function CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DynamicParameterPairs_PersonnelDynamicExtraInformation').value;
    if (error != "") {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        if (erroParts[3] == 'Reload')
            Refresh_GridDynamicParameterPairs_PersonnelDynamicExtraInformation();
    }
}

function CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridDynamicParameterPairs_PersonnelDynamicExtraInformation').innerHTML = '';
    ShowConnectionError_PersonnelDynamicExtraInformation();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    var role = DialogConfirm.get_value();
    switch (ConfirmState_PersonnelDynamicExtraInformation) {
        case 'Delete':
            switch (role) {
                case 'DynamicParameter':
                    DialogConfirm.Close();
                    UpdateDynamicParameter_PersonnelDynamicExtraInformation();
                    break;
                case 'DynamicParameterPair':
                    DialogConfirm.Close();
                    UpdateDynamicParameterPair_PersonnelDynamicExtraInformation();
                    break;
            }
            break;
        case 'Exit':
            ClearList_DynamicParameters_PersonnelDynamicExtraInformation();
            ClearList_DynamicParameterPairs_PersonnelDynamicExtraInformation();
            CloseDialogPersonnelExtraInformation();
            break;
    }

}

function CloseDialogPersonnelExtraInformation() {
    parent.document.getElementById('DialogPersonnelDynamicExtraInformation_IFrame').src = 'WhitePage.aspx';
    parent.DialogPersonnelDynamicExtraInformation.Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    var role = DialogConfirm.get_value();
    switch (role) {
        case 'DynamicParameter':
            ChangePageState_DynamicParameters_PersonnelDynamicExtraInformation('View');
            break;
        case 'DynamicParameterPair':
            ChangePageState_DynamicParameterPairs_PersonnelDynamicExtraInformation('View');
            break;
    }
}

function CollapseControls_PersonnelDynamicExtraInformation() {
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_PersonnelDynamicExtraInformation');
}







