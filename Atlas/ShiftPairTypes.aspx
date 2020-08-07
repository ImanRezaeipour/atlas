<%@ Page Language="C#" AutoEventWireup="true" Inherits="ShiftPairTypes" Codebehind="ShiftPairTypes.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ShiftPairTypesForm" runat="server" meta:resourcekey="ShiftPairTypesForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <table style="width: 90%; height: 400px; font-family: Arial; font-size: small">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbShiftPairTypes" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemNew_TlbShiftPairTypes" runat="server" DropDownImageHeight="16px"
                                                ClientSideCommand="tlbItemNew_TlbShiftPairTypes_onClick();" DropDownImageWidth="16px"
                                                ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbShiftPairTypes"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbShiftPairTypes" runat="server" ClientSideCommand="tlbItemEdit_TlbShiftPairTypes_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbShiftPairTypes"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbShiftPairTypes" runat="server" DropDownImageHeight="16px"
                                                ClientSideCommand="tlbItemDelete_TlbShiftPairTypes_onClick();" DropDownImageWidth="16px"
                                                ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                                meta:resourcekey="tlbItemDelete_TlbShiftPairTypes" TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbShiftPairTypes" runat="server" DropDownImageHeight="16px"
                                                DropDownImageWidthhiftint="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemHelp_TlbShiftPairTypes" TextImageSpacing="5"
                                                ClientSideCommand="tlbItemHelp_TlbShiftPairTypes_onClick();" />
                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbShiftPairTypes" runat="server" DropDownImageHeight="16px"
                                                ClientSideCommand="tlbItemSave_TlbShiftPairTypes_onClick();" DropDownImageWidth="16px"
                                                Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemSave_TlbShiftPairTypes" TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbShiftPairTypes" runat="server" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                Enabled="false" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbShiftPairTypes"
                                                TextImageSpacing="5" ClientSideCommand="tlbItemCancel_TlbShiftPairTypes_onClick();" />
                                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbShiftPairTypes" runat="server"
                                                ClientSideCommand="tlbItemFormReconstruction_TlbShiftPairTypes_onClick();" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbShiftPairTypes"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbShiftPairTypes" runat="server" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemExit_TlbShiftPairTypes" TextImageSpacing="5"
                                                ClientSideCommand="tlbItemExit_TlbShiftPairTypes_onClick();" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td id="ActionMode_ShiftPairTypes" class="ToolbarMode"></td>
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
                                            <td style="color: White; width: 100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td id="header_ShiftPairTypes_ShiftPairTypes" class="HeaderLabel" style="width: 50%">انواع بازه شیفت
                                                        </td>
                                                        <td id="loadingPanel_GridShiftPairTypes_ShiftPairTypes" class="HeaderLabel" style="width: 45%"></td>
                                                        <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                            <ComponentArt:ToolBar ID="TlbRefresh_GridShiftPairTypes_ShiftPairTypes" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridShiftPairTypes_ShiftPairTypes"
                                                                        runat="server" ClientSideCommand="Refresh_GridShiftPairTypes_ShiftPairTypes();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridShiftPairTypes_ShiftPairTypes"
                                                                        TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <ComponentArt:CallBack runat="server" ID="CallBack_GridShiftPairTypes_ShiftPairTypes"
                                                    OnCallback="CallBack_GridShiftPairTypes_ShiftPairTypes_onCallback">
                                                    <Content>
                                                        <ComponentArt:DataGrid ID="GridShiftPairTypes_ShiftPairTypes" runat="server" CssClass="Grid"
                                                            EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                            PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
                                                            SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                                            ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                            <Levels>
                                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                    SortImageWidth="9">
                                                                    <Columns>
                                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                        <ComponentArt:GridColumn Align="Center" DataField="Active" HeadingText="فعال"  HeadingTextCssClass="HeadingText" ColumnType="CheckBox" meta:resourcekey="clmnActive_GridShiftPairTypes"/>
                                                                        <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                            HeadingText="کد نوع بازه شیفت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftPairTypeCode_GridShiftPairTypes" TextWrap="true" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                                            HeadingText="عنوان نوع بازه شیفت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShiftPairTypeTitle_GridShiftPairTypes" TextWrap="true" />
                                                                        <ComponentArt:GridColumn DataField="Description" Visible="false" />
                                                                    </Columns>
                                                                </ComponentArt:GridLevel>
                                                            </Levels>
                                                            <ClientEvents>
                                                                <Load EventHandler="GridShiftPairTypes_ShiftPairTypes_onLoad" />
                                                                <ItemSelect EventHandler="GridShiftPairTypes_ShiftPairTypes_onItemSelect" />
                                                            </ClientEvents>
                                                        </ComponentArt:DataGrid>
                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_ShiftPairTypes" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <CallbackComplete EventHandler="CallBack_GridShiftPairTypes_ShiftPairTypes_onCallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_GridShiftPairTypes_ShiftPairTypes_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="middle">
                                    <table style="width: 100%;" class="BoxStyle" id="tblShiftPairTypes_ShiftPairTypes">
                                        <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                            <td id="Td2" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="DetailsBoxHeaderStyle">
                                                            <div id="header_ShiftPairTypeDetails_ShiftPairTypes" runat="server" meta:resourcekey="AlignObj"
                                                                style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                                                جزئیات نوع بازه شیفت
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 5%">
                                                                        <input id="chbActive_ShiftPairTypes" type="checkbox" checked="checked" disabled="disabled" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblActive_ShiftPairTypes" runat="server" Text="فعال" CssClass="WhiteLabel" meta:resourcekey="lblActive_ShiftPairTypes"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblShiftPairTypeCode_ShiftPairTypes" runat="server" meta:resourcekey="lblShiftPairTypeCode_ShiftPairTypes"
                                                                Text=": کد نوع بازه شیفت" CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtShiftPairTypeCode_ShiftPairTypes"
                                                                disabled="disabled"   />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblShiftPairTypeTitle_ShiftPairTypes" runat="server" meta:resourcekey="lblShiftPairTypeTitle_ShiftPairTypes"
                                                                Text=": عنوان نوع بازه شیفت" CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtShiftPairTypeTitle_ShiftPairTypes"
                                                                disabled="disabled"   />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblShiftPairTypeDescriptions_ShiftPairTypes" runat="server" meta:resourcekey="lblShiftPairTypeDescriptions_ShiftPairTypes"
                                                                Text=": توضیحات نوع بازه شیفت" CssClass="WhiteLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <textarea id="txtShiftPairTypeDescriptions_ShiftPairTypes" cols="20" name="S1" rows="2" disabled="disabled" class="TextBoxes"   style="width: 98%; height: 30px;"></textarea></td>
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
        </div>
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
        <asp:HiddenField runat="server" ID="hfheader_ShiftPairTypeDetails_ShiftPairTypes"
            meta:resourcekey="hfheader_ShiftPairTypeDetails_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfheader_ShiftPairTypes_ShiftPairTypes" meta:resourcekey="hfheader_ShiftPairTypes_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfView_ShiftPairTypes" meta:resourcekey="hfView_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfAdd_ShiftPairTypes" meta:resourcekey="hfAdd_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfEdit_ShiftPairTypes" meta:resourcekey="hfEdit_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfDelete_ShiftPairTypes" meta:resourcekey="hfDelete_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_ShiftPairTypes" meta:resourcekey="hfDeleteMessage_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_ShiftPairTypes" meta:resourcekey="hfCloseMessage_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridShiftPairTypes_ShiftPairTypes"
            meta:resourcekey="hfloadingPanel_GridShiftPairTypes_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfErrorType_ShiftPairTypes" meta:resourcekey="hfErrorType_ShiftPairTypes" />
        <asp:HiddenField runat="server" ID="hfConnectionError_ShiftPairTypes" meta:resourcekey="hfConnectionError_ShiftPairTypes" />
    </form>
</body>
</html>
