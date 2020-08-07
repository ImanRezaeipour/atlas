$(document).ready
        (
            function () {
                document.body.dir = document.FromTime_ToTime_ReportParameterForm.dir;
                SetWrapper_Alert_Box(document.FromTime_ToTime_ReportParameterForm.id);
                SetReportParameterObj_FromTime_ToTime_ReportParameter();
                initTimePickers_FromTime_ToTime_ReportParameter();
            }
        );