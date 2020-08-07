﻿
var CurrentPageState_Personnel = 'View';
var ConfirmState_Personnel = null;
var ObjPersonnel_Personnel = null;
var CurrentPageIndex_GridPersonnel_Personnel = 0;
var OriginalText_lblPersonnelCount_Personnel = null;
var LoadState_Personnel = 'Normal';
var AdvancedSearchTerm_Personnel = '';

function ShowDialogPersonnelMainInformation() {
    var CurrentStateObj_Personnel = new Object();
    switch (CurrentPageState_Personnel) {
        case 'Add':
            CurrentStateObj_Personnel.PageState = CurrentPageState_Personnel;
            CurrentStateObj_Personnel.PageSize = document.getElementById('hfPersonnelPageSize_Personnel').value;
            CurrentStateObj_Personnel.PersonnelID = '0';
            CurrentStateObj_Personnel.StrPersonnelExtraInformation = '';
            parent.DialogPersonnelMainInformation.set_value(CurrentStateObj_Personnel);
            parent.DialogPersonnelMainInformation.Show();
            break;
        case 'Edit':
            var selectedPersonnel_Personnel = GridPersonnel_Personnel.getSelectedItems()[0];
            if (selectedPersonnel_Personnel != undefined && selectedPersonnel_Personnel.getMember('IsDeleted').get_text() == 'false') {
                CurrentStateObj_Personnel.PageState = CurrentPageState_Personnel;
                CurrentStateObj_Personnel.PageSize = document.getElementById('hfPersonnelPageSize_Personnel').value;
                CurrentStateObj_Personnel.PersonnelID = selectedPersonnel_Personnel.getMember('ID').get_text();
                CurrentStateObj_Personnel.DigitalSignature = selectedPersonnel_Personnel.getMember('DigitalSignature').get_text();
                CurrentStateObj_Personnel.IsActive = selectedPersonnel_Personnel.getMember('Active').get_text();
                CurrentStateObj_Personnel.HasPeyment = selectedPersonnel_Personnel.getMember('PersonTASpec.HasPeyment').get_text();
                CurrentStateObj_Personnel.NightWork = selectedPersonnel_Personnel.getMember('PersonTASpec.NightWork').get_text();
                CurrentStateObj_Personnel.HolidayWork = selectedPersonnel_Personnel.getMember('PersonTASpec.HolidayWork').get_text();
                CurrentStateObj_Personnel.OverTimeWork = selectedPersonnel_Personnel.getMember('PersonTASpec.OverTimeWork').get_text();
                CurrentStateObj_Personnel.FirstName = selectedPersonnel_Personnel.getMember('FirstName').get_text();
                CurrentStateObj_Personnel.LastName = selectedPersonnel_Personnel.getMember('LastName').get_text();
                CurrentStateObj_Personnel.SexID = selectedPersonnel_Personnel.getMember('Sex').get_text();
                CurrentStateObj_Personnel.SexTitle = selectedPersonnel_Personnel.getMember('SexTitle').get_text();
                CurrentStateObj_Personnel.FatherName = selectedPersonnel_Personnel.getMember('PersonDetail.FatherName').get_text();
                CurrentStateObj_Personnel.NationalCode = selectedPersonnel_Personnel.getMember('PersonDetail.MeliCode').get_text();
                CurrentStateObj_Personnel.MilitaryState = selectedPersonnel_Personnel.getMember('PersonDetail.MilitaryStatus').get_text();
                CurrentStateObj_Personnel.MilitaryStateTitle = selectedPersonnel_Personnel.getMember('PersonDetail.MilitaryStatusTitle').get_text();
                CurrentStateObj_Personnel.IdentityCertificate = selectedPersonnel_Personnel.getMember('PersonDetail.BirthCertificate').get_text();
                CurrentStateObj_Personnel.IssuanceLocation = selectedPersonnel_Personnel.getMember('PersonDetail.PlaceIssued').get_text();
                CurrentStateObj_Personnel.Education = selectedPersonnel_Personnel.getMember('Education').get_text();
                CurrentStateObj_Personnel.MarriageStateID = selectedPersonnel_Personnel.getMember('MaritalStatus').get_text();
                CurrentStateObj_Personnel.MarriageStateTitle = selectedPersonnel_Personnel.getMember('MaritalStatusTitle').get_text();
                CurrentStateObj_Personnel.Tel = selectedPersonnel_Personnel.getMember('PersonDetail.Tel').get_text();
                CurrentStateObj_Personnel.MobileNumber = selectedPersonnel_Personnel.getMember('PersonDetail.MobileNumber').get_text();
                CurrentStateObj_Personnel.Address = selectedPersonnel_Personnel.getMember('PersonDetail.Address').get_text();
                CurrentStateObj_Personnel.EmailAddress = selectedPersonnel_Personnel.getMember('PersonDetail.EmailAddress').get_text();
                CurrentStateObj_Personnel.BirthLocation = selectedPersonnel_Personnel.getMember('PersonDetail.BirthPlace').get_text();
                CurrentStateObj_Personnel.BirthDate = CheckDate_PersonnelMainInformation(selectedPersonnel_Personnel.getMember('PersonDetail.UIBirthDate').get_text());
                CurrentStateObj_Personnel.ChildBirthDate = CheckDate_PersonnelMainInformation(selectedPersonnel_Personnel.getMember('PersonDetail.UIChildBirthDate').get_text());
                CurrentStateObj_Personnel.PersonnelNumber = selectedPersonnel_Personnel.getMember('PersonCode').get_text();
                CurrentStateObj_Personnel.CardNumber = selectedPersonnel_Personnel.getMember('CardNum').get_text();
                CurrentStateObj_Personnel.DepartmentID = selectedPersonnel_Personnel.getMember('Department.ID').get_text();
                CurrentStateObj_Personnel.DepartmentName = selectedPersonnel_Personnel.getMember('Department.Name').get_text();
                CurrentStateObj_Personnel.OrganizationPostID = selectedPersonnel_Personnel.getMember('OrganizationUnit.ID').get_text();
                CurrentStateObj_Personnel.OrganizationPostName = selectedPersonnel_Personnel.getMember('OrganizationUnit.Name').get_text();
                CurrentStateObj_Personnel.OldOrganizationPostID = selectedPersonnel_Personnel.getMember('OrganizationUnit.ID').get_text();
                CurrentStateObj_Personnel.OrganizationPostCustomCode = selectedPersonnel_Personnel.getMember('OrganizationUnit.CustomCode').get_text();
                CurrentStateObj_Personnel.OrganizationPostParentPath = selectedPersonnel_Personnel.getMember('OrganizationUnit.ParentPath').get_text();
                CurrentStateObj_Personnel.ParentOrganizationPostID = selectedPersonnel_Personnel.getMember('OrganizationUnit.ParentID').get_text();
                CurrentStateObj_Personnel.CurrentActiveWorkGroup = selectedPersonnel_Personnel.getMember('CurrentActiveWorkGroup').get_text();
                CurrentStateObj_Personnel.CurrentActiveRuleGroup = selectedPersonnel_Personnel.getMember('CurrentActiveRuleGroup').get_text();
                //CurrentStateObj_Personnel.CurrentActiveEmploymentType = selectedPersonnel_Personnel.getMember('CurrentActiveEmploymentType').get_text();
                CurrentStateObj_Personnel.CurrentActiveCalculationRangeGroup = selectedPersonnel_Personnel.getMember('CurrentActiveDateRangeGroup').get_text();
                CurrentStateObj_Personnel.ControlStationID = selectedPersonnel_Personnel.getMember('PersonTASpec.ControlStation.ID').get_text();
                CurrentStateObj_Personnel.ControlStationName = selectedPersonnel_Personnel.getMember('PersonTASpec.ControlStation.Name').get_text();
                CurrentStateObj_Personnel.EmployNumber = selectedPersonnel_Personnel.getMember('EmploymentNum').get_text();
                CurrentStateObj_Personnel.EmployTypeID = selectedPersonnel_Personnel.getMember('EmploymentType.ID').get_text();
                CurrentStateObj_Personnel.EmployTypeName = selectedPersonnel_Personnel.getMember('EmploymentType.Name').get_text();
                CurrentStateObj_Personnel.EmployDate = CheckDate_PersonnelMainInformation(selectedPersonnel_Personnel.getMember('UIEmploymentDate').get_text());
                CurrentStateObj_Personnel.EmployEndDate = CheckDate_PersonnelMainInformation(selectedPersonnel_Personnel.getMember('UIEndEmploymentDate').get_text());
                CurrentStateObj_Personnel.UserInterfaceRuleGroupID = selectedPersonnel_Personnel.getMember('PersonTASpec.UIValidationGroup.ID').get_text();
                CurrentStateObj_Personnel.UserInterfaceRuleGroupName = selectedPersonnel_Personnel.getMember('PersonTASpec.UIValidationGroup.Name').get_text();
                CurrentStateObj_Personnel.GradeID = selectedPersonnel_Personnel.getMember('Grade.ID').get_text();
                CurrentStateObj_Personnel.GradeName = selectedPersonnel_Personnel.getMember('Grade.Name').get_text();
                CurrentStateObj_Personnel.CostCenterID = selectedPersonnel_Personnel.getMember('CostCenter.ID').get_text();
                CurrentStateObj_Personnel.CostCenterName = selectedPersonnel_Personnel.getMember('CostCenter.Name').get_text();
                CurrentStateObj_Personnel.Image = selectedPersonnel_Personnel.getMember('PersonDetail.Image').get_text();
                CurrentStateObj_Personnel.Reserve1 = selectedPersonnel_Personnel.getMember('PersonTASpec.R1').get_text();
                CurrentStateObj_Personnel.Reserve2 = selectedPersonnel_Personnel.getMember('PersonTASpec.R2').get_text();
                CurrentStateObj_Personnel.Reserve3 = selectedPersonnel_Personnel.getMember('PersonTASpec.R3').get_text();
                CurrentStateObj_Personnel.Reserve4 = selectedPersonnel_Personnel.getMember('PersonTASpec.R4').get_text();
                CurrentStateObj_Personnel.Reserve5 = selectedPersonnel_Personnel.getMember('PersonTASpec.R5').get_text();
                CurrentStateObj_Personnel.Reserve6 = selectedPersonnel_Personnel.getMember('PersonTASpec.R6').get_text();
                CurrentStateObj_Personnel.Reserve7 = selectedPersonnel_Personnel.getMember('PersonTASpec.R7').get_text();
                CurrentStateObj_Personnel.Reserve8 = selectedPersonnel_Personnel.getMember('PersonTASpec.R8').get_text();
                CurrentStateObj_Personnel.Reserve9 = selectedPersonnel_Personnel.getMember('PersonTASpec.R9').get_text();
                CurrentStateObj_Personnel.Reserve10 = selectedPersonnel_Personnel.getMember('PersonTASpec.R10').get_text();
                CurrentStateObj_Personnel.Reserve11 = selectedPersonnel_Personnel.getMember('PersonTASpec.R11').get_text();
                CurrentStateObj_Personnel.Reserve12 = selectedPersonnel_Personnel.getMember('PersonTASpec.R12').get_text();
                CurrentStateObj_Personnel.Reserve13 = selectedPersonnel_Personnel.getMember('PersonTASpec.R13').get_text();
                CurrentStateObj_Personnel.Reserve14 = selectedPersonnel_Personnel.getMember('PersonTASpec.R14').get_text();
                CurrentStateObj_Personnel.Reserve15 = selectedPersonnel_Personnel.getMember('PersonTASpec.R15').get_text();
                CurrentStateObj_Personnel.Reserve16 = selectedPersonnel_Personnel.getMember('PersonTASpec.R16').get_text();
                CurrentStateObj_Personnel.Reserve16Text = selectedPersonnel_Personnel.getMember('PersonTASpec.R16Text').get_text();
                CurrentStateObj_Personnel.Reserve17 = selectedPersonnel_Personnel.getMember('PersonTASpec.R17').get_text();
                CurrentStateObj_Personnel.Reserve17Text = selectedPersonnel_Personnel.getMember('PersonTASpec.R17Text').get_text();
                CurrentStateObj_Personnel.Reserve18 = selectedPersonnel_Personnel.getMember('PersonTASpec.R18').get_text();
                CurrentStateObj_Personnel.Reserve18Text = selectedPersonnel_Personnel.getMember('PersonTASpec.R18Text').get_text();
                CurrentStateObj_Personnel.Reserve19 = selectedPersonnel_Personnel.getMember('PersonTASpec.R19').get_text();
                CurrentStateObj_Personnel.Reserve19Text = selectedPersonnel_Personnel.getMember('PersonTASpec.R19Text').get_text();
                CurrentStateObj_Personnel.Reserve20 = selectedPersonnel_Personnel.getMember('PersonTASpec.R20').get_text();
                CurrentStateObj_Personnel.Reserve20Text = selectedPersonnel_Personnel.getMember('PersonTASpec.R20Text').get_text();
                CurrentStateObj_Personnel.IsLeaveYearDependonContract = selectedPersonnel_Personnel.getMember('PersonTASpec.IsLeaveYearDependonContract').get_text();
                CurrentStateObj_Personnel.LeaveYearMonth = selectedPersonnel_Personnel.getMember('PersonTASpec.LeaveYearMonth').get_text();
                CurrentStateObj_Personnel.LeaveYearDay = selectedPersonnel_Personnel.getMember('PersonTASpec.LeaveYearDay').get_text();
                CurrentStateObj_Personnel.CurrentActiveContract = selectedPersonnel_Personnel.getMember('CurrentActiveContract').get_text();
                CurrentStateObj_Personnel.StrPersonnelExtraInformation = '';

                parent.DialogPersonnelMainInformation.set_value(CurrentStateObj_Personnel);
                parent.DialogPersonnelMainInformation.Show();
            }
    }
}

function CheckDate_PersonnelMainInformation(date) {
    if (document.getElementById('hfBaseDateString_Personnel').value != date)
        return date;
    else
        return "";
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
}

function tlbItemNew_TlbPersonnel_onClick() {
    CurrentPageState_Personnel = 'Add';
    ShowDialogPersonnelMainInformation();
}

function tlbItemEdit_TlbPersonnel_onClick() {
    CurrentPageState_Personnel = 'Edit';
    ShowDialogPersonnelMainInformation();
}

function tlbItemDelete_TlbPersonnel_onClick() {
    var SelectedPersonnel_Personnel = GridPersonnel_Personnel.getSelectedItems();
    if (SelectedPersonnel_Personnel.length > 0 && SelectedPersonnel_Personnel[0].getMember('IsDeleted').get_text() == 'false') {
        CurrentPageState_Personnel = 'Delete';
        ShowDialogConfirm('Delete');
    }    
}

function tlbItemExit_TlbPersonnel_onClick() {
    ShowDialogConfirm('Exit');
}

function tlbItemSearch_TlbPersonnel_onClick() {
    LoadState_Personnel = 'AdvancedSearch';
    ShowDialogPersonnelSearch('MasterPersonnel');
}

function tlbItemSearch_TlbPersonnelQuickSearch_onClick() {
    LoadState_Personnel = 'Search';
    SetPageIndex_GridPersonnel_Personnel(0);
}

function Refresh_GridPersonnel_Personnel() {
    LoadState_Personnel = 'Normal';
    SetPageIndex_GridPersonnel_Personnel(0);
}

function Fill_GridPersonnel_Personnel(pageIndex) {
    document.getElementById('loadingPanel_GridPersonnel_Personnel').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridPersonnel_Personnel').value);
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel').value);
    switch (LoadState_Personnel) {
        case 'Normal':           
                CallBack_GridPersonnel_Personnel.callback(CharToKeyCode_Personnel(LoadState_Personnel), CharToKeyCode_Personnel(pageSize.toString()), CharToKeyCode_Personnel(pageIndex.toString()));           
            break;
        case 'Search':
            var seachTerm = document.getElementById('txtSerchTerm_Personnel').value;
            CallBack_GridPersonnel_Personnel.callback(CharToKeyCode_Personnel(LoadState_Personnel), CharToKeyCode_Personnel(pageSize.toString()), CharToKeyCode_Personnel(pageIndex.toString()), CharToKeyCode_Personnel(seachTerm));
            break;
        case 'AdvancedSearch':
            var seachTerm = AdvancedSearchTerm_Personnel;
            CallBack_GridPersonnel_Personnel.callback(CharToKeyCode_Personnel(LoadState_Personnel), CharToKeyCode_Personnel(pageSize.toString()), CharToKeyCode_Personnel(pageIndex.toString()), CharToKeyCode_Personnel(seachTerm));
            break;
    }
}

function Fill_GridPersonnel_Personnel_onPersonnelOperationCompleted(pageState, RetMessage) {
    switch (pageState) {
        case 'Add':
            Refresh_GridPersonnel_Personnel();
            break;
        case 'Edit':
            SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
            break;
    }
    showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
}

function GridPersonnel_Personnel_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridPersonnel_Personnel').innerHTML = '';
}

function CallBack_GridPersonnel_Personnel_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Personnel').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridPersonnel_Personnel(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else    
        Changefooter_GridPersonnel_Personnel();               
}

function Changefooter_GridPersonnel_Personnel() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridPersonnel_Personnel').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value) > 0 ? CurrentPageIndex_GridPersonnel_Personnel + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfPersonnelPageCount_Personnel').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridPersonnel_Personnel').innerHTML = retfooterVal;
    document.getElementById('lblPersonnelCount_Personnel').innerHTML = OriginalText_lblPersonnelCount_Personnel + ' ' + document.getElementById('hfPersonnelCount_Personnel').value;
}

function GetSexTitle_Personnel(sex) {
    var SexList = document.getElementById('hfSexList_Personnel').value.split('#');
    for (var i = 0; i < SexList.length; i++) {
        var sexObj = SexList[i].split(':');
        if (sexObj.length > 1) {
            if (sexObj[1] == sex.toString())
                return sexObj[0];
        }
    }
}

function GetMilitaryStatusTitle_Personnel(MilitaryStatus) {
    var MilitaryStatusList = document.getElementById('hfMilitaryStatusList_Personnel').value.split('#');
    for (var i = 0; i < MilitaryStatusList.length; i++) {
        var militaryStatusObj = MilitaryStatusList[i].split(':');
        if (militaryStatusObj.length > 1) {
            if (militaryStatusObj[1] == MilitaryStatus.toString())
                return militaryStatusObj[0];
        }
    }
}

function tlbItemRefresh_TlbPaging_GridPersonnel_Personnel_onClick() {
    Refresh_GridPersonnel_Personnel();
}

function tlbItemFirst_TlbPaging_GridPersonnel_Personnel_onClick() {
    SetPageIndex_GridPersonnel_Personnel(0);
}

function tlbItemBefore_TlbPaging_GridPersonnel_Personnel_onClick() {
    if (CurrentPageIndex_GridPersonnel_Personnel != 0) {
        CurrentPageIndex_GridPersonnel_Personnel = CurrentPageIndex_GridPersonnel_Personnel - 1;
        SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
    }
}

function tlbItemNext_TlbPaging_GridPersonnel_Personnel_onClick() {
    if (CurrentPageIndex_GridPersonnel_Personnel < parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value) - 1) {
        CurrentPageIndex_GridPersonnel_Personnel = CurrentPageIndex_GridPersonnel_Personnel + 1;
        SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
    }
}

function tlbItemLast_TlbPaging_GridPersonnel_Personnel_onClick() {
    SetPageIndex_GridPersonnel_Personnel(parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value) - 1);
}

function SetPageIndex_GridPersonnel_Personnel(pageIndex) {
    CurrentPageIndex_GridPersonnel_Personnel = pageIndex;
    Fill_GridPersonnel_Personnel(pageIndex);
}

function GetMaritalStatusTitle_Personnel(MaritalStatus) {
    var MaritalStatusList = document.getElementById('hfMaritalStatusList_Personnel').value.split('#');
    for (var i = 0; i < MaritalStatusList.length; i++) {
        var maritalStatusObj = MaritalStatusList[i].split(':');
        if (maritalStatusObj.length > 1) {
            if (maritalStatusObj[1] == MaritalStatus.toString())
                return maritalStatusObj[0];
        }
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Personnel) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePersonnel_Personnel();
            break;
        case 'Exit':
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    CurrentPageState_Personnel = 'View';
}

function CharToKeyCode_Personnel(str) {
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


function UpdatePersonnel_Personnel() {
    ObjPersonnel_Personnel = new Object();
    ObjPersonnel_Personnel.ID = '0';
    ObjPersonnel_Personnel.Image = null;
    var SelectedPersonnel_Personnel = GridPersonnel_Personnel.getSelectedItems();
    if (SelectedPersonnel_Personnel.length > 0) {
        ObjPersonnel_Personnel.ID = SelectedPersonnel_Personnel[0].getMember('ID').get_text();
        ObjPersonnel_Personnel.Image = SelectedPersonnel_Personnel[0].getMember('PersonDetail.Image').get_text();
        ObjPersonnel_Personnel.Department = SelectedPersonnel_Personnel[0].getMember('Department.ID').get_text();
    }
    UpdatePersonnel_PersonnelPage(CharToKeyCode_Personnel(CurrentPageState_Personnel), CharToKeyCode_Personnel(ObjPersonnel_Personnel.ID), CharToKeyCode_Personnel(ObjPersonnel_Personnel.Image), CharToKeyCode_Personnel(ObjPersonnel_Personnel.Department));
    DialogWaiting.Show();
}

function UpdatePersonnel_PersonnelPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Personnel').value;
            Response[1] = document.getElementById('hfConnectionError_Personnel').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Personnel_OnAfterUpdate(Response);
            CurrentPageState_Personnel = 'View';
        }
        else {
            if (CurrentPageState_Personnel == 'Delete')
                CurrentPageState_Personnel = 'View';
        }
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function Personnel_OnAfterUpdate(Response) {
    if (ObjPersonnel_Personnel != null) {
        GridPersonnel_Personnel.beginUpdate();
        switch (CurrentPageState_Personnel) {
            case 'Delete':
                GridPersonnel_Personnel.selectByKey(ObjPersonnel_Personnel.ID, 0, false);
                GridPersonnel_Personnel.deleteSelected();
                UpdateFeatures_GridPersonnel_Personnel();
                break;
        }
        GridPersonnel_Personnel.endUpdate();
    }
}

function UpdateFeatures_GridPersonnel_Personnel() {
    var personnelCount = parseInt(document.getElementById('hfPersonnelCount_Personnel').value);
    var PersonnelPageCount = parseInt(document.getElementById('hfPersonnelPageCount_Personnel').value);
    var PersonnelPageSize = parseInt(document.getElementById('hfPersonnelPageSize_Personnel').value);
    if (personnelCount > 0) {
        personnelCount = personnelCount - 1;
        var divRem = mod(personnelCount, PersonnelPageSize);
        if (divRem == 0) {
            PersonnelPageCount = PersonnelPageCount - 1;
            if (CurrentPageIndex_GridPersonnel_Personnel == PersonnelPageCount)
                CurrentPageIndex_GridPersonnel_Personnel = CurrentPageIndex_GridPersonnel_Personnel - 1 >= 0 ? CurrentPageIndex_GridPersonnel_Personnel - 1 : 0;
        }
        SetPageIndex_GridPersonnel_Personnel(CurrentPageIndex_GridPersonnel_Personnel);
        document.getElementById('hfPersonnelCount_Personnel').value = personnelCount.toString();
        document.getElementById('hfPersonnelPageCount_Personnel').value = PersonnelPageCount.toString();
        Changefooter_GridPersonnel_Personnel();
    }
}

function GetBoxesHeaders_Personnel() {
    document.getElementById('header_Personnel_Personnel').innerHTML = document.getElementById('hfheader_Personnel_Personnel').value;
    document.getElementById('footer_GridPersonnel_Personnel').innerHTML = document.getElementById('hffooter_GridPersonnel_Personnel').value;
    OriginalText_lblPersonnelCount_Personnel = document.getElementById('lblPersonnelCount_Personnel').innerHTML;
}

function SetActionMode_Personnel(state) {
    document.getElementById('ActionMode_Personnel').innerHTML = document.getElementById('hf' + state + '_Personnel').value;
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Personnel = confirmState;
    if (CurrentPageState_Personnel == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Personnel').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Personnel').value;
    DialogConfirm.Show();
}

function CallBack_GridPersonnel_Personnel_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridPersonnel_Personnel').innerHTML = '';
    ShowConnectionError_Personnel();
}

function ShowConnectionError_Personnel() {
    var error = document.getElementById('hfErrorType_Personnel').value;
    var errorBody = document.getElementById('hfConnectionError_Personnel').value;
    showDialog(error, errorBody, 'error');
}


function MasterPersonnelMainInformation_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_Personnel = SearchTerm;
    SetPageIndex_GridPersonnel_Personnel(0);
}

function tlbItemFormReconstruction_TlbPersonnel_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPersonnelIntroduction_iFrame').src = parent.ModulePath + 'MasterPersonnel.aspx';
}

function tlbItemHelp_TlbPersonnel_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPersonnel');
}

function ChangeDirection_Container_GridPersonnel_Personnel() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('Container_GridPersonnel_Personnel').style.direction = 'ltr';
}

function txtSerchTerm_Personnel_onKeyPess(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbPersonnelQuickSearch_onClick();
    }
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function tlbItemBarCodeOk_TlbBarCodeOk_onClick() {
    var selectedPersonnel_Personnel = GridPersonnel_Personnel.getSelectedItems()[0];
    if (selectedPersonnel_Personnel != undefined) {
       var  PersonnelID_Personnel = selectedPersonnel_Personnel.getMember('ID').get_text();
       var  PersonnelBarCode_Personnel = document.getElementById('txtBarCode_Personnel').value;
        if (PersonnelBarCode_Personnel != '') {          
            UpdateDeletedPersonnel_Personnel(PersonnelID_Personnel, PersonnelBarCode_Personnel);
        }
    }        
}
function tlbItemBarCodeCancel_TlbBarCodeCancel_onClick() {
    DialogPersonnelBarCode.Close();
}
function txtSBarCode_Personnel_onKeyPess() {

}
function tlbItemPersonnelRetrieval_TlbPersonnel_onClick() {  
    var selectedPersonnel_Personnel = GridPersonnel_Personnel.getSelectedItems()[0];
    if (selectedPersonnel_Personnel != undefined && selectedPersonnel_Personnel.getMember('IsDeleted').get_text() == "true") {
        var PersonnelID_Personnel = selectedPersonnel_Personnel.getMember('ID').get_text();
        var PersonnelBarCode_Personnel = selectedPersonnel_Personnel.getMember('PersonCode').get_text();
        UpdateDeletedPersonnel_Personnel(PersonnelID_Personnel, PersonnelBarCode_Personnel);
    }   
}
function UpdateDeletedPersonnel_Personnel(PersonnelID, PersonnelBarCode) {
    UpdateDeletedPersonnel_PersonnelPage(CharToKeyCode_Personnel(PersonnelID), CharToKeyCode_Personnel(PersonnelBarCode.toString()), CharToKeyCode_Personnel(EncryptData_Personnel(PersonnelBarCode.toString()).toString()));
}

function EncryptData_Personnel(text) {
    var iv = CryptoJS.enc.Utf8.parse('1234567891234567');
    var key = CryptoJS.enc.Utf8.parse("www.ghadirco.net");
    var encryptedText = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(text), key, {
        keySize: 128 / 8,
        iv: iv,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });
    return encryptedText;
}


function UpdateDeletedPersonnel_PersonnelPage_onCallBack(Response) {    
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {      
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Personnel').value;
            Response[1] = document.getElementById('hfConnectionError_Personnel').value;
        }      
        if (RetMessage[2] == 'success') {
            if (RetMessage[3] != '' && RetMessage[4] != '') {
                DialogPersonnelBarCode.Show();
                document.getElementById('PersonnelBarcode_Personnel').innerHTML = document.getElementById('hfPersonnelBarcode_Personnel').value + RetMessage[3];
                document.getElementById('UserName_Personnel').innerHTML = document.getElementById('hfUserName_Personnel').value + RetMessage[5];
                document.getElementById('PersonnelName_Personnel').innerHTML = document.getElementById('hfPersonnelName_Personnel').value + RetMessage[4];                
                document.getElementById('txtBarCode_Personnel').value = '';
            }
            else {
                DialogPersonnelBarCode.Close();
                LoadState_Personnel = 'Normal';
                Fill_GridPersonnel_Personnel(0);
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
            }            
        }
        else {
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }

    }
}
