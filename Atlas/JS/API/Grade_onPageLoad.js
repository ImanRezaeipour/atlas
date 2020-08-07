$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.GradeForm.dir;
                SetWrapper_Alert_Box(document.GradeForm.id);
                GetBoxesHeaders_Grade();
                SetActionMode_Grade('View');
                Fill_GridGrade_Grade();
            }
        );
