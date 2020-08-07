
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.ShiftsForm.dir;
                ChangeDirection_Container_GridShift_Shift();
                initTimePickers_Shift();
                ChangeColorPickerEnabled_Shift('disable');
                SetWrapper_Alert_Box(document.ShiftsForm.id);
                GetBoxesHeaders_Shift();
                SetActionMode_Shift('Form', 'View');
                Fill_GridShift_Shift();
            }
        );

