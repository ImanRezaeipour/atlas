
$(document).ready
        (
            function () {
                document.body.dir = document.UpdateCalculationResultForm.dir;
                SetWrapper_Alert_Box(document.UpdateCalculationResultForm.id);
                GetBoxesHeaders_UpdateCalculationResult();
                ChangeDirection_Container_GridUpdateCalculationResult_UpdateCalculationResult();
                SetPageIndex_GridUpdateCalculationResult_UpdateCalculationResult(0);
            }
        );
