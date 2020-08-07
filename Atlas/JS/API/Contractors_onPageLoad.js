$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.ContractorsForm.dir;
                SetWrapper_Alert_Box(document.ContractorsForm.id);
                GetBoxesHeaders_Contractors();
                SetActionMode_Contractors('View');
                 Fill_GridContractors_Contractors(0);
            }
        );