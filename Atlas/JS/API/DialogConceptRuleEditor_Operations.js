
var CurrentConceptOrRuleIdentification = null;
var CurrentPageState_Concept = 'Edit';
var ConfirmState_Concept = null;
var ObjConcept_Concept = null;
var Obj_CncptExprsn_ExpandedNodes = null;
var ExpressionConcept_Selected = null;
var DetailedConcept_Selected = null;
var DetailsJsonObject = null;
var ScriptEn = null;
var ScriptFa = null;
var CallerDialog = null;
var loadedBefore = false;
var CurrentPageTreeViewsObj = new Object();
var lastWasEditable = false;


function trvConceptExpression_ConceptRuleEditor_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvConceptExpression_ConceptRuleEditor').innerHTML = '';
}

function trvConceptExpression_ConceptRuleEditor_onNodeSelect(sender, e) {
    Fill_ExpressionConcept_Selected(e.get_node());
}

function Fill_ExpressionConcept_Selected(selectedExpressionConceptNode) {
    if (selectedExpressionConceptNode != undefined) {
        ExpressionConcept_Selected = new Object();
        ExpressionConcept_Selected.ID = selectedExpressionConceptNode.get_id();
        ExpressionConcept_Selected.Value = selectedExpressionConceptNode.get_value();
        ExpressionConcept_Selected.Text = selectedExpressionConceptNode.get_text();
        ExpressionConcept_Selected.ImageUrl = selectedExpressionConceptNode.get_imageUrl();

        var InnerInDepth = JSON.parse(selectedExpressionConceptNode.Value)[0].Value;
        if (InnerInDepth != undefined) {
            if (TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor') != null) {
                if (InnerInDepth.CanAddToFinal) {
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_enabled(true);
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_imageUrl('arrow-left.png');

                } else {
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_enabled(false);
                    TlbInterAction_ConceptRuleEditor.get_items().getItemById('tlbItemAdd_TlbInterAction_ConceptRuleEditor').set_imageUrl('arrow-left_silver.png');
                }
            }
        }
    }
}

function trvConceptExpression_ConceptRuleEditor_onNodeBeforeExpand(sender, e) {
    Obj_CncptExprsn_ExpandedNodes = new Object();
    Obj_CncptExprsn_ExpandedNodes.Node = e.get_node();
    if (
        e.get_node().get_nodes().get_length() == 1 &&
        (
            e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined ||
            e.get_node().get_nodes().get_nodeArray()[0].get_id() == ''
        )
       ) {
        Obj_CncptExprsn_ExpandedNodes.HasChild = true;
        trvConceptExpression_ConceptRuleEditor.beginUpdate();
        Obj_CncptExprsn_ExpandedNodes.Node.get_nodes().remove(0);
        trvConceptExpression_ConceptRuleEditor.endUpdate();

        Resize_trvConceptExpression_ConceptRuleEditor();
        ChangeDirection_trvConceptExpression_ConceptRuleEditor();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            Obj_CncptExprsn_ExpandedNodes.HasChild = false;
        else
            Obj_CncptExprsn_ExpandedNodes.HasChild = true;
    }
}

function trvConceptExpression_ConceptRuleEditor_onCallbackComplete(sender, e) {
    if (Obj_CncptExprsn_ExpandedNodes != null) {
        if (Obj_CncptExprsn_ExpandedNodes.Node.get_nodes().get_length() == 0 && Obj_CncptExprsn_ExpandedNodes.HasChild) {
            Obj_CncptExprsn_ExpandedNodes = null;
            GetLoadonDemandError_ConceptsPage();
        }
        else{
            Obj_CncptExprsn_ExpandedNodes = null;
            Resize_trvConceptExpression_ConceptRuleEditor();
            ChangeDirection_trvConceptExpression_ConceptRuleEditor();
        }
    }
}

function trvConceptExpression_ConceptRuleEditor_onNodeRename(sender, eventArgs) {
    Fill_ExpressionConcept_Selected(eventArgs.get_node());
}

function GetLoadonDemandError_ConceptsPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvConceptExpression_ConceptRuleEditor_onNodeMouseDoubleClick(sender, e) {
    //Fill_ExpressionConcept_Selected(e.get_node());
    //AddExpressionToDetailed();
}


function CallBack_trvConceptExpression_ConceptRuleEditor_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_trvConceptExpression_ConceptRuleEditor').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvConceptExpression_ConceptRuleEditor();
    }
    else {
        if (!loadedBefore) {
            loadedBefore = true;
            FillByDialogValue_trvDetails_ConceptRuleEditor();
            SetDetailedRootAsSelected();
        }
        Resize_trvDetails_ConceptRuleEditor();
        ChangeDirection_trvDetails_ConceptRuleEditor();
    }
}

function trvDetails_ConceptRuleEditor_onNodeExpand(sender, e) {
    Resize_trvDetails_ConceptRuleEditor();
    ChangeDirection_trvDetails_ConceptRuleEditor();
}

function Resize_trvConceptExpression_ConceptRuleEditor() {
    document.getElementById('trvConceptExpression_ConceptRuleEditor').style.width = CurrentPageTreeViewsObj.trvConceptExpression_ConceptRuleEditor;
}

function ChangeDirection_trvConceptExpression_ConceptRuleEditor() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvConceptExpression_ConceptRuleEditor').style.direction = 'ltr';
}

function Resize_trvDetails_ConceptRuleEditor() {
    document.getElementById('trvDetails_ConceptRuleEditor').style.width = CurrentPageTreeViewsObj.trvDetails_ConceptRuleEditor;
}

function ChangeDirection_trvDetails_ConceptRuleEditor() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvDetails_ConceptRuleEditor').style.direction = 'ltr';
}

function CallBack_trvConceptExpression_ConceptRuleEditor_onCallbackError(sender, e) {
    ShowConnectionError_ConceptRuleEditor();
}

function ShowConnectionError_ConceptRuleEditor() {
    var error = document.getElementById('hfErrorType_Concepts').value;
    var errorBody = document.getElementById('hfConnectionError_Concepts').value;
    showDialog(error, errorBody, 'error');
}

function Fill_trvConceptExpression_ConceptRuleEditor() {
    document.getElementById('loadingPanel_trvConceptExpression_ConceptRuleEditor').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvConceptExpression_ConceptRuleEditor').value);
    CallBack_trvConceptExpression_ConceptRuleEditor.callback();
}

function tlbConceptExpressionReferesh_TlbConcepts_onClick() {
    Obj_CncptExprsn_ExpandedNodes = null;
    Fill_trvConceptExpression_ConceptRuleEditor();
}

function SetCurrentConceptOrRuleIdentification() {
    CurrentConceptOrRuleIdentification = document.getElementById("hfConceptOrRuleIdentification").value;
}

function SetDetailedRootAsSelected() {
    trvDetails_ConceptRuleEditor.selectNodeById(CurrentConceptOrRuleIdentification);
}

function trvDetails_ConceptRuleEditor_onNodeSelect(sender, e) {
    Fill_DetailedConcept_Selected(e.get_node());
}

function Fill_DetailedConcept_Selected(selectedDetailedConcept) {
    if (selectedDetailedConcept != undefined) {
        DetailedConcept_Selected = new Object();
        DetailedConcept_Selected.ID = selectedDetailedConcept.get_id();
        DetailedConcept_Selected.Value = selectedDetailedConcept.get_value();
        DetailedConcept_Selected.Text = selectedDetailedConcept.get_text();
    }
}

function trvDetails_ConceptRuleEditor_onLoad(sender, e) {
    if (trvDetails_ConceptRuleEditor.get_selectedNode() == undefined && trvDetails_ConceptRuleEditor.get_nodes() != undefined && trvDetails_ConceptRuleEditor.get_nodes().getNode(0) != undefined) {
        trvDetails_ConceptRuleEditor.get_nodes().getNode(0).select();
    }
}

function CallBack_trvDetails_ConceptRuleEditor_onCallbackComplete(sender, e) {
}

function CallBack_trvDetails_ConceptRuleEditor_onCallbackError(sender, e) {
    ShowConnectionError_ConceptRuleEditor();
}

function TlbConceptsDetails_Delete_TlbConcepts_onClick() {

    var selected = trvDetails_ConceptRuleEditor.get_selectedNode();
    var rootNode = trvDetails_ConceptRuleEditor.get_nodes().getNode(0);

    RemoveSelectedNodeFromDetailed();

}

function trvDetails_ConceptRuleEditor_onNodeSelect(sender, e) {
    DetailedConcept_Selected = e.get_node();
}

function trvDetails_ConceptRuleEditor_onNodeBeforeRename(sender, eventArgs) {
    if (eventArgs.get_newText().length < 0) {
        eventArgs.set_cancel(true);
    }
}

function TlbItemDetails_Refresh_JsonObj_TlbConceptsDetails_onClick() {
    GenerateAllScript();
}

function GenerateAllScript() {
    DetailsJsonObject = new Object();

    if (trvDetails_ConceptRuleEditor.get_nodes() != null &&
        trvDetails_ConceptRuleEditor.get_nodes().get_length() > 0
    ) {

        if (trvDetails_ConceptRuleEditor.get_nodes().getNode(0) != undefined
            && trvDetails_ConceptRuleEditor.get_nodes().getNode(0).get_nodes().get_length() > 0) {

            DetailsJsonObject = TreeViewNodesToObject(trvDetails_ConceptRuleEditor.get_nodes().getNode(0).get_nodes());

            var stringJson = JSON.stringify(DetailsJsonObject);
            document.getElementById('ConceptExpressionScriptFull').innerHTML = stringJson;
            document.getElementById('ObjectJsonHiddenField_trDetails_Concept').Value = stringJson;

            ScriptEn = GenerateScript(DetailsJsonObject, "En");
            document.getElementById('ConceptExpressionScript').innerHTML = ScriptEn;

            ScriptFa = GenerateScript(DetailsJsonObject, "Fa");
            document.getElementById('ConceptExpressionScriptFa').innerHTML = ScriptFa;
        }
    }
}

function tlbItemSave_TlbConceptRuleEditor_onClick() {
    try {
        GenerateAllScript();
        if (DetailsJsonObject == null || ScriptEn == null || ScriptFa == null) return;

        UpdateConceptOnParentForm();

    } catch (e) {

    }
}

function UpdateConceptOnParentForm() {
    var ObjectToSendToParent = new Object();
    ObjectToSendToParent.DetailsJsonObject = DetailsJsonObject;
    ObjectToSendToParent.ScriptEn = ScriptEn;
    ObjectToSendToParent.ScriptFa = ScriptFa;

    switch (CallerDialog) {
        case "ConceptManagement":
            parent.document.getElementById(parent.ClientPerfixId + 'DialogConceptsManagement_IFrame').contentWindow.Apply_Object_CSharp_Script_FromConceptRuleEditor(ObjectToSendToParent);
            break;
        case "RuleManagement":
            parent.document.getElementById(parent.ClientPerfixId + 'DialogRulesManagement_IFrame').contentWindow.Apply_Object_CSharp_Script_FromRuleRuleEditor(ObjectToSendToParent);
            break;
    }

}

function FillByDialogValue_trvDetails_ConceptRuleEditor() {
    if (parent.DialogConceptRuleEditor.get_value() != undefined) {

        if (parent.DialogConceptRuleEditor.get_value().ID == undefined) {
            CurrentConceptOrRuleIdentification = document.getElementById('hfNotSaved_ConceptRyleEditor').value;

            DetailsJsonObject = parent.DialogConceptRuleEditor.get_value().DetailsJsonObject;
            ScriptEn = parent.DialogConceptRuleEditor.get_value().ScriptEn;
            ScriptFa = parent.DialogConceptRuleEditor.get_value().ScriptFa;
            CallerDialog = parent.DialogConceptRuleEditor.get_value().CallerDialog;
            return;
        } else {
            CurrentConceptOrRuleIdentification = parent.DialogConceptRuleEditor.get_value().ID;

            DetailsJsonObject = parent.DialogConceptRuleEditor.get_value().DetailsJsonObject;
            ScriptEn = parent.DialogConceptRuleEditor.get_value().ScriptEn;
            ScriptFa = parent.DialogConceptRuleEditor.get_value().ScriptFa;
            CallerDialog = parent.DialogConceptRuleEditor.get_value().CallerDialog;
            Fill_trvDetails_ConceptRuleEditor_By_DetailsJsonObject();
        }
    }
}

function Fill_trvDetails_ConceptRuleEditor_By_DetailsJsonObject() {
    CleartrvDetails_ConceptRuleEditorAllNodes();

    var ConceptRule;

    if (DetailsJsonObject != undefined && DetailsJsonObject != "") {
        ConceptRule = DetailsJsonObject;
    }

    trvDetails_ConceptRuleEditor.beginUpdate();

    var treeViewNodeToAdd = new ComponentArt.Web.UI.TreeViewNode();
    treeViewNodeToAdd.set_expanded(true);
    treeViewNodeToAdd.set_id(CurrentConceptOrRuleIdentification);
    treeViewNodeToAdd.set_value(JSON.stringify(""));
    treeViewNodeToAdd.set_text(CurrentConceptOrRuleIdentification);
    treeViewNodeToAdd.set_imageUrl("Images/TreeView/folder.gif");
    treeViewNodeToAdd.set_editingEnabled(false);
    trvDetails_ConceptRuleEditor.get_nodes().add(treeViewNodeToAdd);

    trvDetails_ConceptRuleEditor.endUpdate();

    trvDetails_ConceptRuleEditor.selectNodeById(treeViewNodeToAdd.get_id());

    if (ConceptRule != undefined && ConceptRule != "") {
        trvDetails_ConceptRuleEditor.beginUpdate();
        GenerateNode(ConceptRule, treeViewNodeToAdd.get_id());
        trvDetails_ConceptRuleEditor.endUpdate();
    }

    Resize_trvDetails_ConceptRuleEditor();
    ChangeDirection_trvDetails_ConceptRuleEditor();
}

function CleartrvDetails_ConceptRuleEditorAllNodes() {
    trvDetails_ConceptRuleEditor.get_nodes().clear();
}

function tlbItemAdd_TlbInterAction_ConceptRuleEditor_onClick() {
    AddExpressionToDetailed();
}

function tlbItemDelete_TlbInterAction_ConceptRuleEditor_onClick() {
    RemoveSelectedNodeFromDetailed();
}

function Refresh_trvConceptExpression_ConceptRuleEditor() {
    Fill_trvConceptExpression_ConceptRuleEditor();
}

function tlbItemHelp_TlbConceptRuleEditor_onClick(sender, e) {
    LoadHelpPage('tlbItemHelp_TlbConceptRuleEditors');
}

function tlbConceptExit_TlbConceptRuleEditor_onClick(sender, e) {
    ShowDialogConfirm('Exit');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Concept = confirmState;
    switch (confirmState) {
        case 'NodeDelete':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfNodeDeleteMessage_Concepts').value;
            break;
        case 'NodeWithithChildDelete':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfNodeWithChildDeleteMessage_Concepts').value;
            break;
        case 'Exit':
            document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Concepts').value;
            break;
    }
    DialogConfirm.Show();
}

function CharToKeyCode(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

function GetBoxesHeaders_Concepts() {
    parent.document.getElementById('Title_DialogConceptRuleEditor').innerHTML = document.getElementById('hfTitle_DialogConceptRuleEditor').value;
    document.getElementById('header_Expressions_ConceptRuleEditor').innerHTML = document.getElementById('hfheader_Expressions_ConceptRuleEditor').value;
    document.getElementById('header_Content_ConceptRuleEditor').innerHTML = document.getElementById('hfheader_Content_ConceptRuleEditor').value;
}

function AddExpressionToDetailed() {
    if (trvDetails_ConceptRuleEditor.get_selectedNode() == null) DetailedConcept_Selected == null;
    if (ExpressionConcept_Selected == null || DetailedConcept_Selected == null) return;

    var newTreeViewNode = new ComponentArt.Web.UI.TreeViewNode();

    var newIntId = 1;
    var newTreeViewNode_Id = DetailedConcept_Selected.ID.toString() + "_" + ExpressionConcept_Selected.ID + "_" + newIntId;

    var nodeCollection = DetailedConcept_Selected.get_nodes();

    for (var i = 0; i < nodeCollection.get_length() ; i++) {

        var curNode = nodeCollection.getNode(i);
        if (curNode.get_id() != newTreeViewNode_Id) continue;

        newIntId++;

        var dd = newTreeViewNode_Id.substr(0, newTreeViewNode_Id.length - 1);
        newTreeViewNode_Id = dd + newIntId;
    }

    newTreeViewNode.set_id(newTreeViewNode_Id);
    newTreeViewNode.set_value(ExpressionConcept_Selected.Value);
    newTreeViewNode.set_text(ExpressionConcept_Selected.Text);
    newTreeViewNode.set_imageUrl(ExpressionConcept_Selected.ImageUrl);

    var outterValueConvertedToObject = JSON.parse(ExpressionConcept_Selected.Value)[0].Value;
    newTreeViewNode.set_editingEnabled(outterValueConvertedToObject.CanEditInFinal);

    trvDetails_ConceptRuleEditor.beginUpdate();
    trvDetails_ConceptRuleEditor.get_selectedNode().get_nodes().add(newTreeViewNode);
    trvDetails_ConceptRuleEditor.endUpdate();

    trvDetails_ConceptRuleEditor.get_selectedNode().expand();

    Resize_trvDetails_ConceptRuleEditor();
    ChangeDirection_trvDetails_ConceptRuleEditor();

    CallGetChildrenOnCreation(CharToKeyCode(ExpressionConcept_Selected.ID), CharToKeyCode(newTreeViewNode_Id));
}

function RemoveSelectedNodeFromDetailed() {
    if (DetailedConcept_Selected == undefined || trvDetails_ConceptRuleEditor.get_selectedNode() == undefined) return;

    if (trvDetails_ConceptRuleEditor.get_selectedNode().get_parentNode() == null ||
        trvDetails_ConceptRuleEditor.get_selectedNode().get_parentNode() == undefined) {
        showDialog('error', document.getElementById('hfErrorType_Concepts').value, document.getElementById('hfRootNodeDeleteMessage_ConceptRuleEditor').value);
        return;
    }

    if (trvDetails_ConceptRuleEditor.get_selectedNode().get_nodes().get_length() > 0)
        ShowDialogConfirm('NodeWithithChildDelete');
    else
        ShowDialogConfirm('NodeDelete');
}

function TreeViewNodesToObject(treeViewNodeCollection) {
    var jsonObjInner = [];
    for (var index = 0; index < treeViewNodeCollection.get_length() ; index++) {

        var curNode = treeViewNodeCollection.getNode(index);

        if (curNode.get_nodes() == undefined ||
            curNode.get_nodes().get_length() < 1) {
            var hh = {
                id: treeViewNodeCollection.getNode(index).get_id(),
                title: treeViewNodeCollection.getNode(index).get_text(),
                value: treeViewNodeCollection.getNode(index).get_value(),
                nodes: undefined
            };
            jsonObjInner.push(hh);
        } else {
            var oo = {
                id: treeViewNodeCollection.getNode(index).get_id(),
                title: treeViewNodeCollection.getNode(index).get_text(),
                value: treeViewNodeCollection.getNode(index).get_value(),
                nodes: TreeViewNodesToObject(treeViewNodeCollection.getNode(index).get_nodes())
            };
            jsonObjInner.push(oo);
        }
    }
    return jsonObjInner;
}

function GenerateScript(detailsJsonbObject, scriptType) {
    var result = "";
    var i;
    var expressionObject;
    var detail;

    switch (scriptType) {
        case "Fa":
            {
                for (i = 0; i < detailsJsonbObject.length; i++) {
                    detail = detailsJsonbObject[i];

                    expressionObject = SingleNodeToPrepare(detail);

                    if (expressionObject != undefined) {

                        if (expressionObject.CanEditInFinal != undefined && expressionObject.CanEditInFinal == true) {
                            result += " " + expressionObject.ScriptBeginFa;
                        } else {
                            if (expressionObject.ScriptBeginFa != undefined) {
                                result += " " + expressionObject.ScriptBeginFa;
                            }
                            if (detail.nodes != undefined && detail.nodes.length > 0) {
                                result += GenerateScript(detail.nodes, "Fa");
                            }
                            if (expressionObject.ScriptEndFa != undefined) {
                                result += " " + expressionObject.ScriptEndFa;
                            }
                        }
                    }
                }
                break;
            }
        case "En":
        default:
            {
                for (i = 0; i < detailsJsonbObject.length; i++) {
                    detail = detailsJsonbObject[i];

                    expressionObject = SingleNodeToPrepare(detail);

                    if (expressionObject != undefined) {

                        if (expressionObject.CanEditInFinal != undefined && expressionObject.CanEditInFinal == true) {
                            result += "" + expressionObject.ScriptBeginEn;
                            lastWasEditable = true;
                        } else {
                            if (expressionObject.ScriptBeginEn != undefined) {
                                if (lastWasEditable) {
                                    result += "" + expressionObject.ScriptBeginEn;
                                } else {
                                    result += " " + expressionObject.ScriptBeginEn;
                                }
                            }
                            if (detail.nodes != undefined && detail.nodes.length > 0) {
                                result += GenerateScript(detail.nodes, "En");
                            }
                            if (expressionObject.ScriptEndEn != undefined) {
                                if (lastWasEditable) {
                                    result += "" + expressionObject.ScriptEndEn;
                                } else {
                                    result += " " + expressionObject.ScriptEndEn;
                                }
                            }
                            lastWasEditable = false;
                        }

                    }
                }
                break;
            }
    }
    return result;
}

function SingleNodeToPrepare(node) {
    var ValueObject;

    try {
        ValueObject = JSON.parse(JSON.parse(node.value))[0].Value;
        ValueObject.ImageUrl = JSON.parse(JSON.parse(node.value))[0].ImageUrl;
    } catch (e) {
        try {
            ValueObject = JSON.parse(node.value)[0].Value;
            ValueObject.ImageUrl = JSON.parse(node.value)[0].ImageUrl;
        } catch (e) {
            try {
                ValueObject = JSON.parse(JSON.parse(node.value))[0].Value;
                ValueObject.ImageUrl = JSON.parse(JSON.parse(node.value))[0].ImageUrl;
            } catch (e) {

            }
        }
    }
    if (ValueObject != undefined && ValueObject.CanEditInFinal) {
        ValueObject.ScriptBeginEn = node.title;
        ValueObject.ScriptBeginFa = node.title;
    }
    return ValueObject;
}

function CallGetChildrenOnCreation(parentDbId, parentId) {
    GetChildrenOnCreation_ConceptEditorPage(parentDbId, parentId);
}

function GetChildrenOnCreation_ConceptEditorPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Concepts').value;
            Response[1] = document.getElementById('hfConnectionError_Concepts').value;
        }
        if (RetMessage[2] == 'success') {
            var parentID = RetMessage[3];
            var strJson = RetMessage[4];
            AddToDetailedFromCallback(strJson, parentID);
        }
        else {
            if (RetMessage[2] == 'error')
                showDialog(RetMessage[0], Response[1], RetMessage[2]);
        }
    }
}

function AddToDetailedFromCallback(stringJson, parentId) {
    var treeViewNodeToAdd = trvDetails_ConceptRuleEditor.findNodeById(parentId);
    var jsonObjectCollection = JSON.parse(stringJson);

    for (var i = 0; i < jsonObjectCollection.length; i++) {

        var jsonObjectItem = jsonObjectCollection[i];
        var jsonObjectValueObject = jsonObjectItem.Value;

        var newTreeViewNode = new ComponentArt.Web.UI.TreeViewNode();

        newTreeViewNode.set_id(parentId.toString() + "_" + jsonObjectItem.Id.toString());
        newTreeViewNode.set_value(JSON.stringify(jsonObjectItem.Value));
        newTreeViewNode.set_text(jsonObjectValueObject.ScriptBeginFa);
        newTreeViewNode.set_imageUrl(jsonObjectItem.ImageUrl);
        newTreeViewNode.set_editingEnabled(jsonObjectValueObject.CanEditInFinal);

        trvDetails_ConceptRuleEditor.beginUpdate();
        treeViewNodeToAdd.get_nodes().add(newTreeViewNode);
        trvDetails_ConceptRuleEditor.endUpdate();
    }
    treeViewNodeToAdd.get_selectedNode().expand();

    Resize_trvDetails_ConceptRuleEditor();
    ChangeDirection_trvDetails_ConceptRuleEditor();
}

function GenerateNode(jsonObjectCollection, parentNodeId) {
    var treeViewNodeToAdd = trvDetails_ConceptRuleEditor.findNodeById(parentNodeId);

    if (jsonObjectCollection != undefined) {

        for (var i = 0; i < jsonObjectCollection.length; i++) {

            var ConceptRule = jsonObjectCollection[i];
            if (ConceptRule != undefined) {

                var newTreeViewNode = new ComponentArt.Web.UI.TreeViewNode();

                newTreeViewNode.set_id(parentNodeId + "_" + ConceptRule.id.toString());
                newTreeViewNode.set_value(ConceptRule.value);
                newTreeViewNode.set_text(ConceptRule.title);
                newTreeViewNode.set_imageUrl(SingleNodeToPrepare(ConceptRule).ImageUrl);
                newTreeViewNode.set_editingEnabled(SingleNodeToPrepare(ConceptRule).CanEditInFinal);

                treeViewNodeToAdd.get_nodes().add(newTreeViewNode);

                if (ConceptRule.nodes != undefined && ConceptRule.nodes.length > 0) {
                    GenerateNode(ConceptRule.nodes, newTreeViewNode.get_id());
                }
            }
        }
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Concept) {
        case 'NodeDelete':
        case 'NodeWithithChildDelete':
            var parentNode = trvDetails_ConceptRuleEditor.get_selectedNode().get_parentNode();
            trvDetails_ConceptRuleEditor.beginUpdate();
            trvDetails_ConceptRuleEditor.get_selectedNode().remove();
            trvDetails_ConceptRuleEditor.endUpdate();
            parentNode.select();
            Resize_trvDetails_ConceptRuleEditor();
            ChangeDirection_trvDetails_ConceptRuleEditor();
            break;
        case 'Exit':
            CloseDialogConceptRuleEditor();
            break;
    }
    DialogConfirm.Close();
}

function CacheTreeViewsSize_Concepts() {
    CurrentPageTreeViewsObj.trvConceptExpression_ConceptRuleEditor = document.getElementById('trvConceptExpression_ConceptRuleEditor').clientWidth + 'px';
    CurrentPageTreeViewsObj.trvDetails_ConceptRuleEditor = document.getElementById('trvDetails_ConceptRuleEditor').clientWidth + 'px';
}

function tlbItemFormReconstruction_TlbConceptRuleEditor_onClick() {
    CloseDialogConceptRuleEditor();
    parent.document.getElementById(parent.ClientPerfixId + 'DialogRulesManagement_IFrame').contentWindow.ShowDialogConceptRuleEditor();
}

function tlbItemExit_TlbConceptRuleEditor_onClick() {
    ShowDialogConfirm('Exit');
}

function CloseDialogConceptRuleEditor() {
    parent.document.getElementById(parent.ClientPerfixId + 'DialogConceptRuleEditor_IFrame').src =parent.ModulePath + 'WhitePage.aspx';
    parent.eval(parent.ClientPerfixId + 'DialogConceptRuleEditor').Close();
}

function tlbItemRefresh_TlbConceptsExpression_onClick() {
    Refresh_trvConceptExpression_ConceptRuleEditor();
}

function trvConceptExpression_ConceptRuleEditor_onNodeExpand(sender, e) {
    Resize_trvConceptExpression_ConceptRuleEditor();
    ChangeDirection_trvConceptExpression_ConceptRuleEditor();
}


function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}