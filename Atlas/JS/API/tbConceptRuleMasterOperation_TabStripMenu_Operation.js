﻿
var CurrentPageState_Concepts = 'View';

function tlbItemExit_TlbConceptRuleMasterOperation_onClick() {
    ShowDialogConfirm();
}

function ShowDialogConfirm() {
    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_ConceptRuleMasterOperation').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    parent.CloseCurrentTabOnTabStripMenus();
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemConcepts_TlbConcepts_ConceptRuleMasterOperation_onClick() {
    ShowDialogConceptsManagement();
}

function ShowDialogConceptsManagement() {
    parent.DialogConceptsManagement.Show();
}

function tlbItemRules_TlbRules_ConceptRuleMasterOperation_onClick() {
    ShowDialogRulesManagements();
}

function ShowDialogRulesManagements() {
    parent.DialogRulesManagement.Show();
}

function tlbItemCompileConceptAndRule_TlbConceptRuleMasterOperation_onClick(sender, e) {
    TlbConceptRuleMasterOperation.get_items().getItemById('tlbItemCompileConceptAndRule_TlbConceptRuleMasterOperation').set_enabled(false);
    CompileAndMakeDll_ConceptRuleMasterOperation();
}

function CompileAndMakeDll_ConceptRuleMasterOperation_onCallBack(Response) {
    var RetMessage = Response;
    try {
        if (RetMessage != null && RetMessage.length > 0) {
            if (Response[1] == "ConnectionError") {
                Response[0] = document.getElementById('hfErrorType_Posts').value;
                Response[1] = document.getElementById('hfConnectionError_Posts').value;
            }
            showDialog(RetMessage[0], Response[1], RetMessage[2]);
            if (RetMessage[2] == 'success') {
                Fill_GridConcepts_Concepts(CurrentPageIndex_GridConcepts_Concepts);
                ClearList_Concepts();
                RefreshConcept_Concepts();
                ClearControls_Concepts();
                ChangePageState_Concepts('View');
            } else {
                if (CurrentPageState_Concepts == 'Delete')
                    ChangePageState_Concepts('View');
            }
        }

    } catch(e){

    }
    finally
    {
        TlbConceptRuleMasterOperation.get_items().getItemById('tlbItemCompileConceptAndRule_TlbConceptRuleMasterOperation').set_enabled(true);
    }

}

function tlbItemHelp_TlbConceptRuleMasterOperation_onClick() {
    LoadHelpPage('tlbItemHelp_TlbDailyRequestOnAbsence');
}


function tlbItemFormReconstruction_TlbConceptRuleMasterOperation_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvConceptRuleMasterOperation_iFrame').src =parent.ModulePath + 'ConceptRuleMasterOperation.aspx';
}

