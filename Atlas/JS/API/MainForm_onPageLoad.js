$(document).ready(function () {

    //document.body.dir = document.MainForm.dir;

    //document.title = document.MainForm.title;
    //document.MainForm.title = "";
    SetCurrentCulture();
    SetNavBarHeight();
    InitializeQuickLaunch_MainForm();
    $('imgHeaderLogo').imgscale();

    //DNN Note
    $('iframe').on('load', function () {
        if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
    });

});
//window.onbeforeunload = function (event) {
//    $.ajax({
//        url: "userExpiration.aspx",
//        context: document.body
//    }).done(function () {

//    });
//};


















