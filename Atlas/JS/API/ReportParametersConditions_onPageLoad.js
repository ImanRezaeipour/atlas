
$(document).ready
(
    function () {
        document.body.dir = document.ReportParametersConditionsForm.dir;
        SetWrapper_Alert_Box(document.ReportParametersConditionsForm.id);
        Fill_txtFieldCondition_ReportParametersConditions();
        Fill_txtFieldOrder_ReportParametersConditions();
        GetBoxesHeaders_ReportParametersConditions();
        Fill_GridGroupColumn_ReportParametersConditions();
    }
)