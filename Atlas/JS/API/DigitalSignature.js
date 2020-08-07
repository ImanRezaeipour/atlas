
var dataPair = null;

$(document).ready(function () {
    $("#signature").jSignature()
})

function GetData() {
    dataPair = $("#signature").jSignature('getData', 'base30');
}

function Clear() {
    $("#signature").jSignature('clear');
}

function SetData() {
    $("#signature").jSignature('setData', "data:" + dataPair.join(","));
}

function SaveImage() {
    dataPair = $("#signature").jSignature('getData', 'base30');
    SaveImagePage(dataPair[1].toString());
}

function SaveImagePage_OnCallback(response) {
    alert(response);
}
