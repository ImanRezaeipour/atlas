
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadState_cmbPersonnel_UserSettings = 'Normal';
var SearchTerm_cmbPersonnel_UserSettings = '';
var AdvancedSearchTerm_cmbPersonnel_UserSettings = '';
var CurrentPageIndex_cmbPersonnel_UserSettings = 0;
var PersonnelCountState_UserSettings = 'Single';
var ConfirmState_UserSettings = null;
var ObjEmailSMSSettings_UserSettings = null;
var ObjDashboardSettings_UserSettings = null;
var ObjOperatorCollectiveRequestRegistType = null;


function GetBoxesHeaders_UserSettings() {
    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    var DialogUserSettingsTitle = null;
    var Caller = ObjDialogUserSettings.Caller;
    switch (Caller) {
        case 'Management':
            document.getElementById('headerPersonnelSelect_UserSettings').innerHTML = document.getElementById('hfheaderPersonnelSelect_UserSettings').value;
            document.getElementById('clmnName_cmbPersonnel_UserSettings').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_UserSettings').value;
            document.getElementById('clmnBarCode_cmbPersonnel_UserSettings').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_UserSettings').value;
            document.getElementById('clmnCardNum_cmbPersonnel_UserSettings').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_UserSettings').value;
            DialogUserSettingsTitle = document.getElementById('hfManagementTitle_DialogUserSettings').value;
            break;
        case 'Personal':
            document.getElementById('headerSkinsSettings_UserSettings').innerHTML = document.getElementById('hfheaderSkinsSettings_UserSettings').value;
            DialogUserSettingsTitle = document.getElementById('hfPersonalTitle_DialogUserSettings').value;
            break;
    }
    parent.document.getElementById('Title_DialogUserSettings').innerHTML = DialogUserSettingsTitle;
    document.getElementById('headerEmailSMSSettings_UserSettings').innerHTML = document.getElementById('hfheaderEmailSMSSettings_UserSettings').value;
}

function ChangeDirection_Mastertbl_UserSettings() {
    var direction = null;
    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('Mastertbl_UserSettings').dir =
    document.getElementById('cmbDay_DayEmail_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbHour_DayEmail_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbMinute_DayEmail_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbHour_HourEmail_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbMinute_HourEmail_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbDay_DaySMS_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbHour_DaySMS_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbMinute_DaySMS_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbHour_HourSMS_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbMinute_HourSMS_EmailSMSSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbDashboard1_DashboradSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbDashboard2_DashboradSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbDashboard3_DashboradSettings_UserSettings_DropDown').dir =
    document.getElementById('cmbDashboard4_DashboradSettings_UserSettings_DropDown').dir =
    document.getElementById('tblConfirm_DialogConfirm').dir = direction;
    switch (ObjDialogUserSettings.Caller) {
        case 'Management':
            ChangeDirection_cmbPersonnel_UserSettings();
            break;
        case 'Personal':
            document.getElementById('cmbSkins_SkinsSettings_UserSettings_DropDown').dir = direction;
            break;
    }
}

function ChangeDirection_cmbPersonnel_UserSettings() {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('cmbPersonnel_UserSettings_DropDown').dir = direction;
}

function CallBack_cmbPersonnel_UserSettings_onBeforeCallback(sender, e) {
    cmbPersonnel_UserSettings.dispose();
}

function cmbPersonnel_UserSettings_onChange(sender, e) {
    SetSelectedPersonnelCount_UserSettings();
}

function CallBack_cmbPersonnel_UserSettings_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_UserSettings').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_UserSettings').value;
    document.getElementById('clmnBarCode_cmbPersonnel_UserSettings').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_UserSettings').value;
    document.getElementById('clmnCardNum_cmbPersonnel_UserSettings').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_UserSettings').value;

    ChangeDirection_cmbPersonnel_UserSettings();

    SetSelectedPersonnelCount_UserSettings();
    var error = document.getElementById('ErrorHiddenField_Personnel_UserSettings').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_UserSettings_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_UserSettings_DropImage').mousedown();
        else
            cmbPersonnel_UserSettings.expand();

        var personnelCount = document.getElementById('hfPersonnelCount_UserSettings').value;
        ChangeRequestPersonnelCount_UserSettings(personnelCount);
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_UserSettings_DropDown').style.display = 'none';
    }
}

function ChangeRequestPersonnelCount_UserSettings(personnelCount) {
    var countVal = document.getElementById('headerPersonnelCount_UserSettings').innerHTML;
    var countValCol = countVal.split(':');
    countVal = countValCol[0] + ':' + personnelCount;
}

function SetPosition_cmbPersonnel_UserSettings() {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('cmbPersonnel_UserSettings_DropDown').style.left = document.getElementById('Mastertbl_UserSettings').clientWidth - 400 + 'px';
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('cmbPersonnel_UserSettings_DropDown').style.left = '10px';
}


function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbPersonnel_UserSettings_onCallbackError(sender, e) {
    ShowConnectionError_UserSettings();
}

function ShowConnectionError_UserSettings() {
    var error = document.getElementById('hfErrorType_UserSettings').value;
    var errorBody = document.getElementById('hfConnectionError_UserSettings').value;
    showDialog(error, errorBody, 'error');
}


function SetSelectedPersonnelCount_UserSettings() {
    switch (PersonnelCountState_UserSettings) {
        case 'Single':
            if (cmbPersonnel_UserSettings.get_selectedIndex() >= 0)
                document.getElementById('lblSelectedPersonnelCount_UserSettings').innerHTML = document.getElementById('hfheaderPersonnelCount_UserSettings').value + 1;
            else
                document.getElementById('lblSelectedPersonnelCount_UserSettings').innerHTML = document.getElementById('hfheaderPersonnelCount_UserSettings').value + 0;
            break;
        case 'Group':
            document.getElementById('lblSelectedPersonnelCount_UserSettings').innerHTML = document.getElementById('hfheaderPersonnelCount_UserSettings').value + document.getElementById('hfPersonnelSelectedCount_UserSettings').value;
            break;
    }
}

function tlbItemSearch_TlbSearchPersonnel_UserSettings_onClick() {
    LoadState_cmbPersonnel_UserSettings = 'Search';
    CurrentPageIndex_cmbPersonnel_UserSettings = 0;
    SetPageIndex_cmbPersonnel_UserSettings(0);
}

function cmbPersonnel_UserSettings_onExpand(sender, e) {
    if (cmbPersonnel_UserSettings.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_UserSettings == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_UserSettings = true;
        SetPageIndex_cmbPersonnel_UserSettings(0);
    }
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_UserSettings_onClick() {
    LoadState_cmbPersonnel_UserSettings = 'AdvancedSearch';
    CurrentPageIndex_cmbPersonnel_UserSettings = 0;
    ShowDialogPersonnelSearch('UserSettings');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_UserSettings();
}

function UserSettings_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_UserSettings = SearchTerm;
    SetPageIndex_cmbPersonnel_UserSettings(0);
}


function tlbItemRefresh_TlbPaging_PersonnelSearch_UserSettings_onClick() {
    Refresh_cmbPersonnel_UserSettings();
}

function Refresh_cmbPersonnel_UserSettings() {
    ChangeLoadState_cmbPersonnel_UserSettings('Normal');
}

function ChangeLoadState_cmbPersonnel_UserSettings(state) {
    LoadState_cmbPersonnel_UserSettings = state;
    SetPageIndex_cmbPersonnel_UserSettings(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_UserSettings_onClick() {
    SetPageIndex_cmbPersonnel_UserSettings(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_UserSettings_onClick() {
    if (CurrentPageIndex_cmbPersonnel_UserSettings != 0) {
        CurrentPageIndex_cmbPersonnel_UserSettings = CurrentPageIndex_cmbPersonnel_UserSettings - 1;
        SetPageIndex_cmbPersonnel_UserSettings(CurrentPageIndex_cmbPersonnel_UserSettings);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_UserSettings_onClick() {
    if (CurrentPageIndex_cmbPersonnel_UserSettings < parseInt(document.getElementById('hfPersonnelPageCount_UserSettings').value) - 1) {
        CurrentPageIndex_cmbPersonnel_UserSettings = CurrentPageIndex_cmbPersonnel_UserSettings + 1;
        SetPageIndex_cmbPersonnel_UserSettings(CurrentPageIndex_cmbPersonnel_UserSettings);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_UserSettings_onClick() {
    SetPageIndex_cmbPersonnel_UserSettings(parseInt(document.getElementById('hfPersonnelPageCount_UserSettings').value) - 1);
}

function SetPageIndex_cmbPersonnel_UserSettings(pageIndex) {
    CurrentPageIndex_cmbPersonnel_UserSettings = pageIndex;
    Fill_cmbPersonnel_UserSettings(pageIndex);
}

function Fill_cmbPersonnel_UserSettings(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_UserSettings').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_UserSettings) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_UserSettings = SearchTermConditions = document.getElementById('txtPersonnelSearch_UserSettings').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_UserSettings;
            break;
    }
    ComboBox_onBeforeLoadData('cmbPersonnel_UserSettings');
    CallBack_cmbPersonnel_UserSettings.callback(CharToKeyCode_UserSettings(LoadState_cmbPersonnel_UserSettings), CharToKeyCode_UserSettings(pageSize.toString()), CharToKeyCode_UserSettings(pageIndex.toString()), CharToKeyCode_UserSettings(SearchTermConditions));
}

function tlbItemHelp_TlbUserSettings_onClick() {
    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    var Caller = ObjDialogUserSettings.Caller;
    switch (Caller) {
        case 'Personal':
            LoadHelpPage('tlbItemHelp_TlbPersonalUserSettings');
            break;
        case 'Management':
            LoadHelpPage('tlbItemHelp_TlbManagementUserSettings');
            break;
    }
}

function tlbItemFormReconstruction_TlbUserSettings_onClick() {
    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    parent.DialogUserSettings.Close();
    parent.DialogUserSettings.set_value(ObjDialogUserSettings);
    parent.DialogUserSettings.Show();
}

function tlbItemExit_TlbUserSettings_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    CollapseControls_UserSettings();
    ConfirmState_UserSettings = confirmState;
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_UserSettings').value;
    DialogConfirm.Show();
}

function CollapseControls_UserSettings() {
    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    var Caller = ObjDialogUserSettings.Caller;
    switch (Caller) {
        case 'Management':
            cmbPersonnel_UserSettings.collapse();
            break;
        case 'Personal':
            cmbSkins_SkinsSettings_UserSettings.collapse();
            break;
    }
    cmbDay_DayEmail_EmailSMSSettings_UserSettings.collapse();
    cmbHour_DayEmail_EmailSMSSettings_UserSettings.collapse();
    cmbMinute_DayEmail_EmailSMSSettings_UserSettings.collapse();
    cmbHour_HourEmail_EmailSMSSettings_UserSettings.collapse();
    cmbMinute_HourEmail_EmailSMSSettings_UserSettings.collapse();
    cmbDay_DaySMS_EmailSMSSettings_UserSettings.collapse();
    cmbHour_DaySMS_EmailSMSSettings_UserSettings.collapse();
    cmbMinute_DaySMS_EmailSMSSettings_UserSettings.collapse();
    cmbHour_HourSMS_EmailSMSSettings_UserSettings.collapse();
    cmbMinute_HourSMS_EmailSMSSettings_UserSettings.collapse();
}

function CollapseControls_DashboardSettings_UserSettings(exception) {
    if (exception == null || exception != cmbDashboard1_DashboradSettings_UserSettings)
        cmbDashboard1_DashboradSettings_UserSettings.collapse();
    if (exception == null || exception != cmbDashboard2_DashboradSettings_UserSettings)
        cmbDashboard2_DashboradSettings_UserSettings.collapse();
    if (exception == null || exception != cmbDashboard3_DashboradSettings_UserSettings)
        cmbDashboard3_DashboradSettings_UserSettings.collapse();
    if (exception == null || exception != cmbDashboard4_DashboradSettings_UserSettings)
        cmbDashboard4_DashboradSettings_UserSettings.collapse();
}

function CloseDialogUserSettings() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogUserSettings_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogUserSettings').Close();
}

function tlbItemSave_TlbEmailSMSSettings_onClick() {
    SaveSettings_EmailSMSSettings_UserSettings();
}

function SaveSettings_EmailSMSSettings_UserSettings() {
    UpdateSettings_UserSettings('EmailSMS');
}

function UpdateSettings_UserSettings(SettingsState) {
    var StrSettingsTerms = '';
    var PersonnelID = '0';
    var SearchTermConditions = '';

    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    var Caller = ObjDialogUserSettings.Caller;

    switch (LoadState_cmbPersonnel_UserSettings) {
        case 'Normal':
            break;
        case 'Search':
            SearchTermConditions = document.getElementById('txtPersonnelSearch_UserSettings').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_UserSettings;
            break;
    }
    if (Caller == 'Management' && cmbPersonnel_UserSettings.getSelectedItem() != undefined)
        PersonnelID = cmbPersonnel_UserSettings.getSelectedItem().get_id();

    switch (SettingsState) {
        case 'EmailSMS':
            if (ObjEmailSMSSettings_UserSettings == null) {
                ObjEmailSMSSettings_UserSettings = new Object();

                ObjEmailSMSSettings_UserSettings.IsSendEmail = false;
                ObjEmailSMSSettings_UserSettings.IsSendSMS = false;
                ObjEmailSMSSettings_UserSettings.SendEmailState = '';
                ObjEmailSMSSettings_UserSettings.SendSMSState = '';
                ObjEmailSMSSettings_UserSettings.DailyEmailDay = '';
                ObjEmailSMSSettings_UserSettings.DailyEmailHour = '';
                ObjEmailSMSSettings_UserSettings.DailyEmailMinute = '';
                ObjEmailSMSSettings_UserSettings.HourlyEmailHour = '';
                ObjEmailSMSSettings_UserSettings.HourlyEmailMinute = '';
                ObjEmailSMSSettings_UserSettings.DailySMSDay = '';
                ObjEmailSMSSettings_UserSettings.DailySMSHour = '';
                ObjEmailSMSSettings_UserSettings.DailySMSMinute = '';
                ObjEmailSMSSettings_UserSettings.HourlySMSHour = '';
                ObjEmailSMSSettings_UserSettings.HourlySMSMinute = '';
            }

            if (document.getElementById('chbVerifySendEmail_EmailSMSSettings_UserSettings').checked)
                ObjEmailSMSSettings_UserSettings.IsSendEmail = true;
            if (document.getElementById('rdbSendEmail_DayEmail_EmailSMSSettings_UserSettings').checked)
                ObjEmailSMSSettings_UserSettings.SendEmailState = 'Daily';
            if (cmbDay_DayEmail_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.DailyEmailDay = cmbDay_DayEmail_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (cmbHour_DayEmail_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.DailyEmailHour = cmbHour_DayEmail_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (cmbMinute_DayEmail_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.DailyEmailMinute = cmbMinute_DayEmail_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (document.getElementById('rdbSendEmail_HourEmail_EmailSMSSettings_UserSettings').checked)
                ObjEmailSMSSettings_UserSettings.SendEmailState = 'Hourly';
            if (cmbHour_HourEmail_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.HourlyEmailHour = cmbHour_HourEmail_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (cmbMinute_HourEmail_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.HourlyEmailMinute = cmbMinute_HourEmail_EmailSMSSettings_UserSettings.getSelectedItem().get_text();

            if (document.getElementById('chbVerifySendSMS_EmailSMSSettings_UserSettings').checked)
                ObjEmailSMSSettings_UserSettings.IsSendSMS = true;
            if (document.getElementById('rdbSendSMS_DaySMS_EmailSMSSettings_UserSettings').checked)
                ObjEmailSMSSettings_UserSettings.SendSMSState = 'Daily';
            if (cmbDay_DaySMS_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.DailySMSDay = cmbDay_DaySMS_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (cmbHour_DaySMS_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.DailySMSHour = cmbHour_DaySMS_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (cmbMinute_DaySMS_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.DailySMSMinute = cmbMinute_DaySMS_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (document.getElementById('rdbSendSMS_HourSMS_EmailSMSSettings_UserSettings').checked)
                ObjEmailSMSSettings_UserSettings.SendSMSState = 'Hourly';
            if (cmbHour_HourSMS_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.HourlySMSHour = cmbHour_HourSMS_EmailSMSSettings_UserSettings.getSelectedItem().get_text();
            if (cmbMinute_HourSMS_EmailSMSSettings_UserSettings.getSelectedItem() != undefined)
                ObjEmailSMSSettings_UserSettings.HourlySMSMinute = cmbMinute_HourSMS_EmailSMSSettings_UserSettings.getSelectedItem().get_text();

            StrSettingsTerms = '{"IsSendEmail":"' + ObjEmailSMSSettings_UserSettings.IsSendEmail.toString() + '","IsSendSMS":"' + ObjEmailSMSSettings_UserSettings.IsSendSMS.toString() + '","SendEmailState":"' + ObjEmailSMSSettings_UserSettings.SendEmailState + '","SendSMSState":"' + ObjEmailSMSSettings_UserSettings.SendSMSState + '","DailyEmailDay":"' + ObjEmailSMSSettings_UserSettings.DailyEmailDay + '","DailyEmailHour":"' + ObjEmailSMSSettings_UserSettings.DailyEmailHour + '","DailyEmailMinute":"' + ObjEmailSMSSettings_UserSettings.DailyEmailMinute + '","HourlyEmailHour":"' + ObjEmailSMSSettings_UserSettings.HourlyEmailHour + '","HourlyEmailMinute":"' + ObjEmailSMSSettings_UserSettings.HourlyEmailMinute + '","DailySMSDay":"' + ObjEmailSMSSettings_UserSettings.DailySMSDay + '","DailySMSHour":"' + ObjEmailSMSSettings_UserSettings.DailySMSHour + '","DailySMSMinute":"' + ObjEmailSMSSettings_UserSettings.DailySMSMinute + '","HourlySMSHour":"' + ObjEmailSMSSettings_UserSettings.HourlySMSHour + '","HourlySMSMinute":"' + ObjEmailSMSSettings_UserSettings.HourlySMSMinute + '","SettingType":"EmailSMS"}';
            break;
        case 'Dashboard':
            if (ObjDashboardSettings_UserSettings == null) {
                ObjDashboardSettings_UserSettings = new Object();
                ObjDashboardSettings_UserSettings.DashboardID1 = '';
                ObjDashboardSettings_UserSettings.DashboardID2 = '';
                ObjDashboardSettings_UserSettings.DashboardID3 = '';
                ObjDashboardSettings_UserSettings.DashboardID4 = '';
            }
            if (cmbDashboard1_DashboradSettings_UserSettings.getSelectedItem() != undefined)
                ObjDashboardSettings_UserSettings.DashboardID1 = cmbDashboard1_DashboradSettings_UserSettings.getSelectedItem().get_value();

            if (cmbDashboard2_DashboradSettings_UserSettings.getSelectedItem() != undefined)
                ObjDashboardSettings_UserSettings.DashboardID2 = cmbDashboard2_DashboradSettings_UserSettings.getSelectedItem().get_value();

            if (cmbDashboard3_DashboradSettings_UserSettings.getSelectedItem() != undefined)
                ObjDashboardSettings_UserSettings.DashboardID3 = cmbDashboard3_DashboradSettings_UserSettings.getSelectedItem().get_value();

            if (cmbDashboard4_DashboradSettings_UserSettings.getSelectedItem() != undefined)
                ObjDashboardSettings_UserSettings.DashboardID4 = cmbDashboard4_DashboradSettings_UserSettings.getSelectedItem().get_value();

            StrSettingsTerms = '{"DashboardID1":"' + ObjDashboardSettings_UserSettings.DashboardID1 + '","DashboardID2":"' + ObjDashboardSettings_UserSettings.DashboardID2 + '","DashboardID3":"' + ObjDashboardSettings_UserSettings.DashboardID3 + '","DashboardID4":"' + ObjDashboardSettings_UserSettings.DashboardID4 + '"}';
            break;
        case 'CollectiveRequestRegistType':
            if (ObjOperatorCollectiveRequestRegistType == null)
            {
                ObjOperatorCollectiveRequestRegistType = new Object();
                ObjOperatorCollectiveRequestRegistType.CollectiveRequestType = '';
               
            }
            if (document.getElementById('rdbOperatorCollectiveRequestRegistByBusiness_UserSettings').checked)
                ObjOperatorCollectiveRequestRegistType.CollectiveRequestType = 'Business';
            if (document.getElementById('rdbOperatorCollectiveRequestRegistByService_UserSettings').checked)
                ObjOperatorCollectiveRequestRegistType.CollectiveRequestType = 'Service';
            StrSettingsTerms = '{"CollectiveRequestRegistType":"' + ObjOperatorCollectiveRequestRegistType.CollectiveRequestType + '","SettingType":"CollectiveRequestRegistType"}';
            break;
    }

    UpdateSettings_UserSettingsPage(CharToKeyCode_UserSettings(Caller), CharToKeyCode_UserSettings(SettingsState), CharToKeyCode_UserSettings(LoadState_cmbPersonnel_UserSettings), CharToKeyCode_UserSettings(PersonnelCountState_UserSettings), CharToKeyCode_UserSettings(PersonnelID), CharToKeyCode_UserSettings(SearchTermConditions), CharToKeyCode_UserSettings(StrSettingsTerms));
    DialogWaiting.Show();
}

function UpdateSettings_UserSettingsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UserSettings').value;
            Response[1] = document.getElementById('hfConnectionError_UserSettings').value;
        }
        UserSettings_onAfterUpdate(Response);
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function UserSettings_onAfterUpdate(Response) {
    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    var Caller = ObjDialogUserSettings.Caller;
    switch (Caller) {
        case 'Management':
            break;
        case 'Personal':
            if (Response[3] != null && Response[3] == 'Dashboard')
                parent.document.getElementById('pgvWelcome_iFrame').contentWindow.initializeParts_MainView();
            break;
    }
}

function CharToKeyCode_UserSettings(str) {
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

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_UserSettings) {
        case 'Exit':
            CloseDialogUserSettings();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}


function tlbItemSave_TlbSkinsSettings_onClick() {
    if (cmbSkins_SkinsSettings_UserSettings.getSelectedItem() != undefined) {
        var skinID = cmbSkins_SkinsSettings_UserSettings.getSelectedItem().get_id();
        ChangeSkin(skinID);
    }
}

function ChangeSkin(skinID) {
    if (skinID != null && skinID != undefined) {
        ChangeSkin_UserSettingsPage(CharToKeyCode_UserSettings(skinID));
        DialogWaiting.Show();
    }

}

function ChangeSkin_UserSettingsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UserSettings').value;
            Response[1] = document.getElementById('hfConnectionError_UserSettings').value;
        }
        if (RetMessage[2] == 'success') {
            //DNN Note
            this.parent.window.location = '/';
            //this.parent.window.location = 'MainPage.aspx?reload=' + (new Date()).getTime();
        }
        else
            if (RetMessage[2] == 'error')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function tlbItemPersonnelSettingsView_TlbUserSettings_onClick() {
    GetSettings_UserSettings('View');
}

function GetSettings_UserSettings(state) {
    var ObjDialogUserSettings = parent.DialogUserSettings.get_value();
    var Caller = ObjDialogUserSettings.Caller;
    var personnelID = '0';
    switch (Caller) {
        case 'Management':
            if (state == 'View' && cmbPersonnel_UserSettings.getSelectedItem() != undefined)
                personnelID = cmbPersonnel_UserSettings.getSelectedItem().get_id();
            break;
        case 'Personal':
            break;
    }
    GetSettings_UserSettingsPage(CharToKeyCode_UserSettings(Caller), CharToKeyCode_UserSettings(personnelID));
    DialogWaiting.Show();
}

function GetSettings_UserSettingsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UserSettings').value;
            Response[1] = document.getElementById('hfConnectionError_UserSettings').value;
        }
        if (RetMessage[2] == 'success') {
            var SettingsBatch = RetMessage[3];
            SetSettings_UserSettings(SettingsBatch);
            SetSettingSchema_UserSettings(SettingsBatch);
        }
        else
            if (RetMessage[2] == 'error')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function SetSettingSchema_UserSettings(SettingsBatch) {
    SettingsBatch = eval('(' + SettingsBatch + ')');
    switch (SettingsBatch[2].Schema) {
        case 'Default':
            if (document.getElementById('rdbMonthlyOperationSchemaDefault_UserSettings') != undefined)
                document.getElementById('rdbMonthlyOperationSchemaDefault_UserSettings').checked = true;
            break;
        case 'Grid':
            if (document.getElementById('rdbMonthlyOperationGridSchema_UserSettings') != undefined)
                document.getElementById('rdbMonthlyOperationGridSchema_UserSettings').checked = true;
            break;
        case 'GanttChart':
            if (document.getElementById('rdbMonthlyOperationGanttChartSchema_UserSettings') != undefined)
                document.getElementById('rdbMonthlyOperationGanttChartSchema_UserSettings').checked = true;
            break;
    }
}
function SetSettingOperatorCollectiveRequestRegist_UserSettings(SettingTerm) {
    switch (SettingTerm.CollectiveRequestRegistType) {
        case 'Business':
            if (document.getElementById('rdbOperatorCollectiveRequestRegistByBusiness_UserSettings') != undefined)
                document.getElementById('rdbOperatorCollectiveRequestRegistByBusiness_UserSettings').checked = true;
            break;
        case 'Service':
            if (document.getElementById('rdbOperatorCollectiveRequestRegistByService_UserSettings') != undefined)
                document.getElementById('rdbOperatorCollectiveRequestRegistByService_UserSettings').checked = true;
            break;
    }
}

function SetSettings_UserSettings(SettingsBatch) {
    SettingsBatch = eval('(' + SettingsBatch + ')');
    for (var i = 0; i < SettingsBatch.length; i++) {
        switch (SettingsBatch[i].SettingType) {
            case 'EmailSMS':
                SetEmailSMSSettings_UserSettings(SettingsBatch[i]);
                break;
            case 'Dashboard':
                SetDashboardSettings_UserSettings(SettingsBatch[i]);
                break;
            case 'CollectiveRequestRegistType':
                SetSettingOperatorCollectiveRequestRegist_UserSettings(SettingsBatch[i]);
                break;
        }
    }
}

function SetDashboardSettings_UserSettings(SettingTerm) {
    if (ObjDashboardSettings_UserSettings == null)
        ObjDashboardSettings_UserSettings = new Object();
    ObjDashboardSettings_UserSettings.DashboardTitle1 = document.getElementById('cmbDashboard1_DashboradSettings_UserSettings_Input').value = SettingTerm.DashboardTitle1;
    ObjDashboardSettings_UserSettings.DashboardTitle2 = document.getElementById('cmbDashboard2_DashboradSettings_UserSettings_Input').value = SettingTerm.DashboardTitle2;
    ObjDashboardSettings_UserSettings.DashboardTitle3 = document.getElementById('cmbDashboard3_DashboradSettings_UserSettings_Input').value = SettingTerm.DashboardTitle3;
    ObjDashboardSettings_UserSettings.DashboardTitle4 = document.getElementById('cmbDashboard4_DashboradSettings_UserSettings_Input').value = SettingTerm.DashboardTitle4;
    ObjDashboardSettings_UserSettings.DashboardID1 = SettingTerm.DashboardID1;
    ObjDashboardSettings_UserSettings.DashboardID2 = SettingTerm.DashboardID2;
    ObjDashboardSettings_UserSettings.DashboardID3 = SettingTerm.DashboardID3;
    ObjDashboardSettings_UserSettings.DashboardID4 = SettingTerm.DashboardID4;

}

function SetEmailSMSSettings_UserSettings(SettingTerms) {
    if (ObjEmailSMSSettings_UserSettings == null)
        ObjEmailSMSSettings_UserSettings = new Object();
    ObjEmailSMSSettings_UserSettings.IsSendEmail = document.getElementById('chbVerifySendEmail_EmailSMSSettings_UserSettings').checked = GetCheckedValue_UserSettings(SettingTerms.IsSendEmail);
    ObjEmailSMSSettings_UserSettings.IsSendSMS = document.getElementById('chbVerifySendSMS_EmailSMSSettings_UserSettings').checked = GetCheckedValue_UserSettings(SettingTerms.IsSendSMS);
    ObjEmailSMSSettings_UserSettings.SendEmailState = SettingTerms.SendEmailState;
    SetRelativeEmailSMSTransferChecked_UserSettings(SettingTerms.SendEmailState, document.getElementById('rdbSendEmail_DayEmail_EmailSMSSettings_UserSettings'), document.getElementById('rdbSendEmail_HourEmail_EmailSMSSettings_UserSettings'));
    ObjEmailSMSSettings_UserSettings.SendSMSState = SettingTerms.SendSMSState;
    SetRelativeEmailSMSTransferChecked_UserSettings(SettingTerms.SendSMSState, document.getElementById('rdbSendSMS_DaySMS_EmailSMSSettings_UserSettings'), document.getElementById('rdbSendSMS_HourSMS_EmailSMSSettings_UserSettings'));

    ObjEmailSMSSettings_UserSettings.DailyEmailDay = SettingTerms.DailyEmailDay;
    ObjEmailSMSSettings_UserSettings.DailyEmailHour = SettingTerms.DailyEmailHour;
    ObjEmailSMSSettings_UserSettings.DailyEmailMinute = SettingTerms.DailyEmailMinute;
    ObjEmailSMSSettings_UserSettings.HourlyEmailHour = SettingTerms.HourlyEmailHour;
    ObjEmailSMSSettings_UserSettings.HourlyEmailMinute = SettingTerms.HourlyEmailMinute;
    document.getElementById('cmbDay_DayEmail_EmailSMSSettings_UserSettings_Input').value = SettingTerms.DailyEmailDay;
    document.getElementById('cmbHour_DayEmail_EmailSMSSettings_UserSettings_Input').value = SettingTerms.DailyEmailHour;
    document.getElementById('cmbMinute_DayEmail_EmailSMSSettings_UserSettings_Input').value = SettingTerms.DailyEmailMinute;
    document.getElementById('cmbHour_HourEmail_EmailSMSSettings_UserSettings_Input').value = SettingTerms.HourlyEmailHour;
    document.getElementById('cmbMinute_HourEmail_EmailSMSSettings_UserSettings_Input').value = SettingTerms.HourlyEmailMinute;

    ObjEmailSMSSettings_UserSettings.DailySMSDay = SettingTerms.DailySMSDay;
    ObjEmailSMSSettings_UserSettings.DailySMSHour = SettingTerms.DailySMSHour;
    ObjEmailSMSSettings_UserSettings.DailySMSMinute = SettingTerms.DailySMSMinute;
    ObjEmailSMSSettings_UserSettings.HourlySMSHour = SettingTerms.HourlySMSHour;
    ObjEmailSMSSettings_UserSettings.HourlySMSMinute = SettingTerms.HourlySMSMinute;
    document.getElementById('cmbDay_DaySMS_EmailSMSSettings_UserSettings_Input').value = SettingTerms.DailySMSDay;
    document.getElementById('cmbHour_DaySMS_EmailSMSSettings_UserSettings_Input').value = SettingTerms.DailySMSHour;
    document.getElementById('cmbMinute_DaySMS_EmailSMSSettings_UserSettings_Input').value = SettingTerms.DailySMSMinute;
    document.getElementById('cmbHour_HourSMS_EmailSMSSettings_UserSettings_Input').value = SettingTerms.HourlySMSHour;
    document.getElementById('cmbMinute_HourSMS_EmailSMSSettings_UserSettings_Input').value = SettingTerms.HourlySMSMinute;
}

function GetEmailSMSSettingsDefaultTime_UserSettings() {
    var ObjDefaultTime = document.getElementById('hfEmailSMSSettingsDefaultTime_UserSettings').value;
    ObjDefaultTime = eval('(' + ObjDefaultTime + ')');
    var strDailyTime = ObjDefaultTime.DailyTime;
    var strDailyTimeBatch = strDailyTime.split(':');
    var strHourlyTime = ObjDefaultTime.HourlyTime;
    var strHourlyTimeBatch = strHourlyTime.split(':');
    var ObjEmailSMSSettingsDefaultTime = new Object();
    ObjEmailSMSSettingsDefaultTime.DailyDay = ObjDefaultTime.DailyDay;
    ObjEmailSMSSettingsDefaultTime.DailyHour = strDailyTimeBatch[0];
    ObjEmailSMSSettingsDefaultTime.DailyMinute = strDailyTimeBatch[1];
    ObjEmailSMSSettingsDefaultTime.HourlyHour = strHourlyTimeBatch[0];
    ObjEmailSMSSettingsDefaultTime.HourlyMinute = strHourlyTimeBatch[1];
    return ObjEmailSMSSettingsDefaultTime;
}

function SetRelativeEmailSMSTransferChecked_UserSettings(TranferState, rdbDaily, rdbHourly) {
    switch (TranferState) {
        case 'Daily':
            rdbDaily.checked = true;
            break;
        case 'Hourly':
            rdbHourly.checked = true;
            break;
    }
}

function GetCheckedValue_UserSettings(strBool) {
    var bool = null;
    switch (strBool) {
        case 'true':
            bool = true;
            break;
        case 'false':
            bool = false;
            break;
    }
    return bool;
}

function ChangePersonnelCountState_UserSettings(state) {
    switch (state) {
        case "Single":
            document.getElementById('RdbSinglePersonel_UserSettings').checked = true;
            document.getElementById('RdbGroupPersonel_UserSettings').checked = false;
            PersonnelCountState_UserSettings = 'Single';
            break;
        case "Group":
            document.getElementById('RdbSinglePersonel_UserSettings').checked = false;
            document.getElementById('RdbGroupPersonel_UserSettings').checked = true;
            PersonnelCountState_UserSettings = 'Group';
            break;
    }
    SetSelectedPersonnelCount_UserSettings();
}

function tlbItemSave_TlbDashboardSettings_onClick() {
    SaveDashboardSettings_DashboardSettings_UserSettings();
}

function cmbDashboard1_DashboradSettings_UserSettings_onExpand(sender, e) {
    CollapseControls_DashboardSettings_UserSettings(cmbDashboard1_DashboradSettings_UserSettings);
    if (cmbDashboard1_DashboradSettings_UserSettings.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard1_DashboradSettings_UserSettings == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard1_DashboradSettings_UserSettings = true;
        Fill_cmbDashboard1_DashboradSettings_UserSettings();
    }
}

function Fill_cmbDashboard1_DashboradSettings_UserSettings() {
    ComboBox_onBeforeLoadData('cmbDashboard1_DashboradSettings_UserSettings');
    CallBack_cmbDashboard1_DashboradSettings_UserSettings.callback();
}

function CallBack_cmbDashboard1_DashboradSettings_UserSettings_onBeforeCallback(sender, e) {
    cmbDashboard1_DashboradSettings_UserSettings.dispose();
}

function CallBack_cmbDashboard1_DashboradSettings_UserSettings_onCallbackComplete(sender, e) {
    ChangeDirection_cmbDashBoard_DashboradSettings_UserSettings('cmbDashboard1_DashboradSettings_UserSettings');
    var error = document.getElementById('ErrorHiddenField_Dashboard1_DashboradSettings_UserSettings').value;
    if (error == "") {
        document.getElementById('cmbDashboard1_DashboradSettings_UserSettings_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDashboard1_DashboradSettings_UserSettings_DropImage').mousedown();
        cmbDashboard1_DashboradSettings_UserSettings.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDashboard1_DashboradSettings_UserSettings_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDashboard1_DashboradSettings_UserSettings_onCallbackError(sender, e) {
    ShowConnectionError_UserSettings();
}

function cmbDashboard2_DashboradSettings_UserSettings_onExpand(sender, e) {
    CollapseControls_DashboardSettings_UserSettings(cmbDashboard2_DashboradSettings_UserSettings);
    if (cmbDashboard2_DashboradSettings_UserSettings.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard2_DashboradSettings_UserSettings == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard2_DashboradSettings_UserSettings = true;
        Fill_cmbDashboard2_DashboradSettings_UserSettings();
    }
}

function Fill_cmbDashboard2_DashboradSettings_UserSettings() {
    ComboBox_onBeforeLoadData('cmbDashboard2_DashboradSettings_UserSettings');
    CallBack_cmbDashboard2_DashboradSettings_UserSettings.callback();
}

function CallBack_cmbDashboard2_DashboradSettings_UserSettings_onBeforeCallback(sender, e) {
    cmbDashboard2_DashboradSettings_UserSettings.dispose();
}

function CallBack_cmbDashboard2_DashboradSettings_UserSettings_onCallbackComplete(sender, e) {
    ChangeDirection_cmbDashBoard_DashboradSettings_UserSettings('cmbDashboard2_DashboradSettings_UserSettings');
    var error = document.getElementById('ErrorHiddenField_Dashboard2_DashboradSettings_UserSettings').value;
    if (error == "") {
        document.getElementById('cmbDashboard2_DashboradSettings_UserSettings_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDashboard2_DashboradSettings_UserSettings_DropImage').mousedown();
        cmbDashboard2_DashboradSettings_UserSettings.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDashboard2_DashboradSettings_UserSettings_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDashboard2_DashboradSettings_UserSettings_onCallbackError(sender, e) {
    ShowConnectionError_UserSettings();
}

function cmbDashboard3_DashboradSettings_UserSettings_onExpand(sender, e) {
    CollapseControls_DashboardSettings_UserSettings(cmbDashboard3_DashboradSettings_UserSettings);
    if (cmbDashboard3_DashboradSettings_UserSettings.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard3_DashboradSettings_UserSettings == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard3_DashboradSettings_UserSettings = true;
        Fill_cmbDashboard3_DashboradSettings_UserSettings();
    }
}

function Fill_cmbDashboard3_DashboradSettings_UserSettings() {
    ComboBox_onBeforeLoadData('cmbDashboard3_DashboradSettings_UserSettings');
    CallBack_cmbDashboard3_DashboradSettings_UserSettings.callback();
}

function CallBack_cmbDashboard3_DashboradSettings_UserSettings_onBeforeCallback(sender, e) {
    cmbDashboard3_DashboradSettings_UserSettings.dispose();
}

function CallBack_cmbDashboard3_DashboradSettings_UserSettings_onCallbackComplete(sender, e) {
    ChangeDirection_cmbDashBoard_DashboradSettings_UserSettings('cmbDashboard3_DashboradSettings_UserSettings');
    var error = document.getElementById('ErrorHiddenField_Dashboard3_DashboradSettings_UserSettings').value;
    if (error == "") {
        document.getElementById('cmbDashboard3_DashboradSettings_UserSettings_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDashboard3_DashboradSettings_UserSettings_DropImage').mousedown();
        cmbDashboard3_DashboradSettings_UserSettings.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDashboard3_DashboradSettings_UserSettings_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDashboard3_DashboradSettings_UserSettings_onCallbackError(sender, e) {
    ShowConnectionError_UserSettings();
}

function cmbDashboard4_DashboradSettings_UserSettings_onExpand(sender, e) {
    CollapseControls_DashboardSettings_UserSettings(cmbDashboard4_DashboradSettings_UserSettings);
    if (cmbDashboard4_DashboradSettings_UserSettings.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard4_DashboradSettings_UserSettings == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDashboard4_DashboradSettings_UserSettings = true;
        Fill_cmbDashboard4_DashboradSettings_UserSettings();
    }
}

function Fill_cmbDashboard4_DashboradSettings_UserSettings() {
    ComboBox_onBeforeLoadData('cmbDashboard4_DashboradSettings_UserSettings');
    CallBack_cmbDashboard4_DashboradSettings_UserSettings.callback();
}

function CallBack_cmbDashboard4_DashboradSettings_UserSettings_onBeforeCallback(sender, e) {
    cmbDashboard4_DashboradSettings_UserSettings.dispose();
}

function CallBack_cmbDashboard4_DashboradSettings_UserSettings_onCallbackComplete(sender, e) {
    ChangeDirection_cmbDashBoard_DashboradSettings_UserSettings('cmbDashboard4_DashboradSettings_UserSettings');
    var error = document.getElementById('ErrorHiddenField_Dashboard4_DashboradSettings_UserSettings').value;
    if (error == "") {
        document.getElementById('cmbDashboard4_DashboradSettings_UserSettings_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDashboard4_DashboradSettings_UserSettings_DropImage').mousedown();
        cmbDashboard4_DashboradSettings_UserSettings.expand();
    }
    else {
        var erroParts = eval('(' + error + ')');
        showDialog(erroParts[0], erroParts[1], erroParts[2]);
        document.getElementById('cmbDashboard4_DashboradSettings_UserSettings_DropDown').style.display = 'none';
    }
}

function CallBack_cmbDashboard4_DashboradSettings_UserSettings_onCallbackError(sender, e) {
    ShowConnectionError_UserSettings();
}


function SaveDashboardSettings_DashboardSettings_UserSettings() {
    UpdateSettings_UserSettings('Dashboard');
}

function cmbDashboard1_DashboradSettings_UserSettings_onCollapse(sender, e) {
    if (cmbDashboard1_DashboradSettings_UserSettings.getSelectedItem() == undefined && ObjDashboardSettings_UserSettings != null && ObjDashboardSettings_UserSettings.DashboardID1 != undefined && ObjDashboardSettings_UserSettings.DashboardID1 != null)
        document.getElementById('cmbDashboard1_DashboradSettings_UserSettings_Input').value = ObjDashboardSettings_UserSettings.DashboardTitle1;
}

function cmbDashboard2_DashboradSettings_UserSettings_onCollapse(sender, e) {
    if (cmbDashboard2_DashboradSettings_UserSettings.getSelectedItem() == undefined && ObjDashboardSettings_UserSettings != null && ObjDashboardSettings_UserSettings.DashboardID2 != undefined && ObjDashboardSettings_UserSettings.DashboardID2 != null)
        document.getElementById('cmbDashboard2_DashboradSettings_UserSettings_Input').value = ObjDashboardSettings_UserSettings.DashboardTitle2;
}

function cmbDashboard3_DashboradSettings_UserSettings_onCollapse(sender, e) {
    if (cmbDashboard3_DashboradSettings_UserSettings.getSelectedItem() == undefined && ObjDashboardSettings_UserSettings != null && ObjDashboardSettings_UserSettings.DashboardID3 != undefined && ObjDashboardSettings_UserSettings.DashboardID3 != null)
        document.getElementById('cmbDashboard3_DashboradSettings_UserSettings_Input').value = ObjDashboardSettings_UserSettings.DashboardTitle3;
}

function cmbDashboard4_DashboradSettings_UserSettings_onCollapse(sender, e) {
    if (cmbDashboard4_DashboradSettings_UserSettings.getSelectedItem() == undefined && ObjDashboardSettings_UserSettings != null && ObjDashboardSettings_UserSettings.DashboardID4 != undefined && ObjDashboardSettings_UserSettings.DashboardID4 != null)
        document.getElementById('cmbDashboard4_DashboradSettings_UserSettings_Input').value = ObjDashboardSettings_UserSettings.DashboardTitle4;
}

function ChangeDirection_cmbDashBoard_DashboradSettings_UserSettings(cmb) {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById(cmb + '_DropDown').dir = direction;
}


function txtPersonnelSearch_UserSettings_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbSearchPersonnel_UserSettings_onClick();
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

function tlbItemSave_MonthlyOperationReport_onClick() {

    ObjMonthlyOperationReportSchema_UserSetting = new Object();
    if (document.getElementById('rdbMonthlyOperationSchemaDefault_UserSettings').checked)
        ObjMonthlyOperationReportSchema_UserSetting.Schema = 'Default';
    if (document.getElementById('rdbMonthlyOperationGridSchema_UserSettings').checked)
        ObjMonthlyOperationReportSchema_UserSetting.Schema = 'Grid';
    if (document.getElementById('rdbMonthlyOperationGanttChartSchema_UserSettings').checked)
        ObjMonthlyOperationReportSchema_UserSetting.Schema = 'GanttChart';
    ChangeMonthlyOperationSchema_UserSettingsPage(CharToKeyCode_UserSettings(ObjMonthlyOperationReportSchema_UserSetting.Schema));
    DialogWaiting.Show();
}
function tlbItemSave_OperatorCollectiveRequestRegistType_UserSettings_onClick() {

    UpdateSettings_UserSettings('CollectiveRequestRegistType');
}
function ChangeMonthlyOperationSchema_UserSettingsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UserSettings').value;
            Response[1] = document.getElementById('hfConnectionError_UserSettings').value;
        }
        if (RetMessage[2] == 'success') {
            //DNN Note
            //this.parent.window.location = 'MainPage.aspx?reload=' + (new Date()).getTime();
            this.parent.window.location = '/';
        }
        else
            if (RetMessage[2] == 'error')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}






