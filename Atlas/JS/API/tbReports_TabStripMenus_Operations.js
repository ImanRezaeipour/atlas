
var CurrentPageState_Reports = 'View';
var ConfirmState_Reports = null;
var ObjReport_Reports = null;
var CurrentPageMode_Reports = null;
var CurrentPageCombosCallBcakStateObj = new Object();
var SelectedReportFile_Reports = null;
var SelectedReportGroup_Reports = null;
var CurrentPageTreeViewsObj = new Object();
var chekedList_ReportAccessLevelsAsign = '';
var LoadState_Reports = 'Normal';

function GetBoxesHeaders_Reports() {
    document.getElementById('header_Reports_Reports').innerHTML = document.getElementById('hfheader_Reports_Reports').value;
    document.getElementById('header_ReportDetails_Reports').innerHTML = document.getElementById('hfheader_ReportDetails_Reports').value;
    document.getElementById('header_ReportGroupDetails_Reports').innerHTML = document.getElementById('hfheader_ReportGroupDetails_Reports').value;
    document.getElementById('cmbReportFiles_Reports_Input').value = document.getElementById('hfcmbAlarm_Reports').value;
    document.getElementById('clmnFileName_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnFileName_cmbReportFiles_Reports').value;
    document.getElementById('clmnDescription_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnDescription_cmbReportFiles_Reports').value;
    document.getElementById('header_ReportAccessLevels_Reports').innerHTML = document.getElementById('hfheader_ReportAccessLevels_Reports').value;
}

function CacheTreeViewsSize_Reports() {
    CurrentPageTreeViewsObj.trvReports_Reports = document.getElementById('trvReports_Reports').clientWidth + 'px';
}

function tlbItemNewGroup_TlbReports_onClick() {
    if (!CheckIsReport_Reports()) {
        CurrentPageMode_Reports = 'ReportGroup';
        ChangePageState_Reports('Add');
        ClearList_Reports();
    }
}

function tlbItemNewReport_TlbReports_onClick() {
    if (!CheckIsReport_Reports()) {
        CurrentPageMode_Reports = 'Report';
        ChangePageState_Reports('Add');
        ClearList_Reports();
    }
}

function CheckIsReport_Reports() {
    var isReport = false;
    var selectedNode_trvReports_Reports = trvReports_Reports.get_selectedNode();
    if (selectedNode_trvReports_Reports != undefined) {
        var TargetType = selectedNode_trvReports_Reports.get_value();
        TargetType = eval('(' + TargetType + ')').TargetType;
        switch (TargetType) {
            case 'Report':
                isReport = true;
                break;
            case 'ReportGroup':
                break;
        }
    }
    return isReport;
}

function tlbItemEdit_TlbReports_onClick() {
    if (trvReports_Reports.get_selectedNode() != null) {
        if (trvReports_Reports.get_selectedNode().get_parentNode() != undefined)
            ChangePageState_Reports('Edit');
    }
}

function tlbItemDelete_TlbReports_onClick() {
    ChangePageState_Reports('Delete');
}

function tlbItemSave_TlbReports_onClick() {
    Report_onSave();
}

function tlbItemCancel_TlbReports_onClick() {
    DialogConfirm.Close();
    ChangePageState_Reports('View');
}

function tlbItemExit_TlbReports_onClick() {
    ShowDialogConfirm('Exit');
}

function Refresh_trvReports_Reports() {
    LoadState_Reports = "Normal";
    document.getElementById('txtQuickSearch_Reports').value = "";
    Fill_trvReports_Reports();
}

function trvReports_Reports_onNodeSelect(sender, e) {
    if (CurrentPageState_Reports != 'Add')
        NavigateReport_Reports(e.get_node());
    else {
        var TargetType = eval('(' + e.get_node().get_value() + ')').TargetType;
        switch (TargetType) {
            case 'Report':
                ChangePageState_Reports('View');
                break;
            case 'ReportGroup':
                break;
        }
    }
    CheckType_Reports(e.get_node());
}

function trvReports_Reports_NodeMouseDoubleClick(sender, e) {
    var trvNodeVal = eval('(' + e.get_node().get_value() + ')');
    var TargetType = trvNodeVal.TargetType;
    switch (TargetType) {
        case 'Report':
            if (trvNodeVal.HasParameter) {
                if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null)
                    ShowDialogReportParameters_Reports();
            }
            else
                if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null)
                    GetReport_Reports();
            break;
        case 'ReportGroup':
            break;
    }
}

function CheckType_Reports(trvNode) {
    var trvNodeVal = eval('(' + trvNode.get_value() + ')');
    switch (trvNodeVal.TargetType) {
        case 'Report':
            var IsReportHasParameter = trvNodeVal.HasParameter;
            if (IsReportHasParameter) {
                if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_enabled(true);
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_imageUrl('regulation.png');
                }
                if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_enabled(false);
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_imageUrl('Report_silver.png');
                }
            }
            else {
                if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_enabled(false);
                    TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_imageUrl('regulation_silver.png');
                }
                if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null) {
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_enabled(true);
                    TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_imageUrl('Report.png');
                }
            }
            break;
        case 'ReportGroup':
            if (TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports') != null) {
                TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_enabled(false);
                TlbReports.get_items().getItemById('tlbItemReportsParametersRegulation_TlbReports').set_imageUrl('regulation_silver.png');
            }
            if (TlbReports.get_items().getItemById('tlbItemReportView_TlbReports') != null) {
                TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_enabled(false);
                TlbReports.get_items().getItemById('tlbItemReportView_TlbReports').set_imageUrl('Report_silver.png');
            }
            break;
    }
}

function trvReports_Reports_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvReports_Reports').innerHTML = '';
}

function CallBack_trvReports_Reports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Reports').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvReports_Reports();
    }
    else {
        Resize_trvReports_Reports();
        ChangeDirection_trvReports_Reports();
    }
}

function CallBack_trvReports_Reports_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvReports_Reports').innerHTML = '';
    ShowConnectionError_Reports();
}

function cmbReportFiles_Reports_onExpand(sender, e) {
    SetPosition_cmbReportFiles_Reports();
    if (cmbReportFiles_Reports.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportFiles_Reports == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportFiles_Reports = true;
        Fill_cmbReportFiles_Reports();
    }
}

function SetPosition_cmbReportFiles_Reports() {
    var is_ie = ((navigator.userAgent.indexOf('msie') >= 0) || ((navigator.appName == 'Netscape') && (new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})").exec(navigator.userAgent) != null)));
    if (is_ie) {
        var left = document.getElementById('cmbReportFiles_Reports_DropDown').style.left.replace('px', '');
        left = parseInt(left);
        document.getElementById('cmbReportFiles_Reports_DropDown').style.left = left - $(window).scrollLeft() + 'px';
    }
    else
        window.scroll(0, 0);
}


function Fill_cmbReportFiles_Reports() {
    ComboBox_onBeforeLoadData('cmbReportFiles_Reports');
    CallBack_cmbReportFiles_Reports.callback();
}

function cmbReportFiles_Reports_onCollapse(sender, e) {

    if (cmbReportFiles_Reports.getSelectedItem() == undefined && SelectedReportFile_Reports == null)
        document.getElementById('cmbReportFiles_Reports_Input').value = document.getElementById('hfcmbAlarm_Reports').value;
    else {
        if (cmbReportFiles_Reports.getSelectedItem() != undefined && SelectedReportFile_Reports != null)
            document.getElementById('cmbReportFiles_Reports_Input').value = cmbReportFiles_Reports.getSelectedItem().get_text();
        else {
            if (cmbReportFiles_Reports.getSelectedItem() == undefined && SelectedReportFile_Reports != null)
                document.getElementById('cmbReportFiles_Reports_Input').value = SelectedReportFile_Reports.Description;
        }
    }
}

function CallBack_cmbReportFiles_Reports_onBeforeCallback(sender, e) {
    cmbReportFiles_Reports.dispose();
}

function CallBack_cmbReportFiles_Reports_onCallbackComplete(sender, e) {
    document.getElementById('clmnFileName_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnFileName_cmbReportFiles_Reports').value;
    document.getElementById('clmnDescription_cmbReportFiles_Reports').innerHTML = document.getElementById('hfclmnDescription_cmbReportFiles_Reports').value;

    var error = document.getElementById('ErrorHiddenField_ReportFiles_Reports').value;
    if (error == "") {
        document.getElementById('cmbReportFiles_Reports_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbReportFiles_Reports_DropImage').mousedown();
        else
            cmbReportFiles_Reports.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbReportFiles_Reports_DropDown').style.display = 'none';
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Reports) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateReport_Reports();
            break;
        case 'Exit':
            ClearList_Reports();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Reports('View');
}

function ChangePageState_Reports(state) {
    CurrentPageState_Reports = state;
    SetActionMode_Reports(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_imageUrl('group_silver.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_imageUrl('newReport_silver.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemEdit_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_imageUrl('edit_silver.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemDelete_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_enabled(false);
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_imageUrl('remove_silver.png');
        }
        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_enabled(true);
        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_imageUrl('save.png');
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_enabled(true);
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_imageUrl('cancel.png');
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                document.getElementById('txtReportGroupName_Reports').disabled = '';
                cmbReportFiles_Reports.disable();
                cmbReportGroup_Reports.disable();
                break;
            case 'Report':
                document.getElementById('txtReportName_Reports').disabled = '';
                cmbReportFiles_Reports.enable();
                if (state == 'Edit')
                    cmbReportGroup_Reports.enable();
                break;
        }
        if (state == 'Edit')
            NavigateReport_Reports(trvReports_Reports.get_selectedNode());
        if (state == 'Delete')
            Report_onSave();
    }
    if (state == 'View') {
        if (TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemNewGroup_TlbReports').set_imageUrl('group.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemNewReport_TlbReports').set_imageUrl('newReport.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemEdit_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemEdit_TlbReports').set_imageUrl('edit.png');
        }
        if (TlbReports.get_items().getItemById('tlbItemDelete_TlbReports') != null) {
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_enabled(true);
            TlbReports.get_items().getItemById('tlbItemDelete_TlbReports').set_imageUrl('remove.png');
        }

        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_enabled(false);
        TlbReports.get_items().getItemById('tlbItemSave_TlbReports').set_imageUrl('save_silver.png');
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_enabled(false);
        TlbReports.get_items().getItemById('tlbItemCancel_TlbReports').set_imageUrl('cancel_silver.png');
        document.getElementById('txtReportGroupName_Reports').disabled = 'disabled';
        document.getElementById('txtReportName_Reports').disabled = 'disabled';
        cmbReportFiles_Reports.disable();
        cmbReportGroup_Reports.disable();
        var objSelectedNode = trvReports_Reports.get_selectedNode();
        if (objSelectedNode != undefined && objSelectedNode != null) {
            if (objSelectedNode.get_parentNode().get_text() != '' && SelectedReportFile_Reports != null)
                document.getElementById('cmbReportGroup_Reports_Input').value = objSelectedNode.get_parentNode().get_text();
        }
    }
}

function SetActionMode_Reports(state) {
    document.getElementById('ActionMode_Reports').innerHTML = document.getElementById('hf' + state + '_Reports').value + (CurrentPageState_Reports != 'View' ? ' ' + document.getElementById('hf' + CurrentPageMode_Reports + '_Reports').value : '');
}

function NavigateReport_Reports(selectedReportNode) {
    cmbReportGroup_Reports.unSelect();
    cmbReportFiles_Reports.unSelect();
    if (selectedReportNode != undefined) {
        ObjTarget_Reports = selectedReportNode.get_value();
        ObjTarget_Reports = eval('(' + ObjTarget_Reports + ')');
        CurrentPageMode_Reports = ObjTarget_Reports.TargetType;
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                document.getElementById('txtReportGroupName_Reports').value = selectedReportNode.get_text();
                document.getElementById('txtReportName_Reports').value = '';
                document.getElementById('cmbReportFiles_Reports_Input').value = '';
                document.getElementById('cmbReportGroup_Reports_Input').value = '';
                cmbReportFiles_Reports.disable();
                cmbReportGroup_Reports.disable();
                SelectedReportFile_Reports = null;
                break;
            case 'Report':
                SelectedReportFile_Reports = new Object();
                SelectedReportGroup_Reports = new Object();
                SelectedReportFile_Reports.ID = selectedReportNode.get_id();
                document.getElementById('txtReportName_Reports').value = selectedReportNode.get_text();
                document.getElementById('cmbReportGroup_Reports_Input').value = selectedReportNode.get_parentNode().get_text();
                document.getElementById('txtReportGroupName_Reports').value = '';
                document.getElementById('cmbReportFiles_Reports_Input').value = SelectedReportFile_Reports.Description = ObjTarget_Reports.Description;
                if (ObjTarget_Reports.IsDesigned) {
                    cmbReportFiles_Reports.disable();
                    cmbReportFiles_Reports.unSelect();
                }
                SelectedReportFile_Reports.FileID = ObjTarget_Reports.FileID;
                SelectedReportFile_Reports.FileName = ObjTarget_Reports.FileName;
                SelectedReportGroup_Reports.ParentNodeId = selectedReportNode.get_parentNode().get_id();
                SelectedReportGroup_Reports.ParentNodeName = selectedReportNode.get_parentNode().get_text();
                break;
        }
    }
}

function Report_onSave() {
    if (CurrentPageState_Reports != 'Delete')
        UpdateReport_Reports();
    else
        ShowDialogConfirm('Delete');
}

function UpdateReport_Reports() {
    ObjReport_Reports = new Object();
    ObjReport_Reports.TargetType = null;
    ObjReport_Reports.ReportParentId = '0';
    ObjReport_Reports.SelectedID = '0';
    ObjReport_Reports.Name = null;
    ObjReport_Reports.ReportFileID = '0';
    ObjReport_Reports.HasParameter = false;
    ObjReport_Reports.ReportFileName = null;
    ObjReport_Reports.ReportFileDescription = null;
    ObjReport_Reports.IsDesigned = false;
    ObjReport_Reports.DesignedTypeCustomCode = null;

    ObjReport_Reports.TargetType = CurrentPageMode_Reports;
    var SelectedReportNode_Reports = trvReports_Reports.get_selectedNode();
    if (SelectedReportNode_Reports != undefined) {
        var SelectedReportNodeValue_Reports = trvReports_Reports.get_selectedNode().get_value();
        SelectedReportNodeValue_Reports = eval('(' + SelectedReportNodeValue_Reports + ')');
        ObjReport_Reports.SelectedID = SelectedReportNode_Reports.get_id();
        ObjReport_Reports.HasParameter = SelectedReportNodeValue_Reports.HasParameter;
        ObjReport_Reports.IsDesigned = SelectedReportNodeValue_Reports.IsDesigned;
        ObjReport_Reports.DesignedTypeCustomCode = SelectedReportNodeValue_Reports.DesignedTypeCustomCode;
        ObjReport_Reports.IsContainsForm = SelectedReportNodeValue_Reports.IsContainsForm;
    }
    if (CurrentPageState_Reports != 'Delete') {
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                if (SelectedReportNode_Reports != undefined) {
                    if (SelectedReportNode_Reports.get_parentNode() != undefined)
                        ObjReport_Reports.ReportParentId = SelectedReportNode_Reports.get_parentNode().get_id();
                }
                ObjReport_Reports.Name = document.getElementById('txtReportGroupName_Reports').value;
                break;
            case 'Report':
                ObjReport_Reports.Name = document.getElementById('txtReportName_Reports').value;
                var SelectedItem_cmbReportFiles_Reports = cmbReportFiles_Reports.getSelectedItem();
                if (SelectedItem_cmbReportFiles_Reports != undefined) {
                    ObjReport_Reports.ReportFileID = SelectedItem_cmbReportFiles_Reports.get_value();
                    ObjReport_Reports.ReportFileName = SelectedItem_cmbReportFiles_Reports.Name;
                    ObjReport_Reports.ReportFileDescription = SelectedItem_cmbReportFiles_Reports.get_text();
                }
                else {
                    if (SelectedReportFile_Reports != null) {
                        ObjReport_Reports.ReportFileID = SelectedReportFile_Reports.FileID;
                        ObjReport_Reports.ReportFileName = SelectedReportFile_Reports.FileName;
                        ObjReport_Reports.ReportFileDescription = SelectedReportFile_Reports.Description;
                    }
                }
                var SelectedItem_cmbReportGroup_Reports = cmbReportGroup_Reports.getSelectedItem();
                if (SelectedItem_cmbReportGroup_Reports != null) {
                    ObjReport_Reports.ReportParentId = SelectedItem_cmbReportGroup_Reports.get_value();

                }
                else {
                    if (SelectedReportFile_Reports != null) {
                        ObjReport_Reports.ReportParentId = SelectedReportGroup_Reports.ParentNodeId;

                    }
                }


                break;
        }
    }
    UpdateReport_ReportsPage(CharToKeyCode_Reports(CurrentPageState_Reports), CharToKeyCode_Reports(ObjReport_Reports.TargetType), CharToKeyCode_Reports(ObjReport_Reports.SelectedID), CharToKeyCode_Reports(ObjReport_Reports.Name), CharToKeyCode_Reports(ObjReport_Reports.ReportFileID), CharToKeyCode_Reports(ObjReport_Reports.ReportParentId), CharToKeyCode_Reports(ObjReport_Reports.IsDesigned.toString()));
    DialogWaiting.Show();
}

function UpdateReport_ReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Reports').value;
            Response[1] = document.getElementById('hfConnectionError_Reports').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearList_Reports();
            Report_OnAfterUpdate(Response);
            ChangePageState_Reports('View');
        }
        else {
            if (CurrentPageState_Reports == 'Delete')
                ChangePageState_Reports('View');
        }
    }
}

function Report_OnAfterUpdate(Response) {
    var ReportNodeText = ObjReport_Reports.Name;
    var ReportNodeValue = '{"TargetType":"' + ObjReport_Reports.TargetType + '","FileID":"' + ObjReport_Reports.ReportFileID + '","FileName":"' + ObjReport_Reports.ReportFileName + '","Description":"' + ObjReport_Reports.ReportFileDescription + '","HasParameter":"' + ObjReport_Reports.HasParameter + '","IsDesigned":"' + ObjReport_Reports.IsDesigned + '","DesignedTypeCustomCode":"' + ObjReport_Reports.DesignedTypeCustomCode + '","IsContainsForm":"' + ObjReport_Reports.IsContainsForm + '"}';

    trvReports_Reports.beginUpdate();
    switch (CurrentPageState_Reports) {
        case 'Add':
            var newReportNode = new ComponentArt.Web.UI.TreeViewNode();
            newReportNode.set_text(ReportNodeText);
            newReportNode.set_value(ReportNodeValue);
            newReportNode.set_id(Response[3]);
            var imageName = null;
            switch (ObjReport_Reports.TargetType) {
                case 'ReportGroup':
                    imageName = 'group.png';
                    break;
                case 'Report':
                    imageName = 'report.png';
                    break;
            }
            newReportNode.set_imageUrl('Images/TreeView/' + imageName);
            trvReports_Reports.findNodeById(ObjReport_Reports.SelectedID).get_nodes().add(newReportNode);
            trvReports_Reports.selectNodeById(ObjReport_Reports.SelectedID);
            break;
        case 'Edit':
            var selectedReportNode = trvReports_Reports.findNodeById(Response[3]);
            selectedReportNode.set_text(ReportNodeText);
            selectedReportNode.set_value(ReportNodeValue);
            if (cmbReportGroup_Reports.getSelectedItem() != null) {
                if (cmbReportGroup_Reports.getSelectedItem().get_value() != SelectedReportGroup_Reports.ParentNodeId)
                    Fill_trvReports_Reports();
            }
            trvReports_Reports.selectNodeById(Response[3]);
            break;
        case 'Delete':
            trvReports_Reports.findNodeById(ObjReport_Reports.SelectedID).remove();
            break;
    }
    trvReports_Reports.endUpdate();
    if (CurrentPageState_Reports == 'Add')
        trvReports_Reports.get_selectedNode().expand();
}

function CharToKeyCode_Reports(str) {
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
    ConfirmState_Reports = confirmState;
    if (CurrentPageState_Reports == 'Delete') {
        var confirmMessage = null;
        switch (CurrentPageMode_Reports) {
            case 'ReportGroup':
                confirmMessage = document.getElementById('hfReportGroupDeleteMessage_Reports').value;
                break;
            case 'Report':
                confirmMessage = document.getElementById('hfReportDeleteMessage_Reports').value;
                break;
        }
        document.getElementById('lblConfirm').innerHTML = confirmMessage;
    }
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Reports').value;
    DialogConfirm.Show();
    CollapseControls_Reports();
}

function ClearList_Reports() {
    document.getElementById('txtReportGroupName_Reports').value = '';
    document.getElementById('txtReportName_Reports').value = '';
    document.getElementById('cmbReportFiles_Reports_Input').value = document.getElementById('hfcmbAlarm_Reports').value;
    cmbReportFiles_Reports.unSelect();
}

function Fill_trvReports_Reports() {
    switch (LoadState_Reports) {
        case 'Normal':
            var SearchItem = '';
            break;
        case 'Search':
            var SearchItem = document.getElementById('txtQuickSearch_Reports').value;
            break;
    }

    document.getElementById('loadingPanel_trvReports_Reports').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvReports_Reports').value);
    CallBack_trvReports_Reports.callback(CharToKeyCode_Reports(LoadState_Reports), CharToKeyCode_Reports(SearchItem));
}

function ShowConnectionError_Reports() {
    var error = document.getElementById('hfErrorType_Reports').value;
    var errorBody = document.getElementById('hfConnectionError_Reports').value;
    showDialog(error, errorBody, 'error');
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function tlbItemReportsParametersRegulation_TlbReports_onClick() {
    ShowDialogReportParameters_Reports();
}

function tlbItemReportView_TlbReports_onClick() {
    GetReport_Reports();
}

function GetReport_Reports() {
    if (trvReports_Reports.get_selectedNode() != undefined) {
        var reportObj = trvReports_Reports.get_selectedNode().get_value();
        reportObj = eval('(' + reportObj + ')');
        if (reportObj.TargetType == 'Report' && !reportObj.HasParameter) {
            ObjReport_Reports = new Object();
            ObjReport_Reports.ReportFileID = reportObj.FileID;
            ObjReport_Reports.ReportFileName = reportObj.FileName;
            ObjReport_Reports.ReportName = trvReports_Reports.get_selectedNode().get_text();
            ObjReport_Reports.Description = reportObj.Description;
            ObjReport_Reports.HasParameter = reportObj.HasParameter;
            ObjReport_Reports.IsDesigned = reportObj.IsDesigned;
            ObjReport_Reports.DesignedTypeCustomCode = reportObj.DesignedTypeCustomCode;
            ObjReport_Reports.IsContainsForm = reportObj.IsContainsForm;
            GetReport_ReportsPage(CharToKeyCode_Reports(ObjReport_Reports.ReportFileID));
            DialogWaiting.Show();
        }
    }
}

function GetReport_ReportsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Reports').value;
            Response[1] = document.getElementById('hfConnectionError_Reports').value;
        }
        if (RetMessage[2] == 'success')
            ShowReport_Reports(Response);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function ShowReport_Reports(Response) {
    var stiReportGUID = Response[3];
    var reportName = null;
    var target = null;
    if (ObjReport_Reports != null) {
        if (ObjReport_Reports.IsContainsForm)
            target = 'ReportViewer';
        else
            target = 'MasterReportViewer';
        var NewReportWindow = window.open("" + target + ".aspx?ReportGUID=" + stiReportGUID + "&ReportTitle=" + CharToKeyCode_Reports(ObjReport_Reports.reportName) + "&IsDesigned=" + CharToKeyCode_Reports(ObjReport_Reports.IsDesigned.toString()) + "&IsContainsForm=" + CharToKeyCode_Reports(ObjReport_Reports.IsContainsForm.toString()) + "&Width=" + CharToKeyCode_Reports((screen.width).toString()) + "&Height=" + CharToKeyCode_Reports((screen.height).toString()) + "", "" + target + "" + (new Date()).getTime() + "", "width=" + screen.width + ",height=" + screen.height + ",toolbar=yes,location=yes,directories=yes,status=yes,menubar=yes,scrollbars=yes,copyhistory=yes,resizable=yes");
    }
}

function ShowDialogReportParameters_Reports() {
    if (trvReports_Reports.get_selectedNode() != undefined) {
        var reportObj = trvReports_Reports.get_selectedNode().get_value();
        reportObj = eval('(' + reportObj + ')');
        switch (reportObj.TargetType) {
            case 'Report':
                var ObjReportParameters = new Object();
                ObjReportParameters.ReportID = trvReports_Reports.get_selectedNode().get_id();
                ObjReportParameters.ReportFileID = reportObj.FileID;
                ObjReportParameters.ReportName = trvReports_Reports.get_selectedNode().get_text();
                ObjReportParameters.ReportIsDesigned = reportObj.IsDesigned;
                ObjReportParameters.ReportTypeCustomCode = reportObj.DesignedTypeCustomCode;
                ObjReportParameters.IsContainsForm = reportObj.IsContainsForm;
                parent.DialogReportParameters.set_value(ObjReportParameters);
                parent.DialogReportParameters.Show();
                break;
            case 'ReportGroup':
                break;
        }
        CollapseControls_Reports();
    }
}

function CallBack_cmbReportFiles_Reports_onCallbackError(sender, e) {
    ShowConnectionError_Reports();
}

function CollapseControls_Reports() {
    cmbReportFiles_Reports.collapse();
}

function tlbItemFormReconstruction_TlbReports_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvReportsIntroduction_iFrame').src = parent.ModulePath + 'Reports.aspx';
}

function tlbItemHelp_TlbReports_onClick() {
    LoadHelpPage('tlbItemHelp_TlbReports');
}

function trvReports_Reports_NodeCheckChange(sender, e) {
    Resize_trvReports_Reports();
    ChangeDirection_trvReports_Reports();
}

function Resize_trvReports_Reports() {
    document.getElementById('trvReports_Reports').style.width = CurrentPageTreeViewsObj.trvReports_Reports;
}

function ChangeDirection_trvReports_Reports() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvReports_Reports').style.direction = 'ltr';
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



function tlbItemSave_TlbReportAccessLevels_onClick() {
    UpdateReportAccessLevelsAsign_Reports();
}

function UpdateReportAccessLevelsAsign_Reports() {
    var reportID = trvReports_Reports.get_selectedNode().get_id();
    var parentNodesCol = trvReportAccessLevels_Reports.get_nodes();
    for (var i = 0; i < parentNodesCol.get_length() ; i++) {
        var parentNode = parentNodesCol.getNode(i);
        GetChildNodesCheck_trvReportAccessLevels_Reports(parentNode);
    }
    if (chekedList_ReportAccessLevelsAsign.charAt(chekedList_ReportAccessLevelsAsign.length - 1) == ',')
        chekedList_ReportAccessLevelsAsign = chekedList_ReportAccessLevelsAsign.substring(0, chekedList_ReportAccessLevelsAsign.length - 1);
    chekedList_ReportAccessLevelsAsign = '[' + chekedList_ReportAccessLevelsAsign + ']';
    UpdateReportAccessLevelsAsign_ReportPage(CharToKeyCode_Reports(reportID), CharToKeyCode_Reports(chekedList_ReportAccessLevelsAsign));
    DialogWaiting.Show();
}
function GetChildNodesCheck_trvReportAccessLevels_Reports(parentNode) {
    var childNodesCol = parentNode.get_nodes();
    for (var i = 0; i < childNodesCol.get_length() ; i++) {
        var childNode = childNodesCol.getNode(i);
        if (childNode.get_checked())
            chekedList_ReportAccessLevelsAsign += childNode.get_id() + ',';
        if (childNode != undefined)
            GetChildNodesCheck_trvReportAccessLevels_Reports(childNode);
    }
}
function UpdateReportAccessLevelsAsign_ReportPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        chekedList_ReportAccessLevelsAsign = '';
    }
}

function tlbItemExit_TlbReportAccessLevels_onClick() {
    CloseDialogReportAccessLevels();
}

function CloseDialogReportAccessLevels() {
    ClearNodes_trvReportAccessLevels_Reports();
    DialogReportAccessLevels.Close();
}

function ClearNodes_trvReportAccessLevels_Reports() {
    trvReportAccessLevels_Reports.beginUpdate();
    trvReportAccessLevels_Reports.get_nodes().clear();
    trvReportAccessLevels_Reports.endUpdate();
}

function Refresh_trvReportAccessLevels_Reports() {
    Fill_trvReportAccessLevels_Reports();
}

function Fill_trvReportAccessLevels_Reports() {

    if (trvReports_Reports.get_selectedNode() != undefined) {
        document.getElementById('loadingPanel_trvReportAccessLevels_Reports').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvReportAccessLevels_Reports').value);
        var reportID = trvReports_Reports.get_selectedNode().get_id();
        CallBack_trvReportAccessLevels_Reports.callback(CharToKeyCode_Reports(reportID));
    }
}
function trvReportAccessLevels_Reports_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvReportAccessLevels_Reports').innerHTML = '';
}

function trvReportAccessLevels_Reports_onNodeCheckChange(sender, e) {
    var parentNode = e.get_node();
    var checked = parentNode.get_checked();
    ChangeChildNodesCheck_trvReportAccessLevels_Reports(parentNode, checked);
}

function ChangeChildNodesCheck_trvReportAccessLevels_Reports(parentNode, checked) {
    var childNodesCol = parentNode.get_nodes();
    for (var i = 0; i < childNodesCol.get_length() ; i++) {
        var childNode = childNodesCol.getNode(i);
        childNode.set_checked(checked);
        ChangeChildNodesCheck_trvReportAccessLevels_Reports(childNode, checked);
    }
}

function trvReportAccessLevels_Reports_onNodeExpand(sender, e) {
    Resize_trvReportAccessLevels_Reports();
    ChangeDirection_trvReportAccessLevels_Reports();
}

function Resize_trvReportAccessLevels_Reports() {
    document.getElementById('trvReportAccessLevels_Reports').style.width = CurrentPageTreeViewsObj.trvReportAccessLevels_Reports;
}

function ChangeDirection_trvReportAccessLevels_Reports() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvReportAccessLevels_Reports').style.direction = 'ltr';
}

function CallBack_trvReportAccessLevels_Reports_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_ReportAccessLevels').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvReportAccessLevels_Reports();
    }
    else {
        Resize_trvReportAccessLevels_Reports();
        ChangeDirection_trvReportAccessLevels_Reports();
    }
}

function CallBack_trvReportAccessLevels_Reports_onCallbackError(sender, e) {
    ShowConnectionError_Reports();
}

function tlbItemReportAccessLevels_TlbReports_onClick() {
    ShowDialogReportAccessLevels();
}

function ShowDialogReportAccessLevels() {
    if (trvReports_Reports.get_selectedNode() != undefined) {
        Fill_trvReportAccessLevels_Reports();
        DialogReportAccessLevels.Show();
    }
}
function cmbReportGroup_Reports_onExpand(sender, e) {
    SetPosition_cmbReportGroup_Reports();
    if (cmbReportGroup_Reports.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportGroup_Reports == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbReportGroup_Reports = true;
        Fill_cmbReportGroup_Reports();
    }
}
function Fill_cmbReportGroup_Reports() {
    ComboBox_onBeforeLoadData('cmbReportGroup_Reports');
    CallBack_cmbReportGroup_Reports.callback();
}
function cmbReportGroup_Reports_onCollapse(sender, e) {
    if (cmbReportGroup_Reports.getSelectedItem() == undefined && SelectedReportGroup_Reports == null)

        document.getElementById('cmbReportGroup_Reports_Input').value = document.getElementById('hfcmbAlarm_Reports').value;
    else {
        if (cmbReportGroup_Reports.getSelectedItem() != undefined && SelectedReportGroup_Reports != null)
            document.getElementById('cmbReportGroup_Reports_Input').value = cmbReportGroup_Reports.getSelectedItem().get_text();
        else
            if (cmbReportGroup_Reports.getSelectedItem() == undefined && SelectedReportGroup_Reports != null) {
                document.getElementById('cmbReportGroup_Reports_Input').value = SelectedReportGroup_Reports.ParentNodeName;
            }
    }

}
function CallBack_cmbReportGroup_Reports_onBeforeCallback(sender, e) {
    cmbReportGroup_Reports.dispose();
}
function CallBack_cmbReportGroup_Reports_onCallbackComplete(sender, e) {

    var error = document.getElementById('ErrorHiddenField_ReportFiles_Reports').value;
    if (error == "") {
        document.getElementById('cmbReportGroup_Reports_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbReportGroup_Reports_DropImage').mousedown();
        else
            cmbReportGroup_Reports.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbReportGroup_Reports_DropDown').style.display = 'none';
    }
}

function CallBack_cmbReportGroup_Reports_onCallbackError(sender, e) {
    ShowConnectionError_Reports();
}

function SetPosition_cmbReportGroup_Reports() {
    var is_ie = ((navigator.userAgent.indexOf('msie') >= 0) || ((navigator.appName == 'Netscape') && (new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})").exec(navigator.userAgent) != null)));
    if (is_ie) {
        var left = document.getElementById('cmbReportGroup_Reports_DropDown').style.left.replace('px', '');
        left = parseInt(left);
        document.getElementById('cmbReportGroup_Reports_DropDown').style.left = left - $(window).scrollLeft() + 'px';
    }
    else
        window.scroll(0, 0);
}


function tlbItemSearch_TlbQuickSearch_Reports_onClick() {
    if (document.getElementById('txtQuickSearch_Reports').value != '')
        LoadState_Reports = 'Search';
    else
        LoadState_Reports = 'Normal';
    Fill_trvReports_Reports();
}