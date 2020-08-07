
$(document).ready
        (
            function () {
                document.body.dir = document.FlowConditionsForm.dir;
                SetWrapper_Alert_Box(document.FlowConditionsForm.id);
                GetBoxesHeaders_FlowConditions();
                Fill_GridFlowConditions_FlowConditions();
            }
        );
