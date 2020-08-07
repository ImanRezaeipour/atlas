
function GetBoxesHeaders_PrivateNews() {
    var boxHeader = document.getElementById('hfheader_PrivateNews').value;
    if (this.frameElement.id != 'MainViewMaximizedPartIFrame_MainView')
        parent.document.getElementById('header_' + this.frameElement.id).innerHTML = document.getElementById('hfheader_PrivateNews').value;
    else
        parent.document.getElementById('Title_DialogMainViewMaximizedPart').innerHTML = boxHeader;
}

function GetErrorMessage_PrivateNews() {
    var errorMessage = document.getElementById('ErrorHiddenField_PrivateNews').value;
    if (errorMessage != '' && errorMessage != undefined) {
        errorMessage = eval('(' + errorMessage + ')');
        if (errorMessage[2] != 'success')
            showDialog(errorMessage[0], errorMessage[1], errorMessage[2]);
    }
}
function InitializeLink_PrivateNews(keyItem, lastRequestDate) {
    if (keyItem == 'MainRecievedRequestCount' || keyItem == 'SubstituteRecievedRequestCount' || keyItem == 'UnderReviewRequestSubstituteRequestsCount') {
        var ObjDialogKartable = new Object();
        if (keyItem != 'UnderReviewRequestSubstituteRequestsCount')
            ObjDialogKartable.RequestCaller = 'Kartable';
        else
            ObjDialogKartable.RequestCaller = 'RequestSubstituteKartable';
        ObjDialogKartable.Applicant = 'PrivateNews';
        ObjDialogKartable.KeyApplicant = keyItem;
        ObjDialogKartable.LastRequestDate = lastRequestDate;
        parent.parent.DialogKartable.set_value(ObjDialogKartable);
        parent.parent.DialogKartable.Show();
    }
    if (keyItem == 'ConfirmedRequestCount' || keyItem == 'NotConfirmedRequestCount' || keyItem == 'InFlowRequestCount')
    {
        var ObjDialogRegisteredRequests = new Object();
        ObjDialogRegisteredRequests.Caller = 'MainPage';
        ObjDialogRegisteredRequests.Applicant = 'PrivateNews';
        ObjDialogRegisteredRequests.KeyApplicant = keyItem;
        ObjDialogRegisteredRequests.LastRequestDate = lastRequestDate;
        parent.parent.DialogRegisteredRequests.set_value(ObjDialogRegisteredRequests);
        parent.parent.DialogRegisteredRequests.Show();
    }

    if (keyItem == 'PrivateMessageCount') {
        var itemNavbarPrivateMessage = parent.parent.NavBarMain.findItemById('nvbItemPrivateMessage_NavBarMain');
        parent.parent.NavBarMain_onItemSelect_Operations(itemNavbarPrivateMessage);
    }

    
}