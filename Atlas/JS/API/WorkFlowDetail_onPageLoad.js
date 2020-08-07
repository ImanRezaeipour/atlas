
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.WorkFlowDetailForm.dir;
                SetWrapper_Alert_Box(document.WorkFlowDetailForm.id);
                GetBoxesHeaders_WorkFlowDetail();
                //ChangeEnabled_DropDownDive_WorkFlowDetail('disabled');
                CacheTreeViewsSize_WorkFlowDetail();
            }
        );