
var ConfirmState_SystemReports = null;
var CurrentPageState_SystemReports = 'View';
var CurrentPageCombosCallBcakStateObj = new Object();
var CurrentPageIndex_GridSystemReportType_SystemReports = 0;
var ObjCurrentSystemReportFilterConditions_SystemReports = null;
var Basefooter_GridSystemReportType_SystemReports = null;
var CurrentRuleDebugPersonnelFeatures_SystemReports = null;

function GetBoxesHeaders_SystemReports() {
    parent.document.getElementById('Title_DialogSystemReports').innerHTML = document.getElementById('hfTitle_DialogSystemReports').value;
    document.getElementById('header_GridSystemReportType_SystemReports').innerHTML = document.getElementById('hfheader_GridSystemReportType_SystemReports').value;
    document.getElementById('header_SystemReportTypeFeature_SystemReports').innerHTML = document.getElementById('hfheader_SystemReportTypeFeature_SystemReports').value;
    Basefooter_GridSystemReportType_SystemReports = document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML = document.getElementById('hffooter_GridSystemReportType_SystemReports').value;
}

function tlbItemDeleteAll_TlbSystemReports_onClick() {
    CurrentPageState_SystemReports = 'DeleteAll';
    ShowDialogConfirm('DeleteAll');
}

function UpdateSystemReport_SystemReports() {
    var SelectedItem_cmbSystemReportType_SystemReports = cmbSystemReportType_SystemReports.getSelectedItem();
    if (SelectedItem_cmbSystemReportType_SystemReports != null && SelectedItem_cmbSystemReportType_SystemReports != undefined) {
        var SelectedSystemReportType = SelectedItem_cmbSystemReportType_SystemReports.get_value();
        UpdateSystemReport_SystemReportsPage(CharToKeyCode_SystemReports(CurrentPageState_SystemReports), CharToKeyCode_SystemReports(SelectedSystemReportType));
        DialogWaiting.Show();
    }
}

function GetCurrentSystemReportType_SystemReports() {
    var ObjCurrentSystemReportType = eval('(' + document.getElementById('hfCurrentSystemReportType_SystemReports').value + ')');
    return ObjCurrentSystemReportType;
}

function UpdateSystemReport_SystemReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {
            if (CurrentPageState_SystemReports == 'DeleteAll') {
                DeleteAllItems_GridSystemReportType_SystemReports();
                document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML = Basefooter_GridSystemReportType_SystemReports;
                ResetSystemReportTypeFeature_SystemReports();
                CurrentPageState_SystemReports = 'View';
            }
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function DeleteAllItems_GridSystemReportType_SystemReports() {
    var GridSystemReportType = GetGridSystemReportType_SystemReports();
    GridSystemReportType.beginUpdate();
    GridSystemReportType.get_table().clearData();
    GridSystemReportType.endUpdate();
}

function GetGridSystemReportType_SystemReports() {
    var GridSystemReportType = null;
    GridSystemReportType = 'Grid' + GetCurrentSystemReportType_SystemReports().Value + '_SystemReports';
    GridSystemReportType = eval(GridSystemReportType);
    return GridSystemReportType;
}

function tlbItemHelp_TlbSystemReports_onClick() {
    LoadHelpPage('tlbItemHelp_TlbSystemReports');
}

function tlbItemFormReconstruction_TlbSystemReports_onClick() {
    parent.DialogSystemReports.Close();
    parent.DialogSystemReports.Show();
}

function tlbItemExit_TlbSystemReports_onClick() {
    ShowDialogConfirm('Exit');
}

function ChangeCurrentSystemReportType_SystemReports() {
    if (cmbSystemReportType_SystemReports.getSelectedItem() != undefined && cmbSystemReportType_SystemReports.getSelectedItem() != null)
    {
        var ObjCurrentSystemReportType = '{"Text":"' + cmbSystemReportType_SystemReports.getSelectedItem().get_text() + '","Value":"' + cmbSystemReportType_SystemReports.getSelectedItem().get_value() + '"}';
        document.getElementById('hfCurrentSystemReportType_SystemReports').value = ObjCurrentSystemReportType;
    }
}

function ResetSystemReportTypeFeature_SystemReports() {
    document.getElementById('header_SystemReportTypeFeature_SystemReports').innerHTML = document.getElementById('hfheader_SystemReportTypeFeature_SystemReports').value;
    document.getElementById('txtSystemReportTypeFeature_SystemReports').value = '';
}

function gdpFromDate_SystemReports_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_SystemReports.getSelectedDate();
    gCalFromDate_SystemReports.setSelectedDate(FromDate);
}

function gCalFromDate_SystemReports_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_SystemReports.getSelectedDate();
    gdpFromDate_SystemReports.setSelectedDate(FromDate);
}

function btn_gdpFromDate_SystemReports_OnClick(event) {
    if (gCalFromDate_SystemReports.get_popUpShowing()) {
        gCalFromDate_SystemReports.hide();
    }
    else {
        gCalFromDate_SystemReports.setSelectedDate(gdpFromDate_SystemReports.getSelectedDate());
        gCalFromDate_SystemReports.show();
    }
}

function btn_gdpFromDate_SystemReports_OnMouseUp(event) {
    if (gCalFromDate_SystemReports.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_SystemReports_onLoad(sender, e) {
    window.gCalFromDate_SystemReports.PopUpObject.z = 25000000;
}

function gdpToDate_SystemReports_OnDateChange(sender, eventArgs) {
    var ToDate = gdpToDate_SystemReports.getSelectedDate();
    gCalToDate_SystemReports.setSelectedDate(ToDate);
}
function gCalToDate_SystemReports_OnChange(sender, eventArgs) {
    var ToDate = gCalToDate_SystemReports.getSelectedDate();
    gdpToDate_SystemReports.setSelectedDate(ToDate);
}

function btn_gdpToDate_SystemReports_OnClick(event) {
    if (gCalToDate_SystemReports.get_popUpShowing()) {
        gCalToDate_SystemReports.hide();
    }
    else {
        gCalToDate_SystemReports.setSelectedDate(gdpToDate_SystemReports.getSelectedDate());
        gCalToDate_SystemReports.show();
    }
}

function btn_gdpToDate_SystemReports_OnMouseUp(event) {
    if (gCalToDate_SystemReports.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_SystemReports_onLoad(sender, e) {
    window.gCalToDate_SystemReports.PopUpObject.z = 25000000;
}

function tlbItemClear_TlbClear_FromDateCalendars_SystemReports_onClick() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_SystemReports').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_SystemReports_picker').value = "";
            break;
    }
}

function tlbItemClear_TlbClear_ToDateCalendars_SystemReports_onClick() {
    switch (parent.parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpToDate_SystemReports').value = "";
            break;
        case 'en-US':
            document.getElementById('gdpToDate_SystemReports_picker').value = "";
            break;
    }
}

function tlbItemResultsView_TlbResultView_onClick() {
    ViewSystemReportResult_SystemReports();
}

function ViewSystemReportResult_SystemReports() {
    ChangeCurrentSystemReportType_SystemReports();
    Changeheader_GridSystemReportType_SystemReports(false);
    CreateSystemReportsFilterConditions_SystemReports();
    ResetSystemReportTypeFeature_SystemReports();
    SetPageIndex_GridSystemReportType_SystemReports(0);
}

function CreateSystemReportsFilterConditions_SystemReports() {
    ObjCurrentSystemReportFilterConditions_SystemReports = new Object();
    var SearchTerm = document.getElementById('txtSearchTerm_SystemReports').value;
    var FromDate = null;
    var ToDate = null;
    switch (parent.SysLangID) {
        case 'fa-IR':
            FromDate = document.getElementById('pdpFromDate_SystemReports').value;
            ToDate = document.getElementById('pdpToDate_SystemReports').value;
            break;
        case 'en-US':
            FromDate = document.getElementById('gdpFromDate_SystemReports_picker').value;
            ToDate = document.getElementById('gdpToDate_SystemReports_picker').value;
            break;
    }
    ObjCurrentSystemReportFilterConditions_SystemReports.SearchTerm = SearchTerm;
    ObjCurrentSystemReportFilterConditions_SystemReports.FromDate = FromDate;
    ObjCurrentSystemReportFilterConditions_SystemReports.ToDate = ToDate;
    ObjCurrentSystemReportFilterConditions_SystemReports.Concept = document.getElementById('txtConcept_SystemReports').value;
    ObjCurrentSystemReportFilterConditions_SystemReports.RuleCode = document.getElementById('txtRuleCode_SystemReports').value;
}

function SetPageIndex_GridSystemReportType_SystemReports(pageIndex) {
    CurrentPageIndex_GridSystemReportType_SystemReports = pageIndex;
    Fill_GridSystemReportType_SystemReports(pageIndex);
}

function Fill_GridSystemReportType_SystemReports(pageIndex) {
    document.getElementById('loadingPanel_GridSystemReportType_SystemReports').innerHTML =GetLoadingMessage(document.getElementById('hfloadingPanel_GridSystemReportType_SystemReports').value);
    var pageSize = parseInt(document.getElementById('hfSystemReportTypePageSize_SystemReports').value);
    var FilterConditions = '';
    var SearchTerm = '';
    var FromDate = '';
    var ToDate = '';
    if (ObjCurrentSystemReportFilterConditions_SystemReports != null) {
        var SearchTerm = ObjCurrentSystemReportFilterConditions_SystemReports.SearchTerm;
        var FromDate = ObjCurrentSystemReportFilterConditions_SystemReports.FromDate;
        var ToDate = ObjCurrentSystemReportFilterConditions_SystemReports.ToDate;
        var Concept = ObjCurrentSystemReportFilterConditions_SystemReports.Concept;
        var RuleCode = ObjCurrentSystemReportFilterConditions_SystemReports.RuleCode;
    }
    FilterConditions = '{"SearchTerm":"' + SearchTerm + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '","Concept":"' + Concept + '","RuleCode":"' + RuleCode + '"}';

    CallBack_GridSystemReportType_SystemReports.callback(CharToKeyCode_SystemReports(GetCurrentSystemReportType_SystemReports().Value), CharToKeyCode_SystemReports(pageSize.toString()), CharToKeyCode_SystemReports(pageIndex.toString()), CharToKeyCode_SystemReports(FilterConditions));
}

function NavigateSystemReportTypeFeature_SystemReports_onCelldbClick(systemReportTypeFeature) {
    var GridSystemReportType = GetGridSystemReportType_SystemReports();
    document.getElementById('txtSystemReportTypeFeature_SystemReports').value = GridSystemReportType.getSelectedItems()[0].getMember(systemReportTypeFeature).get_value();
    Changeheader_SystemReportTypeFeature_SystemReports(systemReportTypeFeature);
}

function Changeheader_SystemReportTypeFeature_SystemReports(systemReportTypeFeature) {
    var GridSystemReportType = GetGridSystemReportType_SystemReports();
    var GridColumnsCol = GridSystemReportType.get_table().get_columns();
    for (var i = 0; i < GridColumnsCol.length; i++) {
        if (GridColumnsCol[i].get_dataField() == systemReportTypeFeature)
        {
            document.getElementById('header_SystemReportTypeFeature_SystemReports').innerHTML = GridColumnsCol[i].get_headingText();
            break;
        }
    }
}

function GridSystemBusinessReport_SystemReports_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridSystemReportType_SystemReports').innerHTML = '';
}

function GridSystemEngineReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}
function GridSystemEngineDebugReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}

function GridSystemWindowsServiceReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}

function GridSystemUserActionReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}
function GridSystemDataCollectorReport_SystemReports_onLoad(sender, e) {
    ResetLoadingPanel_SystemReports();
}

function CallBack_GridSystemReportType_SystemReports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GridSystemReportType_SystemReports').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridSystemReportType_SystemReports(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        Changefooter_GridSystemReportType_SystemReports();
        Changeheader_GridSystemReportType_SystemReports(false);
    }
}

function Changeheader_GridSystemReportType_SystemReports(IsReset) {
    var SelectedItem_cmbSystemReportType_SystemReports = cmbSystemReportType_SystemReports.getSelectedItem();
    if (SelectedItem_cmbSystemReportType_SystemReports != undefined && SelectedItem_cmbSystemReportType_SystemReports != null) 
        document.getElementById('header_GridSystemReportType_SystemReports').innerHTML = GetCurrentSystemReportType_SystemReports().Text;
}

function CallBack_GridSystemReportType_SystemReports_onCallbackError(sender, e) {
    ResetLoadingPanel_SystemReports();
    ShowConnectionError_SystemReports();
}

function tlbItemRefresh_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    Refresh_GridSystemReportType_SystemReports();
}

function tlbItemFirst_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    SetPageIndex_GridSystemReportType_SystemReports(0);
}

function tlbItemBefore_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    if (CurrentPageIndex_GridSystemReportType_SystemReports != 0) {
        CurrentPageIndex_GridSystemReportType_SystemReports = CurrentPageIndex_GridSystemReportType_SystemReports - 1;
        SetPageIndex_GridSystemReportType_SystemReports(CurrentPageIndex_GridSystemReportType_SystemReports);
    }
}

function tlbItemNext_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    if (CurrentPageIndex_GridSystemReportType_SystemReports < parseInt(document.getElementById('hfSystemReportTypePageCount_SystemReports').value) - 1) {
        CurrentPageIndex_GridSystemReportType_SystemReports = CurrentPageIndex_GridSystemReportType_SystemReports + 1;
        SetPageIndex_GridSystemReportType_SystemReports(CurrentPageIndex_GridSystemReportType_SystemReports);
    }
}

function tlbItemLast_TlbPaging_GridSystemReportType_SystemReports_onClick() {
    SetPageIndex_GridSystemReportType_SystemReports(parseInt(document.getElementById('hfSystemReportTypePageCount_SystemReports').value) - 1);
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_SystemReports) {
        case 'DeleteAll':
            DialogConfirm.Close();
            UpdateSystemReport_SystemReports();
            break;
        case 'Exit':
            CloseDialogSystemReports();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function CharToKeyCode_SystemReports(str) {
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

function ShowDialogConfirm(confirmState) {
    ConfirmState_SystemReports = confirmState;
    if (ConfirmState_SystemReports == 'DeleteAll')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_SystemReports').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_SystemReports').value;
    DialogConfirm.Show();
}

function ShowConnectionError_SystemReports() {
    var error = document.getElementById('hfErrorType_SystemReports').value;
    var errorBody = document.getElementById('hfConnectionError_SystemReports').value;
    showDialog(error, errorBody, 'error');
}

function ResetLoadingPanel_SystemReports() {
    document.getElementById('loadingPanel_GridSystemReportType_SystemReports').innerHTML = '';
}

function Changefooter_GridSystemReportType_SystemReports() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfSystemReportTypePageCount_SystemReports').value) > 0 ? CurrentPageIndex_GridSystemReportType_SystemReports + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfSystemReportTypePageCount_SystemReports').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridSystemReportType_SystemReports').innerHTML = retfooterVal;
}

function Refresh_GridSystemReportType_SystemReports() {
    CurrentPageState_SystemReports = 'View';
    ObjCurrentSystemReportFilterConditions_SystemReports = null;
    SetPageIndex_GridSystemReportType_SystemReports(0);
}

function CloseDialogSystemReports() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogSystemReports_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogSystemReports').Close();
}

function CollapseControls_SystemReports(exception) {
    if (exception == null || exception != cmbSystemReportType_SystemReports)
        cmbSystemReportType_SystemReports.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_SystemReports');
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function tlbItemReportView_TlbReportView_onClick() {
    ChangeCurrentSystemReportType_SystemReports();
    Changeheader_GridSystemReportType_SystemReports(false);
    CreateSystemReportsFilterConditions_SystemReports();
    ResetSystemReportTypeFeature_SystemReports();
    GetReport_SystemReports();
}
function GetReport_SystemReports(){
    var FilterConditions = '';
    var SearchTerm = '';
    var FromDate = '';
    var ToDate = '';
    if (ObjCurrentSystemReportFilterConditions_SystemReports != null) {
        var SearchTerm = ObjCurrentSystemReportFilterConditions_SystemReports.SearchTerm;
        var FromDate = ObjCurrentSystemReportFilterConditions_SystemReports.FromDate;
        var ToDate = ObjCurrentSystemReportFilterConditions_SystemReports.ToDate;
        var Concept = ObjCurrentSystemReportFilterConditions_SystemReports.Concept;
        var RuleCode = ObjCurrentSystemReportFilterConditions_SystemReports.RuleCode;
    }
    FilterConditions = '{"SearchTerm":"' + SearchTerm + '","FromDate":"' + FromDate + '","ToDate":"' + ToDate + '","Concept":"' + Concept + '","RuleCode":"' + RuleCode + '"}';
    GetReport_SystemReportsPage(CharToKeyCode_SystemReports(GetCurrentSystemReportType_SystemReports().Value),CharToKeyCode_SystemReports(FilterConditions));
}

function GetReport_SystemReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SystemReports').value;
            Response[1] = document.getElementById('hfConnectionError_SystemReports').value;
        }
        if (RetMessage[2] == 'success')
            ShowReport_SystemReports(Response);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function ShowReport_SystemReports(Response) {
    var stiReportGUID = Response[3];
    var reportName = parent.DialogReportParameters.get_value().ReportName;
    var NewReportWindow = window.open("MasterReportViewer.aspx?ReportGUID=" + stiReportGUID + "&ReportTitle=" + CharToKeyCode_SystemReports(reportName) + "&IsDesigned=" + CharToKeyCode_SystemReports(false.toString()) + "&IsContainsForm=" + CharToKeyCode_SystemReports(false.toString()) + "&Width=" + CharToKeyCode_SystemReports((screen.width).toString()) + "&Height=" + CharToKeyCode_SystemReports((screen.height).toString()), "MasterReportViewer" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
}

function tlbItemRegisterPersonnelRuleDebugSettings_TlbPersonnelRuleDebugSettings_onClick() {
    RegisterPersonnelRuleDebugSettings_SystemReports();
}

function RegisterPersonnelRuleDebugSettings_SystemReports() {
    var personnelCode = document.getElementById('txtPersonnelCode_SystemReports').value;
    var isRuleDebugActive = document.getElementById('chbPersonnelRuleDebugActive_SystemReports').checked;
    RegisterPersonnelRuleDebugSettings_SystemReportsPage(CharToKeyCode_SystemReports(personnelCode), CharToKeyCode_SystemReports(isRuleDebugActive.toString()));
    DialogWaiting.Show();
}

function RegisterPersonnelRuleDebugSettings_SystemReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    var message = null;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SystemReports').value;
            Response[1] = document.getElementById('hfConnectionError_SystemReports').value;
        }
        if (RetMessage[2] == 'success') {
            var ruleDebugPersonnelFeatures = eval('(' + RetMessage[3] + ')');
            message = ruleDebugPersonnelFeatures.Name;
            CurrentRuleDebugPersonnelFeatures_SystemReports = ruleDebugPersonnelFeatures;
            ViewSystemReportResult_SystemReports();
        }
        else
            message = Response[1];
        document.getElementById('tdPersonnelRuleDebugSettingsMessage_SystemReports').innerHTML = message;
    }
}

function cmbSystemReportType_SystemReports_onChange(sender, e) {
    if (cmbSystemReportType_SystemReports.getSelectedItem() != null && cmbSystemReportType_SystemReports.getSelectedItem() != undefined) {
        var systemReportType = cmbSystemReportType_SystemReports.getSelectedItem().get_value();
        var isSystemEngineDebugReport = systemReportType == "SystemEngineDebugReport" ? true : false;
        FillPersonnelRuleDebugSettings_SystemReports(isSystemEngineDebugReport);
        ChangeFilterState_SystemReports(isSystemEngineDebugReport);
    }
}

function FillPersonnelRuleDebugSettings_SystemReports(IsSystemEngineDebugReport) {
    CurrentRuleDebugPersonnelFeatures_SystemReports = document.getElementById('hfCurrentRuleDebugPersonnelFeatures_SystemReports').value;
    if (IsSystemEngineDebugReport && CurrentRuleDebugPersonnelFeatures_SystemReports != undefined && CurrentRuleDebugPersonnelFeatures_SystemReports != null && CurrentRuleDebugPersonnelFeatures_SystemReports != "") 
        CurrentRuleDebugPersonnelFeatures_SystemReports = eval('(' + CurrentRuleDebugPersonnelFeatures_SystemReports + ')');
}

function ChangeFilterState_SystemReports(IsSystemEngineDebugReport) {
    if (IsSystemEngineDebugReport) {
        document.getElementById('tblGeneralSearchTermContainer_SystemReports').style.visibility = 'hidden';
        document.getElementById('tblPersonnelRuleDebugContainer_SystemReports').style.visibility = 'visible';
        document.getElementById('tblConceptContainer_SystemReports').style.visibility = 'visible';
        document.getElementById('tblRuleCodeContainer_SystemReports').style.visibility = 'visible';
        document.getElementById('tdCalculationContainer_SystemReports').style.visibility = 'visible';
        document.getElementById('tdClearContainer_FromDateCalendars_SystemReports').style.visibility = 'hidden';
        document.getElementById('tdClearContainer_ToDateCalendars_SystemReports').style.visibility = 'hidden';
        document.getElementById('txtSearchTerm_SystemReports').value = '';
        CurrentRuleDebugPersonnelFeatures_SystemReports = document.getElementById('hfCurrentRuleDebugPersonnelFeatures_SystemReports').value;
        if (IsSystemEngineDebugReport && CurrentRuleDebugPersonnelFeatures_SystemReports != undefined && CurrentRuleDebugPersonnelFeatures_SystemReports != null && CurrentRuleDebugPersonnelFeatures_SystemReports != "") {
            CurrentRuleDebugPersonnelFeatures_SystemReports = eval('(' + CurrentRuleDebugPersonnelFeatures_SystemReports + ')');
            document.getElementById('txtPersonnelCode_SystemReports').value = CurrentRuleDebugPersonnelFeatures_SystemReports.Barcode;
            document.getElementById('chbPersonnelRuleDebugActive_SystemReports').checked = CurrentRuleDebugPersonnelFeatures_SystemReports.IsRuleDebugActive;
            document.getElementById('tdPersonnelRuleDebugSettingsMessage_SystemReports').innerHTML = CurrentRuleDebugPersonnelFeatures_SystemReports.Name;
        }
        var currentDate_SystemReports = document.getElementById('hfCurrentDate_SystemReports').value;
        switch (parent.SysLangID) {
            case 'en-US':
                currentDate_SystemReports = new Date(currentDate_SystemReports);
                gdpFromDate_SystemReports.setSelectedDate(currentDate_SystemReports);
                gCalFromDate_SystemReports.setSelectedDate(currentDate_SystemReports);
                gdpToDate_SystemReports.setSelectedDate(currentDate_SystemReports);
                gCalToDate_SystemReports.setSelectedDate(currentDate_SystemReports);
                break;
            case 'fa-IR':
                document.getElementById('pdpFromDate_SystemReports').value = currentDate_SystemReports;
                document.getElementById('pdpToDate_SystemReports').value = currentDate_SystemReports;
                break;
        }
    }
    else {
        document.getElementById('tblGeneralSearchTermContainer_SystemReports').style.visibility = 'visible';
        document.getElementById('tblPersonnelRuleDebugContainer_SystemReports').style.visibility = 'hidden';
        document.getElementById('tblConceptContainer_SystemReports').style.visibility = 'hidden';
        document.getElementById('tblRuleCodeContainer_SystemReports').style.visibility = 'hidden';
        document.getElementById('tdCalculationContainer_SystemReports').style.visibility = 'hidden';
        document.getElementById('tdClearContainer_FromDateCalendars_SystemReports').style.visibility = 'visible';
        document.getElementById('tdClearContainer_ToDateCalendars_SystemReports').style.visibility = 'visible';
        document.getElementById('txtPersonnelCode_SystemReports').value = '';
        document.getElementById('chbPersonnelRuleDebugActive_SystemReports').checked = false;
        document.getElementById('txtConcept_SystemReports').value = '';
        document.getElementById('txtRuleCode_SystemReports').value = '';
        document.getElementById('tdPersonnelRuleDebugSettingsMessage_SystemReports').innerHTML = '';
        switch (parent.SysLangID) {
            case 'en-US':
                document.getElementById('gdpFromDate_SystemReports_picker').value = "";
                document.getElementById('gdpToDate_SystemReports_picker').value = "";
                break;
            case 'fa-IR':
                document.getElementById('pdpFromDate_SystemReports').value = "";
                document.getElementById('pdpToDate_SystemReports').value = "";
                break;
        }
        CurrentRuleDebugPersonnelFeatures_SystemReports = null;
    }
}

function tlbItemCalculation_TlbCalculation_onClick() {
    Calculate_SystemReports();
}

function Calculate_SystemReports() {
    var personnelID = CurrentRuleDebugPersonnelFeatures_SystemReports != undefined && CurrentRuleDebugPersonnelFeatures_SystemReports != null && CurrentRuleDebugPersonnelFeatures_SystemReports != "" ? CurrentRuleDebugPersonnelFeatures_SystemReports.ID : '0';
    var fromDate = null;
    var toDate = null;
    switch (parent.SysLangID) {
        case 'fa-IR':
            fromDate = document.getElementById('pdpFromDate_SystemReports').value;
            toDate = document.getElementById('pdpToDate_SystemReports').value;
            break;
        case 'en-US':
            fromDate = document.getElementById('gdpFromDate_SystemReports_picker').value;
            toDate = document.getElementById('gdpToDate_SystemReports_picker').value;
            break;
    }
    Calculate_SystemReportsPage(CharToKeyCode_SystemReports(personnelID.toString()), CharToKeyCode_SystemReports(fromDate), CharToKeyCode_SystemReports(toDate));
    DialogWaiting.Show();
}

function Calculate_SystemReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    var message = null;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_SystemReports').value;
            Response[1] = document.getElementById('hfConnectionError_SystemReports').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}




