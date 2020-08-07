$(document).ready
        (
            function () {
                document.body.dir = document.DutyPlace_ReportParameterForm.dir;
                SetWrapper_Alert_Box(document.DutyPlace_ReportParameterForm.id);
                //ViewCurrentLangCalendars_RuleParameters();
                SetReportParameterObj_DutyPlace_ReportParameter();

            }
        );