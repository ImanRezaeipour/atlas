$(document).ready
        (
            function () {
                document.body.dir = document.SendPrivateMessageForm.dir;
                SetWrapper_Alert_Box(document.SendPrivateMessageForm.id);
                GetBoxesHeaders_SendPrivateMessage();
                CacheTreeViewsSize_SendPrivateMessage();
                CustomizeForm_SendPrivateMessage();
            }
        );
