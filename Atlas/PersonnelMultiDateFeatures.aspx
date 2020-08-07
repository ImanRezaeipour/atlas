<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="PersonnelMultiDateFeatures" Codebehind="PersonnelMultiDateFeatures.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/PersianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PersonnelMultiDateFeaturesForm" runat="server" meta:resourcekey="PersonnelMultiDateFeaturesForm">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="/DesktopModules/Atlas/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Personnel_PersonnelMainInformation" style="width: 98%;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbPersonnelMultiDateFeatures" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbPersonnelMultiDateFeatures" runat="server"
                                            ClientSideCommand="tlbItemNew_TlbPersonnelMultiDateFeatures_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemNew_TlbPersonnelMultiDateFeatures" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbPersonnelMultiDateFeatures" runat="server"
                                            ClientSideCommand="tlbItemEdit_TlbPersonnelMultiDateFeatures_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemEdit_TlbPersonnelMultiDateFeatures" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbPersonnelMultiDateFeatures" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                                            ImageWidth="16px" ClientSideCommand="tlbItemDelete_TlbPersonnelMultiDateFeatures_onClick();"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbPersonnelMultiDateFeatures" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelMultiDateFeatures" runat="server"
                                            ClientSideCommand="tlbItemSave_TlbPersonnelMultiDateFeatures_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelMultiDateFeatures"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbPersonnelMultiDateFeatures" runat="server"
                                            ClientSideCommand="tlbItemCancel_TlbPersonnelMultiDateFeatures_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" Enabled="false" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbPersonnelMultiDateFeatures"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersonnelMultiDateFeatures" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonnelMultiDateFeatures" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbPersonnelMultiDateFeatures_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonnelMultiDateFeatures" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbPersonnelMultiDateFeatures_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonnelMultiDateFeatures"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelMultiDateFeatures" runat="server"
                                            ClientSideCommand="tlbItemExit_TlbPersonnelMultiDateFeatures_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelMultiDateFeatures" TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_PersonnelMultiDateFeatures" class="ToolbarMode"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblPersonnelMultiDateFeatureName_PersonnelMultiDateFeatures" runat="server"
                                    CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lblFromDate_PersonnelMultiDateFeatures" runat="server" Text=": از تاریخ"
                                    CssClass="WhiteLabel" meta:resourcekey="lblFromDate_PersonnelMultiDateFeatures"></asp:Label>
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lblToDate_PersonnelMultiDateFeatures" runat="server" Text=": تا تاریخ" CssClass="WhiteLabel"
                                    meta:resourcekey="lblToDate_PersonnelMultiDateFeatures"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" id="tdcmbMultiDateFeatures_PersonnelMultiDateFeatures">
                                <ComponentArt:CallBack ID="CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures" runat="server"
                                    OnCallback="CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onCallBack" Height="26">
                                    <Content>
                                        <ComponentArt:ComboBox ID="cmbMultiDateFeatures_PersonnelMultiDateFeatures" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropImageUrl="Images/ComboBox/ddn.png" Enabled="false" FocusedCssClass="comboBoxHover"
                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                            SelectedItemCssClass="comboItemHover" Style="width: 90%" TextBoxCssClass="comboTextBox"
                                            DataTextField="Name" DataValueField="ID" TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Expand EventHandler="cmbMultiDateFeatures_PersonnelMultiDateFeatures_onExpand" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures" />
                                    </Content>
                                    <ClientEvents>
                                        <BeforeCallback EventHandler="CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onBeforeCallback" />
                                        <CallbackComplete EventHandler="CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBackcmbMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                            <td valign="top" id="Container_FromDateCalendars_PersonnelMultiDateFeatures">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <table runat="server" id="Container_pdpFromDate_PersonnelMultiDateFeatures" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpFromDate_PersonnelMultiDateFeatures" runat="server" CssClass="PersianDatePicker"
                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpFromDate_PersonnelMultiDateFeatures" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalFromDate_PersonnelMultiDateFeatures" border="0" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpFromDate_PersonnelMultiDateFeatures_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpFromDate_PersonnelMultiDateFeatures" runat="server" ControlType="Picker"
                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                        SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpFromDate_PersonnelMultiDateFeatures_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp;
                                                                </td>
                                                                <td>
                                                                    <img id="btn_gdpFromDate_PersonnelMultiDateFeatures" alt="" class="calendar_button" onclick="btn_gdpFromDate_PersonnelMultiDateFeatures_OnClick(event)"
                                                                        onmouseup="btn_gdpFromDate_PersonnelMultiDateFeatures_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalFromDate_PersonnelMultiDateFeatures" runat="server" AllowMonthSelection="false"
                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_PersonnelMultiDateFeatures"
                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalFromDate_PersonnelMultiDateFeatures_OnChange" />
                                                                <Load EventHandler="gCalFromDate_PersonnelMultiDateFeatures_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" id="Container_ToDateCalendars_PersonnelMultiDateFeatures">
                                <table style="width: 100%">
                                    <tr>
                                        <td>

                                            <table runat="server" id="Container_pdpToDate_PersonnelMultiDateFeatures" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpToDate_PersonnelMultiDateFeatures" runat="server" CssClass="PersianDatePicker"
                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpToDate_PersonnelMultiDateFeatures" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalToDate_PersonnelMultiDateFeatures" border="0" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpToDate_PersonnelMultiDateFeatures_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpToDate_PersonnelMultiDateFeatures" runat="server" ControlType="Picker"
                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                        SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpToDate_PersonnelMultiDateFeatures_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp;
                                                                </td>
                                                                <td>
                                                                    <img id="btn_gdpToDate_PersonnelMultiDateFeatures" alt="" class="calendar_button" onclick="btn_gdpToDate_PersonnelMultiDateFeatures_OnClick(event)"
                                                                        onmouseup="btn_gdpToDate_PersonnelMultiDateFeatures_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalToDate_PersonnelMultiDateFeatures" runat="server" AllowMonthSelection="false"
                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_PersonnelMultiDateFeatures"
                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalToDate_PersonnelMultiDateFeatures_OnChange" />
                                                                <Load EventHandler="gCalToDate_PersonnelMultiDateFeatures_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="Container_TlbClear_ToDateCalendars_PersonnelMultiDateFeatures" runat="server" style="width: 5%" valign="top">
                                            <ComponentArt:ToolBar ID="TlbClear_ToDateCalendars_PersonnelMultiDateFeatures"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemClear_TlbClear_ToDateCalendars_PersonnelMultiDateFeatures"
                                                        runat="server" ClientSideCommand="tlbItemClear_TlbClear_ToDateCalendars_PersonnelMultiDateFeatures_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbClear_ToDateCalendars_PersonnelMultiDateFeatures"
                                                        TextImageSpacing="5" />
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
            <tr>
                <td>
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures" class="HeaderLabel" style="width: 50%">Personnel Rules Groups
                                        </td>
                                        <td id="loadingPanel_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
                                                        runat="server" ClientSideCommand="Refresh_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
                                    OnCallback="CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures" runat="server"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="11" RunningMode="Client" SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false"
                                            ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="240">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn DataField="RuleCategory.ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RuleCategory.Name" DefaultSortDirection="Descending"
                                                            HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
                                                            Visible="true" Width="80" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="UIFromDate" DefaultSortDirection="Descending"
                                                            HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
                                                            Visible="true" Width="80" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="UIToDate" DefaultSortDirection="Descending"
                                                            HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
                                                            Visible="true" Width="80" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onLoad" />
                                                <ItemSelect EventHandler="GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonnelMultiDateFeatures" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="320px">
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
                            <img id="Img1" runat="server" alt="" src="/DesktopModules/Atlas/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfheader_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
            meta:resourcekey="hfheader_PersonnelMultiDateFeatures_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelMultiDateFeatures" meta:resourcekey="hfTitle_DialogPersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfView_PersonnelMultiDateFeatures" meta:resourcekey="hfView_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfAdd_PersonnelMultiDateFeatures" meta:resourcekey="hfAdd_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfEdit_PersonnelMultiDateFeatures" meta:resourcekey="hfEdit_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfDelete_PersonnelMultiDateFeatures" meta:resourcekey="hfDelete_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_PersonnelMultiDateFeatures" meta:resourcekey="hfDeleteMessage_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_PersonnelMultiDateFeatures" meta:resourcekey="hfCloseMessage_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures"
            meta:resourcekey="hfloadingPanel_GridPersonnelMultiDateFeatures_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_PersonnelMultiDateFeatures" meta:resourcekey="hfcmbAlarm_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfErrorType_PersonnelMultiDateFeatures" meta:resourcekey="hfErrorType_PersonnelMultiDateFeatures" />
        <asp:HiddenField runat="server" ID="hfConnectionError_PersonnelMultiDateFeatures" meta:resourcekey="hfConnectionError_PersonnelMultiDateFeatures" />
    </form>
</body>
</html>
