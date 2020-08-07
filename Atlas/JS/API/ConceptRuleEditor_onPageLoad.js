$(document).ready(
 function () {
     document.body.dir = document.conceptEditorForm.dir;
     SetWrapper_Alert_Box(document.conceptEditorForm.id);
     GetBoxesHeaders_Concepts();
     SetCurrentConceptOrRuleIdentification();
     CacheTreeViewsSize_Concepts();
     Fill_trvConceptExpression_ConceptRuleEditor();
 });