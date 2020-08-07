
$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelMultiDateFeaturesForm.dir;
                SetWrapper_Alert_Box(document.PersonnelMultiDateFeaturesForm.id);
                GetBoxesHeaders_PersonnelMultiDateFeatures();
                //ViewCurrentLangCalendars_PersonnelMultiDateFeatures();
                ResetCalendars_PersonnelMultiDateFeatures();
                SetActionMode_PersonnelMultiDateFeatures('View');
                ChangeCalendarsEnabled_PersonnelMultiDateFeatures('disable');
                Fill_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures();
            }
        );
