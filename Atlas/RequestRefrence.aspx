<%@ Page Language="C#" AutoEventWireup="true" Inherits="RequestRefrence" Codebehind="RequestRefrence.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup" TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="~/css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RequestRefrenceForm" runat="server" meta:resourcekey="RequestRefrenceForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td>
                    <ComponentArt:ToolBar ID="TlbRequest_RequestRefrence" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRequest_RequestRefrence" runat="server" ClientSideCommand="tlbItemSave_TlbRequest_RequestRefrence_onClick();"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" Visible="false"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRequest_RequestRefrence"
                                TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRequest_RequestRefrence" runat="server"
                                ClientSideCommand="tlbItemFormReconstruction_TlbRequest_RequestRefrence_onClick();" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" Visible="false"
                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRequest_RequestRefrence" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRequest_RequestRefrence" runat="server" ClientSideCommand="tlbItemExit_TlbRequest_RequestRefrence_onClick();"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRequest_RequestRefrence"
                                TextImageSpacing="5" />
                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td id="header_RequestRefrence_RequestRefrence" class="HeaderLabel" style="width: 65%">Request Refrence
                            </td>
                            <td id="loadingPanel_GridRequestRefrence_RequestRefrence" class="HeaderLabel"
                                style="width: 30%"></td>
                            <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                                <ComponentArt:ToolBar ID="TlbRefresh_GridRequestRefrence_RequestRefrence"
                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRequestRefrence_RequestRefrence"
                                            runat="server" ClientSideCommand="Refresh_GridRequestRefrence_RequestRefrence();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRequestRefrence_RequestRefrence"
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
                    <table id="Container_GridRequestRefrence_RequestRefrence" style="width: 100%">
                        <tr>
                            <td>
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRequestRefrence_RequestRefrence"
                                    OnCallback="CallBack_GridRequestRefrence_RequestRefrence_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRequestRefrence_RequestRefrence" runat="server"
                                            AllowHorizontalScrolling="false" CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                            FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PageSize="1" RunningMode="Client" AllowMultipleSelect="false"
                                            AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Applicant" DefaultSortDirection="Descending"
                                                            HeadingText="درخواست کننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnApplicant_GridRequestRefrence_RequestRefrence"
                                                            Width="150" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RequestTitle" DefaultSortDirection="Descending"
                                                            HeadingText="نوع درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestTopic_GridRequestRefrence_RequestRefrence"
                                                            Width="150" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                            HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridRequestRefrence_RequestRefrence"
                                                            Width="130" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                            HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridRequestRefrence_RequestRefrence"
                                                            Width="130" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromTime_GridRequestRefrence_RequestRefrence"
                                                            Width="80" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToTime_GridRequestRefrence_RequestRefrence"
                                                            Width="80" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheDuration" DefaultSortDirection="Descending"
                                                            HeadingText="مقدار" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDuration_GridRequestRefrence_RequestRefrence"
                                                            Width="80" TextWrap="true" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestStatus_GridEndorsementFlowState_EndorsementFlowState">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridEndorsementFlowState_EndorsementFlowState(DataItem.GetMember('RequestStatus').Value)##">
                                                                <img src="##SetClmnImage_GridEndorsementFlowState_EndorsementFlowState(DataItem.GetMember('RequestStatus').Value)##"
                                                                    alt="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridRequestRefrence_RequestRefrence_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Refrence_RequestRefrence" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRequestRefrence_RequestRefrence_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRequestRefrence_RequestRefrence_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
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
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="320px">
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
            <ClientEvents>
                <OnShow EventHandler="DialogConfirm_OnShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfTitle_DialogRequestRefrence" meta:resourcekey="hfTitle_DialogRequestRefrence" />
        <asp:HiddenField runat="server" ID="hfheader_RequestRefrence_RequestRefrence" meta:resourcekey="hfheader_RequestRefrence_RequestRefrence" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRequestRefrence_RequestRefrence" meta:resourcekey="hfloadingPanel_GridRequestRefrence_RequestRefrence" />
        <asp:HiddenField runat="server" ID="hfRequestRefrence_RequestRefrence" />
        <asp:HiddenField runat="server" ID="hfErrorType_RequestRefrence" meta:resourcekey="hfErrorType_RequestRefrence" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RequestRefrence" meta:resourcekey="hfConnectionError_RequestRefrence" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_RequestRefrence" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RequestRefrence" meta:resourcekey="hfCloseMessage_RequestRefrence" />
    </form>
</body>
</html>
