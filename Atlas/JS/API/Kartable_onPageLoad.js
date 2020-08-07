$(document).ready(function () {
    document.body.dir = document.KartableForm.dir;
    SetWrapper_Alert_Box(document.KartableForm.id);

    SetDirection_Alert_Box(parent.document.getElementById(parent.parent.ClientPerfixId + 'MainForm').style.direction);
    //SetDirection_Alert_Box(parent.document.MainForm.dir);
    //ViewCurrentLangCalendars_Calendar();
    ChangeDateControlContainersWidth_Kartable();
    SetCurrentDate_Kartable();
    GetBoxesHeaders_Kartable();
    ChangeDirection_cmbControls_Kartable();
    ChangeDirection_Mastertbl_KartableForm();
    ChangeDirection_GridKartable_Kartable();
    GetDefaultLoadState_Kartable();
    SetPageIndex_GridKartable_Kartable(0);

    //DNN Note
    $('iframe').on('load', function () {
        if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
    });
});