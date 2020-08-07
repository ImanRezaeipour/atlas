<%@ Page Language="C#" AutoEventWireup="true" Inherits="CostCenter" CodeBehind="CostCenter.aspx.cs" %>


<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link id="Link1" href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link2" href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link3" href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link4" href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link5" href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link6" href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link7" href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link8" href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link9" href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link10" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link11" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link12" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="CostCenterForm" runat="server" meta:resourcekey="CostCenterForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; height: 400px; font-family: Arial; font-size: small">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbCostCenter" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbCostCenter" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemNew_TlbCostCenter_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbCostCenter"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbCostCenter" runat="server" ClientSideCommand="tlbItemEdit_TlbCostCenter_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbCostCenter"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbCostCenter" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemDelete_TlbCostCenter_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                            meta:resourcekey="tlbItemDelete_TlbCostCenter" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbCostCenter" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidthhiftint="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbCostCenter" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbCostCenter_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbCostCenter" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemSave_TlbCostCenter_onClick();" DropDownImageWidth="16px"
                                            Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbCostCenter" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCostCenter" runat="server" Enabled="false"
                                            ClientSideCommand="tlbItemCancel_TlbCostCenter_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCostCenter" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbCostCenter" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbCostCenter_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbCostCenter" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbCostCenter" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbCostCenter" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemExit_TlbCostCenter_onClick();" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_CostCenter" class="ToolbarMode"></td>
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
                                                    <td id="header_CostCenters_CostCenter" class="HeaderLabel" style="width: 50%">CostCenters
                                                    </td>
                                                    <td id="loadingPanel_GridCostCenter_CostCenter" class="HeaderLabel" style="width: 45%"></td>
                                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_GridCostCenter_CostCenter" runat="server" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridCostCenter_CostCenter" runat="server"
                                                                    ClientSideCommand="Refresh_GridCostCenter_CostCenter();" DropDownImageHeight="16px"
                                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridCostCenter_CostCenter"
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
                                            <ComponentArt:CallBack ID="CallBack_GridCostCenter_CostCenter" runat="server" OnCallback="CallBack_GridCostCenter_CostCenter_onCallBack">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridCostCenter_CostCenter" runat="server" AllowColumnResizing="false"
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
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                        HeadingText="نام مرکز هزینه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnCostCenterName_GridCostCenter"
                                                                        Width="300" TextWrap="true" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Code" DefaultSortDirection="Descending"
                                                                        HeadingText="کد مرکز هزینه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnCostCenterCode_GridCostCenter"
                                                                        Width="50" TextWrap="true" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                        HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridCostCenter"
                                                                        Width="150" TextWrap="true" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientEvents>
                                                            <ItemSelect EventHandler="GridCostCenter_CostCenter_onItemSelect" />
                                                            <Load EventHandler="GridCostCenter_CostCenter_onLoad" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField ID="ErrorHiddenField_CostCenter" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridCostCenter_CostCenter_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridCostCenter_CostCenter_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="middle" align="center">
                                <table style="width: 100%;" class="BoxStyle" id="tblCostCenter_CostCenter">
                                    <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="DetailsBoxHeaderStyle">
                                                        <div id="header_tblCostCenter_CostCenter" runat="server" meta:resourcekey="AlignObj" style="color: White; width: 100%; height: 100%"
                                                            class="BoxContainerHeader">
                                                            CostCenter Details
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCostCenterName_CostCenter" runat="server" meta:resourcekey="lblCostCenterName_CostCenter"
                                                            Text=": نام مرکز هزینه" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtCostCenterName_CostCenter"
                                                            disabled="disabled" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCostCenterCode_CostCenter" runat="server" meta:resourcekey="lblCostCenterCode_CostCenter"
                                                            Text=": کد مرکز هزینه" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtCostCenterCode_CostCenter"
                                                            disabled="disabled" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDescription_CostCenter" runat="server" meta:resourcekey="lblDescription_CostCenter"
                                                            Text=": توضیحات" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <textarea type="text" runat="server" style="width: 98%; height: 35px" rows="7" cols="20"
                                                            class="TextBoxes" id="txtDescription_CostCenter" disabled="disabled"></textarea>
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
        <asp:HiddenField runat="server" ID="hfheader_CostCenters_CostCenter" meta:resourcekey="hfheader_CostCenters_CostCenter" />
        <asp:HiddenField runat="server" ID="hfheader_tblCostCenter_CostCenter" meta:resourcekey="hfheader_tblCostCenter_CostCenter" />
        <asp:HiddenField runat="server" ID="hfView_CostCenter" meta:resourcekey="hfView_CostCenter" />
        <asp:HiddenField runat="server" ID="hfAdd_CostCenter" meta:resourcekey="hfAdd_CostCenter" />
        <asp:HiddenField runat="server" ID="hfEdit_CostCenter" meta:resourcekey="hfEdit_CostCenter" />
        <asp:HiddenField runat="server" ID="hfDelete_CostCenter" meta:resourcekey="hfDelete_CostCenter" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_CostCenter" meta:resourcekey="hfDeleteMessage_CostCenter" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_CostCenter" meta:resourcekey="hfCloseMessage_CostCenter" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridCostCenter_CostCenter" meta:resourcekey="hfloadingPanel_GridCostCenter_CostCenter" />
        <asp:HiddenField runat="server" ID="hfErrorType_CostCenter" meta:resourcekey="hfErrorType_CostCenter" />
        <asp:HiddenField runat="server" ID="hfConnectionError_CostCenter" meta:resourcekey="hfConnectionError_CostCenter" />
    </form>
</body>
</html>
