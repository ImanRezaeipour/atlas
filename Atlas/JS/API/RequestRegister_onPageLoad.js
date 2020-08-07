
$(document).ready
        (
            function () {
                document.body.dir = document.RequestRegisterForm.dir;
                ChangeHideElementsState_RequestRegister(false, false, false, true);
                GetBoxesHeaders_RequestRegister();
                SetWrapper_Alert_Box(document.RequestRegisterForm.id);
                try {
                    SetDirection_Alert_Box(parent.parent.document.getElementById(parent.parent.ClientPerfixId + 'MainForm').style.direction);
                } catch (e) {
                    SetDirection_Alert_Box('rtl');
                }

                //SetDirection_Alert_Box(parent.parent.document.MainForm.dir);
                //ViewCurrentLangCalendars_RequestRegister();
                initTimePickers_RequestRegister('Load');
                ResetCalendars_RequestRegister();
                GetDialogRequestRegisterObjVal_RequestRegister();
                //CustomizeRequestRegister_RequestRegister();
                ChangeControlDirection_RequestRegister('All');
            }
        );
