

$(document).ready
(
    function () {
        try {
            parent.DialogLoading.Close();
        } catch (e) {
            document.body.style.background = "url('Images/TabStrip/boxstyle_13.png') repeat";
            
        }
        
        document.body.dir = document.ManagerMasterMonthlyOperationForm.dir;
        //ChangeDirection_Mastertbl_ManagerMasterMonthlyOperationForm();
        SetWrapper_Alert_Box(document.ManagerMasterMonthlyOperationForm.id);
        GetBoxesHeaders_ManagerMasterMonthlyOperation();
        ChangeDirection_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation();
        SetPosition_DropDownDives_ManagerMasterMonthlyOperation();
        SetPageIndex_GridMonthlyOperationSummary_ManagerMasterMonthlyOperation(0);
        CacheTreeViewsSize_ManagerMasterMonthlyOperation();
        Fill_trvDepartments_ManagerMasterMonthlyOperation();
    }
);