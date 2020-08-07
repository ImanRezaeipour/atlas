
$(document).ready
        (
            function () {
                document.body.dir = document.RequestRefrenceForm.dir;
                SetWrapper_Alert_Box(document.RequestRefrenceForm.id);
                GetBoxesHeaders_RequestRefrence();
                GetRequestRefrence_RequestRefrence();
                //initTimePickers_RequestRefrence('Change');
                //SetValueCalendars_RequestRefrence();
            }
        );