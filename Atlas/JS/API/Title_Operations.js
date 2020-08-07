

function tlbItemRegister_TlbRegister_Title_onClick() {
    ObjTitle_Title.title = document.getElementById('txtTitle_Title').value;
    Register_TitlePage(CharToKeyCode_Title(ObjTitle_Title.ReportParameterID), CharToKeyCode_Title(ObjTitle_Title.ReportParameterActionID), CharToKeyCode_Title(ObjTitle_Title.ReportFileID), CharToKeyCode_Title(ObjTitle_Title.title));
    DialogWaiting.Show();

}

function Register_TitlePage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Title').value;
            Response[1] = document.getElementById('hfConnectionError_Title').value;
        }
        if (RetMessage[2] == 'success')
            parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'DialogReportParameters_IFrame').contentWindow.ReportParametres_OnAfterUpdate(Response, ObjTitle_Title.ReportParameterID);
        else
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}


function SetReportParameterObj_Title() {
    var SelectedItems_GridReportParameters_ReportParameters = parent.GridReportParameters_ReportParameters.getSelectedItems();
    if (SelectedItems_GridReportParameters_ReportParameters.length > 0) {
        ObjTitle_Title = new Object();
        ObjTitle_Title.ReportParameterID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ID').get_text();
        ObjTitle_Title.ReportParameterActionID = SelectedItems_GridReportParameters_ReportParameters[0].getMember('ActionId').get_text();
        ObjTitle_Title.ReportFileID = parent.parent.DialogReportParameters.get_value().ReportFileID;
    }
}

function CharToKeyCode_Title(str) {
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


