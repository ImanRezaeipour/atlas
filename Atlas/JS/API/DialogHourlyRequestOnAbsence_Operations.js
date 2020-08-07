
var box_MissionRequest_HourlyRequestOnAbsence_IsShown = false;
var box_LeaveRequest_HourlyRequestOnAbsence_IsShown = false;
var CurrenPageSate_DialogHourlyRequestOnAbsence = 'View';
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence = null;
var CurrentRequestState_HourlyRequestOnAbsence = null;
var ConfirmState_HourlyRequestOnAbsence = null;
var zeroTime = '00';
var ObjRequestAttachment_HourlyRequestOnAbsence = null;
var SelectedMissionLocationType_HourlyRequestOnAbsence = null;
var Substituteleft = Substitutetop = 0;
var CurrentPageCombosCallBcakStateObj = new Object();
var CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence = 0;
var LoadState_cmbPersonnel_HourlyRequestOnAbsence = 'Normal';
var SearchTerm_cmbPersonnel_HourlyRequestOnAbsence = '';
var AdvancedSearchTerm_cmbPersonnel_HourlyRequestOnAbsence = '';
var SelectedSubstitute_HourlyRequestOnAbsence = '';
//DNN note
var ObjSelectedAbsensePairsItem = null;
//End of DNN note
//DNN note
function SetTypesDefaultValue_HourlyRequestOnAbsence() {
    document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = GetMissionType_HourlyRequestOnAbsence()[0];
    document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = GetLeaveType_HourlyRequestOnAbsence()[0];
}
//End of DNN note

function initTimePickers_HourlyRequestOnAbsence() {
    SetButtonImages_TimeSelectors_DialogHourlyRequestOnAbsence();
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    ChangeTimePickerActions_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
}

function ResetTimepicker_HourlyRequestOnAbsence(TimePicker) {
    var zeroTime = '00';
    document.getElementById(TimePicker + "_txtHour").value = zeroTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function Set_SelectedDateTime_HourlyRequestOnAbsence() {
    document.getElementById('tdSelectedDate_HourlyRequestOnAbsence').innerHTML = parent.DialogHourlyRequestOnAbsence.get_value().RequestDateTitle;
}

function GetBoxesHeaders_HourlyRequestOnAbsence() {
    parent.document.getElementById('Title_DialogHourlyRequestOnAbsence').innerHTML = document.getElementById('hfTitle_DialogHourlyRequestOnAbsence').value;
    document.getElementById('header_AbsenceDetails_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfheader_AbsenceDetails_HourlyRequestOnAbsence').value;
    document.getElementById('header_RegisteredRequests_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfheader_RegisteredRequests_HourlyRequestOnAbsence').value;
    document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
    document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
}

function SetButtonImages_TimeSelectors_DialogHourlyRequestOnAbsence() {
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
}

function ChangeTimePickerActions_DialogHourlyRequestOnAbsence(TimeSelector) {
    document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtHour');
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtMinute');
        addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
    };
    document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtHour');
        CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector + '_txtMinute');
        subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
    };
    //    document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
    //        CheckTimeSelectorPartValue_HourlyRequestOnAbsence(TimeSelector, '_txtHour');
    //    }
    //    document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
    //        CheckTimeSelectorPartValue_HourlyRequestOnAbsence(TimeSelector, '_txtMinute');
    //    }
}

function CheckTimeSelectorPartValue_HourlyRequestOnAbsence(TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        if (document.getElementById(TimeSelectorPartID + identifier).value == "")
            document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
    }
}

function SetButtonImages_TimeSelector_DialogHourlyRequestOnAbsence(TimeSelector) {
    document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    document.getElementById("" + TimeSelector + "_imgUp").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/oie_CustomUp.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    };
    document.getElementById("" + TimeSelector + "_imgDown").onmouseover = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/oie_CustomDown.gif";
        FocusOnCurrentTimeSelector(TimeSelector);
    };
    document.getElementById("" + TimeSelector + "_imgUp").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgUp").src = "images/TimeSelector/CustomUp.gif";
    };
    document.getElementById("" + TimeSelector + "_imgDown").onmouseout = function () {
        document.getElementById("" + TimeSelector + "_imgDown").src = "images/TimeSelector/CustomDown.gif";
    };
}


function FocusOnCurrentTimeSelector(TimeSelector) {
    if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond" && !document.getElementById("" + TimeSelector + "_txtHour").disabled)
        document.getElementById("" + TimeSelector + "_txtHour").focus();
}

function rdbLeaveRequest_HourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onInsert();
    box_LeaveRequest_HourlyRequestOnAbsence_onShowHide();
    //DNN Note
    if (ObjSelectedAbsensePairsItem != null || ObjSelectedAbsensePairsItem != undefined) {
        NavigateAbsensePairs_RequestOnAbsence(ObjSelectedAbsensePairsItem);
    }
    else if (GridAbsencePairs_RequestOnAbsence.get_table().getRowCount() > 0) {
        NavigateAbsensePairs_RequestOnAbsence(GridAbsencePairs_RequestOnAbsence.get_table().getRow(0));
    }
    //End of DNN note
}

function rdbMissionRequest_HourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onInsert();
    box_MissionRequest_HourlyRequestOnAbsence_onShowHide();
    //DNN Note
    if (ObjSelectedAbsensePairsItem != null || ObjSelectedAbsensePairsItem != undefined) {
        NavigateAbsensePairs_RequestOnAbsence(ObjSelectedAbsensePairsItem);
    }
    else if (GridAbsencePairs_RequestOnAbsence.get_table().getRowCount() > 0) {
        NavigateAbsensePairs_RequestOnAbsence(GridAbsencePairs_RequestOnAbsence.get_table().getRow(0));
    }
    //END of DNN Note
}

function box_MissionRequest_HourlyRequestOnAbsence_onShowHide() {
    CollapseControls_HourlyRequestOnAbsence();
    setSlideDownSpeed(200);
    slidedown_showHide('box_MissionRequest_HourlyRequestOnAbsence');
    if (box_MissionRequest_HourlyRequestOnAbsence_IsShown) {
        box_MissionRequest_HourlyRequestOnAbsence_IsShown = false;
        ClearMissionRequestList_HourlyRequestOnAbsence();
    }
    else {
        box_MissionRequest_HourlyRequestOnAbsence_IsShown = true;
        CurrentRequestState_HourlyRequestOnAbsence = 'Mission';
    }
}

function ClearMissionRequestList_HourlyRequestOnAbsence() {
    cmbMissionType_HourlyRequestOnAbsence.unSelect();
    //DNN Note
    document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
    document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = GetMissionType_HourlyRequestOnAbsence()[0];
    //ENd of DNN Note
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
    document.getElementById('txtDescription_Mission_HourlyRequestOnAbsence').value = '';
    var trvNodeNotDetermined = trvMissionLocation_HourlyRequestOnAbsence.findNodeById('-1');
    if (trvNodeNotDetermined != undefined) {
        trvNodeNotDetermined.select();
        cmbMissionLocation_HourlyRequestOnAbsence.set_text(trvNodeNotDetermined.get_text());
    }
    cmbMissionType_HourlyRequestOnAbsence.collapse();
    cmbMissionLocation_HourlyRequestOnAbsence.collapse();
    ObjRequestAttachment_HourlyRequestOnAbsence = null;
    document.getElementById('tdAttachmentName_Mission_HourlyRequestOnAbsence').innerHTML = '';
    if (document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence') != null)
        document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = false;
    if (document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence') != null)
        document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = false;
}

function box_LeaveRequest_HourlyRequestOnAbsence_onShowHide() {
    CollapseControls_HourlyRequestOnAbsence();
    setSlideDownSpeed(200);
    ChangeHideElementsState_DialogHourlyRequestOnAbsence(true);
    slidedown_showHide('box_LeaveRequest_HourlyRequestOnAbsence');
    if (box_LeaveRequest_HourlyRequestOnAbsence_IsShown) {
        box_LeaveRequest_HourlyRequestOnAbsence_IsShown = false;
        ClearLeaveRequestList_HourlyRequestOnAbsence();
    }
    else {
        box_LeaveRequest_HourlyRequestOnAbsence_IsShown = true;
        CurrentRequestState_HourlyRequestOnAbsence = 'Leave';
    }
}

function ClearLeaveRequestList_HourlyRequestOnAbsence() {
    cmbLeaveType_HourlyRequestOnAbsence.unSelect();
    //DNN Note
    //document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
    document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = GetLeaveType_HourlyRequestOnAbsence()[0];
    //ENd of DNN Note
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
    ResetTimepicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
    document.getElementById('txtDescription_Leave_HourlyRequestOnAbsence').value = '';
    cmbDoctorName_HourlyRequestOnAbsence.selectItemByIndex(0);
    cmbIllnessName_HourlyRequestOnAbsence.selectItemByIndex(0);
    cmbLeaveType_HourlyRequestOnAbsence.collapse();
    cmbDoctorName_HourlyRequestOnAbsence.collapse();
    cmbIllnessName_HourlyRequestOnAbsence.collapse();
    ObjRequestAttachment_HourlyRequestOnAbsence = null;
    document.getElementById('tdAttachmentName_Leave_HourlyRequestOnAbsence').innerHTML = '';
    if (document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence') != null)
        document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = false;
    if (document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence') != null)
        document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = false;
}

function ChangeHideElementsState_DialogHourlyRequestOnAbsence(State) {
    var visibility;
    if (State)
        visibility = 'hidden';
    else
        visibility = 'visible';
    document.getElementById('cmbDoctorName_HourlyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('cmbIllnessName_HourlyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('lblDoctorName_HourlyRequestOnAbsence').style.visibility = visibility;
    document.getElementById('lblIllnessName_HourlyRequestOnAbsence').style.visibility = visibility;
    if (document.getElementById('TlbDefineDoctor_tbHourly_HourlyRequestOnAbsence') != null)
        document.getElementById('TlbDefineDoctor_tbHourly_HourlyRequestOnAbsence').style.visibility = visibility;
    if (document.getElementById('TlbDefineIllness_tbHourly_HourlyRequestOnAbsence') != null)
        document.getElementById('TlbDefineIllness_tbHourly_HourlyRequestOnAbsence').style.visibility = visibility;
}

function ChangePageState_DialogHourlyRequestOnAbsence(state) {
    CurrenPageSate_DialogHourlyRequestOnAbsence = state;
    SetActionMode_HourlyRequestOnAbsence(state);
    if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'Add' || CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete') {
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_enabled(false);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_imageUrl('remove_silver.png');

        //DNN Note:--------------------------------------------------------
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_MissionRequest').set_enabled(true);
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_MissionRequest').set_imageUrl('save.png');
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_MissionRequest').set_enabled(true);
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_MissionRequest').set_imageUrl('cancel.png');

        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_LeaveRequest').set_enabled(true);
        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_LeaveRequest').set_imageUrl('save.png');
        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_LeaveRequest').set_enabled(true);
        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_LeaveRequest').set_imageUrl('cancel.png');
        //END DNN Note:--------------------------------------------------------

        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_enabled(false);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_imageUrl('exit_silver.png');
        document.getElementById('rdbLeaveRequest_HourlyRequestOnAbsence').disabled = true;
        document.getElementById('rdbMissionRequest_HourlyRequestOnAbsence').disabled = true;
        if (state == 'Add') {
            var SelectedItems_GridAbsencePairs_RequestOnAbsence = GridAbsencePairs_RequestOnAbsence.getSelectedItems();
            if (SelectedItems_GridAbsencePairs_RequestOnAbsence.length > 0)
                NavigateAbsensePairs_RequestOnAbsence(SelectedItems_GridAbsencePairs_RequestOnAbsence[0]);
        }
        if (state == 'Delete')
            HourlyRequestOnAbsence_onSave();
    }
    if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'View') {
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_enabled(true);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemDelete_TlbHourlyRequestOnAbsence').set_imageUrl('remove.png');

        //DNN Note:--------------------------------------------------------
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_MissionRequest').set_enabled(false);
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_MissionRequest').set_imageUrl('save.png');
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_MissionRequest').set_enabled(false);
        TlbHourlyRequestOnAbsence_MissionRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_MissionRequest').set_imageUrl('cancel.png');

        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_LeaveRequest').set_enabled(false);
        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemSave_TlbHourlyRequestOnAbsence_LeaveRequest').set_imageUrl('save.png');
        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_LeaveRequest').set_enabled(false);
        TlbHourlyRequestOnAbsence_LeaveRequest.get_items().getItemById('tlbItemCancel_TlbHourlyRequestOnAbsence_LeaveRequest').set_imageUrl('cancel.png');
        //END DNN Note:--------------------------------------------------------

        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_enabled(true);
        TlbHourlyRequestOnAbsence.get_items().getItemById('tlbItemExit_TlbHourlyRequestOnAbsence').set_imageUrl('exit.png');
        document.getElementById('rdbLeaveRequest_HourlyRequestOnAbsence').disabled = false;
        document.getElementById('rdbMissionRequest_HourlyRequestOnAbsence').disabled = false;
        document.getElementById('rdbLeaveRequest_HourlyRequestOnAbsence').checked = false;
        document.getElementById('rdbMissionRequest_HourlyRequestOnAbsence').checked = false;
    }
}

function HourlyRequestOnAbsence_onInsert() {
    ChangePageState_DialogHourlyRequestOnAbsence('Add');
}

function HourlyRequestOnAbsence_onSave() {
    if (CurrenPageSate_DialogHourlyRequestOnAbsence != 'Delete')
        UpdateRequest_HourlyRequestOnAbsence(true);
    else
        ShowDialogConfirm('Delete');
}

function UpdateRequest_HourlyRequestOnAbsence(IsWarning) {
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence = new Object();
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID = '0';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestState = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = '0';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestDate = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay = false;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsFromAndToTimeInNextDay = false;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave = 'false';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.DoctorID = '-1';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IllnessID = '-1';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.MissionLocationID = '-1';
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile = null;
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PersonnelID = '0';

    var ObjDialogHourlyRequestOnAbsence = parent.DialogHourlyRequestOnAbsence.get_value();
    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PersonnelID = ObjDialogHourlyRequestOnAbsence.PersonnelID;

    var SelectedItems_GridRegisteredRequests_HourlyRequestOnAbsence = GridRegisteredRequests_HourlyRequestOnAbsence.getSelectedItems();
    if (SelectedItems_GridRegisteredRequests_HourlyRequestOnAbsence.length > 0)
        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID = SelectedItems_GridRegisteredRequests_HourlyRequestOnAbsence[0].getMember("ID").get_text();

    if (CurrenPageSate_DialogHourlyRequestOnAbsence != 'Delete') {
        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestState = CurrentRequestState_HourlyRequestOnAbsence;
        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestDate = parent.DialogHourlyRequestOnAbsence.get_value().RequestDate;
        switch (CurrentRequestState_HourlyRequestOnAbsence) {
            case 'Leave':
                //DNN Note: 
                if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_id();
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_text();
                }
                else {
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = GetLeaveType_HourlyRequestOnAbsence()[1];
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = GetLeaveType_HourlyRequestOnAbsence()[0];
                }
                //End DNN Note
                ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence');
                ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence');
                if (document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence') != null && document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked)
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay = true;
                if (document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence') != null && document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence').checked)
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsFromAndToTimeInNextDay = true;
                ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description = document.getElementById('txtDescription_Leave_HourlyRequestOnAbsence').value;
                if (ObjRequestAttachment_HourlyRequestOnAbsence != null)
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName;
                //DNN Note: 
                if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem() != null)
                    if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_value() == 'true') {
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave = 'true';
                        if (cmbDoctorName_HourlyRequestOnAbsence.getSelectedItem() != undefined)
                            ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.DoctorID = cmbDoctorName_HourlyRequestOnAbsence.getSelectedItem().get_value();
                        if (cmbIllnessName_HourlyRequestOnAbsence.getSelectedItem() != undefined)
                            ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IllnessID = cmbIllnessName_HourlyRequestOnAbsence.getSelectedItem().get_value();
                    }
                    else
                        ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave = 'false';
                //}
                break;
            case 'Mission':
                //DNN Note: 
                if (cmbMissionType_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = cmbMissionType_HourlyRequestOnAbsence.getSelectedItem().get_value();
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = cmbMissionType_HourlyRequestOnAbsence.getSelectedItem().get_text();
                }
                else {
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID = GetMissionType_HourlyRequestOnAbsence()[1];
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle = GetMissionType_HourlyRequestOnAbsence()[0];
                }
                //End DNN Note
                ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence');
                ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime = GetDuration_TimePicker_HourlyRequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence');
                if (document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence') != null && document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked)
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay = true;
                if (document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence') != null && document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence').checked)
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsFromAndToTimeInNextDay = true;
                ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description = document.getElementById('txtDescription_Mission_HourlyRequestOnAbsence').value;
                if (ObjRequestAttachment_HourlyRequestOnAbsence != null)
                    ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName;

                switch (SelectedMissionLocationType_HourlyRequestOnAbsence) {
                    case 'Normal':
                        if (trvMissionLocation_HourlyRequestOnAbsence.get_selectedNode() != undefined) {
                            if (trvMissionLocation_HourlyRequestOnAbsence.get_selectedNode().get_parentNode() != undefined)
                                ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.MissionLocationID = trvMissionLocation_HourlyRequestOnAbsence.get_selectedNode().get_id();
                        }
                        break;
                    case 'Search':
                        var selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence = cmbMissionLocationSearchResult_HourlyRequestOnAbsence.getSelectedItem();
                        if (selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence != undefined && selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence != null) {
                            ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.MissionLocationID = selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence.get_id();
                        }
                        break;
                    default:
                        break;
                }
                break;
        }
    }
    UpdateRequest_HourlyRequestOnAbsencePage(CharToKeyCode_HourlyRequestOnAbsence(ObjDialogHourlyRequestOnAbsence.RequestCaller), CharToKeyCode_HourlyRequestOnAbsence(ObjDialogHourlyRequestOnAbsence.LoadState), CharToKeyCode_HourlyRequestOnAbsence(CurrenPageSate_DialogHourlyRequestOnAbsence), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestState), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.RequestDate), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay.toString()), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsFromAndToTimeInNextDay.toString()), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.Description), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsSeakLeave), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.DoctorID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IllnessID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.MissionLocationID), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.AttachmentFile), CharToKeyCode_HourlyRequestOnAbsence(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PersonnelID), CharToKeyCode_HourlyRequestOnAbsence(SelectedSubstitute_HourlyRequestOnAbsence), CharToKeyCode_HourlyRequestOnAbsence(IsWarning.toString()));
    DialogWaiting.Show();
}

function UpdateRequest_HourlyRequestOnAbsencePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[6] != '') {
            var objWarning = eval('(' + RetMessage[6] + ')');
            if (objWarning.IsWarning)
                ShowDialogConfirm('Warning', RetMessage[1]);
            else {
                if (Response[2] == 'error' || CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete') {
                    showDialog(RetMessage[0], Response[1], RetMessage[2]);
                }
                if (Response[1] == "ConnectionError") {
                    Response[0] = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
                    Response[1] = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
                }
                if (RetMessage[2] == 'success') {
                    SelectedSubstitute_HourlyRequestOnAbsence = '';
                    HourlyRequest_OnAfterUpdate(Response);
                    ClearList_HourlyRequestOnAbsence();
                    SetBaseState_HourlyRequestOnAbsence();
                }
                else {
                    if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete')
                        ChangePageState_DialogHourlyRequestOnAbsence('View');
                }
            }
        }
        else {
            if (Response[2] == 'error' || CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete') {
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
            }
            if (Response[1] == "ConnectionError") {
                Response[0] = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
                Response[1] = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
            }
            if (RetMessage[2] == 'success') {
                SelectedSubstitute_HourlyRequestOnAbsence = '';
                HourlyRequest_OnAfterUpdate(Response);
                ClearList_HourlyRequestOnAbsence();
                SetBaseState_HourlyRequestOnAbsence();
            }
            else {
                if (CurrenPageSate_DialogHourlyRequestOnAbsence == 'Delete')
                    ChangePageState_DialogHourlyRequestOnAbsence('View');
            }
        }
    }
}
function HourlyRequest_OnAfterUpdate(Response) {
    if (ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence != null) {
        var PreCardTitle = ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.PreCardTitle;
        var FromTime = ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.FromTime;
        var ToTime = ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ToTime;
        var RegisterDate = '';
        var RequestState = '';
        var RequestStateTitle = '';

        if (ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.IsToTimeInNextDay)
            ToTime = '+' + ToTime;

        ObjRequestAttachment_HourlyRequestOnAbsence = null;

        var RegisteredRequestItem = null;
        Fill_GridRegisteredRequests_HourlyRequestOnAbsence();
        //GridRegisteredRequests_HourlyRequestOnAbsence.beginUpdate();
        //switch (CurrenPageSate_DialogHourlyRequestOnAbsence) {
        //    case 'Add':
        //        RegisteredRequestItem = GridRegisteredRequests_HourlyRequestOnAbsence.get_table().addEmptyRow(GridRegisteredRequests_HourlyRequestOnAbsence.get_recordCount());
        //        RegisteredRequestItem.setValue(0, Response[3], false);
        //        GridRegisteredRequests_HourlyRequestOnAbsence.selectByKey(Response[3], 0, false);
        //        RequestStateTitle = GetRequestStateTitle_HourlyRequestOnAbsence(Response[4]);
        //        RequestState = Response[4];
        //        RegisterDate = Response[5];
        //        break;
        //    case 'Delete':
        //        GridRegisteredRequests_HourlyRequestOnAbsence.selectByKey(ObjHourlyRequestOnAbsence_HourlyRequestOnAbsence.ID, 0, false);
        //        GridRegisteredRequests_HourlyRequestOnAbsence.deleteSelected();
        //        break;
        //}
        //if (CurrenPageSate_DialogHourlyRequestOnAbsence != 'Delete') {
        //    RegisteredRequestItem.setValue(1, PreCardTitle, false);
        //    RegisteredRequestItem.setValue(2, FromTime, false);
        //    RegisteredRequestItem.setValue(3, ToTime, false);
        //    RegisteredRequestItem.setValue(4, RegisterDate, false);
        //    RegisteredRequestItem.setValue(5, RequestStateTitle, false);
        //    RegisteredRequestItem.setValue(6, RequestState, false);
        //}
        //GridRegisteredRequests_HourlyRequestOnAbsence.endUpdate();
    }
}

function GetDuration_TimePicker_HourlyRequestOnAbsence(TimePicker) {
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

function HourlyRequestOnAbsence_onCancel() {
    CurrenPageSate_DialogHourlyRequestOnAbsence = 'Cancel';
    SetBaseState_HourlyRequestOnAbsence();
}

function SetBaseState_HourlyRequestOnAbsence() {
    HideDives_HourlyRequestOnAbsence();
    ChangePageState_DialogHourlyRequestOnAbsence('View');
}

function HideDives_HourlyRequestOnAbsence() {
    if (box_LeaveRequest_HourlyRequestOnAbsence_IsShown)
        box_LeaveRequest_HourlyRequestOnAbsence_onShowHide();
    if (box_MissionRequest_HourlyRequestOnAbsence_IsShown)
        box_MissionRequest_HourlyRequestOnAbsence_onShowHide();
}

function DialogHourlyRequestOnAbsence_onClose() {
    parent.document.getElementById('DialogHourlyRequestOnAbsence_IFrame').src = 'WhitePage.aspx';
    parent.eval('DialogHourlyRequestOnAbsence').Close();
    var ObjRequest = parent.DialogHourlyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    if (RequestCaller == 'Grid')
        parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}

function SetPosition_DropDownDives_DialogHourlyRequestOnAbsence() {
    if (parent.parent.CurrentLangID == 'fa-IR') {
        document.getElementById('box_LeaveRequest_HourlyRequestOnAbsence').style.right = '144px';
        document.getElementById('box_MissionRequest_HourlyRequestOnAbsence').style.right = '144px';
    }
    if (parent.parent.CurrentLangID == 'en-US') {
        document.getElementById('box_LeaveRequest_HourlyRequestOnAbsence').style.left = '144px';
        document.getElementById('box_MissionRequest_HourlyRequestOnAbsence').style.left = '144px';
    }
}

function tlbItemDelete_TlbHourlyRequestOnAbsence_onClick() {
    ChangePageState_DialogHourlyRequestOnAbsence('Delete');
}

function tlbItemSave_TlbHourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onSave();
}

function tlbItemCancel_TlbHourlyRequestOnAbsence_onClick() {
    HourlyRequestOnAbsence_onCancel();
}

function GridAbsencePairs_RequestOnAbsence_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridAbsencePairs_RequestOnAbsence').innerHTML = '';
}

function Fill_GridAbsencePairs_RequestOnAbsence() {
    document.getElementById('loadingPanel_GridAbsencePairs_RequestOnAbsence').innerHTML = document.getElementById('hfloadingPanel_GridAbsencePairs_RequestOnAbsence').value;
    var ObjRequest = parent.DialogHourlyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    var PersonnelID = ObjRequest.PersonnelID;
    CallBack_GridAbsencePairs_RequestOnAbsence.callback(CharToKeyCode_HourlyRequestOnAbsence(RequestCaller), CharToKeyCode_HourlyRequestOnAbsence(DateKey), CharToKeyCode_HourlyRequestOnAbsence(RequestDate), CharToKeyCode_HourlyRequestOnAbsence(PersonnelID));
}

function CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_AbsencePairs').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridAbsencePairs_RequestOnAbsence();
    }
}

function Refresh_GridAbsencePairs_RequestOnAbsence() {
    Fill_GridAbsencePairs_RequestOnAbsence();
}

function GridRegisteredRequests_HourlyRequestOnAbsence_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence').innerHTML = '';
}

function Fill_GridRegisteredRequests_HourlyRequestOnAbsence() {
    document.getElementById('loadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfloadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence').value;
    var ObjRequest = parent.DialogHourlyRequestOnAbsence.get_value();
    var RequestCaller = ObjRequest.RequestCaller;
    var DateKey = ObjRequest.DateKey;
    var RequestDate = ObjRequest.RequestDate;
    var PersonnelID = ObjRequest.PersonnelID;
    CallBack_GridRegisteredRequests_HourlyRequestOnAbsence.callback(CharToKeyCode_HourlyRequestOnAbsence(RequestCaller), CharToKeyCode_HourlyRequestOnAbsence(DateKey), CharToKeyCode_HourlyRequestOnAbsence(RequestDate), CharToKeyCode_HourlyRequestOnAbsence(PersonnelID));
}

function CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RegisteredRequests').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRegisteredRequests_HourlyRequestOnAbsence();
    }
}

function Refresh_GridRegisteredRequests_HourlyRequestOnAbsence() {
    Fill_GridRegisteredRequests_HourlyRequestOnAbsence();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_HourlyRequestOnAbsence) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateRequest_HourlyRequestOnAbsence(true);
            break;
        case 'Exit':
            DialogHourlyRequestOnAbsence_onClose();
            break;
        case 'Warning':
            UpdateRequest_HourlyRequestOnAbsence(false);
            DialogConfirm.Close();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_DialogHourlyRequestOnAbsence('View');
}

function ShowDialogConfirm(confirmState, Exception) {
    ConfirmState_HourlyRequestOnAbsence = confirmState;
    switch (ConfirmState_HourlyRequestOnAbsence) {
        case 'Delete':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_HourlyRequestOnAbsence').value;
            break;
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_HourlyRequestOnAbsence').value;
            break;
        case 'Warning':
            document.getElementById('lblConfirm').innerHTML = Exception + ' ' + document.getElementById('hfCloseWarningMessage_HourlyRequestOnAbsence').value;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_HourlyRequestOnAbsence();
}

function CharToKeyCode_HourlyRequestOnAbsence(str) {
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

function cmbLeaveType_HourlyRequestOnAbsence_onChange(sender, e) {
    if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
        if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem().get_value() == 'true')
            ChangeHideElementsState_DialogHourlyRequestOnAbsence(false);
        else
            ChangeHideElementsState_DialogHourlyRequestOnAbsence(true);
    }
}

function cmbLeaveType_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbLeaveType_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbLeaveType_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbLeaveType_HourlyRequestOnAbsence = true;
        CallBack_cmbLeaveType_HourlyRequestOnAbsence.callback();
    }
}

function cmbLeaveType_HourlyRequestOnAbsence_onCollapse(sender, e) {
    //DNN Note
    if (cmbLeaveType_HourlyRequestOnAbsence.getSelectedItem() == undefined)
        document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_Input').value = GetLeaveType_HourlyRequestOnAbsence()[0];
    //End of DNN note
}

function GetLeaveType_HourlyRequestOnAbsence() {
    var LeaveType = document.getElementById('hfLeaveType_HourlyRequestOnAbsence').value;
    var LeaveTypeParts = LeaveType.split('#');
    for (var i = 0; i < LeaveTypeParts.length; i++) {
        if (LeaveTypeParts[i] != '') {
            var LeaveTypePartObj = LeaveTypeParts[i].split(':');
            return LeaveTypePartObj;
        }
    }
}

function GetMissionType_HourlyRequestOnAbsence() {
    var MissionType = document.getElementById('hfMissionType_HourlyRequestOnAbsence').value;
    var MissionTypeParts = MissionType.split('#');
    for (var i = 0; i < MissionTypeParts.length; i++) {
        if (MissionTypeParts[i] != '') {
            var MissionTypePartObj = MissionTypeParts[i].split(':');
            return MissionTypePartObj;
        }
    }
}

function CallBack_cmbLeaveType_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbLeaveType_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_LeaveTypes').value;
    if (error == "") {
        document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbLeaveType_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbLeaveType_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbLeaveType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbMissionType_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbMissionType_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionType_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionType_HourlyRequestOnAbsence = true;
        CallBack_cmbMissionType_HourlyRequestOnAbsence.callback();
    }
}

function cmbMissionType_HourlyRequestOnAbsence_onCollapse(sender, e) {
    //DNN Note
    if (cmbMissionType_HourlyRequestOnAbsence.getSelectedItem() == undefined)
        document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = GetMissionType_HourlyRequestOnAbsence()[0];
    //document.getElementById('cmbMissionType_HourlyRequestOnAbsence_Input').value = document.getElementById('hfcmbAlarm_HourlyRequestOnAbsence').value;
    //End of DNN note
}

function CallBack_cmbMissionType_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbMissionType_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionTypes').value;
    if (error == "") {
        document.getElementById('cmbMissionType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMissionType_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbMissionType_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbMissionType_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function trvMissionLocation_HourlyRequestOnAbsence_onNodeSelect(sender, e) {
    SelectedMissionLocationType_HourlyRequestOnAbsence = 'Normal';
    if (document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_Input') != undefined && document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_Input') != null)
        cmbMissionLocation_HourlyRequestOnAbsence.set_text(e.get_node().get_text());
    if (document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_TextBox') != undefined && document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_TextBox') != null)
        document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_TextBox').innerHTML = e.get_node().get_text();
    cmbMissionLocation_HourlyRequestOnAbsence.collapse();
}

function cmbMissionLocation_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (trvMissionLocation_HourlyRequestOnAbsence.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMissionLocation_HourlyRequestOnAbsence = true;
        CallBack_cmbMissionLocation_HourlyRequestOnAbsence.callback();
    }
}

function CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbMissionLocation_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionLocations').value;
    if (error == "") {
        document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMissionLocation_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbMissionLocation_HourlyRequestOnAbsence.expand();
        ChangeDirection_trvMissionLocation_HourlyRequestOnAbsence();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function cmbDoctorName_HourlyRequestOnAbsence_onExpand(sender, e) {
    SetPosition_cmbDoctorName_HourlyRequestOnAbsence();
    if (cmbDoctorName_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctorName_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDoctorName_HourlyRequestOnAbsence = true;
        CallBack_cmbDoctorName_HourlyRequestOnAbsence.callback();
    }
}

function CallBack_cmbDoctorName_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbDoctorName_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    SetPosition_cmbDoctorName_HourlyRequestOnAbsence();
    var error = document.getElementById('ErrorHiddenField_Doctors').value;
    if (error == "") {
        document.getElementById('cmbDoctorName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDoctorName_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbDoctorName_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDoctorName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function SetPosition_cmbDoctorName_HourlyRequestOnAbsence() {
    if (parent.parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbDoctorName_HourlyRequestOnAbsence_DropDown').style.left = document.getElementById('Mastertbl_HourlyRequestOnAbsence').clientWidth - 310 + 'px';
    }
    if (parent.parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbDoctorName_HourlyRequestOnAbsence_DropDown').style.left = '30px';
    }
}

function cmbIllnessName_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbIllnessName_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnessName_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbIllnessName_HourlyRequestOnAbsence = true;
        CallBack_cmbIllnessName_HourlyRequestOnAbsence.callback();
    }
}

function CallBack_cmbIllnessName_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbIllnessName_HourlyRequestOnAbsence.dispose();
}

function CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Illnesses').value;
    if (error == "") {
        document.getElementById('cmbIllnessName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbIllnessName_HourlyRequestOnAbsence_DropImage').mousedown();
        cmbIllnessName_HourlyRequestOnAbsence.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbIllnessName_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}

function GetRequestStateTitle_HourlyRequestOnAbsence(requestState) {
    var RequestStates = document.getElementById('hfRequestStates_HourlyRequestOnAbsence').value.split('#');
    for (var i = 0; i < RequestStates.length; i++) {
        var requestStateObj = RequestStates[i].split(':');
        if (requestStateObj.length > 1) {
            if (requestStateObj[1] == requestState.toString())
                return requestStateObj[0];
        }
    }
}

function tlbItemExit_TlbHourlyRequestOnAbsence_onClick() {
    ShowDialogConfirm('Exit');
}

function SetActionMode_HourlyRequestOnAbsence(state) {
    document.getElementById('ActionMode_HourlyRequestOnAbsence').innerHTML = document.getElementById("hf" + state + "_HourlyRequestOnAbsence").value;
}

function HourlyRequestOnAbsenceForm_onKeyDown(event) {
    var activeID = null;
    if (event.keyCode == 38 || event.keyCode == 40) {
        activeID = document.activeElement.id;
        CheckTimePickerState_HourlyRequestOnAbsence(activeID);
    }
}

function CheckTimePickerState_HourlyRequestOnAbsence(TimeSelector) {
    if (((TimeSelector == 'TimeSelector_FromHour_Leave_HourlyRequestOnAbsence_txtHour' || TimeSelector == 'TimeSelector_ToHour_Leave_HourlyRequestOnAbsence_txtHour' || TimeSelector == 'TimeSelector_FromHour_Mission_HourlyRequestOnAbsence_txtHour' || TimeSelector == 'TimeSelector_ToHour_Mission_HourlyRequestOnAbsence_txtHour') && (document.getElementById(TimeSelector).value == '-1' || isNaN(document.getElementById(TimeSelector).value))) || ((TimeSelector == 'TimeSelector_FromHour_Leave_HourlyRequestOnAbsence_txtMinute' || TimeSelector == 'TimeSelector_ToHour_Leave_HourlyRequestOnAbsence_txtMinute' || TimeSelector == 'TimeSelector_FromHour_Mission_HourlyRequestOnAbsence_txtMinute' || TimeSelector == 'TimeSelector_ToHour_Mission_HourlyRequestOnAbsence_txtMinute') && isNaN(document.getElementById(TimeSelector).value))) {
        document.getElementById(TimeSelector).value = zeroTime;
        return;
    }
}

function CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function ShowConnectionError_HourlyRequestOnAbsence() {
    var error = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
    var errorBody = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function CollapseControls_HourlyRequestOnAbsence() {
    cmbLeaveType_HourlyRequestOnAbsence.collapse();
    cmbMissionType_HourlyRequestOnAbsence.collapse();
    cmbPersonnel_HourlyRequestOnAbsence.collapse();
}

function tlbItemFormReconstruction_TlbHourlyRequestOnAbsence_onClick() {
    var ObjDialogHourlyRequestOnAbsence = parent.DialogHourlyRequestOnAbsence.get_value();
    var field = ObjDialogHourlyRequestOnAbsence.Field;
    DialogHourlyRequestOnAbsence_onClose();
    parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.ShowRelativeDialog_MasterMonthlyOperation(field);
}

function GridAbsencePairs_RequestOnAbsence_onItemSelect(sender, e) {
    //DNN note
    ObjSelectedAbsensePairsItem = e.get_item();
    NavigateAbsensePairs_RequestOnAbsence(ObjSelectedAbsensePairsItem);
    //END Of DNN Note
}

function NavigateAbsensePairs_RequestOnAbsence(selectedAbsensePairsItem) {
    if (selectedAbsensePairsItem != undefined) {
        var fromTime = selectedAbsensePairsItem.getMember('From').get_text();
        var toTime = selectedAbsensePairsItem.getMember('To').get_text();
        if (fromTime != '' && toTime != '') {
            switch (CurrentRequestState_HourlyRequestOnAbsence) {
                case 'Leave':
                    if (document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence') != null && fromTime.toString().indexOf('+') >= 0 && toTime.toString().indexOf('+') >= 0)
                        document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = true;
                    else
                        if (document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence') != null && toTime.toString().indexOf('+') >= 0)
                            document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = true;
                    break;
                case 'Mission':
                    if (document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence') != null && fromTime.toString().indexOf('+') >= 0 && toTime.toString().indexOf('+') >= 0)
                        document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = true;
                    else
                        if (document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence') != null && toTime.toString().indexOf('+') >= 0)
                            document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = true;
                    break;
            }
            fromTime = fromTime.toString().replace('+', '');
            toTime = toTime.toString().replace('+', '');
        }
        else
            if (toTime != '') {
                switch (CurrentRequestState_HourlyRequestOnAbsence) {
                    case 'Leave':
                        if (document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence') != null && toTime.toString().indexOf('+') >= 0)
                            document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = true;
                        break;
                    case 'Mission':
                        if (document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence') != null && toTime.toString().indexOf('+') >= 0)
                            document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = true;
                        break;
                }
                toTime = toTime.toString().replace('+', '');
            }

        if (fromTime != '') {
            switch (CurrentRequestState_HourlyRequestOnAbsence) {
                case 'Leave':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_FromHour_Leave_HourlyRequestOnAbsence', fromTime);
                    break;
                case 'Mission':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_FromHour_Mission_HourlyRequestOnAbsence', fromTime);
                    break;
            }
        }
        if (toTime != '') {
            switch (CurrentRequestState_HourlyRequestOnAbsence) {
                case 'Leave':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_ToHour_Leave_HourlyRequestOnAbsence', toTime);
                    break;
                case 'Mission':
                    SetDuration_TimePicker_RequestOnAbsence('TimeSelector_ToHour_Mission_HourlyRequestOnAbsence', toTime);
                    break;
            }
        }

    }
}

function SetDuration_TimePicker_RequestOnAbsence(TimePicker, strTime) {
    if (strTime == "")
        strTime = '00:00';
    var arTime_Shift = strTime.split(':');
    for (var i = 0; i < 2; i++) {
        if (arTime_Shift[i].length < 2)
            arTime_Shift[i] = '0' + arTime_Shift[i];
    }
    document.getElementById(TimePicker + '_txtHour').value = arTime_Shift[0];
    document.getElementById(TimePicker + '_txtMinute').value = arTime_Shift[1];
}

function tlbItemHelp_TlbHourlyRequestOnAbsence_onClick() {
    LoadHelpPage('tlbItemHelp_TlbHourlyRequestOnAbsence');
}

function AttachmentUploader_Leave_HourlyRequestOnAbsence_OnPreFileUpload() {
    var uploader = $('#Subgurim_AttachmentUploader_Leave_HourlyRequestOnAbsence').find('div:first').find('iframe:first').contents().find('#file')[0];
    if (uploader != undefined && uploader != null && uploader.files != undefined && uploader.files != null && uploader.files.length > 0) {
        var filesize = uploader.files[0].size;
        var requestMaxLength = parseInt(document.getElementById('hfMRL').value) * 1000;
        if (filesize > requestMaxLength) {
            var errorMessage = document.getElementById('hfRequestMaxLength_HourlyRequestOnAbsence').value + ' ' + (requestMaxLength / Math.pow(10, 6)).toFixed(2);
            showDialog(document.getElementById('hfErrorType_HourlyRequestOnAbsence').value, errorMessage, 'error');
            Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence.callback();
        }
    }
}

function AttachmentUploader_Leave_HourlyRequestOnAbsence_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_HourlyRequestOnAbsence == null)
        ObjRequestAttachment_HourlyRequestOnAbsence = new Object();
    ObjRequestAttachment_HourlyRequestOnAbsence = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_HourlyRequestOnAbsence.IsErrorOccured)
        message = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_HourlyRequestOnAbsence.Message;
        ObjRequestAttachment_HourlyRequestOnAbsence = null;
    }
    document.getElementById('tdAttachmentName_Leave_HourlyRequestOnAbsence').innerHTML = message;
    Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence.callback();
}

function AttachmentUploader_Mission_HourlyRequestOnAbsence_OnPreFileUpload() {
    var uploader = $('#Subgurim_AttachmentUploader_Mission_HourlyRequestOnAbsence').find('div:first').find('iframe:first').contents().find('#file')[0];
    if (uploader != undefined && uploader != null && uploader.files != undefined && uploader.files != null && uploader.files.length > 0) {
        var filesize = uploader.files[0].size;
        var requestMaxLength = parseInt(document.getElementById('hfMRL').value) * 1000;
        if (filesize > requestMaxLength) {
            var errorMessage = document.getElementById('hfRequestMaxLength_HourlyRequestOnAbsence').value + ' ' + (requestMaxLength / Math.pow(10, 6)).toFixed(2);
            showDialog(document.getElementById('hfErrorType_HourlyRequestOnAbsence').value, errorMessage, 'error');
            Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence.callback();
        }
    }
}

function AttachmentUploader_Mission_HourlyRequestOnAbsence_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_HourlyRequestOnAbsence == null)
        ObjRequestAttachment_HourlyRequestOnAbsence = new Object();
    ObjRequestAttachment_HourlyRequestOnAbsence = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_HourlyRequestOnAbsence.IsErrorOccured)
        message = ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_HourlyRequestOnAbsence.Message;
        ObjRequestAttachment_HourlyRequestOnAbsence = null;
    }
    document.getElementById('tdAttachmentName_Mission_HourlyRequestOnAbsence').innerHTML = message;
    Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence.callback();
}

function Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_Leave_HourlyRequestOnAbsenceadd('1', '4');
}

function Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_Mission_HourlyRequestOnAbsenceadd('1', '4');
}

function Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}

function tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence_onClick() {
    DeleteRequestAttachment_HourlyRequestOnAbsence();
}

function DeleteRequestAttachment_HourlyRequestOnAbsence() {
    if (ObjRequestAttachment_HourlyRequestOnAbsence != null && ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName != null && ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName != '')
        DeleteRequestAttachment_HourlyRequestOnAbsencePage(CharToKeyCode_HourlyRequestOnAbsence(ObjRequestAttachment_HourlyRequestOnAbsence.RequestAttachmentSavedName));
}
function DeleteRequestAttachment_HourlyRequestOnAbsencePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
            Response[1] = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
        }
        if (RetMessage[2] == 'success') {
            ObjRequestAttachment_HourlyRequestOnAbsence = null;
            switch (CurrentRequestState_HourlyRequestOnAbsence) {
                case 'Leave':
                    document.getElementById('tdAttachmentName_Leave_HourlyRequestOnAbsence').innerHTML = '';
                    break;
                case 'Mission':
                    document.getElementById('tdAttachmentName_Mission_HourlyRequestOnAbsence').innerHTML = '';
                    break;
            }
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function ChangeDirection_trvMissionLocation_HourlyRequestOnAbsence() {
    if (parent.parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvMissionLocation_HourlyRequestOnAbsence').style.direction = 'ltr';
}

function trvMissionLocation_HourlyRequestOnAbsence_onNodeExpand(sender, e) {
    ChangeDirection_trvMissionLocation_HourlyRequestOnAbsence();
}

function tlbItemAdd_TlbDefineDoctor_tbHourly_HourlyRequestOnAbsence_onClick() {
    ShowDialogDefinePhysicians('Hourly');
}

function ShowDialogDefinePhysicians(state) {
    var ObjDialogDefinePhysicians = new Object();
    ObjDialogDefinePhysicians.Caller = 'HourlyRequestOnAbsence';
    ObjDialogDefinePhysicians.RequestCaller = parent.DialogHourlyRequestOnAbsence.get_value().RequestCaller;
    ObjDialogDefinePhysicians.State = state;
    ObjDialogDefinePhysicians.LoadState = parent.DialogHourlyRequestOnAbsence.get_value().LoadState;
    ObjDialogDefinePhysicians.UserCaller = 'NormalUser';
    parent.parent.DialogDefinePhysicians.set_value(ObjDialogDefinePhysicians);
    parent.parent.DialogDefinePhysicians.Show();
}

function Refresh_cmbDoctors_HourlyRequestOnAbsence(state) {
    CallBack_cmbDoctorName_HourlyRequestOnAbsence.callback();

}
function tlbItemAdd_TlbDefineIllness_tbHourly_HourlyRequestOnAbsence_onClick() {
    ShowDialogDefineIllness('Hourly');
}

function ShowDialogDefineIllness(state) {
    var ObjDialogDefineIllness = new Object();
    ObjDialogDefineIllness.Caller = 'HourlyRequestOnAbsence';
    ObjDialogDefineIllness.RequestCaller = parent.DialogHourlyRequestOnAbsence.get_value().RequestCaller;
    ObjDialogDefineIllness.State = state;
    ObjDialogDefineIllness.LoadState = parent.DialogHourlyRequestOnAbsence.get_value().LoadState;
    ObjDialogDefineIllness.UserCaller = 'NormalUser';
    parent.parent.DialogDefineIllness.set_value(ObjDialogDefineIllness);
    parent.parent.DialogDefineIllness.Show();
}
function Refresh_cmbIllness_HourlyRequestOnAbsence(state) {
    CallBack_cmbIllnessName_HourlyRequestOnAbsence.callback();
}
function tlbItemMissionSearch_TlbMissionSearch_HourlyRequestOnAbsence_onClick() {
    DialogMissionLocationSearch.Show();
}
function CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbMissionLocationSearchResult_HourlyRequestOnAbsence.dispose();
}
function ChangeControlDirection_HourlyRequestOnAbsence(ctrl) {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    if (ctrl == 'All') {
        document.getElementById('Mastertbl_DialogMissionLocationSearch').dir =
        document.getElementById('cmbMissionLocationSearchResult_HourlyRequestOnAbsence_DropDownContent').dir =
        direction;
    }
    else
        document.getElementById(ctrl).style.direction = direction;
}
function CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MissionLocationSearchResult_HourlyRequestOnAbsence').value;
    if (error == "") {
        cmbMissionLocationSearchResult_HourlyRequestOnAbsence.expand();
        ChangeControlDirection_HourlyRequestOnAbsence('cmbMissionLocationSearchResult_HourlyRequestOnAbsence_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}
function CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}
function tlbItemSearch_TlbMissionLocationSearch_HourlyRequestOnAbsence_onClick() {
    Fill_cmbMissionLocationSearchResult_HourlyRequestOnAbsence();
}

function Fill_cmbMissionLocationSearchResult_HourlyRequestOnAbsence() {

    var SearchTerm = document.getElementById('txtSearchTermMissionLocation_HourlyRequestOnAbsence').value;
    CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence.callback(CharToKeyCode_HourlyRequestOnAbsence(SearchTerm));
}

function tlbItemSave_TlbMissionLocationSearch_HourlyRequestOnAbsence_onClick() {
    var selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence = cmbMissionLocationSearchResult_HourlyRequestOnAbsence.getSelectedItem();
    if (selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence != undefined && selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence != null) {

        if (document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_Input') != undefined && document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_Input') != null)
            document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_Input').value = selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence.get_text();
        if (document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_TextBox') != undefined && document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_TextBox') != null)
            document.getElementById('cmbMissionLocation_HourlyRequestOnAbsence_TextBox').innerHTML = selectedItem_cmbMissionLocationSearchResult_HourlyRequestOnAbsence.get_text();
        SelectedMissionLocationType_HourlyRequestOnAbsence = 'Search';
    }
    DialogMissionLocationSearch.Close();
}
function Search_cmbMissionLocation_HourlyRequestOnAbsence() {
    ShowDialogMissionLocationSearch();
}
function ShowDialogMissionLocationSearch() {

    DialogMissionLocationSearch.Show();
}
function tlbItemExit_TlbMissionLocationSearch_HourlyRequestOnAbsence_onClick() {
    DialogMissionLocationSearch.Close();
}
function cmbPersonnel_HourlyRequestOnAbsence_onChange(sender, e) {
    if (cmbPersonnel_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
    }
}
function cmbPersonnel_HourlyRequestOnAbsence_onExpand(sender, e) {
    if (cmbPersonnel_HourlyRequestOnAbsence.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_HourlyRequestOnAbsence == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_HourlyRequestOnAbsence = true;
        SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(0);
    }
}
function SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(pageIndex) {
    CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence = pageIndex;
    Fill_cmbPersonnel_HourlyRequestOnAbsence(pageIndex);
}
function Fill_cmbPersonnel_HourlyRequestOnAbsence(pageIndex) {

    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_HourlyRequestOnAbsence').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_HourlyRequestOnAbsence) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_HourlyRequestOnAbsence = SearchTermConditions = document.getElementById('txtPersonnelSearch_HourlyRequestOnAbsence').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_HourlyRequestOnAbsence;
            break;
    }
    ComboBox_onBeforeLoadData('cmbPersonnel_HourlyRequestOnAbsence');
    CallBack_cmbPersonnel_HourlyRequestOnAbsence.callback(CharToKeyCode_HourlyRequestOnAbsence(LoadState_cmbPersonnel_HourlyRequestOnAbsence), CharToKeyCode_HourlyRequestOnAbsence(pageSize.toString()), CharToKeyCode_HourlyRequestOnAbsence(pageIndex.toString()), CharToKeyCode_HourlyRequestOnAbsence(SearchTermConditions));
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
function CallBack_cmbPersonnel_HourlyRequestOnAbsence_onBeforeCallback(sender, e) {
    cmbPersonnel_HourlyRequestOnAbsence.dispose();
}
function CallBack_cmbPersonnel_HourlyRequestOnAbsence_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_HourlyRequestOnAbsence').value;
    document.getElementById('clmnBarCode_cmbPersonnel_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_HourlyRequestOnAbsence').value;
    document.getElementById('clmnCardNum_cmbPersonnel_HourlyRequestOnAbsence').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_HourlyRequestOnAbsence').value;

    var error = document.getElementById('ErrorHiddenField_Personnel_HourlyRequestOnAbsence').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_HourlyRequestOnAbsence_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_HourlyRequestOnAbsence_DropImage').mousedown();
        else
            cmbPersonnel_HourlyRequestOnAbsence.expand();
        ChangeControlDirection_HourlyRequestOnAbsence('cmbPersonnel_HourlyRequestOnAbsence_DropDown');
        var personnelCount = document.getElementById('hfPersonnelCount_HourlyRequestOnAbsence').value;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_HourlyRequestOnAbsence_DropDown').style.display = 'none';
    }
}
function CallBack_cmbPersonnel_HourlyRequestOnAbsence_onCallbackError(sender, e) {
    ShowConnectionError_HourlyRequestOnAbsence();
}
function ShowConnectionError_HourlyRequestOnAbsence() {
    var error = document.getElementById('hfErrorType_HourlyRequestOnAbsence').value;
    var errorBody = document.getElementById('hfConnectionError_HourlyRequestOnAbsence').value;
    showDialog(error, errorBody, 'error');
}
function tlbItemSearch_TlbSearchPersonnel_HourlyRequestOnAbsence_onClick() {
    LoadState_cmbPersonnel_HourlyRequestOnAbsence = 'Search';
    CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence = 0;
    SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(0);
}
function tlbItemAdvancedSearch_TlbAdvancedSearch_HourlyRequestOnAbsence_onClick() {
    LoadState_cmbPersonnel_HourlyRequestOnAbsence = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence = 0;
    ShowDialogPersonnelSearch('HourlyRequestOnAbsence');
}
function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.parent.DialogPersonnelSearch.Show();
    CollapseControls_HourlyRequestOnAbsence();
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick() {
    ChangeLoadState_cmbPersonnel_HourlyRequestOnAbsence('Normal');
}
function ChangeLoadState_cmbPersonnel_HourlyRequestOnAbsence(state) {
    LoadState_cmbPersonnel_HourlyRequestOnAbsence = state;
    SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(0);
}
function tlbItemFirst_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick() {
    SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(0);
}
function tlbItemBefore_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick() {
    if (CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence != 0) {
        CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence = CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence - 1;
        SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence);
    }
}
function tlbItemNext_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick() {
    if (CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence < parseInt(document.getElementById('hfPersonnelPageCount_HourlyRequestOnAbsence').value) - 1) {
        CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence = CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence + 1;
        SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(CurrentPageIndex_cmbPersonnel_HourlyRequestOnAbsence);
    }
}
function tlbItemLast_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick() {
    SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(parseInt(document.getElementById('hfPersonnelPageCount_HourlyRequestOnAbsence').value) - 1);
}
function tlbSubstitute_HourlyRequestOnAbsence_onClick() {
    CollapseControls_HourlyRequestOnAbsence();
    ShowDialogSubstitute();
}
function tlbItemSave_TlbSubstitute_onClick() {
    if (cmbPersonnel_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
        var ObjSubstitute = cmbPersonnel_HourlyRequestOnAbsence.getSelectedItem().get_value();
        ObjSubstitute = eval('(' + ObjSubstitute + ')');
        ObjSelectedSubstitute_HourlyRequestOnAbsence = new Object();
        ObjSelectedSubstitute_HourlyRequestOnAbsence.Id = '0';
        ObjSelectedSubstitute_HourlyRequestOnAbsence.Name = '';
        ObjSelectedSubstitute_HourlyRequestOnAbsence.Barcode = '';
        ObjSelectedSubstitute_HourlyRequestOnAbsence.Id = ObjSubstitute.ID;
        ObjSelectedSubstitute_HourlyRequestOnAbsence.Name = ObjSubstitute.Name;
        ObjSelectedSubstitute_HourlyRequestOnAbsence.Barcode = ObjSubstitute.Barcode;
        SelectedSubstitute_HourlyRequestOnAbsence = '{"Id" : "' + ObjSelectedSubstitute_HourlyRequestOnAbsence.Id + '", "Name" : "' + ObjSelectedSubstitute_HourlyRequestOnAbsence.Name + '" , "BarCode" : "' + ObjSelectedSubstitute_HourlyRequestOnAbsence.Barcode + '"}'
    }
    DialogSubstitute.Close();
}
function tlbItemRefuse_TlbSubstitute_onClick() {
    SelectedSubstitute_HourlyRequestOnAbsence = '';
    ClearList_HourlyRequestOnAbsence();
    cmbPersonnel_HourlyRequestOnAbsence.collapse();
    DialogSubstitute.Close();
}
function ClearList_HourlyRequestOnAbsence() {
    if (cmbPersonnel_HourlyRequestOnAbsence.getSelectedItem() != undefined) {
        document.getElementById('cmbPersonnel_HourlyRequestOnAbsence_Input').value = '';
        cmbPersonnel_HourlyRequestOnAbsence.unSelect();
    }
}
function ShowDialogSubstitute() {
    DialogSubstitute.Show();
}
function DialogSubstitute_OnShow(sender, e) {
    var containerID = null;
    switch (CurrentRequestState_HourlyRequestOnAbsence) {
        case 'Leave':
            containerID = 'box_LeaveRequest_HourlyRequestOnAbsence';
            break;
        case 'Mission':
            containerID = 'box_MissionRequest_HourlyRequestOnAbsence';
            break;
    }
    document.getElementById('DialogSubstitute').style.left = document.getElementById(containerID).clientWidth - document.getElementById('DialogSubstitute').clientWidth + document.getElementById(containerID).offsetLeft + 'px';
    document.getElementById('DialogSubstitute').style.top = document.getElementById(containerID).offsetTop + 'px';
}
function txtPersonnelSearch_HourlyRequestOnAbsence_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbSearchPersonnel_HourlyRequestOnAbsence_onClick();
    }
}

function HourlyRequestOnAbsence_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_HourlyRequestOnAbsence = SearchTerm;
    SetPageIndex_cmbPersonnel_HourlyRequestOnAbsence(0);
}

function chbToHourInNextDay_Mission_HourlyRequestOnAbsence_onClick() {
    if (document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked)
        document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = false;
}

function chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence_onclick() {
    if (document.getElementById('chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence').checked)
        document.getElementById('chbToHourInNextDay_Mission_HourlyRequestOnAbsence').checked = false;
}

function chbToHourInNextDay_Leave_HourlyRequestOnAbsence_onclick() {
    if (document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked)
        document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = false;
}

function chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence_onclick() {
    if (document.getElementById('chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence').checked)
        document.getElementById('chbToHourInNextDay_Leave_HourlyRequestOnAbsence').checked = false;
}




























