

$(document).ready
        (
            function() {
                parent.DialogLoading.Close();            
                document.body.dir = document.MasterPersonnelMainInformationForm.dir;
                ChangeDirection_Container_GridPersonnel_Personnel();
                SetWrapper_Alert_Box(document.MasterPersonnelMainInformationForm.id);
                GetBoxesHeaders_Personnel();
                SetActionMode_Personnel('View');
                SetPageIndex_GridPersonnel_Personnel(0);
            }
        );
