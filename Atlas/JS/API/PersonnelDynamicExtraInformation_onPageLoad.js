
$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelDynamicExtraInformationForm.dir;
                SetWrapper_Alert_Box(document.PersonnelDynamicExtraInformationForm.id);
                ChangeCalendarsEnabled_PersonnelDynamicExtraInformation('disable');
                GetBoxesHeaders_PersonnelDynamicExtraInformation();
                SetActionMode_PersonnelDynamicExtraInformation('Form', 'View');
                Fill_GridDynamicParameters_PersonnelDynamicExtraInformation();
            }
        );
