$(document).ready(
 function () {
     document.body.dir = document.ExpressionsManagementForm.dir;
     SetWrapper_Alert_Box(document.ExpressionsManagementForm.id);
     GetBoxesHeader_Expressions();
     SetActionMode_Expressions('View');
     Fill_GridExpressions_Expressions(0);
     CacheTreeViewsSize_Expressions();
     CallBack_trvExpressions_Expressions.callback();
 });