
$(document).ready
    (
        function () {
            parent.DialogLoading.Close();
            document.body.dir = document.ShiftPairTypesForm.dir;
            SetWrapper_Alert_Box(document.ShiftPairTypesForm.id);
            GetBoxesHeaders_ShiftPairTypes();
            SetActionMode_ShiftPairTypes('View');
            Fill_GridShiftPairTypes_ShiftPairTypes();

        }
    );
