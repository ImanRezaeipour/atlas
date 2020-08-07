
$(document).ready(
 function () {
     document.body.dir = document.ConceptsManagementForm.dir;
     SetWrapper_Alert_Box(document.ConceptsManagementForm.id);
     GetBoxesHeaders_Concepts();
     ChangeEnabled_DropDownDive_Concepts('imgbox_SearchByConcept_Concepts', 'disabled');
     SetActionMode_Concepts('View');
     SetPageIndex_GridConcepts_Concepts(0);
     SetEnumTypes();
     SetPosition_DropDownDives_Concepts();
     SetPosition_cmbConcept_Concepts();
 });