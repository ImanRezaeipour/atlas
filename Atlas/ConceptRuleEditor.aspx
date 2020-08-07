<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.ConceptRuleEditor" Codebehind="ConceptRuleEditor.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="conceptEditorForm" runat="server" meta:resourcekey="conceptEditorForm">
        <div>
            <table style="width: 100%; height: 400px; font-family: Arial; font-size: small;" class="BoxStyle">
                <tbody>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td runat="server" style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbConcepts" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbConceptRuleEditor" runat="server" ClientSideCommand="tlbItemSave_TlbConceptRuleEditor_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                    ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbItemSave_TlbConceptRuleEditor"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbConceptRuleEditor" runat="server" ClientSideCommand="tlbItemHelp_TlbConceptRuleEditor_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                                    ImageWidth="16px" ConceptType="Command" meta:resourcekey="tlbItemHelp_TlbConceptRuleEditor"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbConceptRuleEditor" runat="server"
                                                        ClientSideCommand="tlbItemFormReconstruction_TlbConceptRuleEditor_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbConceptRuleEditor" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbConceptRuleEditor" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemExit_TlbConceptRuleEditor_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ConceptType="Command"
                                                    meta:resourcekey="tlbItemExit_TlbConceptRuleEditor" TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" valign="top">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 49%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="header_Expressions_ConceptRuleEditor" class="HeaderLabel" style="width: 50%">Expressions
                                                            </td>
                                                            <td id="loadingPanel_trvConceptExpression_ConceptRuleEditor" class="HeaderLabel"
                                                                style="width: 45%"></td>
                                                            <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                <ComponentArt:ToolBar ID="TlbConceptsExpression" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbConceptsExpression" runat="server"
                                                                            ClientSideCommand="tlbItemRefresh_TlbConceptsExpression_onClick();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                            ConceptType="Command" meta:resourcekey="tlbItemRefresh_TlbConceptsExpression" TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_trvConceptExpression_ConceptRuleEditor"
                                                        OnCallback="CallBack_trvConceptExpression_ConceptRuleEditor_onCallBack">
                                                        <Content>
                                                            <ComponentArt:TreeView ID="trvConceptExpression_ConceptRuleEditor" runat="server" ExpandNodeOnSelect="false"
                                                                CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                                DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                                EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                                ExpandImageUrl="images/TreeView/col.gif" FillContainer="false" ForceHighlightedNodeID="true"
                                                                Height="300" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                                LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit"
                                                                LoadingFeedbackText="Loading" NodeIndent="17" NodeLabelPadding="3" meta:resourcekey="trvConceptExpression_ConceptRuleEditor"
                                                                SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                                <ClientEvents>
                                                                    <NodeSelect EventHandler="trvConceptExpression_ConceptRuleEditor_onNodeSelect" />
                                                                    <Load EventHandler="trvConceptExpression_ConceptRuleEditor_onLoad" />
                                                                    <CallbackComplete EventHandler="trvConceptExpression_ConceptRuleEditor_onCallbackComplete" />
                                                                    <NodeBeforeExpand EventHandler="trvConceptExpression_ConceptRuleEditor_onNodeBeforeExpand" />
                                                                    <NodeRename EventHandler="trvConceptExpression_ConceptRuleEditor_onNodeRename" />
                                                                    <NodeExpand EventHandler="trvConceptExpression_ConceptRuleEditor_onNodeExpand"/>
                                                                    <NodeMouseDoubleClick EventHandler="trvConceptExpression_ConceptRuleEditor_onNodeMouseDoubleClick" />
                                                                </ClientEvents>
                                                            </ComponentArt:TreeView>
                                                            <asp:HiddenField ID="ErrorHiddenField_trvConceptExpression_ConceptRuleEditor" runat="server" />
                                                            <asp:HiddenField ID="VariableItemCodeInExpression" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <CallbackComplete EventHandler="CallBack_trvConceptExpression_ConceptRuleEditor_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_trvConceptExpression_ConceptRuleEditor_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 2%" align="center" valign="middle">
                                        <ComponentArt:ToolBar ID="TlbInterAction_ConceptRuleEditor" runat="server" CssClass="verticaltoolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbInterAction_ConceptRuleEditor" runat="server" ClientSideCommand="tlbItemAdd_TlbInterAction_ConceptRuleEditor_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbInterAction_ConceptRuleEditor" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbInterAction_ConceptRuleEditor" runat="server" ClientSideCommand="tlbItemDelete_TlbInterAction_ConceptRuleEditor_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbInterAction_ConceptRuleEditor" TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td style="width: 49%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="header_Content_ConceptRuleEditor" class="HeaderLabel" style="width: 50%">Components
                                                            </td>
                                                            <td id="loadingPanel_trvDetails_ConceptRuleEditor" class="HeaderLabel"
                                                                style="width: 45%"></td>
                                                            <td id="Td3" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                <ComponentArt:ToolBar ID="TlbConceptsDetails" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="TlbItemDetails_Refresh_JsonObj_TlbConceptsDetails" runat="server"
                                                                            ClientSideCommand="TlbItemDetails_Refresh_JsonObj_TlbConceptsDetails_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="flow.png"
                                                                            ImageWidth="16px" ConceptType="Command" meta:resourcekey="TlbConceptsDetails_Refresh_JsonObj_TlbConceptsDetails"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack runat="server" ID="CallBack_trvDetails_ConceptRuleEditor" OnCallback="CallBack_trvDetails_ConceptRuleEditor_onCallBack">
                                                        <Content>
                                                            <ComponentArt:TreeView ID="trvDetails_ConceptRuleEditor" runat="server" ExpandNodeOnSelect="true"
                                                                CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                                DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                                EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                                ExpandImageUrl="images/TreeView/col.gif" FillContainer="false" ForceHighlightedNodeID="true"
                                                                Height="300" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                                LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit"
                                                                LoadingFeedbackText="Loading" NodeIndent="17" NodeLabelPadding="3" meta:resourcekey="trvDetails_ConceptRuleEditor"
                                                                SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                                <ClientEvents>
                                                                    <NodeSelect EventHandler="trvDetails_ConceptRuleEditor_onNodeSelect" />
                                                                    <Load EventHandler="trvDetails_ConceptRuleEditor_onLoad" />
                                                                    <NodeBeforeRename EventHandler="trvDetails_ConceptRuleEditor_onNodeBeforeRename" />
                                                                    <NodeExpand EventHandler="trvDetails_ConceptRuleEditor_onNodeExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:TreeView>
                                                            <asp:HiddenField ID="ErrorHiddenField_trvDetails_ConceptRuleEditor" runat="server" />
                                                            <asp:HiddenField ID="ObjectJsonHiddenField_trDetails_Concept" runat="server" />
                                                            <asp:HiddenField ID="hfPageIsLoadedBefore" runat="server" />
                                                            <asp:HiddenField ID="hfConceptOrRuleIdentification" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <CallbackComplete EventHandler="CallBack_trvDetails_ConceptRuleEditor_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_trvDetails_ConceptRuleEditor_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td id="ConceptExpressionScriptFa" class="CellText" style="width: 100%; height:140px"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="visibility: hidden;">
                        <td>
                            <table>
                                <tr>
                                    <td id="ConceptExpressionScript" class="CellText" style="width: 100%;"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr style="visibility: hidden;">
                        <td>
                            <table>
                                <tr>
                                    <td id="ConceptExpressionScriptFull" class="CellText" style="width: 100%;"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="280px">
            <Content>
                <table id="tblConfirm_DialogConfirm" style="width: 100%;" class="ConfirmStyle">
                    <tr align="center">
                        <td colspan="2">
                            <asp:Label ID="lblConfirm" runat="server" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 50%">
                            <ComponentArt:ToolBar ID="TlbOkConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemOk_TlbOkConfirm" runat="server" ClientSideCommand="tlbItemOk_TlbOkConfirm_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemOk_TlbOkConfirm"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td>
                            <ComponentArt:ToolBar ID="TlbCancelConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCancelConfirm" runat="server" ClientSideCommand="tlbItemCancel_TlbCancelConfirm_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <asp:HiddenField ID="hfTitle_DialogConceptRuleEditor" runat="server" meta:resourcekey="hfTitle_DialogConceptRuleEditor" />
        <asp:HiddenField ID="hfADD_Concepts" runat="server" meta:resourcekey="hfNew_Concepts" />
        <asp:HiddenField ID="hfEdit_Concepts" runat="server" meta:resourcekey="hfEdit_Concepts" />
        <asp:HiddenField ID="hfCloseMessage_Concepts" runat="server" meta:resourcekey="hfCloseMessage_Concepts" />
        <asp:HiddenField ID="hfConnectionError_Concepts" runat="server" meta:resourcekey="hfConnectionError_Concepts" />
        <asp:HiddenField ID="hfDeleteMessage_Concepts" runat="server" meta:resourcekey="hfDeleteMessage_Concepts" />
        <asp:HiddenField ID="hfErrorType_Concepts" runat="server" meta:resourcekey="hfErrorType_Concepts" />
        <asp:HiddenField ID="hfheader_Expressions_ConceptRuleEditor" runat="server" meta:resourcekey="hfheader_Expressions_ConceptRuleEditor" />
        <asp:HiddenField ID="hfloadingPanel_trvConceptExpression_ConceptRuleEditor" runat="server" meta:resourcekey="hfloadingPanel_trvConceptExpression_ConceptRuleEditor" />
        <asp:HiddenField ID="hfheader_Content_ConceptRuleEditor" runat="server" meta:resourcekey="hfheader_Content_ConceptRuleEditor" />
        <asp:HiddenField ID="hfloadingPanel_trvDetails_ConceptRuleEditor" runat="server" meta:resourcekey="hfloadingPanel_trvDetails_ConceptRuleEditor" />
        <asp:HiddenField ID="hfRootNodeDeleteMessage_ConceptRuleEditor" runat="server" meta:resourcekey="hfRootNodeDeleteMessage_ConceptRuleEditor" />
        <asp:HiddenField ID="hfNotSaved_ConceptRyleEditor" runat="server" meta:resourcekey="hfNotSaved_ConceptRyleEditor" />
        <asp:HiddenField ID="hfNodeDeleteMessage_Concepts" runat="server" meta:resourcekey="hfNodeDeleteMessage_Concepts" />
        <asp:HiddenField ID="hfNodeWithChildDeleteMessage_Concepts" runat="server" meta:resourcekey="hfNodeWithChildDeleteMessage_Concepts" />
    </form>
</body>
</html>
