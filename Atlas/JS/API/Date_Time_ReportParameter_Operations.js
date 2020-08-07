
var ObjDateTime_Date_Time_ReportParameter = null;
function gdpDate_Date_Time_ReportParameter_OnDateChange(sender, e) {
    var Date = gdpDate_Date_Time_ReportParameter.getSelectedDate();
    gCalDate_Date_Time_ReportParameter.setSelectedDate(Date);
}
function gCalDate_Date_Time_ReportParameter_OnChange(sender, e) {
    var Date = gCalDate_Date_Time_ReportParameter.getSelectedDate();
    gdpDate_Date_Time_ReportParameter.setSelectedDate(Date);

}
function gCalDate_Date_Time_ReportParameter_onLoad(sender, e) {
    window.gCalDate_Date_Time_ReportParameter.PopUpObject.z = 25000000;
}

function btn_gdpDate_Date_Time_ReportParameter_OnMouseUp(event) {
    if (gCalDate_Date_Time_ReportParameter.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpDate_Date_Time_ReportParameter_OnClick(event) {
    if (gCalDate_Date_Time_ReportParameter.get_popUpShowing()) {
        gCalDate_Date_Time_ReportParameter.hide();
    }
    else {
        gCalDate_Date_Time_ReportParameter.setSelectedDate(gdpDate_Date_Time_ReportParameter.getSelectedDate());
        gCalDate_Date_Time_ReportParameter.show();
    }
}


//نمایش تقویم بر اساس زبان انتخاب شده در نرم افزار
function ViewCurrentLangCalendars_Date_Time_ReportParameter() {
    switch (parent.parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_Date_Time_ReportParameter").parentNode.removeChild(document.getElementById("pdpDate_Date_Time_ReportParameter"));
            document.getElementById("pdpDate_Date_Time_ReportParametersimgbt").parentNode.removeChild(document.getElementById("pdpDate_Date_Time_ReportParameterimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_Date_Time_ReportParameter").removeChild(document.getElementById("Container_gCalDate_Date_Time_ReportParameter"));
            break;
    }
    setCurrentDateCalendar();
}

// تنظیم تقویم های شمسی و میلادی به تاریخ روز بر اساس زبان نرم افزار
function SetCurrentDate_Date_Time_ReportParameter() {

    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpDate_Date_Time_ReportParameter').value = document.getElementById('hfCurrentDate_Date_Time_ReportParameter').value;
            break;
        case 'en-US':
            currentDate_Date_Time_ReportParameter = new Date(document.getElementById('hfCurrentDate_Date_Time_ReportParameter').value);
            gdpDate_Date_Time_ReportParameter.setSelectedDate(currentDate_Date_Time_ReportParameter);
            gCalDate_Date_Time_ReportParameter.setSelectedDate(currentDate_Date_Time_ReportParameter);
            break;
    }
}

function ResetCalendars_Date_Time_ReportParameter() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpDate_Date_Time_ReportParameter').value = '';
            break;
        case 'en-US':
            document.getElementById('gdpDate_Date_Time_ReportParameter_picker').value = '';
            break;
    }
}

function tlbItemRegister_TlbRegister_Date_Time_ReportParameter_onClick() {
    Register_Date_Time_ReportParameter();
}
// ارسال اطلاعات پارامتر ها به سرور 
function Register_Date_Time_ReportParameter() {

    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            ObjDateTime_Date_Time_ReportParameter.Date = document.getElementById('pdpDate_Date_Time_ReportParameter').value;
            break;
        case 'en-US':
            ObjDateTime_Date_Time_ReportParameter.Date = document.getElementById('gdpDate_Date_Time_ReportParameter_picker').value;
            break;
    }
    ObjDateTime_Date_Time_ReportParameter.Time = GetDuration_TimePicker_Date_Time_ReportParameter('TimeSelector_Date_Time_ReportParameter');
    Register_Date_Time_ReportParameterPage(CharToKeyCode_Date_Time_ReportParameter(ObjDateTime_Date_Time_ReportParameter.ReportParameterID), CharToKeyCode_Date_Time_ReportParameter(ObjDateTime_Date_Time_ReportParameter.ReportParameterActionID), CharToKeyCode_Date_Time_ReportParameter(ObjDateTime_Date_Time_ReportParameter.ReportFileID), CharToKeyCode_Date_Time_ReportParameter(ObjDateTime_Date_Time_ReportParameter.Date), CharToKeyCode_Date_Time_ReportParameter(ObjDateTime_Date_Time_ReportParameter.Time));
    DialogWaiting.Show();
}
function GetDuration_TimePicker_Date_Time_ReportParameter(TimePicker) {
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
function Register_Date_Time_ReportParameterPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Date_Time_ReportParameter').value;
            Response[1] = document.getElementById('hfConnectionError_Date_Time_ReportParameter').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjDateTime_Date_Time_ReportParameter.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function CharToKeyCode_Date_Time_ReportParameter(str) {
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
function SetReportParameterObj_Date_Time_ReportParameter() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjDateTime_Date_Time_ReportParameter = new Object();
        ObjDateTime_Date_Time_ReportParameter.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjDateTime_Date_Time_ReportParameter.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjDateTime_Date_Time_ReportParameter.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}

function TimeSelector_Date_Time_ReportParameter_onChange(partID) {
    var id = 'TimeSelector_Date_Time_ReportParameter_' + partID;
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

