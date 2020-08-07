$(document).ready
        (
            function () {
                document.body.dir = document.Date_Time_ReportParameterForm.dir;
                SetWrapper_Alert_Box(document.Date_Time_ReportParameterForm.id);
                SetCurrentDate_Date_Time_ReportParameter();
                SetReportParameterObj_Date_Time_ReportParameter();
            }
        );