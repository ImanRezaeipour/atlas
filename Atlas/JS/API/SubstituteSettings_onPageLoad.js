﻿

$(document).ready
        (
            function () {
                document.body.dir = document.SubstituteSettingsForm.dir;
                SetWrapper_Alert_Box(document.SubstituteSettingsForm.id);
                GetBoxesHeaders_SubstituteSettings();
                SetSubstituteInfo_SubstituteSettings();
                SetActionMode_SubstituteSettings('View');
                CacheTreeViewsSize_SendPrivateMessage();
                Fill_GridManagerWorkFlows_SubstituteSettings();
            }
        );
