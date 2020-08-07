
$(document).ready
        (
            function () {
                document.body.dir = document.SystemReportsForm.dir;
                SetWrapper_Alert_Box(document.SystemReportsForm.id);
                GetBoxesHeaders_SystemReports();
                ChangeFilterState_SystemReports(false);
                SetPageIndex_GridSystemReportType_SystemReports(0);
            }
        );
