
var ConfirmState_UpdateCalculationResult = null;
var box_SearchByPersonnel_UpdateCalculationResult_IsShown = false;
var LoadState_cmbPersonnel_UpdateCalculationResult = 'Normal';
var CurrentPageIndex_cmbPersonnel_UpdateCalculationResult = 0;
var SearchTerm_cmbPersonnel_UpdateCalculationResult = '';
var AdvancedSearchTerm_cmbPersonnel_UpdateCalculationResult = '';
var CurrentPageCombosCallBcakStateObj = new Object();
var CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult = 0;
var SettingsState_UpdateCalculationResult = 'View';
var CurrentPageState_UpdateCalculationResult = 'View';
var Basefooter_GridUpdateCalculationResult_UpdateCalculationResult = null;

function GetBoxesHeaders_UpdateCalculationResult() {
    parent.document.getElementById('Title_DialogUpdateCalculationResult').innerHTML = document.getElementById('hfTitle_DialogUpdateCalculationResult').value;
    document.getElementById('header_UpdateCalculationResult_UpdateCalculationResult').innerHTML = document.getElementById('hfheader_UpdateCalculationResult_UpdateCalculationResult').value;
    Basefooter_GridUpdateCalculationResult_UpdateCalculationResult = document.getElementById('footer_GridUpdateCalculationResult_UpdateCalculationResult').innerHTML = document.getElementById('hffooter_GridUpdateCalculationResult_UpdateCalculationResult').value;
    document.getElementById('header_GridSettings_UpdateCalculationResult').innerHTML = document.getElementById('hfheader_GridSettings_UpdateCalculationResult').value;
    document.getElementById('tdPersonnelCount_UpdateCalculationResult').innerHTML = document.getElementById('hfPersonnelCountTitle_UpdateCalculationResult').value + ' 0';
}

function tlbItemCalculationResultArchive_TlbUpdateCalculationResult_onClick() {
    ArchiveCalculationResult_UpdateCalculationResult(false);
}

function ArchiveCalculationResult_UpdateCalculationResult(IsForceArchiveCalculationResult) {
    if (cmbPersonnel_UpdateCalculationResult.get_itemCount() > 0) {
        var PersonnelLoadState = LoadState_cmbPersonnel_UpdateCalculationResult;
        var PersonnelSearchTerm = '';
        var PersonnelID = '0';
        if (cmbPersonnel_UpdateCalculationResult.getSelectedItem() != undefined)
            PersonnelID = cmbPersonnel_UpdateCalculationResult.getSelectedItem().get_id();
        switch (LoadState_cmbPersonnel_UpdateCalculationResult) {
            case 'Normal':
                PersonnelSearchTerm = '';
                break;
            case 'Search':
                PersonnelSearchTerm = document.getElementById('txtPersonnelSearch_UpdateCalculationResult').value;
                break;
            case 'AdvancedSearch':
                PersonnelSearchTerm = AdvancedSearchTerm_cmbPersonnel_UpdateCalculationResult;
                break;
        }
        var Year = document.getElementById('hfCurrentYear_UpdateCalculationResult').value;
        var Month = document.getElementById('hfCurrentMonth_UpdateCalculationResult').value;
        var IsForceOverrideArchiveCalculationResult = document.getElementById('chbOverrideCalculationResult_UpdateCalculationResult').checked;
        ArchiveCalculationResult_UpdateCalculationResultPage(CharToKeyCode_UpdateCalculationResult(Year), CharToKeyCode_UpdateCalculationResult(Month), CharToKeyCode_UpdateCalculationResult(PersonnelID), CharToKeyCode_UpdateCalculationResult(PersonnelLoadState), CharToKeyCode_UpdateCalculationResult(PersonnelSearchTerm), CharToKeyCode_UpdateCalculationResult(IsForceArchiveCalculationResult.toString()), CharToKeyCode_UpdateCalculationResult(IsForceOverrideArchiveCalculationResult.toString()));
        DialogWaiting.Show();
    }
}

function ArchiveCalculationResult_UpdateCalculationResultPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        switch (RetMessage[2]) {
            case 'success':
                showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
                break;
            case 'warning':
                if (RetMessage[3] == 'SomeExists' || RetMessage[3] == 'AllExists')
                    ShowDialogOverrideCalculationResult();
                break;
            case 'error':
                showDialog(RetMessage[0], RetMessage[1], RetMessage[2]);
                break;
        }
    }
}

function tlbItemOverrideCalculationResult_TlbOverrideCalculationResult_UpdateCalculationResult_onClick() {
    ArchiveCalculationResult_UpdateCalculationResult(true);
    DialogOverrideCalculationResult.Close();
}

function ShowDialogOverrideCalculationResult() {
    DialogOverrideCalculationResult.Show();
}

function tlbItemHelp_TlbUpdateCalculationResult_onClick() {
    LoadHelpPage('tlbItemHelp_TlbUpdateCalculationResult');
}

function tlbItemFormReconstruction_TlbUpdateCalculationResult_onClick() {
    CloseDialogUpdateCalculationResult();
    parent.DialogUpdateCalculationResult.Show();
}

function CloseDialogUpdateCalculationResult() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogUpdateCalculationResult_IFrame').src =parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogUpdateCalculationResult').Close();
}

function tlbItemExit_TlbUpdateCalculationResult_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_UpdateCalculationResult = confirmState;
    switch (confirmState) {
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_UpdateCalculationResult').value;
            break;
    }
    DialogConfirm.Show();
    CollapseControls_UpdateCalculationResult(null);
}

function CollapseControls_UpdateCalculationResult(exception) {
    if (exception == null || exception != cmbPersonnel_UpdateCalculationResult)
        cmbPersonnel_UpdateCalculationResult.collapse();
    if (exception == null || exception != cmbYear_UpdateCalculationResult)
        cmbYear_UpdateCalculationResult.collapse();
    if (exception == null || exception != cmbMonth_UpdateCalculationResult)
        cmbMonth_UpdateCalculationResult.collapse();
}

function imgbox_SearchByPersonnel_UpdateCalculationResult_onClick() {
    CollapseControls_UpdateCalculationResult(null);
    setSlideDownSpeed(200);
    slidedown_showHide('box_SearchByPersonnel_UpdateCalculationResult');

    if (box_SearchByPersonnel_UpdateCalculationResult_IsShown) {
        box_SearchByPersonnel_UpdateCalculationResult_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel_UpdateCalculationResult').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_SearchByPersonnel_UpdateCalculationResult_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel_UpdateCalculationResult').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick() {
    Refresh_cmbPersonnel_UpdateCalculationResult();
}

function Refresh_cmbPersonnel_UpdateCalculationResult() {
    LoadState_cmbPersonnel_UpdateCalculationResult = 'Normal';
    SetPageIndex_cmbPersonnel_UpdateCalculationResult(0);
}

function SetPageIndex_cmbPersonnel_UpdateCalculationResult(pageIndex) {
    CurrentPageIndex_cmbPersonnel_UpdateCalculationResult = pageIndex;
    Fill_cmbPersonnel_UpdateCalculationResult(pageIndex);
}

function Fill_cmbPersonnel_UpdateCalculationResult(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_UpdateCalculationResult').value);
    var SearchTermConditions = '';
    switch (LoadState_cmbPersonnel_UpdateCalculationResult) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm_cmbPersonnel_UpdateCalculationResult = SearchTermConditions = document.getElementById('txtPersonnelSearch_UpdateCalculationResult').value;
            break;
        case 'AdvancedSearch':
            SearchTermConditions = AdvancedSearchTerm_cmbPersonnel_UpdateCalculationResult;
            break;
    }
    ComboBox_onBeforeLoadData('cmbPersonnel_UpdateCalculationResult');
    CallBack_cmbPersonnel_UpdateCalculationResult.callback(CharToKeyCode_UpdateCalculationResult(LoadState_cmbPersonnel_UpdateCalculationResult), CharToKeyCode_UpdateCalculationResult(pageSize.toString()), CharToKeyCode_UpdateCalculationResult(pageIndex.toString()), CharToKeyCode_UpdateCalculationResult(SearchTermConditions));
}

function CharToKeyCode_UpdateCalculationResult(str) {
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

function tlbItemFirst_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick() {
    SetPageIndex_cmbPersonnel_UpdateCalculationResult(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick() {
    if (CurrentPageIndex_cmbPersonnel_UpdateCalculationResult != 0) {
        CurrentPageIndex_cmbPersonnel_UpdateCalculationResult = CurrentPageIndex_cmbPersonnel_UpdateCalculationResult - 1;
        SetPageIndex_cmbPersonnel_UpdateCalculationResult(CurrentPageIndex_cmbPersonnel_UpdateCalculationResult);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick() {
    if (CurrentPageIndex_cmbPersonnel_UpdateCalculationResult < parseInt(document.getElementById('hfPersonnelPageCount_UpdateCalculationResult').value) - 1) {
        CurrentPageIndex_cmbPersonnel_UpdateCalculationResult = CurrentPageIndex_cmbPersonnel_UpdateCalculationResult + 1;
        SetPageIndex_cmbPersonnel_UpdateCalculationResult(CurrentPageIndex_cmbPersonnel_UpdateCalculationResult);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick() {
    SetPageIndex_cmbPersonnel_UpdateCalculationResult(parseInt(document.getElementById('hfPersonnelPageCount_UpdateCalculationResult').value) - 1);
}

function cmbPersonnel_UpdateCalculationResult_onExpand(sender, e) {
    CollapseControls_UpdateCalculationResult(cmbPersonnel_UpdateCalculationResult);
    SetPosition_cmbPersonnel_UpdateCalculationResult();
    if (cmbPersonnel_UpdateCalculationResult.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_UpdateCalculationResult == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_UpdateCalculationResult = true;
        SetPageIndex_cmbPersonnel_UpdateCalculationResult(0);
    }
}

function SetPosition_cmbPersonnel_UpdateCalculationResult() {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('cmbPersonnel_UpdateCalculationResult_DropDown').style.left = document.getElementById('Mastertbl_UpdateCalculationResult').clientWidth - 400 + 'px';
    }
    if (parent.CurrentLangID == 'en-US') {
        document.getElementById('cmbPersonnel_UpdateCalculationResult_DropDown').style.left = '30px';
    }
}

function CallBack_cmbPersonnel_UpdateCalculationResult_onBeforeCallback(sender, e) {
    cmbPersonnel_UpdateCalculationResult.dispose();
}

function CallBack_cmbPersonnel_UpdateCalculationResult_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_UpdateCalculationResult').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_UpdateCalculationResult').value;
    document.getElementById('clmnBarCode_cmbPersonnel_UpdateCalculationResult').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_UpdateCalculationResult').value;
    document.getElementById('clmnCardNum_cmbPersonnel_UpdateCalculationResult').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_UpdateCalculationResult').value;

    SetPosition_cmbPersonnel_UpdateCalculationResult();

    var error = document.getElementById('ErrorHiddenField_Personnel_UpdateCalculationResult').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_UpdateCalculationResult_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_UpdateCalculationResult_DropImage').mousedown();
        else
            cmbPersonnel_UpdateCalculationResult.expand();
        var personnelCount = document.getElementById('hfPersonnelCount_UpdateCalculationResult').value;
        document.getElementById('tdPersonnelCount_UpdateCalculationResult').innerHTML = document.getElementById('hfPersonnelCountTitle_UpdateCalculationResult').value + ' ' + personnelCount;
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_UpdateCalculationResult_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function CallBack_cmbPersonnel_UpdateCalculationResult_onCallbackError(sender, e) {
    ShowConnectionError_UpdateCalculationResult();
}

function ShowConnectionError_UpdateCalculationResult() {
    var error = document.getElementById('hfErrorType_UpdateCalculationResult').value;
    var errorBody = document.getElementById('hfConnectionError_UpdateCalculationResult').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemSearch_TlbSearchPersonnel_UpdateCalculationResult_onClick() {
    LoadState_cmbPersonnel_UpdateCalculationResult = 'Search';
    SetPageIndex_cmbPersonnel_UpdateCalculationResult(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_UpdateCalculationResult_onClick() {
    LoadState_cmbPersonnel_UpdateCalculationResult = 'AdvancedSearch';
    ShowDialogPersonnelSearch('UpdateCalculationResult');
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_UpdateCalculationResult(null);
}

function UpdateCalculationResult_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_UpdateCalculationResult = SearchTerm;
    SetPageIndex_cmbPersonnel_UpdateCalculationResult(0);
}

function cmbYear_UpdateCalculationResult_onChange(sender, e) {
    document.getElementById('hfCurrentYear_UpdateCalculationResult').value = cmbYear_UpdateCalculationResult.getSelectedItem().get_value();
}

function cmbMonth_UpdateCalculationResult_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_UpdateCalculationResult').value = cmbMonth_UpdateCalculationResult.getSelectedItem().get_value();
}

function tlbItemView_TlbView_UpdateCalculationResult_onClick() {
    SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(0);
}

function SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(pageIndex) {
    CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult = pageIndex;
    Fill_GridUpdateCalculationResult_UpdateCalculationResult(pageIndex);
}

function Fill_GridUpdateCalculationResult_UpdateCalculationResult(pageIndex) {
    document.getElementById('loadingPanel_GridUpdateCalculationResult_UpdateCalculationResult').innerHTML =GetLoadingMessage(document.getElementById('hfloadingPanel_GridUpdateCalculationResult_UpdateCalculationResult').value);
    var pageSize = parseInt(document.getElementById('hfCalculationResultPageSize_UpdateCalculationResult').value);
    var PersonnelLoadState = LoadState_cmbPersonnel_UpdateCalculationResult;
    var PersonnelSearchTerm = '';
    switch (LoadState_cmbPersonnel_UpdateCalculationResult) {
        case 'Normal':
            PersonnelSearchTerm = '';
            break;
        case 'Search':
            PersonnelSearchTerm = document.getElementById('txtPersonnelSearch_UpdateCalculationResult').value;
            break;
        case 'AdvancedSearch':
            PersonnelSearchTerm = AdvancedSearchTerm_cmbPersonnel_UpdateCalculationResult;
            break;
    }
    var Year = document.getElementById('hfCurrentYear_UpdateCalculationResult').value;
    var Month = document.getElementById('hfCurrentMonth_UpdateCalculationResult').value;

    CallBack_GridUpdateCalculationResult_UpdateCalculationResult.callback(CharToKeyCode_UpdateCalculationResult(Year), CharToKeyCode_UpdateCalculationResult(Month), CharToKeyCode_UpdateCalculationResult(pageSize.toString()), CharToKeyCode_UpdateCalculationResult(pageIndex.toString()), CharToKeyCode_UpdateCalculationResult(PersonnelLoadState), CharToKeyCode_UpdateCalculationResult(PersonnelSearchTerm));
}

function EditGridUpdateCalculationResult_UpdateCalculationResult(rowID) {
    CurrentPageState_UpdateCalculationResult = 'Edit';
    GridUpdateCalculationResult_UpdateCalculationResult.edit(GridUpdateCalculationResult_UpdateCalculationResult.getItemFromClientId(rowID));
}

function SetCellTitle_GridUpdateCalculationResult_UpdateCalculationResult(state) {
    return document.getElementById('hf' + state + '_UpdateCalculationResult').value;
}

function UpdateGridUpdateCalculationResult_UpdateCalculationResult() {
    GridUpdateCalculationResult_UpdateCalculationResult.editComplete();
    //UpdateCalculationResult_UpdateCalculationResult();
}

function UpdateCalculationResult_UpdateCalculationResult() {
    var SelectedItems_UpdateCalculationResult_UpdateCalculationResult = GridUpdateCalculationResult_UpdateCalculationResult.getSelectedItems();
    if (SelectedItems_UpdateCalculationResult_UpdateCalculationResult.length > 0) {
        ObjUpdateCalculationResult_UpdateCalculationResult = new Object();
        ObjUpdateCalculationResult_UpdateCalculationResult.ID = SelectedItems_UpdateCalculationResult_UpdateCalculationResult[0].getMember('ID').get_text();
        var PersonnelID = SelectedItems_UpdateCalculationResult_UpdateCalculationResult[0].getMember('PersonId').get_text();
        var Year = document.getElementById('hfCurrentYear_UpdateCalculationResult').value;
        var Month = document.getElementById('hfCurrentMonth_UpdateCalculationResult').value;
        var StrFieldsValCol = '';
        var ArFieldsValCol = {};
        if (CurrentPageState_UpdateCalculationResult == 'Edit') {
            var ColumnsCol_UpdateCalculationResult_UpdateCalculationResult = GridUpdateCalculationResult_UpdateCalculationResult.get_table().get_columns();
            for (var i = 5; i < ColumnsCol_UpdateCalculationResult_UpdateCalculationResult.length - 1; i++) {
                var gridColumn_UpdateCalculationResult_UpdateCalculationResult = ColumnsCol_UpdateCalculationResult_UpdateCalculationResult[i];
                if (gridColumn_UpdateCalculationResult_UpdateCalculationResult.get_visible()) {
                    var FieldVal = SelectedItems_UpdateCalculationResult_UpdateCalculationResult[0].getMember(gridColumn_UpdateCalculationResult_UpdateCalculationResult.get_dataField()).get_text();
                    ArFieldsValCol[ColumnsCol_UpdateCalculationResult_UpdateCalculationResult[i].get_dataField()] = FieldVal;
                }
            }
            ArFieldsValCol['ID'] = SelectedItems_UpdateCalculationResult_UpdateCalculationResult[0].getMember('ID').get_text();
            StrFieldsValCol = JSON.stringify(ArFieldsValCol);
        }
        
        UpdateCalculationResult_UpdateCalculationResultPage(CharToKeyCode_UpdateCalculationResult(CurrentPageState_UpdateCalculationResult), CharToKeyCode_UpdateCalculationResult(PersonnelID), CharToKeyCode_UpdateCalculationResult(Year), CharToKeyCode_UpdateCalculationResult(Month), CharToKeyCode_UpdateCalculationResult(StrFieldsValCol));
        DialogWaiting.Show();
    }
}

function UpdateCalculationResult_UpdateCalculationResultPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {

            var rowGridUpdateCalculationResult_UpdateCalculationResult = eval('(' + RetMessage[3] + ')');
            UpdateCalculationResult_onAfterUpdate(rowGridUpdateCalculationResult_UpdateCalculationResult);
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}


function UpdateCalculationResult_onAfterUpdate(rowUpdated) {
    GridUpdateCalculationResult_UpdateCalculationResult.beginUpdate();
    GridUpdateCalculationResult_UpdateCalculationResult.selectByKey(rowUpdated.ID, 0, false);
    archiveItem = GridUpdateCalculationResult_UpdateCalculationResult.getItemFromKey(0, rowUpdated.ID);

    archiveItem.setValue(6, rowUpdated.P2, false);
    archiveItem.setValue(7, rowUpdated.P3, false);
    archiveItem.setValue(8, rowUpdated.P4, false);
    archiveItem.setValue(9, rowUpdated.P5, false);
    archiveItem.setValue(10, rowUpdated.P6, false);
    archiveItem.setValue(11, rowUpdated.P7, false);
    archiveItem.setValue(12, rowUpdated.P8, false);
    archiveItem.setValue(15, rowUpdated.P11, false);
    archiveItem.setValue(16, rowUpdated.P12, false);
    GridUpdateCalculationResult_UpdateCalculationResult.endUpdate();
}

function GridUpdateCalculationResult_UpdateCalculationResult_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUpdateCalculationResult_UpdateCalculationResult').innerHTML = '';
}

function CallBack_GridUpdateCalculationResult_UpdateCalculationResult_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UpdateCalculationResult').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        Changefooter_GridUpdateCalculationResult_UpdateCalculationResult();
    }
}

function Changefooter_GridUpdateCalculationResult_UpdateCalculationResult() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridUpdateCalculationResult_UpdateCalculationResult').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfCalculationResultPageCount_UpdateCalculationResult').value) > 0 ? CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfCalculationResultPageCount_UpdateCalculationResult').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridUpdateCalculationResult_UpdateCalculationResult').innerHTML = retfooterVal;
}


function CallBack_GridUpdateCalculationResult_UpdateCalculationResult_onCallbackError(sender, e) {
}

function tlbItemRefresh_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick() {
    document.getElementById('loadingPanel_GridUpdateCalculationResult_UpdateCalculationResult').innerHTML = '';
    SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(0);
}

function tlbItemFirst_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick() {
    SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(0);
}

function tlbItemBefore_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick() {
    if (CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult != 0) {
        CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult = CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult - 1;
        SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult);
    }
}

function tlbItemNext_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick() {
    if (CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult < parseInt(document.getElementById('hfCalculationResultPageCount_UpdateCalculationResult').value) - 1) {
        CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult = CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult + 1;
        SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(CurrentPageIndex_GridUpdateCalculationResult_UpdateCalculationResult);
    }
}

function tlbItemLast_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick() {
    SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(parseInt(document.getElementById('hfCalculationResultPageCount_UpdateCalculationResult').value) - 1);
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_UpdateCalculationResult) {
        case 'Exit':
            CloseDialogUpdateCalculationResult();
            break;
        case 'ArchiveCalculationResult':
            ArchiveCalculationResult_UpdateCalculationResult(true);
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemSave_TlbGridSettings_onClick() {
    GridSettings_UpdateCalculationResult_onSave();
}

function GridSettings_UpdateCalculationResult_onSave() {
    SettingsState_UpdateCalculationResult = 'Save';
    CallBack_GridSettings_UpdateCalculationResult.callback('Set', CreateColumnsArray_GridSettings_UpdateCalculationResult());
}

function CreateColumnsArray_GridSettings_UpdateCalculationResult() {
    var ArColumns = '';
    for (var i = 0; i < GridSettings_UpdateCalculationResult.get_table().getRowCount() ; i++) {
        var Splitter = ':';
        if (i == GridSettings_UpdateCalculationResult.get_table().getRowCount() - 1)
            Splitter = '';
        ArColumns = ArColumns + CharToKeyCode_UpdateCalculationResult(GridSettings_UpdateCalculationResult.get_table().getRow(i).getMember('PId').get_text()) + '%' + GridSettings_UpdateCalculationResult.get_table().getRow(i).getMember('Visible').get_text() + Splitter;
    }
    return ArColumns;
}


function tlbItemExit_TlbGridSettings_onClick() {
    DialogGridSettings.Close();
}

function chbSelectAll_GridSettings_UpdateCalculationResult_onClick() {
    var Checked = null;
    if (document.UpdateCalculationResultForm.chbSelectAll_GridSettings_UpdateCalculationResult.checked)
        Checked = true;
    else
        Checked = false;

    GridSettings_UpdateCalculationResult.beginUpdate();
    for (var i = 0; i < GridSettings_UpdateCalculationResult.get_table().getRowCount() ; i++) {
        GridSettings_UpdateCalculationResult.get_table().getRow(i).setValue(2, Checked, false);
    }
    GridSettings_UpdateCalculationResult.endUpdate();
}

function CallBack_GridSettings_UpdateCalculationResult_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GridSettings_UpdateCalculationResult').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        if (SettingsState_UpdateCalculationResult == 'Save') {
            DialogGridSettings.Close();
            parent.DialogUpdateCalculationResult.Close();
            parent.DialogUpdateCalculationResult.Show();
        }
    }
}

function CallBack_GridSettings_UpdateCalculationResult_onCallbackError(sender, e) {
    ShowConnectionError_UpdateCalculationResult();
}

function tlbItemGridSettings_TlbUpdateCalculationResult_onClick() {
    ShowDialogGridSettings();
}

function ShowDialogGridSettings() {
    SettingsState_UpdateCalculationResult = 'View';
    document.UpdateCalculationResultForm.chbSelectAll_GridSettings_UpdateCalculationResult.checked = false;
    CallBack_GridSettings_UpdateCalculationResult.callback('Get', '');
    DialogGridSettings.Show();
    CollapseControls_UpdateCalculationResult();
}

function cmbPersonnel_UpdateCalculationResult_onChange(sender, e) {
    if (cmbPersonnel_UpdateCalculationResult.getSelectedItem() != undefined)
        document.getElementById('tdPersonnelCount_UpdateCalculationResult').innerHTML = document.getElementById('hfPersonnelCountTitle_UpdateCalculationResult').value + ' 1';
}

function ChangeDirection_Container_GridUpdateCalculationResult_UpdateCalculationResult() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('Container_GridUpdateCalculationResult_UpdateCalculationResult').style.direction = 'ltr';
}

function GridUpdateCalculationResult_UpdateCalculationResult_onItemUpdate(sender, e) {
    UpdateCalculationResult_UpdateCalculationResult();
}

function txtPersonnelSearch_UpdateCalculationResult_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbSearchPersonnel_UpdateCalculationResult_onClick();
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

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}













