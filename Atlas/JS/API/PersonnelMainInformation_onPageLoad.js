$(document).ready(function () {

    document.body.dir = document.PersonnelMainInformationForm.dir;
    init_DialogPersonnelMainInformation();
    SetWrapper_Alert_Box(document.PersonnelMainInformationForm.id);
    ResetCalendars_DialogPersonnelMainInformation();
    GetBoxesHeaders_DialogPersonnelMainInformation();
    GetWorkingPersonnelID_PersonnelMainInformation();
    CacheTreeViewsSize_PersonnelMainInformation();
    $("#divDigitalSignature_PersonnelMainInformation").jSignature();

    $('iframe').on('load', function () {
        if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
    });
});