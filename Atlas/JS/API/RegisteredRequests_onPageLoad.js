
$(document).ready
        (
            function () {
                document.body.dir = document.RegisteredRequestsForm.dir;
                GetBoxesHeaders_RegisteredRequests();
                SetWrapper_Alert_Box(document.RegisteredRequestsForm.id);
                //DNN note:this dialog 'DialogEndorsementFlowState' is for opent this page directly in DNN Profile System
                try { 
                    SetDirection_Alert_Box(parent.document.getElementById(parent.ClientPerfixId + 'MainForm').style.direction);
                } catch (e) { 
                    SetDirection_Alert_Box("rtl");
                } 
               // SetDirection_Alert_Box(parent.document.MainForm.dir);
                ChangeDirection_Mastertbl_RegisteredRequestsForm();
                ChangeDirection_Container_GridRegisteredRequests_RegisteredRequests();
                ChangeLoadState_GridRegisteredRequests_RegisteredRequests('UnKnown');
                //ViewCurrentLangCalendars_RegisteredRequests();
                ResetCalendars_RegisteredRequests();
                CustomizeRegisteredRequestsFilter_RegisteredRequests();
                NonViewItemInRegisteredRequests_RegisteredRequests();

                //DNN Note
                $('iframe').on('load', function () {
                    if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
                });
            }
        );
