<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesignedReports" Codebehind="DesignedReports.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link  href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link  href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link  href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link  href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link  href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link  href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link  href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link  href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link  href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body>
     <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="DesignedReportsForm" runat="server" meta:resourcekey="DesignedReportsForm">
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
                            <ComponentArt:ToolBar ID="TlbDesignedReports" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbDesignedReports" runat="server" ClientSideCommand="tlbItemNew_TlbDesignedReports_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbDesignedReports"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbDesignedReports" runat="server" ClientSideCommand="tlbItemEdit_TlbDesignedReports_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbDesignedReports"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbDesignedReports" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbDesignedReports_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbDesignedReports" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbDesignedReports" runat="server" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbDesignedReports" TextImageSpacing="5"
                                        ClientSideCommand="tlbItemHelp_TlbDesignedReports_onClick();" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDesignedReports" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbDesignedReports_onClick();" DropDownImageWidth="16px"
                                        Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbDesignedReports" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDesignedReports" runat="server" ClientSideCommand="tlbItemCancel_TlbDesignedReports_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDesignedReports"
                                        Enabled="false" TextImageSpacing="5" />
                                    
                                    <ComponentArt:ToolBarItem ID="tlbItemSelectColumn_TlbDesignedReports" runat="server"
                                        ClientSideCommand="tlbItemSelectColumn_TlbDesignedReports_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="verde.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSelectColumn_TlbDesignedReports" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbDesignedReports" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbDesignedReports_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbDesignedReports" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDesignedReports" runat="server" ClientSideCommand="tlbItemExit_TlbDesignedReports_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbDesignedReports"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_DesignedReports" class="ToolbarMode">
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
            <td height="55%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 65%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_DesignedReports_DesignedReports" class="HeaderLabel" style="width: 50%">
                                                    Reports
                                                </td>
                                                <td id="loadingPanel_GridDesignedReports_DesignedReports" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridDesignedReports_DesignedReports" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridDesignedReports_DesignedReports" runat="server"
                                                                ClientSideCommand="Refresh_GridDesignedReports_DesignedReports();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridDesignedReports_DesignedReports"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridDesignedReports_DesignedReports" OnCallback="CallBack_GridDesignedReports_DesignedReports_onCallback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridDesignedReports_DesignedReports" runat="server" CssClass="Grid"
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
                                                                <ComponentArt:GridColumn Align="Center" DataField="DesignedType.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نوع گزارش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnReportTypeName_DesignedReports" TextWrap="true"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="عنوان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnReportTitle_DesignedReports" TextWrap="true"/>
                                                                
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                    HeadingText="توضیح" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnReportComment_DesignedReports" TextWrap="true"/>
                                                                
                                                                <ComponentArt:GridColumn DataField="DesignedType.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                               
                                                                <ComponentArt:GridColumn DataField="ParentReport.Name" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="ParentReport.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                 <ComponentArt:GridColumn DataField="DesignedType.CustomCode" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="ReportParameterDesigned.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn DataField="ReportParameterDesigned.Title" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="ParentPath" Visible="false" />
                                                                
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridDesignedReports_DesignedReports_onLoad" />
                                                        <ItemSelect EventHandler="GridDesignedReports_DesignedReports_onItemSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_DesignedReports" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridDesignedReports_DesignedReports_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridDesignedReports_DesignedReports_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 35%">
                            <table style="width: 90%;" class="BoxStyle" id="tblDesignedReports_DesignedReports">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle">
                                        <div id="header_DesignedReportsDetails_DesignedReports" runat="server" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                            Reports Details</div>
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblReportType_DesignedReports" runat="server" meta:resourcekey="lblReportType_DesignedReports"
                                            Text=": نوع گزارش" CssClass="WhiteLabel"></asp:Label></td>
                                </tr>
                                <tr id="Tr3" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbReportTypes_DesignedReports" OnCallback="CallBack_cmbReportTypes_DesignedReports_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbReportTypes_DesignedReports" runat="server" AutoComplete="true"
                                                    DataTextField="Name"   DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbReportTypes_DesignedReports_onExpand" />
                                                        <Collapse EventHandler="cmbReportTypes_DesignedReports_onCollapse" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ReportTypes" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbReportTypes_DesignedReports_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbReportTypes_DesignedReports_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbReportTypes_DesignedReports_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblReportName_DesignedReports" runat="server" meta:resourcekey="lblReportName_DesignedReports"
                                            Text=": عنوان گزارش" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr5" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtReportTitle_DesignedReports"
                                            disabled="disabled"  />
                                    </td>
                                </tr>
                                <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblReportComment_DesignedReports" runat="server" meta:resourcekey="lblReportComment_DesignedReports"
                                            Text=": توضیح" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr7" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <textarea  cols="20" rows="4" runat="server" style="width: 99%; height: 40px;"  class="TextBoxes" id="txtReportComment_DesignedReports"
                                            disabled="disabled" ></textarea>
                                        <%--<input type="text" runat="server" style="width: 99%;"  class="TextBoxes" id="txtReportComment_DesignedReports"
                                            disabled="disabled"  />--%>
                                    </td>
                                </tr>
                                <tr id="Tr8" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblGroupNodes_DesignedReports" runat="server" meta:resourcekey="lblGroupNodes_DesignedReports"
                                            Text=": نمایش در" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr9" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBcak_cmbGroupNodes_DesignedReports" OnCallback="CallBcak_cmbGroupNodes_DesignedReports_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbGroupNodes_DesignedReports" runat="server" AutoComplete="true"
                                                    DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true" ExpandDirection="Up">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbGroupNodes_DesignedReports_onExpand" />
                                                        <Collapse EventHandler="cmbGroupNodes_DesignedReports_onCollapse" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_GroupNodes" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBcak_cmbGroupNodes_DesignedReports_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBcak_cmbGroupNodes_DesignedReports_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBcak_cmbGroupNodes_DesignedReports_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                 <tr id="Tr10" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <asp:Label ID="lblDateParameterType_DesignedReports" runat="server" meta:resourcekey="lblDateParameterType_DesignedReports"
                                            Text=": نوع متغیر تاریخ" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr11" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBcak_cmbDateParameterType_DesignedReports" OnCallback="CallBcak_cmbDateParameterType_DesignedReports_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbDateParameterType_DesignedReports" runat="server" AutoComplete="true"
                                                    DataTextField="Title" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true" ExpandDirection="Up">
                                                      <ClientEvents>
                                                        <Expand EventHandler="cmbDateParameterType_DesignedReports_onExpand" />
                                                        <Collapse EventHandler="cmbDateParameterType_DesignedReports_onCollapse" />
                                                    </ClientEvents>
                                                    
                                                </ComponentArt:ComboBox>
                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_DateParameterType" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBcak_cmbDateParameterType_DesignedReports_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBcak_cmbDateParameterType_DesignedReports_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBcak_cmbDateParameterType_DesignedReports_onCallbackError" />
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
        <asp:HiddenField runat="server" ID="hfheader_DesignedReportsDetails_DesignedReports" meta:resourcekey="hfheader_DesignedReportsDetails_DesignedReports" />
    <asp:HiddenField runat="server" ID="hfheader_DesignedReports_DesignedReports" meta:resourcekey="hfheader_DesignedReports_DesignedReports" />
        <asp:HiddenField runat="server" ID="hfView_DesignedReports" meta:resourcekey="hfView_DesignedReports" />
    <asp:HiddenField runat="server" ID="hfAdd_DesignedReports" meta:resourcekey="hfAdd_DesignedReports" />
    <asp:HiddenField runat="server" ID="hfEdit_DesignedReports" meta:resourcekey="hfEdit_DesignedReports" />
    <asp:HiddenField runat="server" ID="hfDelete_DesignedReports" meta:resourcekey="hfDelete_DesignedReports" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridDesignedReports_DesignedReports" meta:resourcekey="hfloadingPanel_GridDesignedReports_DesignedReports" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_DesignedReports" meta:resourcekey="hfcmbAlarm_DesignedReports" />
        <asp:HiddenField runat="server" ID="hfErrorType_DesignedReports" meta:resourcekey="hfErrorType_DesignedReports" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_DesignedReports" meta:resourcekey="hfDeleteMessage_DesignedReports" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_DesignedReports" meta:resourcekey="hfCloseMessage_DesignedReports" />
    </form>
</body>
</html>
