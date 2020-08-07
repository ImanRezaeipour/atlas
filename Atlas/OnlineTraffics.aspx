<%@ Page Language="C#" AutoEventWireup="true" Inherits="OnlineTraffics" Codebehind="OnlineTraffics.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/GridOnlineTraffics.css" runat="server" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/SignalR/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="JS/SignalR/jquery.signalR-2.2.0.min.js"></script>
    <script type="text/javascript" src="signalr/hubs"></script>
    <form id="OnlineTrafficsForm" runat="server" meta:resourcekey="OnlineTrafficsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <table class="BoxStyle" style="width: 98%;">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbOnlineTraffics" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbOnlineTraffics" runat="server"
                                                ClientSideCommand="tlbItemFormReconstruction_TlbOnlineTraffics_onClick();" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbOnlineTraffics" TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbOnlineTraffics" runat="server" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemHelp_TlbOnlineTraffics" TextImageSpacing="5"
                                                ClientSideCommand="tlbItemHelp_TlbOnlineTraffics_onClick();" />
                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbOnlineTraffics" runat="server" DropDownImageHeight="16px"
                                                ClientSideCommand="tlbItemExit_TlbOnlineTraffics_onClick();" DropDownImageWidth="16px"
                                                ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbOnlineTraffics"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="BoxStyle">
                    <td height="50%">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table class="HeaderLabel">
                                        <tr>
                                            <td id="header_GridOnlineTraffics_OnlineTraffics" class="HeaderLabel" style="width: 50%">OnlineTraffics
                                            </td>
                                            <td id="loadingPanel_GridOnlineTraffics_OnlineTraffics" style="width: 48%"></td>
                                            <td style="width: 2%">
                                                <ComponentArt:ToolBar ID="TlbOnlineTraffics_GridTlbOnlineTraffics" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="ToolBarItem1" runat="server"
                                                            ClientSideCommand="tlbItemFormReconstruction_TlbOnlineTraffics_GridTlbOnlineTraffics_onClick();" DropDownImageHeight="16px"
                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbOnlineTraffics_GridTlbOnlineTraffics" TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                            <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="Container_GridOnlineTraffics_OnlineTraffics" style="width: 100%">
                                        <ComponentArt:DataGrid ID="GridOnlineTraffics_OnlineTraffics" runat="server"
                                            CssClass="Grid" EnableViewState="false" ShowFooter="false" FillContainer="true"
                                            FooterCssClass="GridFooter" Height="100%" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                            PageSize="10" RunningMode="Client" Width="744px" AllowMultipleSelect="false"
                                            AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
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
                                                        <ComponentArt:GridColumn Align="Center" DataField="PersonImage" DefaultSortDirection="Descending"
                                                            HeadingText="عکس پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonImage_GridOnlineTraffics_OnlineTraffics"
                                                            />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PersonName" DefaultSortDirection="Descending"
                                                            HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonName_GridOnlineTraffics_OnlineTraffics" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDate_GridOnlineTraffics_OnlineTraffics"
                                                            TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheTime" DefaultSortDirection="Descending"
                                                            HeadingText="زمان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnTheTime_GridOnlineTraffics_OnlineTraffics"
                                                            TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="MachinName" DefaultSortDirection="Descending"
                                                            HeadingText="دستگاه" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMachinName_GridOnlineTraffics_OnlineTraffics"
                                                            TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TrafficType" DefaultSortDirection="Descending"
                                                            HeadingText="نوع تردد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnTrafficType_GridOnlineTraffics_OnlineTraffics" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PersonBarcode" DefaultSortDirection="Descending"
                                                            HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText"
                                                            meta:resourcekey="clmnPersonBarcode_GridOnlineTraffics_OnlineTraffics" Width="100" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PrecardName" DefaultSortDirection="Descending"
                                                            HeadingText="پیشکارت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPrecardName_GridOnlineTraffics_OnlineTraffics"
                                                            TextWrap="true" />
                                                        <ComponentArt:GridColumn DataField="PersonID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnPersonImage_GridOnlineTraffics_OnlineTraffics">
                                                    <table style="width: 100%; height: 200px;">
                                                        <tr>
                                                            <td align="center">
                                                                <iframe style="height: 200px;" src="about:blank"></iframe>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <ItemSelect EventHandler="GridOnlineTraffics_OnlineTraffics_onItemSelect" />
                                                <Load EventHandler="GridOnlineTraffics_OnlineTraffics_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                    </div>
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
        <asp:HiddenField runat="server" ID="hfErrorType_OnlineTraffics" meta:resourcekey="hfErrorType_OnlineTraffics" />
        <asp:HiddenField runat="server" ID="hfConnectionError_OnlineTraffics" meta:resourcekey="hfConnectionError_OnlineTraffics" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_DialogOnlineTraffics" meta:resourcekey="hfCloseMessage_DialogOnlineTraffics" />
        <asp:HiddenField runat="server" ID="hfheader_GridOnlineTraffics_OnlineTraffics" meta:resourcekey="hfheader_GridOnlineTraffics_OnlineTraffics" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogOnlineTraffics" meta:resourcekey="hfTitle_DialogOnlineTraffics" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridOnlineTraffics_OnlineTraffics" meta:resourcekey="hfloadingPanel_GridOnlineTraffics_OnlineTraffics" />
    </form>
</body>
</html>
