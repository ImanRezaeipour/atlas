$(document).ready(function () {
    document.body.dir = document.MonthlyExceptionShiftsForm.dir;
    SetWrapper_Alert_Box(document.MonthlyExceptionShiftsForm.id);
    GetBoxesHeaders_MonthlyExceptionShifts();
    ChangeDirection_Container_GridMonthlyExceptionShifts_MonthlyExceptionShifts();
    SetPageIndex_GridMonthlyExceptionShifts_MonthlyExceptionShifts(0, '');
    CreatKeyboardMapping();
    ComponentArt_Grid.prototype.EditKeyPress = GridMonthlyExceptionShifts_MonthlyExceptionShifts_onEditKeyPress;

    //DNN Note
    $('iframe').on('load', function () {
        if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
    });

});

