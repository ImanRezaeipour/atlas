
var LastPageIndex_GridKartable_Kartable = 0;
var CurrentPageIndex_GridKartable_Kartable = 0;
var LoadState_Kartable = null;
var ConfirmState_Kartable = null;
var CurrentPageState_Kartable = 'View';
var StrSelectedRequests_Kartable = '#';
var ObjKartable_Kartable = null;
var StrFilterConditions_Kartable = '';
var StateRequest_Kartable = null;
var CurrentPageTreeViewsObj = new Object();
var AccordingToView_Kartable = 'YearMonth';


function GetDefaultLoadState_Kartable() {
    var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
    switch (RequestCaller) {
        case 'Kartable':
            LoadState_Kartable = 'None';
            break;
        case 'Survey':
            LoadState_Kartable = 'UnKnown';
            break;
        case 'Sentry':
            LoadState_Kartable = 'None';
            break;
        case 'SpecialKartable':
            LoadState_Kartable = 'None';
            break;
        case 'RequestSubstituteKartable':
            LoadState_Kartable = 'None';
            break;
    }
    return LoadState_Kartable;
}
function NonViewItemInKartabl_Kartable() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var Applicant = ObjDialogKartable.Applicant;
}
function GetBoxesHeaders_Kartable() {
    var TitleDialog_Kartable = '';
    var HeaderGrid_Kartable = '';
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    switch (RequestCaller) {
        case 'Kartable':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogKartable').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_Kartable_Kartable').value;
            document.getElementById('header_GridSettings_Kartabl').innerHTML = document.getElementById('hfheader_GridSettings_Kartabl').value;
            break;
        case 'Survey':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogSurveyedRequests').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_SurveyedRequests_Kartable').value;
            document.getElementById('header_GridSettings_Kartabl').innerHTML = document.getElementById('hfheader_GridSettings_Kartabl').value;
            break;
        case 'Sentry':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogSentry').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_SentryKartable_Kartable').value;
            break;
        case 'SpecialKartable':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogSpecialKartable').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_SpecialKartable_Kartable').value;
            document.getElementById('header_GridSettings_Kartabl').innerHTML = document.getElementById('hfheader_GridSettings_Kartabl').value;
            break;
        case 'RequestSubstituteKartable':
            TitleDialog_Kartable = document.getElementById('hfTitle_DialogRequestSubstituteKartable').value;
            HeaderGrid_Kartable = document.getElementById('hfheader_SpecialRequestSubstituteKartable_Kartable').value;
            document.getElementById('header_GridSettings_Kartabl').innerHTML = document.getElementById('hfheader_GridSettings_Kartabl').value;
            break;
    }
    parent.document.getElementById('Title_DialogKartable').innerHTML = TitleDialog_Kartable;
    document.getElementById('header_Kartable_Kartable').innerHTML = HeaderGrid_Kartable;
    document.getElementById('beginfooter_GridKartable_Kartable').innerHTML = document.getElementById('endfooter_GridKartable_Kartable').innerHTML = document.getElementById('hffooter_GridKartable_Kartable').value;
}

function ChangeDirection_cmbControls_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var Applicant = ObjDialogKartable.Applicant;   
    if ((RequestCaller == 'Kartable' || RequestCaller == 'Survey' || RequestCaller == 'SpecialKartable' || RequestCaller == 'RequestSubstituteKartable') && Applicant != 'PrivateNews') {
        ChangeComboDirection_MasterMonthlyOperation('cmbYear_Kartable');
        ChangeComboDirection_MasterMonthlyOperation('cmbMonth_Kartable');
    }
    ChangeComboDirection_MasterMonthlyOperation('cmbSortBy_Kartable');
}

function ChangeDateControlContainersWidth_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    if (RequestCaller == 'Kartable' || RequestCaller == 'Survey' || RequestCaller == 'SpecialKartable' || RequestCaller == 'RequestSubstituteKartable') {
        //document.getElementById('Container_DateCalendars_RequestRegister').style.width = '0px';
    }
    if (RequestCaller == 'Sentry') {
    }
}

function ShowDialogKartableFilter_Kartable() {
    DialogKartableFilter.Show();
    CollapseControls_Kartable();
}

function ShowDialogHistory_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        var ObjHistory = new Object();
        ObjHistory.RequestID = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestID').get_text();
        ObjHistory.RequestIssuer = GridKartable_Kartable.getSelectedItems()[0].getMember('Applicant').get_text();
        ObjHistory.RequestTitle = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestTitle').get_text();
        DialogHistory.set_value(ObjHistory);
        DialogHistory.Show();
        CollapseControls_Kartable();
    }
}

function ShowDialogEndorsementFlowState_Kartable(isForceWithoutManagerFlow) {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        var ObjDialogKartable = parent.DialogKartable.get_value();
        var ObjEndorsementFlowState = new Object();
        ObjEndorsementFlowState.ManagerFlowID = !isForceWithoutManagerFlow ? GridKartable_Kartable.getSelectedItems()[0].getMember('ManagerFlowID').get_text() : '0';
        ObjEndorsementFlowState.RequestID = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestID').get_text();
        ObjEndorsementFlowState.PersonnelID = GridKartable_Kartable.getSelectedItems()[0].getMember('PersonId').get_text();
        ObjEndorsementFlowState.RequestSubstituteID = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestSubstituteID').get_text();
        ObjEndorsementFlowState.RequestSubstituteConfirm = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestSubstituteConfirm').get_value();
        ObjEndorsementFlowState.Caller = ObjDialogKartable.RequestCaller;
        parent.DialogEndorsementFlowState.set_value(ObjEndorsementFlowState);
        parent.DialogEndorsementFlowState.Show();
        CollapseControls_Kartable();
    }
}

function ShowDialogRequestRejectDescription_Kartable(state) {
    var description = null;
    StateRequest_Kartable = state;
    switch (state) {
        case 'Reject':
            description = document.getElementById('hfRequestRejectDescription_Kartable').value;
            break;
        case 'Delete':
            description = document.getElementById('hfRequestDeleteDescription_Kartable').value;
            break;
    }
    document.getElementById('hfDescription_RequestReject_Kartable').innerHTML = description;
    DialogRequestRejectDescription.Show();
    CollapseControls_Kartable();
}

function SetHorizontalScrollingDirection_GridKartable_Kartable_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridKartable_Kartable').style.direction = "ltr";
}

function ChangeDirection_Mastertbl_KartableForm() {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById('Mastertbl_KartableForm').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('Mastertbl_KartableForm').dir = 'rtl';
    }
}

function ChangeDirection_GridKartable_Kartable() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('Container_GridKartable_Kartable').style.direction = 'ltr';
}


function DialogRequestRejectDescription_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('tbl_RequestRejectDescription_Kartable').dir = 'rtl';
    }
}

function ShowDialogRequestsState() {
    DialogRequestsState.Show();
    CollapseControls_Kartable();
}

function GridKartable_Kartable_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridKartable_Kartable').innerHTML = '';
    BaseCallBackPrefix_GridKartable_Kartable = GridKartable_Kartable.CallbackPrefix;
}

function CallBack_GridKartable_Kartable_onCallbackComplete(sender, e) {
    SetHorizontalScrollingDirection_GridKartable_Kartable_Opera();
    GridKartable_Kartable.render();
    parent.DialogLoading.Close();
    var error = document.getElementById('ErrorHiddenField_Kartable').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridKartable_Kartable(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('Mastertbl_KartableForm').offsetWidth);
    }
    else
        Changefooter_GridKartable_Kartable();
}
function Changefooter_GridKartable_Kartable() {
    var retfooterVal = '';
    var footerVal = document.getElementById('beginfooter_GridKartable_Kartable').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfKartablePageCount_Kartable').value) > 0 ? CurrentPageIndex_GridKartable_Kartable + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfKartablePageCount_Kartable').value;
        if ((i == 1 || i == 3) && GridKartable_Kartable.get_table().getRowCount() == 0)
            footerValCol[i] = 0;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('beginfooter_GridKartable_Kartable').innerHTML = document.getElementById('endfooter_GridKartable_Kartable').innerHTML = retfooterVal;
    document.getElementById('beginfooter_GridKartable_Kartable').innerHTML = document.getElementById('hfRequestCount_GridKartabl_Kartabl').value + document.getElementById('hfKartableCount_Kartable').value;
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Kartable) {
        case 'PageChange':
            StrSelectedRequests_Kartable = '#';
            Fill_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable, AccordingToView_Kartable);
            break;
        case 'Reject':
            DialogConfirm.Close();
            ShowDialogRequestRejectDescription_Kartable('Reject');
            break;
        case 'Exit':
            DialogKartable_onClose();
            break;
        default:
    }
    DialogConfirm.Close();
}

function DialogKartable_onClose() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogKartable_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogKartable').Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemEndorsement_TlbKartable_onClick() {
    UpdateKartable_Kartable("Confirmed");
}

function tlbItemReject_TlbKartable_onClick() {
    ShowDialogRequestRejectDescription_Kartable('Reject');
}

function tlbItemDelete_TlbKartable_onClick() {
    ShowDialogRequestRejectDescription_Kartable('Delete');
}

function UpdateKartable_Kartable(CurrentPageState) {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    ObjKartable_Kartable = new Object();
    ObjKartable_Kartable.ManagerFlowID = '0';
    if (GridKartable_Kartable.getSelectedItems().length > 0)
        ObjKartable_Kartable.ManagerFlowID = GridKartable_Kartable.getSelectedItems()[0].getMember('ManagerFlowID').get_text();
    switch (CurrentPageState) {
        case 'Confirmed':
            ObjKartable_Kartable.ActionDescription = "";
            break;
        case 'Unconfirmed':
            ObjKartable_Kartable.ActionDescription = document.getElementById('txtDescription_RequestReject_Kartable').value;
            break;
        case 'Deleted':
            ObjKartable_Kartable.ActionDescription = document.getElementById('txtDescription_RequestReject_Kartable').value;
            if (RequestCaller != 'SpecialKartable') {
                var SelectedItems_GridKartable_Kartable = GridKartable_Kartable.getSelectedItems();
                if (SelectedItems_GridKartable_Kartable.length > 0) {
                    if (StrSelectedRequests_Kartable != '#')
                        StrSelectedRequests_Kartable = '#';
                    ChangeRequestsList_Kartable(SelectedItems_GridKartable_Kartable[0]);
                }
            }
    }
    ObjKartable_Kartable.StrSelectedRequests = StrSelectedRequests_Kartable;
    UpdateKartable_KartablePage(CharToKeyCode_Kartable(RequestCaller), CharToKeyCode_Kartable(CurrentPageState), CharToKeyCode_Kartable(ObjKartable_Kartable.StrSelectedRequests), CharToKeyCode_Kartable(ObjKartable_Kartable.ActionDescription));
    DialogWaiting.Show();
}

function UpdateKartable_KartablePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Kartable').value;
            Response[1] = document.getElementById('hfConnectionError_Kartable').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2], false, document.getElementById('Mastertbl_KartableForm').offsetWidth);
        if (RetMessage[2] == 'success' || RetMessage[2] == 'warning') {
            //Kartable_OnAfterUpdate(Response);
            //UpdateFeatures_GridKartable_Kartable();
            Kartable_OnAfterUpdate();
        }
    }
}

function Kartable_OnAfterUpdate() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    ClearList_Kartable(RequestCaller);
    CurrentPageIndex_GridKartable_Kartable = 0;
    Fill_GridKartable_Kartable(0, AccordingToView_Kartable);
}

//function Kartable_OnAfterUpdate(Response) {
//    var ObjDialogKartable = parent.DialogKartable.get_value();
//    var RequestCaller = ObjDialogKartable.RequestCaller;
//    switch (RequestCaller) {
//        case 'Kartable':
//            var strSelectedRequests = ObjKartable_Kartable.StrSelectedRequests;
//            GridKartable_Kartable.beginUpdate();
//            for (var i = 0; i < GridKartable_Kartable.get_table().getRowCount(); i++) {
//                var gridItem = GridKartable_Kartable.get_table().getRow(i);
//                requestID = gridItem.getMember('RequestID').get_text();
//                if (strSelectedRequests.indexOf('#' + requestID + '#') >= 0)
//                    GridKartable_Kartable.deleteItem(gridItem);
//            }
//            GridKartable_Kartable.endUpdate();
//            break;
//        case 'Survey':
//            break;
//        case 'Sentry':
//            break;
//    }
//    ClearList_Kartable(RequestCaller);
//}

function ClearList_Kartable(RequestCaller) {
    StrSelectedRequests_Kartable = '#';
    document.getElementById('txtDescription_RequestReject_Kartable').value = '';
    if (RequestCaller == 'Kartable' || RequestCaller == 'SpecialKartable' || RequestCaller == 'RequestSubstituteKartable')
        document.getElementById('chbSelectAllinthisPage_Kartable').checked = false;
}

function UpdateFeatures_GridKartable_Kartable() {
    var KartableCount = parseInt(document.getElementById('hfKartableCount_Kartable').value);
    var KartablePageCount = parseInt(document.getElementById('hfKartablePageCount_Kartable').value);
    var KartablePageSize = parseInt(document.getElementById('hfKartablePageSize_Kartable').value);
    if (KartableCount > 0) {
        KartableCount = KartableCount - 1;
        var divRem = mod(KartableCount, KartablePageSize);
        if (divRem == 0) {
            KartablePageCount = KartablePageCount - 1;
            if (CurrentPageIndex_GridKartable_Kartable == KartablePageCount)
                CurrentPageIndex_GridKartable_Kartable = CurrentPageIndex_GridKartable_Kartable - 1;
        }
        SetPageIndex_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable);
        document.getElementById('hfKartableCount_Kartable').value = KartableCount.toString();
        document.getElementById('hfKartablePageCount_Kartable').value = KartablePageCount.toString();
        Changefooter_GridKartable_Kartable();
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function tlbItemHistory_TlbKartable_onClick() {
    ShowDialogHistory_Kartable();
}

function tlbItemFilter_TlbKartable_onClick() {
    ShowDialogKartableFilter_Kartable();
}

function Kartable_OnAfterCustomFilter(StrCustomFilter) {
    StrFilterConditions_Kartable = StrCustomFilter;
    ChangeLoadState_GridKartable_Kartable('CustomFilter');
}

function tlbItemSearch_TlbKartableQuickSearch_onClick() {
    StrFilterConditions_Kartable = document.getElementById('txtSearchTerm_Kartable').value;
    ChangeLoadState_GridKartable_Kartable('Search');
}


function tlbItemExit_TlbKartable_onClick() {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Kartable = confirmState;
    switch (confirmState) {
        case 'Reject':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfRejectMessage_Kartable').value;
            break;
        case 'PageChange':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfPageChange_Kartable').value;
            break;
        case 'Exit':
            var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
            var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
            var CloseMessage_DialogKartable = null;
            switch (RequestCaller) {
                case 'Kartable':
                    CloseMessage_DialogKartable = document.getElementById('hfKartableCloseMessage_Kartable').value;
                    break;
                case 'Survey':
                    CloseMessage_DialogKartable = document.getElementById('hfSurveyCloseMessage_Kartable').value;
                    break;
                case 'Sentry':
                    CloseMessage_DialogKartable = document.getElementById('hfSentryCloseMessage_Kartable').value;
                    break;
                case 'SpecialKartable':
                    CloseMessage_DialogKartable = document.getElementById('hfSpecialKartableCloseMessage_Kartable').value;
                    break;
                case 'RequestSubstituteKartable':
                    CloseMessage_DialogKartable = document.getElementById('hfRequestSubstituteKartableCloseMessage_Kartable').value;
                    break;
            }
            document.getElementById('lblConfirm').innerHTML = CloseMessage_DialogKartable;
            break;
    }
    DialogConfirm.Show();
    if (RequestCaller != 'Sentry')
        CollapseControls_Kartable();
}

function tlbItemAllRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable(GetDefaultLoadState_Kartable());
}

function tlbItemDailyRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Daily');
}

function tlbItemHourlyRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Hourly');
}

function tlbItemOverTimeJustification_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('OverWork');
}

function tlbItemImperative_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Imperative');
}

function tlbItemTerminate_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Terminate');
}

function tlbItemConfirmedRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Confirmed');
}

function tlbItemRejectedRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Unconfirmed');
}

function tlbItemDeletedRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Deleted');
}

function tlbItemUnderReviewRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('UnderReview');
}
function tlbItemTerminatedRequests_TlbKartableFilter_Kartable_onClick() {
    ChangeLoadState_GridKartable_Kartable('Terminated');
}

function tlbItemRefresh_TlbPaging_GridKartable_Kartable_onClick() {
    var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
    switch (RequestCaller) {
        case 'Kartable':
            break;
        case 'Survey':
            break;
        case 'Sentry':
            break;
        case 'SpecialKartable':
            StrFilterConditions_Kartable = '';
        case 'RequestSubstituteKartable':
            StrFilterConditions_Kartable = '';
            break;
    }
    ChangeLoadState_GridKartable_Kartable(GetDefaultLoadState_Kartable());
}

function ChangeLoadState_GridKartable_Kartable(state) {
    LoadState_Kartable = state;
    SetPageIndex_GridKartable_Kartable(0);
}

function tlbItemFirst_TlbPaging_GridKartable_Kartable_onClick() {
    SetPageIndex_GridKartable_Kartable(0);
}

function tlbItemBefore_TlbPaging_GridKartable_Kartable_onClick() {
    if (CurrentPageIndex_GridKartable_Kartable != 0) {
        CurrentPageIndex_GridKartable_Kartable = CurrentPageIndex_GridKartable_Kartable - 1;
        SetPageIndex_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable);
    }
}

function tlbItemNext_TlbPaging_GridKartable_Kartable_onClick() {
    if (CurrentPageIndex_GridKartable_Kartable < parseInt(document.getElementById('hfKartablePageCount_Kartable').value) - 1) {
        CurrentPageIndex_GridKartable_Kartable = CurrentPageIndex_GridKartable_Kartable + 1;
        SetPageIndex_GridKartable_Kartable(CurrentPageIndex_GridKartable_Kartable);
    }
}

function tlbItemLast_TlbPaging_GridKartable_Kartable_onClick() {
    SetPageIndex_GridKartable_Kartable(parseInt(document.getElementById('hfKartablePageCount_Kartable').value) - 1);
}

function SetPageIndex_GridKartable_Kartable(pageIndex) {
    CurrentPageIndex_GridKartable_Kartable = pageIndex;
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    switch (RequestCaller) {
        case 'Kartable':
            if (StrSelectedRequests_Kartable == '#')
                Fill_GridKartable_Kartable(pageIndex, AccordingToView_Kartable);
            else
                ShowDialogConfirm('PageChange');
            break;
        case 'Survey':
            Fill_GridKartable_Kartable(pageIndex, AccordingToView_Kartable);
            break;
        case 'Sentry':
            Fill_GridKartable_Kartable(pageIndex, AccordingToView_Kartable);
            break;
        case 'SpecialKartable':
            if (StrSelectedRequests_Kartable == '#')
                Fill_GridKartable_Kartable(pageIndex, AccordingToView_Kartable);
            else
                ShowDialogConfirm('PageChange');
            break;
        case 'RequestSubstituteKartable':
            if (StrSelectedRequests_Kartable == '#')
                Fill_GridKartable_Kartable(pageIndex, AccordingToView_Kartable);
            else
                ShowDialogConfirm('PageChange');
    }
}

function ConfirmPageChange_GridKartable_Kartable() {
}

function cmbYear_Kartable_onChange(sender, e) {
    document.getElementById('hfCurrentYear_Kartable').value = cmbYear_Kartable.getSelectedItem().get_value();
}

function cmbMonth_Kartable_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_Kartable').value = cmbMonth_Kartable.getSelectedItem().get_value();
}

function Fill_GridKartable_Kartable(pageIndex, AccordingToView_Kartable) {
    document.getElementById('loadingPanel_GridKartable_Kartable').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridKartable_Kartable').value);
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var pageSize = parseInt(document.getElementById('hfKartablePageSize_Kartable').value);
    var sortBy = document.getElementById('hfCurrentSortBy_Kartable').value;
    var year = '0';
    var month = '0';
    var date = null;
    var FromDate = null;
    var ToDate = null;
    var isEndFlowRequestsView = false;
    if (RequestCaller == 'Kartable' || RequestCaller == 'Survey' || RequestCaller == 'SpecialKartable' || RequestCaller == 'RequestSubstituteKartable') {       
            year = document.getElementById('hfCurrentYear_Kartable').value;
            month = document.getElementById('hfCurrentMonth_Kartable').value;
            if (document.getElementById('tblViewCordinateDate_Kartable') != null && document.getElementById('tblViewCordinateDate_Kartable') != undefined) {
                switch (parent.parent.SysLangID) {
                    case 'fa-IR':
                        FromDate = document.getElementById('pdpFromDate_Kartable').value;
                        ToDate = document.getElementById('pdpToDate_Kartable').value;
                        break;
                    case 'en-Us':
                        FromDate = document.getElementById('gdpFromDate_Kartable').value;
                        ToDate = document.getElementById('gdpToDate_Kartable').value;
                        break;
                }
            }
            else {
                FromDate = "";
                ToDate = "";
            }
        if (RequestCaller == 'SpecialKartable' || RequestCaller == 'RequestSubstituteKartable')
            StrFilterConditions_Kartable = document.getElementById('txtSearchTerm_Kartable').value;
    }
    if (RequestCaller == 'Sentry') {
        switch (parent.SysLangID) {
            case 'fa-IR':
                date = document.getElementById('pdpDate_Kartable').value;
                break;
            case 'en-US':
                date = document.getElementById('gdpDate_Kartable').value;
                break;
        }
        isEndFlowRequestsView = document.getElementById('chbEndFlowRequestsView_Kartable').checked;
    }
    CallBack_GridKartable_Kartable.callback(CharToKeyCode_Kartable(RequestCaller), CharToKeyCode_Kartable(LoadState_Kartable), CharToKeyCode_Kartable(year), CharToKeyCode_Kartable(month), CharToKeyCode_Kartable(date), CharToKeyCode_Kartable(StrFilterConditions_Kartable), CharToKeyCode_Kartable(isEndFlowRequestsView.toString()), CharToKeyCode_Kartable(sortBy), CharToKeyCode_Kartable(pageSize.toString()), CharToKeyCode_Kartable(pageIndex.toString()), CharToKeyCode_Kartable(AccordingToView_Kartable), CharToKeyCode_Kartable(FromDate), CharToKeyCode_Kartable(ToDate));
    parent.DialogLoading.Show();
}

function CustomizeTlbKartable_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    CallBackTlbKartable_Kartable.callback(CharToKeyCode_Kartable(RequestCaller));
}

function CustomizeTlbKartableFilter_Kartable() {
    var RequestCaller = parent.DialogKartable.get_value().RequestCaller;
    CallBackTlbKartableFilter_Kartable.callback(CharToKeyCode_Kartable(RequestCaller));
}

function CharToKeyCode_Kartable(str) {
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

//DNN Note
function KeyCodeToChar_Kartable(str) {
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

function tlbItemView_TlbView_Kartable_onClick() {
    document.getElementById('txtSearchTerm_Kartable').value = '';
    ChangeLoadState_GridKartable_Kartable(GetDefaultLoadState_Kartable());
}

function tlbItemEndorsement_TlbRequestReject_Kartable_onClick() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var RequestState = null;
    switch (RequestCaller) {
        case 'Kartable':
            RequestState = 'Unconfirmed';
            break;
        case 'Survey':
            RequestState = 'Deleted';
            break;
        case 'SpecialKartable':
            switch (StateRequest_Kartable) {
                case 'Reject':
                    RequestState = 'Unconfirmed';
                    break;
                case 'Delete':
                    RequestState = 'Deleted';
                    break;
                default:
                    break;
            }
        case 'RequestSubstituteKartable':
            switch (StateRequest_Kartable) {
                case 'Reject':
                    RequestState = 'Unconfirmed';
                    break;
                case 'Delete':
                    RequestState = 'Deleted';
                    break;
                default:
                    break;
            }
            break;
    }
    DialogRequestRejectDescription.Close();
    UpdateKartable_Kartable(RequestState);
}

function tlbItemCancel_TlbRequestReject_Kartable_onClick() {
    DialogRequestRejectDescription.Close();
    document.getElementById('txtDescription_RequestReject_Kartable').value = '';
}

function SetClmnImage_GridKartable_Kartable(dataField, Key, ParentID) {
    var preName = '';
    var cellImage = '';
    if (ParentID != undefined && ParentID != null && ParentID != '0') {
        preName = "terminate_";
    }
    switch (dataField) {
        case 'FlowStatus':
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
            break;
        case 'FlowLevels':
            cellImage = 'Images/Grid/info.png';
            break;
        case 'RequestType':
            switch (Key.toString()) {
                case '0':
                    cellImage = 'Images/Grid/all.png';
                    break;
                case '1':
                    cellImage = 'Images/Grid/' + preName + 'clock.png';
                    break;
                case '2':
                    cellImage = 'Images/Grid/' + preName + 'day.png';
                    break;
                case '3':
                    cellImage = 'Images/Grid/' + preName + 'monthly.png';
                    break;
                case '4':
                    cellImage = 'Images/Grid/' + preName + 'Permission.png';
                    break;
                case '5':
                    cellImage = 'Images/Grid/' + preName + 'imperative.png';
                    break;
            }
            break;
        case 'RequestSource':
            switch (Key.toString()) {
                case '0':
                    cellImage = 'Images/Grid/user.png';
                    break;
                case '1':
                    cellImage = 'Images/Grid/role.png';
                    break;
            }
            break;
        case "RequestHistory":
            switch (Key.toString()) {
                case "false":
                    cellImage = 'Images/Grid/edit.png';
                    break;
                case "true":
                    cellImage = 'Images/Grid/edited.png';
                    break;
                default:

            }
            break;
    }
    return cellImage;
}

function GetRequestFlowLevel_GridKartable_Kartable() {
    ShowDialogEndorsementFlowState_Kartable(false);
}

function SetCellTitle_GridKartable_Kartable(dataField, Key) {
    var elementID = null;
    switch (dataField) {
        case 'FlowStatus':
            elementID = 'hfRequestStates_Kartable';
            break;
        case 'RequestType':
            elementID = 'hfRequestTypes_Kartable';
            break;
        case 'RequestSource':
            elementID = 'hfRequestSources_Kartable';
            break;
    }
    strListObj = document.getElementById(elementID).value.split('#');
    for (var i = 0; i < strListObj.length; i++) {
        strListItemObj = strListObj[i].split(':');
        if (strListItemObj.length > 1) {
            if (strListItemObj[1] == Key.toString())
                return strListItemObj[0];
        }
    }
}

function GetShiftTypeTitle_Shift(shiftType) {
    var ShiftTypes = document.getElementById('hfShiftTypes_Shift').value.split('#');
    for (var i = 0; i < ShiftTypes.length; i++) {
        var shiftTypeObj = ShiftTypes[i].split(':');
        if (shiftTypeObj.length > 1) {
            if (shiftTypeObj[1] == shiftType.toString())
                return shiftTypeObj[0];
        }
    }
}

function GridKartable_Kartable_onItemCheckChange(sender, e) {
    ChangeRequestsList_Kartable(e.get_item());
}

function ChangeRequestsList_Kartable(RequestItem) {
    var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
    var requestID = '0';
    var requestSubstituteID = '0';
    requestID = RequestItem.getMember('RequestID').get_text();
    var managerFlowID = RequestItem.getMember('ManagerFlowID').get_text();
    if (RequestCaller != 'RequestSubstituteKartable') {
        if (StrSelectedRequests_Kartable != null && StrSelectedRequests_Kartable != '#' && StrSelectedRequests_Kartable.indexOf('#RID=' + requestID + '%MFID=' + managerFlowID + '#') >= 0)
            StrSelectedRequests_Kartable = StrSelectedRequests_Kartable.replace('#RID=' + requestID + '%MFID=' + managerFlowID + '#', '#');
        else
            StrSelectedRequests_Kartable += 'RID=' + requestID + '%MFID=' + managerFlowID + '#';
    }
    else {
        requestSubstituteID = RequestItem.getMember('ID').get_text();
        if (StrSelectedRequests_Kartable != null && StrSelectedRequests_Kartable != '#' && StrSelectedRequests_Kartable.indexOf('#RID=' + requestID + '%MFID=' + managerFlowID + '%RSID=' + requestSubstituteID + '#') >= 0)
            StrSelectedRequests_Kartable = StrSelectedRequests_Kartable.replace('#RID=' + requestID + '%MFID=' + managerFlowID + '%RSID=' + requestSubstituteID + '#', '#');
        else
            StrSelectedRequests_Kartable += '#RID=' + requestID + '%MFID=' + managerFlowID + '%RSID=' + requestSubstituteID + '#';
    }
}

function chbSelectAllinthisPage_Kartable_onClick() {
    StrSelectedRequests_Kartable = '#';
    var checked = false;
    if (document.getElementById('chbSelectAllinthisPage_Kartable').checked)
        checked = true;
    GridKartable_Kartable.beginUpdate();
    for (var i = 0; i < GridKartable_Kartable.get_table().getRowCount() ; i++) {
        var gridItem = GridKartable_Kartable.get_table().getRow(i);
        gridItem.setValue(7, checked, false);
        if (checked) {
            var requestID = gridItem.getMember('RequestID').get_value();
            var managerFlowID = gridItem.getMember('ManagerFlowID').get_text();
            StrSelectedRequests_Kartable += 'RID=' + requestID + '%MFID=' + managerFlowID + '#';
        }
    }
    GridKartable_Kartable.endUpdate();
}

function DialogRequestDescription_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tbl_DialogRequestDescription_Kartable').style.direction = 'rtl';
}

function ShowRequestDescription_GridKartable_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        document.getElementById('txtDescription_RequestDescription_Kartable').value = GridKartable_Kartable.getSelectedItems()[0].getMember('Description').get_text();
        DialogRequestDescription.Show();
    }
}

function tlbItemExit_tlbExit_RequestDescription_Kartable_onClick() {
    DialogRequestDescription.Close();
}

function tlbItemExit_tlbExit_RequestReject_Kartable_onClick() {
    DialogRequestRejectDescription.Close();
}

function DialogConfirm_OnShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById('tblConfirm_DialogConfirm').style.direction = 'rtl';
}

function CallBack_GridKartable_Kartable_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridKartable_Kartable').innerHTML = '';
    ShowConnectionError_Kartable();
}

function ShowConnectionError_Kartable() {
    var error = document.getElementById('hfErrorType_Kartable').value;
    var errorBody = document.getElementById('hfConnectionError_Kartable').value;
    showDialog(error, errorBody, 'error');
}

function CollapseControls_Kartable() {
    cmbYear_Kartable.collapse();
    cmbMonth_Kartable.collapse();
}

function tlbItemFormReconstruction_TlbKartable_onClick() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    DialogKartable_onClose();
    parent.DialogKartable.set_value(ObjDialogKartable);
    parent.DialogKartable.Show();
}

function tlbItemHelp_TlbKartable_onClick() {
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var helpID = null;
    switch (RequestCaller) {
        case 'Kartable':
            helpID = 'tlbItemHelp_TlbKartable';
            break;
        case 'Survey':
            helpID = 'tlbItemHelp_TlbSurvey';
            break;
        case 'Sentry':
            helpID = 'tlbItemHelp_TlbSentry';
            break;
        case 'SpecialKartable':
            helpID = 'tlbItemHelp_TlbSpecialKartable';
            break;
        case 'RequestSubstituteKartable':
            helpID = 'tlbItemHelp_TlbRequestSubstituteKartable';
            break;
    }
    LoadHelpPage(helpID);
}

function cmbSortBy_Kartable_onChange(sender, e) {
    document.getElementById('hfCurrentSortBy_Kartable').value = cmbSortBy_Kartable.getSelectedItem().get_value();
}

function ChangeComboDirection_MasterMonthlyOperation(cmbID) {
    if (parent.CurrentLangID == 'en-US')
        document.getElementById(cmbID + '_DropDownContent').dir = 'ltr';
    if (parent.CurrentLangID == 'fa-IR')
        document.getElementById(cmbID + '_DropDownContent').dir = 'rtl';
}

function btn_gdpDate_Kartable_OnMouseUp(event) {
    if (gCalDate_Kartable.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function btn_gdpDate_Kartable_OnClick(event) {
    if (gCalDate_Kartable.get_popUpShowing()) {
        gCalDate_Kartable.hide();
    }
    else {
        gCalDate_Kartable.setSelectedDate(gdpDate_Kartable.getSelectedDate());
        gCalDate_Kartable.show();
    }
}

function gdpDate_Kartable_OnDateChange(sender, e) {
    var RequestDate = gdpDate_Kartable.getSelectedDate();
    gCalDate_Kartable.setSelectedDate(RequestDate);
}

function gCalDate_Kartable_OnChange(sender, e) {
    var RequestDate = gCalDate_Kartable.getSelectedDate();
    gdpDate_Kartable.setSelectedDate(RequestDate);
}

function gCalDate_Kartable_OnLoad(sender, e) {
    window.gCalDate_Kartable.PopUpObject.z = 25000000;
}

function ViewCurrentLangCalendars_Calendar() {
    switch (parent.SysLangID) {
        case 'en-US':
            document.getElementById("pdpDate_Kartable").parentNode.removeChild(document.getElementById("pdpDate_Kartable"));
            break;
        case 'fa-IR':
            document.getElementById("Container_DateCalendars_RequestRegister").removeChild(document.getElementById("Container_gCalDate_Kartable"));
            break;
    }
}

function SetCurrentDate_Kartable() {
    var ObjDialogKartable_Kartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable_Kartable.RequestCaller;
    if (RequestCaller == 'Sentry') {
        switch (parent.SysLangID) {
            case 'fa-IR':
                document.getElementById('pdpDate_Kartable').value = document.getElementById('hfCurrentDate_Kartable').value;
                break;
            case 'en-US':
                currentDate_Kartable = new Date(document.getElementById('hfCurrentDate_Kartable').value);
                gdpDate_Kartable.setSelectedDate(currentDate_Kartable);
                gCalDate_Kartable.setSelectedDate(currentDate_Kartable);
                break;
        }
    }
}

function tlbItemClosePicture_TlbApplicantPicture_onClick() {
    document.getElementById(parent.ClientPerfixId + 'ApplicantImage_DialogApplicantImage').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogApplicantImage').Close();
}

function ShowApplicantImage_GridKartable_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        var dialogApplicantImageDirection = null;
        switch (parent.CurrentLangID) {
            case 'fa-IR':
                dialogApplicantImageDirection = 'rtl';
                break;
            case 'en-US':
                dialogApplicantImageDirection = 'ltr';
                break;
        }
        document.getElementById('Mastertbl_DialogApplicantImage').dir = dialogApplicantImageDirection;
        var PersonnelImage = GridKartable_Kartable.getSelectedItems()[0].getMember('PersonImage').get_text();
        document.getElementById('tdCurrentApplicant_DialogApplicantImage').innerHTML = GridKartable_Kartable.getSelectedItems()[0].getMember('Applicant').get_text();
        document.getElementById('ApplicantImage_DialogApplicantImage').src = "ImageViewer.aspx?reload=" + (new Date()).getTime() + "&AttachmentType=Personnel&ClientAttachment=" + CharToKeyCode_Kartable(PersonnelImage);
        DialogApplicantImage.Show();
    }
}

function ShowAttachmentFile_GridKartable_Kartable() {
    var SelectedItems_GridKartable_Kartable = GridKartable_Kartable.getSelectedItems();
    if (SelectedItems_GridKartable_Kartable.length > 0 && SelectedItems_GridKartable_Kartable[0].getMember('AttachmentFile').get_text() != '') {
        var AttachmentFile = SelectedItems_GridKartable_Kartable[0].getMember('AttachmentFile').get_text();
        window.open("ClientAttachmentViewer.aspx?AttachmentType=Request&ClientAttachment=" + CharToKeyCode_Kartable(AttachmentFile) + "", "ClientAttachmentViewer" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
    }
}

function SetAttachmentFileImage_GridKartable_Kartable(attachmentFile) {
    var innerHTML = '';
    if (attachmentFile != undefined && attachmentFile != null && attachmentFile != '')
        innerHTML = '<img src="Images/Grid/attachment.png" alt="" />';
    return innerHTML;
}

function txtSearchTerm_Kartable_onKeyPess(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbKartableQuickSearch_onClick();
    }
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function GetRequestHistory_GridKartable_Kartable() {
    ShowDialogRequestHistory_Kartable();
}

function ShowDialogRequestHistory_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        if (GridKartable_Kartable.getSelectedItems()[0].getMember('RequestSubstituteID').get_text() == '0') {
            var RequestType = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestType').get_text();
            if (RequestType == "1" || RequestType == "2" || RequestType == "4") {
                var ObjDialogKartable = parent.DialogKartable.get_value();
                var ObjRequestHistory = new Object();
                ObjRequestHistory.RequestID = GridKartable_Kartable.getSelectedItems()[0].getMember('RequestID').get_text();
                ObjRequestHistory.RequestCaller = ObjDialogKartable.RequestCaller;
                ObjRequestHistory.RequestType = RequestType;
                ObjRequestHistory.FromDate = GridKartable_Kartable.getSelectedItems()[0].getMember('ThePureFromDate').get_text();
                ObjRequestHistory.ToDate = GridKartable_Kartable.getSelectedItems()[0].getMember('ThePureToDate').get_text();
                ObjRequestHistory.FromTime = GridKartable_Kartable.getSelectedItems()[0].getMember('TheFromTime').get_text();
                ObjRequestHistory.ToTime = GridKartable_Kartable.getSelectedItems()[0].getMember('TheToTime').get_text();
                ObjRequestHistory.Duration = GridKartable_Kartable.getSelectedItems()[0].getMember('TheDuration').get_text();
                ObjRequestHistory.AttachmentFile = GridKartable_Kartable.getSelectedItems()[0].getMember('AttachmentFile').get_text();
                parent.DialogRequestHistory.set_value(ObjRequestHistory);
                parent.DialogRequestHistory.Show();
                CollapseControls_Kartable();
            }
        }
    }
}

function CallBack_GridSettings_Kartabl_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_GridSettings_Kartable').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2], false, document.getElementById('KartableForm').offsetWidth);
    }
    else {
        if (SettingsState_Kartable == 'View') {
            DialogGridSettings.Show();
            CollapseControls_Kartable();
        }
        if (SettingsState_Kartable == 'Save') {
            DialogGridSettings.Close();
            parent.DialogKartable.Close();
            parent.DialogKartable.Show();
        }
    }
}
function CallBack_GridSettings_Kartabl_onCallbackError(sender, e) {
    ShowConnectionError_Kartable();
}
function tlbItemGridSetting_TlbKartable_onClick() {
    ShowDialogGridSettings();
}
function ShowDialogGridSettings() {
    SettingsState_Kartable = 'View';
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    document.KartableForm.chbSelectAll_GridSettings_Kartable.checked = false;
    CallBack_GridSettings_Kartable.callback(CreateColumnsArray_GridKartable_Kartable(), 'Get', '', CharToKeyCode_Kartable(RequestCaller));
}
function CreateColumnsArray_GridKartable_Kartable() {
    var ArColumns = '';
    var ColumnsCol_GridKartable_Kartable = GridKartable_Kartable.get_table().get_columns();
    for (var i = 1; i < (ColumnsCol_GridKartable_Kartable.length - 6) ; i++) {
        var Splitter = ":";
        if (i == ColumnsCol_GridKartable_Kartable.length - 7)
            Splitter = '';
        ArColumns = ArColumns + CharToKeyCode_Kartable(ColumnsCol_GridKartable_Kartable[i].get_dataField()) + "%" + CharToKeyCode_Kartable(ColumnsCol_GridKartable_Kartable[i].get_headingText()) + Splitter;
    }
    return ArColumns;
}
function chbSelectAll_GridSettings_Kartable_onClick() {
    var Checked = null;
    if (document.KartableForm.chbSelectAll_GridSettings_Kartable.checked)
        Checked = true;
    else
        Checked = false;

    GridSettings_Kartable.beginUpdate();
    for (var i = 0; i < GridSettings_Kartable.get_table().getRowCount() ; i++) {
        GridSettings_Kartable.get_table().getRow(i).setValue(4, Checked, false);
    }
    GridSettings_Kartable.endUpdate();
}
function tlbItemSave_TlbGridSettings_onClick() {
    GridSettings_Kartable_onSave();
}
function GridSettings_Kartable_onSave() {
    SettingsState_Kartable = 'Save';
    var ObjDialogKartable = parent.DialogKartable.get_value();
    var RequestCaller = ObjDialogKartable.RequestCaller;
    var GridSettingExist_Kartable = document.getElementById('hfExist_GridSettings_Kartable').value;
    var GridSettingId_Kartable = document.getElementById('hfCurrentId_GridSettings_Kartable').value;
    var GridCurrentUserSettingId_Kartable = document.getElementById('hfCurrentUserSettingId_GridSettings_Kartable').value;
    CallBack_GridSettings_Kartable.callback(CreateColumnsArray_GridKartable_Kartable(), 'Set', CreateColumnsArray_GridSettings_Kartable(), CharToKeyCode_Kartable(RequestCaller), CharToKeyCode_Kartable(GridSettingExist_Kartable), CharToKeyCode_Kartable(GridSettingId_Kartable), CharToKeyCode_Kartable(GridCurrentUserSettingId_Kartable));
}
function CreateColumnsArray_GridSettings_Kartable() {
    var ArColumns = '';
    for (var i = 0; i < GridSettings_Kartable.get_table().getRowCount() ; i++) {
        var Splitter = ':';
        if (i == GridSettings_Kartable.get_table().getRowCount() - 1)
            Splitter = '';
        ArColumns = ArColumns + CharToKeyCode_Kartable(GridSettings_Kartable.get_table().getRow(i).getMember('GridColumn').get_text()) + '%' + GridSettings_Kartable.get_table().getRow(i).getMember('ViewState').get_text() + Splitter;
    }
    return ArColumns;
}
function tlbItemExit_TlbGridSettings_onClick() {
    DialogGridSettings.Close();
    SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame();
}
function SetScrollPosition_DialogMonthlyOperationGridSchema_IFrame() {
    if (parent.CurrentLangID == 'fa-IR')
        parent.document.getElementById('DialogKartable_IFrame').contentWindow.scroll(20000, 0);
}

function ShowParentRequest_GridKartable_Kartable() {
    var SelectedItems_GridKartable_Kartable = GridKartable_Kartable.getSelectedItems();
    if (SelectedItems_GridKartable_Kartable.length > 0 &&
        SelectedItems_GridKartable_Kartable[0].getMember('ParentID').get_text() != '0') {

        var RequestID = SelectedItems_GridKartable_Kartable[0].getMember('ParentID').get_text();
        ShowDialogRefrenceRequest_Kartable(RequestID, 'IsParent');
    }
    if (SelectedItems_GridKartable_Kartable.length > 0 &&
        SelectedItems_GridKartable_Kartable[0].getMember('ChildsCount').get_text() != '0' &&
        SelectedItems_GridKartable_Kartable[0].getMember('ParentID').get_text() == '0') {

        var RequestID = SelectedItems_GridKartable_Kartable[0].getMember('RequestID').get_text();
        ShowDialogRefrenceRequest_Kartable(RequestID, 'IsChild');
    }
}

function SetParentRequestImage_GridKartable_Kartable(ParentID, ChildsCount) {
    var innerHTML = '';
    if (ParentID != undefined && ParentID != null && ParentID != '0')
        innerHTML = '<img src="Images/Grid/thread_pinned_new.gif" alt="" />';
    else if (ParentID = '0' && ChildsCount > '0')
        innerHTML = '<img src="Images/Grid/thread_popular_read.gif" alt="" />';
    return innerHTML;
}


function SetRequestHistoryImage_GridKartable_Kartable(key, ParentID, ReuquestSubstituteId) {
    if (ReuquestSubstituteId == '0') {
        var innerHTML = '';
        if (ParentID != '0') {
            innerHTML = '<td align="center" style="font-family: Tahoma; font-size: 10px;"></td>';
        }
        else {
            switch (key.toString()) {
                case "false":
                    innerHTML = '<td align="center" style="font-family: Tahoma; font-size: 10px; cursor: crosshair;" ondblclick="GetRequestHistory_GridKartable_Kartable();"><img style="cursor: crosshair;" src="Images/Grid/edit.png" alt="" /></td>';
                    break;
                case "true":
                    innerHTML = '<td align="center" style="font-family: Tahoma; font-size: 10px; cursor: crosshair;" ondblclick="GetRequestHistory_GridKartable_Kartable();"><img style="cursor: crosshair;" src="Images/Grid/edited.png" alt="" /></td>';
                    break;
                default:
            }
        }
        return innerHTML;
    }
}


function ShowDialogRefrenceRequest_Kartable(RequestID, RefrenceType) {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {

        var ObjRequestRefrence = new Object();
        ObjRequestRefrence.RequestID = RequestID;
        ObjRequestRefrence.Caller = 'RegisteredRequests';
        ObjRequestRefrence.RefrenceType = RefrenceType;

        parent.DialogRequestRefrence.set_value(ObjRequestRefrence);
        parent.DialogRequestRefrence.Show();
        CollapseControls_Kartable();
    }
}

function chbEndFlowRequestsView_Kartable_onClick() {
    SetPageIndex_GridKartable_Kartable(0);
}

function tlbItemExit_tlbExit_ParentDepartments_Kartable_onClick() {
    CloseDialogParentDepartments_Kartable();
}

function CloseDialogParentDepartments_Kartable() {
    DialogParentDepartments.Close();
}

function trvParentDepartments_Kartable_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvParentDepartments_Kartable').innerHTML = "";
}

function trvParentDepartments_Kartable_onNodeExpand() {
    Resize_trvParentDepartments_Kartable();
    ChangeDirection_trvParentDepartments_Kartable();
}

function ChangeDirection_trvParentDepartments_Kartable() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvParentDepartments_Kartable').style.direction = 'ltr';
    }
}

function Resize_trvParentDepartments_Kartable() {
    document.getElementById('trvParentDepartments_Kartable').style.width = CurrentPageTreeViewsObj.trvParentDepartments_Kartable;
}


function CallBack_trvParentDepartments_Kartable_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ParentDepartments_Kartable').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload') {
            var departmentID = DialogParentDepartments.get_value();
            if (departmentID != null && departmentID != undefined && departmentID != '0')
                Fill_trvParentDepartments_PersonnelMainInformation();
        }
    }
    else {
        Resize_trvParentDepartments_Kartable();
        ChangeDirection_trvParentDepartments_Kartable();
    }
}

function CallBack_trvParentDepartments_Kartable_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvParentDepartments_Kartable').innerHTML = '';
    ShowConnectionError_Kartable();
}

function ShowDialogParentDepartments() {
    Fill_trvParentDepartments_Kartable();
    DialogParentDepartments.Show();
}

function Fill_trvParentDepartments_Kartable() {
    if (GridKartable_Kartable.getSelectedItems().length > 0) {
        var departmentID = GridKartable_Kartable.getSelectedItems()[0].getMember('DepartmentId').get_text();
        DialogParentDepartments.set_value(departmentID);
        document.getElementById('loadingPanel_trvParentDepartments_Kartable').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvParentDepartments_Kartable').value);
        CallBack_trvParentDepartments_Kartable.callback(CharToKeyCode_Kartable(departmentID.toString()));
    }
}

function CacheTreeViewsSize_Kartable() {
    CurrentPageTreeViewsObj.trvParentDepartments_Kartable = document.getElementById('trvParentDepartments_Kartable').clientWidth + 'px';
}

function DialogParentDepartments_onShow(sender, e) {
    if (parent.CurrentLangID == 'fa-IR') {
        document.getElementById('tbl_DialogParentDepartments_Kartable').dir = 'rtl';
    }
}
function btn_gdpFromDate_Kartable_OnMouseUp(event) {
    if (gCalFromDate_Kartable.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function gdpFromDate_Kartable_OnDateChange(sender , e) {
    var FromDate = gdpFromDate_Kartable.getSelectedDate();
    gCalFromDate_Kartable.setSelectedDate(FromDate);
}
function btn_gdpFromDate_Kartable_OnClick(event) {
    if (gCalFromDate_Kartable.get_popUpShowing()) {
        gCalFromDate_Kartable.hide();
    }
    else {
        gCalFromDate_Kartable.setSelectedDate(gdpFromDate_Kartable.getSelectedDate());
        gCalFromDate_Kartable.show();
    }
}
function gCalFromDate_Kartable_OnChange(sender, e) {
    var FromDate = gCalFromDate_Kartable.getSelectedDate();
    gdpFromDate_Kartable.setSelectedDate(FromDate);

}
function gCalFromDate_Kartable_onLoad(sender, e) {
    window.gCalFromDate_Kartable.PopUpObject.z = 25000000;
}
function btn_gdpToDate_Kartable_OnMouseUp(event) {
    if (gCalToDate_Kartable.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function gdpToDate_Kartable_OnDateChange(sender , e) {
    var ToDate = gdpToDate_Kartable.getSelectedDate();
    gCalToDate_Kartable.setSelectedDate(ToDate);
}
function btn_gdpToDate_Kartable_OnClick(event) {
    if (gCalToDate_Kartable.get_popUpShowing()) {
        gCalToDate_Kartable.hide();
    }
    else {
        gCalToDate_Kartable.setSelectedDate(gdpToDate_Kartable.getSelectedDate());
        gCalToDate_Kartable.show();
    }
}
function btn_gdpToDate_Kartable_OnMouseUp(event) {
    if (gCalToDate_Kartable.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function gCalToDate_Kartable_OnChange(sender, e) {
    var ToDate = gCalToDate_Kartable.getSelectedDate();
    gdpToDate_Kartable.setSelectedDate(ToDate);
}
function gCalToDate_Kartable_onLoad(sender, e) {
    window.gCalToDate_Kartable.PopUpObject.z = 25000000;
}
function tlbItemView_TlbViewCordinateDate_Kartable_onClick() {
    document.getElementById('txtSearchTerm_Kartable').value = '';
    GetDefaultLoadState_Kartable();   
    AccordingToView_Kartable = 'Date';
    CurrentPageIndex_GridKartable_Kartable = 0;
    Fill_GridKartable_Kartable(0, AccordingToView_Kartable);
}
function tlbItemView_TlbViewCordinateYearAndMonth_Kartable_onClick() {
    document.getElementById('txtSearchTerm_Kartable').value = '';
    GetDefaultLoadState_Kartable();
    AccordingToView_Kartable = 'YearMonth';
    CurrentPageIndex_GridKartable_Kartable = 0;
    Fill_GridKartable_Kartable(0, AccordingToView_Kartable);
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