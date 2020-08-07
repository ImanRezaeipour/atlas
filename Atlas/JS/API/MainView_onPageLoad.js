$(document).ready(function () {
    document.body.dir = document.MainViewForm.dir;
    initializeParts_MainView();

    $('iframe').on('load', function () {
        if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
    });
});