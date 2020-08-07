
var ConfirmState_MonthlyExceptionShifts = null;
var CurrentPageState_MonthlyExceptionShifts = 'View';
var Basefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts = null;
var CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = 0;
var ObjMonthlyExceptionShifts_MonthlyExceptionShifts = null;
var mappingObjectArray = null;
var ShortcutKey = false;
var KeyboardSetting = false;
var PrimaryShiftCode;
//var objShifts_MonthlyExceptionShifts = [MonthlyExceptionShift = {Id: 0 , Name:1 , CustomCode:2  , ShortcutsKey :3 , }];
var objShifts_MonthlyExceptionShifts = [];
var SearchItem_MonthlyExceptionShifts = '';
var State_MonthlyExceptionShifts = 'Normal';
var CurrentPageCombosCallBcakStateObj = new Object();
var HolidaysList_MonthlyExceptionShifts = '';

function GetBoxesHeaders_MonthlyExceptionShifts() {
    parent.document.getElementById('Title_DialogMonthlyExceptionShifts').innerHTML = document.getElementById('hfTitle_DialogMonthlyExceptionShifts').value;
    document.getElementById('header_MonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = document.getElementById('hfheader_MonthlyExceptionShifts_MonthlyExceptionShifts').value;
    Basefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts = document.getElementById('footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = document.getElementById('hffooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts').value;
}

function tlbItemHelp_TlbMonthlyExceptionShifts_onClick() {
    LoadHelpPage('tlbItemHelp_TlbMonthlyExceptionShifts');
}

function tlbItemFormReconstruction_TlbMonthlyExceptionShifts_onClick() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyExceptionShifts_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogMonthlyExceptionShifts').Close();
    parent.eval(parent.ClientPerfixId + 'DialogMonthlyExceptionShifts').Show();
}

function tlbItemExit_TlbMonthlyExceptionShifts_onClick() {
    ShowDialogConfirm('Exit');
}

function cmbYear_MonthlyExceptionShifts_onChange(sender, e) {
    document.getElementById('hfCurrentYear_MonthlyExceptionShifts').value = cmbYear_MonthlyExceptionShifts.getSelectedItem().get_value();
}

function cmbMonth_MonthlyExceptionShifts_onChange(sender, e) {
    document.getElementById('hfCurrentMonth_MonthlyExceptionShifts').value = cmbMonth_MonthlyExceptionShifts.getSelectedItem().get_value();
}

function tlbItemView_TlbView_MonthlyExceptionShifts_onClick() {
    // var searchItem_MonthlyExceptionShifts =document.getElementById('txtSerach_MonthlyExceptionShifts').value;
    SearchItem_MonthlyExceptionShifts = '';
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0, '');
}

function GridMonthlyExceptionShifts_MonthlyExceptionShifts_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = '';
}

function CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_MonthlyExceptionShifts').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0, SearchItem_MonthlyExceptionShifts);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else {
        Changefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts();
    }
    ChangeControlsEnabled_MonthlyExceptionShifts(true);
}

function CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyExceptionShifts();
}

function tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    Refresh_GridMonthlyExceptionShifts_MonthlyExceptionShifts();
}

function tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0, SearchItem_MonthlyExceptionShifts);
}

function tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    if (CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts != 0) {
        CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts - 1;
        SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts, SearchItem_MonthlyExceptionShifts);
    }
}

function tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    if (CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts < parseInt(document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value) - 1) {
        CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts + 1;
        SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts, SearchItem_MonthlyExceptionShifts);
    }
}

function tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick() {
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(parseInt(document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value) - 1, SearchItem_MonthlyExceptionShifts);
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_MonthlyExceptionShifts) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateMonthlyExceptionShifts_MonthlyExceptionShifts();
            break;
        case 'Exit':
            CloseDialogMonthlyExceptionShifts();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function EditGridMonthlyExceptionShifts_MonthlyExceptionShifts(rowID) {
    CurrentPageState_MonthlyExceptionShifts = 'Edit';
    GridMonthlyExceptionShifts_MonthlyExceptionShifts.edit(GridMonthlyExceptionShifts_MonthlyExceptionShifts.getItemFromClientId(rowID));
    document.getElementById('txtPersonNameEditing_MasterLeaveRemains').value = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getItemFromClientId(rowID).getMember('PersonName').get_text();
    document.getElementById('txtPersonBarcodeEditing_MasterLeaveRemains').value = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getItemFromClientId(rowID).getMember('PersonCode').get_text();

}

function DeleteGridMonthlyExceptionShifts_MonthlyExceptionShifts(rowID) {
    CurrentPageState_MonthlyExceptionShifts = 'Delete';
    ShowDialogConfirm('Delete');
}

function UpdateGridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    GridMonthlyExceptionShifts_MonthlyExceptionShifts.editComplete();
    UpdateMonthlyExceptionShifts_MonthlyExceptionShifts();
    CurrentPageState_MonthlyExceptionShifts = 'View';
    document.getElementById('txtPersonNameEditing_MasterLeaveRemains').value = '';
    document.getElementById('txtPersonBarcodeEditing_MasterLeaveRemains').value = '';
}

function UpdateMonthlyExceptionShifts_MonthlyExceptionShifts() {
    var SelectedItems_MasterExceptionShifts_MasterExceptionShifts = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getSelectedItems();
    if (SelectedItems_MasterExceptionShifts_MasterExceptionShifts.length > 0) {
        ObjMonthlyExceptionShifts_MonthlyExceptionShifts = new Object();
        ObjMonthlyExceptionShifts_MonthlyExceptionShifts.ID = SelectedItems_MasterExceptionShifts_MasterExceptionShifts[0].getMember('ID').get_text();
        var PersonnelID = SelectedItems_MasterExceptionShifts_MasterExceptionShifts[0].getMember('PersonID').get_text();
        var Year = document.getElementById('hfCurrentYear_MonthlyExceptionShifts').value;
        var Month = document.getElementById('hfCurrentMonth_MonthlyExceptionShifts').value;
        var StrDaysShiftCol = '';
        var ArDaysShiftCol = {};
        var separator = '&';
        if (CurrentPageState_MonthlyExceptionShifts == 'Edit') {
            var ColumnsCol_MasterExceptionShifts_MasterExceptionShifts = GridMonthlyExceptionShifts_MonthlyExceptionShifts.get_table().get_columns();
            for (var i = 5; i < ColumnsCol_MasterExceptionShifts_MasterExceptionShifts.length - 1; i++) {
                var gridColumn_MasterExceptionShifts_MasterExceptionShifts = ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i];
                if (gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_visible()) {
                    var DayShiftVal = SelectedItems_MasterExceptionShifts_MasterExceptionShifts[0].getMember(gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_dataField()).get_text();
                    ArDaysShiftCol[ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i].get_dataField()] = DayShiftVal;
                }
            }
            StrDaysShiftCol = JSON.stringify(ArDaysShiftCol);
        }
        UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage(CharToKeyCode_MonthlyExceptionShifts(CurrentPageState_MonthlyExceptionShifts), CharToKeyCode_MonthlyExceptionShifts(PersonnelID), CharToKeyCode_MonthlyExceptionShifts(Year), CharToKeyCode_MonthlyExceptionShifts(Month), CharToKeyCode_MonthlyExceptionShifts(StrDaysShiftCol));
        DialogWaiting.Show();
    }
}
function UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {
            if (CurrentPageState_MonthlyExceptionShifts == 'Delete')
                DeletePersonnelMonthlyExceptionShifts_GridMonthlyExceptionShifts_MonthlyExceptionShifts();
            CurrentPageState_MonthlyExceptionShifts = 'View';
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'error' || RetMessage[2] == 'warning')
            SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts, SearchItem_MonthlyExceptionShifts);

    }
}
function GridMonthlyExceptionShifts_MonthlyExceptionShifts_onItemSelect() {
    if (CurrentPageState_MonthlyExceptionShifts != 'Edit') {
        TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemPeriodRepeat_TlbMonthlyExceptionShifts').set_enabled(true);
        TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemPeriodRepeat_TlbMonthlyExceptionShifts').set_imageUrl('cycle.png');
    }
    else {
        TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemPeriodRepeat_TlbMonthlyExceptionShifts').set_enabled(false);
        TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemPeriodRepeat_TlbMonthlyExceptionShifts').set_imageUrl('cycle_Silver.png');
    }
}
function tlbItemEndorsement_TlbPeriodRepeat_MonthlyExceptionShifts_onClick() {
    if (GridMonthlyExceptionShifts_MonthlyExceptionShifts.getSelectedItems().length > 0) {
        DialogWaiting.Show();
        document.getElementById('cmbHolidays_MonthlyExceptionShifts_DropDown').style.display = 'none';
        var PersonId = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getSelectedItems()[0].getMember('PersonID').get_text();
        var Year = document.getElementById('hfCurrentYear_MonthlyExceptionShifts').value;
        var Month = document.getElementById('hfCurrentMonth_MonthlyExceptionShifts').value;
        var FromDay = cmbFromDay_MonthlyExceptionShifts.getSelectedItem().get_value();
        var ToDay = cmbToDay_MonthlyExceptionShifts.getSelectedItem().get_value();
        var StrHolidayList = HolidaysList_MonthlyExceptionShifts;
        var ColumnsCol_MasterExceptionShifts_MasterExceptionShifts = GridMonthlyExceptionShifts_MonthlyExceptionShifts.get_table().get_columns();
        var ArDaysShiftCol = {};
        for (var i = 5; i < ColumnsCol_MasterExceptionShifts_MasterExceptionShifts.length - 1; i++) {
            var gridColumn_MasterExceptionShifts_MasterExceptionShifts = ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i];
            if (gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_visible()) {
                var DayShiftVal = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getSelectedItems()[0].getMember(gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_dataField()).get_text();
                ArDaysShiftCol[ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i].get_dataField()] = DayShiftVal;
            }
        }
        StrDaysShiftCol = JSON.stringify(ArDaysShiftCol);
        UpdatePeriodRepeats_MonthlyExceptionShiftsPage(CharToKeyCode_MonthlyExceptionShifts(Year), CharToKeyCode_MonthlyExceptionShifts(Month), CharToKeyCode_MonthlyExceptionShifts(FromDay), CharToKeyCode_MonthlyExceptionShifts(ToDay), CharToKeyCode_MonthlyExceptionShifts(PersonId), CharToKeyCode_MonthlyExceptionShifts(StrHolidayList), CharToKeyCode_MonthlyExceptionShifts(StrDaysShiftCol));
    }
}
function UpdatePeriodRepeats_MonthlyExceptionShifts_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (RetMessage[2] == 'success') {
            var ArPeriodRepeate = eval('(' + RetMessage[3] + ')');
            var DayCount = cmbToDay_MonthlyExceptionShifts.get_itemCount();
            for (i = 0 ; i < DayCount ; i++) {
                GridMonthlyExceptionShifts_MonthlyExceptionShifts.beginUpdate();
                GridMonthlyExceptionShifts_MonthlyExceptionShifts.getSelectedItems()[0].setValue(i + 5, ArPeriodRepeate[i]);
                GridMonthlyExceptionShifts_MonthlyExceptionShifts.endUpdate();
            }            
        }
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'error' || RetMessage[2] == 'warning')
            SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts, SearchItem_MonthlyExceptionShifts);
        ClearHolidaysList_MonthlyExceptionShifts();
        DialogPeriodRepeat.Close();
    }
}
function DeletePersonnelMonthlyExceptionShifts_GridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    if (ObjMonthlyExceptionShifts_MonthlyExceptionShifts != null) {
        GridMonthlyExceptionShifts_MonthlyExceptionShifts.beginUpdate();
        MonthlyExceptionShiftsItem = GridMonthlyExceptionShifts_MonthlyExceptionShifts.getItemFromKey(0, ObjMonthlyExceptionShifts_MonthlyExceptionShifts.ID);
        GridMonthlyExceptionShifts_MonthlyExceptionShifts.selectByKey(ObjMonthlyExceptionShifts_MonthlyExceptionShifts.ID, 0, false);
        var ColumnsCol_MasterExceptionShifts_MasterExceptionShifts = GridMonthlyExceptionShifts_MonthlyExceptionShifts.get_table().get_columns();
        for (var i = 5; i < ColumnsCol_MasterExceptionShifts_MasterExceptionShifts.length - 1; i++) {
            var gridColumn_MasterExceptionShifts_MasterExceptionShifts = ColumnsCol_MasterExceptionShifts_MasterExceptionShifts[i];
            if (gridColumn_MasterExceptionShifts_MasterExceptionShifts.get_visible())
                MonthlyExceptionShiftsItem.setValue(i, '', false);
        }
        GridMonthlyExceptionShifts_MonthlyExceptionShifts.endUpdate();
    }
}

function SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts(state) {
    return document.getElementById('hf' + state + '_MonthlyExceptionShifts').value;
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_MonthlyExceptionShifts = confirmState;
    if (ConfirmState_MonthlyExceptionShifts == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_MonthlyExceptionShifts').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_MonthlyExceptionShifts').value;
    DialogConfirm.Show();
}

function SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(pageIndex, SearchItem_MonthlyExceptionShifts) {
    ChangeControlsEnabled_MonthlyExceptionShifts(false);
    CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts = pageIndex;
    Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(pageIndex, SearchItem_MonthlyExceptionShifts);
}

function Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(pageIndex, searchItem) {
    document.getElementById('loadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts').value);
    var pageSize = parseInt(document.getElementById('hfMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts').value);
    var PersonnelLoadStateConditions = '';
    var ObjDialogMonthlyExceptionShifts = parent.DialogMonthlyExceptionShifts.get_value();
    var Year = document.getElementById('hfCurrentYear_MonthlyExceptionShifts').value;
    var Month = document.getElementById('hfCurrentMonth_MonthlyExceptionShifts').value;
    CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts.callback(CharToKeyCode_MonthlyExceptionShifts(Year), CharToKeyCode_MonthlyExceptionShifts(Month), CharToKeyCode_MonthlyExceptionShifts(pageSize.toString()), CharToKeyCode_MonthlyExceptionShifts(pageIndex.toString()), CharToKeyCode_MonthlyExceptionShifts(searchItem));
}

function ChangeControlsEnabled_MonthlyExceptionShifts(enabled) {
    if (TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemShiftsView_TlbMonthlyExceptionShifts') != null) {
        if (!enabled) {
            TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemShiftsView_TlbMonthlyExceptionShifts').set_enabled(false);
            TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemShiftsView_TlbMonthlyExceptionShifts').set_imageUrl('add_silver.png');
        }
        else {
            TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemShiftsView_TlbMonthlyExceptionShifts').set_enabled(true);
            TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemShiftsView_TlbMonthlyExceptionShifts').set_imageUrl('add.png');
        }
        TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemPeriodRepeat_TlbMonthlyExceptionShifts').set_enabled(false);
        TlbMonthlyExceptionShifts.get_items().getItemById('tlbItemPeriodRepeat_TlbMonthlyExceptionShifts').set_imageUrl('cycle_Silver.png');
    }
}

function CharToKeyCode_MonthlyExceptionShifts(str) {
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

function Changefooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value) > 0 ? CurrentPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts').innerHTML = retfooterVal;
}

function ShowConnectionError_MonthlyExceptionShifts() {
    var error = document.getElementById('hfErrorType_MonthlyExceptionShifts').value;
    var errorBody = document.getElementById('hfConnectionError_MonthlyExceptionShifts').value;
    showDialog(error, errorBody, 'error');
}

function Refresh_GridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    CurrentPageState_MonthlyExceptionShifts = 'View';
    SearchItem_MonthlyExceptionShifts = '';
    LoadState_MonthlyExceptionShifts = 'Normal';
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0, SearchItem_MonthlyExceptionShifts);
}

function CloseDialogMonthlyExceptionShifts() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogMonthlyExceptionShifts_IFrame').src =parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogMonthlyExceptionShifts').Close();
}

function ChangeDirection_Container_GridMonthlyExceptionShifts_MonthlyExceptionShifts() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('Container_GridMonthlyExceptionShifts_MonthlyExceptionShifts').style.direction = 'ltr';
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

function CreatKeyboardMapping() {
    var mappingObjectArray = [
      mappingObject = {
          PrimaryName: 'A',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 65
              },
              UniqueCodeObject = {
                  UniqueCode: 97
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1588
              },
              UniqueCodeObject = {
                  UniqueCode: 1614
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'B',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 66
              },
              UniqueCodeObject = {
                  UniqueCode: 98
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1584
              },
              UniqueCodeObject = {
                  UniqueCode: 1573
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'C',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 67
              },
              UniqueCodeObject = {
                  UniqueCode: 99
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1586
              },
              UniqueCodeObject = {
                  UniqueCode: 1688
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'D',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 68
              },
              UniqueCodeObject = {
                  UniqueCode: 100
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1740
              },
              UniqueCodeObject = {
                  UniqueCode: 1610
              },
              UniqueCodeObject = {
                  UniqueCode: 1616
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'E',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 69
              },
              UniqueCodeObject = {
                  UniqueCode: 101
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1579
              },
              UniqueCodeObject = {
                  UniqueCode: 1613
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'F',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 70
              },
              UniqueCodeObject = {
                  UniqueCode: 102
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1576
              },
              UniqueCodeObject = {
                  UniqueCode: 1617
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'G',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 71
              },
              UniqueCodeObject = {
                  UniqueCode: 103
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1604
              },
              UniqueCodeObject = {
                  UniqueCode: 1728
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'H',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 72
              },
              UniqueCodeObject = {
                  UniqueCode: 104
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1575
              },
              UniqueCodeObject = {
                  UniqueCode: 1570
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'I',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 73
              },
              UniqueCodeObject = {
                  UniqueCode: 105
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1607
              },
              UniqueCodeObject = {
                  UniqueCode: 93
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'J',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 74
              },
              UniqueCodeObject = {
                  UniqueCode: 106
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1578
              },
              UniqueCodeObject = {
                  UniqueCode: 1600
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'K',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 75
              },
              UniqueCodeObject = {
                  UniqueCode: 107
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1606
              },
              UniqueCodeObject = {
                  UniqueCode: 171
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'L',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 76
              },
              UniqueCodeObject = {
                  UniqueCode: 108
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1605
              },
              UniqueCodeObject = {
                  UniqueCode: 187
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'M',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 77
              },
              UniqueCodeObject = {
                  UniqueCode: 109
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1574
              },
              UniqueCodeObject = {
                  UniqueCode: 1569
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'N',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 78
              },
              UniqueCodeObject = {
                  UniqueCode: 110
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1583
              },
              UniqueCodeObject = {
                  UniqueCode: 1571
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'O',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 79
              },
              UniqueCodeObject = {
                  UniqueCode: 111
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1582
              },
              UniqueCodeObject = {
                  UniqueCode: 91
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'P',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 80
              },
              UniqueCodeObject = {
                  UniqueCode: 112
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1581
              },
              UniqueCodeObject = {
                  UniqueCode: 92
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'Q',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 81
              },
              UniqueCodeObject = {
                  UniqueCode: 113
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1590
              },
              UniqueCodeObject = {
                  UniqueCode: 1611
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'R',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 82
              },
              UniqueCodeObject = {
                  UniqueCode: 114
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1602
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'S',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 83
              },
              UniqueCodeObject = {
                  UniqueCode: 115
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1587
              },
              UniqueCodeObject = {
                  UniqueCode: 1615
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'T',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 84
              },
              UniqueCodeObject = {
                  UniqueCode: 116
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1601
              },
              UniqueCodeObject = {
                  UniqueCode: 1548
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'U',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 85
              },
              UniqueCodeObject = {
                  UniqueCode: 117
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1593
              },
              UniqueCodeObject = {
                  UniqueCode: 44
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'V',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 86
              },
              UniqueCodeObject = {
                  UniqueCode: 118
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1585
              },
              UniqueCodeObject = {
                  UniqueCode: 1572
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'W',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 87
              },
              UniqueCodeObject = {
                  UniqueCode: 119
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1589
              },
              UniqueCodeObject = {
                  UniqueCode: 1612
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'X',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 88
              },
              UniqueCodeObject = {
                  UniqueCode: 120
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1591
              },
              UniqueCodeObject = {
                  UniqueCode: 1610
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'Y',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 89
              },
              UniqueCodeObject = {
                  UniqueCode: 121
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1594
              },
              UniqueCodeObject = {
                  UniqueCode: 1563
              }
          ]
      },
      mappingObject = {
          PrimaryName: 'Z',
          PrimaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 90
              },
              UniqueCodeObject = {
                  UniqueCode: 122
              }
          ],
          SecondaryUniqueCodeArray: [
              UniqueCodeObject = {
                  UniqueCode: 1592
              },
              UniqueCodeObject = {
                  UniqueCode: 1577
              }
          ]
      },
    ];
}
GridMonthlyExceptionShifts_MonthlyExceptionShifts_onEditKeyPress = function (e) {
    var keyCode = cart_browser_ie ? event.keyCode : e.which;
    if (keyCode == 13) {
        this.EditComplete();
        UpdateGridMonthlyExceptionShifts_MonthlyExceptionShifts();
        return false;
    } else {
        if (keyCode == 27 || keyCode == 0 || keyCode == 8) {
            if (keyCode == 27)
                this.EditCancel();
            return false;
        } else {
            if (KeyboardSetting == true)
                ConvertKeyCodeToEnglish(e);
            if (ShortcutKey == true) {
                if (PrimaryShiftCode == undefined)
                    ConvertToUniqueCode(e);
                GetShortcutKey(e);
            }
            else
                PrimaryShiftCode == undefined;
            this.EditingDirty = true;
            return true;
        }
    }
};
function ConvertKeyCodeToEnglish(e) {
    var keyCode = cart_browser_ie ? event.keyCode : e.which;
    var ShiftCode;
    $.each(mappingObjectArray, function (i, mappingObject) {
        return $.each(mappingObject.SecondaryUniqueCodeArray, function (j, UniqueCodeObject) {
            if (UniqueCodeObject.UniqueCode == keyCode) {
                ShiftCode = PrimaryShiftCode = mappingObject.PrimaryName;
                e.currentTarget.value = e.currentTarget.value + ShiftCode;
                e.preventDefault();
                return;
            }
        });
    });
}

function ConvertToUniqueCode(e) {
    var ShiftCode;
    var keyCode = cart_browser_ie ? event.keyCode : e.which;
    $.each(mappingObjectArray, function (i, mappingObject) {
        $.each(mappingObject.SecondaryUniqueCodeArray, function (j, UniqueCodeObject) {
            if (UniqueCodeObject.UniqueCode == keyCode) {
                PrimaryShiftCode = mappingObject.PrimaryName;
                return;
            }
        });
        $.each(mappingObject.PrimaryUniqueCodeArray, function (j, UniqueCodeObject) {
            if (UniqueCodeObject.UniqueCode == keyCode) {
                PrimaryShiftCode = mappingObject.PrimaryName;
                return;
            }
        });
    });
}

function GetShortcutKey(e) {
    objShifts_MonthlyExceptionShifts = eval(
        '(' + document.getElementById('hfShiftsObject_MonthlyExceptionShifts').value + ')');
    for (var item in objShifts_MonthlyExceptionShifts) {
        if (objShifts_MonthlyExceptionShifts[item].ShortcutsKey == PrimaryShiftCode && PrimaryShiftCode != undefined) {
            PrimaryShiftCode = undefined;
            e.currentTarget.value = objShifts_MonthlyExceptionShifts[item].CustomCode;
            e.preventDefault();
            return;
        }
    }
    PrimaryShiftCode = undefined;
    e.currentTarget.value = '';
    e.preventDefault();
}

function chbKeyboardSetting_MonthlyExceptionShifts_OnChange() {
    if (document.getElementById('chbKeyboardSetting_MonthlyExceptionShifts').checked)
        KeyboardSetting = true;
    else
        KeyboardSetting = false;
}
function chbShortcuts_MonthlyExceptionShifts_OnChange() {
    if (document.getElementById('chbShortcuts_MonthlyExceptionShifts').checked)
        ShortcutKey = true;
    else
        ShortcutKey = false;
}
//function btnTest_onClick() {
//    var x = mappingObjectArray;

//    var y = []

//    $.each(x, function(i, mappingObj){
//        return $.each(mappingObj.PrimaryUniqueCodeArray, function (j, UniqueCodeObj) {
//            if (UniqueCodeObj.UniqueCode == 88) {
//                y.push(mappingObj);
//                return;
//            }
//        })
//    });
//}
function tlbItemShiftsView_TlbMonthlyExceptionShifts_onClick() {
    document.getElementById('header_ShiftsView_MonthlyExceptionShifts').innerHTML = document.getElementById('hfheader_ShiftsView_MonthlyExceptionShifts').value;
    DialogShiftsView.Show();
    Fill_GridShiftsView('Normal');
}
function Fill_GridShiftsView(State, Searchvalue) {
    switch (State) {
        case 'Normal':
            CallBack_ShiftsView_MonthlyExceptionShifts.callback(CharToKeyCode_MonthlyExceptionShifts(State));
            break;
        case 'Search':
            CallBack_ShiftsView_MonthlyExceptionShifts.callback(CharToKeyCode_MonthlyExceptionShifts(State), CharToKeyCode_MonthlyExceptionShifts(Searchvalue));
            break;
    }
}
function CallBack_ShiftsView_MonthlyExceptionShifts_CallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ShiftsView_MonthlyExceptionShifts').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            Fill_GridShiftsView('Normal');
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}
function CallBack_ShiftsView_MonthlyExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyExceptionShifts();
}
function tlbItemExit_TlbShiftsView_onClick(event) {
    DialogShiftsView.Close();
}
function tlbItemSearch_TlbShiftSearch_onClick() {
    var SearchValue = document.getElementById('txtSearchTerm_MonthlyExceptionShifts').value;
    Fill_GridShiftsView('Search', SearchValue);
}
function txtSearchTerm_MonthlyExceptionShifts_onKeyPess(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbShiftSearch_onClick();
    }
}
function tlbItemRefresh_TlbShiftsView_onClick() {
    Fill_GridShiftsView('Normal');
}
function txtSerchExpression_MonthlyExceptionShifts_onKeyPess(event) {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItem_TlbSearch_MonthlyExceptionShifts_onClick();
    }
}
function tlbItem_TlbSearch_MonthlyExceptionShifts_onClick() {
    SearchItem_MonthlyExceptionShifts = document.getElementById('txtSerach_MonthlyExceptionShifts').value;
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0, SearchItem_MonthlyExceptionShifts);
}
function tlbItemPeriodRepeat_TlbMonthlyExceptionShifts_onClick() {
    document.getElementById('Title_DialogPeriodRepeat').innerHTML = document.getElementById('hfTitle_DialogPeriodRepeat').value;
    DialogPeriodRepeat.Show();
    Fill_cmbFromDay_MonthlyExceptionShifts();
    Fill_cmbToDay_MonthlyExceptionShifts();
}
function tlbItemExit_TlbPeriodRepeat_MonthlyExceptionShifts_onClick() {
    ClearHolidaysList_MonthlyExceptionShifts();
    DialogPeriodRepeat.Close();
}
function ClearHolidaysList_MonthlyExceptionShifts() {
    document.getElementById('cmbHolidays_MonthlyExceptionShifts_DropDown').style.display = 'none';
    CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbHolidays_MonthlyExceptionShifts = undefined;
    HolidaysList_MonthlyExceptionShifts = '';
}
function cmbFromDay_MonthlyExceptionShifts_onExpand(sender, e) {
    CollapseControls_MonthlyExceptionShifts(cmbFromDay_MonthlyExceptionShifts);
}
function Fill_cmbFromDay_MonthlyExceptionShifts() {
    ComboBox_onBeforeLoadData('cmbFromDay_MonthlyExceptionShifts');
    var Year = cmbYear_MonthlyExceptionShifts.getSelectedItem().get_value();
    var Month = cmbMonth_MonthlyExceptionShifts.getSelectedItem().get_value();
    CallBack_cmbFromDay_MonthlyExceptionShifts.callback(CharToKeyCode_MonthlyExceptionShifts(Year), CharToKeyCode_MonthlyExceptionShifts(Month));
}
function Fill_cmbToDay_MonthlyExceptionShifts() {
    ComboBox_onBeforeLoadData('cmbToDay_MonthlyExceptionShifts');
    var Year = cmbYear_MonthlyExceptionShifts.getSelectedItem().get_value();
    var Month = cmbMonth_MonthlyExceptionShifts.getSelectedItem().get_value();
    CallBack_cmbToDay_MonthlyExceptionShifts.callback(CharToKeyCode_MonthlyExceptionShifts(Year), CharToKeyCode_MonthlyExceptionShifts(Month));
}
function CallBack_cmbFromDay_MonthlyExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_FromDay_MonthlyExceptionShifts').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbFromDay_MonthlyExceptionShifts = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbFromDay_MonthlyExceptionShifts = false;
        document.getElementById('cmbFromDay_MonthlyExceptionShifts_DropDown').style.display = 'none';
        ChangeControlDirection_MonthlyExceptionShifts('cmbFromDay_MonthlyExceptionShifts_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbFromDay_MonthlyExceptionShifts_DropDown').style.display = 'none';
    }
}
function CallBack_cmbFromDay_MonthlyExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyExceptionShifts();
}
function cmbToDay_MonthlyExceptionShifts_onExpand(sender, e) {
    CollapseControls_MonthlyExceptionShifts(cmbToDay_MonthlyExceptionShifts);
}
function CallBack_cmbToDay_MonthlyExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ToDay_MonthlyExceptionShifts').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            CurrentPageCombosCallBcakStateObj.cmbToDay_MonthlyExceptionShifts = true;
        else
            CurrentPageCombosCallBcakStateObj.cmbToDay_MonthlyExceptionShifts = false;
        document.getElementById('cmbToDay_MonthlyExceptionShifts_DropDown').style.display = 'none';
        ChangeControlDirection_MonthlyExceptionShifts('cmbToDay_MonthlyExceptionShifts_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbToDay_MonthlyExceptionShifts_DropDown').style.display = 'none';
    }
}
function CallBack_cmbToDay_MonthlyExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyExceptionShifts();
}
function trvHolidays_MonthlyExceptionShifts_onNodeCheckChange(sender, e) {
    var holidayID = e.get_node().get_value();
    if (HolidaysList_MonthlyExceptionShifts.indexOf('#' + holidayID + '#') < 0)
        HolidaysList_MonthlyExceptionShifts += holidayID + '#';
    else
        HolidaysList_MonthlyExceptionShifts = HolidaysList_MonthlyExceptionShifts.replace('#' + holidayID + '#', '#');

    if (HolidaysList_MonthlyExceptionShifts.charAt(0) != '#')
        HolidaysList_MonthlyExceptionShifts = '#' + HolidaysList_MonthlyExceptionShifts;
    if (HolidaysList_MonthlyExceptionShifts.length == 1 && HolidaysList_MonthlyExceptionShifts.charAt(0) == '#')
        HolidaysList_MonthlyExceptionShifts = '';
}
function cmbHolidays_MonthlyExceptionShifts_onExpand(sender, e) {
    CollapseControls_MonthlyExceptionShifts(cmbHolidays_MonthlyExceptionShifts);
    if (cmbHolidays_MonthlyExceptionShifts.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbHolidays_MonthlyExceptionShifts == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbHolidays_MonthlyExceptionShifts = true;
        Fill_cmbHolidays_MonthlyExceptionShifts();
    }
}
function Fill_cmbHolidays_MonthlyExceptionShifts() {
    ComboBox_onBeforeLoadData('cmbHolidays_MonthlyExceptionShifts');
    CallBack_cmbHolidays_MonthlyExceptionShifts.callback();
}
function CollapseControls_MonthlyExceptionShifts(exception) {
    if (exception == null || exception != cmbFromDay_MonthlyExceptionShifts)
        cmbFromDay_MonthlyExceptionShifts.collapse();
    if (exception == null || exception != cmbToDay_MonthlyExceptionShifts)
        cmbToDay_MonthlyExceptionShifts.collapse();
    if (exception == null || exception != cmbHolidays_MonthlyExceptionShifts)
        cmbHolidays_MonthlyExceptionShifts.collapse();
}
function cmbHolidays_MonthlyExceptionShifts_onBeforeCallback(sender, e) {
    cmbHolidays_MonthlyExceptionShifts.dispose();
}
function cmbHolidays_MonthlyExceptionShifts_onCallbackError(sender, e) {
    ShowConnectionError_MonthlyExceptionShifts();
}
function ShowConnectionError_MonthlyExceptionShifts() {
    var error = document.getElementById('hfErrorType_MonthlyExceptionShifts').value;
    var errorBody = document.getElementById('hfConnectionError_MonthlyExceptionShifts').value;
    showDialog(error, errorBody, 'error');
}
function cmbHolidays_MonthlyExceptionShifts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Holidays').value;
    if (error == "") {
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbHolidays_MonthlyExceptionShifts_DropImage').mousedown();
        cmbHolidays_MonthlyExceptionShifts.expand();
        ChangeControlDirection_MonthlyExceptionShifts('cmbHolidays_MonthlyExceptionShifts_DropDownContent');
        ChangeDirection_trvHolidays_MonthlyExceptionShifts();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbHolidays_MonthlyExceptionShifts_DropDown').style.display = 'none';
    }
}
function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}
function ChangeControlDirection_MonthlyExceptionShifts(ctrl) {
    var direction = null;
    switch (parent.parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    if (ctrl == 'All') {
        document.getElementById('Mastertbl_MonthlyExceptionShifts').dir = document.getElementById('cmbFromDay_MonthlyExceptionShifts_DropDownContent').dir = document.getElementById('cmbToDay_MonthlyExceptionShifts_DropDownContent').dir = document.getElementById('cmbHolidays_MonthlyExceptionShifts_DropDownContent').dir = direction;
    }
    else
        document.getElementById(ctrl).style.direction = direction;
}
function ChangeDirection_trvHolidays_MonthlyExceptionShifts() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvHolidays_MonthlyExceptionShifts').style.direction = 'ltr';
}
function Refresh_cmbHolidays_MonthlyExceptionShifts() {
    Fill_cmbHolidays_MonthlyExceptionShifts();
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
function txtSearchTerm_MonthlyExceptionShifts_onKeyPess() {
    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        var SearchValue = document.getElementById('txtSearchTerm_MonthlyExceptionShifts').value;
        Fill_GridShiftsView('Search', SearchValue);
    }
}
