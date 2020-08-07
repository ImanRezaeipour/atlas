<%@ Page Language="C#" AutoEventWireup="true" Inherits="FlowGroup" Codebehind="FlowGroup.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="FlowGroupForm" runat="server" meta:resourcekey="FlowGroupForm">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 90%; height: 400px; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbFlowGroup" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbFlowGroup" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemNew_TlbFlowGroup_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbFlowGroup"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbFlowGroup" runat="server" ClientSideCommand="tlbItemEdit_TlbFlowGroup_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbFlowGroup"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbFlowGroup" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbFlowGroup_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbFlowGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbFlowGroup" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidthhiftint="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbFlowGroup" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbFlowGroup_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbFlowGroup" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbFlowGroup_onClick();" DropDownImageWidth="16px"
                                        Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbFlowGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbFlowGroup" runat="server" Enabled="false"
                                        ClientSideCommand="tlbItemCancel_TlbFlowGroup_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbFlowGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbFlowGroup" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbFlowGroup_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbFlowGroup" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbFlowGroup" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_TlbFlowGroup" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemExit_TlbFlowGroup_onClick();" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_FlowGroup" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_FlowGroups_FlowGroup" class="HeaderLabel" style="width: 50%">
                                                    FlowGroups
                                                </td>
                                                <td id="loadingPanel_GridFlowGroup_FlowGroup" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridFlowGroup_FlowGroup" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridFlowGroup_FlowGroup" runat="server"
                                                                ClientSideCommand="Refresh_GridFlowGroup_FlowGroup();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridFlowGroup_FlowGroup"
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
                                        <ComponentArt:CallBack ID="CallBack_GridFlowGroup_FlowGroup" runat="server" OnCallback="CallBack_GridFlowGroup_FlowGroup_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridFlowGroup_FlowGroup" runat="server" AllowColumnResizing="false"
                                                    AllowMultipleSelect="false" CssClass="Grid" EnableViewState="false" FillContainer="true"
                                                    FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                    PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client" ScrollBar="On"
                                                    ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16"
                                                    ScrollGripCssClass="ScrollGrip" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16"
                                                    SearchTextCssClass="GridHeaderText" ShowFooter="false" Width="100%">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام رتبه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFlowGroupName_GridFlowGroup"
                                                                    Width="150" TextWrap="true"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                    HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridFlowGroup"
                                                                    Width="200" TextWrap="true"/>
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridFlowGroup_FlowGroup_onItemSelect" />
                                                        <Load EventHandler="GridFlowGroup_FlowGroup_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_FlowGroup" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridFlowGroup_FlowGroup_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridFlowGroup_FlowGroup_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="middle" align="center">
                            <table style="width: 100%;" class="BoxStyle" id="tblFlowGroup_FlowGroup">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_tblFlowGroup_FlowGroup" runat="server" meta:resourcekey="AlignObj" style="color: White;
                                                        width: 100%; height: 100%" class="BoxContainerHeader">
                                                        FlowGroup Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFlowGroupName_FlowGroup" runat="server" meta:resourcekey="lblFlowGroupName_FlowGroup"
                                                        Text=": نام رتبه" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtFlowGroupName_FlowGroup"
                                                        disabled="disabled"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescription_FlowGroup" runat="server" meta:resourcekey="lblDescription_FlowGroup"
                                                        Text=": توضیحات" CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea type="text" runat="server" style="width: 98%; height: 35px" rows="7" cols="20"
                                                        class="TextBoxes" id="txtDescription_FlowGroup" disabled="disabled"
                                                        ></textarea>
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
    <asp:HiddenField runat="server" ID="hfheader_FlowGroups_FlowGroup" meta:resourcekey="hfheader_FlowGroups_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfheader_tblFlowGroup_FlowGroup" meta:resourcekey="hfheader_tblFlowGroup_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfView_FlowGroup" meta:resourcekey="hfView_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfAdd_FlowGroup" meta:resourcekey="hfAdd_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfEdit_FlowGroup" meta:resourcekey="hfEdit_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfDelete_FlowGroup" meta:resourcekey="hfDelete_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_FlowGroup" meta:resourcekey="hfDeleteMessage_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_FlowGroup" meta:resourcekey="hfCloseMessage_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridFlowGroup_FlowGroup" meta:resourcekey="hfloadingPanel_GridFlowGroup_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfErrorType_FlowGroup" meta:resourcekey="hfErrorType_FlowGroup" />
    <asp:HiddenField runat="server" ID="hfConnectionError_FlowGroup" meta:resourcekey="hfConnectionError_FlowGroup" />
    </form>
</body>
</html>
