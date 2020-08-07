
$(document).ready
        (
            function () {
                document.body.dir = document.CalendarForm.dir;
                SetWrapper_Alert_Box(document.CalendarForm.id);
                //SetDirection_Alert_Box(parent.document.MainForm.dir);
                SetDirection_Alert_Box(parent.document.getElementById(parent.parent.ClientPerfixId + 'MainForm').style.direction);
                ChangeDirection_Mastertbl_Calendar();
                GetBoxesHeaders_Calendar();
                GetAxises_Calendar();
                Calendar_FillCal();
                CreateBasePanel_Calendar();
                GetCalData_Calendar();
            }
        );



