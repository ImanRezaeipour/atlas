
var CurrentPageState_Posts = 'View';
var ConfirmState_Posts = null;
var ObjPost_Posts = null;
var ObjexpandingOrgPostNode_Posts = null;
var CurrentPageTreeViewsObj = new Object();
var PostsSelectedType_Posts = null;

function GetBoxesHeaders_Posts() {
    document.getElementById('header_Posts_Posts').innerHTML = document.getElementById('hfheader_Posts_Posts').value;
    document.getElementById('header_tblPostsDetails_Posts').innerHTML = document.getElementById('hfheader_tblPostsDetails_Posts').value;
}

function CacheTreeViewsSize_Posts() {
    CurrentPageTreeViewsObj.trvPosts_Post = document.getElementById('trvPosts_Post').clientWidth + 'px';
}

function trvPosts_Post_onNodeSelect(sender, e) {
    if (CurrentPageState_Posts != 'Add')
        NavigatePost_Posts(e.get_node());
    PostsSelectedType_Posts = 'Normal';
}

function NavigatePost_Posts(selectedPostNode) {
    if (selectedPostNode != undefined) {
        var OrgPostNodeValue = selectedPostNode.get_value();
        OrgPostNodeValue = eval('(' + OrgPostNodeValue + ')');
        document.getElementById('txtOrgPostCode_Posts').value = OrgPostNodeValue.CustomCode;
        document.getElementById('txtOrgPostName_Posts').value = selectedPostNode.get_text();
        document.getElementById('txtPersonnelName_Posts').value = OrgPostNodeValue.PersonnelName;
        document.getElementById('txtPersonnelCode_Posts').value = OrgPostNodeValue.PersonnelCode;
    }
}

function tlbItemNew_TlbPosts_onClick() {
    ChangePageState_Posts('Add');
    ClearList_Posts();
    FocusOnFirstElement_Posts();
}

function tlbItemEdit_TlbPosts_onClick() {
    ChangePageState_Posts('Edit');
    FocusOnFirstElement_Posts();
}

function tlbItemFormReconstruction_TlbPosts_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvPostsIntroduction_iFrame').src =parent.ModulePath + 'OrganizationPosts.aspx';
}

function tlbItemDelete_TlbPosts_onClick() {
    ChangePageState_Posts('Delete');
}

function tlbItemSave_TlbPosts_onClick() {
    Post_onSave();
}

function Post_onSave() {
    if (CurrentPageState_Posts != 'Delete')
        UpdatePost_Posts();
    else
        ShowDialogConfirm('Delete');
}

function UpdatePost_Posts() {
    ObjPost_Posts = new Object();
    ObjPost_Posts.CustomCode = null;
    ObjPost_Posts.Name = null;
    ObjPost_Posts.SelectedID = '0';
    ObjPost_Posts.ParentID = '0';
    ObjPost_Posts.ParentPath = '';
    ObjPost_Posts.PersonnelID = '0';
    ObjPost_Posts.PersonnelName = '';
    ObjPost_Posts.PersonnelCode = '';
    ObjPost_Posts.ChildCount = '0';

    switch (PostsSelectedType_Posts) {
        case 'Normal':
            var SelectedPostNode_Posts = trvPosts_Post.get_selectedNode();
            if (SelectedPostNode_Posts != undefined)
                ObjPost_Posts.SelectedID = SelectedPostNode_Posts.get_id();
            break;
        case 'Search':
            var selectedItem_cmbPostsSearchResult_Posts = cmbPostsSearchResult_Posts.getSelectedItem();
            if (selectedItem_cmbPostsSearchResult_Posts != undefined && selectedItem_cmbPostsSearchResult_Posts != null) {
                ObjPost_Posts.SelectedID = selectedItem_cmbPostsSearchResult_Posts.get_id();
            }
            break;
    }

    if (CurrentPageState_Posts != 'Delete') {
        ObjPost_Posts.CustomCode = document.getElementById('txtOrgPostCode_Posts').value;
        ObjPost_Posts.Name = document.getElementById('txtOrgPostName_Posts').value;
        if (SelectedPostNode_Posts != undefined) {
            var SelectedPostNodeValue_Posts = SelectedPostNode_Posts.get_value();
            SelectedPostNodeValue_Posts = eval('(' + SelectedPostNodeValue_Posts + ')');
            switch (CurrentPageState_Posts) {
                case 'Add':
                    ObjPost_Posts.ParentPath = SelectedPostNodeValue_Posts.ParentPath + ObjPost_Posts.SelectedID + ',';
                    ObjPost_Posts.ChildCount = '0';
                    ObjPost_Posts.ParentID = ObjPost_Posts.SelectedID;
                    break;
                case 'Edit':
                    ObjPost_Posts.ParentPath = SelectedPostNodeValue_Posts.ParentPath;
                    ObjPost_Posts.PersonnelID = SelectedPostNodeValue_Posts.PersonnelID;
                    ObjPost_Posts.PersonnelName = SelectedPostNodeValue_Posts.PersonnelName;
                    ObjPost_Posts.PersonnelCode = SelectedPostNodeValue_Posts.PersonnelCode;
                    ObjPost_Posts.ChildCount = SelectedPostNodeValue_Posts.ChildCount;
                    if (SelectedPostNodeValue_Posts != undefined && SelectedPostNode_Posts.get_parentNode() != null && SelectedPostNode_Posts.get_parentNode() != undefined)
                        ObjPost_Posts.ParentID = SelectedPostNode_Posts.get_parentNode().get_id();
                    else
                        if (selectedItem_cmbPostsSearchResult_Posts != undefined && selectedItem_cmbPostsSearchResult_Posts != null)
                            ObjPost_Posts.ParentID = eval('(' + selectedItem_cmbPostsSearchResult_Posts.get_value() + ')').ParentID;
                        else
                            ObjPost_Posts.ParentID = '0';
                    break;
            }
        }
    }
    else {
        if (SelectedPostNode_Posts != undefined && SelectedPostNode_Posts.get_parentNode() != null && SelectedPostNode_Posts.get_parentNode() != undefined) {
            ObjPost_Posts.ParentID = SelectedPostNode_Posts.get_parentNode().get_id();
            var ObjPostValue_Posts = eval('(' + SelectedPostNode_Posts.get_value() + ')');
            ObjPost_Posts.ParentPath = ObjPostValue_Posts.ParentPath;
        }
        else {
            if (selectedItem_cmbPostsSearchResult_Posts != undefined && selectedItem_cmbPostsSearchResult_Posts != null) {
                var ObjPostValue_Posts = eval('(' + selectedItem_cmbPostsSearchResult_Posts.get_value() + ')');
                ObjPost_Posts.ParentID = ObjPostValue_Posts.ParentID;
                ObjPost_Posts.ParentPath = ObjPostValue_Posts.ParentPath;
            }
            else {
                ObjPost_Posts.ParentID = '0';
                ObjPost_Posts.ParentPath = '';
            }
        }
    }
    UpdatePost_PostsPage(CharToKeyCode_Posts(CurrentPageState_Posts), CharToKeyCode_Posts(ObjPost_Posts.SelectedID), CharToKeyCode_Posts(ObjPost_Posts.ParentID), CharToKeyCode_Posts(ObjPost_Posts.PersonnelID), CharToKeyCode_Posts(ObjPost_Posts.CustomCode), CharToKeyCode_Posts(ObjPost_Posts.Name), CharToKeyCode_Posts(ObjPost_Posts.ParentPath));
    DialogWaiting.Show();
}


function UpdatePost_PostsPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Posts').value;
            Response[1] = document.getElementById('hfConnectionError_Posts').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            Post_OnAfterUpdate(Response);
            UpdateParentNode_OnAfterUpdate(Response[3]);
            Clear_cmbPostsSearchResult_Posts();
            ClearList_Posts();
            ChangePageState_Posts('View');
        }
        else {
            if (CurrentPageState_Posts == 'Delete')
                ChangePageState_Posts('View');
        }
    }
}

function Post_OnAfterUpdate(Response) {
    trvPosts_Post.beginUpdate();
    switch (CurrentPageState_Posts) {
        case 'Add':
            var newPostNode = new ComponentArt.Web.UI.TreeViewNode();
            newPostNode.set_text(ObjPost_Posts.Name);
            newPostNode.set_value(GetOrganizationPostNodeValueObj_Posts());
            newPostNode.set_id(Response[3]);
            newPostNode.set_imageUrl('Images/TreeView/folder.gif');
            trvPosts_Post.findNodeById(ObjPost_Posts.SelectedID).get_nodes().add(newPostNode);
            trvPosts_Post.selectNodeById(ObjPost_Posts.SelectedID);
            break;
        case 'Edit':
            var selectedPostNode = trvPosts_Post.findNodeById(Response[3]);
            selectedPostNode.set_text(ObjPost_Posts.Name);
            selectedPostNode.set_value(GetOrganizationPostNodeValueObj_Posts());
            trvPosts_Post.selectNodeById(Response[3]);
            break;
        case 'Delete':
            trvPosts_Post.findNodeById(ObjPost_Posts.SelectedID).remove();
            break;

    }
    trvPosts_Post.endUpdate();
    Resize_trvPosts_Posts();
    ChangeDirection_trvPosts_Posts();
}

function UpdateParentNode_OnAfterUpdate(nodeID) {
    var selectedPostNode = trvPosts_Post.findNodeById(nodeID);
    var parentPostNode = selectedPostNode.get_parentNode();
    if (parentPostNode != null && parentPostNode != undefined) {
        var parentPostNodeChildCount = 0;
        var parentPostNodeValue = parentPostNode.get_value();
        parentPostNodeValue = eval('(' + parentPostNodeValue + ')');
        trvPosts_Post.beginUpdate();
        switch (CurrentPageState_Posts) {
            case 'Add':
                parentPostNodeChildCount = parseInt(parentPostNodeValue.ChildCount) + 1;
                break;
            case 'Edit':
                parentPostNodeChildCount = parseInt(parentPostNodeValue.ChildCount);
                break;
            case 'Delete':
                if (parseInt(parentPostNodeValue.ChildCount) > 0)
                    parentPostNodeChildCount = parseInt(parentPostNodeValue.ChildCount) - 1;
                break;
        }
        parentPostNodeValue = '{"CustomCode":"' + parentPostNodeValue.CustomCode + '","ParentPath":"' + parentPostNodeValue.ParentPath + '","PersonnelName":"' + parentPostNodeValue.PersonnelName + '","PersonnelCode":"' + parentPostNodeValue.PersonnelCode + '","PersonnelID":"' + parentPostNodeValue.PersonnelID + '","ChildCount":"' + parentPostNodeChildCount + '","ParentID":"' + parentPostNodeValue.ParentID + '"}';
        parentPostNode.set_value(parentPostNodeValue);
        trvPosts_Post.endUpdate();
    }
}

function Clear_cmbPostsSearchResult_Posts() {
    cmbPostsSearchResult_Posts.beginUpdate();
    cmbPostsSearchResult_Posts.removeAll();
    cmbPostsSearchResult_Posts.endUpdate();
}

function GetOrganizationPostNodeValueObj_Posts() {
    var StrObjOrganizationPostNodeValue = '';
    if (ObjPost_Posts != null)
        StrObjOrganizationPostNodeValue = '{"CustomCode":"' + ObjPost_Posts.CustomCode + '","ParentPath":"' + ObjPost_Posts.ParentPath + '","PersonnelName":"' + ObjPost_Posts.PersonnelName + '","PersonnelCode":"' + ObjPost_Posts.PersonnelCode + '","PersonnelID":"' + ObjPost_Posts.PersonnelID + '","ChildCount":"' + ObjPost_Posts.ChildCount + '","":"' + ObjPost_Posts.ParentID + '"}';
    return StrObjOrganizationPostNodeValue;
}

function tlbItemCancel_TlbPosts_onClick() {
    ChangePageState_Posts('View');
    ClearList_Posts();
}

function tlbItemExit_TlbPosts_onClick() {
    ShowDialogConfirm('Exit');
}

function ChangePageState_Posts(state) {
    CurrentPageState_Posts = state;
    SetActionMode_Posts(state);
    if (state == 'Add' || state == 'Edit' || state == 'Delete') {
        if (TlbPosts.get_items().getItemById('tlbItemNew_TlbPosts') != null) {
            TlbPosts.get_items().getItemById('tlbItemNew_TlbPosts').set_enabled(false);
            TlbPosts.get_items().getItemById('tlbItemNew_TlbPosts').set_imageUrl('add_silver.png');
        }
        if (TlbPosts.get_items().getItemById('tlbItemEdit_TlbPosts') != null) {
            TlbPosts.get_items().getItemById('tlbItemEdit_TlbPosts').set_enabled(false);
            TlbPosts.get_items().getItemById('tlbItemEdit_TlbPosts').set_imageUrl('edit_silver.png');
        }
        if (TlbPosts.get_items().getItemById('tlbItemDelete_TlbPosts') != null) {
            TlbPosts.get_items().getItemById('tlbItemDelete_TlbPosts').set_enabled(false);
            TlbPosts.get_items().getItemById('tlbItemDelete_TlbPosts').set_imageUrl('remove_silver.png');
        }
        TlbPosts.get_items().getItemById('tlbItemSave_TlbPosts').set_enabled(true);
        TlbPosts.get_items().getItemById('tlbItemSave_TlbPosts').set_imageUrl('save.png');
        TlbPosts.get_items().getItemById('tlbItemCancel_TlbPosts').set_enabled(true);
        TlbPosts.get_items().getItemById('tlbItemCancel_TlbPosts').set_imageUrl('cancel.png');
        document.getElementById('txtOrgPostCode_Posts').disabled = '';
        document.getElementById('txtOrgPostName_Posts').disabled = '';
        if (state == 'Edit') {
            switch (PostsSelectedType_Posts) {
                case 'Normal':
                    NavigatePost_Posts(trvPosts_Post.get_selectedNode());
                    break;
                case 'Search':
                    NavigatePost_Posts(cmbPostsSearchResult_Posts.getSelectedItem());
                    break;
            }
        }

        if (state == 'Delete')
            Post_onSave();
    }
    if (state == 'View') {
        if (TlbPosts.get_items().getItemById('tlbItemNew_TlbPosts') != null) {
            TlbPosts.get_items().getItemById('tlbItemNew_TlbPosts').set_enabled(true);
            TlbPosts.get_items().getItemById('tlbItemNew_TlbPosts').set_imageUrl('add.png');
        }
        if (TlbPosts.get_items().getItemById('tlbItemEdit_TlbPosts') != null) {
            TlbPosts.get_items().getItemById('tlbItemEdit_TlbPosts').set_enabled(true);
            TlbPosts.get_items().getItemById('tlbItemEdit_TlbPosts').set_imageUrl('edit.png');
        }
        if (TlbPosts.get_items().getItemById('tlbItemDelete_TlbPosts') != null) {
            TlbPosts.get_items().getItemById('tlbItemDelete_TlbPosts').set_enabled(true);
            TlbPosts.get_items().getItemById('tlbItemDelete_TlbPosts').set_imageUrl('remove.png');
        }
        TlbPosts.get_items().getItemById('tlbItemSave_TlbPosts').set_enabled(false);
        TlbPosts.get_items().getItemById('tlbItemSave_TlbPosts').set_imageUrl('save_silver.png');
        TlbPosts.get_items().getItemById('tlbItemCancel_TlbPosts').set_enabled(false);
        TlbPosts.get_items().getItemById('tlbItemCancel_TlbPosts').set_imageUrl('cancel_silver.png');
        document.getElementById('txtOrgPostCode_Posts').disabled = 'disabled';
        document.getElementById('txtOrgPostName_Posts').disabled = 'disabled';
    }
}

function SetActionMode_Posts(state) {
    document.getElementById('ActionMode_Posts').innerHTML = document.getElementById("hf" + state + "_Posts").value;
}

function ClearList_Posts() {
    if (CurrentPageState_Posts != 'Edit') {
        document.getElementById('txtOrgPostCode_Posts').value = '';
        document.getElementById('txtOrgPostName_Posts').value = '';
        document.getElementById('txtPersonnelName_Posts').value = '';
        document.getElementById('txtPersonnelCode_Posts').value = '';
    }
}

function FocusOnFirstElement_Posts() {
    document.getElementById('txtOrgPostCode_Posts').focus();
}

function CharToKeyCode_Posts(str) {
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

function Fill_trvPosts_Post() {
    document.getElementById('loadingPanel_trvPosts_Post').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_trvPosts_Post').value);
    ObjexpandingOrgPostNode_Posts = null;
    CallBack_trvPosts_Post.callback();
}


function Refresh_trvPosts_Post() {
    Fill_trvPosts_Post();
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Posts = confirmState;
    if (CurrentPageState_Posts == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Posts').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Posts').value;
    DialogConfirm.Show();
}

function tlbItemOk_TlbOkConfirm_onClick() {
    switch (ConfirmState_Posts) {
        case 'Delete':
            DialogConfirm.Close();
            UpdatePost_Posts();
            break;
        case 'Exit':
            ClearList_Posts();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
        default:
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Posts('View');
}

function trvPosts_Post_onLoad(sender, e) {
    document.getElementById('loadingPanel_trvPosts_Post').innerHTML = "";
}

function Fill_trvPosts_Posts() {
    CallBack_trvPosts_Post.callback();
}

function trvPosts_Post_onCallbackComplete(sender, e) {
    if (ObjexpandingOrgPostNode_Posts != null) {
        if (ObjexpandingOrgPostNode_Posts.Node.get_nodes().get_length() == 0 && ObjexpandingOrgPostNode_Posts.HasChild) {
            ObjexpandingOrgPostNode_Posts = null;
            GetLoadonDemandError_PostsPage();
        }
        else
            ObjexpandingOrgPostNode_Posts = null;
    }
}

function GetLoadonDemandError_PostsPage_onCallBack(Response) {
    if (Response != '') {
        var ResponseParts = eval('(' + Response + ')');
        showDialog(ResponseParts[0], ResponseParts[1], ResponseParts[2]);
    }
}

function trvPosts_Post_onNodeBeforeExpand(sender, e) {
    if (ObjexpandingOrgPostNode_Posts != null)
        ObjexpandingOrgPostNode_Posts = null;
    ObjexpandingOrgPostNode_Posts = new Object();
    ObjexpandingOrgPostNode_Posts.Node = e.get_node();
    if (e.get_node().get_nodes().get_length() == 1 && (e.get_node().get_nodes().get_nodeArray()[0].get_id() == undefined || e.get_node().get_nodes().get_nodeArray()[0].get_id() == '')) {
        ObjexpandingOrgPostNode_Posts.HasChild = true;
        trvPosts_Post.beginUpdate();
        ObjexpandingOrgPostNode_Posts.Node.get_nodes().remove(0);
        trvPosts_Post.endUpdate();
    }
    else {
        if (e.get_node().get_nodes().get_length() == 0)
            ObjexpandingOrgPostNode_Posts.HasChild = false;
        else
            ObjexpandingOrgPostNode_Posts.HasChild = true;
    }
}

function CallBack_trvPosts_Post_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Posts').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        if (errorParts[3] == 'Reload')
            Fill_trvPosts_Post();
    }
    else {
        Resize_trvPosts_Posts();
        ChangeDirection_trvPosts_Posts();
    }
}

function CallBack_trvPosts_Post_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_trvPosts_Post').innerHTML = '';
    ShowConnectionError_Posts();
}

function ShowConnectionError_Posts() {
    var error = document.getElementById('hfErrorType_Posts').value;
    var errorBody = document.getElementById('hfConnectionError_Posts').value;
    showDialog(error, errorBody, 'error');
}

function tlbItemHelp_TlbPosts_onClick() {
    LoadHelpPage('tlbItemHelp_TlbPosts');
}

function trvPosts_Post_onNodeExpand(sender, e) {
    Resize_trvPosts_Posts();
    ChangeDirection_trvPosts_Posts();
}

function Resize_trvPosts_Posts() {
    document.getElementById('trvPosts_Post').style.width = CurrentPageTreeViewsObj.trvPosts_Post;
}

function ChangeDirection_trvPosts_Posts() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1)))
        document.getElementById('trvPosts_Post').style.direction = 'ltr';
}


function tlbItemPostsSearch_TlbPostsSearch_Posts_onClick() {
    Fill_cmbPostsSearchResult_Posts();
}

function Fill_cmbPostsSearchResult_Posts() {
    var SearchTerm = document.getElementById('txtSearchTerm_Posts').value;
    ComboBox_onBeforeLoadData('cmbPostsSearchResult_Posts');
    CallBack_cmbPostsSearchResult_Posts.callback(CharToKeyCode_Posts(SearchTerm));
}

function cmbPostsSearchResult_Posts_onChange(sender, e) {
    var selectedItem_cmbPostsSearchResult_Posts = cmbPostsSearchResult_Posts.getSelectedItem();
    if (selectedItem_cmbPostsSearchResult_Posts != undefined && selectedItem_cmbPostsSearchResult_Posts != null) {
        PostsSelectedType_Posts = 'Search';
        NavigatePost_Posts(selectedItem_cmbPostsSearchResult_Posts);
    }
}

function ChangeControlDirection_Posts(ctrl) {
    var direction = null;
    switch (parent.CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    if (ctrl == 'All') {
        document.getElementById('cmbPostsSearchResult_Posts_DropDownContent').dir =
        direction;
    }
    else
        document.getElementById(ctrl).style.direction = direction;
}

function CallBack_cmbPostsSearchResult_Posts_onBeforeCallback(sender, e) {
    cmbPostsSearchResult_Posts.dispose();
}

function CallBack_cmbPostsSearchResult_Posts_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_PostsSearchResult_Posts').value;
    if (error == "") {
        cmbPostsSearchResult_Posts.expand();
        ChangeControlDirection_Posts('cmbPostsSearchResult_Posts_DropDownContent');
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
}

function CallBack_cmbPostsSearchResult_Posts_onCallbackError(sender, e) {
    ShowConnectionError_Posts();
}

function txtSerchTerm_Posts_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemPostsSearch_TlbPostsSearch_Posts_onClick();
    }
}
function ComboBox_onBeforeLoadData(cmbID) {
    document.getElementById(cmbID).onmouseover = " ";
    document.getElementById(cmbID).onmouseout = " ";
    document.getElementById(cmbID + '_Input').onfocus = " ";
    document.getElementById(cmbID + '_Input').onblur = " ";
    document.getElementById(cmbID + '_Input').onkeydown = " ";
    document.getElementById(cmbID + '_Input').onmouseup = " ";
    document.getElementById(cmbID + '_Input').onmousedown = " ";
    document.getElementById(cmbID + '_DropImage').src = 'Images/ComboBox/ComboBoxLoading.gif';
    eval(cmbID).disable();
}
function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}



