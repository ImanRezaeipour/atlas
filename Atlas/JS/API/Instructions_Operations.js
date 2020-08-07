
function GetBoxesHeaders_Instructions() {
    var boxHeader = document.getElementById('hfheader_Instructions').value;
    if (this.frameElement.id != 'MainViewMaximizedPartIFrame_MainView')
        parent.document.getElementById('header_' + this.frameElement.id).innerHTML = document.getElementById('hfheader_Instructions').value;
    else
        parent.document.getElementById('Title_DialogMainViewMaximizedPart').innerHTML = boxHeader;
}


