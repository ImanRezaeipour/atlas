
$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.MasterTrafficsControlForm.dir;
                document.body.dir = 'ltr';
                SetWrapper_Alert_Box(document.MasterTrafficsControlForm.id);
                SetDirection_Alert_Box(parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'MainForm').style.direction);
                //SetDirection_Alert_Box(parent.document.MainForm.dir);
                GetBoxesHeaders_MasterTrafficsControl();
                SetActionMode_MasterTrafficsControl('View');
                //ViewCurrentLangCalendars_MasterTrafficsControl();
                Init_TimeSelectors_MasterTrafficsControl();
                ResetCalendar_MasterTrafficsControl();
                ChangeDirection_MasterTrafficsControl();
                SetPosition_cmbPersonnel_MasterTrafficsControl();
            }
        );
