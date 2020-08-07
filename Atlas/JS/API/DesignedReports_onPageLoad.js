$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.DesignedReportsForm.dir;
                SetWrapper_Alert_Box(document.DesignedReportsForm.id);
                GetBoxesHeaders_DesignedReports();
                SetActionMode_DesignedReports('View');
                Fill_GridDesignedReports_DesignedReports();
            }
        );