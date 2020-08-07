<%@ Page Language="C#" AutoEventWireup="true" Inherits="RulesGroup" Codebehind="RulesGroup.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RulesGroupForm" runat="server" meta:resourcekey="RulesGroupForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>

                <table id="Mastertbl_Reports" style="width: 90%; height: 400px; font-family: Arial; font-size: small">
            <tr>
                <td valign="top">
                    <table style="width: 100%; height: 400px">
                        <tr>
                            <td style="height: 10%">
                                <table style="width: 100%;">
                        <tr>
                            <td>
                                <componentart:toolbar id="TlbRulesGroup" runat="server" cssclass="toolbar" defaultitemactivecssclass="itemActive"
                                    defaultitemcheckedcssclass="itemChecked" defaultitemcheckedhovercssclass="itemActive"
                                    defaultitemcssclass="item" defaultitemhovercssclass="itemHover" defaultitemimageheight="16px"
                                    defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText" defaultitemtextimagespacing="0"
                                    imagesbaseurl="images/ToolBar/" itemspacing="1px" usefadeeffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNewGroup_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ClientSideCommand="tlbItemNewGroup_TlbRulesGroup_onClick();"
                                                        ImageHeight="16px" ImageUrl="group.png" ImageWidth="16px" ItemType="Command"
                                                        meta:resourcekey="tlbItemNewGroup_TlbRulesGroup" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRulesGroup" runat="server" ClientSideCommand="tlbItemNew_TlbRulesGroup_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbRulesGroup"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRulesGroup" runat="server" ClientSideCommand="tlbItemEdit_TlbRulesGroup_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbRulesGroup"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemDelete_TlbRulesGroup_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                            meta:resourcekey="tlbItemDelete_TlbRulesGroup" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRulesGroup" runat="server" ClientSideCommand="tlbItemSave_TlbRulesGroup_onClick();"
                                                        Enabled="false" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                                        ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRulesGroup"
                                                        TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRulesGroup" runat="server" Enabled="false"
                                                        ClientSideCommand="tlbItemCancel_TlbRulesGroup_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRulesGroup" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemRulesView_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                            ClientSideCommand="tlbItemRulesView_TlbRulesGroup_onClick();" ItemType="Command"
                                            meta:resourcekey="tlbItemRulesView_TlbRulesGroup" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemLeaveBudget_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="LeaveBudget.png" ImageWidth="16px"
                                            ClientSideCommand="tlbItemLeaveBudget_TlbRulesGroup_onClick();" ItemType="Command"
                                            meta:resourcekey="tlbItemLeaveBudget_TlbRulesGroup" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemRuleGroupCopy_TlbRulesGroup" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="copy.png"
                                            ImageWidth="16px" ClientSideCommand="tlbItemRuleGroupCopy_TlbRulesGroup_onClick();"
                                            ItemType="Command" meta:resourcekey="tlbItemRuleGroupCopy_TlbRulesGroup" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRulesGroup" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRulesGroup" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbRulesGroup_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRulesGroup" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbRulesGroup_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRulesGroup"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRulesGroup" runat="server" ClientSideCommand="tlbItemExit_TlbRulesGroup_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRulesGroup"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <%--<td id="ActionMode_RulesGroup" class="ToolbarMode"></td>--%>
                        </tr>
                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 60%">
                                            <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_RulesGroup_RulesGroup" class="HeaderLabel" style="width: 20%">Rules Group
                                        </td>
                                        <td id="loadingPanel_trvRulesGroup_RulesGroup" class="HeaderLabel" style="width: 15%"></td>
                                        <td style="width: 35%">
                                                                     <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtQuickSearch_RulesGroup" onkeypress="txtDataAccessLevelsSourceQuickSearch_MultiLevelsDataAccessLevels_onKeyPress(event)" />
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <ComponentArt:ToolBar ID="TlbQuickSearch_RulesGroup"
                                                                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                    UseFadeEffect="false">
                                                                                    <Items>
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbQuickSearch_RulesGroup"
                                                                                            runat="server" ClientSideCommand="tlbItemSearch_TlbQuickSearch_RulesGroup_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbQuickSearch_RulesGroup"
                                                                                            TextImageSpacing="5" />
                                                                                    </Items>
                                                                                </ComponentArt:ToolBar>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                        <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_trvRulesGroup_RulesGroup" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvRulesGroup_RulesGroup"
                                                        runat="server" ClientSideCommand="Refresh_trvRulesGroup_RulesGroup();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvRulesGroup_RulesGroup"
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_trvRulesGroup_RulesGroup" OnCallback="CallBack_trvRulesGroup_RulesGroup_onCallBack">
                                    <Content>
                                        <ComponentArt:TreeView ID="trvRulesGroup_RulesGroup" runat="server" BorderColor="Black"
                                            CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                            DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                            ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                            Height="300" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                            LineImageHeight="20" LineImageWidth="19" meta:resourcekey="trvRulesGroup_RulesGroup"
                                            NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                            SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                            <ClientEvents>
                                                <NodeMouseDoubleClick EventHandler="trvRulesGroup_RulesGroup_onNodeMouseDoubleClick" />
                                                <Load EventHandler="trvRulesGroup_RulesGroup_onLoad" />
                                                <NodeExpand EventHandler="trvRulesGroup_RulesGroup_onNodeExpand" />
                                            </ClientEvents>
                                        </ComponentArt:TreeView>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RulesGroup" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="trvRulesGroup_RulesGroup_onCallbackComplete" />
                                        <CallbackError EventHandler="trvRulesGroup_RulesGroup_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                                        </td>
                                        <td>
                                            <table style="width: 90%">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td id="ActionMode_RulesGroup" class="CulturToolbarMode"></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;" class="BoxStyle">

                                                            <tr>
                                                                <td class="DetailsBoxHeaderStyle">
                                                                    <div id="header_RuleGroupDetails_RuleGroups" runat="server" meta:resourcekey="AlignObj"
                                                                        style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                                        Rule Group Details
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblRuleGroupName_RuleGroups" runat="server" Text="نام گروه قانون :"
                                                                        CssClass="WhiteLabel" meta:resourcekey="lblRuleGroupName_RuleGroups"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input id="txtRuleGroupName_RulesGroup" type="text" class="TextBoxes" style="width: 98%"
                                                                        disabled="disabled" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="280px">
            <Content>
                <table style="width: 100%;" class="ConfirmStyle">
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
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancel"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
            runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogWaiting">
            <Content>
                <table>
                    <tr>
                        <td>
                            <img id="Img1" runat="server" alt="" src="~/DesktopModules/Atlas/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfheader_RulesGroup_RulesGroup" meta:resourcekey="hfheader_RulesGroup_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_trvRulesGroup_RulesGroup" meta:resourcekey="hfloadingPanel_trvRulesGroup_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfView_RulesGroup" meta:resourcekey="hfView_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RulesGroup" meta:resourcekey="hfCloseMessage_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_RulesGroup" meta:resourcekey="hfDeleteMessage_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfErrorType_RulesGroup" meta:resourcekey="hfErrorType_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RulesGroup" meta:resourcekey="hfConnectionError_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfAddGroup_RulesGroup" meta:resourcekey="hfView_RulesGroups" />
        <asp:HiddenField runat="server" ID="hfEdit_RulesGroup" meta:resourcekey="hfEdit_RulesGroup" />
        <asp:HiddenField runat="server" ID="hfRuleGroup_RulesGroup" meta:resourcekey="hfRuleGroup_RuleGroups" />
    </form>
</body>
</html>
