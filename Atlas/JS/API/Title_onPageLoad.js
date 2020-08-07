
$(document).ready
        (
            function () {
                document.body.dir = document.TitleForm.dir;
                SetWrapper_Alert_Box(document.TitleForm.id);
                SetReportParameterObj_Title();
            }
        );
