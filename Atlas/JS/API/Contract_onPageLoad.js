
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.ContractForm.dir;
                SetWrapper_Alert_Box(document.ContractForm.id);
                GetBoxesHeaders_Contract();
                SetActionMode_Contract('View');
                SetPageIndex_GridContracts_Contract(0);
            }
        );
