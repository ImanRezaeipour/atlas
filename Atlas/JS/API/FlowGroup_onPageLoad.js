$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.FlowGroupForm.dir;
                SetWrapper_Alert_Box(document.FlowGroupForm.id);
                GetBoxesHeaders_FlowGroup();
                SetActionMode_FlowGroup('View');
                Fill_GridFlowGroup_FlowGroup();
            }
        );
