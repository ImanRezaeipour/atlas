<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.Roles" Codebehind="Roles.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RolesForm" runat="server" meta:resourcekey="RolesForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; height: 400px; font-family: Arial; font-size: small;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbRoles" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRoles" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ClientSideCommand="tlbItemNew_TlbRoles_onClick();"
                                        ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRoles" runat="server" ClientSideCommand="tlbItemEdit_TlbRoles_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRoles" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ClientSideCommand="tlbItemDelete_TlbRoles_onClick();"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDelete_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRoles" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRoles" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbRoles_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRoles" runat="server" ClientSideCommand="tlbItemSave_TlbRoles_onClick();"
                                        Enabled="false" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                        ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRoles" runat="server" Enabled="false"
                                        ClientSideCommand="tlbItemCancel_TlbRoles_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRoles" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemUserInterfaceAccessLevels_TlbRoles" runat="server"
                                        ClientSideCommand="tlbItemUserInterfaceAccessLevels_TlbRoles_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="access.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemUserInterfaceAccessLevels_TlbRoles"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemGridSettings_TlbRoles" runat="server" ClientSideCommand="tlbItemGridSettings_TlbRoles_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="package_settings.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemGridSettings_TlbRoles"
                                        TextImageSpacing="5"  />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRoles" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbRoles_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRoles" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRoles" runat="server" ClientSideCommand="tlbItemExit_TlbRoles_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRoles"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_RolesForm" class="ToolbarMode" style="width: 40%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Roles_Roles" class="HeaderLabel" style="width: 50%">
                                                    Roles
                                                </td>
                                                <td id="loadingPanel_trvRoles_Roles" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvRoles_Roles" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvRoles_Roles" runat="server"
                                                                ClientSideCommand="Refresh_trvRoles_Roles();" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                                ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command"
                                                                meta:resourcekey="tlbItemRefresh_TlbRefresh_trvRoles_Roles" TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvRoles_Roles" OnCallback="CallBack_trvRoles_Roles_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvRoles_Roles" runat="server" ExpandNodeOnSelect="true"
                                                    CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                    EnableViewState="false" ExpandCollapseImageHeight="15" LoadingFeedbackText="loading......."
                                                    ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                    ForceHighlightedNodeID="true" Height="330" HoverNodeCssClass="HoverNestingTreeNode"
                                                    ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" meta:resourcekey="trvRoles_Roles"
                                                    BorderColor="Black">
                                                    <ClientEvents>
                                                        <NodeSelect EventHandler="trvRoles_Roles_onNodeSelect" />
                                                        <Load EventHandler="trvRoles_Roles_onLoad" />
                                                        <NodeExpand EventHandler="trvRoles_Roles_onNodeExpand"/>
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField ID="ErrorHiddenField_Roles" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvRoles_Roles_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvRoles_Roles_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table style="width: 80%;" class="BoxStyle" id="tblRoles_Roles">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_RoleDetails_Roles" runat="server" class="BoxContainerHeader" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%">
                                            Role Details</div>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server" meta:resourcekey="AlignObj">
                                    <td id="Td1" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRoleCode_Roles" runat="server" meta:resourcekey="lblRoleCode_Roles"
                                                        Text=": کد نقش" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtRoleCode_Roles"
                                                        disabled="disabled"   />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRoleName_Roles" runat="server" meta:resourcekey="lblRoleName_Roles"
                                                        Text=": نام نقش" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtRoleName_Roles"
                                                        disabled="disabled"   />
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
        <tr>
            <td>
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
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogGridSettings"
            runat="server" Width="200px">
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 100%;"
                    meta:resourcekey="tblGridSettings_DialogGridSettings"
                    class="BodyStyle">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbGridSettings" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGridSettings" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbGridSettings_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbGridSettings"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbGridSettings" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbGridSettings_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbGridSettings"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td id="header_GridSettings_Roles" style="color: White; font-weight: bold; font-family: Arial; width: 100%">Grid Settings
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 60%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAll_GridSettings_Roles" type="checkbox" checked="checked"
                                                        onclick="chbSelectAll_GridSettings_Roles_onClick();" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblSelectAll_GridSettings_Roles" meta:resourcekey="lblSelectAll_GridSettings_Roles"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridSettings_Roles"
                                            OnCallback="CallBack_GridSettings_Roles_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridSettings_Roles" runat="server" AllowMultipleSelect="false"
                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" Height="495"
                                                    ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerStyle="Numbered"
                                                    ShowFooter="false" PagerTextCssClass="GridFooterText" PageSize="13" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" TabIndex="0" Width="180" AllowColumnResizing="false"
                                                    ScrollBar="Auto" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                <ComponentArt:GridColumn DataField="RoleID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                <%--<ComponentArt:GridColumn DataField="Exist" Visible="false" ColumnType="CheckBox" />--%>
                                                                <ComponentArt:GridColumn Align="Center" DataField="GridColumn" DefaultSortDirection="Descending"
                                                                    HeadingText="ستون گرید" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGridColumn_GridSettings_Roles" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ViewState" DefaultSortDirection="Descending"
                                                                    HeadingText="نمایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnView_GridSettings_Roles"
                                                                    ColumnType="CheckBox" AllowEditing="True" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_GridSettings_Roles" />
                                                <asp:HiddenField runat="server" ID="hfExist_GridSettings_Roles" />
                                                 <asp:HiddenField runat="server" ID="hfId_GridSettings_Roles" />
                                                <asp:HiddenField runat="server" ID="hfRuleId_GridSettings_Roles" />
                                                <asp:HiddenField runat="server" ID="hfSuccessType_GridSettings_Roles" />
                                                 <asp:HiddenField runat="server" ID="hfComplete_GridSettings_Roles" />
                                                <asp:HiddenField runat="server" ID="hfSuccess_GridSettings_Roles" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridSettings_Roles_CallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridSettings_Roles_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfheader_Roles_Roles" meta:resourcekey="hfheader_Roles_Roles" />
    <asp:HiddenField runat="server" ID="hfheader_RoleDetails_Roles" meta:resourcekey="hfheader_RoleDetails_Roles" />
    <asp:HiddenField runat="server" ID="hfView_Roles" meta:resourcekey="hfView_Roles" />
    <asp:HiddenField runat="server" ID="hfAdd_Roles" meta:resourcekey="hfAdd_Roles" />
    <asp:HiddenField runat="server" ID="hfEdit_Roles" meta:resourcekey="hfEdit_Roles" />
    <asp:HiddenField runat="server" ID="hfDelete_Roles" meta:resourcekey="hfDelete_Roles" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Roles" meta:resourcekey="hfDeleteMessage_Roles" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Roles" meta:resourcekey="hfCloseMessage_Roles" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_trvRoles_Roles" meta:resourcekey="hfloadingPanel_trvRoles_Roles" />
    <asp:HiddenField runat="server" ID="hfAccessor_trvRoles" />
    <asp:HiddenField runat="server" ID="hfErrorType_Roles" meta:resourcekey="hfErrorType_Roles" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Roles" meta:resourcekey="hfConnectionError_Roles" />
     <asp:HiddenField runat="server" ID="hfheader_GridSettings_Roles" meta:resourcekey="hfheader_GridSettings_Roles" />
    </form>
</body>
</html>
