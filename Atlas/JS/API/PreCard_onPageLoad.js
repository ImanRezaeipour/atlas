﻿

$(document).ready
        (
            function() {
               parent.DialogLoading.Close();
               document.body.dir = document.PreCardForm.dir;
               SetWrapper_Alert_Box(document.PreCardForm.id);
               GetBoxesHeaders_PreCard();
               SetActionMode_PreCard('View');
               CacheTreeViewsSize_PreCard();
               Fill_GridPreCards_PreCard();
            }
        );
