var ObjDashboards_MainView = null;
function initializeParts_MainView() {
    GetDashboards_MainViewPage("");
    DialogWaiting.Show();    
}
function GetDashboards_MainViewPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_UserSettings').value;
            Response[1] = document.getElementById('hfConnectionError_UserSettings').value;
        }
        if (RetMessage[2] == 'success') {
            var SettingsBatch = RetMessage[3];
            SetDashboards_MainView(SettingsBatch);
        }
        else
            if (RetMessage[2] == 'error')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
    }
}

function SetDashboards_MainView(DashboardsBatch) {  
    ObjDashboards_MainView = eval('(' + DashboardsBatch + ')');
    if (ObjDashboards_MainView.DashboardName1 != null){
        document.getElementById('MainViewPart1_MainView').src = ObjDashboards_MainView.DashboardName1 + '?' + (new Date()).getDate();
        document.getElementById('Container_MainViewPart1_MainView').style.visibility = 'visible';
    }
    else {
        document.getElementById('MainViewPart1_MainView').src = 'about:blank';
        document.getElementById('Container_MainViewPart1_MainView').style.visibility = 'hidden';
    }
    if (ObjDashboards_MainView.DashboardName2 != null){
        document.getElementById('MainViewPart2_MainView').src = ObjDashboards_MainView.DashboardName2 + '?' + (new Date()).getDate();
        document.getElementById('Container_MainViewPart2_MainView').style.visibility = 'visible';
    }
    else {
        document.getElementById('MainViewPart2_MainView').src = 'about:blank';
        document.getElementById('Container_MainViewPart2_MainView').style.visibility = 'hidden';
    }
    if (ObjDashboards_MainView.DashboardName3 != null) {
        document.getElementById('MainViewPart3_MainView').src = ObjDashboards_MainView.DashboardName3 + '?' + (new Date()).getDate();
        document.getElementById('Container_MainViewPart3_MainView').style.visibility = 'visible';
    }
    else {
        document.getElementById('MainViewPart3_MainView').src = 'about:blank';
        document.getElementById('Container_MainViewPart3_MainView').style.visibility = 'hidden';
    }
    if (ObjDashboards_MainView.DashboardName4 != null){
        document.getElementById('MainViewPart4_MainView').src = ObjDashboards_MainView.DashboardName4 + '?' + (new Date()).getDate();
        document.getElementById('Container_MainViewPart4_MainView').style.visibility = 'visible';
    }
    else {
        document.getElementById('MainViewPart4_MainView').src = 'about:blank';
        document.getElementById('Container_MainViewPart4_MainView').style.visibility = 'hidden';
    }
}

function Refresh_MainViewPart1_MainView() {
    if (ObjDashboards_MainView.DashboardName1 != null)
    document.getElementById('MainViewPart1_MainView').src = ObjDashboards_MainView.DashboardName1 +'?' + (new Date()).getDate();
}

function Refresh_MainViewPart2_MainView() {
    if (ObjDashboards_MainView.DashboardName2 != null)
    document.getElementById('MainViewPart2_MainView').src = ObjDashboards_MainView.DashboardName2 +  '?' + (new Date()).getDate();
}

function Refresh_MainViewPart3_MainView() {
    if (ObjDashboards_MainView.DashboardName3 != null)
    document.getElementById('MainViewPart3_MainView').src = ObjDashboards_MainView.DashboardName3 + '?' + (new Date()).getDate();
}

function Refresh_MainViewPart4_MainView() {
    if (ObjDashboards_MainView.DashboardName4 != null)
    document.getElementById('MainViewPart4_MainView').src = ObjDashboards_MainView.DashboardName4 + '?' + (new Date()).getDate();
}

function Maximize_MainViewPart1_MainView() {
    ShowDialogMainViewMaximizedPart(ObjDashboards_MainView.DashboardName1);
}

function Maximize_MainViewPart2_MainView() {
    ShowDialogMainViewMaximizedPart(ObjDashboards_MainView.DashboardName2);
}

function Maximize_MainViewPart3_MainView() {
    ShowDialogMainViewMaximizedPart(ObjDashboards_MainView.DashboardName3);
}

function Maximize_MainViewPart4_MainView() {
    ShowDialogMainViewMaximizedPart(ObjDashboards_MainView.DashboardName4);
}


function ShowDialogMainViewMaximizedPart(Caller) {
    document.getElementById('MainViewMaximizedPartIFrame_MainView').src = Caller;
    DialogMainViewMaximizedPart.Show();
}



