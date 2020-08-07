

$(document).ready
        (
            function () {
                try {
                    parent.DialogLoading.Close();
                } catch (e) {
                    document.body.style.background = "url('Images/TabStrip/boxstyle_13.png') repeat";
                }
                document.body.dir = document.SubstituteForm.dir;
                SetWrapper_Alert_Box(document.SubstituteForm.id);
                //ViewCurrentLangCalendars_tbSubstitute_TabStripMenus();
                ChangeCalendarsEnabled_Substitute('disable');
                SetPosition_DropDownDives_Substitute();
                GetBoxesHeaders_Substitute();
                ResetCalendars_Substitute();
                SetActionMode_Substitute('View');
            }
        );
