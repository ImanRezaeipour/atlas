
$(document).ready
        (
            function () {
                document.body.dir = document.UserSettingsForm.dir;
                SetWrapper_Alert_Box(document.UserSettingsForm.id);
                SetDirection_Alert_Box(parent.document.getElementById(parent.ClientPerfixId + 'MainForm').style.direction);
                //SetDirection_Alert_Box(parent.document.MainForm.dir);
                GetBoxesHeaders_UserSettings();
                GetSettings_UserSettings('Load');
                ChangeDirection_Mastertbl_UserSettings();
            }
        );
