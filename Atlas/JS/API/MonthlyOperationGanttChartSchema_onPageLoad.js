$(document).ready(function () {

    document.body.dir = document.MonthlyOperationGanttChartSchemaForm.dir;
    SetWrapper_Alert_Box(document.MonthlyOperationGanttChartSchemaForm.id);
    GetBoxesHeaders_MonthlyOperationGanttChartSchema();
    Fill_cmbMonth_MonthlyOperationGanttChartSchema();
    ChangeTlbMasterMonthlyOperation_MonthlyOperationGanttChartSchema();

    //DNN Note
    $('iframe').on('load', function () {
        if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
    });


});



