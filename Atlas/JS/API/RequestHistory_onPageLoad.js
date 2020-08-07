
$(document).ready
        (
            function () {
                document.body.dir = document.RequestHistoryForm.dir;
                SetWrapper_Alert_Box(document.RequestHistoryForm.id);
                GetBoxesHeaders_RequestHistory();
                GetRequestHistory_RequestHistory();
                initTimePickers_RequestHistory('Change');
                SetValueCalendars_RequestHistory();
            }
        );