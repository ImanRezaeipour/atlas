$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.CostCenterForm.dir;
                SetWrapper_Alert_Box(document.CostCenterForm.id);
                GetBoxesHeaders_CostCenter();
                SetActionMode_CostCenter('View');
                Fill_GridCostCenter_CostCenter();
            }
        );
