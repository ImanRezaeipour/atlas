var CurrentPageIndex_bulletedListPublicNews_PublicNews = 0;
function GetBoxesHeaders_PublicNews() {
    var boxHeader = document.getElementById('hfheader_PublicNews').value;
    if (this.frameElement.id != 'MainViewMaximizedPartIFrame_MainView')
        parent.document.getElementById('header_' + this.frameElement.id).innerHTML = boxHeader;
    else
        parent.document.getElementById('Title_DialogMainViewMaximizedPart').innerHTML = boxHeader;
    document.getElementById('PageCount_bulletedListPublicNews_PublicNews').innerHTML = document.getElementById('hfPageCount_bulletedListPublicNews_PublicNews').value;
}

function GetErrorMessage_PublicNews() {
    var errorMessage = document.getElementById('ErrorHiddenField_PublicNews').value;
    if (errorMessage != '' && errorMessage != undefined) {
        errorMessage = eval('(' + errorMessage + ')');
        if (errorMessage[2] != 'success')
            showDialog(errorMessage[0], errorMessage[1], errorMessage[2]);
    }
}

function tlbItemRefresh_TlbPaging_PublicNews_onClick() {
    ChangePage_lookUpBox_PublicNews('Refresh');
}

function tlbItemFirst_TlbPaging_PublicNews_onClick() {
    ChangePage_lookUpBox_PublicNews('First');
}

function tlbItemBefore_TlbPaging_PublicNews_onClick() {
    ChangePage_lookUpBox_PublicNews('Before');
}

function tlbItemNext_TlbPaging_PublicNews_onClick() {
    ChangePage_lookUpBox_PublicNews('Next');
}

function tlbItemLast_TlbPaging_PublicNews_onClick() {
    ChangePage_lookUpBox_PublicNews('Last');
}
function ChangePage_lookUpBox_PublicNews(pageState) {
    var currentPageIndex = 0;
    var pageCount = 0;
    currentPageIndex = CurrentPageIndex_bulletedListPublicNews_PublicNews;
    pageCount = document.getElementById('hfPublicNewsPageCount_PublicNews').value;
   
    switch (pageState) {
        case 'First':
            currentPageIndex = 0;
            SetPageIndex_lookUpBox_PublicNews(0);
            break;
        case 'Before':
            if (currentPageIndex != 0) {
                currentPageIndex = currentPageIndex - 1;
                SetPageIndex_lookUpBox_PublicNews(currentPageIndex);
            }
            break;
        case 'Next':
            if (currentPageIndex < parseInt(pageCount) - 1) {
                currentPageIndex = currentPageIndex + 1;
                SetPageIndex_lookUpBox_PublicNews(currentPageIndex);
            }
            break;
        case 'Last':
            currentPageIndex = parseInt(pageCount) - 1;
            SetPageIndex_lookUpBox_PublicNews(parseInt(pageCount) - 1);
            break;
        case 'Refresh':
            currentPageIndex = 0;
            SetPageIndex_lookUpBox_PublicNews(0);
            break;
    }
    CurrentPageIndex_bulletedListPublicNews_PublicNews = currentPageIndex;
   
}
function SetPageIndex_lookUpBox_PublicNews(pageIndex) {
    CurrentPageIndex_cmbManagers_PublicNews = pageIndex;
    Fill_bulletedListPublicNews_PublicNews(pageIndex);

}
function CallBack_bulletedListPublicNews_PublicNews_onBeforeCallback(sender, e) {
   
}
function CallBack_bulletedListPublicNews_PublicNews_onCallbackComplete(sender, e) {

    var error = document.getElementById('ErrorHiddenField_PublicNews').value;
    if (error == "") {
        ChangePageCount_bulletedListPublicNews_PublicNews();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
       
    }
}
function CallBack_bulletedListPublicNews_PublicNews_onCallbackError(sender, e) {
    ShowConnectionError_PublicNews();
}
function ShowConnectionError_PublicNews() {
    var error = document.getElementById('hfErrorType_PublicNews').value;
    var errorBody = document.getElementById('hfConnectionError_PublicNews').value;
    showDialog(error, errorBody, 'error');
}
function SetPageIndex_bulletedListPublicNews_PublicNews(pageIndex) {
    CurrentPageIndex_bulletedListPublicNews_PublicNews = pageIndex;
    Fill_bulletedListPublicNews_PublicNews(pageIndex);
}
function Fill_bulletedListPublicNews_PublicNews(pageIndex) {
    var pageSize = parseInt(document.getElementById('hfPublicNewsPageSize_PublicNews').value);
    CallBack_bulletedListPublicNews_PublicNews.callback(CharToKeyCode_Substitute(pageSize.toString()), CharToKeyCode_Substitute(pageIndex.toString()));
}

function CharToKeyCode_Substitute(str) {
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
function ChangePageCount_bulletedListPublicNews_PublicNews() {
    var retfooterVal = '';
    var footerVal = document.getElementById('PageCount_bulletedListPublicNews_PublicNews').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfPublicNewsPageCount_PublicNews').value) > 0 ? CurrentPageIndex_bulletedListPublicNews_PublicNews + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfPublicNewsPageCount_PublicNews').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('PageCount_bulletedListPublicNews_PublicNews').innerHTML = retfooterVal;
    
}
