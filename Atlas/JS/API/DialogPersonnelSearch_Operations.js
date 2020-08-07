
var CurrentPageCombosCallBcakStateObj = new Object();
var ObjexpandingOrgPostNode_PersonnelSearch = null;
var DepartmentSelectedType_PersonnelSearch = 'Normal';
var OrganizationPostSelectedType_PersonnelSearch = null;
var NodesSelectedList_TrvDepartment_PersonnelSearch = '';
var CountSelectedNode_trvDepartment_PersonnelSearch = 0;
var NodesSelectedList_TrvEmployType_PersonnelSearch = '';
var CountSelectedNode_trvEmployType_PersonnelSearch = 0;
var NodesSelectedList_TrvControlStation_PersonnelSearch = '';
var CountSelectedNode_trvControlStation_PersonnelSearch = 0;
var NodesSelectedList_trvUIValidationRulesGroup_PersonnelSearch = '';
var CountSelectedNode_trvUIValidationRulesGroup_PersonnelSearch = 0;
function ViewCurrentLangCalendars_PersonnelSearch() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpFromDate_DualCalendars_PersonnelSearch").parentNode.removeChild(document.getElementById("pdpFromDate_DualCalendars_PersonnelSearch"));
            document.getElementById("pdpFromDate_DualCalendars_PersonnelSearchimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_DualCalendars_PersonnelSearchimgbt"));
            document.getElementById("pdpToDate_DualCalendars_PersonnelSearch").parentNode.removeChild(document.getElementById("pdpToDate_DualCalendars_PersonnelSearch"));
            document.getElementById("pdpToDate_DualCalendars_PersonnelSearchimgbt").parentNode.removeChild(document.getElementById("pdpToDate_DualCalendars_PersonnelSearchimgbt"));
            document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearch").parentNode.removeChild(document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearch"));
            document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearchimgbt").parentNode.removeChild(document.getElementById("pdpFromDate_SingleCalendar_PersonnelSearchimgbt"));
            break;
        case 'fa-IR':
            document.getElementById("Container_FromDateCalendars_DualCalendars_PersonnelSearch").removeChild(document.getElementById("Container_gCalFromDate_DualCalendars_PersonnelSearch"));
            document.getElementById("Container_ToDateCalendars_DualCalendars_PersonnelSearch").removeChild(document.getElementById("Container_gCalToDate_DualCalendars_PersonnelSearch"));
            document.getElementById("Container_FromDateCalendars_SingleCalendar_PersonnelSearch").removeChild(document.getElementById("Container_gCalFromDate_SingleCalendar_PersonnelSearch"));
            break;
    }
}

function ChangeComboTreeDirection_PersonnelSearch() {
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            document.getElementById('trvDepartment_PersonnelSearch').style.direction = 'ltr';
            document.getElementById('trvOrganizationPost_PersonnelSearch').style.direction = 'ltr';
            break;
        case 'en-US':
            document.getElementById('trvDepartment_PersonnelSearch').style.direction = 'rtl';
            document.getElementById('trvOrganizationPost_PersonnelSearch').style.direction = 'rtl';
            break;
    }
}


function gdpFromDate_DualCalendars_PersonnelSearch_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gCalFromDate_DualCalendars_PersonnelSearch.setSelectedDate(FromDate);
}
function gCalFromDate_DualCalendars_PersonnelSearch_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gdpFromDate_DualCalendars_PersonnelSearch.setSelectedDate(FromDate);
}
function btn_gdpFromDate_DualCalendars_PersonnelSearch_OnClick(event) {
    if (gCalFromDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        gCalFromDate_DualCalendars_PersonnelSearch.hide();
    }
    else {
        gCalFromDate_DualCalendars_PersonnelSearch.setSelectedDate(gdpFromDate_DualCalendars_PersonnelSearch.getSelectedDate());
        gCalFromDate_DualCalendars_PersonnelSearch.show();
    }
}
function btn_gdpFromDate_DualCalendars_PersonnelSearch_OnMouseUp(event) {
    if (gCalFromDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_DualCalendars_PersonnelSearch_onLoad(sender, e) {
    window.gCalFromDate_DualCalendars_PersonnelSearch.PopUpObject.z = 25000000;
}

function gdpToDate_DualCalendars_PersonnelSearch_OnDateChange(sender, eventArgs) {
    var ToDate = gdpToDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gCalToDate_DualCalendars_PersonnelSearch.setSelectedDate(ToDate);
}
function gCalToDate_DualCalendars_PersonnelSearch_OnChange(sender, eventArgs) {
    var ToDate = gCalToDate_DualCalendars_PersonnelSearch.getSelectedDate();
    gdpToDate_DualCalendars_PersonnelSearch.setSelectedDate(ToDate);
}
function btn_gdpToDate_DualCalendars_PersonnelSearch_OnClick(event) {
    if (gCalToDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        gCalToDate_DualCalendars_PersonnelSearch.hide();
    }
    else {
        gCalToDate_DualCalendars_PersonnelSearch.setSelectedDate(gdpToDate_DualCalendars_PersonnelSearch.getSelectedDate());
        gCalToDate_DualCalendars_PersonnelSearch.show();
    }
}
function btn_gdpToDate_DualCalendars_PersonnelSearch_OnMouseUp(event) {
    if (gCalToDate_DualCalendars_PersonnelSearch.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalToDate_DualCalendars_PersonnelSearch_onLoad(sender, e) {
    window.gCalToDate_DualCalendars_PersonnelSearch.PopUpObject.z = 25000000;
}

function gdpFromDate_SingleCalendar_PersonnelSearch_OnDateChange(sender, eventArgs) {
    var FromDate = gdpFromDate_SingleCalendar_PersonnelSearch.getSelectedDate();
    gCalFromDate_SingleCalendar_PersonnelSearch.setSelectedDate(FromDate);
}
function gCalFromDate_SingleCalendar_PersonnelSearch_OnChange(sender, eventArgs) {
    var FromDate = gCalFromDate_SingleCalendar_PersonnelSearch.getSelectedDate();
    gdpFromDate_SingleCalendar_PersonnelSearch.setSelectedDate(FromDate);
}
function btn_gdpFromDate_SingleCalendar_PersonnelSearch_OnClick(event) {
    if (gCalFromDate_SingleCalendar_PersonnelSearch.get_popUpShowing()) {
        gCalFromDate_SingleCalendar_PersonnelSearch.hide();
    }
    else {
        gCalFromDate_SingleCalendar_PersonnelSearch.setSelectedDate(gdpFromDate_SingleCalendar_PersonnelSearch.getSelectedDate());
        gCalFromDate_SingleCalendar_PersonnelSearch.show();
    }
}
function btn_gdpFromDate_SingleCalendar_PersonnelSearch_OnMouseUp(event) {
    if (gCalFromDate_SingleCalendar_PersonnelSearch.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function gCalFromDate_SingleCalendar_PersonnelSearch_onLoad(sender, e) {
    window.gCalFromDate_SingleCalendar_PersonnelSearch.PopUpObject.z = 25000000;
}

function GetBoxesHeaders_PersonnelSearch() {
    parent.document.getElementById('Title_DialogPersonnelSearch').innerHTML = document.getElementById('hfTitle_DialogPersonnelSearch').value;
}

function tlbItemSave_TlbPersonnelSearch_onClick() {
    CollapseControls_PersonnelSearch();
    CloseDialogPersonnelSearch();
}

function PersonnelSearch_onSave() {
    var StrObjPersonnelSearch = '';
    var FirstName = '';
    var LastName = '';
    var NationalCode = '';
    var PersonnelNumber = '';
    var Active = '';
    var Sex = '-1';
    var FatherName = '';
    var MarriageState = '-1';
    var MilitaryState = '-1';
    var Education = '';
    var BirthLocation = '';
    var CardNumber = '0';
    var EmployNumber = '';
    //var DepartmentID = '0';
    var DepartmentID = '';
    var IsContainsSubDepartment = false;
    var OrganizationPostID = '0';
    var EmployTypeID = '0';
    var EmployFromDate = '';
    var EmployToDate = '';
    var ControlStationID = '0';
    var FromBirthDate = '';
    var ToBirthDate = '';
    var WorkGroupID = '0';
    var WorkGroupFromDate = '';
    var RuleGroupID = '0';
    var RuleGroupFromDate = '';
    var RuleGroupToDate = '';
    var CalculationRangeID = '0';
    var CalculationRangeFromDate = '';
    var IsDeleted = '';
    var GradeID = '0';
    var CostCenterID = '0';
    var UiValidationGroupID = '0';
    var ContractID = '0';
    var ContractFromDate = '';
    var ContractToDate = '';
    if (document.getElementById('rdbIsDeleted_PersonnelSearch').checked) {
        IsDeleted = true;
        // Active = null;
    }
    else
        if (document.getElementById('rdbAllPersonnel_PersonnelSearch').checked)
            Active = '';
        else
            if (document.getElementById('rdbActive_PersonnelSearch').checked)
                Active = true;
            else
                if (document.getElementById('rdbDeactive_PersonnelSearch').checked)
                    Active = false;
    FirstName = document.getElementById('txtFirstName_PersonnelSearch').value;
    LastName = document.getElementById('txtLastName_PersonnelSearch').value;
    NationalCode = document.getElementById('txtNationalCode_PersonnelSearch').value;
    PersonnelNumber = document.getElementById('txtPersonnelNumber_PersonnelSearch').value;
    if (cmbSex_PersonnelSearch.getSelectedItem() != undefined)
        Sex = cmbSex_PersonnelSearch.getSelectedItem().get_value();
    FatherName = document.getElementById('txtFatherName_PersonnelSearch').value;
    if (cmbMarriageState_PersonnelSearch.getSelectedItem() != undefined)
        MarriageState = cmbMarriageState_PersonnelSearch.getSelectedItem().get_value();
    if (cmbMilitaryState_PersonnelSearch.getSelectedItem() != undefined)
        MilitaryState = cmbMilitaryState_PersonnelSearch.getSelectedItem().get_value();
    Education = document.getElementById('txtEducation_PersonnelSearch').value;
    BirthLocation = document.getElementById('txtBirthLocation_PersonnelSearch').value;
    CardNumber = document.getElementById('txtCardNumber_PersonnelSearch').value;
    EmployNumber = document.getElementById('txtEmployNumber_PersonnelSearch').value;
    switch (DepartmentSelectedType_PersonnelSearch) {
        case 'Search':
            var selectedItem_cmbDepartmentSearchResult_PersonnelSearch = cmbDepartmentSearchResult_PersonnelSearch.getSelectedItem();
            if (selectedItem_cmbDepartmentSearchResult_PersonnelSearch != undefined && selectedItem_cmbDepartmentSearchResult_PersonnelSearch != null) {
                var departmentObj = selectedItem_cmbDepartmentSearchResult_PersonnelSearch.get_value();
                departmentObj = eval('(' + departmentObj + ')');
                DepartmentID = departmentObj.ID;
            }
            break;
        case 'Normal':
            DepartmentID = NodesSelectedList_TrvDepartment_PersonnelSearch;
            //if (trvDepartment_PersonnelSearch.get_selectedNode() != undefined)
            //     DepartmentID = trvDepartment_PersonnelSearch.get_selectedNode().get_id();

            break;
    }
    IsContainsSubDepartment = document.getElementById('chbSubDepartment_PersonnelSearch').checked;

    switch (OrganizationPostSelectedType_PersonnelSearch) {
        case 'Search':
            var selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch = cmbOrganizationPostSearchResult_PersonnelSearch.getSelectedItem();
            if (selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch != undefined && selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch != null) {
                var organizationPostObj = selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch.get_value();
                organizationPostObj = eval('(' + organizationPostObj + ')');
                OrganizationPostID = organizationPostObj.ID;
            }
            break;
        case 'Normal':
            if (trvOrganizationPost_PersonnelSearch.get_selectedNode() != undefined)
                OrganizationPostID = trvOrganizationPost_PersonnelSearch.get_selectedNode().get_id();
            break;
        default:
            break;
    }




    //if (cmbEmployType_PersonnelSearch.getSelectedItem() != undefined)
    //    EmployTypeID = cmbEmployType_PersonnelSearch.getSelectedItem().get_value();
    EmployTypeID = NodesSelectedList_TrvEmployType_PersonnelSearch;
    EmployFromDate = document.getElementById('txtFromDate_EmployDate_PersonnelSearch').value;
    EmployToDate = document.getElementById('txtToDate_EmployDate_PersonnelSearch').value;
    //if (cmbControlStation_PersonnelSearch.getSelectedItem() != undefined)
    //    ControlStationID = cmbControlStation_PersonnelSearch.getSelectedItem().get_value();
    ControlStationID = NodesSelectedList_TrvControlStation_PersonnelSearch;
    FromBirthDate = document.getElementById('txtFromDate_BirthDate_PersonnelSearch').value;
    ToBirthDate = document.getElementById('txtToDate_BirthDate_PersonnelSearch').value;
    if (cmbWorkGroups_PersonnelSearch.getSelectedItem() != undefined)
        WorkGroupID = cmbWorkGroups_PersonnelSearch.getSelectedItem().get_value();
    WorkGroupFromDate = document.getElementById('txtFromDate_WorkGroups_PersonnelSearch').value;
    if (cmbRuleGroups_PersonnelSearch.getSelectedItem() != undefined)
        RuleGroupID = cmbRuleGroups_PersonnelSearch.getSelectedItem().get_value();
    RuleGroupFromDate = document.getElementById('txtFromDate_RuleGroups_PersonnelSearch').value;
    RuleGroupToDate = document.getElementById('txtToDate_RuleGroups_PersonnelSearch').value;
    if (cmbCalculationRange_PersonnelSearch.getSelectedItem() != undefined)
        CalculationRangeID = cmbCalculationRange_PersonnelSearch.getSelectedItem().get_value();
    CalculationRangeFromDate = document.getElementById('txtFromDate_CalculationDateRange_PersonnelSearch').value;

    if (cmbGrade_PersonnelSearch.getSelectedItem() != undefined)
        GradeID = cmbGrade_PersonnelSearch.getSelectedItem().get_value();
    if (cmbContract_PersonnelSearch.getSelectedItem() != undefined)
        ContractID = cmbContract_PersonnelSearch.getSelectedItem().get_value();
    ContractFromDate = document.getElementById('txtFromDate_Contract_PersonnelSearch').value;
    ContractToDate = document.getElementById('txtToDate_Contract_PersonnelSearch').value;
    if (cmbCostCenter_PersonnelSearch.getSelectedItem() != undefined)
        CostCenterID = cmbCostCenter_PersonnelSearch.getSelectedItem().get_value();
    UiValidationGroupID = NodesSelectedList_trvUIValidationRulesGroup_PersonnelSearch;
    StrObjPersonnelSearch = '{"FirstName":"' + FirstName + '","LastName":"' + LastName + '","NationalCode":"' + NationalCode + '","PersonnelNumber":"' + PersonnelNumber + '","Active":"' + Active + '","Sex":"' + Sex + '","FatherName":"' + FatherName + '","MarriageState":"' + MarriageState + '","MilitaryState":"' + MilitaryState + '","Education":"' + Education + '","BirthLocation":"' + BirthLocation + '","CardNumber":"' + CardNumber + '","EmployNumber":"' + EmployNumber + '","DepartmentID":"' + DepartmentID + '","IsContainsSubDepartment":"' + IsContainsSubDepartment.toString() + '","OrganizationPostID":"' + OrganizationPostID + '","EmployTypeID":"' + EmployTypeID + '","EmployFromDate":"' + EmployFromDate + '","EmployToDate":"' + EmployToDate + '","ControlStationID":"' + ControlStationID + '","FromBirthDate":"' + FromBirthDate + '","ToBirthDate":"' + ToBirthDate + '","WorkGroupID":"' + WorkGroupID + '","WorkGroupFromDate":"' + WorkGroupFromDate + '","RuleGroupID":"' + RuleGroupID + '","RuleGroupFromDate":"' + RuleGroupFromDate + '","RuleGroupToDate":"' + RuleGroupToDate + '","CalculationRangeID":"' + CalculationRangeID + '","CalculationRangeFromDate":"' + CalculationRangeFromDate + '","GradeID":"' + GradeID + 
    '","CostCenterID":"' + CostCenterID + '" , "IsDeleted": "' + IsDeleted + '" , "UIValidationGroupID":"' + UiValidationGroupID + '" , "ContractID":"' + ContractID + '" , "ContractFromDate":"' + ContractFromDate + '" , "ContractToDate":"' + ContractToDate + '"}';
    FetchRelativeOperation_PersonnelSearch(StrObjPersonnelSearch);
}

function FetchRelativeOperation_PersonnelSearch(StrObjPersonnelSearch) {
    var caller = parent.eval(parent.ClientPerfixId + 'DialogPersonnelSearch').get_value().Caller;
    switch (caller) {
        case 'Manager_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.Substitute_onAfterPersonnelAdvancedSearch('Manager', StrObjPersonnelSearch);
            break;
        case 'Personnel_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.Substitute_onAfterPersonnelAdvancedSearch('Substitute', StrObjPersonnelSearch);
            break;
        case 'Operators':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogOperators_IFrame').contentWindow.Operators_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'RegisteredRequests':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogRegisteredRequests_IFrame').contentWindow.RegisteredRequests_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'RequestRegister':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogRegisteredRequests_IFrame').contentWindow.document.getElementById('DialogRequestRegister_IFrame').contentWindow.RequestRegister_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'LeaveRemains':
            parent.document.getElementById('pgvMasterLeaveRemains_iFrame').contentWindow.LeaveRemains_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'MasterPersonnel':
            parent.document.getElementById('pgvPersonnelIntroduction_iFrame').contentWindow.MasterPersonnelMainInformation_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'Users':
            parent.document.getElementById('pgvUsersIntroduction_iFrame').contentWindow.Users_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'UnderManagementPersonnel':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogUnderManagementPersonnel_IFrame').contentWindow.UnderManagementPersonnel_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'PersonnelOrganizationFeaturesChange':
            parent.document.getElementById('pgvPersonnelOrganizationFeaturesChange_iFrame').contentWindow.PersonnelOrganizationFeaturesChange_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'MasterTrafficsControl':
            parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.MasterTrafficsControl_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'MasterExceptionShifts':
            parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').contentWindow.MasterExceptionShifts_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'ExceptionShifts':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogExceptionShifts_IFrame').contentWindow.ExceptionShifts_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'Calculations':
            parent.document.getElementById('pgvCalculations_iFrame').contentWindow.Calculations_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'UserSettings':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogUserSettings_IFrame').contentWindow.UserSettings_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'ManagersWorkFlow':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogUnderManagementPersonnel_IFrame').contentWindow.document.getElementById('DialogManagersWorkFlow_IFrame').contentWindow.ManagersWorkFlow_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'UpdateCalculationResult':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').contentWindow.UpdateCalculationResult_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'HourlyRequestOnAbsence':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.document.getElementById('DialogHourlyRequestOnAbsence_IFrame').contentWindow.HourlyRequestOnAbsence_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'DailyRequestOnAbsence':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyOperationGridSchema_IFrame').contentWindow.document.getElementById('DialogDailyRequestOnAbsence_IFrame').contentWindow.DailyRequestOnAbsence_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;
        case 'DialogWorkFlowDetail':
            parent.document.getElementById('DialogWorkFlowDetail_IFrame').contentWindow.WorkFlowDetail_onAfterPersonnelAdvancedSearch(StrObjPersonnelSearch);
            break;


    }
}

function tlbItemFormReconstruction_TlbPersonnelSearch_onClick() {
    CloseDialogPersonnelSearch();
    var caller = parent.eval(parent.ClientPerfixId + 'DialogPersonnelSearch').get_value().Caller;
    switch (caller) {
        case 'Manager_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('Manager_Substitute');
            break;
        case 'Personnel_Substitute':
            parent.document.getElementById('pgvSubstituteIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('Personnel_Substitute');
            break;
        case 'Operators':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogOperators_IFrame').contentWindow.ShowDialogPersonnelSearch('Operators');
            break;
        case 'RegisteredRequests':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogRegisteredRequests_IFrame').contentWindow.ShowDialogPersonnelSearch('RegisteredRequests');
            break;
        case 'RequestRegister':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogRegisteredRequests_IFrame').contentWindow.document.getElementById('DialogRequestRegister_IFrame').contentWindow.ShowDialogPersonnelSearch('RequestRegister');
            break;
        case 'LeaveRemains':
            parent.document.getElementById('pgvMasterLeaveRemains_iFrame').contentWindow.ShowDialogPersonnelSearch('LeaveRemains');
            break;
        case 'MasterPersonnel':
            parent.document.getElementById('pgvPersonnelIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('MasterPersonnel');
            break;
        case 'Users':
            parent.document.getElementById('pgvUsersIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('Users');
            break;
        case 'UnderManagementPersonnel':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogUnderManagementPersonnel_IFrame').contentWindow.ShowDialogPersonnelSearch('UnderManagementPersonnel');
            break;
        case 'PersonnelOrganizationFeaturesChange':
            parent.document.getElementById('pgvPersonnelOrganizationFeaturesChange_iFrame').contentWindow.ShowDialogPersonnelSearch('PersonnelOrganizationFeaturesChange');
            break;
        case 'MasterTrafficsControl':
            parent.document.getElementById('pgvTrafficsControl_iFrame').contentWindow.ShowDialogPersonnelSearch('MasterTrafficsControl');
            break;
        case 'MasterExceptionShifts':
            parent.document.getElementById('pgvExceptionShiftsIntroduction_iFrame').contentWindow.ShowDialogPersonnelSearch('MasterExceptionShifts');
            break;
        case 'ExceptionShifts':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogExceptionShifts_IFrame').contentWindow.ShowDialogPersonnelSearch('ExceptionShifts');
            break;
        case 'Calculations':
            parent.document.getElementById('pgvCalculations_iFrame').contentWindow.ShowDialogPersonnelSearch('Calculations');
            break;
        case 'UserSettings':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogUserSetings_IFrame').contentWindow.ShowDialogPersonnelSearch('UserSettings');
            break;
        case 'ManagersWorkFlow':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogManagersWorkFlow_IFrame').contentWindow.ShowDialogPersonnelSearch('ManagersWorkFlow');
            break;
        case 'UpdateCalculationResult':
            parent.document.getElementById(parent.ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').contentWindow.ShowDialogPersonnelSearch('UpdateCalculationResult');
            break;
    }
}


function tlbItemExit_TlbPersonnelSearch_onClick() {
    ShowDialogConfirm();
}

function cmbSex_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbSex_PersonnelSearch);
    if (cmbSex_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSex_PersonnelSearch = true;
        Fill_cmbSex_PersonnelSearch();
    }
}

function Fill_cmbSex_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbSex_PersonnelSearch');
    CallBack_cmbSex_PersonnelSearch.callback();
}

function CallBack_cmbSex_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbSex_PersonnelSearch.dispose();
}

function CallBack_cmbSex_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Sex_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbSex_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSex_PersonnelSearch_DropImage').mousedown();
        cmbSex_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbSex_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbSex_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_Sex_PersonnelSearch_onClick() {
    document.getElementById('cmbSex_PersonnelSearch_Input').value = '';
    cmbSex_PersonnelSearch.unSelect();
}

function cmbMarriageState_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbMarriageState_PersonnelSearch);
    if (cmbMarriageState_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMarriageState_PersonnelSearch = true;
        Fill_cmbMarriageState_PersonnelSearch();
    }
}

function Fill_cmbMarriageState_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbMarriageState_PersonnelSearch');
    CallBack_cmbMarriageState_PersonnelSearch.callback();
}

function CallBack_cmbMarriageState_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbMarriageState_PersonnelSearch.dispose();
}

function CallBack_cmbMarriageState_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MarriageState_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbMarriageState_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMarriageState_PersonnelSearch_DropImage').mousedown();
        cmbMarriageState_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMarriageState_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMarriageState_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_MarriageState_PersonnelSearch_onClick() {
    document.getElementById('cmbMarriageState_PersonnelSearch_Input').value = '';
    cmbMarriageState_PersonnelSearch.unSelect();
}

function cmbMilitaryState_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbMilitaryState_PersonnelSearch);
    if (cmbMilitaryState_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbMilitaryState_PersonnelSearch = true;
        Fill_cmbMilitaryState_PersonnelSearch();
    }
}

function Fill_cmbMilitaryState_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbMilitaryState_PersonnelSearch');
    CallBack_cmbMilitaryState_PersonnelSearch.callback();
}

function CallBack_cmbMilitaryState_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbMilitaryState_PersonnelSearch.dispose();
}

function CallBack_cmbMilitaryState_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MilitaryState_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbMilitaryState_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbMilitaryState_PersonnelSearch_DropImage').mousedown();
        cmbMilitaryState_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbMilitaryState_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbMilitaryState_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_MilitaryState_PersonnelSearch_onClick() {
    document.getElementById('cmbMilitaryState_PersonnelSearch_Input').value = '';
    cmbMilitaryState_PersonnelSearch.unSelect();
}


function trvDepartment_PersonnelSearch_onNodeCheckChange(sender, e) {
    DepartmentSelectedType_PersonnelSearch = 'Normal';
    var checkedNodeStatus_trvDepartment_PersonnelSearch = e.get_node().Checked;
    if (checkedNodeStatus_trvDepartment_PersonnelSearch) {
        NodesSelectedList_TrvDepartment_PersonnelSearch += '#' + e.get_node().get_id() + '#,';
        CountSelectedNode_trvDepartment_PersonnelSearch += 1;
    }
    else {
        NodesSelectedList_TrvDepartment_PersonnelSearch = NodesSelectedList_TrvDepartment_PersonnelSearch.replace('#' + e.get_node().get_id() + '#', '');
        CountSelectedNode_trvDepartment_PersonnelSearch -= 1;
    }
    if (CountSelectedNode_trvDepartment_PersonnelSearch == 0) {
        cmbDepartment_PersonnelSearch.set_text('');
    }
    else {
        cmbDepartment_PersonnelSearch.set_text(CountSelectedNode_trvDepartment_PersonnelSearch.toString() + ' ' + document.getElementById('hfCountSelectedNodeHiddenField_TrvDepartment_PersonnelSearch').value);
    }

}

function trvDepartment_PersonnelSearch_onNodeSelect(sender, e) {
    //DepartmentSelectedType_PersonnelSearch = 'Normal';
    //cmbDepartment_PersonnelSearch.set_text(e.get_node().get_text());
    //cmbDepartment_PersonnelSearch.collapse();
}

function cmbDepartment_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbDepartment_PersonnelSearch);

    if (trvDepartment_PersonnelSearch.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDepartment_PersonnelSearch = true;
        Fill_cmbDepartment_PersonnelSearch();
    }

}

function Fill_cmbDepartment_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbDepartment_PersonnelSearch');
    CallBack_cmbDepartment_PersonnelSearch.callback();
}

function CallBack_cmbDepartment_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbDepartment_PersonnelSearch.dispose();
}

function CallBack_cmbDepartment_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Department_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbDepartment_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDepartment_PersonnelSearch_DropImage').mousedown();
        NodesSelectedList_TrvDepartment_PersonnelSearch = '';
        CountSelectedNode_trvDepartment_PersonnelSearch = 0;
        cmbDepartment_PersonnelSearch.expand();
        ChangeDirection_trvDepartment_PersonnelSearch();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDepartment_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDepartment_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_Department_PersonnelSearch_onClick() {
    document.getElementById('cmbDepartment_PersonnelSearch_Input').value = '';
    cmbDepartment_PersonnelSearch.unSelect();
    trvDepartment_PersonnelSearch.SelectedNode = undefined;
    trvDepartment_PersonnelSearch.unCheckAll();
    NodesSelectedList_TrvDepartment_PersonnelSearch = '';
    CountSelectedNode_trvDepartment_PersonnelSearch = 0;
}

function Refresh_cmbDepartment_PersonnelSearch() {
    Fill_cmbDepartment_PersonnelSearch();
}

function trvOrganizationPost_PersonnelSearch_onNodeSelect(sender, e) {
    OrganizationPostSelectedType_PersonnelSearch = 'Normal';
    cmbOrganizationPost_PersonnelSearch.set_text(e.get_node().get_text());
    cmbOrganizationPost_PersonnelSearch.collapse();
}

function trvOrganizationPost_PersonnelSearch_onCallbackComplete(sender, e) {
    if (ObjexpandingOrgPostNode_PersonnelSearch != null) {
        if (ObjexpandingOrgPostNode_PersonnelSearch.Node.get_nodes().get_length() == 0 && ObjexpandingOrgPostNode_PersonnelSearch.HasChild) {
            ObjexpandingOrgPostNode_PersonnelSearch = null;
            GetLoadonDemandError_PersonnelSearchPage();
        }
        else
            ObjexpandingOrgPostNode_PersonnelSearch = null;
    }
}

function GetLoadonDemandError_PersonnelSearchPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvOrganizationPost_PersonnelSearch_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingOrgPostNode_PersonnelSearch != null)
        ObjexpandingOrgPostNode_PersonnelSearch = null;
    ObjexpandingOrgPostNode_PersonnelSearch = new Object();
    ObjexpandingOrgPostNode_PersonnelSearch.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingOrgPostNode_PersonnelSearch.HasChild = true;
        trvOrganizationPost_PersonnelSearch.beginUpdate();
        ObjexpandingOrgPostNode_PersonnelSearch.Node.get_nodes().remove(0);
        trvOrganizationPost_PersonnelSearch.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingOrgPostNode_PersonnelSearch.HasChild = false;
        else
            ObjexpandingOrgPostNode_PersonnelSearch.HasChild = true;
    }
}

function cmbOrganizationPost_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbOrganizationPost_PersonnelSearch);
    if (trvOrganizationPost_PersonnelSearch.get_nodes().get_length() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbOrganizationPost_PersonnelSearch = true;
        ObjexpandingOrgPostNode_PersonnelSearch = null;
        Fill_cmbOrganizationPost_PersonnelSearch();
    }
}

function Fill_cmbOrganizationPost_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbOrganizationPost_PersonnelSearch');
    CallBack_cmbOrganizationPost_PersonnelSearch.callback();
}

function CallBack_cmbOrganizationPost_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPost_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbOrganizationPost_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbOrganizationPost_PersonnelSearch_DropImage').mousedown();
        cmbOrganizationPost_PersonnelSearch.expand();
        ChangeDirection_trvOrganizationPost_PersonnelSearch();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbOrganizationPost_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbOrganizationPost_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_OrganizationPost_PersonnelSearch_onClick() {
    document.getElementById('cmbOrganizationPost_PersonnelSearch_Input').value = '';
    cmbOrganizationPost_PersonnelSearch.unSelect();
    trvOrganizationPost_PersonnelSearch.SelectedNode = undefined;
}

function Refresh_cmbOrganizationPost_PersonnelSearch() {
    Fill_cmbOrganizationPost_PersonnelSearch();
}

function cmbControlStation_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbControlStation_PersonnelSearch);
    if (cmbControlStation_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbControlStation_PersonnelSearch = true;
        Fill_cmbControlStation_PersonnelSearch();
    }
}

function Fill_cmbControlStation_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbControlStation_PersonnelSearch');
    CallBack_cmbControlStation_PersonnelSearch.callback();
}

function CallBack_cmbControlStation_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbControlStation_PersonnelSearch.dispose();
}

function CallBack_cmbControlStation_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ControlStation_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbControlStation_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbControlStation_PersonnelSearch_DropImage').mousedown();
        cmbControlStation_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbControlStation_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbControlStation_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_ControlStation_PersonnelSearch_onClick() {
    document.getElementById('cmbControlStation_PersonnelSearch_Input').value = '';
    cmbControlStation_PersonnelSearch.unSelect();
}

function Refresh_cmbControlStation_PersonnelSearch() {
    Fill_cmbControlStation_PersonnelSearch();
}

function tlbItemSetDate_TlbSetDate_BirthDate_PersonnelSearch() {
    ShowDialogDualCalendars('BirthDate');
}

function cmbWorkGroups_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbWorkGroups_PersonnelSearch);
    if (cmbWorkGroups_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroups_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbWorkGroups_PersonnelSearch = true;
        Fill_cmbWorkGroups_PersonnelSearch();
    }
}

function Fill_cmbWorkGroups_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbWorkGroups_PersonnelSearch');
    CallBack_cmbWorkGroups_PersonnelSearch.callback();
}

function CallBack_cmbWorkGroups_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbWorkGroups_PersonnelSearch.dispose();
}

function CallBack_cmbWorkGroups_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_WorkGroups_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbWorkGroups_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbWorkGroups_PersonnelSearch_DropImage').mousedown();
        cmbWorkGroups_PersonnelSearch.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbWorkGroups_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbWorkGroups_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function Refresh_cmbWorkGroups_PersonnelSearch() {
    Fill_cmbWorkGroups_PersonnelSearch();
}

function tlbItemClear_TlbClear_WorkGroups_PersonnelSearch_onClick() {
    document.getElementById('cmbWorkGroups_PersonnelSearch_Input').value = '';
    cmbWorkGroups_PersonnelSearch.unSelect();
}

function tlbItemSetDate_TlbSetDate_WorkGroups_PersonnelSearch() {
    ShowDialogSingleCalendar('WorkGroup');
}

function cmbRuleGroups_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbRuleGroups_PersonnelSearch);
    if (cmbRuleGroups_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroups_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbRuleGroups_PersonnelSearch = true;
        Fill_cmbRuleGroups_PersonnelSearch();
    }
}

function Fill_cmbRuleGroups_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbRuleGroups_PersonnelSearch');
    CallBack_cmbRuleGroups_PersonnelSearch.callback();
}

function CallBack_cmbRuleGroups_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbRuleGroups_PersonnelSearch.dispose();
}

function CallBack_cmbRuleGroups_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_RuleGroups_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbRuleGroups_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbRuleGroups_PersonnelSearch_DropImage').mousedown();
        cmbRuleGroups_PersonnelSearch.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbRuleGroups_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbRuleGroups_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function Refresh_cmbRuleGroups_PersonnelSearch() {
    Fill_cmbRuleGroups_PersonnelSearch();
}

function tlbItemClear_TlbClear_RuleGroups_PersonnelSearch_onClick() {
    document.getElementById('cmbRuleGroups_PersonnelSearch_Input').value = '';
    cmbRuleGroups_PersonnelSearch.unSelect();
}

function tlbItemSetDate_TlbSetDate_RuleGroups_PersonnelSearch() {
    ShowDialogDualCalendars('RuleGroup');
}

function cmbCalculationRange_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbCalculationRange_PersonnelSearch);
    if (cmbCalculationRange_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCalculationRange_PersonnelSearch = true;
        Fill_cmbCalculationRange_PersonnelSearch();
    }
}

function Fill_cmbCalculationRange_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbCalculationRange_PersonnelSearch');
    CallBack_cmbCalculationRange_PersonnelSearch.callback();
}

function CallBack_cmbCalculationRange_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbCalculationRange_PersonnelSearch.dispose();
}

function CallBack_cmbCalculationRange_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CalculationRange_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbCalculationRange_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbCalculationRange_PersonnelSearch_DropImage').mousedown();
        cmbCalculationRange_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbCalculationRange_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbCalculationRange_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_CalculationRange_PersonnelSearch_onClick() {
    document.getElementById('cmbCalculationRange_PersonnelSearch_Input').value = '';
    cmbCalculationRange_PersonnelSearch.unSelect();
}

function Refresh_cmbCalculationRange_PersonnelSearch() {
    Fill_cmbCalculationRange_PersonnelSearch();
}

function tlbItemSetDate_TlbSetDate_CalculationRange_PersonnelSearch() {
    ShowDialogSingleCalendar('CalculationRange');
}

function cmbEmployType_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbEmployType_PersonnelSearch);
    if (cmbEmployType_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbEmployType_PersonnelSearch = true;
        Fill_cmbEmployType_PersonnelSearch();
    }
}

function Fill_cmbEmployType_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbEmployType_PersonnelSearch');
    CallBack_cmbEmployType_PersonnelSearch.callback();
}

function CallBack_cmbEmployType_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbEmployType_PersonnelSearch.dispose();
}

function CallBack_cmbEmployType_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_EmployType_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbEmployType_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbEmployType_PersonnelSearch_DropImage').mousedown();

        NodesSelectedList_TrvEmployType_PersonnelSearch = '';
        CountSelectedNode_trvEmployType_PersonnelSearch = 0;

        cmbEmployType_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbEmployType_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbEmployType_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_EmployDate_PersonnelSearch_onClick() {
    document.getElementById('cmbEmployType_PersonnelSearch_Input').value = '';
    cmbEmployType_PersonnelSearch.unSelect();
    trvEmployType_PersonnelSearch.SelectedNode = undefined;
    trvEmployType_PersonnelSearch.unCheckAll();
    NodesSelectedList_TrvEmployType_PersonnelSearch = '';
    CountSelectedNode_trvEmployType_PersonnelSearch = 0;
}

function Refresh_cmbEmployType_PersonnelSearch() {
    Fill_cmbEmployType_PersonnelSearch();
}

function tlbItemSetDate_TlbSetDate_EmployDate_PersonnelSearch() {
    ShowDialogDualCalendars('EmployType');
}

function tlbItemSave_TlbDualCalendars_PersonnelSearch_onClick() {
    FetchRelativeDates_DualCalendars_PersonnelSearch();
    DialogDualCalendars.Close();
}

function FetchRelativeDates_DualCalendars_PersonnelSearch() {
    var caller = DialogDualCalendars.get_value();
    var txtFromDateID = null;
    var txtToDateID = null;
    var targetFromCalID = null;
    var targetToCalID = null;
    switch (caller) {
        case 'BirthDate':
            txtFromDateID = 'txtFromDate_BirthDate_PersonnelSearch';
            txtToDateID = 'txtToDate_BirthDate_PersonnelSearch';
            break;
        case 'RuleGroup':
            txtFromDateID = 'txtFromDate_RuleGroups_PersonnelSearch';
            txtToDateID = 'txtToDate_RuleGroups_PersonnelSearch';
            break;
        case 'EmployType':
            txtFromDateID = 'txtFromDate_EmployDate_PersonnelSearch';
            txtToDateID = 'txtToDate_EmployDate_PersonnelSearch';
            break;
        case 'Contract':
            txtFromDateID = 'txtFromDate_Contract_PersonnelSearch';
            txtToDateID = 'txtToDate_Contract_PersonnelSearch';
            break;
    }
    switch (parent.SysLangID) {
        case 'fa-IR':
            targetFromCalID = 'pdpFromDate_DualCalendars_PersonnelSearch';
            targetToCalID = 'pdpToDate_DualCalendars_PersonnelSearch';
            break;
        case 'en-US':
            targetFromCalID = 'gdpFromDate_DualCalendars_PersonnelSearch_picker';
            targetToCalID = 'gdpToDate_DualCalendars_PersonnelSearch_picker';
            break;
    }
    document.getElementById(txtFromDateID).value = document.getElementById(targetFromCalID).value;
    document.getElementById(txtToDateID).value = document.getElementById(targetToCalID).value;
}

function tlbItemExit_TlbDualCalendars_PersonnelSearch_onClick() {
    DialogDualCalendars.Close();
}

function tlbItemSave_TlbSingleCalendar_PersonnelSearch_onClick() {
    FetchRelativeDate_SingleCalendars_PersonnelSearch();
    DialogSingleCalendar.Close();
}

function FetchRelativeDate_SingleCalendars_PersonnelSearch() {
    var caller = DialogSingleCalendar.get_value();
    var txtDateID = null;
    var targetCalID = null;
    switch (caller) {
        case 'WorkGroup':
            txtDateID = 'txtFromDate_WorkGroups_PersonnelSearch';
            break;
        case 'CalculationRange':
            txtDateID = 'txtFromDate_CalculationDateRange_PersonnelSearch';
            break;
    }
    switch (parent.SysLangID) {
        case 'fa-IR':
            targetCalID = 'pdpFromDate_SingleCalendar_PersonnelSearch';
            break;
        case 'en-US':
            targetCalID = 'gdpFromDate_SingleCalendar_PersonnelSearch_picker';
            break;
    }
    document.getElementById(txtDateID).value = document.getElementById(targetCalID).value;
}

function tlbItemExit_TlbSingleCalendar_PersonnelSearch_onClick() {
    DialogSingleCalendar.Close();
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function ShowConnectionError_PersonnelSearch() {
    var error = document.getElementById('hfErrorType_PersonnelSearch').value;
    var errorBody = document.getElementById('hfConnectionError_PersonnelSearch').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemOk_TlbOkConfirm_onClick() {
    CloseDialogPersonnelSearch();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_PersonnelSearch').value;
    DialogConfirm.Show();
}
function CloseDialogPersonnelSearch() {
    PersonnelSearch_onSave();
    parent.document.getElementById(parent.ClientPerfixId + 'DialogPersonnelSearch_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogPersonnelSearch').Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function ShowDialogDualCalendars(caller) {
    var ObjDatesOfCaller = GetDatesOfCaller_PersonnelSearch(caller);
    SetDate_FromDateCalendars_DualCalendars_PersonnelSearch(ObjDatesOfCaller.FromDate);
    SetDate_ToDateCalendars_DualCalendars_PersonnelSearch(ObjDatesOfCaller.ToDate);
    DialogDualCalendars.set_value(caller);
    DialogDualCalendars.Show();
}

function GetDateOfCaller_PersonnelSearch(caller) {
    var date = '';
    switch (caller) {
        case 'WorkGroup':
            date = document.getElementById('txtFromDate_WorkGroups_PersonnelSearch').value;
            break;
        case 'CalculationRange':
            date = document.getElementById('txtFromDate_CalculationDateRange_PersonnelSearch').value;
            break;
    }
    return date;
}

function GetDatesOfCaller_PersonnelSearch(caller) {
    var ObjDateOfCaller = new Object();
    ObjDateOfCaller.FromDate = '';
    ObjDateOfCaller.ToDate = '';
    switch (caller) {
        case 'BirthDate':
            ObjDateOfCaller.FromDate = document.getElementById('txtFromDate_BirthDate_PersonnelSearch').value;
            ObjDateOfCaller.ToDate = document.getElementById('txtToDate_BirthDate_PersonnelSearch').value;
            break;
        case 'RuleGroup':
            ObjDateOfCaller.FromDate = document.getElementById('txtFromDate_RuleGroups_PersonnelSearch').value;
            ObjDateOfCaller.ToDate = document.getElementById('txtToDate_RuleGroups_PersonnelSearch').value;
            break;
        case 'EmployType':
            ObjDateOfCaller.FromDate = document.getElementById('txtFromDate_EmployDate_PersonnelSearch').value;
            ObjDateOfCaller.ToDate = document.getElementById('txtToDate_EmployDate_PersonnelSearch').value;
            break;
        case 'Contract':
            ObjDateOfCaller.FromDate = document.getElementById('txtFromDate_Contract_PersonnelSearch').value;
            ObjDateOfCaller.ToDate = document.getElementById('txtToDate_Contract_PersonnelSearch').value;
            break;
    }
    return ObjDateOfCaller;
}

function ShowDialogSingleCalendar(caller) {
    SetDate_FromDateCalendars_SingleCalendar_PersonnelSearch(GetDateOfCaller_PersonnelSearch(caller));
    DialogSingleCalendar.set_value(caller);
    DialogSingleCalendar.Show();
}

function tlbItemClear_TlbClear_FromDateCalendars_SingleCalendar_PersonnelSearch_onClick() {
    SetDate_FromDateCalendars_SingleCalendar_PersonnelSearch('');
}

function SetDate_FromDateCalendars_SingleCalendar_PersonnelSearch(date) {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_SingleCalendar_PersonnelSearch').value = date;
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_SingleCalendar_PersonnelSearch_picker').value = date;
            break;
    }
}

function tlbItemClear_TlbClear_FromDateCalendars_DualCalendars_PersonnelSearch_onClick() {
    SetDate_FromDateCalendars_DualCalendars_PersonnelSearch('');
}

function SetDate_FromDateCalendars_DualCalendars_PersonnelSearch(date) {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_DualCalendars_PersonnelSearch').value = date;
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_DualCalendars_PersonnelSearch_picker').value = date;
            break;
    }
}

function tlbItemClear_TlbClear_ToDateCalendars_DualCalendars_PersonnelSearch_onClick() {
    SetDate_ToDateCalendars_DualCalendars_PersonnelSearch('');
}

function SetDate_ToDateCalendars_DualCalendars_PersonnelSearch(date) {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpToDate_DualCalendars_PersonnelSearch').value = date;
            break;
        case 'en-US':
            document.getElementById('gdpToDate_DualCalendars_PersonnelSearch_picker').value = date;
            break;
    }
}

function ResetCalendars_PersonnelSearch() {
    switch (parent.SysLangID) {
        case 'fa-IR':
            document.getElementById('pdpFromDate_SingleCalendar_PersonnelSearch').value = '';
            document.getElementById('pdpFromDate_DualCalendars_PersonnelSearch').value = '';
            document.getElementById('pdpToDate_DualCalendars_PersonnelSearch').value = '';
            break;
        case 'en-US':
            document.getElementById('gdpFromDate_SingleCalendar_PersonnelSearch_picker').value = '';
            document.getElementById('gdpFromDate_DualCalendars_PersonnelSearch_picker').value = '';
            document.getElementById('gdpToDate_DualCalendars_PersonnelSearch_picker').value = '';
            break;
    }
}

function CollapseControls_PersonnelSearch(exception) {
    if (exception == null || exception != cmbSex_PersonnelSearch)
        cmbSex_PersonnelSearch.collapse();
    if (exception == null || exception != cmbMarriageState_PersonnelSearch)
        cmbMarriageState_PersonnelSearch.collapse();
    if (exception == null || exception != cmbMilitaryState_PersonnelSearch)
        cmbMilitaryState_PersonnelSearch.collapse();
    if (exception == null || exception != cmbDepartment_PersonnelSearch)
        cmbDepartment_PersonnelSearch.collapse();
    if (exception == null || exception != cmbOrganizationPost_PersonnelSearch)
        cmbOrganizationPost_PersonnelSearch.collapse();
    if (exception == null || exception != cmbControlStation_PersonnelSearch)
        cmbControlStation_PersonnelSearch.collapse();
    if (exception == null || exception != cmbWorkGroups_PersonnelSearch)
        cmbWorkGroups_PersonnelSearch.collapse();
    if (exception == null || exception != cmbRuleGroups_PersonnelSearch)
        cmbRuleGroups_PersonnelSearch.collapse();
    if (exception == null || exception != cmbCalculationRange_PersonnelSearch)
        cmbCalculationRange_PersonnelSearch.collapse();
    if (exception == null || exception != cmbEmployType_PersonnelSearch)
        cmbEmployType_PersonnelSearch.collapse();
    if (exception == null || exception != cmbUIValidationRulesGroup_PersonnelSearch)
        cmbUIValidationRulesGroup_PersonnelSearch.collapse();
    if (document.getElementById('datepickeriframe') != null && document.getElementById('datepickeriframe').style.visibility == 'visible')
        displayDatePicker('pdpFromDate_DualCalendars_PersonnelSearch');
}

function trvDepartment_PersonnelSearch_onNodeExpand(sender, e) {
    ChangeDirection_trvDepartment_PersonnelSearch();
}

function ChangeDirection_trvDepartment_PersonnelSearch() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvDepartment_PersonnelSearch').style.direction = 'ltr';
    }
}

function trvOrganizationPost_PersonnelSearch_onNodeExpand(sender, e) {
    ChangeDirection_trvOrganizationPost_PersonnelSearch();
}

function ChangeDirection_trvOrganizationPost_PersonnelSearch() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvOrganizationPost_PersonnelSearch').style.direction = 'ltr';
    }
}

function CallBack_cmbDepartmentSearchResult_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbDepartmentSearchResult_PersonnelSearch.dispose();
}

function CallBack_cmbDepartmentSearchResult_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DepartmentSearchResult_PersonnelSearch').value;
    if (error == "") {
        trvDepartment_PersonnelSearch.SelectedNode = undefined;
        trvDepartment_PersonnelSearch.unCheckAll();
        NodesSelectedList_TrvDepartment_PersonnelSearch = '';
        CountSelectedNode_trvDepartment_PersonnelSearch = 0;
        document.getElementById('ParentDepartment_PersonnelSearch').innerHTML = '';
        cmbDepartmentSearchResult_PersonnelSearch.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDepartmentSearchResult_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDepartmentSearchResult_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemDepartmentSearch_TlbDepartmentSearch_PersonnelSearch_onClick() {
    Fill_cmbDepartmentSearchResult_PersonnelSearch();
}

function Fill_cmbDepartmentSearchResult_PersonnelSearch() {
    var SearchTerm = document.getElementById('txtSearchTermDepartment_PersonnelSearch').value;
    ComboBox_onBeforeLoadData('cmbDepartmentSearchResult_PersonnelSearch');
    CallBack_cmbDepartmentSearchResult_PersonnelSearch.callback(CharToKeyCode_PersonnelSearch(SearchTerm));
}

function tlbItemSave_TlbDepartmentSearch_TlbPersonnelSearch_onClick() {
    var selectedItem_cmbDepartmentSearchResult_PersonnelSearch = cmbDepartmentSearchResult_PersonnelSearch.getSelectedItem();
    if (selectedItem_cmbDepartmentSearchResult_PersonnelSearch != undefined && selectedItem_cmbDepartmentSearchResult_PersonnelSearch != null) {
        DepartmentSelectedType_PersonnelSearch = 'Search';
        document.getElementById('cmbDepartment_PersonnelSearch_Input').value = selectedItem_cmbDepartmentSearchResult_PersonnelSearch.get_text();
    }
    DialogDepartmentSearch.Close();
}

function Search_cmbDepartment_PersonnelSearch() {
    ShowDialogDepartmentSearch();
}

function ShowDialogDepartmentSearch() {
    DialogDepartmentSearch.Show();
}

function tlbItemExit_TlbDepartmentSearch_PersonnelSearch_onClick() {
    DialogDepartmentSearch.Close();
}

function CharToKeyCode_PersonnelSearch(str) {
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



function cmbGrade_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbGrade_PersonnelSearch);
    if (cmbGrade_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGrade_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbGrade_PersonnelSearch = true;
        Fill_cmbGrade_PersonnelSearch();
    }
}

function Fill_cmbGrade_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbGrade_PersonnelSearch');
    CallBack_cmbGrade_PersonnelSearch.callback();
}

function CallBack_cmbGrade_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbGrade_PersonnelSearch.dispose();
}

function CallBack_cmbGrade_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Grade_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbGrade_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbGrade_PersonnelSearch_DropImage').mousedown();
        cmbGrade_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbGrade_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbGrade_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_Grade_PersonnelSearch_onClick() {
    document.getElementById('cmbGrade_PersonnelSearch_Input').value = '';
    cmbGrade_PersonnelSearch.unSelect();
}

//----------------------------------------
function cmbCostCenter_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbCostCenter_PersonnelSearch);
    if (cmbCostCenter_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCostCenter_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbCostCenter_PersonnelSearch = true;
        Fill_cmbCostCenter_PersonnelSearch();
    }
}

function Fill_cmbCostCenter_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbCostCenter_PersonnelSearch');
    CallBack_cmbCostCenter_PersonnelSearch.callback();
}

function CallBack_cmbCostCenter_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbCostCenter_PersonnelSearch.dispose();
}

function CallBack_cmbCostCenter_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_CostCenter_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbCostCenter_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbCostCenter_PersonnelSearch_DropImage').mousedown();
        cmbCostCenter_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbCostCenter_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbCostCenter_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function tlbItemClear_TlbClear_CostCenter_PersonnelSearch_onClick() {
    document.getElementById('cmbCostCenter_PersonnelSearch_Input').value = '';
    cmbCostCenter_PersonnelSearch.unSelect();
}
//-----------------------------------------------

function Search_cmbOrganizationPost_PersonnelSearch() {
    ShowDialogOrganizationPostSearch();
}
function ShowDialogOrganizationPostSearch() {

    DialogOrganizationPostSearch.Show();
}
function tlbItemSave_TlbOrganizationPostSearch_TlbPersonnelSearch_onClick() {
    var selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch = cmbOrganizationPostSearchResult_PersonnelSearch.getSelectedItem();
    if (selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch != undefined && selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch != null) {

        OrganizationPostSelectedType_PersonnelSearch = 'Search';
        document.getElementById('cmbOrganizationPost_PersonnelSearch_Input').value = selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch.get_text();


    }
    DialogOrganizationPostSearch.Close();
}

function tlbItemOrganizationPostSearch_TlbOrganizationPostSearch_PersonnelSearch_onClick() {
    Fill_cmbOrganizationPostSearchResult_PersonnelSearch();
}

function Fill_cmbOrganizationPostSearchResult_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbOrganizationPostSearchResult_PersonnelSearch');
    var SearchTerm = document.getElementById('txtSearchTermOrganizationPost_PersonnelSearch').value;
    CallBack_cmbOrganizationPostSearchResult_PersonnelSearch.callback(CharToKeyCode_PersonnelSearch(SearchTerm));
}


function tlbItemExit_TlbOrganizationPostSearch_PersonnelSearch_onClick() {
    DialogOrganizationPostSearch.Close();
}

function CallBack_cmbOrganizationPostSearchResult_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbOrganizationPostSearchResult_PersonnelSearch.dispose();
}

function CallBack_cmbOrganizationPostSearchResult_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_OrganizationPostSearchResult_PersonnelSearch').value;
    if (error == ""){
        cmbOrganizationPostSearchResult_PersonnelSearch.expand();
        document.getElementById('ParentOrganizationPosts_PersonnelSearch').innerHTML = '';
    }        
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbOrganizationPostSearchResult_PersonnelSearch_DropDown').style.display = 'none';
    }
}
function CallBack_cmbOrganizationPostSearchResult_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function txtSearchTermDepartment_PersonnelSearch_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemDepartmentSearch_TlbDepartmentSearch_PersonnelSearch_onClick();
    }
}

function txtSearchTermOrganizationPost_PersonnelSearch_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemOrganizationPostSearch_TlbOrganizationPostSearch_PersonnelSearch_onClick();
    }
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

function trvEmployType_PersonnelSearch_onNodeCheckChange(sender, e) {
    var checkedNodeStatus_trvEmployType_PersonnelSearch = e.get_node().Checked;
    if (checkedNodeStatus_trvEmployType_PersonnelSearch) {
        NodesSelectedList_TrvEmployType_PersonnelSearch += '#' + e.get_node().get_id() + '#,';
        CountSelectedNode_trvEmployType_PersonnelSearch += 1;
    }
    else {
        NodesSelectedList_TrvEmployType_PersonnelSearch = NodesSelectedList_TrvEmployType_PersonnelSearch.replace('#' + e.get_node().get_id() + '#', '');
        CountSelectedNode_trvEmployType_PersonnelSearch -= 1;
    }
    if (CountSelectedNode_trvEmployType_PersonnelSearch == 0) {
        cmbEmployType_PersonnelSearch.set_text('');
    }
    else {
        cmbEmployType_PersonnelSearch.set_text(CountSelectedNode_trvEmployType_PersonnelSearch.toString() + ' ' + document.getElementById('hfCountSelectedNodeHiddenField_TrvEmployType_PersonnelSearch').value);
    }

}
function trvEmployType_PersonnelSearch_onNodeExpand(sender, e) {
    ChangeDirection_trvEmployType_PersonnelSearch();
}
function ChangeDirection_trvEmployType_PersonnelSearch() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvEmployType_PersonnelSearch').style.direction = 'ltr';
    }
}


function trvControlStation_PersonnelSearch_onNodeCheckChange(sender, e) {
    var checkedNodeStatus_trvControlStation_PersonnelSearch = e.get_node().Checked;
    if (checkedNodeStatus_trvControlStation_PersonnelSearch) {
        NodesSelectedList_TrvControlStation_PersonnelSearch += '#' + e.get_node().get_id() + '#,';
        CountSelectedNode_trvControlStation_PersonnelSearch += 1;
    }
    else {
        NodesSelectedList_TrvControlStation_PersonnelSearch = NodesSelectedList_TrvControlStation_PersonnelSearch.replace('#' + e.get_node().get_id() + '#', '');
        CountSelectedNode_trvControlStation_PersonnelSearch -= 1;
    }
    if (CountSelectedNode_trvControlStation_PersonnelSearch == 0) {
        cmbControlStation_PersonnelSearch.set_text('');
    }
    else {
        cmbControlStation_PersonnelSearch.set_text(CountSelectedNode_trvControlStation_PersonnelSearch.toString() + ' ' + document.getElementById('hfCountSelectedNodeHiddenField_TrvControlStation_PersonnelSearch').value);
    }

}
function trvControlStation_PersonnelSearch_onNodeExpand(sender, e) {
    ChangeDirection_trvControlStation_PersonnelSearch();
}
function ChangeDirection_trvControlStation_PersonnelSearch() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvControlStation_PersonnelSearch').style.direction = 'ltr';
    }
}
function cmbUIValidationRulesGroup_PersonnelSearch_onExpand() {
    CollapseControls_PersonnelSearch(cmbUIValidationRulesGroup_PersonnelSearch);
    if (cmbUIValidationRulesGroup_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbUIValidationRulesGroup_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbUIValidationRulesGroup_PersonnelSearch = true;
        Fill_cmbUIValidationRulesGroup_PersonnelSearch();
    }
}
function CallBack_cmbUIValidationRulesGroup_PersonnelSearch_onBeforeCallback() {
    cmbUIValidationRulesGroup_PersonnelSearch.dispose();
}
function CallBack_cmbUIValidationRulesGroup_PersonnelSearch_onCallbackComplete() {
    var error = document.getElementById('ErrorHiddenField_UIValidationRulesGroup_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbUIValidationRulesGroup_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbUIValidationRulesGroup_PersonnelSearch_DropImage').mousedown();
        NodesSelectedList_trvUIValidationRulesGroup_PersonnelSearch = '';
        CountSelectedNode_trvUIValidationRulesGroup_PersonnelSearch = 0;
        cmbUIValidationRulesGroup_PersonnelSearch.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbUIValidationRulesGroup_PersonnelSearch_DropDown').style.display = 'none';
    }
}
function CallBack_cmbUIValidationRulesGroup_PersonnelSearch_onCallbackError() {
    ShowConnectionError_PersonnelSearch();
}
function tlbItemClear_TlbClear_UiValidationRulesGroup_PersonnelSearch_onClick() {
    document.getElementById('cmbUIValidationRulesGroup_PersonnelSearch_Input').value = '';
    cmbUIValidationRulesGroup_PersonnelSearch.unSelect();
    trvUIValidationRulesGroup_PersonnelSearch.SelectedNode = undefined;
    trvUIValidationRulesGroup_PersonnelSearch.unCheckAll();
    NodesSelectedList_TrvUIValidationRulesGroup_PersonnelSearch = '';
    CountSelectedNode_trvUIValidationRulesGroup_PersonnelSearch = 0;
}
function Refresh_cmbUiValidationRulesGroup_PersonnelSearch() {
    Fill_cmbUIValidationRulesGroup_PersonnelSearch();
}
function Fill_cmbUIValidationRulesGroup_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbUIValidationRulesGroup_PersonnelSearch');
    CallBack_cmbUIValidationRulesGroup_PersonnelSearch.callback();
}

function ChangeVisibilityState_PersonnelSearch() {
    var deactivePersonnelVisibility = null;
    var deletedPersonnelVisibility = null;
    var caller = parent.DialogPersonnelSearch.get_value().Caller;
    switch (caller) {
        case 'MasterPersonnel':
            deletedPersonnelVisibility = 'visible';
            deactivePersonnelVisibility = 'visible';
            break;
        case 'Manager_Substitute':
        case 'Personnel_Substitute':
        case 'UnderManagementPersonnel':
        case 'ManagersWorkFlow':
        case 'MasterTrafficsControl':
        case 'RequestRegister':
        case 'Calculations':
        case 'PersonnelOrganizationFeaturesChange':
        case 'Operators':
        case 'Users':
            deletedPersonnelVisibility = 'hidden';
            deactivePersonnelVisibility = 'hidden';
            break;
        default:
            deletedPersonnelVisibility = 'hidden';
            deactivePersonnelVisibility = 'visible';
            break;
    }
    document.getElementById('container_rdbIsDeleted_PersonnelSearch').style.visibility = deletedPersonnelVisibility;
    document.getElementById('container_lblIsDeleted_PersonnelSearch').style.visibility = deletedPersonnelVisibility;
    document.getElementById('container_rdbDeactive_PersonnelSearch').style.visibility = deactivePersonnelVisibility;
    document.getElementById('container_lblDeactive_PersonnelSearch').style.visibility = deactivePersonnelVisibility;
}


function trvUIValidationRulesGroup_PersonnelSearch_onNodeCheckChange(sender, e) {
    var checkedNodeStatus_trvUIValidationRulesGroup_PersonnelSearch = e.get_node().Checked;
    if (checkedNodeStatus_trvUIValidationRulesGroup_PersonnelSearch) {
        NodesSelectedList_trvUIValidationRulesGroup_PersonnelSearch += '#' + e.get_node().get_id() + '#,';
        CountSelectedNode_trvUIValidationRulesGroup_PersonnelSearch += 1;
    }
    else {
        NodesSelectedList_trvUIValidationRulesGroup_PersonnelSearch = NodesSelectedList_trvUIValidationRulesGroup_PersonnelSearch.replace('#' + e.get_node().get_id() + '#', '');
        CountSelectedNode_trvUIValidationRulesGroup_PersonnelSearch -= 1;
    }
    if (CountSelectedNode_trvUIValidationRulesGroup_PersonnelSearch == 0) {
        cmbUIValidationRulesGroup_PersonnelSearch.set_text('');

    } else {
        cmbUIValidationRulesGroup_PersonnelSearch.set_text(CountSelectedNode_trvUIValidationRulesGroup_PersonnelSearch.toString() + ' ' + document.getElementById('hfCountSelectedNodeHiddenField_trvUIValidationRulesGroup_PersonnelSearch').value);
    }

}

function trvUIValidationRulesGroup_PersonnelSearch_onNodeExpand(sender, e) {
    ChangeDirection_trvUIValidationRulesGroup_PersonnelSearch();
}
function ChangeDirection_trvUIValidationRulesGroup_PersonnelSearch() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvUIValidationRulesGroup_PersonnelSearch').style.direction = 'ltr';
}




function cmbContract_PersonnelSearch_onExpand(sender, e) {
    CollapseControls_PersonnelSearch(cmbContract_PersonnelSearch);
    if (cmbContract_PersonnelSearch.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbContract_PersonnelSearch == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbContract_PersonnelSearch = true;
        Fill_cmbContract_PersonnelSearch();
    }
}

function Fill_cmbContract_PersonnelSearch() {
    ComboBox_onBeforeLoadData('cmbContract_PersonnelSearch');
    CallBack_cmbContract_PersonnelSearch.callback();
}

function CallBack_cmbContract_PersonnelSearch_onBeforeCallback(sender, e) {
    cmbContract_PersonnelSearch.dispose();
}

function CallBack_cmbContract_PersonnelSearch_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Contract_PersonnelSearch').value;
    if (error == "") {
        document.getElementById('cmbContract_PersonnelSearch_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbContract_PersonnelSearch_DropImage').mousedown();
        cmbContract_PersonnelSearch.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbContract_PersonnelSearch_DropDown').style.display = 'none';
    }
}

function CallBack_cmbContract_PersonnelSearch_onCallbackError(sender, e) {
    ShowConnectionError_PersonnelSearch();
}

function Refresh_cmbContract_PersonnelSearch() {
    Fill_cmbContract_PersonnelSearch();
}

function Refresh_cmbCostCenter_PersonnelSearch() {
    Fill_cmbCostCenter_PersonnelSearch();
}

function tlbItemClear_TlbClear_Contract_PersonnelSearch_onClick() {
    document.getElementById('cmbContract_PersonnelSearch_Input').value = '';
    cmbContract_PersonnelSearch.unSelect();
}

function tlbItemSetDate_TlbSetDate_Contract_PersonnelSearch() {
    ShowDialogDualCalendars('Contract');
}
function tlbItemParentDepartment_TlbParentDepartments_PersonnelSearch_onClick() {
    var selectedItem_cmbDepartmentSearchResult_PersonnelSearch = cmbDepartmentSearchResult_PersonnelSearch.getSelectedItem();
    if (selectedItem_cmbDepartmentSearchResult_PersonnelSearch != undefined && selectedItem_cmbDepartmentSearchResult_PersonnelSearch != null) {
        var departmentObj = selectedItem_cmbDepartmentSearchResult_PersonnelSearch.get_value();
        departmentObj = eval('(' + departmentObj + ')');
        DepartmentID = departmentObj.ID;
        ViewPrentDepartments_PersonnelSearchPage(CharToKeyCode_PersonnelSearch(DepartmentID));
    }
   
}
function ViewPrentDepartments_PersonnelSearchPage_onCallBack(Response){   
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {      
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelSearch').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelSearch').value;
        }
        else {
            document.getElementById('ParentDepartment_PersonnelSearch').innerHTML = eval('(' + RetMessage[2] + ')');
        }               
    }
}
function cmbDepartmentSearchResult_PersonnelSearch_OnChange() {
    document.getElementById('ParentDepartment_PersonnelSearch').innerHTML = '';
}
function tlbItemParentOrganizationPosts_TlbParentOrganizationPosts_PersonnelSearch_onClick() {
    var selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch = cmbOrganizationPostSearchResult_PersonnelSearch.getSelectedItem();
    if (selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch != undefined && selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch != null) {
        var OrganizationPostObj = selectedItem_cmbOrganizationPostSearchResult_PersonnelSearch.get_value();
        OrganizationPostObj = eval('(' + OrganizationPostObj + ')');
        OrganizationPostID = OrganizationPostObj.ID;
        ViewPrentOrganizationPosts_PersonnelSearchPage(CharToKeyCode_PersonnelSearch(OrganizationPostID));
    }
}
function ViewPrentOrganizationPosts_PersonnelSearchPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_PersonnelSearch').value;
            Response[1] = document.getElementById('hfConnectionError_PersonnelSearch').value;
        }
        else {
            document.getElementById('ParentOrganizationPosts_PersonnelSearch').innerHTML = eval('(' + RetMessage[2] + ')');
        }
    }
}
function cmbOrganizationPosSearchResult_PersonnelSearch_OnChange() {
    document.getElementById('ParentOrganizationPosts_PersonnelSearch').innerHTML = '';
}