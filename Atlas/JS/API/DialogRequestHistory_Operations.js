var zeroTime = '00';
NullTime_RequestHistory = '';
var ObjRequestAttachment_RequestHistory = null;
function GetBoxesHeaders_RequestHistory() {
    parent.document.getElementById('Title_DialogRequestHistory').innerHTML = document.getElementById('hfTitle_DialogRequestHistory').value;
    document.getElementById('header_RequestHistory_RequestHistory').innerHTML = document.getElementById('hfheader_RequestHistory_RequestHistory').value;
}
 
function Fill_GridRequestHistory_RequestHistory() {
    document.getElementById('loadingPanel_GridRequestHistory_RequestHistory').innerHTML = document.getElementById('hfloadingPanel_GridRequestHistory_RequestHistory').value;
    var ObjRequestHistory = parent.DialogRequestHistory.get_value();
    var RequestID = ObjRequestHistory.RequestID;
    CallBack_GridRequestHistory_RequestHistory.callback(CharToKeyCode_RequestHistory(RequestID));
}

function GridRequestHistory_RequestHistory_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridRequestHistory_RequestHistory').innerHTML = '';
}

function CallBack_GridRequestHistory_RequestHistory_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_History_RequestHistory').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_GridRequestHistory_RequestHistory();
    }
}

function CharToKeyCode_RequestHistory(str) {
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

function SetCellTitle_GridRequestHistory_RequestHistory(Key) {
    strListObj = document.getElementById('hfRequestHistory_RequestHistory').value.split('#');
    for (var i = 0; i < strListObj.length; i++) {
        strListItemObj = strListObj[i].split(':');
        if (strListItemObj.length > 1) {
            if (strListItemObj[1] == Key.toString())
                return strListItemObj[0];
        }
    }
}

function SetClmnImage_GridRequestHistory_RequestHistory(Key) {
    switch (Key.toString()) {
        case '1':
            cellImage = 'Images/Grid/save.png';
            break;
        case '2':
            cellImage = 'Images/Grid/cancel.png';
            break;
        case '3':
            cellImage = 'Images/Grid/waiting_flow.png';
            break;
        case '4':
            cellImage = 'Images/Grid/remove.png';
            break;
    }
    return cellImage;
}

function Refresh_GridRequestHistory_RequestHistory() {
    GetRequestHistory_RequestHistory();
}

function CallBack_GridRequestHistory_RequestHistory_onCallbackError(sender, e) {
    ShowConnectionError_RequestHistory();
}

function ShowConnectionError_RequestHistory() {
    var error = document.getElementById('hfErrorType_RequestHistory').value;
    var errorBody = document.getElementById('hfConnectionError_RequestHistory').value;
    showDialog(error, errorBody, 'error');
}


function GetRequestHistory_RequestHistory() {
    //DNN Note
    try {
        var ObjRequestHistory = parent.DialogRequestHistory.get_value();
    } catch (e) {
        var ObjRequestHistory = parent.DialogRequestHistoryDNN.get_value();
    }

    var RequestID = ObjRequestHistory.RequestID;
    if (RequestID != 0)
        Fill_GridRequestHistory_RequestHistory();
}

function btn_gdpRequestDate_tbHourly_RequestHistory_OnClick(event) {
    if (gCalRequestDate_tbHourly_RequestHistory.get_popUpShowing()) {
        gCalRequestDate_tbHourly_RequestHistory.hide();
    }
    else {
        gCalRequestDate_tbHourly_RequestHistory.setSelectedDate(gdpRequestDate_tbHourly_RequestHistory.getSelectedDate());
        gCalRequestDate_tbHourly_RequestHistory.show();
    }
}

function gCalRequestDate_tbHourly_RequestHistory_OnChange(sender, e) {
    var RequestDate = gCalRequestDate_tbHourly_RequestHistory.getSelectedDate();
    gdpRequestDate_tbHourly_RequestHistory.setSelectedDate(RequestDate);
}

function gCalRequestDate_tbHourly_RequestHistory_OnLoad(sender, e) {
    window.gCalRequestDate_tbHourly_RequestHistory.PopUpObject.z = 25000000;
}

function gdpRequestDate_tbHourly_RequestHistory_OnDateChange(sender, e) {
    var RequestDate = gdpRequestDate_tbHourly_RequestHistory.getSelectedDate();
    gCalRequestDate_tbHourly_RequestHistory.setSelectedDate(RequestDate);
}

function gdpFromDate_tbDaily_RequestHistory_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_tbDaily_RequestHistory.getSelectedDate();
    gCalFromDate_tbDaily_RequestHistory.setSelectedDate(FromDate);
}

function gCalFromDate_tbDaily_RequestHistory_OnChange(sender, e) {
    var FromDate = gCalFromDate_tbDaily_RequestHistory.getSelectedDate();
    gdpFromDate_tbDaily_RequestHistory.setSelectedDate(FromDate);
}

function gCalFromDate_tbDaily_RequestHistory_OnLoad(sender, e) {
    window.gCalFromDate_tbDaily_RequestHistory.PopUpObject.z = 25000000;
}

function gdpToDate_tbDaily_RequestHistory_OnDateChange(sender, e) {
    var ToDate = gdpToDate_tbDaily_RequestHistory.getSelectedDate();
    gCalToDate_tbDaily_RequestHistory.setSelectedDate(ToDate);
}

function btn_gdpToDate_tbDaily_RequestHistory_OnMouseUp(event) {
    if (gCalToDate_tbDaily_RequestHistory.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_tbDaily_RequestHistory_OnChange(sender, e) {
    var ToDate = gCalToDate_tbDaily_RequestHistory.getSelectedDate();
    gdpToDate_tbDaily_RequestHistory.setSelectedDate(ToDate);
}

function gdpFromDate_tbOverTime_RequestHistory_OnDateChange(sender, e) {
    var FromDate = gdpFromDate_tbOverTime_RequestHistory.getSelectedDate();
    gCalFromDate_tbOverTime_RequestHistory.setSelectedDate(FromDate);
}

function btn_gdpFromDate_tbOverTime_RequestHistory_OnClick(event) {
    if (gCalFromDate_tbOverTime_RequestHistory.get_popUpShowing()) {
        gCalFromDate_tbOverTime_RequestHistory.hide();
    }
    else {
        gCalFromDate_tbOverTime_RequestHistory.setSelectedDate(gdpFromDate_tbOverTime_RequestHistory.getSelectedDate());
        gCalFromDate_tbOverTime_RequestHistory.show();
    }
}

function gCalFromDate_tbOverTime_RequestHistory_OnChange(sender, e) {
    var FromDate = gCalFromDate_tbOverTime_RequestHistory.getSelectedDate();
    gdpFromDate_tbOverTime_RequestHistory.setSelectedDate(FromDate);
}

function gCalFromDate_tbOverTime_RequestHistory_OnLoad(sender, e) {
    window.gCalFromDate_tbOverTime_RequestHistory.PopUpObject.z = 25000000;
}

function gdpToDate_tbOverTime_RequestHistory_OnDateChange(sender, e) {
    var ToDate = gdpToDate_tbOverTime_RequestHistory.getSelectedDate();
    gCalToDate_tbOverTime_RequestHistory.setSelectedDate(ToDate);
}

function btn_gdpToDate_tbOverTime_RequestHistory_OnClick(event) {
    if (gCalToDate_tbOverTime_RequestHistory.get_popUpShowing()) {
        gCalToDate_tbOverTime_RequestHistory.hide();
    }
    else {
        gCalToDate_tbOverTime_RequestHistory.setSelectedDate(gdpToDate_tbOverTime_RequestHistory.getSelectedDate());
        gCalToDate_tbOverTime_RequestHistory.show();
    }

}

function btn_gdpToDate_tbOverTime_RequestHistory_OnMouseUp(event) {
    if (gCalToDate_tbOverTime_RequestHistory.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_tbOverTime_RequestHistory_OnChange(sender, e) {
    var ToDate = gCalToDate_tbOverTime_RequestHistory.getSelectedDate();
    gdpToDate_tbOverTime_RequestHistory.setSelectedDate(ToDate);
}

function gCalToDate_tbOverTime_RequestHistory_OnLoad(sender, e) {
    window.gCalToDate_tbOverTime_RequestHistory.PopUpObject.z = 25000000;
}

function btn_gdpRequestDate_tbHourly_RequestHistory_OnMouseUp(event) {
    if (gCalRequestDate_tbHourly_RequestHistory.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function initTimePickers_RequestHistory(pageState) {
    SetButtonImages_TimeSelectors_DialogRequestHistory();
    ChangeTimePickersEnabled_RequestHistory(pageState, 'enable');
    ResetTimepickers_RequestHistory(pageState);
}

function SetButtonImages_TimeSelectors_DialogRequestHistory() {
    SetButtonImages_TimeSelector_DialogRequestHistory('TimeSelector_FromHour_tbHourly_RequestHistory');
    SetButtonImages_TimeSelector_DialogRequestHistory('TimeSelector_ToHour_tbHourly_RequestHistory');
    SetButtonImages_TimeSelector_DialogRequestHistory('TimeSelector_FromHour_tbOverTime_RequestHistory');
    SetButtonImages_TimeSelector_DialogRequestHistory('TimeSelector_ToHour_tbOverTime_RequestHistory');
    SetButtonImages_TimeSelector_DialogRequestHistory('TimeSelector_Duration_tbOverTime_RequestHistory');
}

function ChangeTimePickersEnabled_RequestHistory(pageState, state) {
    ChangeTimePickerEnabled_RequestHistory(pageState, 'TimeSelector_FromHour_tbHourly_RequestHistory', state);
    ChangeTimePickerEnabled_RequestHistory(pageState, 'TimeSelector_ToHour_tbHourly_RequestHistory', state);
    ChangeTimePickerEnabled_RequestHistory(pageState, 'TimeSelector_FromHour_tbOverTime_RequestHistory', state);
    ChangeTimePickerEnabled_RequestHistory(pageState, 'TimeSelector_ToHour_tbOverTime_RequestHistory', state);
    ChangeTimePickerEnabled_RequestHistory(pageState, 'TimeSelector_Duration_tbOverTime_RequestHistory', state);
}

function ResetTimepickers_RequestHistory(pageState) {
    ResetTimepicker_RequestHistory(pageState, 'TimeSelector_FromHour_tbHourly_RequestHistory');
    ResetTimepicker_RequestHistory(pageState, 'TimeSelector_ToHour_tbHourly_RequestHistory');
    //ResetTimepicker_RequestHistory(pageState, 'TimeSelector_FromHour_tbOverTime_RequestHistory');
    //ResetTimepicker_RequestHistory(pageState, 'TimeSelector_ToHour_tbOverTime_RequestHistory');
    //ResetTimepicker_RequestHistory(pageState, 'TimeSelector_Duration_tbOverTime_RequestHistory');
}

function ResetTimepicker_RequestHistory(pageState, TimePicker) {
    var strTime = NullTime_RequestHistory;
    switch (pageState) {
        case 'Load':
            break;
        case 'Change':
            if (document.getElementById(TimePicker + "_txtHour").value == zeroTime)
            {
                document.getElementById(TimePicker + "_txtHour").value = strTime;
            }

                //var ObjRequestTargetFeatures = cmbRequestType_tbHourly_RequestHistory.getSelectedItem().get_value();
                //ObjRequestTargetFeatures = eval('(' + ObjRequestTargetFeatures + ')');
                //if (ObjRequestTargetFeatures.IsTraffic)
                //    strTime = NullTime_RequestHistory;
            
            break;
    }
    //document.getElementById(TimePicker + "_txtHour").value = strTime;
    //document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    //document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}



function SetButtonImages_TimeSelector_DialogRequestHistory(TimeSelector) {
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

function ChangeTimePickerEnabled_RequestHistory(pageState, TimeSelector, state) {
    var disabled = null;
    switch (state) {
        case 'disable':
            disabled = 'disabled';
            document.getElementById("" + TimeSelector + "_imgUp").onclick = " ";
            document.getElementById("" + TimeSelector + "_imgDown").onclick = " ";
            break;
        case 'enable':
            disabled = '';
            document.getElementById("" + TimeSelector + "_imgUp").onclick = function () {
                CheckTimePickerState_RequestHistory(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestHistory(TimeSelector + '_txtMinute');
                addTime(document.getElementById("" + TimeSelector + "_imgUp"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_imgDown").onclick = function () {
                CheckTimePickerState_RequestHistory(TimeSelector + '_txtHour');
                CheckTimePickerState_RequestHistory(TimeSelector + '_txtMinute');
                subtractTime(document.getElementById("" + TimeSelector + "_imgDown"), 24, 1, 1);
            };
            document.getElementById("" + TimeSelector + "_txtHour").onchange = function () {
                CheckTimeSelectorPartValue_RequestHistory(pageState, TimeSelector, '_txtHour');
            };
            document.getElementById("" + TimeSelector + "_txtMinute").onchange = function () {
                CheckTimeSelectorPartValue_RequestHistory(pageState, TimeSelector, '_txtMinute');
            };
            SetValueTimers_RequestHistory(TimeSelector, '_txtHour');
            SetValueTimers_RequestHistory(TimeSelector, '_txtMinute');
            break;

    }
    document.getElementById("" + TimeSelector + "_txtHour").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtHour").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtMinute").nextSibling.disabled = disabled;
    document.getElementById("" + TimeSelector + "_txtSecond").disabled = disabled;
}

function FocusOnCurrentTimeSelector(TimeSelector) {
    try {
        if (document.activeElement.id != "" + TimeSelector + "_txtHour" && document.activeElement.id != "" + TimeSelector + "_txtMinute" && document.activeElement.id != "" + TimeSelector + "_txtSecond")
            document.getElementById("" + TimeSelector + "_txtHour").focus();
    }
    catch (error) {
    }
}

function CheckTimePickerState_RequestHistory(TimeSelector) {
    if ((TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestHistory_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestHistory_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_FromHour_tbHourly_RequestHistory_txtHour').value == NullTime_RequestHistory)) || (TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestHistory_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_FromHour_tbHourly_RequestHistory_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestHistory_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestHistory_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_ToHour_tbHourly_RequestHistory_txtHour').value == NullTime_RequestHistory)) || (TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestHistory_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_ToHour_tbHourly_RequestHistory_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestHistory_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestHistory_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_FromHour_tbOverTime_RequestHistory_txtHour').value == NullTime_RequestHistory)) || (TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestHistory_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_FromHour_tbOverTime_RequestHistory_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestHistory_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestHistory_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_ToHour_tbOverTime_RequestHistory_txtHour').value == NullTime_RequestHistory)) || (TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestHistory_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_ToHour_tbOverTime_RequestHistory_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestHistory_txtHour' && isNaN(document.getElementById(TimeSelector).value)) || (TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestHistory_txtMinute' && (isNaN(document.getElementById(TimeSelector).value) || document.getElementById('TimeSelector_Duration_tbOverTime_RequestHistory_txtHour').value == NullTime_RequestHistory)) || (TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestHistory_txtHour' && parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById('TimeSelector_Duration_tbOverTime_RequestHistory_txtHour').value = zeroTime;
    if ((TimeSelector == 'TimeSelector_FromHour_tbHourly_RequestHistory_txtMinute' || TimeSelector == 'TimeSelector_ToHour_tbHourly_RequestHistory_txtMinute' || TimeSelector == 'TimeSelector_FromHour_tbOverTime_RequestHistory_txtMinute' || TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestHistory_txtMinute' || TimeSelector == 'TimeSelector_ToHour_tbOverTime_RequestHistory_txtMinute' || TimeSelector == 'TimeSelector_Duration_tbOverTime_RequestHistory_txtMinute') && (isNaN(document.getElementById(TimeSelector).value) || parseInt(document.getElementById(TimeSelector).value) < 0))
        document.getElementById(TimeSelector).value = zeroTime;
}

function ViewCurrentLangCalendars_RequestHistory() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpRequestDate_tbHourly_RequestHistory").parentNode.removeChild(document.getElementById("pdpRequestDate_tbHourly_RequestHistory"));
            document.getElementById("pdpRequestDate_tbHourly_RequestHistoryimgbt").parentNode.removeChild(document.getElementById("pdpRequestDate_tbHourly_RequestHistoryimgbt"));
            document.getElementById("pdpFromDate_tbDaily_RequestHistory").parentNode.removeChild(document.getElementById("pdpFromDate_tbDaily_RequestHistory"));
            document.getElementById("pdpFromDate_tbDaily_RequestHistoryimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_tbDaily_RequestHistoryimgbt"));
            document.getElementById("pdpToDate_tbDaily_RequestHistory").parentNode.removeChild(document.getElementById("pdpToDate_tbDaily_RequestHistory"));
            document.getElementById("pdpToDate_tbDaily_RequestHistoryimgbt").parentNode.removeChild(document.getElementById("pdpToDate_tbDaily_RequestHistoryimgbt"));
            document.getElementById("pdpFromDate_tbOverTime_RequestHistory").parentNode.removeChild(document.getElementById("pdpFromDate_tbOverTime_RequestHistory"));
            document.getElementById("pdpFromDate_tbOverTime_RequestHistoryimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_tbOverTime_RequestHistoryimgbt"));
            document.getElementById("pdpToDate_tbOverTime_RequestHistory").parentNode.removeChild(document.getElementById("pdpToDate_tbOverTime_RequestHistory"));
            document.getElementById("pdpToDate_tbOverTime_RequestHistoryimgbt").parentNode.removeChild(document.getElementById("pdpToDate_tbOverTime_RequestHistoryimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_RequestDateCalendars_tbHourly_RequestHistory").removeChild(document.getElementById("Container_gCalRequestDate_tbHourly_RequestHistory"));
            document.getElementById("Container_FromDateCalendars_tbDaily_RequestHistory").removeChild(document.getElementById("Container_gCalFromDate_tbDaily_RequestHistory"));
            document.getElementById("Container_ToDateCalendars_tbDaily_RequestHistory").removeChild(document.getElementById("Container_gCalToDate_tbDaily_RequestHistory"));
            document.getElementById("Container_FromDateCalendars_tbOverTime_RequestHistory").removeChild(document.getElementById("Container_gCalFromDate_tbOverTime_RequestHistory"));
            document.getElementById("Container_ToDateCalendars_tbOverTime_RequestHistory").removeChild(document.getElementById("Container_gCalToDate_tbOverTime_RequestHistory"));
            break;
            //DNN Note
        default:
            document.getElementById("Container_RequestDateCalendars_tbHourly_RequestHistory").removeChild(document.getElementById("Container_gCalRequestDate_tbHourly_RequestHistory"));
            document.getElementById("Container_FromDateCalendars_tbDaily_RequestHistory").removeChild(document.getElementById("Container_gCalFromDate_tbDaily_RequestHistory"));
            document.getElementById("Container_ToDateCalendars_tbDaily_RequestHistory").removeChild(document.getElementById("Container_gCalToDate_tbDaily_RequestHistory"));
            document.getElementById("Container_FromDateCalendars_tbOverTime_RequestHistory").removeChild(document.getElementById("Container_gCalFromDate_tbOverTime_RequestHistory"));
            document.getElementById("Container_ToDateCalendars_tbOverTime_RequestHistory").removeChild(document.getElementById("Container_gCalToDate_tbOverTime_RequestHistory"));
            break;
    }
}

function tlbItemSave_TlbRequest_RequestHistory_onClick() {
    UpdateRequest_RequestHistory();
}

function tlbItemFormReconstruction_TlbRequest_RequestHistory_onClick() {
    ReconstrucForm_RequestHistory();
}

function ReconstrucForm_RequestHistory() {
    var ObjDialogRequestHistory = parent.DialogRequestHistory.get_value();
    var caller = ObjDialogRequestHistory.Caller;
    DialogRequestHistory_onClose();
    var ObjRequestHistory = parent.DialogRequestHistory.get_value();
    switch (ObjRequestHistory.RequestCaller) {
        case 'RegisteredRequest':
            parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'DialogRegisteredRequests_IFrame').contentWindow.ShowDialogRequestHistory_RegisteredRequests();
            break;
        case 'Kartable':
        case 'Survey':
        case 'SpecialKartable':
            parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'DialogKartable_IFrame').contentWindow.ShowDialogRequestHistory_Kartable();
            break;
        default:
            break;
    }
}

//DNN Note:this code must be commented
//function DialogRequestHistory_onClose() {
//    
//    try {
//        parent.document.getElementById(parent.ClientPerfixId + 'DialogRequestHistory_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
//        parent.eval(parent.ClientPerfixId + 'DialogRequestHistory').Close();
//    } catch (e) {
//        parent.document.getElementById('DialogRequestHistoryDNN_IFrame').src = 'WhitePage.aspx';
//        parent.eval('DialogRequestHistoryDNN').Close();
//    }
//}

function tlbItemExit_TlbRequest_RequestHistory_onClick() {
    ShowDialogConfirm();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    DialogKartable_onClose();
    DialogConfirm.Close();
}

function DialogKartable_onClose() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogRequestHistory_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogRequestHistory').Close();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_RequestHistory').value;
    DialogConfirm.Show();
    
}
function DialogConfirm_OnShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tblConfirm_DialogConfirm').style.direction = 'rtl';
}
function UpdateRequest_RequestHistory() {
    ObjRequestTarget_RequestHistory = new Object();
    ObjRequestTarget_RequestHistory.RequestID = '0';
    ObjRequestTarget_RequestHistory.RequestDate = null;
    ObjRequestTarget_RequestHistory.FromDate = null;
    ObjRequestTarget_RequestHistory.ToDate = null;
    ObjRequestTarget_RequestHistory.FromTime = null;
    ObjRequestTarget_RequestHistory.ToTime = null;
    ObjRequestTarget_RequestHistory.IsToTimeInNextDay = false;
    ObjRequestTarget_RequestHistory.IsFromAndToTimeInNextDay = false;
    ObjRequestTarget_RequestHistory.Duration = '00:00';
    var ObjRequestHistory = parent.DialogRequestHistory.get_value();
    ObjRequestTarget_RequestHistory.RequestType = ObjRequestHistory.RequestType;
    ObjRequestTarget_RequestHistory.RequestID = ObjRequestHistory.RequestID;
    ObjRequestTarget_RequestHistory.RequestCaller = ObjRequestHistory.RequestCaller;
    ObjRequestTarget_RequestHistory.AttachmentFile = null;
    ObjRequestTarget_RequestHistory.OldAttachmentFile = ObjRequestHistory.AttachmentFile;

    switch (ObjRequestTarget_RequestHistory.RequestType.toString()) {
        case '0':
            break;
        case '1':
            ObjRequestTarget_RequestHistory.FromTime = GetDuration_TimePicker_RequestHistory('TimeSelector_FromHour_tbHourly_RequestHistory');
            ObjRequestTarget_RequestHistory.ToTime = GetDuration_TimePicker_RequestHistory('TimeSelector_ToHour_tbHourly_RequestHistory');
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    ObjRequestTarget_RequestHistory.RequestDate = document.getElementById('pdpRequestDate_tbHourly_RequestHistory').value;
                    break;
                case 'en-Us':
                    ObjRequestTarget_RequestHistory.RequestDate = document.getElementById('gdpRequestDate_tbHourly_RequestHistory_picker').value;
                    break;
            }
            if (document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory') != null && document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory').checked)
                ObjRequestTarget_RequestHistory.IsToTimeInNextDay = true;
            if (document.getElementById('chbFromAndToHourInNextDay_tbHourly_RequestHistory') != null && document.getElementById('chbFromAndToHourInNextDay_tbHourly_RequestHistory').checked)
                ObjRequestTarget_RequestHistory.IsFromAndToTimeInNextDay = true;
            break;
        case '2':
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    ObjRequestTarget_RequestHistory.FromDate = document.getElementById('pdpFromDate_tbDaily_RequestHistory').value;
                    ObjRequestTarget_RequestHistory.ToDate = document.getElementById('pdpToDate_tbDaily_RequestHistory').value;
                    break;
                case 'en-Us':
                    ObjRequestTarget_RequestHistory.FromDate = document.getElementById('gdpFromDate_tbDaily_RequestHistory_picker').value;
                    ObjRequestTarget_RequestHistory.ToDate = document.getElementById('gdpToDate_tbDaily_RequestHistory_picker').value;
                    break;
            }
            break;
        case '4':
            ObjRequestTarget_RequestHistory.FromTime = GetDuration_TimePicker_RequestHistory('TimeSelector_FromHour_tbOverTime_RequestHistory');
            ObjRequestTarget_RequestHistory.ToTime = GetDuration_TimePicker_RequestHistory('TimeSelector_ToHour_tbOverTime_RequestHistory');
            ObjRequestTarget_RequestHistory.Duration = GetDuration_TimePicker_RequestHistory('TimeSelector_Duration_tbOverTime_RequestHistory');
            switch (parent.parent.SysLangID) {
                case 'fa-IR':
                    ObjRequestTarget_RequestHistory.FromDate = document.getElementById('pdpFromDate_tbOverTime_RequestHistory').value;
                    ObjRequestTarget_RequestHistory.ToDate = document.getElementById('pdpToDate_tbOverTime_RequestHistory').value;
                    break;
                case 'en-Us':
                    ObjRequestTarget_RequestHistory.FromDate = document.getElementById('gdpFromDate_tbOverTime_RequestHistory_picker').value;
                    ObjRequestTarget_RequestHistory.ToDate = document.getElementById('gdpToDate_tbOverTime_RequestHistory_picker').value;
                    break;
            }
            if (document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory') != null && document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory').checked)
                ObjRequestTarget_RequestHistory.IsToTimeInNextDay = true;
            if (document.getElementById('chbFromAndToHourInNextDay_tbOvertime_RequestHistory') != null && document.getElementById('chbFromAndToHourInNextDay_tbOvertime_RequestHistory').checked)
                ObjRequestTarget_RequestHistory.IsFromAndToTimeInNextDay = true;
            break;
    }
    if (ObjRequestAttachment_RequestHistory != null)
        ObjRequestTarget_RequestHistory.AttachmentFile = ObjRequestAttachment_RequestHistory.RequestAttachmentSavedName;
    UpdateRequest_RequestHistoryPage(CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.RequestID), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.FromDate), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.ToDate), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.FromTime), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.ToTime), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.IsToTimeInNextDay.toString()), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.IsFromAndToTimeInNextDay.toString()), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.RequestDate), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.Duration), CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.RequestType),CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.RequestCaller),CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.AttachmentFile),CharToKeyCode_RequestHistory(ObjRequestTarget_RequestHistory.OldAttachmentFile));
    DialogWaiting.Show();
}

function UpdateRequest_RequestHistoryPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RequestHistory').value;
            Response[1] = document.getElementById('hfConnectionError_RequestHistory').value;
        }
        if (RetMessage[2] == 'success') {

            Fill_GridRequestHistory_RequestHistory();
            parent.document.getElementById(parent.ClientPerfixId + 'DialogKartable_IFrame').contentWindow.SetPageIndex_GridKartable_Kartable(0);
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CheckTimeSelectorPartValue_RequestHistory(pageState, TimeSelectorPartID, identifier) {
    if (document.getElementById(TimeSelectorPartID + identifier).value == "") {
        switch (identifier) {
            case '_txtHour':
                var strTime = zeroTime;
                if (TimeSelectorPartID + identifier != ("TimeSelector_FromHour_tbHourly_RequestHistory" + "_txtHour") && TimeSelectorPartID + identifier != ("TimeSelector_ToHour_tbHourly_RequestHistory" + "_txtHour"))
                document.getElementById(TimeSelectorPartID + identifier).value = strTime;
                break;
            case '_txtMinute':
                document.getElementById(TimeSelectorPartID + identifier).value = zeroTime;
                intOnly(document.getElementById(TimeSelectorPartID + identifier), 24);
                break;
        }
    }
    else {
        intOnly(document.getElementById(TimeSelectorPartID + identifier), 24);
    }
}

function GetDuration_TimePicker_RequestHistory(TimePicker) {
    if (document.getElementById(TimePicker + '_txtHour').value == NullTime_RequestHistory)
        return;
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

function SetValueTimers_RequestHistory(TimeSelectorPartID, identifier) {
    var ObjRequestHistory = parent.DialogRequestHistory.get_value();
    var strTime = zeroTime;
    if (ObjRequestHistory.FromTime != null && ObjRequestHistory.FromTime != undefined && ObjRequestHistory.ToTime != null && ObjRequestHistory.ToTime != undefined) {
        switch (ObjRequestHistory.RequestType.toString()) {
            case '1':
                if (document.getElementById('chbFromAndToHourInNextDay_tbHourly_RequestHistory') != null && ObjRequestHistory.FromTime.toString().indexOf('+') >= 0 && ObjRequestHistory.ToTime.toString().indexOf('+') >= 0)
                    document.getElementById('chbFromAndToHourInNextDay_tbHourly_RequestHistory').checked = true;
                else
                    if (document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory') != null && ObjRequestHistory.ToTime.toString().indexOf('+') >= 0)
                        document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory').checked = true;
                break;
            case '4':
                if (document.getElementById('chbFromAndToHourInNextDay_tbOvertime_RequestHistory') != null && ObjRequestHistory.FromTime.toString().indexOf('+') >= 0 && ObjRequestHistory.ToTime.toString().indexOf('+') >= 0)
                    document.getElementById('chbFromAndToHourInNextDay_tbOvertime_RequestHistory').checked = true;
                else
                    if (document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory') != null && ObjRequestHistory.ToTime.toString().indexOf('+') >= 0)
                        document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory').checked = true;
                break;
        }
        ObjRequestHistory.FromTime = ObjRequestHistory.FromTime.toString().replace('+', '');
        ObjRequestHistory.ToTime = ObjRequestHistory.ToTime.toString().replace('+', '');
    }
    else {
        if (ObjRequestHistory.ToTime != null && ObjRequestHistory.ToTime != undefined) {
            switch (ObjRequestHistory.RequestType.toString()) {
                case '1':
                    if (document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory') != null && ObjRequestHistory.ToTime.toString().indexOf('+') >= 0)
                        document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory').checked = true;
                    break;
                case '4':
                    if (document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory') != null && ObjRequestHistory.ToTime.toString().indexOf('+') >= 0)
                        document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory').checked = true;
                    break;
            }
            ObjRequestHistory.ToTime = ObjRequestHistory.ToTime.toString().replace('+', '');
        }
    }
    switch (identifier) {
        case '_txtHour':
            switch (ObjRequestHistory.RequestType.toString()) {
                case "1":
                    switch (TimeSelectorPartID) {
                        case "TimeSelector_FromHour_tbHourly_RequestHistory":
                            if (ObjRequestHistory.FromTime!="")
                            strTime = ObjRequestHistory.FromTime.substring(0, 2);
                            break;
                        case "TimeSelector_ToHour_tbHourly_RequestHistory":
                            if (ObjRequestHistory.ToTime != "")
                            strTime = ObjRequestHistory.ToTime.substring(0, 2);
                            break;
                    }
                case "4":
                    switch (TimeSelectorPartID) {

                        case "TimeSelector_FromHour_tbOverTime_RequestHistory":
                            if (ObjRequestHistory.FromTime != "")
                            strTime = ObjRequestHistory.FromTime.substring(0, 2);
                            break;
                        case "TimeSelector_ToHour_tbOverTime_RequestHistory":
                            if (ObjRequestHistory.ToTime != "")
                            strTime = ObjRequestHistory.ToTime.substring(0, 2);
                            break;
                        case "TimeSelector_Duration_tbOverTime_RequestHistory":
                            if (ObjRequestHistory.Duration != "")
                                strTime = ObjRequestHistory.Duration.substring(0, 2);
                            break;
                    }
                    break;
            }
            break;
        case '_txtMinute':
            switch (ObjRequestHistory.RequestType.toString()) {
                case "1":
                    switch (TimeSelectorPartID) {

                        case "TimeSelector_FromHour_tbHourly_RequestHistory":
                            if (ObjRequestHistory.FromTime != "")
                            strTime = ObjRequestHistory.FromTime.substring(3, 5);
                            break;
                        case "TimeSelector_ToHour_tbHourly_RequestHistory":
                            if (ObjRequestHistory.ToTime != "")
                            strTime = ObjRequestHistory.ToTime.substring(3, 5);
                            break;
                    }
                    break;
                case "4":
                    switch (TimeSelectorPartID) {
                        case "TimeSelector_FromHour_tbOverTime_RequestHistory":
                            if (ObjRequestHistory.FromTime != "")
                            strTime = ObjRequestHistory.FromTime.substring(3, 5);
                            break;
                        case "TimeSelector_ToHour_tbOverTime_RequestHistory":
                            if (ObjRequestHistory.ToTime != "")
                            strTime = ObjRequestHistory.ToTime.substring(3, 5);
                            break;
                        case "TimeSelector_Duration_tbOverTime_RequestHistory":
                            if (ObjRequestHistory.Duration != "")
                                strTime = ObjRequestHistory.Duration.substring(3, 5);
                            break;
                    }
                    break;
            }            
            break;
    }
    if (TimeSelectorPartID != undefined && identifier != undefined)
        document.getElementById(TimeSelectorPartID + identifier).value = strTime;

}

function SetValueCalendars_RequestHistory() {
    var ObjRequestHistory = parent.DialogRequestHistory.get_value();
    switch (parent.parent.SysLangID) {
        case 'en-US':
            gdpRequestDate_tbHourly_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            gCalRequestDate_tbHourly_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            gdpFromDate_tbDaily_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            gCalFromDate_tbDaily_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            gdpToDate_tbDaily_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.ToDate));
            gCalToDate_tbDaily_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.ToDate));
            gdpFromDate_tbOverTime_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            gCalFromDate_tbOverTime_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            gdpToDate_tbOverTime_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            gCalToDate_tbOverTime_RequestHistory.setSelectedDate(new Date(ObjRequestHistory.FromDate));
            break;
        case 'fa-IR':
            document.getElementById('pdpRequestDate_tbHourly_RequestHistory').value = ObjRequestHistory.FromDate;
            document.getElementById('pdpFromDate_tbDaily_RequestHistory').value = ObjRequestHistory.FromDate;
            document.getElementById('pdpToDate_tbDaily_RequestHistory').value = ObjRequestHistory.ToDate;
            document.getElementById('pdpFromDate_tbOverTime_RequestHistory').value = ObjRequestHistory.FromDate;
            document.getElementById('pdpToDate_tbOverTime_RequestHistory').value = ObjRequestHistory.ToDate;
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function chbToHourInNextDay_tbHourly_RequestHistory_onClick() {
    if (document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory').checked)
        document.getElementById('chbFromAndToHourInNextDay_tbHourly_RequestHistory').checked = false;
}

function chbFromAndToHourInNextDay_tbHourly_RequestHistory_onclick() {
    if (document.getElementById('chbFromAndToHourInNextDay_tbHourly_RequestHistory').checked)
        document.getElementById('chbToHourInNextDay_tbHourly_RequestHistory').checked = false;
}

function chbToHourInNextDay_tbOvertime_RequestHistory_onClick() {
    if (document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory').checked)
        document.getElementById('chbFromAndToHourInNextDay_tbOvertime_RequestHistory').checked = false;
}

function chbFromAndToHourInNextDay_tbOvertime_RequestHistory_onclick() {
    if (document.getElementById('chbFromAndToHourInNextDay_tbOvertime_RequestHistory').checked)
        document.getElementById('chbToHourInNextDay_tbOvertime_RequestHistory').checked = false;
}

function ShowAttachmentFile_GridRequestHistory_RequestHistory() {
    var SelectedItems_GridRequestHistory_RequestHistory = GridRequestHistory_RequestHistory.getSelectedItems();
    if (SelectedItems_GridRequestHistory_RequestHistory.length > 0 && SelectedItems_GridRequestHistory_RequestHistory[0].getMember('AttachmentFile').get_text() != '') {
        var AttachmentFile = SelectedItems_GridRequestHistory_RequestHistory[0].getMember('AttachmentFile').get_text();
        window.open("ClientAttachmentViewer.aspx?AttachmentType=Request&ClientAttachment=" + CharToKeyCode_RequestHistory(AttachmentFile) + "", "ClientAttachmentViewer" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
    }
}

function SetAttachmentFileImage_GridRequestHistroy_RequestHistory(attachmentFile) {
    var innerHTML = '';
    if (attachmentFile != undefined && attachmentFile != null && attachmentFile != '')
        innerHTML = '<img src="Images/Grid/attachment.png" alt="" />';
    return innerHTML;
}

function Callback_AttachmentUploader_RequestHistory_onCallBackComplete(sender, e) {
    Subgurim_AttachmentUploader_RequestHistoryadd('1', '4');
}

function Callback_AttachmentUploader_RequestHistory_onCallbackError(sender, e) {
    ShowConnectionError_RequestHistory();
}



function tlbItemDeleteAttachment_TlbDeleteAttachment_RequestHistory_onClick() {
    DeleteRequestAttachment_RequestHistory();
}



function DeleteRequestAttachment_RequestHistory() {
    if (ObjRequestAttachment_RequestHistory != null && eval('ObjRequestAttachment_RequestHistory').RequestAttachmentSavedName != '')
        DeleteRequestAttachment_RequestHistoryPage(CharToKeyCode_RequestHistory(eval('ObjRequestAttachment_RequestHistory').RequestAttachmentSavedName));
}

function DeleteRequestAttachment_RequestHistoryPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RequestRegister').value;
            Response[1] = document.getElementById('hfConnectionError_RequestRegister').value;
        }
        if (RetMessage[2] == 'success') {
            var requestTarget = RetMessage[3];
           ObjRequestAttachment_RequestHistory = null;
                   
           
           document.getElementById('tdAttachmentName_RequestHistory').innerHTML = '';
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function AttachmentUploader_RequestHistory_OnPreFileUpload() {
    var uploader = $('#Subgurim_AttachmentUploader_RequestHistory').find('div:first').find('iframe:first').contents().find('#file')[0];
    if (uploader != undefined && uploader != null && uploader.files != undefined && uploader.files != null && uploader.files.length > 0) {
        var filesize = uploader.files[0].size;
        var requestMaxLength = parseInt(document.getElementById('hfMRL').value) * 1000;
        if (filesize > requestMaxLength) {
            var errorMessage = document.getElementById('hfRequestMaxLength_RequestHistory').value + ' ' + (requestMaxLength / Math.pow(10, 6)).toFixed(2);
            showDialog(document.getElementById('hfErrorType_RequestHistory').value, errorMessage, 'error');
            Callback_AttachmentUploader_RequestHistory.callback();
        }
    }
}

function AttachmentUploader_RequestHistory_OnAfterFileUpload(StrRequestAttachment) {
    var message = null;
    if (ObjRequestAttachment_RequestHistory == null)
        ObjRequestAttachment_RequestHistory = new Object();
    ObjRequestAttachment_RequestHistory = eval('(' + StrRequestAttachment + ')');
    if (!ObjRequestAttachment_RequestHistory.IsErrorOccured)
        message = ObjRequestAttachment_RequestHistory.RequestAttachmentRealName;
    else {
        message = ObjRequestAttachment_RequestHistory.Message;
        ObjRequestAttachment_RequestHistory = null;
    }
    document.getElementById('tdAttachmentName_RequestHistory').innerHTML = message;
    Callback_AttachmentUploader_RequestHistory.callback();
}

//DNN Note
function KeyCodeToChar_RequestHistory(str) {
    var OutStr = "";
    if (str != null && str != undefined) {
        var res = str.split('//');
        res.forEach(function (entry) {
            if (entry != "") {
                OutStr = OutStr + String.fromCharCode(entry);
            }
        });
    }
    return OutStr.toString();
}

//DNN Note
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}