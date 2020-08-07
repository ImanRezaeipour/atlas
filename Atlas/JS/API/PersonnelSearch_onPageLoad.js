

$(document).ready
        (
            function () {
                document.body.dir = document.PersonnelSearchForm.dir;
                SetWrapper_Alert_Box(document.PersonnelSearchForm.id);
                //ViewCurrentLangCalendars_PersonnelSearch();
                //ChangeComboTreeDirection_PersonnelSearch();
                GetBoxesHeaders_PersonnelSearch();
                ResetCalendars_PersonnelSearch();
                ChangeVisibilityState_PersonnelSearch();
            }

        );
