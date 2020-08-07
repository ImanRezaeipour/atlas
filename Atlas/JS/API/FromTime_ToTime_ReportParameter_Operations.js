
var ObjFromTime_ToTime_ReportParameter = null;
var zeroTime = '00';

function tlbItemRegister_TlbRegister_FromTime_ToTime_ReportParameter_onClick() {
    Register_FromTime_ToTime_ReportParameter();
}
// ارسال اطلاعات پارامتر ها به سرور 
function Register_FromTime_ToTime_ReportParameter() {

    ObjFromTime_ToTime_ReportParameter.from = GetDuration_TimePicker_FromTime_ToTime_ReportParameter('TimeSelector_FromTime_ReportParameter');
    ObjFromTime_ToTime_ReportParameter.to = GetDuration_TimePicker_FromTime_ToTime_ReportParameter('TimeSelector_ToTime_ReportParameter');
    Register_FromTime_ToTime_ReportParameterPage(CharToKeyCode_FromTime_ToTime_ReportParameter(ObjFromTime_ToTime_ReportParameter.ReportParameterID), CharToKeyCode_FromTime_ToTime_ReportParameter(ObjFromTime_ToTime_ReportParameter.ReportParameterActionID), CharToKeyCode_FromTime_ToTime_ReportParameter(ObjFromTime_ToTime_ReportParameter.ReportFileID), CharToKeyCode_FromTime_ToTime_ReportParameter(ObjFromTime_ToTime_ReportParameter.from), CharToKeyCode_FromTime_ToTime_ReportParameter(ObjFromTime_ToTime_ReportParameter.to));
    DialogWaiting.Show();
}

function Register_FromTime_ToTime_ReportParameterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_FromTime_ToTime_ReportParameter').value;
            Response[1] = document.getElementById('hfConnectionError_FromTime_ToTime_ReportParameter').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById('DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjFromTime_ToTime_ReportParameter.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_FromTime_ToTime_ReportParameter(str) {
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
//در یافت اطلاعات پارامتر انتخاب شده و نگهداری در آبجکت  برای ارسال مجدد به سرور 
function SetReportParameterObj_FromTime_ToTime_ReportParameter() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjFromTime_ToTime_ReportParameter = new Object();
        ObjFromTime_ToTime_ReportParameter.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjFromTime_ToTime_ReportParameter.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjFromTime_ToTime_ReportParameter.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}

function GetDuration_TimePicker_FromTime_ToTime_ReportParameter(TimePicker) {
    
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
function initTimePickers_FromTime_ToTime_ReportParameter() {
    SetButtonImages_TimeSelectors_FromTime_ToTime_ReportParameter();
    ResetTimepickers_FromTime_ToTime_ReportParameter();
}
function SetButtonImages_TimeSelectors_FromTime_ToTime_ReportParameter() {
    SetButtonImages_TimeSelector_FromTime_ToTime_ReportParameter('TimeSelector_FromTime_ReportParameter');
    SetButtonImages_TimeSelector_FromTime_ToTime_ReportParameter('TimeSelector_ToTime_ReportParameter');
}

function SetButtonImages_TimeSelector_FromTime_ToTime_ReportParameter(TimeSelector) {
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
function ResetTimepickers_FromTime_ToTime_ReportParameter() {
    ResetTimepicker_FromTime_ToTime_ReportParameter('TimeSelector_FromTime_ReportParameter');
    ResetTimepicker_FromTime_ToTime_ReportParameter('TimeSelector_ToTime_ReportParameter');

}
function ResetTimepicker_FromTime_ToTime_ReportParameter(TimePicker) {
    var strTime = zeroTime;
 
    document.getElementById(TimePicker + "_txtHour").value = strTime;
    document.getElementById(TimePicker + "_txtMinute").value = zeroTime;
    document.getElementById(TimePicker + "_txtSecond").value = zeroTime;
}