<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonthlyExceptionShifts" Codebehind="MonthlyExceptionShifts.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>



</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MonthlyExceptionShiftsForm" runat="server" meta:resourcekey="MonthlyExceptionShiftsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_MonthlyExceptionShifts" style="width: 99%; height: 100%; font-family: Arial; font-size: small;"
            class="BoxStyle">
            <tr>
                <td>
                    <ComponentArt:ToolBar ID="TlbMonthlyExceptionShifts" runat="server" CssClass="toolbar"
                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbMonthlyExceptionShifts" runat="server" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemHelp_TlbMonthlyExceptionShifts" TextImageSpacing="5"
                                ClientSideCommand="tlbItemHelp_TlbMonthlyExceptionShifts_onClick();" />
                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbMonthlyExceptionShifts" runat="server"
                                ClientSideCommand="tlbItemFormReconstruction_TlbMonthlyExceptionShifts_onClick();" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbMonthlyExceptionShifts" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemShiftsView_TlbMonthlyExceptionShifts" runat="server" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.gif" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemShiftsView_TlbMonthlyExceptionShifts" TextImageSpacing="5"
                                ClientSideCommand="tlbItemShiftsView_TlbMonthlyExceptionShifts_onClick();" />
                            <ComponentArt:ToolBarItem ID="tlbItemPeriodRepeat_TlbMonthlyExceptionShifts" runat="server"
                                ClientSideCommand="tlbItemPeriodRepeat_TlbMonthlyExceptionShifts_onClick();" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cycle_Silver.png" ImageWidth="16px"
                                ItemType="Command" meta:resourcekey="tlbItemPeriodRepeat_TlbMonthlyExceptionShifts" TextImageSpacing="5" Enabled ="false" />
                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMonthlyExceptionShifts" runat="server" DropDownImageHeight="16px"
                                ClientSideCommand="tlbItemExit_TlbMonthlyExceptionShifts_onClick();" DropDownImageWidth="16px"
                                ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMonthlyExceptionShifts"
                                TextImageSpacing="5" />

                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 80%">

                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblYear_MonthlyExceptionShifts" runat="server" Text=": سال" CssClass="WhiteLabel"
                                                meta:resourcekey="lblYear_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMonth_MonthlyExceptionShifts" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                                meta:resourcekey="lblMonth_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPersonNameEditing_MonthlyExceptionShifts" runat="server" CssClass="WhiteLabel" Text="نام و نام خانوادگی :" meta:resourcekey="lblPersonNameEditing_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPersonBarcodeEditing_MonthlyExceptionShifts" runat="server" CssClass="WhiteLabel" Text="شماره پرسنلی :" meta:resourcekey="lblPersonBarcodeEditing_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSearchExpression_MonthlyExceptionShifts" runat="server" CssClass="WhiteLabel" Text="عبارت جستجو :" meta:resourcekey="lblSearchExpression_MonthlyExceptionShifts" onkeypress="txtSerchExpression_MonthlyExceptionShifts_onKeyPess(event);"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ComponentArt:ComboBox ID="cmbYear_MonthlyExceptionShifts" runat="server" AutoComplete="true"
                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100">
                                                <ClientEvents>
                                                    <Change EventHandler="cmbYear_MonthlyExceptionShifts_onChange" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                        </td>
                                        <td>
                                            <ComponentArt:ComboBox ID="cmbMonth_MonthlyExceptionShifts" runat="server" AutoComplete="true"
                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100" DropDownHeight="280">
                                                <ClientEvents>
                                                    <Change EventHandler="cmbMonth_MonthlyExceptionShifts_onChange" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                        </td>
                                        <td>
                                            <ComponentArt:ToolBar ID="TlbView_MonthlyExceptionShifts" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_MonthlyExceptionShifts" runat="server"
                                                        ClientSideCommand="tlbItemView_TlbView_MonthlyExceptionShifts_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemView_TlbView_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" Enabled="true" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>

                                        <td>
                                            <input id="txtPersonNameEditing_MasterLeaveRemains" class="TextBoxes" style="width: 95%; text-align: center"
                                                type="text" readonly="readonly" />
                                        </td>
                                        <td>
                                            <input id="txtPersonBarcodeEditing_MasterLeaveRemains" class="TextBoxes" style="width: 95%; text-align: center"
                                                type="text" readonly="readonly" />
                                        </td>
                                        <td>
                                            <input id="txtSerach_MonthlyExceptionShifts" class="TextBoxes" style="width: 95%;" type="text" />
                                        </td>
                                        <td>
                                            <ComponentArt:ToolBar ID="TlbSearch_MonthlyExceptionShifts" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItem_TlbSearch_MonthlyExceptionShifts" runat="server"
                                                        ClientSideCommand="tlbItem_TlbSearch_MonthlyExceptionShifts_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItem_TlbSearch_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" Enabled="true" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--   start check--%>

                            <td style="width: 20%">
                                <table>
                                    <tr>
                                        <td style="width: 2%">
                                            <input id="chbKeyboardSetting_MonthlyExceptionShifts" type="checkbox" onchange="chbKeyboardSetting_MonthlyExceptionShifts_OnChange();" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblKeyboardSetting_MonthlyExceptionShifts" runat="server" Text="تنظیم صفحه کلید بر اساس زبان انگلیسی" CssClass="WhiteLabel"
                                                meta:resourcekey="lblKeyboardSetting_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%">
                                            <input id="chbShortcuts_MonthlyExceptionShifts" type="checkbox" onchange="chbShortcuts_MonthlyExceptionShifts_OnChange();" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShortcuts_MonthlyExceptionShifts" runat="server" Text="جستجو بر اساس کلید میانبر" CssClass="WhiteLabel"
                                                meta:resourcekey="lblShortcutKey_MonthlyExceptionShifts"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%-- end check--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <table style="width: 100%; height: 300px; border: outset 1px black;" class="BoxStyle">
                        <tr>
                            <td style="height: 5%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td id="header_MonthlyExceptionShifts_MonthlyExceptionShifts" class="HeaderLabel" style="width: 50%;">Monthly Exception Shifts
                                        </td>
                                        <td id="loadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts" class="HeaderLabel"
                                            style="width: 45%"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <div id="Container_GridMonthlyExceptionShifts_MonthlyExceptionShifts" style="width: 100%">
                                    <ComponentArt:CallBack runat="server" ID="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                        OnCallback="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallBack">
                                        <Content>
                                            <ComponentArt:DataGrid ID="GridMonthlyExceptionShifts_MonthlyExceptionShifts" runat="server" AllowEditing="true"
                                                AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                                FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" EditOnClickSelectedItem="false"
                                                PagePaddingEnabled="true" PageSize="16" RunningMode="Client" AllowMultipleSelect="false"
                                                AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                                ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="960">
                                                <Levels>
                                                    <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                        DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                        RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                        SortImageWidth="9" EditCommandClientTemplateId="EditCommandTemplate" AllowReordering="false">
                                                        <Columns>
                                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="EditTemplate" EditControlType="EditCommand" Width="50" Align="Center" />
                                                            <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn DataField="PersonID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="PersonName" DefaultSortDirection="Descending" AllowEditing="False"
                                                                HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonName_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                                Width="140" TextWrap="true" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="PersonCode" DefaultSortDirection="Descending" AllowEditing="False"
                                                                HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonCode_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                                Width="110" TextWrap="true" />
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>
                                                <ClientTemplates>
                                                    <ComponentArt:ClientTemplate ID="EditTemplate">
                                                        <a>
                                                            <img src="Images/ToolBar/edit.png" alt="" onclick="javascript:EditGridMonthlyExceptionShifts_MonthlyExceptionShifts('## DataItem.ClientId ##');" title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Edit')##" /></a>&nbsp;<a>
                                                                <img src="Images/ToolBar/remove.png" alt="" onclick="javascript:DeleteGridMonthlyExceptionShifts_MonthlyExceptionShifts('## DataItem.ClientId ##')" title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Delete')##" /></a>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                                                        <a>
                                                            <img src="Images/ToolBar/save.png" alt="" onclick="javascript:UpdateGridMonthlyExceptionShifts_MonthlyExceptionShifts();" title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Save')##" /></a>&nbsp;<a>
                                                                <img src="Images/ToolBar/cancel.png" alt="" onclick="javascript:GridMonthlyExceptionShifts_MonthlyExceptionShifts.EditCancel();document.getElementById('txtPersonNameEditing_MasterLeaveRemains').value = '';document.getElementById('txtPersonBarcodeEditing_MasterLeaveRemains').value = '';  CurrentPageState_MonthlyExceptionShifts = 'View'; " title="##SetCellTitle_GridMonthlyExceptionShifts_MonthlyExceptionShifts('Cancel')##" /></a>
                                                    </ComponentArt:ClientTemplate>

                                                </ClientTemplates>
                                                <ClientEvents>
                                                    <Load EventHandler="GridMonthlyExceptionShifts_MonthlyExceptionShifts_onLoad" />
                                                    <ItemSelect EventHandler="GridMonthlyExceptionShifts_MonthlyExceptionShifts_onItemSelect" />

                                                </ClientEvents>
                                            </ComponentArt:DataGrid>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_MonthlyExceptionShifts" />
                                            <asp:HiddenField runat="server" ID="hfMonthlyExceptionShiftsCount_MonthlyExceptionShifts" />
                                            <asp:HiddenField runat="server" ID="hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 10%;">
                                            <ComponentArt:ToolBar ID="TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                Style="direction: ltr" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridMonthlyExceptionShifts_MonthlyExceptionShifts"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td id="footer_GridMonthlyExceptionShifts_MonthlyExceptionShifts" runat="server" class="WhiteLabel"
                                            meta:resourcekey="InverseAlignObj" style="width: 45%"></td>
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
            runat="server" Width="420px">
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
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogShiftsView"
            runat="server" Width="350px">
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 100%;"
                    meta:resourcekey="tblShiftsView_DialogShiftsView"
                    class="BodyStyle">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbShiftsView" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbShiftsView" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemRefresh_TlbShiftsView_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbShiftsView"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbShiftsView" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbShiftsView_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbShiftsView"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td id="header_ShiftsView_MonthlyExceptionShifts" style="color: White; font-weight: bold; font-family: Arial; width: 100%">Shifts View
                                    </td>
                                </tr>
                                <%-- start--%>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 80%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>&nbsp;
                                                    <asp:Label ID="lblShiftSearch_MonthlyExceptionShifts" runat="server" meta:resourcekey="lblShiftSearch_MonthlyExceptionShifts"
                                                        Text=": جستجوی شیفت" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSearchTerm_MonthlyExceptionShifts"
                                                                    onkeypress="txtSearchTerm_MonthlyExceptionShifts_onKeyPess(event);" />
                                                            </td>
                                                            <td style="width: 5%">
                                                                <ComponentArt:ToolBar ID="TlbShiftSearch" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbShiftSearch" runat="server"
                                                                            ClientSideCommand="tlbItemSearch_TlbShiftSearch_onClick();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemSearch_TlbShiftSearch" TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <%-- end--%>
                                <tr>
                                    <td>
                                        <table style="width: 60%">
                                            <tr>
                                                <%-- <td style="width: 5%">
                                                    <input id="chbSelectAll_GridSettings_Roles" type="checkbox" checked="checked"
                                                        onclick="chbSelectAll_GridSettings_Roles_onClick();" />
                                                </td>--%>
                                                <%--<td>
                                                    <asp:Label runat="server" ID="lblSelectAll_GridSettings_Roles" meta:resourcekey="lblSelectAll_GridSettings_Roles"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_ShiftsView_MonthlyExceptionShifts"
                                            OnCallback="CallBack_ShiftsView_MonthlyExceptionShifts_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridShiftsView_MonthlyExceptionShifts" runat="server" AllowMultipleSelect="false"
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

                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام شیفت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_ShiftsView_MonthlyExceptionShifts" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                    HeadingText="کد شیفت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnCustomCode_ShiftsView_MonthlyExceptionShifts" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ShortcutsKey" DefaultSortDirection="Descending"
                                                                    HeadingText="کلید میانبر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnShortcutsKey_ShiftsView_MonthlyExceptionShifts"
                                                                    AllowEditing="True" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ShiftsView_MonthlyExceptionShifts" />
                                                <%--<asp:HiddenField runat="server" ID="hfExist_GridSettings_Roles" />
                                                 <asp:HiddenField runat="server" ID="hfId_GridSettings_Roles" />
                                                <asp:HiddenField runat="server" ID="hfRuleId_GridSettings_Roles" />
                                                <asp:HiddenField runat="server" ID="hfSuccessType_GridSettings_Roles" />
                                                 <asp:HiddenField runat="server" ID="hfComplete_GridSettings_Roles" />
                                                <asp:HiddenField runat="server" ID="hfSuccess_GridSettings_Roles" />--%>
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_ShiftsView_MonthlyExceptionShifts_CallbackComplete" />
                                                <CallbackError EventHandler="CallBack_ShiftsView_MonthlyExceptionShifts_onCallbackError" />
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
        <%-- strat Dialog--%>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPeriodRepeat"
            runat="server" Width="300px">
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 100%;"
                    meta:resourcekey="tblPeriodRepeat_DialogPeriodRepeat"
                    class="BodyStyle">
                    <tr>
                        <td id="Title_DialogPeriodRepeat" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bolder"></td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbPeriodRepeat" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbPeriodRepeat_MonthlyExceptionShifts" runat="server"
                                        ClientSideCommand="tlbItemEndorsement_TlbPeriodRepeat_MonthlyExceptionShifts_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbPeriodRepeat" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPeriodRepeat_MonthlyExceptionShifts" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbPeriodRepeat_MonthlyExceptionShifts_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPeriodRepeat"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lblFromDay_MonthlyExceptionShifts" runat="server" Text=": از روز" CssClass="WhiteLabel"
                                            meta:resourcekey="lblFromDay_MonthlyExceptionShifts"></asp:Label>
                                    </td>
                                    <td style="width: 35%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbFromDay_MonthlyExceptionShifts" OnCallback="CallBack_cmbFromDay_MonthlyExceptionShifts_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbFromDay_MonthlyExceptionShifts" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropDownHeight="190" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                                    HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                    SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                                                    Width="100">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbFromDay_MonthlyExceptionShifts_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_FromDay_MonthlyExceptionShifts" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_cmbFromDay_MonthlyExceptionShifts_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbFromDay_MonthlyExceptionShifts_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:Label ID="lblToDay_MonthlyExceptionShifts" runat="server" Text=": تا روز" CssClass="WhiteLabel"
                                            meta:resourcekey="lblToDay_MonthlyExceptionShifts"></asp:Label>
                                    </td>
                                    <td style="width: 35%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbToDay_MonthlyExceptionShifts" OnCallback="CallBack_cmbToDay_MonthlyExceptionShifts_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbToDay_MonthlyExceptionShifts" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DropDownHeight="190" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                                    HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                                    SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true"
                                                    Width="100">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbToDay_MonthlyExceptionShifts_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ToDay_MonthlyExceptionShifts" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_cmbToDay_MonthlyExceptionShifts_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbToDay_MonthlyExceptionShifts_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblHolidaysRemove_MonthlyExceptionShifts" runat="server" Text=": حذف تعطیلات"
                                            CssClass="WhiteLabel" meta:resourcekey="lblHolidaysRemove_MonthlyExceptionShifts"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbHolidays_MonthlyExceptionShifts" OnCallback="CallBack_cmbHolidays_MonthlyExceptionShifts_onCallBack">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbHolidays_MonthlyExceptionShifts" runat="server" AutoComplete="true"
                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                    DataTextField="Name" DataValueField="ID" DropImageUrl="Images/ComboBox/ddn.png"
                                                    DropDownHeight="120" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                    Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                    <DropDownContent>
                                                        <ComponentArt:TreeView ID="trvHolidays_MonthlyExceptionShifts" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                            CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                            EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                            ExpandImageUrl="images/TreeView/col.gif" Height="98%" HoverNodeCssClass="HoverTreeNode"
                                                            ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                            LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17"
                                                            NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true"
                                                            Width="100%" meta:resourcekey="trvHolidays_MonthlyExceptionShifts">
                                                            <ClientEvents>
                                                                <NodeCheckChange EventHandler="trvHolidays_MonthlyExceptionShifts_onNodeCheckChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:TreeView>
                                                    </DropDownContent>
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbHolidays_MonthlyExceptionShifts_onExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Holidays" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="cmbHolidays_MonthlyExceptionShifts_onBeforeCallback" />
                                                <CallbackError EventHandler="cmbHolidays_MonthlyExceptionShifts_onCallbackError" />
                                                <CallbackComplete EventHandler="cmbHolidays_MonthlyExceptionShifts_onCallbackComplete" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td style="width: 5%">
                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbHolidays_MonthlyExceptionShifts" runat="server" CssClass="toolbar"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbHolidays_MonthlyExceptionShifts"
                                                    runat="server" ClientSideCommand="Refresh_cmbHolidays_MonthlyExceptionShifts();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbHolidays_MonthlyExceptionShifts"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <%--end dialog--%>
        <asp:HiddenField runat="server" ID="hfTitle_DialogMonthlyExceptionShifts" meta:resourcekey="hfTitle_DialogMonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfheader_MonthlyExceptionShifts_MonthlyExceptionShifts" meta:resourcekey="hfheader_MonthlyExceptionShifts_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_MonthlyExceptionShifts" meta:resourcekey="hfDeleteMessage_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_MonthlyExceptionShifts" meta:resourcekey="hfCloseMessage_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCurrentYear_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCurrentMonth_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hffooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts" meta:resourcekey="hffooter_GridMonthlyExceptionShifts_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfErrorType_MonthlyExceptionShifts" meta:resourcekey="hfErrorType_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfConnectionError_MonthlyExceptionShifts" meta:resourcekey="hfConnectionError_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts" meta:resourcekey="hfloadingPanel_GridMonthlyExceptionShifts_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfEdit_MonthlyExceptionShifts" meta:resourcekey="hfEdit_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfDelete_MonthlyExceptionShifts" meta:resourcekey="hfDelete_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfSave_MonthlyExceptionShifts" meta:resourcekey="hfSave_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfCancel_MonthlyExceptionShifts" meta:resourcekey="hfCancel_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfShiftsObject_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfheader_ShiftsView_MonthlyExceptionShifts" meta:resourcekey="hfheader_ShiftsView_MonthlyExceptionShifts" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogPeriodRepeat" meta:resourcekey="hfTitle_DialogPeriodRepeat" />
        <%--<input type="button" id="btnTest" onclick="btnTest_onClick();"/>--%>
    </form>
</body>
</html>
