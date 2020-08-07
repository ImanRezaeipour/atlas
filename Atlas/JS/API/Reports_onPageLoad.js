
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.ReportsForm.dir;
                SetWrapper_Alert_Box(document.ReportsForm.id);
                GetBoxesHeaders_Reports();
                SetActionMode_Reports('View');
                CacheTreeViewsSize_Reports();
                Fill_trvReports_Reports();
            }
        );
