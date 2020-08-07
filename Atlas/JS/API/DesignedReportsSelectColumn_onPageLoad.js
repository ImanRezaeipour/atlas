$(document).ready
(
    function () {
        
       document.body.dir = document.DesignedReportsSelectColumnForm.dir;
       SetWrapper_Alert_Box(document.DesignedReportsSelectColumnForm.id);
       GetBoxesHeaders_DesignedReportsSelectColumn();
       SetActionMode_DesignedReportsSelectColumn('View');
       Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn();
    }
);