


function tlbItemFormReconstruction_TlbOnlineTraffics_onClick() {
    ReconstructForm_DialogOnlineTraffics();
}

function ReconstructForm_DialogOnlineTraffics() {
    CloseDialogOnlineTraffics();
    parent.DialogOnlineTraffics.Show();
}

function CloseDialogOnlineTraffics() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogOnlineTraffics_iFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogOnlineTraffics').Close();
}

function tlbItemHelp_TlbOnlineTraffics_onClick() {
    LoadHelpPage('tlbItemHelp_TlbOnlineTraffics');
}

function tlbItemExit_TlbOnlineTraffics_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_DialogOnlineTraffics').value;
    DialogConfirm.Show();
}

function GridOnlineTraffics_OnlineTraffics_onLoad() {

}

function GridOnlineTraffics_OnlineTraffics_onItemSelect() {

}

function tlbItemOk_TlbOkConfirm_onClick() {
    DialogOnlineTraffics_onClose();
}

function DialogOnlineTraffics_onClose() {
    CloseDialogOnlineTraffics();
}

function CloseDialogOnlineTraffics() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogOnlineTraffics_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogOnlineTraffics').Close();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function GetHeaderGrid_OnlineTraffics() {
    document.getElementById('header_GridOnlineTraffics_OnlineTraffics').innerHTML = document.getElementById('hfheader_GridOnlineTraffics_OnlineTraffics').value;
}

function DialogOnlineTraffics_onClose() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogOnlineTraffics_IFrame').src = parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogOnlineTraffics').Close();
}

function GetBoxesHeaders_OnlineTraffics() {
    parent.document.getElementById('Title_DialogOnlineTraffics').innerHTML = document.getElementById('hfTitle_DialogOnlineTraffics').value;
}

function tlbItemFormReconstruction_TlbOnlineTraffics_GridTlbOnlineTraffics_onClick() {

}

function tlbItemFormReconstruction_TlbOnlineTraffics_GridTlbOnlineTraffics_onClick() {
  //  document.getElementById('loadingPanel_GridOnlineTraffics_OnlineTraffics').innerHTML = GetLoadingMessage(document.getElementById//('hfloadingPanel_GridOnlineTraffics_OnlineTraffics').value);
}


$(function () {
    var onlineTraffics = $.connection.OnlineTrafficsHub;
    onlineTraffics.client.RecieveTraffic = function (traffic) {
        if (traffic != null && traffic != "") {
            traffic = eval('(' + traffic + ')');
            Update_GridOnlineTraffics_OnlineTraffics(traffic);
        }
    };
    $.connection.hub.start().done(function () {
    }).fail(function (e) {
    });
});

function Update_GridOnlineTraffics_OnlineTraffics(traffic) {
    var GridItem = null;
    GridOnlineTraffics_OnlineTraffics.beginUpdate();
    GridOnlineTraffics_OnlineTraffics.unSelectAll();
    GridItem = GridOnlineTraffics_OnlineTraffics.get_table().addEmptyRow(GridOnlineTraffics_OnlineTraffics.get_recordCount());
    GridItem.setValue(0, traffic.ID, false);
    //GridOnlineTraffics_OnlineTraffics.selectByKey(traffic.ID, 0, false);
    GridItem.setValue(1, "<table style='width: 100%; height: 200px;'><tr><td align='center'><img style='height: 200px;' src='" + traffic.PersonImage + "'/></td></tr></table>", false);
    GridItem.setValue(2, traffic.PersonName, false);
    GridItem.setValue(3, traffic.TheDate, false);
    GridItem.setValue(4, traffic.TheTime, false);
    GridItem.setValue(5, traffic.MachineName, false);
    GridItem.setValue(6, traffic.TrafficType, false);
    GridItem.setValue(7, traffic.PersonBarcode, false);
    GridItem.setValue(8, traffic.PreCardName, false);
    GridItem.setValue(9, traffic.PersonID, false);
    GridOnlineTraffics_OnlineTraffics.endUpdate();
    GridOnlineTraffics_OnlineTraffics.get_table().sort("ID DESC");
    GridOnlineTraffics_OnlineTraffics.render();
}

function GetImagePersonel_OnlineTraffics(ImageName) {
    return "ImageViewer.aspx?reload=" + (new Date()).getTime() + "&AttachmentType=Personnel&ClientAttachment=" + CharToKeyCode_OnlineTraffics(ImageName);
}

function CharToKeyCode_OnlineTraffics(str) {
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

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}

